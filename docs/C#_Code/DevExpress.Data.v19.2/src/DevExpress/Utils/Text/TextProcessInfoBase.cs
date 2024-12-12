namespace DevExpress.Utils.Text
{
    using DevExpress.Utils.Text.Internal;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class TextProcessInfoBase
    {
        public object Context;
        public StringInfoBase Info;
        public StringBlock Block;
        public bool AllowMultiLine;
        public Rectangle Bounds;
        public Point End;
        public int LineHeight;
        public int LineNumber;
        public bool IsNewLine;
        public Point CurrentPosition;

        public TextProcessInfoBase()
        {
            this.LineHeight = this.LineNumber = 0;
            this.End = Point.Empty;
            this.IsNewLine = true;
        }

        public int CurrentX
        {
            get => 
                this.CurrentPosition.X;
            set => 
                this.CurrentPosition.X = value;
        }

        public int CurrentY
        {
            get => 
                this.CurrentPosition.Y;
            set => 
                this.CurrentPosition.Y = value;
        }

        public bool RoundTextHeight { get; set; }
    }
}

