namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.OpenXml.Export;
    using System;
    using System.Xml;

    public static class ImportShadowEffectHelper
    {
        public static OffsetShadowInfo GetOffsetShadowInfo(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            long num = importer.GetLongValue(reader, "dist", 0L);
            int num2 = importer.GetIntegerValue(reader, "dir", 0);
            DrawingValueChecker.CheckPositiveCoordinate(num, "distance");
            DrawingValueChecker.CheckPositiveFixedAngle(num2, "direction");
            return new OffsetShadowInfo(num, num2);
        }

        public static OuterShadowEffectInfo GetOuterShadowEffectInfo(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            RectangleAlignType shadowAlignment = importer.GetWpEnumValue<RectangleAlignType>(reader, "algn", OpenXmlExporterBase.RectangleAlignTypeTable, RectangleAlignType.Bottom);
            long num = importer.GetLongValue(reader, "blurRad", 0L);
            DrawingValueChecker.CheckPositiveCoordinate(num, "blurRadius");
            return new OuterShadowEffectInfo(GetOffsetShadowInfo(importer, reader), GetScalingFactorInfo(importer, reader), GetSkewAnglesInfo(importer, reader), shadowAlignment, num, importer.GetOnOffValue(reader, "rotWithShape", true));
        }

        public static ScalingFactorInfo GetScalingFactorInfo(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ScalingFactorInfo(importer.GetIntegerValue(reader, "sx", 0x186a0), importer.GetIntegerValue(reader, "sy", 0x186a0));

        public static SkewAnglesInfo GetSkewAnglesInfo(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            int num = importer.GetIntegerValue(reader, "kx", 0);
            int num2 = importer.GetIntegerValue(reader, "ky", 0);
            DrawingValueChecker.CheckFixedAngle(num, "horizontalSkew");
            DrawingValueChecker.CheckFixedAngle(num2, "verticalSkew");
            return new SkewAnglesInfo(num, num2);
        }
    }
}

