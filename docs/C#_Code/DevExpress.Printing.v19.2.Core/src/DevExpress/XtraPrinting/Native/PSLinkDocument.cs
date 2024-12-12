namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;

    public class PSLinkDocument : PSDocument
    {
        private MarginDocumentBand topMargin;
        private MarginDocumentBand bottomMargin;
        private DocumentBand pageHeader;
        private DocumentBand pageFooter;
        private DocumentBand reportHeader;
        private DocumentBand reportFooter;

        public PSLinkDocument(PrintingSystemBase ps);
        protected internal override void Begin();
        protected internal override void End(bool buildPagesInBackground);
        private DocumentBand GetBand(DocumentBandKind bandKind);
        protected internal override void HandleNewPageSettings();
        protected override void OnModifierChanged(BrickModifier modifier);

        private float PageHeaderHeight { get; }

        private float PageFooterHeight { get; }
    }
}

