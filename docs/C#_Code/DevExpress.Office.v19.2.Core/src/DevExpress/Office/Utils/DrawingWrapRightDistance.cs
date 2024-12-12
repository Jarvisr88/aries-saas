namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingWrapRightDistance : OfficeDrawingIntPropertyBase
    {
        public const int DefaultValue = 0x1be7c;

        public DrawingWrapRightDistance()
        {
            base.Value = 0x1be7c;
        }

        public DrawingWrapRightDistance(int value)
        {
            base.Value = value;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtProperties properties = owner as IOfficeArtProperties;
            if (properties != null)
            {
                properties.WrapRightDistance = base.Value;
                properties.UseWrapRightDistance = true;
            }
        }

        public override bool Complex =>
            false;
    }
}

