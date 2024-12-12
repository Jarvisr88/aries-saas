namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Utils;
    using System;
    using System.Xml;

    public class OfficeThemeEffectStyleDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly DrawingEffectStyle style;

        public OfficeThemeEffectStyleDestination(DestinationAndXmlBasedImporter importer, DrawingEffectStyle style) : base(importer)
        {
            Guard.ArgumentNotNull(style, "DrawingEffectStyle");
            this.style = style;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("effectDag", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeEffectStyleDestination.OnEffectDag));
            table.Add("effectLst", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeEffectStyleDestination.OnEffectList));
            table.Add("scene3d", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeEffectStyleDestination.OnEffectScene3D));
            table.Add("sp3d", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeEffectStyleDestination.OnEffectSp3D));
            return table;
        }

        private static OfficeThemeEffectStyleDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (OfficeThemeEffectStyleDestination) importer.PeekDestination();

        private static Destination OnEffectDag(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new DrawingEffectsDAGDestination(importer, GetThis(importer).style.ContainerEffect);

        private static Destination OnEffectList(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new DrawingEffectsListDestination(importer, GetThis(importer).style.ContainerEffect);

        private static Destination OnEffectScene3D(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new Scene3DPropertiesDestination(importer, GetThis(importer).style.Scene3DProperties);

        private static Destination OnEffectSp3D(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new Shape3DPropertiesDestination(importer, GetThis(importer).style.Shape3DProperties);

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

