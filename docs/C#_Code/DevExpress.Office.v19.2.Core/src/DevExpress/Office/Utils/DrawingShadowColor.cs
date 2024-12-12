namespace DevExpress.Office.Utils
{
    using System;
    using System.Drawing;

    public class DrawingShadowColor : OfficeDrawingColorPropertyBase
    {
        public DrawingShadowColor()
        {
        }

        public DrawingShadowColor(Color color)
        {
            base.ColorRecord = new OfficeColorRecord(color);
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
        }
    }
}

