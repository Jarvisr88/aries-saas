namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;

    public class VmlRectDestination : VmlShapeDestination
    {
        public VmlRectDestination(DestinationAndXmlBasedImporter importer, IOfficeShape shape, VmlShapeTypeTable shapeTypeTable, Destination textBoxContentDestination, Action<IOfficeShape, OfficeVmlShape> processClose) : base(importer, shape, shapeTypeTable, textBoxContentDestination, processClose)
        {
        }

        protected override void SetupAdjustValues()
        {
        }

        protected override void SetupShapePreset()
        {
            base.Shape.ShapeProperties.ShapeType = ShapePreset.Rect;
        }
    }
}

