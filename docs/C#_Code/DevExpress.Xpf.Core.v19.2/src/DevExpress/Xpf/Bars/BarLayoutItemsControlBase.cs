namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class BarLayoutItemsControlBase : BarItemsControl
    {
        public static readonly DependencyProperty ContainerKeyProperty;
        public static readonly DependencyProperty OrientationProperty;

        static BarLayoutItemsControlBase();
        protected BarLayoutItemsControlBase();

        public System.Windows.Controls.Orientation Orientation { get; set; }

        public object ContainerKey { get; set; }
    }
}

