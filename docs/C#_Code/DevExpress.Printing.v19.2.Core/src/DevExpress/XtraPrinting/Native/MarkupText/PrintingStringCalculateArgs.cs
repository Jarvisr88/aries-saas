namespace DevExpress.XtraPrinting.Native.MarkupText
{
    using DevExpress.Utils.Text;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class PrintingStringCalculateArgs : StringCalculateArgsBase
    {
        public readonly BrickStyle Style;
        public readonly IMeasurer Measurer;

        public PrintingStringCalculateArgs(MarkupTextBrick markupTextBrick, Rectangle bounds);
        public PrintingStringCalculateArgs(string text, Rectangle bounds, BrickStyle style, IMeasurer measurer, ImageItemCollection imageResources);
        private static IMeasurer GetMeasurer(MarkupTextBrick markupTextBrick);
    }
}

