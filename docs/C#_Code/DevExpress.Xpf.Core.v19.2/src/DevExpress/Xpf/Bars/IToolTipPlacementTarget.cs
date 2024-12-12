namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows;

    public interface IToolTipPlacementTarget
    {
        BarItemLinkControlToolTipHorizontalPlacement HorizontalPlacement { get; }

        BarItemLinkControlToolTipVerticalPlacement VerticalPlacement { get; }

        BarItemLinkControlToolTipPlacementTargetType HorizontalPlacementTargetType { get; }

        BarItemLinkControlToolTipPlacementTargetType VerticalPlacementTargetType { get; }

        double HorizontalOffset { get; }

        double VerticalOffset { get; }

        DependencyObject ExternalPlacementTarget { get; }
    }
}

