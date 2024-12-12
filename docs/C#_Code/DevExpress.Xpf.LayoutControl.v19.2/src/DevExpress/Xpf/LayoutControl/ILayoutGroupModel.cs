namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public interface ILayoutGroupModel : ILayoutModelBase
    {
        System.Windows.Controls.Orientation CollapseDirection { get; }

        LayoutGroupCollapseMode CollapseMode { get; }

        bool MeasureUncollapsedChildOnly { get; }

        System.Windows.Controls.Orientation Orientation { get; set; }

        FrameworkElement UncollapsedChild { get; }
    }
}

