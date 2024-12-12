namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ShowMenuEventArgs<T> : RoutedEventArgs where T: BaseLayoutElementMenu
    {
        public ShowMenuEventArgs(T menu)
        {
            this.Show = true;
            this.Menu = menu;
            this.Items = menu.GetItems();
        }

        private IInputElement GetTargetElement()
        {
            UIElement placementTarget = this.Menu.PlacementTarget;
            return ((placementTarget == null) ? null : (DockLayoutManager.GetLayoutItem(placementTarget) ?? placementTarget));
        }

        public T Menu { get; private set; }

        public bool Show { get; set; }

        public ReadOnlyCollection<BarItem> Items { get; private set; }

        public BarManagerActionCollection ActionList =>
            this.Menu.Customizations;

        public IInputElement TargetElement =>
            this.GetTargetElement();
    }
}

