﻿namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingCropFromTop : OfficeDrawingFixedPointPropertyBase
    {
        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtProperties properties = owner as IOfficeArtProperties;
            if (properties != null)
            {
                properties.CropFromTop = base.Value;
            }
        }
    }
}
