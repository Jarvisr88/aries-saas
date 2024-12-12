namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using System;
    using System.Drawing;

    public abstract class PdfGraphicsDrawStringCommand : PdfGraphicsCommand
    {
        private readonly string text;
        private readonly PdfExportFontInfo fontInfo;
        private readonly DXSolidBrush foreBrush;

        protected PdfGraphicsDrawStringCommand(string text, PdfExportFontInfo fontInfo, SolidBrush foreBrush)
        {
            this.text = text;
            this.fontInfo = fontInfo;
            this.foreBrush = new DXSolidBrush(PdfGDIPlusGraphicsConverter.ConvertColor(foreBrush.Color));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                this.foreBrush.Dispose();
            }
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            constructor.SetBrush(this.foreBrush);
            if (this.fontInfo.ShouldSetStrokingColor)
            {
                constructor.SetUnscaledPen(this.foreBrush.Color, this.fontInfo.FontLineSize);
            }
        }

        protected string Text =>
            this.text;

        protected PdfExportFontInfo FontInfo =>
            this.fontInfo;
    }
}

