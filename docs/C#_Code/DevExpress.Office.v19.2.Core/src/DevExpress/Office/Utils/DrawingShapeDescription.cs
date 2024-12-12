namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingShapeDescription : OfficeDrawingTerminatedStringPropertyValueBase
    {
        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeShapeProperties properties = owner as IOfficeShapeProperties;
            if (properties != null)
            {
                properties.ShapeDescription = base.TrimmedData;
            }
        }
    }
}

