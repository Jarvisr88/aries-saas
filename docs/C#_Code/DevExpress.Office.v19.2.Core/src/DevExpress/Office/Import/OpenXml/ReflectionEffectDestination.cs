namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class ReflectionEffectDestination : DrawingEffectDestinationBase
    {
        private OuterShadowEffectInfo outerShadowEffectInfo;
        private int startOpacity;
        private int endOpacity;
        private int startPosition;
        private int endPosition;
        private int fadeDirection;

        public ReflectionEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override void CheckPropertyValues()
        {
            DrawingValueChecker.CheckPositiveFixedPercentage(this.startOpacity, "startOpacity");
            DrawingValueChecker.CheckPositiveFixedPercentage(this.startPosition, "startPosition");
            DrawingValueChecker.CheckPositiveFixedPercentage(this.endOpacity, "endOpacity");
            DrawingValueChecker.CheckPositiveFixedPercentage(this.endPosition, "endPosition");
            DrawingValueChecker.CheckPositiveFixedAngle(this.fadeDirection, "fadeDirection");
        }

        protected override IDrawingEffect CreateEffect() => 
            new ReflectionEffect(new ReflectionOpacityInfo(this.startOpacity, this.startPosition, this.endOpacity, this.endPosition, this.fadeDirection), this.outerShadowEffectInfo);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.startOpacity = this.Importer.GetIntegerValue(reader, "stA", 0x186a0);
            this.endOpacity = this.Importer.GetIntegerValue(reader, "endA", 0);
            this.startPosition = this.Importer.GetIntegerValue(reader, "stPos", 0);
            this.endPosition = this.Importer.GetIntegerValue(reader, "endPos", 0x186a0);
            this.fadeDirection = this.Importer.GetIntegerValue(reader, "fadeDir", 0x5265c0);
            this.outerShadowEffectInfo = ImportShadowEffectHelper.GetOuterShadowEffectInfo(this.Importer, reader);
        }
    }
}

