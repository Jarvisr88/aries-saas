namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.OpenXml.Export;
    using System;
    using System.Xml;

    public class OutlinePresetDashDestination : OutlinePropertiesDestinationBase
    {
        public OutlinePresetDashDestination(DestinationAndXmlBasedImporter importer, Outline outline) : base(importer, outline)
        {
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            base.Outline.Dashing = this.Importer.GetWpEnumValue<OutlineDashing>(reader, "val", OpenXmlExporterBase.PresetDashTable, OutlineInfo.DefaultInfo.Dashing);
        }
    }
}

