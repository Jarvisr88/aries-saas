namespace DevExpress.Data
{
    using System;

    public class CustomSummaryEventArgs : EventArgs
    {
        private CustomSummaryProcess summaryProcess;
        private object totalValue;
        private object fieldValue;
        private int controllerRow;
        private int groupRowHandle;
        private int groupLevel;
        private object item;
        internal DataController controller;
        private bool totalValueReady;

        public CustomSummaryEventArgs();
        public CustomSummaryEventArgs(int controllerRow, object totalValue, object fieldValue, int groupRowHandle, CustomSummaryProcess summaryProcess, object item, int groupLevel);
        public object GetGroupSummary(int groupRowHandle, object summaryItem);
        public object GetValue(string fieldName);
        protected internal void Setup(int controllerRow, object totalValue, object fieldValue, GroupRowInfo groupRow, CustomSummaryProcess summaryProcess, object item);
        protected internal void SetupCell(int controllerRow, object fieldValue);
        protected internal void SetupSummaryProcess(CustomSummaryProcess summaryProcess);

        public object TotalValue { get; set; }

        public bool TotalValueReady { get; set; }

        public int GroupLevel { get; }

        public object Item { get; }

        public CustomSummaryProcess SummaryProcess { get; }

        public int GroupRowHandle { get; }

        public object FieldValue { get; }

        public int RowHandle { get; }

        public virtual bool IsGroupSummary { get; }

        public virtual bool IsTotalSummary { get; }

        public object Row { get; }
    }
}

