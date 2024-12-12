namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class YPageContentEngine
    {
        private PSPage psPage;
        protected IPrintingSystemContext ps;
        private List<BandBricksPair> addedDocBands;
        private Dictionary<MultiKey, float> bandHeights;

        public YPageContentEngine(PSPage psPage, IPrintingSystemContext ps);
        public virtual void CorrectPrintAtBottomBricks(List<BandBricksPair> docBands, float pageBottom, bool ignoreBottomSpan);
        protected virtual IPageContentAlgorithm CreateAlgorithm(DocumentBand docBand, PointF offset, bool forceSplit);
        public List<BandBricksPair> GetAddedBands(int markedBandID);
        public float GetBandHeight(MultiKey key, Func<float> callback);
        private IPageContentAlgorithm GetPageContentAlgorithm(DocumentBand docBand, Func<IPageContentAlgorithm> callback);
        public virtual void OnBuildDocumentBand(DocumentBand docBand);
        public void ResetObservableItems();
        public void SetBandHeight(MultiKey key, float height);
        public bool TryGetBandHeight(MultiKey key, out float height);
        public virtual PageUpdateData UpdateContent(DocumentBand docBand, PointD offset, RectangleF bounds, bool forceSplit);

        public PrintingSystemBase PrintingSystem { get; }

        public Observable<bool> Stopped { get; private set; }

        public Observable<bool> BuildInfoIncreased { get; private set; }

        public virtual int PageBricksCount { get; }

        public PSPage Page { get; }

        public int SectionID { get; set; }

        public List<BandBricksPair> AddedBands { get; }

        public DocumentBand LastAddedBand { get; }

        public bool WasAddedActualBand { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly YPageContentEngine.<>c <>9;
            public static Func<BandBricksPair, bool> <>9__32_0;

            static <>c();
            internal bool <get_WasAddedActualBand>b__32_0(BandBricksPair item);
        }

        protected class ContentAlgorithmByY : ContentAlgorithmBase
        {
            protected PointF offset;
            protected bool forceSplitY;
            protected IPrintingSystemContext context;
            protected YPageContentEngine pageContentEngine;
            private DocumentBand documentBand;

            public ContentAlgorithmByY(YPageContentEngine pageContentEngine, DocumentBand documentBand, PointF offset, bool forceSplit, IPrintingSystemContext ps);
            private static bool AreRepeatableHeaders(IEnumerable<DocumentBand> bands);
            protected static bool BetweenTopAndBottom(float value, RectangleF rect);
            protected override bool ContainsFunction(RectangleF rect1, RectangleF rect2);
            protected override void FillPage(out float maxBrickBound);
            protected override float GetBrickBound(Brick brick, bool forceSplit, float maxBrickBound);
            protected override ContentAlgorithmBase.BrickItem GetIntersectItem();
            protected override float GetMaxBound(RectangleF rect);
            private static DocumentBand GetTopLevelBand(DocumentBand band);
            protected override bool IntersectFunction(RectangleF rect1, RectangleF rect2);
            protected override void OnIntersectedBrickAdded(Brick brick, float brickBound);
            protected static bool RectContains(RectangleF baseRect, RectangleF rect);
            protected override bool ShouldFillNextPage();
            private static float ValidateBottom(float bottom, RectangleF bounds, float maxBrickBound);

            protected override float DefaultMaxBound { get; }

            protected override float MinBound { get; }

            protected override float MaxBound { get; set; }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly YPageContentEngine.ContentAlgorithmByY.<>c <>9;
                public static Func<ContentAlgorithmBase.BrickItem, Brick> <>9__21_0;
                public static Func<BandBricksPair, DocumentBand> <>9__22_0;
                public static Func<DocumentBand, bool> <>9__23_0;
                public static Func<DocumentBand, bool> <>9__24_0;

                static <>c();
                internal bool <AreRepeatableHeaders>b__24_0(DocumentBand x);
                internal DocumentBand <GetBrickBound>b__22_0(BandBricksPair x);
                internal bool <GetTopLevelBand>b__23_0(DocumentBand x);
                internal Brick <ShouldFillNextPage>b__21_0(ContentAlgorithmBase.BrickItem x);
            }
        }
    }
}

