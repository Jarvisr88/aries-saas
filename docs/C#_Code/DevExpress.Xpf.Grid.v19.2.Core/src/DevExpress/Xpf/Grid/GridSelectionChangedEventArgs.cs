namespace DevExpress.Xpf.Grid
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    public class GridSelectionChangedEventArgs : RoutedEventArgs
    {
        private readonly CollectionChangeAction action;
        private readonly int controllerRow;
        private readonly DataViewBase view;

        public GridSelectionChangedEventArgs(DataViewBase view, CollectionChangeAction action, int controllerRow)
        {
            this.action = action;
            this.controllerRow = controllerRow;
            this.view = view;
        }

        public CollectionChangeAction Action =>
            this.action;

        public int ControllerRow =>
            this.controllerRow;

        public DataViewBase Source =>
            this.view;
    }
}

