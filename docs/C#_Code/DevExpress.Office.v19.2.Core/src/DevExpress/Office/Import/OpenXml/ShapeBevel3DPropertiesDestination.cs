namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.OpenXml.Export;
    using System;
    using System.Xml;

    public class ShapeBevel3DPropertiesDestination : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private ShapeBevel3DProperties bevel;

        public ShapeBevel3DPropertiesDestination(DestinationAndXmlBasedImporter importer, ShapeBevel3DProperties bevel) : base(importer)
        {
            this.bevel = bevel;
        }

        private long GetCoordinate(XmlReader reader, string attributeName)
        {
            long num = this.Importer.GetLongValue(reader, attributeName, 0x129a8L);
            DrawingValueChecker.CheckPositiveCoordinate(num, "shape3DCoordinate");
            return this.Importer.DocumentModel.UnitConverter.EmuToModelUnitsL(num);
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.bevel.Width = this.GetCoordinate(reader, "w");
            this.bevel.Height = this.GetCoordinate(reader, "h");
            this.bevel.PresetType = this.Importer.GetWpEnumValue<PresetBevelType>(reader, "prst", OpenXmlExporterBase.PresetBevelTypeTable, PresetBevelType.Circle);
        }
    }
}

