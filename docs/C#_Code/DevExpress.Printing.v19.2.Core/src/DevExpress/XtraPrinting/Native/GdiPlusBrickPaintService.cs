namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public class GdiPlusBrickPaintService : BrickPaintServiceBase
    {
        public GdiPlusBrickPaintService(EditingFieldCollection editingFields, IServiceProvider provider, float dpi) : base(editingFields, provider, dpi)
        {
        }

        protected override System.Drawing.Image CreateImage(bool isAscending)
        {
            int width = GraphicsUnitConverter.Convert(11, 96f, base.Dpi);
            Bitmap image = new Bitmap(width, width);
            image.SetResolution(base.Dpi, base.Dpi);
            Color color = Color.FromArgb(0x59, Color.Black);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                using (SolidBrush brush = new SolidBrush(color))
                {
                    graphics.Clear(Color.Transparent);
                    float sx = base.Dpi / 96f;
                    graphics.ScaleTransform(sx, sx);
                    graphics.FillPolygon(brush, GetArrowPath(isAscending));
                    graphics.Restore(graphics.Save());
                }
            }
            return image;
        }

        private static Point[] GetArrowPath(bool isAscending)
        {
            Point[] pointArray3;
            if (!isAscending)
            {
                pointArray3 = new Point[] { new Point(10, 3), new Point(5, 8), new Point(0, 3) };
            }
            else
            {
                pointArray3 = new Point[] { new Point(0, 7), new Point(5, 2), new Point(10, 7) };
            }
            return pointArray3;
        }
    }
}

