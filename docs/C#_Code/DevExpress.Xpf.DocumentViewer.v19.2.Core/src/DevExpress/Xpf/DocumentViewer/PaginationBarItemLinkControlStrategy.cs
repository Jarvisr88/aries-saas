namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows.Input;

    public class PaginationBarItemLinkControlStrategy : BarStaticItemLinkControlStrategy
    {
        public PaginationBarItemLinkControlStrategy(IBarItemLinkControl instance) : base(instance)
        {
        }

        public override bool OnMouseLeftButtonDown(MouseButtonEventArgs args) => 
            false;

        public override bool OnMouseLeftButtonUp(MouseButtonEventArgs args) => 
            false;
    }
}

