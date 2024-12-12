namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Layout;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class SoftEdgesOpacityMaskCreator
    {
        private readonly ShapeLayoutInfo shapeLayoutInfo;

        public SoftEdgesOpacityMaskCreator(ShapeLayoutInfo shapeLayoutInfo)
        {
            this.shapeLayoutInfo = shapeLayoutInfo;
        }

        public Bitmap Create(Bitmap figureBitmap, Rectangle bounds, float radius, DocumentLayoutUnitConverter converter)
        {
            Bitmap image = null;
            using (Region region = this.GetSoftEdgeClipRegion(figureBitmap, bounds, radius, converter))
            {
                if (region != null)
                {
                    image = new Bitmap(converter.LayoutUnitsToPixels(bounds.Width), converter.LayoutUnitsToPixels(bounds.Height));
                    using (Graphics graphics = GdipExtensions.PrepareGraphicsFromImage(image, GraphicsUnit.Pixel, 1f))
                    {
                        graphics.Clip = region;
                        graphics.TranslateTransform((float) -bounds.Left, (float) -bounds.Top);
                        graphics.DrawImage(figureBitmap, bounds.Location);
                    }
                }
            }
            return image;
        }

        private Pen GetCropPen(float penWidth)
        {
            if (penWidth <= 0f)
            {
                return new Pen(Color.Black);
            }
            Pen pen = (Pen) this.shapeLayoutInfo.PenInfo.Pen.Clone();
            pen.Color = Color.Black;
            return pen;
        }

        private GraphicsPath GetOutlinePath(Bitmap bitmap)
        {
            GraphicsPath path = new GraphicsPath(FillMode.Winding);
            foreach (Point[] pointArray in new BitmapOutlineFinder().Process(bitmap))
            {
                path.AddPolygon(pointArray);
            }
            return path;
        }

        private Region GetSoftEdgeClipRegion(Bitmap figureBitmap, Rectangle bounds, float radius, DocumentLayoutUnitConverter converter)
        {
            float num = 2f * radius;
            if ((num > bounds.Width) || (num > bounds.Height))
            {
                return null;
            }
            Region region = null;
            using (GraphicsPath path = this.GetOutlinePath(figureBitmap))
            {
                using (Pen pen = this.GetCropPen(this.shapeLayoutInfo.PenWidth))
                {
                    region = new Region(path);
                    pen.Width = converter.LayoutUnitsToPixelsF(num);
                    if (GdipExtensions.AllowPathWidening(path))
                    {
                        WidenHelper.Apply(path, pen);
                    }
                    region.Exclude(path);
                }
            }
            return region;
        }
    }
}

