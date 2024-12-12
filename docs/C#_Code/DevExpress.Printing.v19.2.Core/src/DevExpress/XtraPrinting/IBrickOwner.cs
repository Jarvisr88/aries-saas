namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrinting.Export.Web;
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraReports.UI;
    using System;
    using System.Drawing;

    public interface IBrickOwner
    {
        void AddToSummaryUpdater(VisualBrick brick, VisualBrick prototypeBrick);
        bool IsSeparableHorz(bool isBrickSeparableHorz);
        bool IsSeparableVert(bool isBrickSeparableVert);
        bool RaiseAfterPrintOnPage(VisualBrick brick, Page page, int pageNumber, int pageCount);
        void RaiseDraw(VisualBrick brick, IGraphics gr, RectangleF bounds);
        void RaiseHtmlItemCreated(VisualBrick brick, IScriptContainer scriptContainer, DXHtmlContainerControl contentCell);
        void RaiseSummaryCalculated(VisualBrick brick, string text, string format, object value);
        void UpdateBrickBounds(VisualBrick brick);

        string Text { get; }

        bool IsNavigationLink { get; }

        bool IsNavigationTarget { get; }

        bool NeedCalcContainerHeight { get; }

        bool HasPageSummary { get; }

        bool HasRunningSummary { get; }

        DevExpress.XtraPrinting.Native.ConvertHelper ConvertHelper { get; }

        BookmarkInfo EmptyBookmarkInfo { get; }

        bool CanCacheImages { get; }

        object RealControl { get; }

        VerticalAnchorStyles AnchorVertical { get; }

        HorizontalAnchorStyles AnchorHorizontal { get; }
    }
}

