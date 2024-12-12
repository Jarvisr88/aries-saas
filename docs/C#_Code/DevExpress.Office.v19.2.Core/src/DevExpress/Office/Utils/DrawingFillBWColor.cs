namespace DevExpress.Office.Utils
{
    using System;
    using System.Drawing;

    public class DrawingFillBWColor : OfficeDrawingColorPropertyBase
    {
        public DrawingFillBWColor()
        {
        }

        public DrawingFillBWColor(Color color)
        {
            base.ColorRecord = new OfficeColorRecord(color);
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
        }
    }
}

