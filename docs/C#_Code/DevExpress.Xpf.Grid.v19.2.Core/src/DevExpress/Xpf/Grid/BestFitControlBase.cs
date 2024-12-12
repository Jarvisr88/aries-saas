namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class BestFitControlBase : ContentControl
    {
        private static readonly DependencyPropertyKey ColumnPropertyKey;
        public static readonly DependencyProperty ColumnProperty;

        static BestFitControlBase()
        {
            Type ownerType = typeof(BestFitControlBase);
            ColumnPropertyKey = DependencyPropertyManager.RegisterReadOnly("Column", typeof(ColumnBase), ownerType, new PropertyMetadata(null));
            ColumnProperty = ColumnPropertyKey.DependencyProperty;
        }

        protected BestFitControlBase(DataViewBase view, ColumnBase column)
        {
            this.Column = column;
            this.SetRowData(view);
        }

        protected override Size ArrangeOverride(Size arrangeBounds) => 
            new Size(0.0, 0.0);

        internal virtual DevExpress.Xpf.Grid.RowData CreateRowData(DataViewBase view) => 
            new StandaloneRowData(view.VisualDataTreeBuilder, false, true);

        protected virtual void SetRowData(DataViewBase view)
        {
            this.RowData = this.CreateRowData(view);
            DevExpress.Xpf.Grid.RowData.SetRowDataInternal(this, this.RowData);
            base.DataContext = this.RowData;
            this.CellData = this.RowData.GetCellDataByColumn(this.Column, false, true);
        }

        public void Update(int rowHandle)
        {
            this.RowData.AssignFrom(rowHandle);
        }

        public virtual void UpdateIsFocusedCell(bool isFocusedCell)
        {
        }

        public void UpdateValue(object value)
        {
            this.CellData.Value = value;
        }

        public DevExpress.Xpf.Grid.RowData RowData { get; protected set; }

        protected GridColumnData CellData { get; private set; }

        public ColumnBase Column
        {
            get => 
                (ColumnBase) base.GetValue(ColumnProperty);
            private set => 
                base.SetValue(ColumnPropertyKey, value);
        }
    }
}

