namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;

    public class GridMenuEventArgs : RoutedEventArgs
    {
        private DataControlPopupMenu menu;
        private ReadOnlyCollection<BarItem> items;

        public GridMenuEventArgs(DataControlPopupMenu menu)
        {
            this.menu = menu;
            this.items = menu.GetItems(null);
        }

        protected DataControlPopupMenu GridMenu =>
            this.menu;

        public GridMenuType? MenuType =>
            this.GridMenu.MenuType;

        public ReadOnlyCollection<BarItem> Items =>
            this.items;

        public BarManagerActionCollection Customizations =>
            this.GridMenu.Customizations;

        public IInputElement TargetElement =>
            this.GridMenu.PlacementTarget;

        public GridMenuInfo MenuInfo =>
            this.GridMenu.MenuInfo;

        public DataViewBase Source =>
            this.GridMenu.View;
    }
}

