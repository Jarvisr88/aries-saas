namespace DevExpress.Office.Utils
{
    using System;
    using System.Drawing;

    public class DrawingFillBackColorExt : OfficeDrawingColorPropertyBase
    {
        public DrawingFillBackColorExt()
        {
            base.ColorRecord = new OfficeColorRecord();
        }

        public DrawingFillBackColorExt(Color color)
        {
            base.ColorRecord = new OfficeColorRecord(color);
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
        }
    }
}

