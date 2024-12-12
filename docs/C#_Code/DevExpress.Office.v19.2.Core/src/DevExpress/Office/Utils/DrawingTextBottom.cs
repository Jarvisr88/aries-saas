namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingTextBottom : OfficeDrawingIntPropertyBase
    {
        private const int defaultValue = 0xb298;

        public DrawingTextBottom()
        {
            base.Value = 0xb298;
        }

        public DrawingTextBottom(int value)
        {
            base.Value = value;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtProperties properties = owner as IOfficeArtProperties;
            if (properties != null)
            {
                properties.TextBottom = base.Value;
                properties.UseTextBottom = true;
            }
        }

        public override bool Complex =>
            false;
    }
}

