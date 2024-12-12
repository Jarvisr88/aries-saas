namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.OpenXml.Export;
    using System;
    using System.Xml;

    public class DrawingPresetTextWarpDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly DrawingTextBodyProperties properties;

        public DrawingPresetTextWarpDestination(DestinationAndXmlBasedImporter importer, DrawingTextBodyProperties properties) : base(importer)
        {
            this.properties = properties;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("avLst", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingPresetTextWarpDestination.OnAdjustValueList));
            return table;
        }

        private static DrawingPresetTextWarpDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (DrawingPresetTextWarpDestination) importer.PeekDestination();

        private static Destination OnAdjustValueList(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ModelShapeGuideListDestination(importer, GetThis(importer).properties.PresetAdjustValues, new AdjustableCoordinateCache());

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.properties.PresetTextWarp = this.Importer.GetWpEnumValue<DrawingPresetTextWarp>(reader, "prst", OpenXmlExporterBase.PresetTextWarpTable, DrawingPresetTextWarp.NoShape);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

