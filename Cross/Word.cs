using System.Drawing;

namespace Cross
{
    public class Word
    {
        public string Text { get; private set; }
        public Point Position { get; set; }
        public bool IsHorizontal { get; set; }

        public Word(string text)
        {
            Text = text;
        }
    }
}
