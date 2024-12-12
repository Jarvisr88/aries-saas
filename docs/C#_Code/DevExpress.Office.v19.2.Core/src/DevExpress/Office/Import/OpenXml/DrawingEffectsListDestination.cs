namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Utils;
    using System;
    using System.Xml;

    public class DrawingEffectsListDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private DevExpress.Office.Drawing.ContainerEffect containerEffect;

        public DrawingEffectsListDestination(DestinationAndXmlBasedImporter importer) : base(importer)
        {
            this.containerEffect = new DevExpress.Office.Drawing.ContainerEffect(importer.ActualDocumentModel);
        }

        public DrawingEffectsListDestination(DestinationAndXmlBasedImporter importer, DevExpress.Office.Drawing.ContainerEffect containerEffect) : base(importer)
        {
            Guard.ArgumentNotNull(containerEffect, "containerEffect");
            this.containerEffect = containerEffect;
        }

        protected static void AddEffectListHandlerTable(ElementHandlerTable<DestinationAndXmlBasedImporter> table)
        {
            table.Add("blur", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsListDestination.OnBlurEffect));
            table.Add("fillOverlay", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsListDestination.OnFillOverlayEffect));
            table.Add("glow", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsListDestination.OnGlowEffect));
            table.Add("innerShdw", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsListDestination.OnInnerShadowEffect));
            table.Add("outerShdw", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsListDestination.OnOuterShadowEffect));
            table.Add("prstShdw", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsListDestination.OnPresetShadowEffect));
            table.Add("reflection", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsListDestination.OnReflectionEffect));
            table.Add("softEdge", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingEffectsListDestination.OnSoftEdgeEffect));
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            AddEffectListHandlerTable(table);
            return table;
        }

        private static DrawingEffectsListDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (DrawingEffectsListDestination) importer.PeekDestination();

        private static Destination OnBlurEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new BlurEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnFillOverlayEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new FillOverlayEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnGlowEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new GlowEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnInnerShadowEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new InnerShadowEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnOuterShadowEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OuterShadowEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnPresetShadowEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new PresetShadowEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnReflectionEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ReflectionEffectDestination(importer, GetThis(importer).Effects);

        private static Destination OnSoftEdgeEffect(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new SoftEdgeEffectDestination(importer, GetThis(importer).Effects);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.ContainerEffect.SetApplyEffectListCore(true);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;

        public DevExpress.Office.Drawing.ContainerEffect ContainerEffect =>
            this.containerEffect;

        protected DrawingEffectCollection Effects =>
            this.containerEffect.Effects;
    }
}

