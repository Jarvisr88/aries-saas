namespace DevExpress.Data
{
    using System;

    public class CustomSummaryExistEventArgs : EventArgs
    {
        private bool exists;
        private int groupRowHandle;
        private int groupLevel;
        private object item;

        protected internal CustomSummaryExistEventArgs(GroupRowInfo groupRow, object item);
        public CustomSummaryExistEventArgs(int groupRowHandle, int groupLevel, object item);

        public object Item { get; }

        public virtual bool Exists { get; set; }

        public int GroupLevel { get; }

        public virtual int GroupRowHandle { get; }

        public virtual bool IsGroupSummary { get; }

        public virtual bool IsTotalSummary { get; }
    }
}

