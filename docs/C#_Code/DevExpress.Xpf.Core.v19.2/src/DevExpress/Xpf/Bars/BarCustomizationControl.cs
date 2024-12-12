namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows;

    public class BarCustomizationControl : FrameworkElement
    {
        private ItemCollection<BarItem> barItems;
        private ItemCollection<BarItemLinkBase> barItemLinks;

        public BarCustomizationControl();

        public ItemCollection<BarItem> Items { get; }

        public ItemCollection<BarItemLinkBase> ItemLinks { get; }
    }
}

