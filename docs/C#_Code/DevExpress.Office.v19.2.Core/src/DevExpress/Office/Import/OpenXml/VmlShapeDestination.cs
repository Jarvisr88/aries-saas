namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.Import.Binary;
    using DevExpress.Office.OpenXml.Export;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class VmlShapeDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly IOfficeShape shape;
        private readonly VmlShapeTypeTable shapeTypeTable;
        private readonly Destination textBoxContentDestination;
        private readonly Action<IOfficeShape, OfficeVmlShape> processClose;

        public VmlShapeDestination(DestinationAndXmlBasedImporter importer, IOfficeShape shape, VmlShapeTypeTable shapeTypeTable, Destination textBoxContentDestination, Action<IOfficeShape, OfficeVmlShape> processClose) : base(importer)
        {
            this.shape = shape;
            this.shapeTypeTable = shapeTypeTable;
            this.textBoxContentDestination = textBoxContentDestination;
            this.VmlShape = new OfficeVmlShape(shape.DocumentModel);
            this.processClose = processClose;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("fill", new ElementHandler<DestinationAndXmlBasedImporter>(VmlShapeDestination.OnFill));
            table.Add("shadow", new ElementHandler<DestinationAndXmlBasedImporter>(VmlShapeDestination.OnShadow));
            table.Add("stroke", new ElementHandler<DestinationAndXmlBasedImporter>(VmlShapeDestination.OnStroke));
            table.Add("path", new ElementHandler<DestinationAndXmlBasedImporter>(VmlShapeDestination.OnPath));
            table.Add("textbox", new ElementHandler<DestinationAndXmlBasedImporter>(VmlShapeDestination.OnTextBox));
            table.Add("lock", new ElementHandler<DestinationAndXmlBasedImporter>(VmlShapeDestination.OnShapeProtections));
            table.Add("formulas", new ElementHandler<DestinationAndXmlBasedImporter>(VmlShapeDestination.OnFormulas));
            table.Add("imagedata", new ElementHandler<DestinationAndXmlBasedImporter>(VmlShapeDestination.OnImageData));
            return table;
        }

        private VmlFormulaNamedValues CreateNamedValues()
        {
            DocumentModelUnitConverter unitConverter = this.shape.DocumentModel.UnitConverter;
            VmlFormulaNamedValues values = new VmlFormulaNamedValues {
                Width = this.VmlShape.CoordSize.X,
                Height = this.VmlShape.CoordSize.Y,
                CenterX = (this.VmlShape.CoordSize.X + this.VmlShape.CoordOrigin.X) / 2,
                CenterY = (this.VmlShape.CoordSize.Y + this.VmlShape.CoordOrigin.Y) / 2,
                PixelLineWidth = unitConverter.ModelUnitsToPixels(this.VmlShape.StrokeWeight),
                EmuWidth = unitConverter.ModelUnitsToEmuF(this.ShapeWidth),
                EmuHeight = unitConverter.ModelUnitsToEmuF(this.ShapeHeight)
            };
            values.EmuWidth2 = values.EmuWidth / 2;
            values.EmuHeight2 = values.EmuHeight / 2;
            values.LineDrawn = this.VmlShape.GetStroked();
            values.PixelWidth = (int) Math.Round((double) unitConverter.ModelUnitsToPixelsF(this.ShapeWidth));
            values.PixelHeight = (int) Math.Round((double) unitConverter.ModelUnitsToPixelsF(this.ShapeHeight));
            if (this.VmlShape.ShapePath != null)
            {
                values.LimoX = this.VmlShape.ShapePath.LimoX;
                values.LimoY = this.VmlShape.ShapePath.LimoY;
            }
            Array.Copy(this.VmlShape.AdjustValues, values.AdjustValues, 8);
            return values;
        }

        private static VmlShapeDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (VmlShapeDestination) importer.PeekDestination();

        private void LookUpVmlShapeType(string value)
        {
            VmlShapeType type;
            if (!this.shapeTypeTable.TryGetValue(value, out type))
            {
                type = this.TryGetPresetShapeType(value);
            }
            if (type != null)
            {
                this.VmlShape.CopyFrom(type);
            }
        }

        private static Destination OnFill(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            OfficeVmlShape vmlShape = GetThis(importer).VmlShape;
            vmlShape.Fill ??= new VmlShapeFillProperties(importer.DocumentModel);
            return new VmlShapeFillPropertiesDestination(importer, vmlShape.Fill);
        }

        private static Destination OnFormulas(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            OfficeVmlShape vmlShape = GetThis(importer).VmlShape;
            vmlShape.Formulas = new VmlSingleFormulasCollection();
            return new VmlShapeFormulasDestination(importer, vmlShape.Formulas);
        }

        private static Destination OnImageData(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            OfficeVmlShape vmlShape = GetThis(importer).VmlShape;
            vmlShape.ImageData = new VmlShapeImageData(importer.DocumentModel);
            return new VmlShapeImageDataDestination(importer, vmlShape.ImageData);
        }

        private static Destination OnPath(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            OfficeVmlShape vmlShape = GetThis(importer).VmlShape;
            vmlShape.ShapePath ??= new VmlShapePath();
            return new VmlShapePathDestination(importer, vmlShape.ShapePath);
        }

        private static Destination OnShadow(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            OfficeVmlShape vmlShape = GetThis(importer).VmlShape;
            vmlShape.ShadowEffect ??= new VmlShadowEffect(importer.DocumentModel);
            return new VmlShadowEffectDestination(importer, vmlShape.ShadowEffect);
        }

        private static Destination OnShapeProtections(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            OfficeVmlShape vmlShape = GetThis(importer).VmlShape;
            vmlShape.ShapeProtections ??= new VmlShapeProtections();
            return new VmlShapeProtectionsDestination(importer, vmlShape.ShapeProtections);
        }

        private static Destination OnStroke(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            OfficeVmlShape vmlShape = GetThis(importer).VmlShape;
            vmlShape.Stroke ??= new VmlLineStrokeSettings(importer.DocumentModel);
            return new VmlLineStrokeSettingsDestination(importer, vmlShape.Stroke);
        }

        private static Destination OnTextBox(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new VmlTextBoxDestination(importer, GetThis(importer).textBoxContentDestination);

        private void PrepareAdjustValues(string value)
        {
            VmlDrawingImportHelper.PrepareAdjustValues(this.VmlShape.AdjustValues, value);
        }

        public override void ProcessElementClose(XmlReader reader)
        {
            this.SetupShapeProperies();
            this.processClose(this.Shape, this.VmlShape);
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.VmlShape.Id = this.Importer.ReadAttribute(reader, "id");
            this.VmlShape.SpId = this.Importer.ReadAttribute(reader, "spid", "urn:schemas-microsoft-com:office:office");
            string str = this.Importer.ReadAttribute(reader, "type");
            if (!string.IsNullOrEmpty(str))
            {
                char[] trimChars = new char[] { '#' };
                str = str.TrimStart(trimChars);
            }
            if (!string.IsNullOrEmpty(str))
            {
                this.LookUpVmlShapeType(str);
            }
            string str2 = this.Importer.ReadAttribute(reader, "path");
            if (!string.IsNullOrEmpty(str2))
            {
                this.VmlShape.Path = str2;
            }
            this.PrepareAdjustValues(this.Importer.ReadAttribute(reader, "adj"));
            VmlDrawingImportHelper.PrepareCoordSize(this.Importer.ReadAttribute(reader, "coordsize"), this.VmlShape.CoordSize);
            VmlDrawingImportHelper.PrepareCoordOrigin(this.Importer.ReadAttribute(reader, "coordorigin"), this.VmlShape.CoordOrigin);
            VmlDrawingImportHelper.PrepareShapeStyleProperties(this.VmlShape.ShapeStyleProperties, this.Importer.ReadAttribute(reader, "style"));
            bool? onOffNullValue = this.Importer.GetOnOffNullValue(reader, "filled");
            if (onOffNullValue != null)
            {
                this.VmlShape.Filled = onOffNullValue;
            }
            this.VmlShape.FillColor = VmlDrawingImportHelper.GetVmlSTColorValue(reader, "fillcolor", this.VmlShape.FillColor);
            this.VmlShape.InsetMode = this.Importer.GetWpEnumValue<VmlInsetMode>(reader, "insetmode", OpenXmlExporterBase.VmlInsetModeTable, VmlInsetMode.Custom, "urn:schemas-microsoft-com:office:office");
            this.VmlShape.AltText = this.Importer.ReadAttribute(reader, "alt");
            this.VmlShape.StrokeColor = VmlDrawingImportHelper.GetVmlSTColorValue(reader, "strokecolor", this.VmlShape.StrokeColor);
            this.VmlShape.StrokeWeight = VmlLineStrokeSettingsDestination.ConvertStrokeWeight(this.Importer.ReadAttribute(reader, "strokeweight"), this.Importer.DocumentModel.UnitConverter, this.VmlShape.StrokeWeight);
            onOffNullValue = this.Importer.GetOnOffNullValue(reader, "stroked");
            if (onOffNullValue != null)
            {
                this.VmlShape.Stroked = onOffNullValue;
            }
            this.VmlShape.BlackAndWhiteMode = this.Importer.GetWpEnumValue<VmlBlackAndWhiteMode>(reader, "bwmode", OpenXmlExporterBase.VmlBlackAndWhiteModeTable, this.VmlShape.BlackAndWhiteMode, "urn:schemas-microsoft-com:office:office");
        }

        protected virtual void SetupAdjustValues()
        {
            DocumentModelUnitConverter unitConverter = this.Shape.DocumentModel.UnitConverter;
            int?[] adjustValues = this.VmlShape.AdjustValues;
            AdjustValuesConverterFromBinaryFormat.Convert(unitConverter.CeilingModelUnitsToEmu(this.ShapeWidth), unitConverter.CeilingModelUnitsToEmu(this.ShapeHeight), this.Shape.ShapeProperties.ShapeType, adjustValues);
            this.Shape.ShapeProperties.SetupShapeAdjustList(adjustValues);
        }

        private void SetupBlackAndWhiteMode()
        {
            VmlDrawingImportHelper.SetupBlackAndWhiteMode(this.shape.ShapeProperties, this.VmlShape.BlackAndWhiteMode);
        }

        private void SetupCustomGeometry()
        {
            string path = this.VmlShape.GetPath();
            if (!string.IsNullOrEmpty(path))
            {
                VmlDrawingImportHelper.SetupCustomGeometry(this.shape, path, this.VmlShape.Formulas, this.CreateNamedValues());
            }
        }

        private void SetupFillProperties()
        {
            if ((this.VmlShape.ImageData != null) && (this.VmlShape.ImageData.Image != null))
            {
                VmlDrawingImportHelper.SetupImageData(this.shape.ShapeProperties, this.VmlShape.ImageData);
            }
            else if (this.VmlShape.GetFilled())
            {
                VmlDrawingImportHelper.SetupDrawingFill(this.Shape.ShapeProperties, this.VmlShape.Fill, this.VmlShape.FillColor);
            }
            else
            {
                this.Shape.ShapeProperties.Fill = DrawingFill.None;
            }
        }

        private void SetupOutlineProperties()
        {
            if (this.VmlShape.GetStroked())
            {
                VmlDrawingImportHelper.SetupOutlineProperties(this.Shape.ShapeProperties, this.VmlShape.Stroke, this.VmlShape.StrokeColor, this.VmlShape.StrokeWeight);
            }
            else
            {
                this.Shape.ShapeProperties.OutlineType = OutlineType.None;
            }
        }

        private void SetupShadowProperties()
        {
            if (this.VmlShape.GetShadowed())
            {
                VmlDrawingImportHelper.SetupShadowProperties(this.shape.ShapeProperties, this.VmlShape.ShadowEffect);
            }
        }

        private void SetupShapeGeometry()
        {
            if (this.Shape.ShapeProperties.ShapeType != ShapePreset.None)
            {
                this.SetupAdjustValues();
            }
            else
            {
                this.SetupCustomGeometry();
            }
        }

        protected virtual void SetupShapePreset()
        {
            this.Shape.ShapeProperties.ShapeType = BinaryDrawingImportHelper.GetShapeType((int) this.VmlShape.Spt, ShapePreset.None);
        }

        private void SetupShapeProperies()
        {
            this.SetupShapePreset();
            this.SetupSize();
            this.SetupTransform();
            this.SetupShapeGeometry();
            this.SetupFillProperties();
            this.SetupOutlineProperties();
            this.SetupShadowProperties();
            this.SetupBlackAndWhiteMode();
            this.SetupShapeProtections();
        }

        private void SetupShapeProtections()
        {
            if (this.VmlShape.ShapeProtections != null)
            {
                VmlDrawingImportHelper.SetupShapeProtections(this.shape.Locks, this.VmlShape.ShapeProtections);
            }
        }

        private void SetupSize()
        {
            DocumentModelUnitConverter unitConverter = this.Importer.DocumentModel.UnitConverter;
            this.ShapeWidth = VmlDrawingImportHelper.GetModelUnitValue(this.VmlShape.ShapeStyleProperties.GetProperty("width"), unitConverter);
            this.ShapeHeight = VmlDrawingImportHelper.GetModelUnitValue(this.VmlShape.ShapeStyleProperties.GetProperty("height"), unitConverter);
        }

        private void SetupTransform()
        {
            VmlDrawingImportHelper.SetupShapeTransform(this.Shape.ShapeProperties.Transform2D, this.VmlShape.ShapeStyleProperties);
        }

        private VmlShapeType TryGetPresetShapeType(string shapeType) => 
            VmlShapeTypePresets.GetVmlShapeType(shapeType);

        protected IOfficeShape Shape =>
            this.shape;

        protected VmlShapeTypeTable ShapeTypeTable =>
            this.shapeTypeTable;

        protected OfficeVmlShape VmlShape { get; private set; }

        protected float ShapeWidth { get; private set; }

        protected float ShapeHeight { get; private set; }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

