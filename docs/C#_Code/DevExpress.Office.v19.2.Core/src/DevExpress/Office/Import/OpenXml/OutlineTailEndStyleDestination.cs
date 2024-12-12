namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.OpenXml.Export;
    using System;
    using System.Xml;

    public class OutlineTailEndStyleDestination : OutlinePropertiesDestinationBase
    {
        public OutlineTailEndStyleDestination(DestinationAndXmlBasedImporter importer, Outline outline) : base(importer, outline)
        {
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            OutlineHeadTailSize? nullable = this.Importer.GetWpEnumOnOffNullValue<OutlineHeadTailSize>(reader, "len", OpenXmlExporterBase.HeadTailSizeTable);
            if (nullable != null)
            {
                base.Outline.TailLength = nullable.Value;
            }
            OutlineHeadTailType? nullable2 = this.Importer.GetWpEnumOnOffNullValue<OutlineHeadTailType>(reader, "type", OpenXmlExporterBase.HeadTailTypeTable);
            if (nullable2 != null)
            {
                base.Outline.TailType = nullable2.Value;
            }
            OutlineHeadTailSize? nullable3 = this.Importer.GetWpEnumOnOffNullValue<OutlineHeadTailSize>(reader, "w", OpenXmlExporterBase.HeadTailSizeTable);
            if (nullable3 != null)
            {
                base.Outline.TailWidth = nullable3.Value;
            }
        }
    }
}

