namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class PictureImagePathInfo : BitmapBasedPathInfo
    {
        public PictureImagePathInfo(GraphicsPath graphicsPath, Func<Image> bitmapRenderer, Rectangle boundsInLayoutUnits) : base(graphicsPath, bitmapRenderer, boundsInLayoutUnits)
        {
            this.<HitTestInfos>k__BackingField = new HitTestInfoCollection();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            foreach (HitTestInfo info in this.HitTestInfos)
            {
                info.Dispose();
            }
            this.HitTestInfos.Clear();
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

        protected internal override bool ShouldDrawGlowPath() => 
            true;

        internal HitTestInfoCollection HitTestInfos { get; }

        public override bool AllowHitTest =>
            true;

        public override bool Filled =>
            true;
    }
}

