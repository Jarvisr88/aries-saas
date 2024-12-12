namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingLineWidth : OfficeDrawingIntPropertyBase
    {
        public DrawingLineWidth()
        {
            base.Value = 0x2535;
        }

        public DrawingLineWidth(int width)
        {
            base.Value = width;
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtProperties properties = owner as IOfficeArtProperties;
            if (properties != null)
            {
                properties.LineWidth = base.Value;
            }
        }

        public override bool Complex =>
            false;
    }
}

