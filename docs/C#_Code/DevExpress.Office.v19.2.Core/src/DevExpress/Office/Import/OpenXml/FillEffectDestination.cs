namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class FillEffectDestination : DrawingFillDestinationBase
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly DrawingEffectCollection effects;

        public FillEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer)
        {
            this.effects = effects;
        }

        protected virtual IDrawingEffect CreateEffect() => 
            new FillEffect(base.Fill);

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            AddFillHandlers(table);
            return table;
        }

        public override void ProcessElementClose(XmlReader reader)
        {
            this.effects.Add(this.CreateEffect());
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

