namespace DevExpress.Xpf.Grid.Printing
{
    using DevExpress.Xpf.Data;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class ItemsGenerationStrategyBase
    {
        private DataViewBase view;
        private bool autoExpandAllGroups;

        public ItemsGenerationStrategyBase(DataViewBase view)
        {
            this.view = view;
        }

        public virtual void Clear()
        {
        }

        public void ClearAll()
        {
            this.ClearAllCore();
            if (this.RequireFullExpand)
            {
                this.DataProvider.AutoExpandAllGroups = this.autoExpandAllGroups;
            }
        }

        protected virtual void ClearAllCore()
        {
        }

        public void GenerateAll()
        {
            this.autoExpandAllGroups = this.DataProvider.AutoExpandAllGroups;
            if (this.RequireFullExpand)
            {
                this.DataProvider.AutoExpandAllGroups = false;
            }
            this.GenerateAllCore();
        }

        protected virtual void GenerateAllCore()
        {
        }

        public abstract object GetCellValue(RowData rowData, string fieldName);
        public abstract string GetFixedTotalSummaryLeftText();
        public abstract string GetFixedTotalSummaryRightText();
        public abstract object GetRowValue(RowData rowData);
        public abstract string GetTotalSummaryText(ColumnBase column);
        public void PrepareDataControllerAndPerformPrintingAction(Action printingAction)
        {
            try
            {
                this.GenerateAll();
                printingAction();
            }
            finally
            {
                this.ClearAll();
            }
        }

        protected DataViewBase View =>
            this.view;

        protected DataProviderBase DataProvider =>
            this.view.DataProviderBase;

        internal bool StoreOriginalRowHandles { get; set; }

        public abstract bool RequireFullExpand { get; }
    }
}

