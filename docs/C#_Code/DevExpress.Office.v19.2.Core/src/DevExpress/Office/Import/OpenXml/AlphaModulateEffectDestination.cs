namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class AlphaModulateEffectDestination : DrawingEffectDestinationBase
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private DevExpress.Office.Drawing.ContainerEffect containerEffect;

        public AlphaModulateEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override IDrawingEffect CreateEffect() => 
            new AlphaModulateEffect(this.containerEffect);

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("cont", new ElementHandler<DestinationAndXmlBasedImporter>(AlphaModulateEffectDestination.OnCont));
            return table;
        }

        private static AlphaModulateEffectDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (AlphaModulateEffectDestination) importer.PeekDestination();

        private static Destination OnCont(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            AlphaModulateEffectDestination @this = GetThis(importer);
            DevExpress.Office.Drawing.ContainerEffect containerEffect = new DevExpress.Office.Drawing.ContainerEffect(@this.Effects.DocumentModel);
            @this.containerEffect = containerEffect;
            return new DrawingEffectsDAGDestination(importer, containerEffect);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;

        protected DevExpress.Office.Drawing.ContainerEffect ContainerEffect =>
            this.containerEffect;
    }
}

