namespace DevExpress.Xpf.Editors.DateNavigator
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class DateNavigatorRequestCellStateEventArgs : RoutedEventArgs
    {
        internal DateNavigatorRequestCellStateEventArgs(System.DateTime dateTime) : base(DevExpress.Xpf.Editors.DateNavigator.DateNavigator.RequestCellStateEvent)
        {
            this.DateTime = dateTime;
        }

        public System.DateTime DateTime { get; private set; }

        public DateNavigatorCellState CellState { get; set; }
    }
}

