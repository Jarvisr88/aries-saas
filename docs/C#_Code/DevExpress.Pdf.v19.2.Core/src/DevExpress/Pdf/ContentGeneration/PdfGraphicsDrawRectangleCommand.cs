namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;
    using System.Drawing;

    public class PdfGraphicsDrawRectangleCommand : PdfGraphicsStrokingCommand
    {
        private readonly RectangleF bounds;

        public PdfGraphicsDrawRectangleCommand(Pen pen, RectangleF bounds) : base(pen)
        {
            this.bounds = bounds;
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            base.Execute(constructor, page);
            constructor.DrawRectangle(this.bounds);
        }
    }
}

