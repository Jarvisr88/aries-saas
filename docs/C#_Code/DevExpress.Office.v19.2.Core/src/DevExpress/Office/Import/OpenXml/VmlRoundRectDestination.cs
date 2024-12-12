namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class VmlRoundRectDestination : VmlShapeDestination
    {
        private float arcSize;

        public VmlRoundRectDestination(DestinationAndXmlBasedImporter importer, IOfficeShape shape, VmlShapeTypeTable shapeTypeTable, Destination textBoxContentDestination, Action<IOfficeShape, OfficeVmlShape> processClose) : base(importer, shape, shapeTypeTable, textBoxContentDestination, processClose)
        {
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            base.ProcessElementOpen(reader);
            this.arcSize = VmlDrawingImportHelper.GetWpSTFloatOrVulgarFractionValue(reader, "arcsize", 0.2f, 65536f);
        }

        protected override void SetupAdjustValues()
        {
            int?[] values = new int?[] { new int?((int) Math.Round((double) (this.arcSize * 100000f))) };
            base.Shape.ShapeProperties.SetupShapeAdjustList(values);
        }

        protected override void SetupShapePreset()
        {
            base.Shape.ShapeProperties.ShapeType = ShapePreset.RoundRect;
        }
    }
}

