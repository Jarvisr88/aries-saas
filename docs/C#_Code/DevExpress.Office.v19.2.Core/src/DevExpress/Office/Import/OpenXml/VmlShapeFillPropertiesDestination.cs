namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.OpenXml.Export;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Xml;

    public class VmlShapeFillPropertiesDestination : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private readonly float[] vector2DDefaultValue;
        private readonly VmlShapeFillProperties shapeFillProperties;

        public VmlShapeFillPropertiesDestination(DestinationAndXmlBasedImporter importer, VmlShapeFillProperties shapeFillProperties) : base(importer)
        {
            this.vector2DDefaultValue = new float[2];
            this.shapeFillProperties = shapeFillProperties;
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.shapeFillProperties.Color = VmlDrawingImportHelper.GetVmlSTColorValue(reader, "color", this.shapeFillProperties.Color);
            string str = this.Importer.ReadAttribute(reader, "color2");
            if (!string.IsNullOrEmpty(str))
            {
                this.shapeFillProperties.Color2 = !str.StartsWith("fill darken") ? (!str.StartsWith("fill lighten") ? VmlDrawingImportHelper.ParseColor(str, DXColor.White) : VmlDrawingImportHelper.CreateLightenColor(str, this.shapeFillProperties.Color)) : VmlDrawingImportHelper.CreateDarkenColor(str, this.shapeFillProperties.Color);
            }
            this.shapeFillProperties.Filled = this.Importer.GetOnOffNullValue(reader, "on");
            this.shapeFillProperties.Opacity = VmlDrawingImportHelper.GetWpSTFloatOrVulgarFractionValue(reader, "opacity", this.shapeFillProperties.Opacity, 65536f);
            string str2 = this.Importer.ReadAttribute(reader, "opacity2", "urn:schemas-microsoft-com:office:office");
            if (!string.IsNullOrEmpty(str2))
            {
                this.shapeFillProperties.Opacity2 = VmlDrawingImportHelper.GetFloatOrVulgarFractionValue(str2, this.shapeFillProperties.Opacity2, 65536f);
            }
            this.shapeFillProperties.Focus = VmlDrawingImportHelper.ReadPercentageAttribute(reader, "focus");
            this.shapeFillProperties.Method = this.Importer.GetWpEnumValue<VmlFillMethod>(reader, "method", OpenXmlExporterBase.VmlFillMethodTable, VmlFillMethod.Sigma);
            this.shapeFillProperties.Type = this.Importer.GetWpEnumValue<VmlFillType>(reader, "type", OpenXmlExporterBase.VmlFillTypeTable, VmlFillType.Solid);
            this.shapeFillProperties.Aspect = this.Importer.GetWpEnumValue<VmlImageAspect>(reader, "aspect", OpenXmlExporterBase.VmlImageAspectTable, VmlImageAspect.Ignore);
            this.shapeFillProperties.Recolor = this.Importer.GetOnOffValue(reader, "recolor", false);
            this.shapeFillProperties.Rotate = this.Importer.GetOnOffValue(reader, "rotate", false);
            this.shapeFillProperties.Title = this.Importer.ReadAttribute(reader, "title", "urn:schemas-microsoft-com:office:office");
            this.shapeFillProperties.Angle = this.Importer.GetWpSTFloatValue(reader, "angle", NumberStyles.Float, 0f);
            string str3 = this.Importer.ReadAttribute(reader, "size");
            if (!string.IsNullOrEmpty(str3))
            {
                string[] strArray = this.SplitCoordinates(str3);
                this.shapeFillProperties.SizeX = VmlDrawingImportHelper.GetFloatOrVulgarFractionValue(strArray[0], 0f, 65536f);
                this.shapeFillProperties.SizeY = VmlDrawingImportHelper.GetFloatOrVulgarFractionValue(strArray[1], 0f, 65536f);
            }
            str3 = this.Importer.ReadAttribute(reader, "origin");
            if (!string.IsNullOrEmpty(str3))
            {
                string[] strArray2 = this.SplitCoordinates(str3);
                this.shapeFillProperties.OriginX = VmlDrawingImportHelper.GetFloatOrVulgarFractionValue(strArray2[0], 0f, 65536f);
                this.shapeFillProperties.OriginY = VmlDrawingImportHelper.GetFloatOrVulgarFractionValue(strArray2[1], 0f, 65536f);
            }
            str3 = this.Importer.ReadAttribute(reader, "position");
            if (!string.IsNullOrEmpty(str3))
            {
                string[] strArray3 = this.SplitCoordinates(str3);
                this.shapeFillProperties.PositionX = VmlDrawingImportHelper.GetFloatOrVulgarFractionValue(strArray3[0], 0f, 65536f);
                this.shapeFillProperties.PositionY = VmlDrawingImportHelper.GetFloatOrVulgarFractionValue(strArray3[1], 0f, 65536f);
            }
            str3 = this.Importer.ReadAttribute(reader, "focusposition");
            if (!string.IsNullOrEmpty(str3))
            {
                float[] numArray = this.SplitVector2D(str3, this.vector2DDefaultValue);
                this.shapeFillProperties.FocusPosition.Left = numArray[0];
                this.shapeFillProperties.FocusPosition.Top = numArray[1];
            }
            str3 = this.Importer.ReadAttribute(reader, "focussize");
            if (!string.IsNullOrEmpty(str3))
            {
                float[] numArray2 = this.SplitVector2D(str3, this.vector2DDefaultValue);
                this.shapeFillProperties.FocusSize.Width = numArray2[0];
                this.shapeFillProperties.FocusSize.Height = numArray2[1];
            }
            str3 = this.Importer.ReadAttribute(reader, "colors");
            if (!string.IsNullOrEmpty(str3))
            {
                char[] separator = new char[] { ';' };
                foreach (string str5 in str3.Split(separator, StringSplitOptions.RemoveEmptyEntries))
                {
                    char[] chArray2 = new char[] { ' ' };
                    string[] strArray6 = str5.Split(chArray2, StringSplitOptions.RemoveEmptyEntries);
                    float num2 = VmlDrawingImportHelper.GetFloatOrVulgarFractionValue(strArray6[0], 0f, 65536f);
                    Color color = VmlDrawingImportHelper.ParseColor(strArray6[1], DXColor.White);
                    OfficeShadeColor item = new OfficeShadeColor(color, (double) num2);
                    this.shapeFillProperties.Colors.Add(item);
                }
            }
            string str4 = this.Importer.ReadAttribute(reader, "relid", "urn:schemas-microsoft-com:office:office");
            if (string.IsNullOrEmpty(str4))
            {
                str4 = this.Importer.ReadAttribute(reader, "id", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            }
            if (!string.IsNullOrEmpty(str4))
            {
                this.shapeFillProperties.EmbeddedImage = this.Importer.LookupImageByRelationId(this.Importer.DocumentModel, str4, this.Importer.DocumentRootFolder);
            }
        }

        private string[] SplitCoordinates(string temp)
        {
            char[] separator = new char[] { ',' };
            string[] strArray = temp.Split(separator);
            if (strArray.Length != 2)
            {
                this.Importer.ThrowInvalidFile();
            }
            return strArray;
        }

        private float[] SplitVector2D(string value, float[] defaultValue)
        {
            float[] numArray = defaultValue;
            char[] separator = new char[] { ',' };
            string[] strArray = value.Split(separator);
            int num = Math.Min(2, strArray.Length);
            for (int i = 0; i < num; i++)
            {
                numArray[i] = VmlDrawingImportHelper.GetFloatOrPercentageValue(strArray[i]);
            }
            return numArray;
        }
    }
}

