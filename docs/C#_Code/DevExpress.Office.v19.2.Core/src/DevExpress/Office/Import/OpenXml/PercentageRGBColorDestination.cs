namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.Model;
    using System;
    using System.Xml;

    public class PercentageRGBColorDestination : DrawingColorPropertiesDestinationBase
    {
        public PercentageRGBColorDestination(DestinationAndXmlBasedImporter importer, DrawingColor color) : base(importer, color)
        {
        }

        protected override void SetColorPropertyValue(XmlReader reader)
        {
            int integerValue = this.Importer.GetIntegerValue(reader, "r");
            int scG = this.Importer.GetIntegerValue(reader, "g");
            int scB = this.Importer.GetIntegerValue(reader, "b");
            if ((integerValue == -2147483648) || ((scG == -2147483648) || (scB == -2147483648)))
            {
                this.Importer.ThrowInvalidFile();
            }
            base.ColorModelInfo.ScRgb = new ScRGBColor(integerValue, scG, scB);
        }
    }
}

