namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Model;
    using System;
    using System.Xml;

    public class OfficeThemeColorDestination : DrawingColorDestination
    {
        private readonly ThemeColorIndex colorIndex;

        public OfficeThemeColorDestination(DestinationAndXmlBasedImporter importer, ThemeColorIndex colorIndex) : base(importer, new DrawingColor(importer.ActualDocumentModel))
        {
            this.colorIndex = colorIndex;
        }

        public override void ProcessElementClose(XmlReader reader)
        {
            this.Importer.DocumentModel.OfficeTheme.Colors.SetDrawingColor(this.colorIndex, this.Color);
        }
    }
}

