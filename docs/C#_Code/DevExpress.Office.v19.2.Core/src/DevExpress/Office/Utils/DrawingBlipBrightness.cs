namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingBlipBrightness : OfficeDrawingIntPropertyBase
    {
        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtProperties properties = owner as IOfficeArtProperties;
            if (properties != null)
            {
                properties.PictureBrightness = base.Value;
            }
        }

        public override bool Complex =>
            false;
    }
}

