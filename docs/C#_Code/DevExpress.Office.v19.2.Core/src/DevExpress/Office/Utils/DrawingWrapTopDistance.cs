namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingWrapTopDistance : OfficeDrawingIntPropertyBase
    {
        public const int DefaultValue = 0;

        public DrawingWrapTopDistance()
        {
            base.Value = 0;
        }

        public DrawingWrapTopDistance(int value)
        {
            base.Value = value;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtProperties properties = owner as IOfficeArtProperties;
            if (properties != null)
            {
                properties.UseWrapTopDistance = true;
                properties.WrapTopDistance = base.Value;
            }
        }

        public override bool Complex =>
            false;
    }
}

