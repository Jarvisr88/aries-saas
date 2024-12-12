namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public abstract class ShapePropertiesDestinationBase : DrawingFillDestinationBase
    {
        private readonly DevExpress.Office.Drawing.ShapeProperties shapeProperties;

        protected ShapePropertiesDestinationBase(DestinationAndXmlBasedImporter importer, DevExpress.Office.Drawing.ShapeProperties shapeProperties) : base(importer)
        {
            this.shapeProperties = shapeProperties;
        }

        protected static void AddCommonHandlers(ElementHandlerTable<DestinationAndXmlBasedImporter> table)
        {
            table.Add("ln", new ElementHandler<DestinationAndXmlBasedImporter>(ShapePropertiesDestinationBase.OnOutline));
            table.Add("effectDag", new ElementHandler<DestinationAndXmlBasedImporter>(ShapePropertiesDestinationBase.OnEffectGraph));
            table.Add("effectLst", new ElementHandler<DestinationAndXmlBasedImporter>(ShapePropertiesDestinationBase.OnEffectList));
            table.Add("sp3d", new ElementHandler<DestinationAndXmlBasedImporter>(ShapePropertiesDestinationBase.OnShape3DProperties));
            table.Add("scene3d", new ElementHandler<DestinationAndXmlBasedImporter>(ShapePropertiesDestinationBase.OnScene3DProperties));
            AddFillHandlers(table);
        }

        private static ShapePropertiesDestinationBase GetThis(DestinationAndXmlBasedImporter importer) => 
            (ShapePropertiesDestinationBase) importer.PeekDestination();

        private static Destination OnEffectGraph(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new DrawingEffectsDAGDestination(importer, GetThis(importer).shapeProperties.EffectStyle.ContainerEffect);

        private static Destination OnEffectList(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new DrawingEffectsListDestination(importer, GetThis(importer).shapeProperties.EffectStyle.ContainerEffect);

        private static Destination OnOutline(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OutlineDestination(importer, GetThis(importer).shapeProperties.Outline);

        private static Destination OnScene3DProperties(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new Scene3DPropertiesDestination(importer, GetThis(importer).shapeProperties.EffectStyle.Scene3DProperties);

        private static Destination OnShape3DProperties(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new Shape3DPropertiesDestination(importer, GetThis(importer).shapeProperties.EffectStyle.Shape3DProperties);

        public override void ProcessElementClose(XmlReader reader)
        {
            if (base.Fill != null)
            {
                this.shapeProperties.Fill = base.Fill;
            }
        }

        protected internal DevExpress.Office.Drawing.ShapeProperties ShapeProperties =>
            this.shapeProperties;
    }
}

