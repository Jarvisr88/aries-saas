namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.Model;
    using System;
    using System.Xml;

    public class HSLColorDestination : DrawingColorPropertiesDestinationBase
    {
        public HSLColorDestination(DestinationAndXmlBasedImporter importer, DrawingColor color) : base(importer, color)
        {
        }

        protected override void SetColorPropertyValue(XmlReader reader)
        {
            int integerValue = this.Importer.GetIntegerValue(reader, "hue");
            int saturation = this.Importer.GetIntegerValue(reader, "sat");
            int luminance = this.Importer.GetIntegerValue(reader, "lum");
            if ((integerValue == -2147483648) || ((saturation == -2147483648) || (luminance == -2147483648)))
            {
                this.Importer.ThrowInvalidFile();
            }
            base.ColorModelInfo.Hsl = new ColorHSL(integerValue, saturation, luminance);
        }
    }
}

