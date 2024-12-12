namespace DevExpress.XtraPrinting.Native
{
    using System;

    public class MarginDocumentBand : DocumentBand
    {
        private float height;

        public MarginDocumentBand(DocumentBandKind kind, float height);
        protected MarginDocumentBand(MarginDocumentBand source, int rowIndex);
        public override DocumentBand GetInstance(int rowIndex);
        internal void SetHeight(float height);

        public override float TotalHeight { get; }

        public override float SelfHeight { get; }

        protected internal override float Bottom { get; }
    }
}

