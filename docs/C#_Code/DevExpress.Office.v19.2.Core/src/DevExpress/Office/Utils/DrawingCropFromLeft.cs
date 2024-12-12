namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingCropFromLeft : OfficeDrawingFixedPointPropertyBase
    {
        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtProperties properties = owner as IOfficeArtProperties;
            if (properties != null)
            {
                properties.CropFromLeft = base.Value;
            }
        }
    }
}

