namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public class SyncRowInfo
    {
        public SyncRowInfo(object rowKey, object row)
        {
            this.RowKey = rowKey;
            this.Row = row;
        }

        public object RowKey { get; private set; }

        public object Row { get; private set; }
    }
}

