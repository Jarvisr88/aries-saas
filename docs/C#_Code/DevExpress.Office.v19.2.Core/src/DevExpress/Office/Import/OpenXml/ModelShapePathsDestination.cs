namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class ModelShapePathsDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly ModelShapePathsList modelShapePathsList;
        private readonly AdjustableCoordinateCache adjustableCoordinateCache;
        private readonly AdjustableAngleCache adjustableAngleCache;

        public ModelShapePathsDestination(DestinationAndXmlBasedImporter importer, ModelShapePathsList modelShapePathsList, AdjustableCoordinateCache adjustableCoordinateCache, AdjustableAngleCache adjustableAngleCache) : base(importer)
        {
            this.modelShapePathsList = modelShapePathsList;
            this.adjustableCoordinateCache = adjustableCoordinateCache;
            this.adjustableAngleCache = adjustableAngleCache;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("path", new ElementHandler<DestinationAndXmlBasedImporter>(ModelShapePathsDestination.OnModelShapePath));
            return table;
        }

        private static ModelShapePathsDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (ModelShapePathsDestination) importer.PeekDestination();

        private static Destination OnModelShapePath(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            ModelShapePath item = new ModelShapePath(importer.DocumentModel.MainPart);
            GetThis(importer).modelShapePathsList.Add(item);
            return new ModelShapePathDestination(importer, item, GetThis(importer).adjustableCoordinateCache, GetThis(importer).adjustableAngleCache);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

