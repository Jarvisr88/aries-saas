namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Text.Fonts;
    using System;
    using System.Drawing;

    public class PdfGraphicsDrawFormattedStringCommand : PdfGraphicsDrawStringCommand
    {
        private readonly RectangleF layout;
        private readonly PdfStringFormat format;
        private readonly bool useKerning;

        public PdfGraphicsDrawFormattedStringCommand(string text, PdfExportFontInfo fontInfo, RectangleF layout, PdfStringFormat format, SolidBrush foreBrush, bool useKerning) : base(text, fontInfo, foreBrush)
        {
            this.layout = layout;
            this.format = new PdfStringFormat(format);
            this.useKerning = useKerning;
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            base.Execute(constructor, page);
            constructor.DrawString(base.Text, base.FontInfo, this.layout, this.format, this.useKerning ? DXKerningMode.Always : DXKerningMode.None);
        }
    }
}

