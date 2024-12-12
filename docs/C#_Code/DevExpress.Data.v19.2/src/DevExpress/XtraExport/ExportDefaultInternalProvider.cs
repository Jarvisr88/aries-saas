namespace DevExpress.XtraExport
{
    using System;
    using System.IO;

    public class ExportDefaultInternalProvider : ExportDefaultProvider, IExportInternalProvider
    {
        public ExportDefaultInternalProvider(Stream stream) : base(stream)
        {
        }

        public ExportDefaultInternalProvider(string fileName) : base(fileName)
        {
        }

        public override IExportProvider Clone(string fileName, Stream stream) => 
            base.IsStreamMode ? new ExportDefaultInternalProvider(base.GetCloneStream(stream)) : new ExportDefaultInternalProvider(base.GetCloneFileName(fileName));

        public virtual void CommitCache(StreamWriter writer)
        {
        }

        public void DeleteCacheFromCell(int col, int row)
        {
            this.SetCacheToCell(col, row, null);
        }

        public void SetCacheToCell(int col, int row, IExportInternalProvider cache)
        {
            base.TestIndex(col, row);
            base.cache[col, row].InternalCache = cache;
        }
    }
}

