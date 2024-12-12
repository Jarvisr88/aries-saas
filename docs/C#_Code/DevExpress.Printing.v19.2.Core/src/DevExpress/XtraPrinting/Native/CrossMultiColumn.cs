namespace DevExpress.XtraPrinting.Native
{
    using System;

    public class CrossMultiColumn : MultiColumn
    {
        private float headerWidth;
        private float totalWidth;
        private bool acrossOnly;

        protected CrossMultiColumn(CrossMultiColumn source);
        public CrossMultiColumn(int columnCount, float columnWidth, float headerWidth, float totalWidth, bool acrossOnly);
        public override MultiColumn Clone();
        public override float GetColumnWidth(DocumentBand docBand);
        public override float GetPageWidth(float defaultValue);
        public override void Scale(double ratio);

        public override bool IsAcross { get; }
    }
}

