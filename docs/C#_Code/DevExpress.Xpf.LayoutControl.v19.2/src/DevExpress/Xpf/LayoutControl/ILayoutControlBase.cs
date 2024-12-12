namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public interface ILayoutControlBase : IScrollControl, IPanel, IControl, ILayoutModelBase
    {
        FrameworkElement GetMoveableItem(Point p);

        bool AllowItemMoving { get; }

        LayoutProviderBase LayoutProvider { get; }

        Brush MovingItemPlaceHolderBrush { get; }
    }
}

