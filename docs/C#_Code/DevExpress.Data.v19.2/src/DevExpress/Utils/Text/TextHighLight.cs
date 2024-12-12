namespace DevExpress.Utils.Text
{
    using DevExpress.Data;
    using System;
    using System.Drawing;

    public class TextHighLight
    {
        private DisplayTextHighlightRange[] ranges;
        private Color backColor;
        private Color foreColor;

        public TextHighLight(DisplayTextHighlightRange[] ranges, Color backColor, Color foreColor)
        {
            this.ranges = ranges;
            this.backColor = backColor;
            this.foreColor = foreColor;
        }

        public TextHighLight(int startIndex, int length, Color backColor, Color foreColor) : this(rangeArray1, backColor, foreColor)
        {
            DisplayTextHighlightRange[] rangeArray1 = new DisplayTextHighlightRange[] { new DisplayTextHighlightRange(startIndex, length) };
        }

        public bool IsTextHighLighted(int textPos, int textLen)
        {
            int num = textPos + textLen;
            for (int i = 0; i < this.Ranges.Length; i++)
            {
                DisplayTextHighlightRange range = this.Ranges[i];
                int start = range.Start;
                int num4 = start + range.Length;
                if ((((textPos <= start) && (num >= start)) || ((num >= num4) && (textPos <= num4))) || ((textPos >= start) && (num <= num4)))
                {
                    return true;
                }
            }
            return false;
        }

        public int StartIndex
        {
            get => 
                ((this.Ranges == null) || (this.Ranges.Length == 0)) ? 0 : this.Ranges[0].Start;
            set
            {
                if ((this.Ranges == null) || (this.Ranges.Length == 0))
                {
                    this.Ranges = new DisplayTextHighlightRange[1];
                }
                this.Ranges[0].SetStart(value);
            }
        }

        public int Length
        {
            get => 
                ((this.Ranges == null) || (this.Ranges.Length == 0)) ? 0 : this.Ranges[0].Length;
            set
            {
                if ((this.Ranges == null) || (this.Ranges.Length == 0))
                {
                    this.Ranges = new DisplayTextHighlightRange[1];
                }
                this.Ranges[0].SetLength(value);
            }
        }

        public int EndIndex =>
            ((this.Ranges == null) || (this.Ranges.Length == 0)) ? 0 : (this.Ranges[0].Start + this.Ranges[0].Length);

        public DisplayTextHighlightRange[] Ranges
        {
            get => 
                this.ranges;
            set => 
                this.ranges = value;
        }

        public Color BackColor =>
            this.backColor;

        public Color ForeColor =>
            this.foreColor;
    }
}

