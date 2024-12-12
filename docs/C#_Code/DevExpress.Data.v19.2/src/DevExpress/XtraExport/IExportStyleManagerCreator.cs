namespace DevExpress.XtraExport
{
    using System;
    using System.IO;

    public interface IExportStyleManagerCreator
    {
        ExportStyleManagerBase CreateInstance(string fileName, Stream stream);
    }
}

