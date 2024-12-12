namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.OpenXml.Export;
    using DevExpress.Utils;
    using System;
    using System.Xml;

    public class Shape3DPropertiesDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private Shape3DProperties shape3d;

        public Shape3DPropertiesDestination(DestinationAndXmlBasedImporter importer, Shape3DProperties shape3d) : base(importer)
        {
            Guard.ArgumentNotNull(shape3d, "shape3d");
            this.shape3d = shape3d;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("bevelT", new ElementHandler<DestinationAndXmlBasedImporter>(Shape3DPropertiesDestination.OnTopBevel));
            table.Add("bevelB", new ElementHandler<DestinationAndXmlBasedImporter>(Shape3DPropertiesDestination.OnBottomBevel));
            table.Add("extrusionClr", new ElementHandler<DestinationAndXmlBasedImporter>(Shape3DPropertiesDestination.OnExtrusionColor));
            table.Add("contourClr", new ElementHandler<DestinationAndXmlBasedImporter>(Shape3DPropertiesDestination.OnContourColor));
            return table;
        }

        private long GetCoordinate(XmlReader reader, string attributeName)
        {
            long num = this.Importer.GetLongValue(reader, attributeName, 0L);
            DrawingValueChecker.CheckPositiveCoordinate(num, "shape3DCoordinate");
            return this.Importer.DocumentModel.UnitConverter.EmuToModelUnitsL(num);
        }

        private static Shape3DPropertiesDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (Shape3DPropertiesDestination) importer.PeekDestination();

        private static Destination OnBottomBevel(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ShapeBevel3DPropertiesDestination(importer, GetThis(importer).shape3d.BottomBevel);

        private static Destination OnContourColor(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new DrawingColorDestination(importer, GetThis(importer).shape3d.ContourColor);

        private static Destination OnExtrusionColor(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new DrawingColorDestination(importer, GetThis(importer).shape3d.ExtrusionColor);

        private static Destination OnTopBevel(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ShapeBevel3DPropertiesDestination(importer, GetThis(importer).shape3d.TopBevel);

        public override void ProcessElementClose(XmlReader reader)
        {
            this.Importer.DocumentModel.EndUpdate();
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.Importer.DocumentModel.BeginUpdate();
            long num = this.Importer.GetLongValue(reader, "z", 0L);
            DrawingValueChecker.CheckCoordinate(num, "shape3DCoordinate");
            this.shape3d.ShapeDepth = this.Importer.DocumentModel.UnitConverter.EmuToModelUnitsL(num);
            this.shape3d.ExtrusionHeight = this.GetCoordinate(reader, "extrusionH");
            this.shape3d.ContourWidth = this.GetCoordinate(reader, "contourW");
            this.shape3d.PresetMaterial = this.Importer.GetWpEnumValue<PresetMaterialType>(reader, "prstMaterial", OpenXmlExporterBase.PresetMaterialTypeTable, PresetMaterialType.WarmMatte);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

