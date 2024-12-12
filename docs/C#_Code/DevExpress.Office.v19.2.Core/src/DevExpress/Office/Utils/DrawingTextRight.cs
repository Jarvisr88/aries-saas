namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingTextRight : OfficeDrawingIntPropertyBase
    {
        private const int defaultValue = 0x16530;

        public DrawingTextRight()
        {
            base.Value = 0x16530;
        }

        public DrawingTextRight(int value)
        {
            base.Value = value;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtProperties properties = owner as IOfficeArtProperties;
            if (properties != null)
            {
                properties.TextRight = base.Value;
                properties.UseTextRight = true;
            }
        }

        public override bool Complex =>
            false;
    }
}

