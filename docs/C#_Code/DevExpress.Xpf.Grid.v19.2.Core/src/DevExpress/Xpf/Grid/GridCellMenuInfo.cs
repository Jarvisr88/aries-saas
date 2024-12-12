namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class GridCellMenuInfo : GridMenuInfo
    {
        public GridCellMenuInfo(DataControlPopupMenu menu) : base(menu)
        {
        }

        protected override void CreateItems()
        {
        }

        protected override void ExecuteMenuController()
        {
            base.ExecuteMenuController();
            Func<DataViewBase, BarManagerMenuController> getMenuController = <>c.<>9__22_0;
            if (<>c.<>9__22_0 == null)
            {
                Func<DataViewBase, BarManagerMenuController> local1 = <>c.<>9__22_0;
                getMenuController = <>c.<>9__22_0 = view => view.RowCellMenuController;
            }
            base.Menu.ExecuteOriginationViewMenuController(getMenuController);
        }

        public override bool Initialize(IInputElement value)
        {
            this.Row = RowData.FindRowData(value as DependencyObject);
            this.OriginalRow = this.Row.Row;
            CellContentPresenter presenter = value as CellContentPresenter;
            if (presenter != null)
            {
                base.BaseColumn = presenter.Column;
            }
            else
            {
                CellEditor editor = value as CellEditor;
                if (editor != null)
                {
                    base.BaseColumn = editor.Column;
                }
            }
            this.IsCellMenu = base.Column != null;
            return base.Initialize(value);
        }

        public override void Uninitialize()
        {
            this.OriginalRow = null;
            base.Uninitialize();
        }

        public RowData Row { get; private set; }

        public object OriginalRow { get; private set; }

        public bool IsCellMenu { get; private set; }

        public override GridMenuType MenuType =>
            GridMenuType.RowCell;

        internal override bool IsMenuEnabled =>
            base.View.IsRowCellMenuEnabled;

        public override BarManagerMenuController MenuController =>
            base.View.RowCellMenuController;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GridCellMenuInfo.<>c <>9 = new GridCellMenuInfo.<>c();
            public static Func<DataViewBase, BarManagerMenuController> <>9__22_0;

            internal BarManagerMenuController <ExecuteMenuController>b__22_0(DataViewBase view) => 
                view.RowCellMenuController;
        }
    }
}

