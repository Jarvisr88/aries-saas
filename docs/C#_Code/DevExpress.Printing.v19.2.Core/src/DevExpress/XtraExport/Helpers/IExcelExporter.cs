namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using System;
    using System.IO;

    public interface IExcelExporter
    {
        void Export(Stream stream);
        void Export(string outputpath);
        void ExportSheet(IXlExport exporter);
    }
}

