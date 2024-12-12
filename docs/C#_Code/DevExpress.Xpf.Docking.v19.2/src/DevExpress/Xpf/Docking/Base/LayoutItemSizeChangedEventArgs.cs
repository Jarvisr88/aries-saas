namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class LayoutItemSizeChangedEventArgs : ItemEventArgs
    {
        public LayoutItemSizeChangedEventArgs(BaseLayoutItem item, bool isWidth, GridLength value, GridLength prevValue) : base(item)
        {
            base.RoutedEvent = DockLayoutManager.LayoutItemSizeChangedEvent;
            this.Value = value;
            this.PrevValue = prevValue;
            this.WidthChanged = isWidth;
            this.HeightChanged = !isWidth;
        }

        public GridLength Value { get; private set; }

        public GridLength PrevValue { get; private set; }

        public bool HeightChanged { get; private set; }

        public bool WidthChanged { get; private set; }
    }
}

