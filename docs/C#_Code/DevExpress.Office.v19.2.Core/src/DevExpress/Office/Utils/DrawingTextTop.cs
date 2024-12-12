namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingTextTop : OfficeDrawingIntPropertyBase
    {
        private const int defaultValue = 0xb298;

        public DrawingTextTop()
        {
            base.Value = 0xb298;
        }

        public DrawingTextTop(int value)
        {
            base.Value = value;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtProperties properties = owner as IOfficeArtProperties;
            if (properties != null)
            {
                properties.TextTop = base.Value;
                properties.UseTextTop = true;
            }
        }

        public override bool Complex =>
            false;
    }
}

