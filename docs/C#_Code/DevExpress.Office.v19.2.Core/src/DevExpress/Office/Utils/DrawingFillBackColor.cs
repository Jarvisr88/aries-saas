namespace DevExpress.Office.Utils
{
    using System;
    using System.Drawing;

    public class DrawingFillBackColor : OfficeDrawingColorPropertyBase
    {
        public DrawingFillBackColor()
        {
        }

        public DrawingFillBackColor(Color color)
        {
            base.ColorRecord = new OfficeColorRecord(color);
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
        }
    }
}

