namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingShapeName : OfficeDrawingTerminatedStringPropertyValueBase
    {
        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeShapeProperties properties = owner as IOfficeShapeProperties;
            if (properties != null)
            {
                properties.ShapeName = base.TrimmedData;
            }
        }
    }
}

