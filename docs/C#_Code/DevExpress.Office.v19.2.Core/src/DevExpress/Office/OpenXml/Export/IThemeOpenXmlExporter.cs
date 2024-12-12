namespace DevExpress.Office.OpenXml.Export
{
    using DevExpress.Office.Drawing;
    using System;

    public interface IThemeOpenXmlExporter
    {
        void GenerateDrawingColorContent(DrawingColor color);
        void GenerateThemeFormatSchemesContent();
    }
}

