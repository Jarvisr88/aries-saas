namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Globalization;
    using System.Xml;

    public class DrawingGradientLinearDestination : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private readonly DrawingGradientFill fill;

        public DrawingGradientLinearDestination(DestinationAndXmlBasedImporter importer, DrawingGradientFill fill) : base(importer)
        {
            this.fill = fill;
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.fill.GradientType = GradientType.Linear;
            string str = this.Importer.ReadAttribute(reader, "ang");
            if (!string.IsNullOrEmpty(str))
            {
                this.fill.Angle = this.Importer.DocumentModel.UnitConverter.AdjAngleToModelUnits(this.Importer.GetIntegerValue(str, NumberStyles.Integer, 0));
            }
            str = this.Importer.ReadAttribute(reader, "scaled");
            if (!string.IsNullOrEmpty(str))
            {
                this.fill.Scaled = this.Importer.GetOnOffValue(str, false);
            }
        }
    }
}

