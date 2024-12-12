namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public abstract class HitTestSupportedPathInfo : BitmapBasedPathInfo
    {
        private Lazy<HitTestInfoCollection> hitTestInfos;

        protected HitTestSupportedPathInfo(GraphicsPath graphicsPath, Func<Bitmap> bitmapRenderer, Func<HitTestInfoCollection> hitTestInfoProvider, Rectangle boundsInLayoutUnits) : base(graphicsPath, bitmapRenderer, boundsInLayoutUnits)
        {
            this.hitTestInfos = new Lazy<HitTestInfoCollection>(hitTestInfoProvider);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (this.hitTestInfos != null)
            {
                if (this.hitTestInfos.IsValueCreated && (this.hitTestInfos.Value != null))
                {
                    this.hitTestInfos.Value.Dispose();
                }
                this.hitTestInfos = null;
            }
        }

        public override bool HitTest(Point logicalPoint, Pen pen, Matrix invertedShapeTransform)
        {
            bool flag;
            if (base.RotateWithShape && (invertedShapeTransform != null))
            {
                logicalPoint = invertedShapeTransform.TransformPoint(logicalPoint);
            }
            using (List<HitTestInfo>.Enumerator enumerator = this.HitTestInfos.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        HitTestInfo current = enumerator.Current;
                        GraphicsPath graphicsPath = current.GraphicsPath;
                        if (current.HasFill && graphicsPath.IsVisible(logicalPoint))
                        {
                            flag = true;
                        }
                        else
                        {
                            if (!base.Stroke || ((pen == null) || !graphicsPath.IsOutlineVisible(logicalPoint, pen)))
                            {
                                continue;
                            }
                            flag = true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public override bool AllowHitTest =>
            true;

        internal HitTestInfoCollection HitTestInfos =>
            this.hitTestInfos.Value;
    }
}

