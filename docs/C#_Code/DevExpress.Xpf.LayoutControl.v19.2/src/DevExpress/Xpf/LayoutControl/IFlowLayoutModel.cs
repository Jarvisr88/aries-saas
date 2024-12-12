namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public interface IFlowLayoutModel : ILayoutModelBase
    {
        FrameworkElement MaximizedElement { get; }

        DevExpress.Xpf.LayoutControl.MaximizedElementPosition MaximizedElementPosition { get; set; }

        System.Windows.Controls.Orientation Orientation { get; }
    }
}

