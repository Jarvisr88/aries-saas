namespace DevExpress.XtraExport
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ExportCacheItem
    {
        public IExportInternalProvider InternalCache;
        public object Data;
        public ExportCacheDataType DataType;
        public int StyleIndex;
        public bool IsUnion;
        public bool IsHidden;
        public int UnionWidth;
        public int UnionHeight;
    }
}

