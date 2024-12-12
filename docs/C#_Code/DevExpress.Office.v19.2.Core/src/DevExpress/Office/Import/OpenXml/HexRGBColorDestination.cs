namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class HexRGBColorDestination : DrawingColorPropertiesDestinationBase
    {
        public HexRGBColorDestination(DestinationAndXmlBasedImporter importer, DrawingColor color) : base(importer, color)
        {
        }

        protected override void SetColorPropertyValue(XmlReader reader)
        {
            base.ColorModelInfo.Rgb = DrawingColorModelInfo.SRgbToRgb(this.Importer.ReadAttribute(reader, "val"));
        }
    }
}

