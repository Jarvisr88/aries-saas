namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Extensions;
    using DevExpress.Office.Model;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;

    public class WPFShapeFillRenderVisitor : IDrawingFillVisitor
    {
        private static HashSet<ShapePreset> shapeTypesUsingOriginalBounds = CreateShapeTypesUsingOriginalBounds();
        private System.Windows.Media.Color styleColor;
        private System.Windows.Media.Color? defaultColor;
        private Rect bounds;
        private ShapePreset shapeType;
        private float shapeRotationAngle;
        private PathGeometry geometry;

        public WPFShapeFillRenderVisitor(System.Windows.Media.Color styleColor, System.Windows.Media.Color? defaultColor, Rect bounds, ShapePreset shapeType, float shapeRotationAngle)
        {
            this.styleColor = styleColor;
            this.defaultColor = defaultColor;
            this.bounds = bounds;
            this.shapeType = shapeType;
            this.shapeRotationAngle = shapeRotationAngle;
        }

        public WPFShapeFillRenderVisitor(System.Windows.Media.Color styleColor, System.Windows.Media.Color? defaultColor, Rect bounds, ShapePreset shapeType, float shapeRotationAngle, PathGeometry geometry) : this(styleColor, defaultColor, bounds, shapeType, shapeRotationAngle)
        {
            this.geometry = geometry;
        }

        private System.Windows.Point CalculateAlignmentOffset(RectangleAlignType alignment, double width, double height, double imageWidth, double imageHeight)
        {
            double x = 0.0;
            double y = 0.0;
            switch (alignment)
            {
                case RectangleAlignType.Top:
                    x = this.CalculateCenterOffset(width, imageWidth);
                    break;

                case RectangleAlignType.TopRight:
                    x = this.CalculateFarOffset(width, imageWidth);
                    break;

                case RectangleAlignType.Left:
                    y = this.CalculateCenterOffset(height, imageHeight);
                    break;

                case RectangleAlignType.Center:
                    x = this.CalculateCenterOffset(width, imageWidth);
                    y = this.CalculateCenterOffset(height, imageHeight);
                    break;

                case RectangleAlignType.Right:
                    x = this.CalculateFarOffset(width, imageWidth);
                    y = this.CalculateCenterOffset(height, imageHeight);
                    break;

                case RectangleAlignType.BottomLeft:
                    y = this.CalculateFarOffset(height, imageHeight);
                    break;

                case RectangleAlignType.Bottom:
                    x = this.CalculateCenterOffset(width, imageWidth);
                    y = this.CalculateFarOffset(height, imageHeight);
                    break;

                case RectangleAlignType.BottomRight:
                    x = this.CalculateFarOffset(width, imageWidth);
                    y = this.CalculateFarOffset(height, imageHeight);
                    break;

                default:
                    break;
            }
            return new System.Windows.Point(x, y);
        }

        private double CalculateCenterOffset(double boundsSize, double imageSize) => 
            (boundsSize / 2.0) - (imageSize / 2.0);

        private System.Windows.Point CalculateCenterPoint(GradientTileInfo tileInfo, Rect bounds)
        {
            System.Windows.Point point = new System.Windows.Point(bounds.Width / 2.0, bounds.Height / 2.0);
            System.Windows.Point center = tileInfo.Center;
            return new System.Windows.Point((0.5 + (center.X / tileInfo.Width)) - (point.X / bounds.Width), (0.5 + (center.Y / tileInfo.Height)) - (point.Y / bounds.Height));
        }

        private double CalculateFarOffset(double boundsSize, double imageSize) => 
            boundsSize - imageSize;

        private System.Windows.Point[] CalculateGradientPoints(Rect bounds, GradientTileInfo tileInfo, double gradientAngle, bool scaled)
        {
            double width = tileInfo.Width;
            double height = tileInfo.Height;
            if (scaled & !(width == height))
            {
                gradientAngle = this.ScaleAngle(gradientAngle, width, height);
            }
            bool flag = (gradientAngle % 180.0) <= 90.0;
            double num3 = DrawingValueConverter.RadianToDegree(Math.Atan2(height, width));
            double degree = flag ? ((gradientAngle + 90.0) - num3) : ((gradientAngle - 90.0) + num3);
            double num5 = tileInfo.Radius * Math.Sin(DrawingValueConverter.DegreeToRadian(degree));
            degree = flag ? gradientAngle : (180.0 - gradientAngle);
            double num6 = (num5 / width) * Math.Cos(DrawingValueConverter.DegreeToRadian(degree));
            double dy = (num5 / height) * Math.Sin(DrawingValueConverter.DegreeToRadian(degree));
            System.Windows.Point center = this.CalculateCenterPoint(tileInfo, bounds);
            double scaleX = width / bounds.Width;
            double scaleY = height / bounds.Height;
            System.Windows.Point point2 = this.CreateGradientPoint(center, flag ? -num6 : num6, -dy, scaleX, scaleY);
            System.Windows.Point point3 = this.CreateGradientPoint(center, flag ? num6 : -num6, dy, scaleX, scaleY);
            if (gradientAngle >= 180.0)
            {
                return new System.Windows.Point[] { point3, point2 };
            }
            return new System.Windows.Point[] { point2, point3 };
        }

        private Rect ChooseBoundsForShapeGradients(ShapePreset shapeType) => 
            ((this.geometry == null) || shapeTypesUsingOriginalBounds.Contains(shapeType)) ? this.bounds : this.geometry.Bounds;

        private TransformGroup CreateBlipFillTransform(DrawingBlipFill fill, double imageWidth, double imageHeight)
        {
            TransformGroup group = new TransformGroup();
            float num = (float) DrawingValueConverter.FromPercentage(fill.ScaleX);
            float num2 = (float) DrawingValueConverter.FromPercentage(fill.ScaleY);
            if ((num != 1f) || (num2 != 1f))
            {
                group.Children.Add(new ScaleTransform((double) num, (double) num2));
            }
            double num5 = imageWidth * num;
            RectangleAlignType tileAlign = fill.TileAlign;
            System.Windows.Point point = this.CalculateAlignmentOffset(tileAlign, this.bounds.Width, this.bounds.Height, num5, imageHeight * num2);
            double totalOffsetX = fill.GetOffsetXInPixels() + point.X;
            double totalOffsetY = fill.GetOffsetYInPixels() + point.Y;
            if (!fill.RotateWithShape)
            {
                group.Children.Add(this.CreateNotRotatedTranslateTransform(tileAlign, totalOffsetX, totalOffsetY));
            }
            else if ((totalOffsetX != 0.0) || (totalOffsetY != 0.0))
            {
                group.Children.Add(new TranslateTransform(totalOffsetX, totalOffsetY));
            }
            return group;
        }

        private System.Windows.Media.Brush CreateGradientBrush(DrawingGradientFill fill, System.Windows.Media.GradientStopCollection stops)
        {
            GradientType gradientType = fill.GradientType;
            Rect bounds = this.bounds;
            if ((this.geometry != null) && ((this.shapeType == ShapePreset.BlockArc) || (this.shapeType == ShapePreset.CircularArrow)))
            {
                bounds = this.geometry.Bounds;
            }
            switch (ShapeFillRenderVisitor.CorrectShapeGradientType(fill.GradientType, this.shapeType, this.OutlineFill))
            {
                case GradientType.Linear:
                    return this.CreateLinearGradientBrush(bounds, fill, stops);

                case GradientType.Rectangle:
                    return new RectangleGradientBrushCalculator(bounds, fill, stops, this.shapeType).CreateBrush();

                case GradientType.Circle:
                    return this.CreateRadialGradientBrush(bounds, fill, stops);

                case GradientType.Shape:
                    if (this.geometry == null)
                    {
                        break;
                    }
                    bounds = this.ChooseBoundsForShapeGradients(this.shapeType);
                    return new PathGradientBrushCalculator(this.geometry, bounds, fill, stops, this.shapeType).CreateBrush();

                default:
                    break;
            }
            return null;
        }

        private System.Windows.Point CreateGradientPoint(System.Windows.Point center, double dx, double dy, double scaleX, double scaleY) => 
            new System.Windows.Point((center.X + dx) * scaleX, (center.Y + dy) * scaleY);

        private System.Windows.Media.Brush CreateLinearGradientBrush(Rect bounds, DrawingGradientFill fill, System.Windows.Media.GradientStopCollection stops)
        {
            bounds.Offset(-bounds.X, -bounds.Y);
            GradientTileInfo tileInfo = new GradientTileInfo(bounds, fill.TileRect);
            if ((tileInfo.Width == 0.0) || (tileInfo.Height == 0.0))
            {
                return new SolidColorBrush(stops[0].Color);
            }
            LinearGradientBrush brush = new LinearGradientBrush {
                SpreadMethod = GradientSpreadMethod.Reflect
            };
            System.Windows.Point[] pointArray = this.CalculateGradientPoints(bounds, tileInfo, (double) (fill.DocumentModel.UnitConverter.ModelUnitsToDegreeF(fill.Angle) % 360f), fill.Scaled);
            brush.StartPoint = pointArray[0];
            brush.EndPoint = pointArray[1];
            brush.GradientStops = stops;
            return brush;
        }

        private Transform CreateNotRotatedScaleTransform()
        {
            System.Windows.Point point = new System.Windows.Point(this.bounds.Width / 2.0, this.bounds.Height / 2.0);
            Rect rect = new RotateTransform((double) this.shapeRotationAngle, point.X, point.Y).TransformBounds(this.bounds);
            return new TransformGroup { Children = { 
                new ScaleTransform(rect.Width / this.bounds.Width, rect.Height / this.bounds.Height, point.X, point.Y),
                new RotateTransform((double) -this.shapeRotationAngle, point.X, point.Y)
            } };
        }

        private System.Windows.Point CreateNotRotatedTransformOffset(Rect boundedRect, RectangleAlignType alignment)
        {
            double x = 0.0;
            double y = 0.0;
            switch (alignment)
            {
                case RectangleAlignType.TopLeft:
                    x = boundedRect.X;
                    y = boundedRect.Y;
                    break;

                case RectangleAlignType.Top:
                    y = boundedRect.Y;
                    break;

                case RectangleAlignType.TopRight:
                    x = -boundedRect.X;
                    y = boundedRect.Y;
                    break;

                case RectangleAlignType.Left:
                    x = boundedRect.X;
                    break;

                case RectangleAlignType.Right:
                    x = -boundedRect.X;
                    break;

                case RectangleAlignType.BottomLeft:
                    x = boundedRect.X;
                    y = -boundedRect.Y;
                    break;

                case RectangleAlignType.Bottom:
                    y = -boundedRect.Y;
                    break;

                case RectangleAlignType.BottomRight:
                    x = -boundedRect.X;
                    y = -boundedRect.Y;
                    break;

                default:
                    break;
            }
            return new System.Windows.Point(x, y);
        }

        private Transform CreateNotRotatedTranslateTransform(RectangleAlignType alignment, double totalOffsetX, double totalOffsetY)
        {
            System.Windows.Point point = new System.Windows.Point(this.bounds.Width / 2.0, this.bounds.Height / 2.0);
            Rect boundedRect = new RotateTransform((double) this.shapeRotationAngle, point.X, point.Y).TransformBounds(this.bounds);
            System.Windows.Point point2 = this.CreateNotRotatedTransformOffset(boundedRect, alignment);
            totalOffsetX += point2.X;
            totalOffsetY += point2.Y;
            TransformGroup group = new TransformGroup();
            if ((totalOffsetX != 0.0) || (totalOffsetY != 0.0))
            {
                group.Children.Add(new TranslateTransform(totalOffsetX, totalOffsetY));
            }
            group.Children.Add(new RotateTransform((double) -this.shapeRotationAngle, point.X, point.Y));
            return group;
        }

        private System.Windows.Media.Brush CreateRadialGradientBrush(Rect bounds, DrawingGradientFill fill, System.Windows.Media.GradientStopCollection stops)
        {
            GradientTileInfo info = new GradientTileInfo(bounds, fill.TileRect);
            double height = info.Height;
            if ((info.Width == 0.0) && (height == 0.0))
            {
                return new SolidColorBrush(stops[0].Color);
            }
            RadialGradientBrush brush = new RadialGradientBrush {
                MappingMode = BrushMappingMode.Absolute,
                RadiusX = info.Radius,
                RadiusY = info.Radius,
                Center = info.Center
            };
            GradientTileInfo info2 = new GradientTileInfo(bounds, fill.FillRect);
            brush.GradientOrigin = info2.Center;
            brush.GradientStops = stops;
            return brush;
        }

        private static HashSet<ShapePreset> CreateShapeTypesUsingOriginalBounds() => 
            new HashSet<ShapePreset> { 
                ShapePreset.MathPlus,
                ShapePreset.MathMinus,
                ShapePreset.MathMultiply,
                ShapePreset.MathEqual,
                ShapePreset.Gear6
            };

        private TransformGroup CreateStretchedBlipFillTransform(DrawingBlipFill fill)
        {
            TransformGroup group = new TransformGroup();
            RectangleOffset fillRectangle = fill.FillRectangle;
            double width = this.bounds.Width;
            double height = this.bounds.Height;
            double offsetX = width * DrawingValueConverter.FromPercentage(fillRectangle.LeftOffset);
            double offsetY = height * DrawingValueConverter.FromPercentage(fillRectangle.TopOffset);
            double num5 = width * DrawingValueConverter.FromPercentage(fillRectangle.RightOffset);
            double num6 = height * DrawingValueConverter.FromPercentage(fillRectangle.BottomOffset);
            double num7 = (width - offsetX) - num5;
            double num8 = (height - offsetY) - num6;
            if ((num7 != width) || (num8 != height))
            {
                group.Children.Add(new ScaleTransform(num7 / width, num8 / height));
            }
            if ((offsetX != 0.0) || (offsetY != 0.0))
            {
                group.Children.Add(new TranslateTransform(offsetX, offsetY));
            }
            if (!fill.RotateWithShape)
            {
                group.Children.Add(this.CreateNotRotatedScaleTransform());
            }
            return group;
        }

        void IDrawingFillVisitor.Visit(DrawingBlipFill fill)
        {
            DrawingBlip blip = fill.Blip;
            if ((blip != null) && (blip.Image != null))
            {
                float num = ShapeFillAttributeCalculator.CalculateBlipAlphaValue(blip);
                if (num != 0f)
                {
                    TransformGroup group;
                    Image image = (fill.BlackAndWhitePrintMode || this.GroupShapeBlackAndWhitePrintMode) ? this.GetGrayScaleImage(blip.Image.NativeImage) : blip.Image.NativeImage;
                    ImageBrush brush = new ImageBrush {
                        ImageSource = image.ToImageSource()
                    };
                    if (fill.Stretch)
                    {
                        group = this.CreateStretchedBlipFillTransform(fill);
                    }
                    else
                    {
                        brush.Stretch = Stretch.None;
                        brush.TileMode = this.GetBlipTileMode(fill.TileFlip);
                        brush.ViewportUnits = BrushMappingMode.Absolute;
                        double width = Math.Floor((double) ((image.Width * 96f) / image.HorizontalResolution));
                        double height = Math.Floor((double) ((image.Height * 96f) / image.VerticalResolution));
                        brush.Viewport = new Rect(0.0, 0.0, width, height);
                        group = this.CreateBlipFillTransform(fill, width, height);
                    }
                    if (group.Children.Count > 0)
                    {
                        brush.Transform = group;
                    }
                    brush.Opacity = num;
                    brush.Freeze();
                    this.Result = brush;
                }
            }
        }

        void IDrawingFillVisitor.Visit(DrawingFill fill)
        {
            if (((fill.FillType != DrawingFillType.None) && (this.defaultColor != null)) && (this.defaultColor.Value.A > 0))
            {
                this.Result = new SolidColorBrush(this.defaultColor.Value);
            }
        }

        void IDrawingFillVisitor.Visit(DrawingGradientFill fill)
        {
            System.Windows.Media.GradientStopCollection stops = this.PrepareGradientStops(fill.GradientStops);
            if ((stops != null) && (stops.Count != 0))
            {
                System.Windows.Media.Brush brush = this.CreateGradientBrush(fill, stops);
                if (brush != null)
                {
                    if (!fill.RotateWithShape)
                    {
                        brush.Transform = this.CreateNotRotatedScaleTransform();
                    }
                    if (brush.CanFreeze)
                    {
                        brush.Freeze();
                    }
                    this.Result = brush;
                }
            }
        }

        void IDrawingFillVisitor.Visit(DrawingPatternFill fill)
        {
            this.Result = WpfPatternFillProvider.GetPatternFillBrush(fill.PatternType, this.GetColorFromStyle(fill.ForegroundColor), this.GetColorFromStyle(fill.BackgroundColor));
        }

        void IDrawingFillVisitor.Visit(DrawingSolidFill fill)
        {
            System.Windows.Media.Color colorFromStyle = this.GetColorFromStyle(fill.Color);
            if (colorFromStyle.A > 0)
            {
                this.Result = new SolidColorBrush(colorFromStyle);
            }
        }

        private TileMode GetBlipTileMode(TileFlipType flip)
        {
            switch (flip)
            {
                case TileFlipType.Horizontal:
                    return TileMode.FlipX;

                case TileFlipType.Vertical:
                    return TileMode.FlipY;

                case TileFlipType.Both:
                    return TileMode.FlipXY;
            }
            return TileMode.Tile;
        }

        private System.Windows.Media.Color GetColorFromStyle(DrawingColor drawingColor)
        {
            if ((((this.styleColor.A + this.styleColor.R) + this.styleColor.G) + this.styleColor.B) == 0)
            {
                System.Drawing.Color finalColor = drawingColor.FinalColor;
                return System.Windows.Media.Color.FromArgb(finalColor.A, finalColor.R, finalColor.G, finalColor.B);
            }
            System.Drawing.Color color = System.Drawing.Color.FromArgb(this.styleColor.A, this.styleColor.R, this.styleColor.G, this.styleColor.B);
            DrawingColor color2 = DrawingColor.Create(FakeDocumentModel.Instance, color);
            color2.CopyFrom(drawingColor);
            color2.Info.SchemeColor = SchemeColorValues.Style;
            System.Drawing.Color color3 = color2.ToRgb(color);
            return System.Windows.Media.Color.FromArgb(color3.A, color3.R, color3.G, color3.B);
        }

        private Bitmap GetGrayScaleImage(Image original)
        {
            Bitmap image = new Bitmap(original.Width, original.Height);
            using (Graphics graphics = Graphics.FromImage(image))
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
                ImageAttributes imageAttr = new ImageAttributes();
                imageAttr.SetColorMatrix(new ColorMatrix(newColorMatrix));
                graphics.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, imageAttr);
            }
            return image;
        }

        private System.Windows.Media.GradientStopCollection PrepareGradientStops(DevExpress.Office.Drawing.GradientStopCollection modelStops)
        {
            if (modelStops == null)
            {
                return null;
            }
            System.Windows.Media.GradientStopCollection stops = new System.Windows.Media.GradientStopCollection();
            bool flag = false;
            foreach (DrawingGradientStop stop in modelStops)
            {
                System.Windows.Media.Color colorFromStyle = this.GetColorFromStyle(stop.Color);
                if (colorFromStyle.A > 0)
                {
                    flag = true;
                }
                stops.Add(new GradientStop(colorFromStyle, DrawingValueConverter.FromPercentage(stop.Position)));
            }
            return (flag ? stops : null);
        }

        private double ScaleAngle(double angle, double width, double height)
        {
            int num = 0;
            while (angle < 0.0)
            {
                angle += 90.0;
                num--;
            }
            while (angle > 90.0)
            {
                angle -= 90.0;
                num++;
            }
            double num2 = Math.Min(width, height);
            double num3 = num2 / width;
            double num4 = num2 / height;
            return ((DrawingValueConverter.RadianToDegree(Math.Atan(Math.Tan(DrawingValueConverter.DegreeToRadian(angle)) * (((num % 2) == 0) ? (num4 / num3) : (num3 / num4)))) + (num * 90)) % 360.0);
        }

        public System.Windows.Media.Brush Result { get; private set; }

        public bool OutlineFill { get; set; }

        public bool GroupShapeBlackAndWhitePrintMode { get; set; }

        [StructLayout(LayoutKind.Sequential)]
        private struct GradientTileInfo
        {
            public GradientTileInfo(Rect originalBounds, RectangleOffset offset)
            {
                this = new WPFShapeFillRenderVisitor.GradientTileInfo();
                double num = Math.Round((double) (originalBounds.Width * DrawingValueConverter.FromPercentage(offset.LeftOffset)));
                double num2 = Math.Round((double) (originalBounds.Height * DrawingValueConverter.FromPercentage(offset.TopOffset)));
                double num3 = Math.Round((double) (originalBounds.Width * (1.0 - DrawingValueConverter.FromPercentage(offset.RightOffset))));
                double num4 = Math.Round((double) (originalBounds.Height * (1.0 - DrawingValueConverter.FromPercentage(offset.BottomOffset))));
                this.Width = num3 - num;
                this.Height = num4 - num2;
                this.Center = new System.Windows.Point(originalBounds.X + ((num3 + num) / 2.0), originalBounds.Y + ((num4 + num2) / 2.0));
                this.Radius = Math.Sqrt((this.Width * this.Width) + (this.Height * this.Height)) / 2.0;
            }

            public double Width { get; private set; }
            public double Height { get; private set; }
            public System.Windows.Point Center { get; private set; }
            public double Radius { get; private set; }
        }
    }
}

