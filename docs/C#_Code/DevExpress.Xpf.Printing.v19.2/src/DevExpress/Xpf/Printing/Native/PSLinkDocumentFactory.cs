namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;

    internal class PSLinkDocumentFactory : IDocumentFactory
    {
        public PrintingDocument Create(PrintingSystemBase ps) => 
            new PSLinkDocument(ps);
    }
}

