namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class ExportPageBuildEngine : ContinuousPageBuildEngine
    {
        protected ExportPageBuildEngine.ExportContentEngine contentEngine;

        public ExportPageBuildEngine(PrintingDocument document);
        protected override bool AddPage(HashSet<long> pages, PSPage psPage, YPageContentEngine pageContentEngine);
        protected override YPageContentEngine CreateContentEngine(PSPage psPage, YPageContentEngine previous);
        public virtual ContinuousExportInfo CreateContinuousExportInfo();
        protected internal virtual Pair<Dictionary<Brick, RectangleDF>, double> Execute();
        protected internal Pair<Dictionary<Brick, RectangleDF>, double> ExecuteInfo(string infoString);
        protected internal Pair<Dictionary<Brick, RectangleF>, double> ExecuteMargin(DocumentBandKind marginKind, BrickModifier modifier);
        protected void FilterBrickList<T>(Dictionary<Brick, T> brickList);
        protected internal void JoinBricks(Dictionary<Brick, RectangleDF> destBricks, IEnumerable<KeyValuePair<Brick, RectangleDF>> bricks, double dy);

        [Serializable, CompilerGenerated]
        private sealed class <>c__9<T>
        {
            public static readonly ExportPageBuildEngine.<>c__9<T> <>9;
            public static Func<KeyValuePair<Brick, T>, bool> <>9__9_0;

            static <>c__9();
            internal bool <FilterBrickList>b__9_0(KeyValuePair<Brick, T> x);
        }

        protected class ContinuousSimplePageRowBuilder : SimplePageRowBuilder
        {
            public ContinuousSimplePageRowBuilder(PrintingSystemBase ps);
            protected override PageUpdateData UpdatePageContent(DocumentBand docBand, RectangleF bounds);
        }

        protected class ExportContentEngine : ContinuousPageBuildEngine.ContinuousContentEngine
        {
            private bool wasMultiColumn;
            private int zOrderColumnCount;
            private float zOrderContentHeight;
            private float intermediateContentHeight;
            private int multiColumnRootID;

            public ExportContentEngine(PSPage psPage, PrintingSystemBase ps);
            private PageBreakData CreateBreakData(float value, CustomPageData pageData);
            private float GetContentHeight(DocumentBand docBand, bool rootIdChanged);
            private void ProcessNOrderMultiColumn(DocumentBand docBand, bool rootIdChanged);
            protected void UpdateAdditionalInfo(DocumentBand docBand);
            public override PageUpdateData UpdateContent(DocumentBand docBand, PointD offset, RectangleF bounds, bool forceSplit);
            private void UpdateContentHeight(DocumentBand docBand, bool rootIdChanged);

            public List<PageBreakData> PageBreaks { get; private set; }

            public List<DevExpress.XtraPrinting.Native.MultiColumnInfo> MultiColumnInfo { get; private set; }

            public Dictionary<Brick, RectangleDF> BrickList { get; private set; }

            public override int PageBricksCount { get; }
        }
    }
}

