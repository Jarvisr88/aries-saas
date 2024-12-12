namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class PresetColorDestination : DrawingColorPropertiesDestinationBase
    {
        public PresetColorDestination(DestinationAndXmlBasedImporter importer, DrawingColor color) : base(importer, color)
        {
        }

        protected override void SetColorPropertyValue(XmlReader reader)
        {
            base.ColorModelInfo.Preset = this.Importer.ReadAttribute(reader, "val");
        }
    }
}

