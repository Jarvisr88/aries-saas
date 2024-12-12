namespace DevExpress.Printing.ExportHelpers
{
    using System;
    using System.Drawing;

    public interface ISheetHeaderFooterExportContext : IExportContext
    {
        void InsertImage(Image image, Size s);
    }
}

