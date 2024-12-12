namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using System;
    using System.Drawing;

    public abstract class PdfGraphicsNonStrokingCommand : PdfGraphicsCommand
    {
        private readonly DXBrush brush;

        protected PdfGraphicsNonStrokingCommand(System.Drawing.Brush brush)
        {
            if (brush == null)
            {
                throw new ArgumentNullException("brush");
            }
            this.brush = PdfGDIPlusGraphicsConverter.ConvertBrush(brush);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                this.brush.Dispose();
            }
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            constructor.SetBrush(this.brush);
        }

        protected DXBrush Brush =>
            this.brush;
    }
}

