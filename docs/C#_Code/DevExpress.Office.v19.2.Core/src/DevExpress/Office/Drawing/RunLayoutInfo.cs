namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    internal class RunLayoutInfo
    {
        private RectangleF bounds;

        public RunLayoutInfo(IDrawingTextCharacterProperties properties, FontInfo runFontInfo, FontInfo baseFontInfo, string text)
        {
            this.Properties = properties;
            this.Text = text;
            this.RunFontInfo = runFontInfo;
            this.BaseFontInfo = baseFontInfo;
        }

        public void OffsetX(float x)
        {
            this.bounds.Offset(x, 0f);
        }

        public void OffsetY(float y)
        {
            this.bounds.Offset(0f, y);
        }

        public IDrawingTextCharacterProperties Properties { get; private set; }

        public FontInfo RunFontInfo { get; private set; }

        public FontInfo BaseFontInfo { get; private set; }

        public string Text { get; private set; }

        public RectangleF Bounds
        {
            get => 
                this.bounds;
            set => 
                this.bounds = value;
        }
    }
}

