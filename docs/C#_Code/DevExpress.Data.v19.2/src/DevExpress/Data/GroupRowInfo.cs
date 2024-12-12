namespace DevExpress.Data
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class GroupRowInfo
    {
        public byte Level;
        public bool Expanded;
        public GroupRowInfo ParentGroup;
        public int ChildControllerRow;
        public int ChildControllerRowCount;
        public int Index;
        private Hashtable summary;

        public GroupRowInfo();
        public GroupRowInfo(byte level, int childControllerRow, GroupRowInfo parentGroup);
        public virtual void ClearSummary();
        public void ClearSummaryItem(SummaryItemBase item);
        public bool ContainsControllerRow(int controllerRow);
        internal GroupRowInfo GetParentGroupAtLevel(int level);
        public object GetSummaryValue(SummaryItemBase item);
        internal object GetSummaryValue(SummaryItemBase item, out bool isValid);
        public int GetVisibleIndexOfControllerRow(int controllerRow);
        public static int GroupIndexToHandle(int groupIndex);
        public static int HandleToGroupIndex(int handle);
        public static bool IsGroupRowHandle(int controllerRowHandle);
        public void SetSummaryValue(SummaryItemBase item, object value);
        protected void SetSummaryValueCore(object key, object value);
        public override string ToString();

        public GroupRowInfo RootGroup { get; }

        public int Handle { get; }

        public bool IsVisible { get; }

        protected internal Hashtable Summary { get; }

        public virtual object GroupValue { get; set; }
    }
}

