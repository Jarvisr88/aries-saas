namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Layout;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Drawing.Text;
    using System.Runtime.CompilerServices;

    public class TextRectanglePathInfo : DrawingPathInfo
    {
        private TextLayoutCalculator textLayoutCalculator;
        private readonly Rectangle textBoundsInLayoutUnits;

        internal TextRectanglePathInfo(TextLayoutCalculator textLayoutCalculator, Rectangle boundsInLayoutUnits) : base(new GraphicsPath(), () => CreateTextImage(textLayoutCalculator), boundsInLayoutUnits)
        {
            this.textLayoutCalculator = textLayoutCalculator;
            this.textBoundsInLayoutUnits = textLayoutCalculator.GetTextBounds();
        }

        private static System.Drawing.Image CreateBitmap(int width, int height) => 
            new Bitmap(width, height);

        private static System.Drawing.Image CreateMetafile(int width, int height)
        {
            RectangleF frameRect = new RectangleF(0f, 0f, (float) width, (float) height);
            return MetafileCreator.CreateInstance(frameRect, MetafileFrameUnit.Pixel, EmfType.EmfPlusOnly);
        }

        private static System.Drawing.Image CreateTextImage(TextLayoutCalculator textLayoutCalculator)
        {
            List<PathInfoBase> list;
            DocumentLayoutUnitConverter layoutUnitConverter = textLayoutCalculator.LayoutUnitConverter;
            using (ParagraphLayoutInfoConverter converter2 = textLayoutCalculator.GetLayoutInfoConverter())
            {
                list = converter2.Convert();
            }
            Size resultBitmapSize = textLayoutCalculator.GetResultBitmapSize();
            int width = layoutUnitConverter.LayoutUnitsToPixels(resultBitmapSize.Width);
            int height = layoutUnitConverter.LayoutUnitsToPixels(resultBitmapSize.Height);
            System.Drawing.Image image = (AzureCompatibility.Enable || !OSHelper.IsWindows) ? CreateBitmap(width, height) : CreateMetafile(width, height);
            using (Graphics graphics = GdipExtensions.PrepareGraphicsFromImage(image, (GraphicsUnit) layoutUnitConverter.GraphicsPageUnit, layoutUnitConverter.GraphicsPageScale))
            {
                if (textLayoutCalculator.WarpGeometry == null)
                {
                    textLayoutCalculator.ApplyGraphicsTransformation(graphics);
                }
                graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                foreach (PathInfoBase base2 in list)
                {
                    if (!base2.SkipDrawing)
                    {
                        base2.Draw(graphics, null, null);
                    }
                }
            }
            foreach (PathInfoBase base3 in list)
            {
                base3.Dispose();
            }
            list.Clear();
            return image;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (this.TextTransform != null)
            {
                this.TextTransform.Dispose();
                this.TextTransform = null;
            }
            if (this.textLayoutCalculator != null)
            {
                this.textLayoutCalculator.Dispose();
                this.textLayoutCalculator = null;
            }
        }

        public override void Draw(Graphics graphics, PenInfo penInfo, Matrix shapeTransform)
        {
            if (base.Print != null)
            {
                this.DrawCore(graphics, penInfo, this.TextTransform);
            }
        }

        public override RectangleF GetRealBounds(Matrix transform, PenInfo penInfo)
        {
            RectangleF boundsInLayoutUnits = base.BoundsInLayoutUnits;
            if (this.TextTransform != null)
            {
                boundsInLayoutUnits = RectangleUtils.BoundingRectangle(boundsInLayoutUnits, this.TextTransform);
            }
            return boundsInLayoutUnits;
        }

        public override bool HitTest(Point logicalPoint, Pen pen, Matrix invertedShapeTransform)
        {
            if (invertedShapeTransform != null)
            {
                logicalPoint = invertedShapeTransform.TransformPoint(logicalPoint);
            }
            return this.textBoundsInLayoutUnits.Contains(logicalPoint);
        }

        public Matrix TextTransform { get; set; }

        public override bool AllowHitTest =>
            true;
    }
}

