namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using DevExpress.Xpf.Bars;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Input;
    using System.Windows.Media;

    public abstract class GridMenuInfo : GridMenuInfoBase
    {
        protected GridMenuInfo(DataControlPopupMenu menu) : base(menu)
        {
        }

        protected virtual BarButtonItem CreateBarButtonItem(string name, GridControlStringId id, bool beginGroup, ImageSource image, ICommand command, object commandParameter = null) => 
            this.CreateBarButtonItem(name, this.View.GetLocalizedString(id), beginGroup, image, command, commandParameter);

        protected virtual BarButtonItem CreateBarButtonItem(BarItemLinkCollection links, string name, GridControlStringId id, bool beginGroup, ImageSource image, ICommand command, object commandParameter = null) => 
            this.CreateBarButtonItem(links, name, this.View.GetLocalizedString(id), beginGroup, image, command, commandParameter);

        protected virtual BarCheckItem CreateBarCheckItem(string name, GridControlStringId id, bool? isChecked, bool beginGroup, ImageSource image, ICommand command) => 
            this.CreateBarCheckItem(name, this.View.GetLocalizedString(id), isChecked, beginGroup, image, command);

        protected virtual BarSplitButtonItem CreateBarSplitButtonItem(BarItemLinkCollection links, string name, GridControlStringId id, bool beginGroup, ImageSource image) => 
            this.CreateBarSplitButtonItem(links, name, this.View.GetLocalizedString(id), beginGroup, image);

        protected virtual BarSubItem CreateBarSubItem(string name, GridControlStringId id, bool beginGroup, ImageSource image, ICommand command) => 
            this.CreateBarSubItem(name, this.View.GetLocalizedString(id), beginGroup, image, command);

        protected virtual BarSubItem CreateBarSubItem(BarItemLinkCollection links, string name, GridControlStringId id, bool beginGroup, ImageSource image, ICommand command) => 
            this.CreateBarSubItem(links, name, this.View.GetLocalizedString(id), beginGroup, image, command);

        protected ColumnSortOrder GetColumnSortOrder(AllowedSortOrders allowedSortOrders, ListSortDirection sortDirection) => 
            GridSortInfo.GetColumnSortOrder(GridSortInfo.GetActualDirection(allowedSortOrders, sortDirection));

        public DataControlPopupMenu Menu =>
            (DataControlPopupMenu) base.Menu;

        public DataViewBase View =>
            this.Menu.View;

        public DataControlBase DataControl =>
            this.View.DataControl;

        protected internal DevExpress.Xpf.Grid.BaseColumn BaseColumn { get; set; }

        public ColumnBase Column =>
            this.BaseColumn as ColumnBase;

        public abstract GridMenuType MenuType { get; }

        public override bool CanCreateItems =>
            this.IsMenuEnabled && !this.View.IsEditFormVisible;

        internal virtual bool IsMenuEnabled =>
            true;
    }
}

