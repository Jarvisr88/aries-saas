namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Utils;
    using System;
    using System.Xml;

    public class AlphaModulateFixedEffectDestination : DrawingEffectDestinationBase
    {
        private int amount;

        public AlphaModulateFixedEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override void CheckPropertyValues()
        {
            Guard.ArgumentNonNegative(this.amount, "amount");
        }

        protected override IDrawingEffect CreateEffect() => 
            new AlphaModulateFixedEffect(this.amount);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.amount = this.Importer.GetIntegerValue(reader, "amt", 0x186a0);
        }
    }
}

