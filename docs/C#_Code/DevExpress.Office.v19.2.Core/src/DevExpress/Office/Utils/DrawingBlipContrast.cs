namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingBlipContrast : OfficeDrawingIntPropertyBase
    {
        public const int DefaultValue = 0x10000;

        public DrawingBlipContrast()
        {
            base.Value = 0x10000;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtProperties properties = owner as IOfficeArtProperties;
            if (properties != null)
            {
                properties.PictureContrast = base.Value;
            }
        }

        public override bool Complex =>
            false;
    }
}

