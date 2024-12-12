namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.PageBuilder;
    using System;

    internal class DXSinglePrintingDocument : DXPrintingDocument
    {
        public DXSinglePrintingDocument(PrintingSystemBase ps) : base(ps)
        {
        }

        protected override PageBuildEngine CreatePageBuildEngine(bool buildPagesInBackground, bool rollPaper) => 
            new SinglePageBuildEngine(this);
    }
}

