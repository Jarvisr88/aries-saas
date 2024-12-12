namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    internal class PrintAtBottomHelper
    {
        private PrintingSystemBase ps;

        public PrintAtBottomHelper(PrintingSystemBase ps);
        private void AddEmptySpaceBricks(PSPage psPage, DocumentBand docBand, float vertOffset);
        private static int CompareDocumentBands(BandBricksPair keyValuePair1, BandBricksPair keyValuePair2);
        private static int CompareDocumentBands2(Pair<BandBricksPair, float> keyValuePair1, Pair<BandBricksPair, float> keyValuePair2);
        private static bool ContainsClippingBrickContainers(IList<Brick> list);
        public static void CorrectPrintAtBottomBricks(List<BandBricksPair> docBands, float pageBottom, PSPage psPage, bool ignoreBottomSpan);
        public void FillEmptySpace(PSPage psPage, IList<DocumentBand> addedBands, float emptySpace, float vertOffset);
        public void FireFillPage(List<BandBricksPair> docBands, float pageBottom, PSPage psPage, bool fillEmptySpace);
        private static float GetBandBottom(BandBricksPair item);
        private static PointF GetBandLocation(BandBricksPair item);
        private static List<BandBricksPair> GetBandsToMove(List<BandBricksPair> printAtBottomBands, List<BandBricksPair> detailBands);
        private static float GetBandTop(BandBricksPair item);
        private static List<BandBricksPair> GetBottomBands(IList<BandBricksPair> docBands);
        private static float GetBottomSpanRecursive(DocumentBand docBand, float bottomSpan);
        private static float GetBricksBottom(IList<Brick> bricks);
        private static List<BandBricksPair> GetDetailBands(List<BandBricksPair> docBands);
        private static List<BandBricksPair> GetPrintAtBottomBands(List<BandBricksPair> docBands);
        private static bool IsPrintAtBottomBand(DocumentBand docBand);
        private static void MoveBandsToBottom(List<BandBricksPair> bandsToMove, float emptySpace);
        private static bool ShouldMoveBottom(BandBricksPair bandForMove, List<BandBricksPair> detailBands);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PrintAtBottomHelper.<>c <>9;
            public static Func<BandBricksPair, bool> <>9__3_0;
            public static Func<BandBricksPair, DocumentBand> <>9__3_1;
            public static Func<BandBricksPair, DocumentBand> <>9__3_2;

            static <>c();
            internal bool <FireFillPage>b__3_0(BandBricksPair x);
            internal DocumentBand <FireFillPage>b__3_1(BandBricksPair x);
            internal DocumentBand <FireFillPage>b__3_2(BandBricksPair x);
        }
    }
}

