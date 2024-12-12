namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.DrawingML;
    using System;
    using System.Xml;

    public abstract class Scene3DInfoDestinationBase : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly IDrawingCache cache;
        private readonly Scene3DProperties properties;
        private readonly Scene3DPropertiesInfo info;
        private readonly Scene3DRotationInfo rotationInfo;
        private bool hasRotation;

        protected Scene3DInfoDestinationBase(DestinationAndXmlBasedImporter importer, IDrawingCache cache, Scene3DProperties properties, Scene3DPropertiesInfo info) : base(importer)
        {
            this.cache = cache;
            this.properties = properties;
            this.info = info;
            this.rotationInfo = cache.Scene3DRotationInfoCache.DefaultItem.Clone();
            this.hasRotation = false;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("rot", new ElementHandler<DestinationAndXmlBasedImporter>(Scene3DInfoDestinationBase.OnRotation));
            return table;
        }

        private static Scene3DInfoDestinationBase GetThis(DestinationAndXmlBasedImporter importer) => 
            (Scene3DInfoDestinationBase) importer.PeekDestination();

        private static Destination OnRotation(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            Scene3DInfoDestinationBase @this = GetThis(importer);
            @this.hasRotation = true;
            return new Scene3DRotationDestination(importer, @this.rotationInfo);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;

        protected internal IDrawingCache Cache =>
            this.cache;

        protected internal Scene3DProperties Properties =>
            this.properties;

        protected internal Scene3DPropertiesInfo Info =>
            this.info;

        protected internal Scene3DRotationInfo RotationInfo =>
            this.rotationInfo;

        protected internal bool HasRotation =>
            this.hasRotation;
    }
}

