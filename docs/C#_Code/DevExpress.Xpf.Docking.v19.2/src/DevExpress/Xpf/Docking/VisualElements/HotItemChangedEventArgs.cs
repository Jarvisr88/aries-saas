namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class HotItemChangedEventArgs : RoutedEventArgs
    {
        public HotItemChangedEventArgs(BaseLayoutItem hot, BaseLayoutItem prevHot)
        {
            this.Hot = hot;
            this.PrevHot = prevHot;
        }

        public BaseLayoutItem Hot { get; private set; }

        public BaseLayoutItem PrevHot { get; private set; }
    }
}

