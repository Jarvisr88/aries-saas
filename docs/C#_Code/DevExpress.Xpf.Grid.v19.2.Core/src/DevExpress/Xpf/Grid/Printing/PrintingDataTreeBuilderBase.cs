namespace DevExpress.Xpf.Grid.Printing
{
    using DevExpress.Xpf.Grid;
    using DevExpress.Xpf.Grid.Native;
    using DevExpress.XtraPrinting.DataNodes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class PrintingDataTreeBuilderBase : DataTreeBuilder
    {
        protected static ColumnBase[] EmptyColumns = new ColumnBase[0];
        private IList<ColumnBase> visibleColumns;

        public PrintingDataTreeBuilderBase(DataViewBase view, double totalHeaderWidth) : base(view, null, null)
        {
            this.PrintRowTemplate = view.GetPrintRowTemplate();
            this.PrintHeaderTemplate = view.PrintHeaderTemplate;
            this.TotalHeaderWidth = totalHeaderWidth;
            this.ReusingRowData = this.CreateReusingRowData();
            this.PrintFooterTemplate = view.PrintFooterTemplate;
            this.PrintFixedFooterTemplate = view.PrintFixedFooterTemplate;
        }

        protected IList<T> CopyList<T>(IList<T> source)
        {
            List<T> list = new List<T>(source.Count);
            foreach (T local in source)
            {
                list.Add(local);
            }
            return list;
        }

        public abstract IDataNode CreateDetailPrintingNode(NodeContainer container, RowNode rowNode, IDataNode parentNode, int index);
        public abstract IDataNode CreateGroupPrintingNode(NodeContainer container, RowNode groupNode, IDataNode parentNode, int index, Size pageSize);
        protected virtual RowData CreateReusingRowData() => 
            new RowData(this, false, false, true);

        public abstract void GenerateAllItems();
        protected virtual double GetBorderThickness() => 
            1.0;

        internal override IList<ColumnBase> GetFixedLeftColumns() => 
            EmptyColumns;

        internal override IList<ColumnBase> GetFixedNoneColumns() => 
            this.VisibleColumns;

        internal override IList<ColumnBase> GetFixedRightColumns() => 
            EmptyColumns;

        protected virtual IList<ColumnBase> GetPrintableColumns() => 
            base.View.PrintableColumns.ToList<ColumnBase>();

        internal override IList<ColumnBase> GetVisibleColumns() => 
            this.VisibleColumns;

        internal virtual void OnRootNodeDispose()
        {
        }

        public DataTemplate PrintRowTemplate { get; private set; }

        public DataTemplate PrintHeaderTemplate { get; private set; }

        public RowData ReusingRowData { get; private set; }

        public double TotalHeaderWidth { get; private set; }

        public override bool SupportsHorizontalVirtualization =>
            false;

        public DataTemplate PrintFooterTemplate { get; private set; }

        public DataTemplate PrintFixedFooterTemplate { get; private set; }

        protected IList<ColumnBase> VisibleColumns
        {
            get
            {
                this.visibleColumns ??= this.GetPrintableColumns();
                return this.visibleColumns;
            }
        }

        protected internal override bool GenerateBottomFixedRowInEnd =>
            true;

        protected internal override bool IsPrinting =>
            true;

        protected internal override int PageOffset =>
            0;

        protected internal override bool IsPagingMode =>
            false;
    }
}

