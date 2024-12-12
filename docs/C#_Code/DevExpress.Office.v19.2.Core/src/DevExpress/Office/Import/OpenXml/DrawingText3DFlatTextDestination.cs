namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class DrawingText3DFlatTextDestination : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private readonly DrawingTextBodyProperties properties;

        public DrawingText3DFlatTextDestination(DestinationAndXmlBasedImporter importer, DrawingTextBodyProperties properties) : base(importer)
        {
            this.properties = properties;
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            long num = this.Importer.GetLongValue(reader, "z", 0L);
            DrawingValueChecker.CheckCoordinate(num, "FlatTextCoordinate");
            this.properties.Text3D = new DrawingText3DFlatText(this.Importer.DocumentModel.UnitConverter.EmuToModelUnitsL(num));
        }
    }
}

