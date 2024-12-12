namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Layout;
    using DevExpress.Office.Model;
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Runtime.CompilerServices;

    public class ShapeFillRenderVisitor : IDrawingFillVisitor
    {
        private static Dictionary<DrawingPatternType, HatchStyle> hatchStyleTable;
        private readonly ShapePreset shapeType;
        private readonly Color styleColor;
        private readonly List<GraphicsPath> graphicsPaths;

        public ShapeFillRenderVisitor(ShapePreset shapeType, Color styleColor, Brush defaultBrush, List<GraphicsPath> graphicsPaths, Matrix shapeTransform)
        {
            this.shapeType = shapeType;
            this.styleColor = styleColor;
            this.DefaultBrush = defaultBrush;
            this.graphicsPaths = graphicsPaths;
            this.ShapeTransform = shapeTransform;
            this.ScaleFactor = 1f;
        }

        protected virtual void ApplyTransformForTextureBrush(TextureBrush brush)
        {
        }

        private TextureBrush BlipFillFromBitmap(DrawingBlipFill fill)
        {
            Rectangle boundsExt;
            TextureBrush brush;
            float num3;
            float num4;
            using (GraphicsPath path = GdipExtensions.BuildFigure(this.graphicsPaths, false))
            {
                boundsExt = path.GetBoundsExt(this.HasPermanentFill ? this.ShapeTransform : null);
            }
            DocumentLayoutUnitConverter layoutUnitConverter = fill.DocumentModel.LayoutUnitConverter;
            Image nativeImage = fill.Blip.Image.NativeImage;
            bool flag = !fill.SourceRectangle.Equals(RectangleOffset.Empty);
            RectangleF imageBounds = nativeImage.GetImageBounds();
            int num = (int) Math.Round((double) imageBounds.Width);
            int num2 = (int) Math.Round((double) imageBounds.Height);
            if ((num == 0) || (num2 == 0))
            {
                return null;
            }
            using (ImageAttributes attributes = this.CreateImageAttributes(fill))
            {
                if (flag)
                {
                    nativeImage = this.CropImage(nativeImage, imageBounds, fill.SourceRectangle);
                }
                else if (fill.BlackAndWhitePrintMode)
                {
                    nativeImage = (Image) nativeImage.Clone();
                }
                num3 = this.CorrectResolution(nativeImage.HorizontalResolution);
                num4 = this.CorrectResolution(nativeImage.VerticalResolution);
                try
                {
                    brush = new TextureBrush(nativeImage, nativeImage.GetImageBounds(), attributes);
                }
                catch
                {
                    return null;
                }
                if (flag || fill.BlackAndWhitePrintMode)
                {
                    nativeImage.Dispose();
                }
            }
            brush.TranslateTransform((float) boundsExt.X, (float) boundsExt.Y);
            float imageWidthInLayouts = (layoutUnitConverter.PixelsToLayoutUnitsF((float) num) * layoutUnitConverter.ScreenDpiX) / layoutUnitConverter.Dpi;
            float imageHeightInLayouts = (layoutUnitConverter.PixelsToLayoutUnitsF((float) num2) * layoutUnitConverter.ScreenDpiY) / layoutUnitConverter.Dpi;
            if (!fill.Stretch)
            {
                this.SetupTileTexture(fill, brush, num3, num4, imageWidthInLayouts, imageHeightInLayouts, boundsExt);
            }
            else
            {
                int width = boundsExt.Width;
                int height = boundsExt.Height;
                RectangleOffset fillRectangle = fill.FillRectangle;
                float dx = (float) (width * DrawingValueConverter.FromPercentage(fillRectangle.LeftOffset));
                float dy = (float) (height * DrawingValueConverter.FromPercentage(fillRectangle.TopOffset));
                float num11 = (float) (width * DrawingValueConverter.FromPercentage(fillRectangle.RightOffset));
                float num12 = (float) (height * DrawingValueConverter.FromPercentage(fillRectangle.BottomOffset));
                float num14 = (height - dy) - num12;
                brush.WrapMode = WrapMode.Clamp;
                brush.TranslateTransform(dx, dy);
                brush.ScaleTransform(((width - dx) - num11) / imageWidthInLayouts, num14 / imageHeightInLayouts);
            }
            return brush;
        }

        private unsafe TextureBrush BlipFillFromMetafile(DrawingBlipFill fill)
        {
            Rectangle boundsExt;
            RectangleF imageBounds;
            TextureBrush brush;
            float num5;
            float num6;
            using (GraphicsPath path = GdipExtensions.BuildFigure(this.graphicsPaths, false))
            {
                if (this.HasPermanentFill)
                {
                    boundsExt = path.GetBoundsExt(this.ShapeTransform);
                }
                else
                {
                    using (Matrix matrix = new Matrix(this.ScaleFactor, 0f, 0f, this.ScaleFactor, 0f, 0f))
                    {
                        boundsExt = path.GetBoundsExt(matrix);
                    }
                }
            }
            DocumentLayoutUnitConverter layoutUnitConverter = fill.DocumentModel.LayoutUnitConverter;
            Image nativeImage = fill.Blip.Image.NativeImage;
            bool flag = !fill.SourceRectangle.Equals(RectangleOffset.Empty);
            int width = boundsExt.Width;
            int height = boundsExt.Height;
            if (!fill.Stretch)
            {
                imageBounds = nativeImage.GetImageBounds();
                RectangleF* efPtr1 = &imageBounds;
                efPtr1.Width *= layoutUnitConverter.ScreenDpiX / this.CorrectResolution(nativeImage.HorizontalResolution);
                RectangleF* efPtr2 = &imageBounds;
                efPtr2.Height *= layoutUnitConverter.ScreenDpiY / this.CorrectResolution(nativeImage.VerticalResolution);
            }
            else
            {
                RectangleOffset fillRectangle = fill.FillRectangle;
                float num9 = (float) (width * DrawingValueConverter.FromPercentage(fillRectangle.LeftOffset));
                float num10 = (float) (height * DrawingValueConverter.FromPercentage(fillRectangle.TopOffset));
                float num11 = (float) (width * DrawingValueConverter.FromPercentage(fillRectangle.RightOffset));
                float num12 = (float) (height * DrawingValueConverter.FromPercentage(fillRectangle.BottomOffset));
                imageBounds = new RectangleF(0f, 0f, (width - num9) - num11, (height - num10) - num12);
            }
            int num3 = (int) Math.Round((double) imageBounds.Width);
            int num4 = (int) Math.Round((double) imageBounds.Height);
            if ((num3 == 0) || (num4 == 0))
            {
                return null;
            }
            using (ImageAttributes attributes = this.CreateImageAttributes(fill))
            {
                using (Image image2 = flag ? this.CropImage(nativeImage, imageBounds, fill.SourceRectangle) : this.ScaleImage(nativeImage, imageBounds))
                {
                    num5 = this.CorrectResolution(image2.HorizontalResolution);
                    num6 = this.CorrectResolution(image2.VerticalResolution);
                    brush = new TextureBrush(image2, image2.GetImageBounds(), attributes);
                }
            }
            brush.TranslateTransform((float) boundsExt.X, (float) boundsExt.Y);
            float imageWidthInLayouts = (layoutUnitConverter.PixelsToLayoutUnitsF((float) num3) * layoutUnitConverter.ScreenDpiX) / layoutUnitConverter.Dpi;
            float imageHeightInLayouts = (layoutUnitConverter.PixelsToLayoutUnitsF((float) num4) * layoutUnitConverter.ScreenDpiY) / layoutUnitConverter.Dpi;
            if (!fill.Stretch)
            {
                this.SetupTileTexture(fill, brush, num5, num6, imageWidthInLayouts, imageHeightInLayouts, boundsExt);
            }
            else
            {
                RectangleOffset fillRectangle = fill.FillRectangle;
                brush.WrapMode = WrapMode.Clamp;
                brush.TranslateTransform((float) (width * DrawingValueConverter.FromPercentage(fillRectangle.LeftOffset)), (float) (height * DrawingValueConverter.FromPercentage(fillRectangle.TopOffset)));
            }
            if (fill.RotateWithShape && (this.ScaleFactor != 1f))
            {
                brush.ScaleTransform(1f / this.ScaleFactor, 1f / this.ScaleFactor, MatrixOrder.Append);
            }
            return brush;
        }

        private float CalculateCenterOffset(float areaSize, float imageSize) => 
            (areaSize / 2f) - (imageSize / 2f);

        private float CalculateFarOffset(float areaSize, float imageSize) => 
            areaSize - imageSize;

        internal static PointF[] CalculateGradientPoints(Rectangle bounds, double gradientAngle)
        {
            PointF tf = (PointF) RectangleUtils.CenterPoint(bounds);
            double num = Math.Sqrt((Math.Pow((double) bounds.Width, 2.0) / 4.0) + (Math.Pow((double) bounds.Height, 2.0) / 4.0));
            double num2 = DrawingValueConverter.RadianToDegree(Math.Atan2((double) bounds.Height, (double) bounds.Width));
            bool flag = (gradientAngle % 180.0) <= 90.0;
            double num4 = num * Math.Sin(DrawingValueConverter.DegreeToRadian(flag ? ((gradientAngle + 90.0) - num2) : ((gradientAngle - 90.0) + num2)));
            double degree = flag ? gradientAngle : (180.0 - gradientAngle);
            double num5 = num4 * Math.Cos(DrawingValueConverter.DegreeToRadian(degree));
            double num6 = num4 * Math.Sin(DrawingValueConverter.DegreeToRadian(degree));
            double num7 = tf.X + (flag ? -num5 : num5);
            double num8 = tf.Y - num6;
            double num9 = tf.X + (flag ? num5 : -num5);
            double num10 = tf.Y + num6;
            return new PointF[] { new PointF((float) num7, (float) num8), new PointF((float) num9, (float) num10) };
        }

        private bool CheckSmallestAreaForGdiPlus(PointF p1, PointF p2)
        {
            float num = Math.Abs((float) (p2.X - p1.X));
            float num2 = Math.Abs((float) (p2.Y - p1.Y));
            return (((num != 0f) || (num2 != 0f)) ? ((num == 0f) || ((num2 == 0f) || ((num * num2) >= 1.192093E-07f))) : false);
        }

        private float CorrectResolution(float resolution) => 
            (resolution == 0f) ? 96f : resolution;

        internal static GradientType CorrectShapeGradientType(GradientType gradientType, ShapePreset shapeType, bool outlineFill)
        {
            if (gradientType == GradientType.Shape)
            {
                if (outlineFill || UseCircleGradient(shapeType))
                {
                    return GradientType.Circle;
                }
                if (UseRectangularGradient(shapeType))
                {
                    return GradientType.Rectangle;
                }
            }
            return gradientType;
        }

        private static Dictionary<DrawingPatternType, HatchStyle> CreateHatchStyleTable() => 
            new Dictionary<DrawingPatternType, HatchStyle> { 
                { 
                    DrawingPatternType.Cross,
                    HatchStyle.Cross
                },
                { 
                    DrawingPatternType.DashedDownwardDiagonal,
                    HatchStyle.DashedDownwardDiagonal
                },
                { 
                    DrawingPatternType.DashedHorizontal,
                    HatchStyle.DashedHorizontal
                },
                { 
                    DrawingPatternType.DashedUpwardDiagonal,
                    HatchStyle.DashedUpwardDiagonal
                },
                { 
                    DrawingPatternType.DashedVertical,
                    HatchStyle.DashedVertical
                },
                { 
                    DrawingPatternType.DiagonalBrick,
                    HatchStyle.DiagonalBrick
                },
                { 
                    DrawingPatternType.DiagonalCross,
                    HatchStyle.DiagonalCross
                },
                { 
                    DrawingPatternType.Divot,
                    HatchStyle.Divot
                },
                { 
                    DrawingPatternType.DarkDownwardDiagonal,
                    HatchStyle.DarkDownwardDiagonal
                },
                { 
                    DrawingPatternType.DarkHorizontal,
                    HatchStyle.DarkHorizontal
                },
                { 
                    DrawingPatternType.DarkUpwardDiagonal,
                    HatchStyle.DarkUpwardDiagonal
                },
                { 
                    DrawingPatternType.DarkVertical,
                    HatchStyle.DarkVertical
                },
                { 
                    DrawingPatternType.DownwardDiagonal,
                    HatchStyle.WideDownwardDiagonal
                },
                { 
                    DrawingPatternType.DottedDiamond,
                    HatchStyle.DottedDiamond
                },
                { 
                    DrawingPatternType.DottedGrid,
                    HatchStyle.DottedGrid
                },
                { 
                    DrawingPatternType.Horizontal,
                    HatchStyle.Horizontal
                },
                { 
                    DrawingPatternType.HorizontalBrick,
                    HatchStyle.HorizontalBrick
                },
                { 
                    DrawingPatternType.LargeCheckerBoard,
                    HatchStyle.LargeCheckerBoard
                },
                { 
                    DrawingPatternType.LargeConfetti,
                    HatchStyle.LargeConfetti
                },
                { 
                    DrawingPatternType.LargeGrid,
                    HatchStyle.Cross
                },
                { 
                    DrawingPatternType.LightDownwardDiagonal,
                    HatchStyle.LightDownwardDiagonal
                },
                { 
                    DrawingPatternType.LightHorizontal,
                    HatchStyle.LightHorizontal
                },
                { 
                    DrawingPatternType.LightUpwardDiagonal,
                    HatchStyle.LightUpwardDiagonal
                },
                { 
                    DrawingPatternType.LightVertical,
                    HatchStyle.LightVertical
                },
                { 
                    DrawingPatternType.NarrowHorizontal,
                    HatchStyle.NarrowHorizontal
                },
                { 
                    DrawingPatternType.NarrowVertical,
                    HatchStyle.NarrowVertical
                },
                { 
                    DrawingPatternType.OpenDiamond,
                    HatchStyle.OutlinedDiamond
                },
                { 
                    DrawingPatternType.Percent10,
                    HatchStyle.Percent10
                },
                { 
                    DrawingPatternType.Percent20,
                    HatchStyle.Percent20
                },
                { 
                    DrawingPatternType.Percent25,
                    HatchStyle.Percent25
                },
                { 
                    DrawingPatternType.Percent30,
                    HatchStyle.Percent30
                },
                { 
                    DrawingPatternType.Percent40,
                    HatchStyle.Percent40
                },
                { 
                    DrawingPatternType.Percent5,
                    HatchStyle.Percent05
                },
                { 
                    DrawingPatternType.Percent50,
                    HatchStyle.Percent50
                },
                { 
                    DrawingPatternType.Percent60,
                    HatchStyle.Percent60
                },
                { 
                    DrawingPatternType.Percent70,
                    HatchStyle.Percent70
                },
                { 
                    DrawingPatternType.Percent75,
                    HatchStyle.Percent75
                },
                { 
                    DrawingPatternType.Percent80,
                    HatchStyle.Percent80
                },
                { 
                    DrawingPatternType.Percent90,
                    HatchStyle.Percent90
                },
                { 
                    DrawingPatternType.Plaid,
                    HatchStyle.Plaid
                },
                { 
                    DrawingPatternType.Shingle,
                    HatchStyle.Shingle
                },
                { 
                    DrawingPatternType.SmallCheckerBoard,
                    HatchStyle.SmallCheckerBoard
                },
                { 
                    DrawingPatternType.SmallConfetti,
                    HatchStyle.SmallConfetti
                },
                { 
                    DrawingPatternType.SmallGrid,
                    HatchStyle.SmallGrid
                },
                { 
                    DrawingPatternType.SolidDiamond,
                    HatchStyle.SolidDiamond
                },
                { 
                    DrawingPatternType.Sphere,
                    HatchStyle.Sphere
                },
                { 
                    DrawingPatternType.Trellis,
                    HatchStyle.Trellis
                },
                { 
                    DrawingPatternType.UpwardDiagonal,
                    HatchStyle.WideUpwardDiagonal
                },
                { 
                    DrawingPatternType.Vertical,
                    HatchStyle.Vertical
                },
                { 
                    DrawingPatternType.Wave,
                    HatchStyle.Wave
                },
                { 
                    DrawingPatternType.WideDownwardDiagonal,
                    HatchStyle.WideDownwardDiagonal
                },
                { 
                    DrawingPatternType.WideUpwardDiagonal,
                    HatchStyle.WideUpwardDiagonal
                },
                { 
                    DrawingPatternType.Weave,
                    HatchStyle.Weave
                },
                { 
                    DrawingPatternType.ZigZag,
                    HatchStyle.ZigZag
                }
            };

        private ImageAttributes CreateImageAttributes(DrawingBlipFill blipFill)
        {
            if (blipFill.BlackAndWhitePrintMode)
            {
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(GetGrayscaleColorMatrix(), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                return attributes;
            }
            DrawingBlipEffectWalker walker = new DrawingBlipEffectWalker();
            walker.Walk(blipFill.Blip.Effects);
            if (!walker.HasColorMatrix && !walker.HasColorMap)
            {
                return null;
            }
            ImageAttributes attributes2 = new ImageAttributes();
            if (walker.HasColorMatrix)
            {
                attributes2.SetColorMatrix(new ColorMatrix(walker.ColorMatrixElements), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            }
            if (walker.HasColorMap)
            {
                attributes2.SetRemapTable(walker.GetColorMap());
            }
            return attributes2;
        }

        protected virtual GraphicsPath CreateRadialGradientPath(Rectangle bounds, ref Point centerPoint)
        {
            GraphicsPath path = new GraphicsPath();
            Point point = RectangleUtils.CenterPoint(bounds);
            int num = (int) Math.Round((double) (Math.Sqrt(Math.Pow((double) bounds.Width, 2.0) + Math.Pow((double) bounds.Height, 2.0)) / 2.0));
            Rectangle rect = new Rectangle(point.X - num, point.Y - num, num * 2, num * 2);
            path.AddEllipse(rect);
            return path;
        }

        protected virtual GraphicsPath CreateRectangularGradientPath(Rectangle bounds, ref Point centerPoint)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(bounds);
            return path;
        }

        private Image CropImage(Image image, RectangleF imageBounds, RectangleOffset sourceRectangle)
        {
            RectangleF rect = ShapeFillAttributeCalculator.CalculatePictureClipBounds(sourceRectangle, imageBounds);
            Image image2 = new Bitmap((int) Math.Round((double) imageBounds.Width), (int) Math.Round((double) imageBounds.Height));
            using (Graphics graphics = GdipExtensions.PrepareGraphicsFromImage(image2))
            {
                graphics.CompositingQuality = CompositingQuality.AssumeLinear;
                graphics.DrawImage(image, rect);
            }
            return image2;
        }

        private Color GetColorFromStyle(DrawingColor drawingColor)
        {
            if (this.StyleColorEmpty)
            {
                return drawingColor.FinalColor;
            }
            DrawingColor color = DrawingColor.Create(FakeDocumentModel.Instance, this.styleColor);
            color.CopyFrom(drawingColor);
            color.Info.SchemeColor = SchemeColorValues.Style;
            return color.ToRgb(this.styleColor);
        }

        public static ColorMatrix GetGrayscaleColorMatrix()
        {
            float[] singleArray1 = new float[] { 0.3f, 0.3f, 0.3f, 0f, 0f };
            float[][] newColorMatrix = new float[5][];
            newColorMatrix[0] = singleArray1;
            newColorMatrix[1] = new float[] { 0.59f, 0.59f, 0.59f, 0f, 0f };
            newColorMatrix[2] = new float[] { 0.11f, 0.11f, 0.11f, 0f, 0f };
            float[] singleArray4 = new float[5];
            singleArray4[3] = 1f;
            newColorMatrix[3] = singleArray4;
            float[] singleArray5 = new float[5];
            singleArray5[4] = 1f;
            newColorMatrix[4] = singleArray5;
            return new ColorMatrix(newColorMatrix);
        }

        private Rectangle GetRectOffset(Rectangle original, RectangleOffset offset)
        {
            int x = (int) Math.Round((double) (original.Left + (original.Width * DrawingValueConverter.FromPercentage(offset.LeftOffset))));
            int y = (int) Math.Round((double) (original.Top + (original.Height * DrawingValueConverter.FromPercentage(offset.TopOffset))));
            int num4 = (int) Math.Round((double) (original.Bottom - (original.Height * DrawingValueConverter.FromPercentage(offset.BottomOffset))));
            return new Rectangle(x, y, Math.Max(0, ((int) Math.Round((double) (original.Right - (original.Width * DrawingValueConverter.FromPercentage(offset.RightOffset))))) - x), Math.Max(0, num4 - y));
        }

        protected virtual RectangleOffset GetTileRect(DrawingGradientFill fill) => 
            fill.TileRect;

        private ColorBlend InverseBlend(ColorBlend value)
        {
            int length = value.Colors.Length;
            ColorBlend blend = new ColorBlend(length);
            for (int i = 0; i < length; i++)
            {
                blend.Colors[i] = value.Colors[(length - 1) - i];
                blend.Positions[i] = 1f - value.Positions[(length - 1) - i];
            }
            return blend;
        }

        private static List<DrawingGradientStop> PrepareGradientStops(DrawingGradientFill fill)
        {
            List<DrawingGradientStop> list = new List<DrawingGradientStop>();
            foreach (DrawingGradientStop stop in fill.GradientStops)
            {
                list.Add(stop);
            }
            list.Sort(new DrawingGradientStopComparer());
            return list;
        }

        private static float ScaleAngle(float angle, Rectangle bounds)
        {
            int num = 0;
            while (angle < 0f)
            {
                angle += 90f;
                num--;
            }
            while (angle > 90f)
            {
                angle -= 90f;
                num++;
            }
            int num2 = Math.Min(bounds.Width, bounds.Height);
            if (num2 == 0)
            {
                return angle;
            }
            double num3 = ((double) num2) / ((double) bounds.Width);
            double num4 = ((double) num2) / ((double) bounds.Height);
            return (float) ((DrawingValueConverter.RadianToDegree(Math.Atan(Math.Tan(DrawingValueConverter.DegreeToRadian((double) angle)) * (((num % 2) == 0) ? (num4 / num3) : (num3 / num4)))) + (num * 90)) % 360.0);
        }

        private Image ScaleImage(Image image, RectangleF imageBounds)
        {
            int width = (int) Math.Round((double) imageBounds.Width);
            int height = (int) Math.Round((double) imageBounds.Height);
            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics graphics = GdipExtensions.PrepareGraphicsFromImage(bitmap))
            {
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.DrawImage(image, new Rectangle(0, 0, width, height));
            }
            return bitmap;
        }

        private void SetupLinearGradientBrush(DrawingGradientFill fill, Rectangle bounds, ColorBlend colorBlend)
        {
            float angle = fill.DocumentModel.UnitConverter.ModelUnitsToDegreeF(fill.Angle) % 360f;
            Rectangle rectangle = bounds;
            if (fill.Scaled)
            {
                angle = ScaleAngle(angle, rectangle);
            }
            PointF[] tfArray = CalculateGradientPoints(rectangle, (double) angle);
            PointF tf = tfArray[0];
            PointF tf2 = tfArray[1];
            if (this.CheckSmallestAreaForGdiPlus(tf, tf2))
            {
                Color[] colors = colorBlend.Colors;
                int length = colors.Length;
                Color color = (angle >= 180f) ? colors[0] : colors[length - 1];
                LinearGradientBrush brush = new LinearGradientBrush(tf, tf2, color, (angle >= 180f) ? colors[length - 1] : colors[0]) {
                    WrapMode = WrapMode.TileFlipXY
                };
                if (length != 2)
                {
                    brush.InterpolationColors = (angle >= 180f) ? this.InverseBlend(colorBlend) : colorBlend;
                }
                else
                {
                    brush.SetSigmaBellShape((angle >= 180f) ? ((float) 1) : ((float) 0), 1f);
                    brush.GammaCorrection = true;
                }
                this.TransformLinearGradientBrush(brush);
                this.Result = brush;
            }
        }

        private void SetupRadialGradientBrush(Rectangle bounds, ColorBlend colorBlend, Point centerPoint)
        {
            using (GraphicsPath path = this.CreateRadialGradientPath(bounds, ref centerPoint))
            {
                PathGradientBrush brush = new PathGradientBrush(path) {
                    CenterPoint = (PointF) centerPoint,
                    InterpolationColors = this.InverseBlend(colorBlend)
                };
                this.Result = brush;
            }
        }

        private void SetupRectangularGradientBrush(Rectangle bounds, ColorBlend colorBlend, Point centerPoint)
        {
            using (GraphicsPath path = this.CreateRectangularGradientPath(bounds, ref centerPoint))
            {
                PathGradientBrush brush = new PathGradientBrush(path) {
                    InterpolationColors = this.InverseBlend(colorBlend),
                    CenterPoint = (PointF) centerPoint
                };
                this.Result = brush;
            }
        }

        private void SetupShapeGradientBrush(GraphicsPath figure, ColorBlend colorBlend, Point centerPoint)
        {
            if (this.HasPermanentFill)
            {
                if (this.ShapeTransform != null)
                {
                    figure.Transform(this.ShapeTransform);
                }
                centerPoint = RectangleUtils.CenterPoint(figure.GetBoundsExt());
            }
            PathGradientBrush brush = new PathGradientBrush(figure) {
                CenterPoint = (PointF) centerPoint,
                InterpolationColors = this.InverseBlend(colorBlend)
            };
            this.Result = brush;
        }

        private void SetupTileTexture(DrawingBlipFill fill, TextureBrush textureBrush, float horizontalResolution, float verticalResolution, float imageWidthInLayouts, float imageHeightInLayouts, Rectangle textureBounds)
        {
            IDocumentModel documentModel = fill.DocumentModel;
            DocumentLayoutUnitConverter layoutUnitConverter = documentModel.LayoutUnitConverter;
            int width = textureBounds.Width;
            int height = textureBounds.Height;
            this.SetupWrapMode(textureBrush, fill.TileFlip);
            DocumentModelUnitToLayoutUnitConverter toDocumentLayoutUnitConverter = documentModel.ToDocumentLayoutUnitConverter;
            float dx = toDocumentLayoutUnitConverter.ToLayoutUnits((float) fill.OffsetX);
            float dy = toDocumentLayoutUnitConverter.ToLayoutUnits((float) fill.OffsetY);
            float sx = ((((float) DrawingValueConverter.FromPercentage(fill.ScaleX)) * layoutUnitConverter.Dpi) / horizontalResolution) * this.ScaleFactor;
            float sy = ((((float) DrawingValueConverter.FromPercentage(fill.ScaleY)) * layoutUnitConverter.Dpi) / verticalResolution) * this.ScaleFactor;
            float imageSize = imageWidthInLayouts * sx;
            float num8 = imageHeightInLayouts * sy;
            switch (fill.TileAlign)
            {
                case RectangleAlignType.Top:
                    dx += this.CalculateCenterOffset((float) width, imageSize);
                    break;

                case RectangleAlignType.TopRight:
                    dx += this.CalculateFarOffset((float) width, imageSize);
                    break;

                case RectangleAlignType.Left:
                    dy += this.CalculateCenterOffset((float) height, num8);
                    break;

                case RectangleAlignType.Center:
                    dx += this.CalculateCenterOffset((float) width, imageSize);
                    dy += this.CalculateCenterOffset((float) height, num8);
                    break;

                case RectangleAlignType.Right:
                    dx += this.CalculateFarOffset((float) width, imageSize);
                    dy += this.CalculateCenterOffset((float) height, num8);
                    break;

                case RectangleAlignType.BottomLeft:
                    dy += this.CalculateFarOffset((float) height, num8);
                    break;

                case RectangleAlignType.Bottom:
                    dx += this.CalculateCenterOffset((float) width, imageSize);
                    dy += this.CalculateFarOffset((float) height, num8);
                    break;

                case RectangleAlignType.BottomRight:
                    dx += this.CalculateFarOffset((float) width, imageSize);
                    dy += this.CalculateFarOffset((float) height, num8);
                    break;

                default:
                    break;
            }
            textureBrush.TranslateTransform(dx, dy);
            textureBrush.ScaleTransform(sx, sy);
        }

        private void SetupWrapMode(TextureBrush brush, TileFlipType flipType)
        {
            switch (flipType)
            {
                case TileFlipType.Horizontal:
                    brush.WrapMode = WrapMode.TileFlipX;
                    return;

                case TileFlipType.Vertical:
                    brush.WrapMode = WrapMode.TileFlipY;
                    return;

                case TileFlipType.Both:
                    brush.WrapMode = WrapMode.TileFlipXY;
                    return;
            }
            brush.WrapMode = WrapMode.Tile;
        }

        protected virtual void TransformLinearGradientBrush(LinearGradientBrush brush)
        {
        }

        private static bool UseCircleGradient(ShapePreset shapePreset)
        {
            if (shapePreset > ShapePreset.FlowChartMagneticDisk)
            {
                return ((shapePreset > ShapePreset.Funnel) ? ((shapePreset == ShapePreset.MathDivide) || (shapePreset == ShapePreset.MathNotEqual)) : ((shapePreset == ShapePreset.FlowChartDisplay) || (shapePreset == ShapePreset.Funnel)));
            }
            if (shapePreset != ShapePreset.None)
            {
                switch (shapePreset)
                {
                    case ShapePreset.Chevron:
                    case ShapePreset.BlockArc:
                    case ShapePreset.Donut:
                    case ShapePreset.NoSmoking:
                    case ShapePreset.RightArrow:
                    case ShapePreset.LeftArrow:
                    case ShapePreset.UpArrow:
                    case ShapePreset.DownArrow:
                    case ShapePreset.StripedRightArrow:
                    case ShapePreset.NotchedRightArrow:
                    case ShapePreset.BentUpArrow:
                    case ShapePreset.LeftRightArrow:
                    case ShapePreset.UpDownArrow:
                    case ShapePreset.LeftUpArrow:
                    case ShapePreset.LeftRightUpArrow:
                    case ShapePreset.QuadArrow:
                    case ShapePreset.LeftArrowCallout:
                    case ShapePreset.RightArrowCallout:
                    case ShapePreset.UpArrowCallout:
                    case ShapePreset.DownArrowCallout:
                    case ShapePreset.LeftRightArrowCallout:
                    case ShapePreset.UpDownArrowCallout:
                    case ShapePreset.QuadArrowCallout:
                    case ShapePreset.BentArrow:
                    case ShapePreset.UturnArrow:
                    case ShapePreset.CircularArrow:
                    case ShapePreset.LeftCircularArrow:
                    case ShapePreset.LeftRightCircularArrow:
                    case ShapePreset.CurvedRightArrow:
                    case ShapePreset.CurvedLeftArrow:
                    case ShapePreset.CurvedUpArrow:
                    case ShapePreset.CurvedDownArrow:
                    case ShapePreset.SwooshArrow:
                    case ShapePreset.LightningBolt:
                    case ShapePreset.Sun:
                    case ShapePreset.Moon:
                    case ShapePreset.SmileyFace:
                    case ShapePreset.LeftBrace:
                    case ShapePreset.RightBrace:
                    case ShapePreset.BracePair:
                    case ShapePreset.WedgeRectCallout:
                    case ShapePreset.WedgeRoundRectCallout:
                    case ShapePreset.WedgeEllipseCallout:
                    case ShapePreset.CloudCallout:
                    case ShapePreset.Cloud:
                    case ShapePreset.Ribbon:
                    case ShapePreset.Ribbon2:
                    case ShapePreset.EllipseRibbon:
                    case ShapePreset.EllipseRibbon2:
                    case ShapePreset.LeftRightRibbon:
                    case ShapePreset.VerticalScroll:
                    case ShapePreset.HorizontalScroll:
                    case ShapePreset.Wave:
                    case ShapePreset.DoubleWave:
                    case ShapePreset.FlowChartDocument:
                    case ShapePreset.FlowChartMultidocument:
                    case ShapePreset.FlowChartPunchedTape:
                        break;

                    case ShapePreset.PieWedge:
                    case ShapePreset.Pie:
                    case ShapePreset.Cube:
                    case ShapePreset.Can:
                    case ShapePreset.Heart:
                    case ShapePreset.IrregularSeal1:
                    case ShapePreset.IrregularSeal2:
                    case ShapePreset.FoldedCorner:
                    case ShapePreset.Bevel:
                    case ShapePreset.Frame:
                    case ShapePreset.HalfFrame:
                    case ShapePreset.Corner:
                    case ShapePreset.DiagStripe:
                    case ShapePreset.Chord:
                    case ShapePreset.Arc:
                    case ShapePreset.LeftBracket:
                    case ShapePreset.RightBracket:
                    case ShapePreset.BracketPair:
                    case ShapePreset.StraightConnector1:
                    case ShapePreset.BentConnector2:
                    case ShapePreset.BentConnector3:
                    case ShapePreset.BentConnector4:
                    case ShapePreset.BentConnector5:
                    case ShapePreset.CurvedConnector2:
                    case ShapePreset.CurvedConnector3:
                    case ShapePreset.CurvedConnector4:
                    case ShapePreset.CurvedConnector5:
                    case ShapePreset.Callout1:
                    case ShapePreset.Callout2:
                    case ShapePreset.Callout3:
                    case ShapePreset.AccentCallout1:
                    case ShapePreset.AccentCallout2:
                    case ShapePreset.AccentCallout3:
                    case ShapePreset.BorderCallout1:
                    case ShapePreset.BorderCallout2:
                    case ShapePreset.BorderCallout3:
                    case ShapePreset.AccentBorderCallout1:
                    case ShapePreset.AccentBorderCallout2:
                    case ShapePreset.AccentBorderCallout3:
                    case ShapePreset.Plus:
                    case ShapePreset.FlowChartProcess:
                    case ShapePreset.FlowChartDecision:
                    case ShapePreset.FlowChartInputOutput:
                    case ShapePreset.FlowChartPredefinedProcess:
                    case ShapePreset.FlowChartInternalStorage:
                    case ShapePreset.FlowChartTerminator:
                    case ShapePreset.FlowChartPreparation:
                    case ShapePreset.FlowChartManualInput:
                    case ShapePreset.FlowChartManualOperation:
                    case ShapePreset.FlowChartConnector:
                    case ShapePreset.FlowChartPunchedCard:
                        return false;

                    default:
                        return (shapePreset == ShapePreset.FlowChartMagneticDisk);
                }
            }
            return true;
        }

        private static bool UseRectangularGradient(ShapePreset shapePreset)
        {
            if (shapePreset > ShapePreset.AccentBorderCallout3)
            {
                switch (shapePreset)
                {
                    case ShapePreset.FlowChartProcess:
                    case ShapePreset.FlowChartPredefinedProcess:
                    case ShapePreset.FlowChartInternalStorage:
                        break;

                    case ShapePreset.FlowChartDecision:
                    case ShapePreset.FlowChartInputOutput:
                        goto TR_0000;

                    default:
                        switch (shapePreset)
                        {
                            case ShapePreset.FlowChartCollate:
                            case ShapePreset.ActionButtonBlank:
                            case ShapePreset.ActionButtonHome:
                            case ShapePreset.ActionButtonHelp:
                            case ShapePreset.ActionButtonInformation:
                            case ShapePreset.ActionButtonForwardNext:
                            case ShapePreset.ActionButtonBackPrevious:
                            case ShapePreset.ActionButtonEnd:
                            case ShapePreset.ActionButtonBeginning:
                            case ShapePreset.ActionButtonReturn:
                            case ShapePreset.ActionButtonDocument:
                            case ShapePreset.ActionButtonSound:
                            case ShapePreset.ActionButtonMovie:
                                break;

                            case ShapePreset.FlowChartSort:
                            case ShapePreset.FlowChartExtract:
                            case ShapePreset.FlowChartMerge:
                            case ShapePreset.FlowChartOfflineStorage:
                            case ShapePreset.FlowChartOnlineStorage:
                            case ShapePreset.FlowChartMagneticTape:
                            case ShapePreset.FlowChartMagneticDisk:
                            case ShapePreset.FlowChartMagneticDrum:
                            case ShapePreset.FlowChartDisplay:
                            case ShapePreset.FlowChartDelay:
                            case ShapePreset.FlowChartAlternateProcess:
                            case ShapePreset.FlowChartOffpageConnector:
                                goto TR_0000;

                            default:
                                switch (shapePreset)
                                {
                                    case ShapePreset.CornerTabs:
                                    case ShapePreset.SquareTabs:
                                    case ShapePreset.PlaqueTabs:
                                    case ShapePreset.ChartX:
                                    case ShapePreset.ChartStar:
                                    case ShapePreset.ChartPlus:
                                        break;

                                    default:
                                        goto TR_0000;
                                }
                                break;
                        }
                        break;
                }
                goto TR_0001;
            }
            else if ((shapePreset == ShapePreset.Rect) || (shapePreset == ShapePreset.Cube))
            {
                goto TR_0001;
            }
            else
            {
                switch (shapePreset)
                {
                    case ShapePreset.FoldedCorner:
                    case ShapePreset.Bevel:
                    case ShapePreset.Frame:
                    case ShapePreset.HalfFrame:
                    case ShapePreset.Corner:
                    case ShapePreset.BracketPair:
                    case ShapePreset.Callout1:
                    case ShapePreset.Callout2:
                    case ShapePreset.Callout3:
                    case ShapePreset.AccentCallout1:
                    case ShapePreset.AccentCallout2:
                    case ShapePreset.AccentCallout3:
                    case ShapePreset.BorderCallout1:
                    case ShapePreset.BorderCallout2:
                    case ShapePreset.BorderCallout3:
                    case ShapePreset.AccentBorderCallout1:
                    case ShapePreset.AccentBorderCallout2:
                    case ShapePreset.AccentBorderCallout3:
                        goto TR_0001;

                    default:
                        break;
                }
            }
        TR_0000:
            return false;
        TR_0001:
            return true;
        }

        public void Visit(DrawingBlipFill fill)
        {
            if ((fill.Blip != null) && (fill.Blip.Image != null))
            {
                this.HasPermanentFill = !fill.RotateWithShape;
                TextureBrush brush = (fill.Blip.Image.NativeImage is Metafile) ? this.BlipFillFromMetafile(fill) : this.BlipFillFromBitmap(fill);
                if (brush != null)
                {
                    this.ApplyTransformForTextureBrush(brush);
                    this.Result = brush;
                }
            }
        }

        public void Visit(DrawingFill fill)
        {
            this.Result = ((fill.FillType == DrawingFillType.None) || (this.DefaultBrush == null)) ? null : ((Brush) this.DefaultBrush.Clone());
        }

        public void Visit(DrawingGradientFill fill)
        {
            int count = fill.GradientStops.Count;
            if (count != 0)
            {
                List<DrawingGradientStop> list = PrepareGradientStops(fill);
                bool flag = list[0].Position > 0;
                bool flag2 = list[count - 1].Position < 0x186a0;
                int num2 = count;
                if (flag)
                {
                    num2++;
                }
                if (flag2)
                {
                    num2++;
                }
                int index = 0;
                ColorBlend colorBlend = new ColorBlend(num2);
                if (flag)
                {
                    colorBlend.Colors[index] = this.GetColorFromStyle(list[0].Color);
                    colorBlend.Positions[index] = 0f;
                    index++;
                }
                for (int i = 0; i < count; i++)
                {
                    colorBlend.Colors[index] = this.GetColorFromStyle(list[i].Color);
                    colorBlend.Positions[index] = (float) DrawingValueConverter.FromPercentage(list[i].Position);
                    index++;
                }
                if (flag2)
                {
                    colorBlend.Colors[index] = this.GetColorFromStyle(list[count - 1].Color);
                    colorBlend.Positions[index] = 1f;
                }
                this.HasPermanentFill = !fill.RotateWithShape;
                GraphicsPath path = GdipExtensions.BuildFigure(this.graphicsPaths, this.OutlineFill);
                if (this.FigureTransform != null)
                {
                    this.FigureTransform(path);
                }
                Rectangle boundsExt = path.GetBoundsExt(this.HasPermanentFill ? this.ShapeTransform : null);
                Rectangle rectOffset = this.GetRectOffset(boundsExt, this.GetTileRect(fill));
                if ((rectOffset.Width != 0) && (rectOffset.Height != 0))
                {
                    Point centerPoint = RectangleUtils.CenterPoint(this.GetRectOffset(boundsExt, fill.FillRect));
                    switch (CorrectShapeGradientType(fill.GradientType, this.shapeType, this.OutlineFill))
                    {
                        case GradientType.Linear:
                            this.SetupLinearGradientBrush(fill, rectOffset, colorBlend);
                            break;

                        case GradientType.Rectangle:
                            this.SetupRectangularGradientBrush(rectOffset, colorBlend, centerPoint);
                            break;

                        case GradientType.Circle:
                            this.SetupRadialGradientBrush(rectOffset, colorBlend, centerPoint);
                            break;

                        case GradientType.Shape:
                            this.SetupShapeGradientBrush(path, colorBlend, centerPoint);
                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    path.Dispose();
                }
            }
        }

        public void Visit(DrawingPatternFill fill)
        {
            HatchBrush brush = new HatchBrush(HatchStyleTable[fill.PatternType], this.GetColorFromStyle(fill.ForegroundColor), this.GetColorFromStyle(fill.BackgroundColor));
            this.Result = brush;
        }

        public void Visit(DrawingSolidFill fill)
        {
            Color colorFromStyle = this.GetColorFromStyle(fill.Color);
            this.Result = new SolidBrush(colorFromStyle);
        }

        private static Dictionary<DrawingPatternType, HatchStyle> HatchStyleTable =>
            hatchStyleTable ??= CreateHatchStyleTable();

        protected Matrix ShapeTransform { get; private set; }

        public bool OutlineFill { get; set; }

        public Action<GraphicsPath> FigureTransform { get; set; }

        public float ScaleFactor { get; set; }

        private bool StyleColorEmpty =>
            this.styleColor == Color.Empty;

        public Brush Result { get; private set; }

        public bool HasPermanentFill { get; private set; }

        private Brush DefaultBrush { get; set; }

        private class DrawingGradientStopComparer : IComparer<DrawingGradientStop>
        {
            public int Compare(DrawingGradientStop x, DrawingGradientStop y) => 
                (x.Position <= y.Position) ? ((x.Position >= y.Position) ? 0 : -1) : 1;
        }
    }
}

