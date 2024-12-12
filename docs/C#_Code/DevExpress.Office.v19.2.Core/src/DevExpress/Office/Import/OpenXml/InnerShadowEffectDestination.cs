namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class InnerShadowEffectDestination : DrawingColorEffectDestinationBase
    {
        private long blurRadius;
        private OffsetShadowInfo info;

        public InnerShadowEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override void CheckPropertyValues()
        {
            base.CheckPropertyValues();
            DrawingValueChecker.CheckPositiveCoordinate(this.blurRadius, "blurRadius");
        }

        protected override IDrawingEffect CreateEffect() => 
            new InnerShadowEffect(this.Color, this.info, this.blurRadius);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.blurRadius = this.Importer.GetLongValue(reader, "blurRad", 0L);
            this.info = ImportShadowEffectHelper.GetOffsetShadowInfo(this.Importer, reader);
        }
    }
}

