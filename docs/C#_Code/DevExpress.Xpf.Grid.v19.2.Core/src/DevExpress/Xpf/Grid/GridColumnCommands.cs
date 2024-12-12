namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class GridColumnCommands : INotifyPropertyChanged
    {
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        public GridColumnCommands(ColumnBase column)
        {
            this.ChangeColumnSortOrder = DelegateCommandFactory.Create<object>(o => column.Owner.ChangeColumnSortOrder(column), false);
            this.ClearColumnFilter = DelegateCommandFactory.Create<object>(o => column.Owner.ClearColumnFilter(column), o => column.Owner.CanClearColumnFilter(column), false);
        }

        [Description("Toggles a column's sort order.")]
        public ICommand ChangeColumnSortOrder { get; private set; }

        [Description("Removes the filter condition applied to a column.")]
        public ICommand ClearColumnFilter { get; private set; }

        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Instead use the ClearColumnFilter property.")]
        public ICommand ClearFilterColumn =>
            this.ClearColumnFilter;
    }
}

