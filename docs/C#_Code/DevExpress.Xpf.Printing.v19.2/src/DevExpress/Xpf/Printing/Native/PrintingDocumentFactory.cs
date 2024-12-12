namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;

    internal class PrintingDocumentFactory : IDocumentFactory
    {
        public PrintingDocument Create(PrintingSystemBase ps) => 
            new DXPrintingDocument(ps);
    }
}

