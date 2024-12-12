namespace DevExpress.Office.Utils
{
    using System;
    using System.Drawing;

    public class DrawingFillColor : OfficeDrawingColorPropertyBase
    {
        public DrawingFillColor()
        {
        }

        public DrawingFillColor(Color color)
        {
            base.ColorRecord = new OfficeColorRecord(color);
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtProperties properties = owner as IOfficeArtProperties;
            if (properties != null)
            {
                properties.FillColor = base.ColorRecord.Color;
            }
        }
    }
}

