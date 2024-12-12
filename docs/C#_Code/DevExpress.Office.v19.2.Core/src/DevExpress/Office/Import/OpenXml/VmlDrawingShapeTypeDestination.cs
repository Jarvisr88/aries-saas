namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.OpenXml.Export;
    using DevExpress.Utils;
    using System;
    using System.Globalization;
    using System.Xml;

    public class VmlDrawingShapeTypeDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly VmlShapeType shapeType;
        private readonly VmlShapeTypeTable shapeTypeTable;

        public VmlDrawingShapeTypeDestination(DestinationAndXmlBasedImporter importer, VmlShapeTypeTable shapeTypeTable) : base(importer)
        {
            this.shapeTypeTable = shapeTypeTable;
            this.shapeType = new VmlShapeType(importer.DocumentModel);
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("stroke", new ElementHandler<DestinationAndXmlBasedImporter>(VmlDrawingShapeTypeDestination.OnStroke));
            table.Add("fill", new ElementHandler<DestinationAndXmlBasedImporter>(VmlDrawingShapeTypeDestination.OnFill));
            table.Add("shadow", new ElementHandler<DestinationAndXmlBasedImporter>(VmlDrawingShapeTypeDestination.OnShadow));
            table.Add("path", new ElementHandler<DestinationAndXmlBasedImporter>(VmlDrawingShapeTypeDestination.OnPath));
            table.Add("formulas", new ElementHandler<DestinationAndXmlBasedImporter>(VmlDrawingShapeTypeDestination.OnFormulas));
            table.Add("lock", new ElementHandler<DestinationAndXmlBasedImporter>(VmlDrawingShapeTypeDestination.OnShapeProtections));
            return table;
        }

        private static VmlDrawingShapeTypeDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (VmlDrawingShapeTypeDestination) importer.PeekDestination();

        private static Destination OnFill(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            VmlShapeFillProperties shapeFillProperties = new VmlShapeFillProperties(importer.DocumentModel);
            GetThis(importer).shapeType.Fill = shapeFillProperties;
            return new VmlShapeFillPropertiesDestination(importer, shapeFillProperties);
        }

        private static Destination OnFormulas(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            VmlShapeType shapeType = GetThis(importer).ShapeType;
            shapeType.Formulas = new VmlSingleFormulasCollection();
            return new VmlShapeFormulasDestination(importer, shapeType.Formulas);
        }

        private static Destination OnPath(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            VmlShapeType shapeType = GetThis(importer).ShapeType;
            shapeType.ShapePath = new VmlShapePath();
            return new VmlShapePathDestination(importer, shapeType.ShapePath);
        }

        private static Destination OnShadow(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            VmlShadowEffect shadowEffect = new VmlShadowEffect(importer.DocumentModel);
            GetThis(importer).shapeType.ShadowEffect = shadowEffect;
            return new VmlShadowEffectDestination(importer, shadowEffect);
        }

        private static Destination OnShapeProtections(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            VmlShapeType shapeType = GetThis(importer).ShapeType;
            shapeType.ShapeProtections = new VmlShapeProtections();
            return new VmlShapeProtectionsDestination(importer, shapeType.ShapeProtections);
        }

        private static Destination OnStroke(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            VmlShapeType shapeType = GetThis(importer).ShapeType;
            shapeType.Stroke = new VmlLineStrokeSettings(importer.DocumentModel);
            return new VmlLineStrokeSettingsDestination(importer, shapeType.Stroke);
        }

        private void PrepareAdjustValues(string value)
        {
            VmlDrawingImportHelper.PrepareAdjustValues(this.shapeType.AdjustValues, value);
        }

        public override void ProcessElementClose(XmlReader reader)
        {
            string id = this.shapeType.Id;
            if (!string.IsNullOrEmpty(id))
            {
                if (this.shapeTypeTable.ContainsKey(id))
                {
                    this.shapeTypeTable.Remove(id);
                }
                this.shapeTypeTable.Add(id, this.shapeType);
            }
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.shapeType.Id = this.Importer.ReadAttribute(reader, "id");
            this.shapeType.Spt = this.Importer.GetWpSTFloatValue(reader, "spt", NumberStyles.Float, 0f, "urn:schemas-microsoft-com:office:office");
            this.shapeType.Path = this.Importer.ReadAttribute(reader, "path");
            VmlDrawingImportHelper.PrepareCoordSize(this.Importer.ReadAttribute(reader, "coordsize"), this.ShapeType.CoordSize);
            VmlDrawingImportHelper.PrepareCoordOrigin(this.Importer.ReadAttribute(reader, "coordorigin"), this.ShapeType.CoordOrigin);
            this.PrepareAdjustValues(this.Importer.ReadAttribute(reader, "adj"));
            this.shapeType.Filled = new bool?(this.Importer.GetOnOffValue(reader, "filled", true));
            this.shapeType.Stroked = new bool?(this.Importer.GetOnOffValue(reader, "stroked", true));
            this.shapeType.FillColor = VmlDrawingImportHelper.GetVmlSTColorValue(reader, "fillcolor", DXColor.White);
            this.shapeType.StrokeColor = VmlDrawingImportHelper.GetVmlSTColorValue(reader, "strokecolor", DXColor.Black);
            this.shapeType.StrokeWeight = VmlLineStrokeSettingsDestination.ConvertStrokeWeight(this.Importer.ReadAttribute(reader, "strokeweight"), this.Importer.DocumentModel.UnitConverter, this.Importer.DocumentModel.UnitConverter.PointsToModelUnits(1));
            this.shapeType.BlackAndWhiteMode = this.Importer.GetWpEnumValue<VmlBlackAndWhiteMode>(reader, "bwmode", OpenXmlExporterBase.VmlBlackAndWhiteModeTable, VmlBlackAndWhiteMode.Auto, "urn:schemas-microsoft-com:office:office");
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;

        public VmlShapeType ShapeType =>
            this.shapeType;
    }
}

