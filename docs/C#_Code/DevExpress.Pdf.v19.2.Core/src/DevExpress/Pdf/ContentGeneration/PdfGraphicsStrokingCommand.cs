namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using System;
    using System.Drawing;

    public abstract class PdfGraphicsStrokingCommand : PdfGraphicsCommand
    {
        private readonly DXPen pen;

        protected PdfGraphicsStrokingCommand(Pen pen)
        {
            if (pen == null)
            {
                throw new ArgumentNullException("pen");
            }
            this.pen = PdfGDIPlusGraphicsConverter.ConvertPen(pen);
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            constructor.SetPen(this.pen);
        }
    }
}

