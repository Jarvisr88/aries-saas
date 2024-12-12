namespace DevExpress.Data.Details
{
    using DevExpress.Data;
    using System;
    using System.Collections;
    using System.Reflection;

    public class MasterRowInfo : CollectionBase
    {
        private int parentListSourceRow;
        private DataController parentController;
        private object parentRowKey;

        public MasterRowInfo(DataController parentController, int parentListSourceRow, object parentRowKey);
        public DetailInfo CreateDetail(IList list, int relationIndex);
        public DetailInfo FindDetail(int relationIndex);
        public DetailInfo FindDetail(object detailOwner);
        public int IndexOf(DetailInfo info);
        public virtual void MakeDetailVisible(DetailInfo info);
        public void MakeDetailVisible(int relationIndex);
        private void MoveToUp(DetailInfo info);
        protected override void OnClear();
        protected override void OnRemoveComplete(int index, object value);
        public void Remove(DetailInfo info);
        protected internal void SetParentListSourceRow(int value);

        public object ParentRowKey { get; }

        public DataController ParentController { get; }

        public int ParentControllerRow { get; }

        public object ParentRow { get; }

        public int ParentListSourceRow { get; }

        public DetailInfo this[int index] { get; }
    }
}

