namespace DevExpress.XtraPrinting.Native.PageBuilder
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;

    internal class SinglePageLinkDocument : PSLinkDocument
    {
        public SinglePageLinkDocument(PrintingSystemBase ps);
        protected override PageBuildEngine CreatePageBuildEngine(bool buildPagesInBackground, bool rollPaper);
    }
}

