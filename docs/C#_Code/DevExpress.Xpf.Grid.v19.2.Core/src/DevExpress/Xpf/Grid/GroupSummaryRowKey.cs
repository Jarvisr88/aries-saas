namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Data;
    using System;
    using System.Runtime.CompilerServices;

    public class GroupSummaryRowKey
    {
        public GroupSummaryRowKey(DevExpress.Xpf.Data.RowHandle rowHandle, int level)
        {
            this.RowHandle = rowHandle;
            this.Level = level;
        }

        public override bool Equals(object obj)
        {
            GroupSummaryRowKey key = obj as GroupSummaryRowKey;
            return ((key != null) ? (this.RowHandle.Equals(key.RowHandle) && (this.Level == key.Level)) : false);
        }

        public override int GetHashCode() => 
            this.RowHandle.Value;

        public DevExpress.Xpf.Data.RowHandle RowHandle { get; private set; }

        public int Level { get; private set; }
    }
}

