namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class ShapeLocksDestination : DrawingLockingDestination
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly ShapeLocks shapeLocks;

        public ShapeLocksDestination(DestinationAndXmlBasedImporter importer, ShapeLocks shapeLocks) : base(importer, shapeLocks)
        {
            this.shapeLocks = shapeLocks;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable() => 
            new ElementHandlerTable<DestinationAndXmlBasedImporter>();

        public override void ProcessElementOpen(XmlReader reader)
        {
            base.ProcessElementOpen(reader);
            this.shapeLocks.NoTextEdit = this.Importer.GetWpSTOnOffValue(reader, "noTextEdit", false);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

