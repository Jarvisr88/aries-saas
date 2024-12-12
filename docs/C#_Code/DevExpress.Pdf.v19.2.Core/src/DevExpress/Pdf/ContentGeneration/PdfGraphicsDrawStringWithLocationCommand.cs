namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Text.Fonts;
    using System;
    using System.Drawing;

    public class PdfGraphicsDrawStringWithLocationCommand : PdfGraphicsDrawStringCommand
    {
        private readonly PointF location;
        private readonly PdfGraphicsTextOrigin textOrigin;
        private readonly bool useKerning;
        private readonly PdfStringFormat format;

        public PdfGraphicsDrawStringWithLocationCommand(string text, PdfExportFontInfo fontInfo, PointF location, PdfStringFormat format, PdfGraphicsTextOrigin textOrigin, SolidBrush foreBrush, bool useKerning) : base(text, fontInfo, foreBrush)
        {
            this.location = location;
            this.textOrigin = textOrigin;
            this.useKerning = useKerning;
            this.format = new PdfStringFormat(format);
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            base.Execute(constructor, page);
            constructor.DrawString(base.Text, base.FontInfo, this.location, this.format, this.textOrigin, this.useKerning ? DXKerningMode.Always : DXKerningMode.None);
        }
    }
}

