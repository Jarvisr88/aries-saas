namespace DevExpress.XtraPrinting.Native.LayoutAdjustment
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public class LayoutDataContext
    {
        private PageRowBuilderBase pageRowBuilderBase;
        private DocumentBand rootBand;
        private RectangleF bounds;
        private float startOffsetY;
        private FillPageResult result;
        private OffsetHelperY offsetHelper;
        private List<List<BandBricksPair>> bandList;
        private List<Pair<Range, List<BandBricksPair>>> bandPairList;
        private float startNegativeOffsetY;

        public LayoutDataContext(PageRowBuilderBase pageRowBuilderBase, DocumentBand rootBand, RectangleF bounds);
        public void Commit();
        public bool FillPage(SubreportDocumentBandLayoutData layoutData, out float offsetY);
        private bool MergeBands(ref Pair<Range, List<BandBricksPair>> samplePair, List<Pair<Range, List<BandBricksPair>>> bandsDictionary);

        public List<List<BandBricksPair>> BandList { get; }

        public FillPageResult Result { get; }

        public PageRowBuilderBase PageRowBuilder { get; }

        public float StartNegativeOffsetY { get; }
    }
}

