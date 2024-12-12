namespace DevExpress.XtraPrinting.Native.TOC
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    internal class TableOfContentsLineTextGenerator : ITextGenerator
    {
        private ITextFormatter textFormatter;

        protected virtual ITextFormatter CreateTextFormatter(System.Drawing.GraphicsUnit graphicsUnit, DevExpress.XtraPrinting.Native.Measurer measurer);
        private string GenerateLastLine(string captionLine, float maxTextWidth);
        public string GenerateText(float maxTextWidth);

        public DevExpress.XtraPrinting.Native.Measurer Measurer { get; set; }

        public System.Drawing.GraphicsUnit GraphicsUnit { get; set; }

        public char LeaderSymbol { get; set; }

        public System.Drawing.Font Font { get; set; }

        public BrickStyle Style { get; set; }

        public string Caption { get; set; }

        public float TextDivisionPosition { get; set; }
    }
}

