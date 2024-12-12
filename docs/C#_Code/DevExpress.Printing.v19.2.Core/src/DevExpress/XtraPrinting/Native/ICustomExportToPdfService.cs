namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.IO;

    public interface ICustomExportToPdfService
    {
        void ExportToPdf(Stream stream, Document document, PdfExportOptions pdfOptions);
    }
}

