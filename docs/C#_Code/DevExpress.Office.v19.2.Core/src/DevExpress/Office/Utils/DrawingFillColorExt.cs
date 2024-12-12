namespace DevExpress.Office.Utils
{
    using System;
    using System.Drawing;

    public class DrawingFillColorExt : OfficeDrawingColorPropertyBase
    {
        public DrawingFillColorExt()
        {
            base.ColorRecord = new OfficeColorRecord();
        }

        public DrawingFillColorExt(Color color)
        {
            base.ColorRecord = new OfficeColorRecord(color);
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
        }
    }
}

