namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.OpenXml.Export;
    using System;
    using System.Xml;

    public class OutlineDestination : DrawingFillPartDestinationBase
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly Outline outline;

        public OutlineDestination(DestinationAndXmlBasedImporter importer, Outline outline) : base(importer)
        {
            this.outline = outline;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("bevel", new ElementHandler<DestinationAndXmlBasedImporter>(OutlineDestination.OnLineJoinBevel));
            table.Add("headEnd", new ElementHandler<DestinationAndXmlBasedImporter>(OutlineDestination.OnHeadEndStyle));
            table.Add("miter", new ElementHandler<DestinationAndXmlBasedImporter>(OutlineDestination.OnMiterLineJoin));
            table.Add("prstDash", new ElementHandler<DestinationAndXmlBasedImporter>(OutlineDestination.OnPresetDash));
            table.Add("round", new ElementHandler<DestinationAndXmlBasedImporter>(OutlineDestination.OnRoundLineJoin));
            table.Add("tailEnd", new ElementHandler<DestinationAndXmlBasedImporter>(OutlineDestination.OnTailEndStyle));
            AddFillPartHandlers(table);
            return table;
        }

        private static OutlineDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (OutlineDestination) importer.PeekDestination();

        private static Destination OnHeadEndStyle(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OutlineHeadEndStyleDestination(importer, GetThis(importer).outline);

        private static Destination OnLineJoinBevel(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OutlineJoinBevelDestination(importer, GetThis(importer).outline);

        private static Destination OnMiterLineJoin(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OutlineMiterLineJoinDestination(importer, GetThis(importer).outline);

        private static Destination OnPresetDash(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OutlinePresetDashDestination(importer, GetThis(importer).outline);

        private static Destination OnRoundLineJoin(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OutlineRoundLineJoinDestination(importer, GetThis(importer).outline);

        private static Destination OnTailEndStyle(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OutlineTailEndStyleDestination(importer, GetThis(importer).outline);

        public override void ProcessElementClose(XmlReader reader)
        {
            if (base.Fill != null)
            {
                this.outline.Fill = base.Fill;
            }
            this.outline.EndUpdate();
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.outline.BeginUpdate();
            OutlineEndCapStyle? nullable = this.Importer.GetWpEnumOnOffNullValue<OutlineEndCapStyle>(reader, "cap", OpenXmlExporterBase.EndCapStyleTable);
            if (nullable != null)
            {
                this.outline.EndCapStyle = nullable.Value;
            }
            OutlineCompoundType? nullable2 = this.Importer.GetWpEnumOnOffNullValue<OutlineCompoundType>(reader, "cmpd", OpenXmlExporterBase.CompoundTypeTable);
            if (nullable2 != null)
            {
                this.outline.CompoundType = nullable2.Value;
            }
            OutlineStrokeAlignment? nullable3 = this.Importer.GetWpEnumOnOffNullValue<OutlineStrokeAlignment>(reader, "algn", OpenXmlExporterBase.StrokeAlignmentTable);
            if (nullable3 != null)
            {
                this.outline.StrokeAlignment = nullable3.Value;
            }
            int? integerNullableValue = this.Importer.GetIntegerNullableValue(reader, "w");
            if (integerNullableValue != null)
            {
                int num = integerNullableValue.Value;
                DrawingValueChecker.CheckOutlineWidth(num, "outlineWidth");
                this.outline.Width = this.Importer.DocumentModel.UnitConverter.EmuToModelUnits(num);
            }
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

