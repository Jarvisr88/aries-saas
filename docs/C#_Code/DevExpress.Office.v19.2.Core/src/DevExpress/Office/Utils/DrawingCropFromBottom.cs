namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingCropFromBottom : OfficeDrawingFixedPointPropertyBase
    {
        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtProperties properties = owner as IOfficeArtProperties;
            if (properties != null)
            {
                properties.CropFromBottom = base.Value;
            }
        }
    }
}

