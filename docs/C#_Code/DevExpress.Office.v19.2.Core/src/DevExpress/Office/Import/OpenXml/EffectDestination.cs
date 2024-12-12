namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class EffectDestination : DrawingEffectDestinationBase
    {
        private string referenceToken;

        public EffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override IDrawingEffect CreateEffect() => 
            new Effect(this.referenceToken);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.referenceToken = this.Importer.ReadAttribute(reader, "ref");
        }
    }
}

