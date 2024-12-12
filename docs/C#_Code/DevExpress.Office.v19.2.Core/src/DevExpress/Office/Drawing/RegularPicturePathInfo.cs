namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Runtime.CompilerServices;

    public class RegularPicturePathInfo : BitmapBasedPathInfo
    {
        private readonly DrawingBlipFill fill;

        public RegularPicturePathInfo(GraphicsPath graphicsPath, DrawingBlipFill fill, Rectangle boundsInLayoutUnits) : this(graphicsPath, func1, boundsInLayoutUnits)
        {
            Func<Image> func1 = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Func<Image> local1 = <>c.<>9__1_0;
                func1 = <>c.<>9__1_0 = (Func<Image>) (() => null);
            }
            this.fill = fill;
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

        internal override void DrawCore(Graphics graphics, PenInfo penInfo)
        {
            DrawingBlipEffectWalker walker = new DrawingBlipEffectWalker();
            walker.Walk(this.fill.Blip.Effects);
            ColorMatrix newColorMatrix = !this.fill.BlackAndWhitePrintMode ? (walker.HasColorMatrix ? new ColorMatrix(walker.ColorMatrixElements) : null) : ShapeFillRenderVisitor.GetGrayscaleColorMatrix();
            ColorMap[] colorMap = walker.GetColorMap();
            Bitmap nativeImage = (Bitmap) this.fill.Blip.Image.NativeImage;
            RectangleF imageBounds = nativeImage.GetImageBounds();
            InterpolationMode interpolationMode = graphics.InterpolationMode;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            try
            {
                if ((newColorMatrix == null) && (colorMap == null))
                {
                    graphics.DrawImage(nativeImage, base.BoundsInLayoutUnits);
                }
                else
                {
                    using (ImageAttributes attributes = new ImageAttributes())
                    {
                        if (newColorMatrix != null)
                        {
                            attributes.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Default);
                        }
                        if (colorMap != null)
                        {
                            attributes.SetRemapTable(colorMap);
                        }
                        graphics.DrawImage(nativeImage, base.BoundsInLayoutUnits, imageBounds.X, imageBounds.Y, imageBounds.Width, imageBounds.Height, GraphicsUnit.Pixel, attributes);
                    }
                }
            }
            finally
            {
                graphics.InterpolationMode = interpolationMode;
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

        protected internal override bool ShouldDrawGlowPath() => 
            false;

        internal HitTestInfoCollection HitTestInfos { get; }

        public override bool AllowHitTest =>
            true;

        public override bool Filled =>
            true;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RegularPicturePathInfo.<>c <>9 = new RegularPicturePathInfo.<>c();
            public static Func<Image> <>9__1_0;

            internal Image <.ctor>b__1_0() => 
                null;
        }
    }
}

