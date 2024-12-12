namespace DevExpress.Data.Helpers
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SelectedRowInfo
    {
        public int ListSourceRow;
        public object ListSourceRowKey;
        public object SelectionObject;
        public bool IsRestored;
    }
}

