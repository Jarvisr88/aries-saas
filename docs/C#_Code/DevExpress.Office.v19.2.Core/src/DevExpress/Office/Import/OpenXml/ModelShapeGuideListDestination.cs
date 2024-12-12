namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class ModelShapeGuideListDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly ModelShapeGuideList modelShapeGuideList;
        private readonly AdjustableCoordinateCache adjustableCoordinateCache;

        public ModelShapeGuideListDestination(DestinationAndXmlBasedImporter importer, ModelShapeGuideList modelShapeGuideList, AdjustableCoordinateCache adjustableCoordinateCache) : base(importer)
        {
            this.modelShapeGuideList = modelShapeGuideList;
            this.adjustableCoordinateCache = adjustableCoordinateCache;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("gd", new ElementHandler<DestinationAndXmlBasedImporter>(ModelShapeGuideListDestination.OnShapeGuide));
            return table;
        }

        private static ModelShapeGuideListDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (ModelShapeGuideListDestination) importer.PeekDestination();

        private static Destination OnShapeGuide(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            ModelShapeGuide item = new ModelShapeGuide();
            GetThis(importer).modelShapeGuideList.Add(item);
            return new ModelShapeGuideDestination(importer, item, GetThis(importer).adjustableCoordinateCache);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

