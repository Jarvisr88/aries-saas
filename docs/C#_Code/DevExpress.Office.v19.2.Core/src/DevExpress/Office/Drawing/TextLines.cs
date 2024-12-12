namespace DevExpress.Office.Drawing
{
    using DevExpress.Utils.Text;
    using System;
    using System.Reflection;

    internal class TextLines
    {
        private TextOutDraw draw;
        private int count;
        private DevExpress.Office.Drawing.TextLine[] lines;

        public TextLines(TextOutDraw draw)
        {
            this.draw = draw;
            this.lines = new DevExpress.Office.Drawing.TextLine[1];
        }

        protected void Add(DevExpress.Office.Drawing.TextLine line)
        {
            if (this.count >= this.lines.Length)
            {
                DevExpress.Office.Drawing.TextLine[] destinationArray = new DevExpress.Office.Drawing.TextLine[this.lines.Length + 3];
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

        protected DevExpress.Office.Drawing.TextLine AddLine(int position, int length)
        {
            DevExpress.Office.Drawing.TextLine line = new DevExpress.Office.Drawing.TextLine(this, position, length);
            this.Add(line);
            return line;
        }

        public int GetCharABCWidths(int position) => 
            this.Draw.GetCharABCWidths(position);

        public int[] GetCharacterWidths(string text) => 
            this.Draw.GetCharactersWidth(text);

        public int Count =>
            this.count;

        public DevExpress.Office.Drawing.TextLine this[int index] =>
            (index < this.count) ? this.lines[index] : null;

        public int[] Widths =>
            this.Draw.Widths;

        public TextOutDraw Draw =>
            this.draw;
    }
}

