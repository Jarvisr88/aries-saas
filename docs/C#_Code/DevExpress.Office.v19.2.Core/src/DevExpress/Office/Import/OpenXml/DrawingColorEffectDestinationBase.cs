namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public abstract class DrawingColorEffectDestinationBase : DrawingColorDestinationBase
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly DrawingEffectCollection effects;

        protected DrawingColorEffectDestinationBase(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, new DrawingColor(effects.DocumentModel))
        {
            this.effects = effects;
        }

        protected virtual void CheckPropertyValues()
        {
            if (!this.HasColor)
            {
                this.Importer.ThrowInvalidFile();
            }
        }

        protected abstract IDrawingEffect CreateEffect();
        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            AddDrawingColorHandlers(table);
            return table;
        }

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

