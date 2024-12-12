namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export.Web;
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using DevExpress.XtraReports.UI;
    using System;
    using System.Drawing;

    public class NullBrickOwner : IBrickOwner
    {
        public static readonly NullBrickOwner Instance;

        static NullBrickOwner();
        internal NullBrickOwner();
        void IBrickOwner.AddToSummaryUpdater(VisualBrick brick, VisualBrick prototypeBrick);
        bool IBrickOwner.IsSeparableHorz(bool isBrickSeparableHorz);
        bool IBrickOwner.IsSeparableVert(bool isBrickSeparableVert);
        bool IBrickOwner.RaiseAfterPrintOnPage(VisualBrick brick, Page page, int pageNumber, int pageCount);
        void IBrickOwner.RaiseDraw(VisualBrick brick, IGraphics gr, RectangleF bounds);
        void IBrickOwner.RaiseHtmlItemCreated(VisualBrick brick, IScriptContainer scriptContainer, DXHtmlContainerControl contentCell);
        void IBrickOwner.RaiseSummaryCalculated(VisualBrick brick, string text, string format, object value);
        void IBrickOwner.UpdateBrickBounds(VisualBrick brick);

        string IBrickOwner.Text { get; }

        bool IBrickOwner.IsNavigationLink { get; }

        bool IBrickOwner.IsNavigationTarget { get; }

        bool IBrickOwner.NeedCalcContainerHeight { get; }

        bool IBrickOwner.HasPageSummary { get; }

        ConvertHelper IBrickOwner.ConvertHelper { get; }

        bool IBrickOwner.HasRunningSummary { get; }

        BookmarkInfo IBrickOwner.EmptyBookmarkInfo { get; }

        bool IBrickOwner.CanCacheImages { get; }

        object IBrickOwner.RealControl { get; }

        VerticalAnchorStyles IBrickOwner.AnchorVertical { get; }

        HorizontalAnchorStyles IBrickOwner.AnchorHorizontal { get; }
    }
}

