namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingCropFromRight : OfficeDrawingFixedPointPropertyBase
    {
        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtProperties properties = owner as IOfficeArtProperties;
            if (properties != null)
            {
                properties.CropFromRight = base.Value;
            }
        }
    }
}

