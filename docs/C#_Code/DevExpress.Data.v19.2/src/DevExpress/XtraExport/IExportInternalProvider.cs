namespace DevExpress.XtraExport
{
    using System;
    using System.IO;

    public interface IExportInternalProvider
    {
        void CommitCache(StreamWriter writer);
        void DeleteCacheFromCell(int col, int row);
        void SetCacheToCell(int col, int row, IExportInternalProvider cache);
    }
}

