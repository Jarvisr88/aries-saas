namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class PSDocument : PrintingDocument
    {
        protected DocumentBand currentDetailContainer;
        private Stack<DocumentBand> detailContainers;
        private DocumentBand activeBand;

        public PSDocument(PrintingSystemBase ps);
        protected internal override void AddBrick(Brick brick);
        protected internal override DocumentBand AddReportContainer();
        protected internal override void AfterBuild();
        protected internal override void Begin();
        protected internal override void BeginReport(DocumentBand docBand, PointF offset);
        private DocumentBand CreateDetailBand();
        protected internal override void Dispose(bool disposing);
        protected internal override void EndReport();
        protected internal override void InsertPageBreak(float pos);
        protected internal override void InsertPageBreak(float pos, CustomPageData nextPageData);
        protected virtual void OnModifierChanged(BrickModifier modifier);
        private void OnModifierChanged(object sender, EventArgs e);
        public override void ShowFromNewPage(Brick brick);

        public DocumentBand ActiveBand { get; protected set; }
    }
}

