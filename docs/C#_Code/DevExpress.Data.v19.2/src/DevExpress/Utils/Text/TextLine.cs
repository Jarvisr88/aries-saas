namespace DevExpress.Utils.Text
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class TextLine
    {
        private static readonly int[] emptyWidths = new int[0];
        private TextLines lines;
        private string elipsisText;
        private int elipsisWidth;
        private int[] elipsisWidths;
        private char[] whiteSpaces;

        public TextLine(TextLines lines, int position, int length)
        {
            this.lines = lines;
            this.Position = position;
            this.Length = length;
            this.elipsisText = string.Empty;
            this.elipsisWidths = emptyWidths;
        }

        protected int[] GetCharactersWidth(string text) => 
            this.lines.GetCharacterWidths(text);

        public int GetCharWidth(int index) => 
            this.GetCharWidth(index, false);

        public int GetCharWidth(int index, bool includeItalic)
        {
            index += this.Position;
            return (this.Lines.Widths[index] + this.Lines.GetCharABCWidths(index));
        }

        protected bool GetIsTrimmingElipsis(StringTrimming trimming) => 
            (trimming == StringTrimming.EllipsisCharacter) || ((trimming == StringTrimming.EllipsisPath) || (trimming == StringTrimming.EllipsisWord));

        private int GetLastValuableSymbolIndex(string text) => 
            !string.IsNullOrEmpty(text) ? (text.TrimEnd(this.WhiteSpaces).Length - 1) : -1;

        protected int GetNextWord(int startIndex, int textLength) => 
            this.Lines.Draw.GetNextWord(startIndex, textLength);

        protected int GetTextWidth(int pos, int len) => 
            this.Lines.Draw.GetTextWidth(pos, len, false);

        private bool IsLineLast(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return true;
            }
            int lastValuableSymbolIndex = this.GetLastValuableSymbolIndex(text);
            return ((((this.Position + this.Length) - 1) == lastValuableSymbolIndex) || (lastValuableSymbolIndex < 0));
        }

        private bool ShouldProceed(bool isLineLast, int currentLen)
        {
            if (!isLineLast)
            {
                return (currentLen >= this.Length);
            }
            this.HasEllipsis = false;
            return true;
        }

        private void TrimmTextToWord(string text, bool wordWrap, int lineLength, int drawWidth, out int len, out int usedLineWidth)
        {
            len = 0;
            usedLineWidth = 0;
            int textLength = wordWrap ? this.Lines.Draw.Text.Length : lineLength;
            int nextWord = this.GetNextWord(this.Position, textLength);
            for (int i = this.GetTextWidth(this.Position, nextWord); i <= drawWidth; i = this.GetTextWidth(this.Position, len + nextWord))
            {
                usedLineWidth = i;
                len += nextWord;
                if (wordWrap && this.ShouldProceed(this.IsLineLast(text), len))
                {
                    return;
                }
                nextWord = this.GetNextWord(this.Position + len, this.Position + textLength);
            }
        }

        public void UpdateTrimmingLine(StringTrimming trimming, int drawBoundsWidth)
        {
            this.UpdateTrimmingLine(trimming, drawBoundsWidth, false, string.Empty);
        }

        public void UpdateTrimmingLine(StringTrimming trimming, int drawBoundsWidth, bool wordWrap, string text)
        {
            if ((trimming != StringTrimming.None) && ((this.TextWidth > drawBoundsWidth) || wordWrap))
            {
                this.HasEllipsis = this.GetIsTrimmingElipsis(trimming);
                int drawWidth = drawBoundsWidth - this.EllipsisWidth;
                int length = this.Length;
                switch (trimming)
                {
                    case StringTrimming.Character:
                    case StringTrimming.EllipsisCharacter:
                    {
                        int usedLineWidth = 0;
                        int len = 0;
                        this.TrimmTextToWord(text, wordWrap, length, drawWidth, out len, out usedLineWidth);
                        while ((usedLineWidth + this.GetCharWidth(len, true)) <= drawWidth)
                        {
                            usedLineWidth += this.GetCharWidth(len++);
                            if (wordWrap && this.ShouldProceed(this.IsLineLast(text), len))
                            {
                                return;
                            }
                        }
                        this.Length = len;
                        return;
                    }
                    case StringTrimming.Word:
                    case StringTrimming.EllipsisWord:
                    {
                        int usedLineWidth = 0;
                        int len = 0;
                        this.TrimmTextToWord(text, wordWrap, length, drawWidth, out len, out usedLineWidth);
                        if (wordWrap)
                        {
                            if (len == 0)
                            {
                                len = text.Length - this.Position;
                                return;
                            }
                            if (string.Equals(this.Lines.Draw.Text[(this.Position + len) - 1].ToString(), " "))
                            {
                                int num12 = (this.Position + len) - 1;
                                while (true)
                                {
                                    if (num12 >= this.Position)
                                    {
                                        char ch = this.Lines.Draw.Text[num12];
                                        if (string.Equals(ch.ToString(), " "))
                                        {
                                            num12--;
                                            continue;
                                        }
                                    }
                                    if (num12 != ((this.Position + len) - 1))
                                    {
                                        len = (num12 - this.Position) + 1;
                                    }
                                    break;
                                }
                            }
                        }
                        this.Length = len;
                        return;
                    }
                    case StringTrimming.EllipsisPath:
                    {
                        int num7 = 0;
                        int num8 = 0;
                        int index = 0;
                        int num10 = length - 1;
                        for (int i = this.GetCharWidth(0); ((num7 + i) + num8) < drawWidth; i = ((num8 >= num7) || (num10 <= 0)) ? this.GetCharWidth(index) : this.GetCharWidth(num10))
                        {
                            if ((num8 < num7) && (num10 > 0))
                            {
                                num8 += this.GetCharWidth(num10--);
                            }
                            else
                            {
                                num7 += this.GetCharWidth(index++);
                            }
                            if (((this.Lines.Widths.Length == (index + this.Position)) || (num10 < 0)) && !this.IsLineFirst)
                            {
                                this.Position2 = this.Lines.Widths.Length - 1;
                                this.Length2 = 0;
                                this.HasEllipsis = false;
                                return;
                            }
                        }
                        num10++;
                        this.Position2 = num10;
                        this.Length2 = length - num10;
                        if (wordWrap && !this.IsLineFirst)
                        {
                            this.Position2 += this.Position;
                        }
                        this.Length = index;
                        return;
                    }
                }
            }
        }

        public int Position { get; private set; }

        public int Length { get; private set; }

        public int Position2 { get; private set; }

        public int Length2 { get; private set; }

        public int TextWidth =>
            this.GetTextWidth(this.Position, this.Length);

        public int Text2Width =>
            this.GetTextWidth(this.Position2, this.Length2);

        public int LineWidth =>
            (this.TextWidth + this.EllipsisWidth) + this.Text2Width;

        public string EllipsisText =>
            this.elipsisText;

        public int EllipsisWidth =>
            this.elipsisWidth;

        public int[] EllipsisWidths =>
            this.elipsisWidths;

        public bool HasEllipsis
        {
            get => 
                this.elipsisText.Length > 0;
            set
            {
                if (this.HasEllipsis != value)
                {
                    this.elipsisWidth = 0;
                    this.elipsisWidths = emptyWidths;
                    if (!value)
                    {
                        this.elipsisText = string.Empty;
                    }
                    else
                    {
                        this.elipsisText = "…";
                        this.elipsisWidths = this.GetCharactersWidth(this.elipsisText);
                        for (int i = 0; i < this.elipsisWidths.Length; i++)
                        {
                            this.elipsisWidth += this.elipsisWidths[i];
                        }
                    }
                }
            }
        }

        private bool IsLineFirst =>
            this.Position == 0;

        private char[] WhiteSpaces
        {
            get
            {
                if (this.whiteSpaces == null)
                {
                    List<char> list = new List<char>();
                    list.AddRange(Environment.NewLine.ToCharArray());
                    char[] collection = new char[] { ' ', '\v', '\r' };
                    list.AddRange(collection);
                    this.whiteSpaces = list.ToArray();
                }
                return this.whiteSpaces;
            }
        }

        protected TextLines Lines =>
            this.lines;

        protected int[] Widths =>
            this.lines.Widths;

        public Point DrawPoint { get; internal set; }

        public string DrawText { get; internal set; }

        public int[] DrawWidths { get; internal set; }
    }
}

