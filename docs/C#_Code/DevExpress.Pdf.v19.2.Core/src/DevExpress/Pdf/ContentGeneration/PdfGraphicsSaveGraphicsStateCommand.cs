namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;

    public class PdfGraphicsSaveGraphicsStateCommand : PdfGraphicsCommand
    {
        public static readonly PdfGraphicsSaveGraphicsStateCommand Instance = new PdfGraphicsSaveGraphicsStateCommand();

        private PdfGraphicsSaveGraphicsStateCommand()
        {
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            constructor.SaveGraphicsState();
        }
    }
}

