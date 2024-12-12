namespace DevExpress.Office.Drawing
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class GdiGraphicsPainter : IGraphicsPainter
    {
        private readonly Stack<Region> clipRegions;

        public GdiGraphicsPainter(GdiGraphics graphics) : base(graphics)
        {
            this.clipRegions = new Stack<Region>();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                while (this.clipRegions.Count > 0)
                {
                    this.clipRegions.Pop().Dispose();
                }
            }
            base.Dispose(disposing);
        }

        public override void PopTransform()
        {
            this.clipRegions.Pop().Dispose();
            base.PopTransform();
        }

        public override void PushRotationTransform(Point center, float angleInDegrees)
        {
            base.PushRotationTransform(center, angleInDegrees);
            this.clipRegions.Push(this.IGraphics.Clip);
        }

        protected override void SetClipBounds(RectangleF bounds)
        {
            if (this.clipRegions.Count <= 0)
            {
                base.SetClipBounds(bounds);
            }
            else
            {
                using (Region region = this.clipRegions.Peek().Clone())
                {
                    region.Intersect(bounds);
                    this.IGraphics.Clip = region;
                }
                base.SetActualClipBounds(bounds);
            }
        }

        public GdiGraphics IGraphics =>
            (GdiGraphics) base.IGraphics;
    }
}

