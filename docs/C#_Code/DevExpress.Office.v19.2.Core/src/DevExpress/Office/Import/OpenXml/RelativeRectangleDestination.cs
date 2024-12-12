namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Model;
    using System;
    using System.Globalization;
    using System.Xml;

    public class RelativeRectangleDestination : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private Action<RectangleOffset> action;

        public RelativeRectangleDestination(DestinationAndXmlBasedImporter importer, Action<RectangleOffset> action) : base(importer)
        {
            this.action = action;
        }

        private int GetPercentValue(string value, int defaultValue)
        {
            double num;
            value = value.Substring(0, value.Length - 1);
            return (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out num) ? defaultValue : ((int) Math.Round((double) (num * 1000.0))));
        }

        private int GetThousandthOfPercentValue(XmlReader reader, string attributeName, int defaultValue)
        {
            string attribute = reader.GetAttribute(attributeName, null);
            return (!string.IsNullOrEmpty(attribute) ? ((attribute[attribute.Length - 1] != '%') ? this.Importer.GetIntegerValue(attribute, NumberStyles.Integer, defaultValue) : this.GetPercentValue(attribute, defaultValue)) : defaultValue);
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            int bottomOffset = this.GetThousandthOfPercentValue(reader, "b", 0);
            this.action(new RectangleOffset(bottomOffset, this.GetThousandthOfPercentValue(reader, "l", 0), this.GetThousandthOfPercentValue(reader, "r", 0), this.GetThousandthOfPercentValue(reader, "t", 0)));
        }
    }
}

