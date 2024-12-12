namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.OpenXml.Export;
    using DevExpress.Office.Utils;
    using System;
    using System.Xml;

    public class VmlLineStrokeSettingsDestination : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private readonly VmlLineStrokeSettings stroke;

        public VmlLineStrokeSettingsDestination(DestinationAndXmlBasedImporter importer, VmlLineStrokeSettings stroke) : base(importer)
        {
            this.stroke = stroke;
        }

        public static int ConvertStrokeWeight(string value, DocumentModelUnitConverter unitConverter, int defaultValue) => 
            string.IsNullOrEmpty(value) ? defaultValue : new VmlUnitConverter(unitConverter).ToModelUnits(new DXVmlUnit(value));

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.stroke.Color = VmlDrawingImportHelper.GetVmlSTColorValue(reader, "color", this.stroke.Color);
            this.stroke.Opacity = VmlDrawingImportHelper.GetWpSTFloatOrVulgarFractionValue(reader, "opacity", this.stroke.Opacity, 65536f);
            this.stroke.Color2 = VmlDrawingImportHelper.GetVmlSTColorValue(reader, "color2", this.stroke.Color2);
            bool? onOffNullValue = this.Importer.GetOnOffNullValue(reader, "on");
            if (onOffNullValue != null)
            {
                this.stroke.Stroked = onOffNullValue;
            }
            this.stroke.StrokeWeight = ConvertStrokeWeight(this.Importer.ReadAttribute(reader, "weight"), this.Importer.DocumentModel.UnitConverter, this.stroke.StrokeWeight);
            this.stroke.JoinStyle = this.Importer.GetWpEnumValue<VmlStrokeJoinStyle>(reader, "joinstyle", OpenXmlExporterBase.VmlStrokeJoinStyleTable, VmlStrokeJoinStyle.Round);
            this.stroke.FillType = this.Importer.GetWpEnumValue<VmlFillType>(reader, "filltype", OpenXmlExporterBase.VmlFillTypeTable, VmlFillType.Solid);
            this.stroke.Title = this.Importer.ReadAttribute(reader, "title", "urn:schemas-microsoft-com:office:office");
            this.stroke.LineStyle = this.Importer.GetWpEnumValue<VmlLineStyle>(reader, "linestyle", OpenXmlExporterBase.VmlLineStyleTable, VmlLineStyle.Single);
            string str = this.Importer.ReadAttribute(reader, "dashstyle");
            if (!string.IsNullOrEmpty(str))
            {
                this.stroke.DashStyle = this.Importer.GetWpEnumValueCore<VmlDashStyle>(str.ToLower(), OpenXmlExporterBase.VmlDashStyleTable, VmlDashStyle.Solid);
            }
            string str2 = this.Importer.ReadAttribute(reader, "relid", "urn:schemas-microsoft-com:office:office");
            if (string.IsNullOrEmpty(str2))
            {
                str2 = this.Importer.ReadAttribute(reader, "id", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            }
            if (!string.IsNullOrEmpty(str2))
            {
                this.stroke.EmbeddedImage = this.Importer.LookupImageByRelationId(this.Importer.DocumentModel, str2, this.Importer.DocumentRootFolder);
            }
            VMLStrokeArrowType? nullable2 = this.Importer.GetWpEnumOnOffNullValue<VMLStrokeArrowType>(reader, "startarrow", OpenXmlExporterBase.VmlStrokeArrowTypeTable);
            if (nullable2 != null)
            {
                this.stroke.StartArrowType = nullable2.Value;
            }
            VMLStrokeArrowLength? nullable3 = this.Importer.GetWpEnumOnOffNullValue<VMLStrokeArrowLength>(reader, "startarrowlength", OpenXmlExporterBase.VmlStrokeArrowLengthTable);
            if (nullable3 != null)
            {
                this.stroke.StartArrowLength = nullable3.Value;
            }
            VMLStrokeArrowWidth? nullable4 = this.Importer.GetWpEnumOnOffNullValue<VMLStrokeArrowWidth>(reader, "startarrowwidth", OpenXmlExporterBase.VmlStrokeArrowWidthTable);
            if (nullable4 != null)
            {
                this.stroke.StartArrowWidth = nullable4.Value;
            }
            VMLStrokeArrowType? nullable5 = this.Importer.GetWpEnumOnOffNullValue<VMLStrokeArrowType>(reader, "endarrow", OpenXmlExporterBase.VmlStrokeArrowTypeTable);
            if (nullable5 != null)
            {
                this.stroke.EndArrowType = nullable5.Value;
            }
            VMLStrokeArrowLength? nullable6 = this.Importer.GetWpEnumOnOffNullValue<VMLStrokeArrowLength>(reader, "endarrowlength", OpenXmlExporterBase.VmlStrokeArrowLengthTable);
            if (nullable6 != null)
            {
                this.stroke.EndArrowLength = nullable6.Value;
            }
            VMLStrokeArrowWidth? nullable7 = this.Importer.GetWpEnumOnOffNullValue<VMLStrokeArrowWidth>(reader, "endarrowwidth", OpenXmlExporterBase.VmlStrokeArrowWidthTable);
            if (nullable7 != null)
            {
                this.stroke.EndArrowWidth = nullable7.Value;
            }
        }
    }
}

