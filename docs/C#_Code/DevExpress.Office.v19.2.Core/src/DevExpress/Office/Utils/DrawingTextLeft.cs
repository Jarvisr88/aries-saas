namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingTextLeft : OfficeDrawingIntPropertyBase
    {
        private const int defaultValue = 0x16530;

        public DrawingTextLeft()
        {
            base.Value = 0x16530;
        }

        public DrawingTextLeft(int value)
        {
            base.Value = value;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtProperties properties = owner as IOfficeArtProperties;
            if (properties != null)
            {
                properties.TextLeft = base.Value;
                properties.UseTextLeft = true;
            }
        }

        public override bool Complex =>
            false;
    }
}

