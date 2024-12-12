namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.OpenXml.Export;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public abstract class VmlDrawingDestinationBase : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private VmlShapeProtections shapeProtections;
        protected IOfficeShape CanvasShape;
        protected VmlShapeTypeTable ShapeTypeTable;

        protected VmlDrawingDestinationBase(DestinationAndXmlBasedImporter importer, VmlShapeTypeTable shapeTypeTable) : base(importer)
        {
            Guard.ArgumentNotNull(shapeTypeTable, "shapeTypeTable");
            this.ShapeTypeTable = shapeTypeTable;
        }

        protected abstract void CreateAndProcessGroupInstance(VmlGroupEditAs editAs);
        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("shapetype", new ElementHandler<DestinationAndXmlBasedImporter>(VmlDrawingDestinationBase.OnShapeType));
            table.Add("lock", new ElementHandler<DestinationAndXmlBasedImporter>(VmlDrawingDestinationBase.OnShapeProtections));
            table.Add("shape", new ElementHandler<DestinationAndXmlBasedImporter>(VmlDrawingDestinationBase.OnShape));
            table.Add("rect", new ElementHandler<DestinationAndXmlBasedImporter>(VmlDrawingDestinationBase.OnRect));
            table.Add("roundrect", new ElementHandler<DestinationAndXmlBasedImporter>(VmlDrawingDestinationBase.OnRoundRect));
            table.Add("oval", new ElementHandler<DestinationAndXmlBasedImporter>(VmlDrawingDestinationBase.OnOval));
            table.Add("group", new ElementHandler<DestinationAndXmlBasedImporter>(VmlDrawingDestinationBase.OnGroup));
            return table;
        }

        protected abstract IOfficeShape CreateShapeInstance();
        protected abstract Destination CreateTextBoxContentDestination();
        protected abstract VmlDrawingDestinationBase CreateVmlGroupDestination();
        private static VmlDrawingDestinationBase GetThis(DestinationAndXmlBasedImporter importer) => 
            (VmlDrawingDestinationBase) importer.PeekDestination();

        private static Destination OnGroup(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            GetThis(importer).CreateVmlGroupDestination();

        protected abstract void OnGroupHandled();
        private static Destination OnOval(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            IOfficeShape shape = GetThis(importer).PrepareShape();
            VmlDrawingDestinationBase @this = GetThis(importer);
            return new VmlOvalDestination(importer, shape, GetThis(importer).ShapeTypeTable, GetThis(importer).CreateTextBoxContentDestination(), new Action<IOfficeShape, OfficeVmlShape>(@this.ProcessShapeClose));
        }

        private static Destination OnRect(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            IOfficeShape shape = GetThis(importer).PrepareShape();
            VmlDrawingDestinationBase @this = GetThis(importer);
            return new VmlRectDestination(importer, shape, GetThis(importer).ShapeTypeTable, GetThis(importer).CreateTextBoxContentDestination(), new Action<IOfficeShape, OfficeVmlShape>(@this.ProcessShapeClose));
        }

        private static Destination OnRoundRect(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            IOfficeShape shape = GetThis(importer).PrepareShape();
            VmlDrawingDestinationBase @this = GetThis(importer);
            return new VmlRoundRectDestination(importer, shape, GetThis(importer).ShapeTypeTable, GetThis(importer).CreateTextBoxContentDestination(), new Action<IOfficeShape, OfficeVmlShape>(@this.ProcessShapeClose));
        }

        private static Destination OnShape(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            IOfficeShape shape = GetThis(importer).PrepareShape();
            VmlDrawingDestinationBase @this = GetThis(importer);
            return new VmlShapeDestination(importer, shape, GetThis(importer).ShapeTypeTable, GetThis(importer).CreateTextBoxContentDestination(), new Action<IOfficeShape, OfficeVmlShape>(@this.ProcessShapeClose));
        }

        private static Destination OnShapeProtections(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            VmlShapeProtections shapeProtections = new VmlShapeProtections();
            GetThis(importer).shapeProtections = shapeProtections;
            return new VmlShapeProtectionsDestination(importer, shapeProtections);
        }

        private static Destination OnShapeType(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new VmlDrawingShapeTypeDestination(importer, GetThis(importer).ShapeTypeTable);

        private IOfficeShape PrepareShape()
        {
            IOfficeShape shape = this.CreateShapeInstance();
            if ((this.EditAs == VmlGroupEditAs.Canvas) && (this.CanvasShape == null))
            {
                this.CanvasShape = shape;
            }
            else
            {
                this.ProcessShapeOpen(shape);
            }
            return shape;
        }

        public override void ProcessElementClose(XmlReader reader)
        {
            if (reader.LocalName == "group")
            {
                this.SetupGroupShapeLocks();
                this.OnGroupHandled();
            }
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            if (reader.LocalName == "group")
            {
                this.EditAs = this.Importer.GetWpEnumValue<VmlGroupEditAs>(reader, "editas", OpenXmlExporterBase.VmlGroupEditAsTable, VmlGroupEditAs.None);
                this.CreateAndProcessGroupInstance(this.EditAs);
                this.Id = this.Importer.ReadAttribute(reader, "id");
                VmlCoordUnit coord = new VmlCoordUnit(0x5460, 0x5460);
                VmlDrawingImportHelper.PrepareCoordSize(this.Importer.ReadAttribute(reader, "coordsize"), coord);
                VmlCoordUnit unit2 = new VmlCoordUnit();
                VmlDrawingImportHelper.PrepareCoordOrigin(this.Importer.ReadAttribute(reader, "coordorigin"), unit2);
                this.StyleProperties = new VmlShapeStyleProperties();
                VmlDrawingImportHelper.PrepareShapeStyleProperties(this.StyleProperties, this.Importer.ReadAttribute(reader, "style"));
                this.GroupShapeProperties = new DevExpress.Office.Drawing.GroupShapeProperties(this.Importer.DocumentModel);
                DocumentModelUnitConverter unitConverter = this.Importer.DocumentModel.UnitConverter;
                this.GroupShapeProperties.ChildTransform2D.SetCxCore(unitConverter.EmuToModelUnitsF(coord.X));
                this.GroupShapeProperties.ChildTransform2D.SetCyCore(unitConverter.EmuToModelUnitsF(coord.Y));
                this.GroupShapeProperties.ChildTransform2D.SetOffsetXCore(unitConverter.EmuToModelUnitsF(unit2.X));
                this.GroupShapeProperties.ChildTransform2D.SetOffsetYCore(unitConverter.EmuToModelUnitsF(unit2.Y));
                VmlDrawingImportHelper.SetupShapeTransform(this.GroupShapeProperties.Transform2D, this.StyleProperties);
            }
        }

        protected abstract void ProcessShapeClose(IOfficeShape shape, OfficeVmlShape vmlShape);
        protected abstract void ProcessShapeOpen(IOfficeShape shape);
        private void SetupGroupShapeLocks()
        {
            if (this.shapeProtections != null)
            {
                this.GroupShapeLocks = new DevExpress.Office.Drawing.GroupShapeLocks(new CommonDrawingLocks(this.Importer.DocumentModel.MainPart));
                VmlDrawingImportHelper.SetupGroupShapeProtections(this.GroupShapeLocks, this.shapeProtections);
            }
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;

        protected string Id { get; private set; }

        protected VmlGroupEditAs EditAs { get; private set; }

        protected VmlShapeStyleProperties StyleProperties { get; private set; }

        public DevExpress.Office.Drawing.GroupShapeProperties GroupShapeProperties { get; private set; }

        public DevExpress.Office.Drawing.GroupShapeLocks GroupShapeLocks { get; private set; }
    }
}

