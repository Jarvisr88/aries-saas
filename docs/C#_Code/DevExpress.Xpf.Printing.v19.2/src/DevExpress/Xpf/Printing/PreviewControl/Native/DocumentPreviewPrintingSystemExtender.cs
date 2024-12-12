namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;

    public class DocumentPreviewPrintingSystemExtender : PrintingSystemExtenderPrint
    {
        private readonly DocumentPreviewProgressReflector progressReflector;

        public DocumentPreviewPrintingSystemExtender(PrintingSystemBase printingSystem, DocumentPreviewProgressReflector progressReflector) : base(printingSystem)
        {
            this.progressReflector = progressReflector;
        }

        public override ProgressReflector ActiveProgressReflector =>
            this.progressReflector;
    }
}

