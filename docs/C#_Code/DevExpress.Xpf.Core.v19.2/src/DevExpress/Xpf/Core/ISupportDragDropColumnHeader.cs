namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public interface ISupportDragDropColumnHeader : ISupportDragDrop
    {
        void UpdateLocation(IndependentMouseEventArgs e);

        FrameworkElement RelativeDragElement { get; }

        FrameworkElement TopVisual { get; }

        bool SkipHitTestVisibleChecking { get; }
    }
}

