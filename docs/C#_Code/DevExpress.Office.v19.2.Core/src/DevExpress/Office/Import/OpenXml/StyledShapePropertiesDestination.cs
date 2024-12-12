namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;

    public class StyledShapePropertiesDestination : ShapePropertiesDestination
    {
        private readonly ShapeStyle shapeStyle;

        public StyledShapePropertiesDestination(DestinationAndXmlBasedImporter importer, ShapeProperties shapeProperties, ShapeStyle shapeStyle) : base(importer, shapeProperties)
        {
            this.shapeStyle = shapeStyle;
        }

        protected override DrawingGradientFill CreateGradientFill() => 
            new ModelShapeGradientFill(this.Importer.ActualDocumentModel, this.shapeStyle);
    }
}

