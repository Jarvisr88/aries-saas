namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;

    public class PdfGraphicsRestoreGraphicsStateCommand : PdfGraphicsCommand
    {
        public static readonly PdfGraphicsRestoreGraphicsStateCommand Instance = new PdfGraphicsRestoreGraphicsStateCommand();

        private PdfGraphicsRestoreGraphicsStateCommand()
        {
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            constructor.RestoreGraphicsState();
        }
    }
}

