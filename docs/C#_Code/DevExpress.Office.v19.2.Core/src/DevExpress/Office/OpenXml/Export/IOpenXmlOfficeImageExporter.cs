namespace DevExpress.Office.OpenXml.Export
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using System;

    public interface IOpenXmlOfficeImageExporter
    {
        string ExportExternalImageData(string imageLink, OfficeImage image);
        string ExportImageData(IDocumentModel documentModel, OfficeImage image);
    }
}

