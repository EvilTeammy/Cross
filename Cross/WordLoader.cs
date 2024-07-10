using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cross
{
    public class WordLoader
    {
        public List<string> LoadWords(string filePath, Encoding encoding)
        {
            var words = new HashSet<string>();

            try
            {
                foreach (var line in File.ReadLines(filePath, encoding))
                {
                    var word = line.Trim();
                    if (!string.IsNullOrEmpty(word))
                    {
                        words.Add(word);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show($"Файл '{filePath}' не найден.", "Ошибка загрузки файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке файла: {ex.Message}", "Ошибка загрузки файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return words.ToList();
        }
    }
}
