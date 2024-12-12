namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.DrawingML;
    using DevExpress.Utils;
    using System;
    using System.Xml;

    public class Scene3DPropertiesDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly Scene3DProperties scene3d;
        private Scene3DPropertiesInfo info;
        private bool hasCamera;
        private bool hasLightRig;

        public Scene3DPropertiesDestination(DestinationAndXmlBasedImporter importer, Scene3DProperties scene3d) : base(importer)
        {
            Guard.ArgumentNotNull(scene3d, "scene3d");
            this.scene3d = scene3d;
            this.info = scene3d.Info.Clone();
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("backdrop", new ElementHandler<DestinationAndXmlBasedImporter>(Scene3DPropertiesDestination.OnBackdropPlane));
            table.Add("camera", new ElementHandler<DestinationAndXmlBasedImporter>(Scene3DPropertiesDestination.OnCamera));
            table.Add("lightRig", new ElementHandler<DestinationAndXmlBasedImporter>(Scene3DPropertiesDestination.OnLightRig));
            return table;
        }

        private static Scene3DPropertiesDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (Scene3DPropertiesDestination) importer.PeekDestination();

        private static Destination OnBackdropPlane(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new Scene3DBackdropPlaneDestination(importer, GetThis(importer).scene3d);

        private static Destination OnCamera(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            Scene3DPropertiesDestination @this = GetThis(importer);
            @this.hasCamera = true;
            return new Scene3DCameraDestination(importer, @this.scene3d.DrawingCache, @this.scene3d, @this.info);
        }

        private static Destination OnLightRig(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            Scene3DPropertiesDestination @this = GetThis(importer);
            @this.hasLightRig = true;
            return new Scene3DLightRigDestination(importer, @this.scene3d.DrawingCache, @this.scene3d, @this.info);
        }

        public override void ProcessElementClose(XmlReader reader)
        {
            if (!this.hasCamera || !this.hasLightRig)
            {
                this.Importer.ThrowInvalidFile();
            }
            this.scene3d.AssignInfoIndex(this.scene3d.DrawingCache.Scene3DPropertiesInfoCache.AddItem(this.info));
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

