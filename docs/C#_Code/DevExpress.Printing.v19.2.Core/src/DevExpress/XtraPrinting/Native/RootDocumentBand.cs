namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;

    public class RootDocumentBand : DocumentBand, IPageSection
    {
        private PrintingSystemBase printingSystem;
        private DevExpress.XtraPrinting.Native.MultiColumn multiColumn;
        private bool completed;

        public RootDocumentBand(PrintingSystemBase printingSystem);
        DocumentBand IPageSection.GetBottomMargin();
        DocumentBand IPageSection.GetTopMargin();
        public override void Scale(double scaleFactor, Predicate<DocumentBand> shouldScale);

        public bool Completed { get; set; }

        public PrintingSystemBase PrintingSystem { get; }

        public override bool HasDataSource { get; }

        public override bool IsRoot { get; }

        public override DevExpress.XtraPrinting.Native.MultiColumn MultiColumn { get; set; }

        DocumentBand IPageSection.Container { get; }
    }
}

