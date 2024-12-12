namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.DrawingML;
    using System;
    using System.Xml;

    public class Scene3DBackdropPlaneDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly Scene3DProperties scene3d;
        private Scene3DVector anchorPoint;
        private Scene3DVector normalVector;
        private Scene3DVector upVector;

        public Scene3DBackdropPlaneDestination(DestinationAndXmlBasedImporter importer, Scene3DProperties scene3d) : base(importer)
        {
            this.scene3d = scene3d;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("anchor", new ElementHandler<DestinationAndXmlBasedImporter>(Scene3DBackdropPlaneDestination.OnAnchorPoint));
            table.Add("norm", new ElementHandler<DestinationAndXmlBasedImporter>(Scene3DBackdropPlaneDestination.OnNormalVector));
            table.Add("up", new ElementHandler<DestinationAndXmlBasedImporter>(Scene3DBackdropPlaneDestination.OnUpVector));
            return table;
        }

        private static Scene3DBackdropPlaneDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (Scene3DBackdropPlaneDestination) importer.PeekDestination();

        private static Destination OnAnchorPoint(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            Scene3DBackdropPlaneDestination @this = GetThis(importer);
            Scene3DVector vector = new Scene3DVector(@this.scene3d.DocumentModel);
            @this.anchorPoint = vector;
            return new Scene3DAnchorPointDestination(importer, vector);
        }

        private static Destination OnNormalVector(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            Scene3DBackdropPlaneDestination @this = GetThis(importer);
            Scene3DVector vector = new Scene3DVector(@this.scene3d.DocumentModel);
            @this.normalVector = vector;
            return new Scene3DVectorDestination(importer, vector);
        }

        private static Destination OnUpVector(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            Scene3DBackdropPlaneDestination @this = GetThis(importer);
            Scene3DVector vector = new Scene3DVector(@this.scene3d.DocumentModel);
            @this.upVector = vector;
            return new Scene3DVectorDestination(importer, vector);
        }

        public override void ProcessElementClose(XmlReader reader)
        {
            if ((this.anchorPoint == null) || ((this.normalVector == null) || (this.upVector == null)))
            {
                this.Importer.ThrowInvalidFile();
            }
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

