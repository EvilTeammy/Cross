using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace Cross
{
    public class CrissCrossForm : Form
    {
        private WordArea area = new WordArea();

        public CrissCrossForm()
        {
            this.Text = "Criss Cross";
            this.FormClosing += (sender, e) => Application.Exit();
            this.Controls.Add(area);
            this.MouseClick += new MouseEventHandler(MouseHandler);

            // Увеличиваем размер окна
            this.MinimumSize = new Size(area.Width + 50, area.Height + 50); // Увеличим немного больше, чтобы оставить место для рамок окна
            this.Size = new Size(area.Width + 100, area.Height + 100);

            LoadWords();
        }

        private void MouseHandler(object sender, MouseEventArgs e)
        {
            area.NextArea();
            this.MinimumSize = new Size(area.Width + 50, area.Height + 50);
            this.Size = new Size(area.Width + 100, area.Height + 100);
        }

        private void LoadWords()
        {
            var wordLoader = new WordLoader();
            var words = wordLoader.LoadWords("words.txt", Encoding.UTF8);

            // Пример добавления слов в область
            foreach (var wordText in words)
            {
                var word = new Word(wordText);
                if (!area.PlaceWord(word))
                {
                    MessageBox.Show("Не удалось разместить слово: " + wordText);
                }
            }
        }
    }
}
