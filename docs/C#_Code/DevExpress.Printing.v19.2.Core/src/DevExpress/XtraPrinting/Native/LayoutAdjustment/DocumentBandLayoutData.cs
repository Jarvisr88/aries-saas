namespace DevExpress.XtraPrinting.Native.LayoutAdjustment
{
    using DevExpress.XtraPrinting.Native;
    using System;

    public class DocumentBandLayoutData : UnprintLayoutData
    {
        private DocumentBand band;

        public DocumentBandLayoutData(DocumentBand band, float dpi);
        public override void SetBoundsY(float y);

        protected override float Y { get; }
    }
}

