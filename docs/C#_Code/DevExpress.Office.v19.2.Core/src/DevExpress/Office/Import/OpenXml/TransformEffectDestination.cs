namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class TransformEffectDestination : DrawingEffectDestinationBase
    {
        private ScalingFactorInfo scalingFactorInfo;
        private SkewAnglesInfo skewAnglesInfo;
        private long horizontalShift;
        private long verticalShift;

        public TransformEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override void CheckPropertyValues()
        {
            DrawingValueChecker.CheckCoordinate(this.horizontalShift, "horizontalShift");
            DrawingValueChecker.CheckCoordinate(this.verticalShift, "verticalShift");
        }

        protected override IDrawingEffect CreateEffect() => 
            new TransformEffect(this.scalingFactorInfo, this.skewAnglesInfo, new CoordinateShiftInfo(this.horizontalShift, this.verticalShift));

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.scalingFactorInfo = ImportShadowEffectHelper.GetScalingFactorInfo(this.Importer, reader);
            this.skewAnglesInfo = ImportShadowEffectHelper.GetSkewAnglesInfo(this.Importer, reader);
            this.horizontalShift = this.Importer.GetLongValue(reader, "tx", 0L);
            this.verticalShift = this.Importer.GetLongValue(reader, "ty", 0L);
        }
    }
}

