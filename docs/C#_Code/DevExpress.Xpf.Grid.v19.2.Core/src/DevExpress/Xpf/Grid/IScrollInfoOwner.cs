namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public interface IScrollInfoOwner
    {
        void InvalidateHorizontalScrolling();
        void InvalidateMeasure();
        bool OnBeforeChangeItemScrollOffset();
        bool OnBeforeChangePixelScrollOffset();
        void OnDefineScrollInfoChanged();
        void OnSecondaryScrollInfoChanged();
        double ScrollInsideActiveEditorIfNeeded(Visual visual, Rect rectangle);

        int Offset { get; }

        int ScrollStep { get; }

        int ItemCount { get; }

        int ItemsOnPage { get; }

        DataControlScrollMode VerticalScrollMode { get; }

        DataControlScrollMode HorizontalScrollMode { get; }

        bool IsDeferredScrolling { get; }

        double WheelScrollLines { get; }

        bool IsHorizontalScrollBarVisible { get; set; }

        bool IsTouchScrollBarsMode { get; set; }

        FrameworkElement ScrollContentPresenter { get; }
    }
}

