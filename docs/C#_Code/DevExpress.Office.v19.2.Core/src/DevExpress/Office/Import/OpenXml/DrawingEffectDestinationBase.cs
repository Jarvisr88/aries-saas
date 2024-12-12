namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public abstract class DrawingEffectDestinationBase : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
        private readonly DrawingEffectCollection effects;

        protected DrawingEffectDestinationBase(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer)
        {
            this.effects = effects;
        }

        protected virtual void CheckPropertyValues()
        {
        }

        protected abstract IDrawingEffect CreateEffect();
        public override void ProcessElementClose(XmlReader reader)
        {
            this.CheckPropertyValues();
            this.effects.AddCore(this.CreateEffect());
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;

        protected DrawingEffectCollection Effects =>
            this.effects;
    }
}

