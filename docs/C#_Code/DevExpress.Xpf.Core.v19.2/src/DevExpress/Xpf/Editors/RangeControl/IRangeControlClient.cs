namespace DevExpress.Xpf.Editors.RangeControl
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public interface IRangeControlClient
    {
        event EventHandler<LayoutChangedEventArgs> LayoutChanged;

        string FormatText(object value);
        double GetComparableValue(object realValue);
        object GetRealValue(double comparable);
        object GetSnappedValue(object value, bool isLeft);
        RangeControlClientHitTestResult HitTest(Point point);
        void Invalidate(Size viewportSize);
        bool SetRange(object start, object end, Size viewportSize);
        bool SetSelectionRange(object selectionStart, object selectionEnd, Size viewportSize, bool isSnapped = true);
        bool SetVisibleRange(object visibleStart, object visibleEnd, Size viewportSize);

        bool GrayOutNonSelectedRange { get; }

        bool AllowThumbs { get; }

        bool SnapSelectionToGrid { get; }

        bool ConvergeThumbsOnZoomingOut { get; }

        object Start { get; }

        object End { get; }

        object SelectionStart { get; }

        object SelectionEnd { get; }

        object VisibleStart { get; }

        object VisibleEnd { get; }

        Rect ClientBounds { get; }
    }
}

