namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;

    public class ShowingMenuEventArgs : ShowMenuEventArgs<BaseLayoutElementMenu>
    {
        public ShowingMenuEventArgs(BaseLayoutElementMenu menu) : base(menu)
        {
            base.RoutedEvent = DockLayoutManager.ShowingMenuEvent;
        }
    }
}

