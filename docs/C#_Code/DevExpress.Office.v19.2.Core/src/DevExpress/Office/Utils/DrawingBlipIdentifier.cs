namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingBlipIdentifier : OfficeDrawingIntPropertyBase
    {
        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtProperties properties = owner as IOfficeArtProperties;
            if (properties != null)
            {
                properties.BlipIndex = base.Value;
            }
        }

        public override bool Complex =>
            false;
    }
}

