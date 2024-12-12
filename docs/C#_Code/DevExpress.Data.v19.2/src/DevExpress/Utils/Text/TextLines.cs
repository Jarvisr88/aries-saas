namespace DevExpress.Utils.Text
{
    using System;
    using System.Reflection;

    public class TextLines
    {
        private TextOutDraw draw;
        private int count;
        private TextLine[] lines;

        public TextLines(TextOutDraw draw)
        {
            this.draw = draw;
            this.lines = new TextLine[1];
        }

        protected void Add(TextLine line)
        {
            if (this.count >= this.lines.Length)
            {
                TextLine[] destinationArray = new TextLine[this.lines.Length + 3];
                Array.Copy(this.lines, destinationArray, this.lines.Length);
                this.lines = destinationArray;
            }
            int count = this.count;
            this.count = count + 1;
            this.lines[count] = line;
        }

        public void Add(int position, int length)
        {
            this.AddLine(position, length);
        }

        protected TextLine AddLine(int position, int length)
        {
            TextLine line = new TextLine(this, position, length);
            this.Add(line);
            return line;
        }

        public int GetCharABCWidths(int position) => 
            this.Draw.GetCharABCWidths(position);

        public int[] GetCharacterWidths(string text) => 
            this.Draw.GetCharactersWidth(text);

        public int Count =>
            this.count;

        public TextLine this[int index] =>
            (index < this.count) ? this.lines[index] : null;

        public int Width
        {
            get
            {
                int num = 0;
                for (int i = 0; i < this.Count; i++)
                {
                    int lineWidth = this[i].LineWidth;
                    if (num < lineWidth)
                    {
                        num = lineWidth;
                    }
                }
                return num;
            }
        }

        public int[] Widths =>
            this.Draw.Widths;

        public TextOutDraw Draw =>
            this.draw;
    }
}

