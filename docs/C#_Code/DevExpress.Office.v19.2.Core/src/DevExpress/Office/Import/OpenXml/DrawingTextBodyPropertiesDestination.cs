namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.DrawingML;
    using DevExpress.Office.OpenXml.Export;
    using System;
    using System.Xml;

    public class DrawingTextBodyPropertiesDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly DrawingTextBodyProperties properties;

        public DrawingTextBodyPropertiesDestination(DestinationAndXmlBasedImporter importer, DrawingTextBodyProperties properties) : base(importer)
        {
            this.properties = properties;
        }

        private int ConvertInsetEmuToModel(int value)
        {
            DrawingValueChecker.CheckCoordinate32(value, "InsetCoordinate");
            return this.Importer.DocumentModel.UnitConverter.EmuToModelUnits(value);
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("flatTx", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingTextBodyPropertiesDestination.OnFlatText));
            table.Add("noAutofit", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingTextBodyPropertiesDestination.OnNoAutoFit));
            table.Add("normAutofit", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingTextBodyPropertiesDestination.OnNormalAutoFit));
            table.Add("prstTxWarp", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingTextBodyPropertiesDestination.OnPresetTextWrap));
            table.Add("scene3d", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingTextBodyPropertiesDestination.OnScene3D));
            table.Add("sp3d", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingTextBodyPropertiesDestination.OnShape3D));
            table.Add("spAutoFit", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingTextBodyPropertiesDestination.OnShapeAutoFit));
            return table;
        }

        private static DrawingTextBodyPropertiesDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (DrawingTextBodyPropertiesDestination) importer.PeekDestination();

        private static Destination OnFlatText(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new DrawingText3DFlatTextDestination(importer, GetThis(importer).properties);

        private static Destination OnNoAutoFit(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            GetThis(importer).properties.AutoFit = DrawingTextAutoFit.None;
            return new EmptyDestination<DestinationAndXmlBasedImporter>(importer);
        }

        private static Destination OnNormalAutoFit(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            DrawingTextNormalAutoFit autoFit = new DrawingTextNormalAutoFit(importer.ActualDocumentModel);
            GetThis(importer).properties.AutoFit = autoFit;
            return new DrawingTextNormalAutoFitDestination(importer, autoFit);
        }

        private static Destination OnPresetTextWrap(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new DrawingPresetTextWarpDestination(importer, GetThis(importer).properties);

        private static Destination OnScene3D(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new Scene3DPropertiesDestination(importer, GetThis(importer).properties.Scene3D);

        private static Destination OnShape3D(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            Shape3DProperties properties = new Shape3DProperties(importer.ActualDocumentModel);
            GetThis(importer).properties.Text3D = properties;
            return new Shape3DPropertiesDestination(importer, properties);
        }

        private static Destination OnShapeAutoFit(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            GetThis(importer).properties.AutoFit = DrawingTextAutoFit.Shape;
            return new EmptyDestination<DestinationAndXmlBasedImporter>(importer);
        }

        public override void ProcessElementClose(XmlReader reader)
        {
            this.properties.EndUpdate();
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.properties.BeginUpdate();
            DrawingTextAnchoringType? nullable = this.Importer.GetWpEnumOnOffNullValue<DrawingTextAnchoringType>(reader, "anchor", OpenXmlExporterBase.AnchoringTypeTable);
            if (nullable != null)
            {
                this.properties.Anchor = nullable.Value;
            }
            bool? onOffNullValue = this.Importer.GetOnOffNullValue(reader, "anchorCtr");
            if (onOffNullValue != null)
            {
                this.properties.AnchorCenter = onOffNullValue.Value;
            }
            int? integerNullableValue = this.Importer.GetIntegerNullableValue(reader, "bIns");
            if (integerNullableValue != null)
            {
                this.properties.Inset.Bottom = this.ConvertInsetEmuToModel(integerNullableValue.Value);
            }
            int? nullable4 = this.Importer.GetIntegerNullableValue(reader, "tIns");
            if (nullable4 != null)
            {
                this.properties.Inset.Top = this.ConvertInsetEmuToModel(nullable4.Value);
            }
            int? nullable5 = this.Importer.GetIntegerNullableValue(reader, "lIns");
            if (nullable5 != null)
            {
                this.properties.Inset.Left = this.ConvertInsetEmuToModel(nullable5.Value);
            }
            int? nullable6 = this.Importer.GetIntegerNullableValue(reader, "rIns");
            if (nullable6 != null)
            {
                this.properties.Inset.Right = this.ConvertInsetEmuToModel(nullable6.Value);
            }
            bool? nullable7 = this.Importer.GetOnOffNullValue(reader, "compatLnSpc");
            if (nullable7 != null)
            {
                this.properties.CompatibleLineSpacing = nullable7.Value;
            }
            bool? nullable8 = this.Importer.GetOnOffNullValue(reader, "forceAA");
            if (nullable8 != null)
            {
                this.properties.ForceAntiAlias = nullable8.Value;
            }
            bool? nullable9 = this.Importer.GetOnOffNullValue(reader, "fromWordArt");
            if (nullable9 != null)
            {
                this.properties.FromWordArt = nullable9.Value;
            }
            DrawingTextHorizontalOverflowType? nullable10 = this.Importer.GetWpEnumOnOffNullValue<DrawingTextHorizontalOverflowType>(reader, "horzOverflow", OpenXmlExporterBase.HorizontalOverflowTypeTable);
            if (nullable10 != null)
            {
                this.properties.HorizontalOverflow = nullable10.Value;
            }
            int? nullable11 = this.Importer.GetIntegerNullableValue(reader, "numCol");
            if (nullable11 != null)
            {
                this.properties.NumberOfColumns = nullable11.Value;
            }
            int? nullable12 = this.Importer.GetIntegerNullableValue(reader, "rot");
            if (nullable12 != null)
            {
                this.properties.Rotation = nullable12.Value;
            }
            bool? nullable13 = this.Importer.GetOnOffNullValue(reader, "rtlCol");
            if (nullable13 != null)
            {
                this.properties.RightToLeftColumns = nullable13.Value;
            }
            int? nullable14 = this.Importer.GetIntegerNullableValue(reader, "spcCol");
            if (nullable14 != null)
            {
                DrawingValueChecker.CheckPositiveCoordinate32(nullable14.Value, "SpaceBetweenColumns");
                this.properties.SpaceBetweenColumns = this.Importer.DocumentModel.UnitConverter.EmuToModelUnits(nullable14.Value);
            }
            bool? nullable15 = this.Importer.GetOnOffNullValue(reader, "spcFirstLastPara");
            if (nullable15 != null)
            {
                this.properties.ParagraphSpacing = nullable15.Value;
            }
            this.properties.UprightText = this.Importer.GetOnOffValue(reader, "upright", DrawingTextBodyInfo.DefaultInfo.UprightText);
            DrawingTextVerticalTextType? nullable16 = this.Importer.GetWpEnumOnOffNullValue<DrawingTextVerticalTextType>(reader, "vert", OpenXmlExporterBase.VerticalTextTypeTable);
            if (nullable16 != null)
            {
                this.properties.VerticalText = nullable16.Value;
            }
            DrawingTextVerticalOverflowType? nullable17 = this.Importer.GetWpEnumOnOffNullValue<DrawingTextVerticalOverflowType>(reader, "vertOverflow", OpenXmlExporterBase.VerticalOverflowTypeTable);
            if (nullable17 != null)
            {
                this.properties.VerticalOverflow = nullable17.Value;
            }
            DrawingTextWrappingType? nullable18 = this.Importer.GetWpEnumOnOffNullValue<DrawingTextWrappingType>(reader, "wrap", OpenXmlExporterBase.TextWrappingTypeTable);
            if (nullable18 != null)
            {
                this.properties.TextWrapping = nullable18.Value;
            }
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

