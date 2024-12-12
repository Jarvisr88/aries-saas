namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;

    internal class SinglePrintingDocumentFactory : IDocumentFactory
    {
        PrintingDocument IDocumentFactory.Create(PrintingSystemBase ps) => 
            new DXSinglePrintingDocument(ps);
    }
}

