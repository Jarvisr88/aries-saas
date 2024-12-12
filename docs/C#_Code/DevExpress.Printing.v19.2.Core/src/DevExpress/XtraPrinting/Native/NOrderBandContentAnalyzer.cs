namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    internal class NOrderBandContentAnalyzer : BandContentAnalyzer
    {
        private double offsetX;

        public NOrderBandContentAnalyzer(IEnumerable<BandBricksPair> docBands, double offsetX);
        protected override bool AboveComparison(RectangleF rect, PointF pageBuilderOffset, float bound);
    }
}

