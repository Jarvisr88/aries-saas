namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public interface IFlowLayoutControl : ILayoutControlBase, IScrollControl, IPanel, IControl, ILayoutModelBase, IFlowLayoutModel
    {
        void BringSeparatorsToFront();
        bool IsLayerSeparator(UIElement element);
        void OnAllowLayerSizingChanged();
        void OnItemPositionChanged(int oldPosition, int newPosition);
        void SendSeparatorsToBack();

        bool AllowAddFlowBreaksDuringItemMoving { get; }

        bool AnimateItemMoving { get; }

        TimeSpan ItemMovingAnimationDuration { get; }

        double LayerMinWidth { get; }

        double LayerMaxWidth { get; }

        double LayerWidth { get; set; }

        Brush LayerSizingCoverBrush { get; }

        Style MaximizedElementPositionIndicatorStyle { get; }
    }
}

