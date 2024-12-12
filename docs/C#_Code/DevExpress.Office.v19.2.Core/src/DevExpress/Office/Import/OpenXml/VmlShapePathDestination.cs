namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.OpenXml.Export;
    using System;
    using System.Globalization;
    using System.Xml;

    public class VmlShapePathDestination : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private readonly VmlShapePath shapePath;

        public VmlShapePathDestination(DestinationAndXmlBasedImporter importer, VmlShapePath shapePath) : base(importer)
        {
            this.shapePath = shapePath;
        }

        private void PrepareLimo(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                int num;
                char[] separator = new char[] { ',' };
                string[] strArray = s.Split(separator);
                if (strArray.Length != 2)
                {
                    this.Importer.ThrowInvalidFile();
                }
                if (int.TryParse(strArray[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
                {
                    this.shapePath.LimoX = num;
                }
                if (int.TryParse(strArray[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
                {
                    this.shapePath.LimoX = num;
                }
            }
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.shapePath.GradientShapeOk = this.Importer.GetOnOffValue(reader, "gradientshapeok", false);
            this.shapePath.ConnectType = this.Importer.GetWpEnumValue<VmlConnectType>(reader, "connecttype", OpenXmlExporterBase.VmlConnectTypeTable, VmlConnectType.None, "urn:schemas-microsoft-com:office:office");
            string str = this.Importer.ReadAttribute(reader, "extrusionok", "urn:schemas-microsoft-com:office:office");
            this.shapePath.ExtrusionOk = string.IsNullOrEmpty(str);
            this.shapePath.FillOk = this.Importer.GetOnOffValue(reader, "fillok", true);
            this.shapePath.ShadowOk = this.Importer.GetOnOffValue(reader, "shadowok", true);
            this.shapePath.StrokeOk = this.Importer.GetOnOffValue(reader, "strokeok", true);
            this.shapePath.Path = this.Importer.ReadAttribute(reader, "v");
            this.PrepareLimo(this.Importer.ReadAttribute(reader, "limo"));
        }
    }
}

