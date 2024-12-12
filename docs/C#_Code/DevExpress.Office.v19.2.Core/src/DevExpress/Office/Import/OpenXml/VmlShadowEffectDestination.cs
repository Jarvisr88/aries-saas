namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.OpenXml.Export;
    using DevExpress.Office.Utils;
    using System;
    using System.Xml;

    public class VmlShadowEffectDestination : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private readonly VmlShadowEffect shadowEffect;

        public VmlShadowEffectDestination(DestinationAndXmlBasedImporter importer, VmlShadowEffect shadowEffect) : base(importer)
        {
            this.shadowEffect = shadowEffect;
        }

        private void PrepareMatrix(string value, VmlMatrix matrix)
        {
            if (!string.IsNullOrEmpty(value))
            {
                char[] separator = new char[] { ',' };
                string[] strArray = value.Split(separator, StringSplitOptions.None);
                string str = (strArray.Length != 0) ? strArray[0] : string.Empty;
                string str2 = (strArray.Length > 1) ? strArray[1] : string.Empty;
                string str3 = (strArray.Length > 2) ? strArray[2] : string.Empty;
                string str4 = (strArray.Length > 3) ? strArray[3] : string.Empty;
                string str5 = (strArray.Length > 4) ? strArray[4] : string.Empty;
                string str6 = (strArray.Length > 5) ? strArray[5] : string.Empty;
                matrix.Sxx = VmlDrawingImportHelper.GetFloatOrVulgarFractionValue(str, matrix.Sxx, 65536f);
                matrix.Sxy = VmlDrawingImportHelper.GetFloatOrVulgarFractionValue(str2, matrix.Sxy, 65536f);
                matrix.Syx = VmlDrawingImportHelper.GetFloatOrVulgarFractionValue(str3, matrix.Syx, 65536f);
                matrix.Syy = VmlDrawingImportHelper.GetFloatOrVulgarFractionValue(str4, matrix.Syy, 65536f);
                matrix.Px = VmlDrawingImportHelper.GetFloatOrVulgarFractionValue(str5, matrix.Px, 65536f);
                matrix.Py = VmlDrawingImportHelper.GetFloatOrVulgarFractionValue(str6, matrix.Py, 65536f);
            }
        }

        private void PrepareOffset(string value, VmlCoordUnit offset)
        {
            if (!string.IsNullOrEmpty(value))
            {
                char[] separator = new char[] { ',' };
                string[] strArray = value.Split(separator, StringSplitOptions.None);
                string str = (strArray.Length != 0) ? strArray[0] : string.Empty;
                string str2 = (strArray.Length > 1) ? strArray[1] : string.Empty;
                VmlUnitConverter converter = new VmlUnitConverter(this.Importer.DocumentModel.UnitConverter);
                if (!string.IsNullOrEmpty(str))
                {
                    offset.X = converter.ToModelUnits(new DXVmlUnit(str));
                }
                if (!string.IsNullOrEmpty(str2))
                {
                    offset.Y = converter.ToModelUnits(new DXVmlUnit(str2));
                }
            }
        }

        private void PrepareOrigin(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                char[] separator = new char[] { ',' };
                string[] strArray = value.Split(separator, StringSplitOptions.None);
                string str = (strArray.Length != 0) ? strArray[0] : string.Empty;
                string str2 = (strArray.Length > 1) ? strArray[1] : string.Empty;
                this.shadowEffect.OriginX = VmlDrawingImportHelper.GetFloatOrPercentageValue(str);
                this.shadowEffect.OriginY = VmlDrawingImportHelper.GetFloatOrPercentageValue(str2);
            }
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.shadowEffect.Color = VmlDrawingImportHelper.GetVmlSTColorValue(reader, "color", this.shadowEffect.Color);
            this.shadowEffect.Color2 = VmlDrawingImportHelper.GetVmlSTColorValue(reader, "color2", this.shadowEffect.Color2);
            this.shadowEffect.Obscured = this.Importer.GetOnOffValue(reader, "obscured", false);
            string str = this.Importer.ReadAttribute(reader, "on");
            if (str != null)
            {
                this.shadowEffect.On = this.Importer.GetOnOffValue(str, true);
            }
            this.shadowEffect.Opacity = VmlDrawingImportHelper.GetFloatOrVulgarFractionValue(this.Importer.ReadAttribute(reader, "opacity"), this.shadowEffect.Opacity, 65536f);
            this.shadowEffect.ShadowType = this.Importer.GetWpEnumValue<VmlShadowType>(reader, "type", OpenXmlExporterBase.VmlShadowTypeTable, this.shadowEffect.ShadowType);
            this.PrepareMatrix(this.Importer.ReadAttribute(reader, "matrix"), this.shadowEffect.Matrix);
            this.PrepareOffset(this.Importer.ReadAttribute(reader, "offset"), this.shadowEffect.Offset);
            this.PrepareOffset(this.Importer.ReadAttribute(reader, "offset2"), this.shadowEffect.Offset2);
            this.PrepareOrigin(this.Importer.ReadAttribute(reader, "origin"));
        }
    }
}

