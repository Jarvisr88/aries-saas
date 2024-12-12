namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingWrapBottomDistance : OfficeDrawingIntPropertyBase
    {
        public const int DefaultValue = 0;

        public DrawingWrapBottomDistance()
        {
            base.Value = 0;
        }

        public DrawingWrapBottomDistance(int value)
        {
            base.Value = value;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtProperties properties = owner as IOfficeArtProperties;
            if (properties != null)
            {
                properties.WrapBottomDistance = base.Value;
                properties.UseWrapBottomDistance = true;
            }
        }

        public override bool Complex =>
            false;
    }
}

