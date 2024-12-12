namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingWrapLeftDistance : OfficeDrawingIntPropertyBase
    {
        public const int DefaultValue = 0x1be7c;

        public DrawingWrapLeftDistance()
        {
            base.Value = 0x1be7c;
        }

        public DrawingWrapLeftDistance(int value)
        {
            base.Value = value;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtProperties properties = owner as IOfficeArtProperties;
            if (properties != null)
            {
                properties.WrapLeftDistance = base.Value;
                properties.UseWrapLeftDistance = true;
            }
        }

        public override bool Complex =>
            false;
    }
}

