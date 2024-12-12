namespace DevExpress.XtraExport
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing.Printing;
    using System.IO;
    using System.Runtime.CompilerServices;

    public interface IExportProvider : IDisposable
    {
        event ProviderProgressEventHandler ProviderProgress;

        IExportProvider Clone(string fileName, System.IO.Stream stream);
        void Commit();
        ExportCacheCellStyle GetCellStyle(int col, int row);
        int GetColumnWidth(int col);
        ExportCacheCellStyle GetDefaultStyle();
        int GetRowHeight(int row);
        ExportCacheCellStyle GetStyle(int styleIndex);
        int RegisterStyle(ExportCacheCellStyle style);
        void SetCellData(int col, int row, object data);
        void SetCellString(int col, int row, string str);
        void SetCellStyle(int col, int row, ExportCacheCellStyle style);
        void SetCellStyle(int col, int row, int styleIndex);
        void SetCellStyle(int col, int row, int exampleCol, int exampleRow);
        void SetCellStyleAndUnion(int col, int row, int width, int height, ExportCacheCellStyle style);
        void SetCellStyleAndUnion(int col, int row, int width, int height, int styleIndex);
        void SetCellUnion(int col, int row, int width, int height);
        void SetColumnWidth(int col, int width);
        void SetDefaultStyle(ExportCacheCellStyle style);
        void SetPageSettings(MarginsF margins, PaperKind paperKind, bool landscape);
        void SetRange(int width, int height, bool isVisible);
        void SetRowHeight(int row, int height);
        void SetStyle(ExportCacheCellStyle style);
        void SetStyle(int styleIndex);

        bool IsStreamMode { get; }

        System.IO.Stream Stream { get; }
    }
}

