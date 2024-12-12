namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class OuterShadowEffectDestination : DrawingColorEffectDestinationBase
    {
        private OuterShadowEffectInfo info;

        public OuterShadowEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override IDrawingEffect CreateEffect() => 
            new OuterShadowEffect(this.Color, this.info);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.info = ImportShadowEffectHelper.GetOuterShadowEffectInfo(this.Importer, reader);
        }
    }
}

