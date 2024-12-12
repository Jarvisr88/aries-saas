namespace DevExpress.Office.Utils
{
    using System;
    using System.Drawing;

    public class DrawingLineColor : OfficeDrawingColorPropertyBase
    {
        public DrawingLineColor()
        {
        }

        public DrawingLineColor(Color color)
        {
            base.ColorRecord = new OfficeColorRecord(color);
        }

        public DrawingLineColor(int colorIndex)
        {
            base.ColorRecord = new OfficeColorRecord(colorIndex);
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtProperties properties = owner as IOfficeArtProperties;
            if (properties != null)
            {
                properties.LineColor = base.ColorRecord.Color;
            }
        }
    }
}

