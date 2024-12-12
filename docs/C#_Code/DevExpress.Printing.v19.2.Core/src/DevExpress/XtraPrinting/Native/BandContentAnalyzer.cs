namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class BandContentAnalyzer
    {
        private IEnumerable<BandBricksPair> docBands;

        public BandContentAnalyzer(IEnumerable<BandBricksPair> docBands);
        protected virtual bool AboveComparison(RectangleF rect, PointF pageBuilderOffset, float bound);
        public bool ExistsDetailBand(DocumentBand documentBand, RectangleF rectangle);
        public bool ExistsPrimaryContentAbove(float bound);
        public bool ExistsPrimaryContentBellow(float bound);
        private static bool HasPageBandInTree(DocumentBand documentBand);
        private static bool IsPrimaryBand(DocumentBand item);
    }
}

