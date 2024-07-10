using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Cross
{
    public class WordArea : Panel
    {
        private List<Word> words = new List<Word>();
        private CharCell[,] grid;

        public WordArea()
        {
            // Увеличиваем размер сетки
            grid = new CharCell[30, 30]; // Увеличим размер до 30x30
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    grid[i, j] = new CharCell();
                }
            }

            // Устанавливаем размеры панели, чтобы соответствовать размеру сетки
            this.Width = 30 * 20; // 30 клеток по 20 пикселей каждая
            this.Height = 30 * 20; // 30 клеток по 20 пикселей каждая
        }

        public bool PlaceWord(Word word)
        {
            if (words.Count == 0)
            {
                // Разместим первое слово в центре
                AddWord(word, grid.GetLength(0) / 2, grid.GetLength(1) / 2, true);
                return true;
            }

            foreach (var existingWord in words)
            {
                for (int i = 0; i < existingWord.Text.Length; i++)
                {
                    for (int j = 0; j < word.Text.Length; j++)
                    {
                        if (existingWord.Text[i] == word.Text[j])
                        {
                            int x = existingWord.Position.X;
                            int y = existingWord.Position.Y;

                            if (existingWord.IsHorizontal)
                            {
                                x += i;
                                y -= j;

                                if (CanPlaceWord(word, x, y, false))
                                {
                                    AddWord(word, x, y, false);
                                    return true;
                                }
                            }
                            else
                            {
                                x -= j;
                                y += i;

                                if (CanPlaceWord(word, x, y, true))
                                {
                                    AddWord(word, x, y, true);
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false; // Не удалось разместить слово
        }

        private bool CanPlaceWord(Word word, int x, int y, bool isHorizontal)
        {
            if (isHorizontal)
            {
                if (x < 0 || x + word.Text.Length > grid.GetLength(0) || y < 0 || y >= grid.GetLength(1))
                    return false;

                for (int i = 0; i < word.Text.Length; i++)
                {
                    if (grid[x + i, y].IsOccupied && grid[x + i, y].Character != word.Text[i])
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (x < 0 || x >= grid.GetLength(0) || y < 0 || y + word.Text.Length > grid.GetLength(1))
                    return false;

                for (int i = 0; i < word.Text.Length; i++)
                {
                    if (grid[x, y + i].IsOccupied && grid[x, y + i].Character != word.Text[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void AddWord(Word word, int x, int y, bool isHorizontal)
        {
            word.Position = new Point(x, y);
            word.IsHorizontal = isHorizontal;
            words.Add(word);

            // Размещение слова в сетке
            for (int i = 0; i < word.Text.Length; i++)
            {
                if (isHorizontal)
                    grid[x + i, y].Character = word.Text[i];
                else
                    grid[x, y + i].Character = word.Text[i];
            }
            Invalidate(); // Перерисовка панели
        }

        public void NextArea()
        {
            // Логика перехода к следующей области, если необходимо
            Invalidate(); // Перерисовка панели
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            // Пример рисования сетки
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    var cell = grid[i, j];
                    var rect = new Rectangle(i * 20, j * 20, 20, 20);
                    g.DrawRectangle(Pens.Black, rect);
                    if (cell.IsOccupied)
                    {
                        g.DrawString(cell.Character.ToString(), this.Font, Brushes.Black, rect);
                    }
                }
            }
        }
    }
}
