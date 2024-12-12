namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.DrawingML;
    using DevExpress.Office.Layout;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    public static class ShapeRenderHelper
    {
        private static Dictionary<OutlineHeadTailType, LineCapInfoCreatorBase> lineCapCreators = CreateLineCapCreators();

        private static void ApplyCamera(GraphicsPath graphicsPath, Scene3DProperties scene3D, Rectangle bounds)
        {
            double num;
            double num2;
            double num3;
            IScene3DCamera camera = scene3D.Camera;
            if (camera.HasRotation)
            {
                num = DrawingValueConverter.FromPositiveFixedAngle(camera.Lon) % 360.0;
                num2 = DrawingValueConverter.FromPositiveFixedAngle(camera.Lat) % 360.0;
                num3 = DrawingValueConverter.FromPositiveFixedAngle(camera.Rev) % 360.0;
            }
            else
            {
                double[] cameraRotationByPreset = GetCameraRotationByPreset(camera.Preset);
                num = cameraRotationByPreset[0];
                num2 = cameraRotationByPreset[1];
                num3 = cameraRotationByPreset[2];
            }
            ApplyCameraCore(graphicsPath, bounds, num, num2, num3);
        }

        private static void ApplyCameraCore(GraphicsPath graphicsPath, Rectangle bounds, double longitude, double latitude, double revolution)
        {
            PointF tf = (PointF) RectangleUtils.CenterPoint(bounds);
            double num = (((double) bounds.Width) / 2.0) * Math.Cos(DrawingValueConverter.DegreeToRadian(longitude));
            double num2 = (((double) bounds.Width) / 2.0) * Math.Sin(DrawingValueConverter.DegreeToRadian(longitude));
            Rectangle rectangle = new Rectangle(0, bounds.Y, (int) Math.Round((double) (num2 * 2.0)), bounds.Height);
            Point point = new Point(rectangle.Left, rectangle.Top);
            Point point2 = new Point(rectangle.Right, rectangle.Top);
            Point point3 = new Point(rectangle.Left, rectangle.Bottom);
            Point point4 = new Point(rectangle.Right, rectangle.Bottom);
            if (latitude != 0.0)
            {
                Matrix matrix2 = TransformMatrixExtensions.CreateTransformUnsafe((float) latitude, rectangle);
                point = matrix2.TransformPoint(point);
                point2 = matrix2.TransformPoint(point2);
                point3 = matrix2.TransformPoint(point3);
                point4 = matrix2.TransformPoint(point4);
                matrix2.Dispose();
            }
            PointF[] points = new PointF[] { new PointF(tf.X - ((float) num), (float) point.Y), new PointF(tf.X + ((float) num), (float) point2.Y), new PointF(tf.X - ((float) num), (float) point3.Y), new PointF(tf.X + ((float) num), (float) point4.Y) };
            Matrix matrix = (revolution != 0.0) ? TransformMatrixExtensions.CreateTransformUnsafe((float) -revolution, Rectangle.Round(RectangleUtils.BoundingRectangle(points))) : null;
            graphicsPath.Warp(points, bounds, matrix);
            if (matrix != null)
            {
                matrix.Dispose();
            }
        }

        public static void ApplyEffects(ShapeProperties shapeProperties, ShapeStyle shapeStyle, ShapeLayoutInfo shapeLayoutInfo)
        {
            IDocumentModel documentModel = shapeProperties.DocumentModel;
            DrawingEffectCollection effects = null;
            ContainerEffect containerEffect = shapeProperties.EffectStyle.ContainerEffect;
            if (containerEffect.ApplyEffectList)
            {
                effects = containerEffect.Effects;
            }
            else
            {
                DrawingEffectStyle effectStyle = documentModel.OfficeTheme.FormatScheme.GetEffectStyle(shapeStyle.EffectReferenceIdx);
                if ((effectStyle != null) && effectStyle.ContainerEffect.ApplyEffectList)
                {
                    effects = effectStyle.ContainerEffect.Effects;
                }
            }
            if (effects != null)
            {
                new ShapeEffectRenderingWalker(effects, documentModel, shapeLayoutInfo, shapeProperties.BlackAndWhitePrintMode).ApplyEffects();
            }
        }

        private static void ApplyScene3DProperties(List<GraphicsPath> graphicsPaths, Scene3DProperties scene3D)
        {
            if (!scene3D.IsDefault && ((graphicsPaths != null) && (graphicsPaths.Count != 0)))
            {
                GraphicsPath graphicsPath = new GraphicsPath(FillMode.Alternate);
                foreach (GraphicsPath path2 in graphicsPaths)
                {
                    graphicsPath.AddPath(path2, false);
                }
                Rectangle boundsExt = graphicsPath.GetBoundsExt();
                graphicsPath.Dispose();
                foreach (GraphicsPath path3 in graphicsPaths)
                {
                    ApplyCamera(path3, scene3D, boundsExt);
                }
            }
        }

        public static ConnectionShapePathInfo[] CalculateConnectionShapePathsInfo(ModelShapeCustomGeometry geometry, List<GraphicsPath> graphicsPaths, float scaleFactor)
        {
            int count = geometry.Paths.Count;
            ConnectionShapePathInfo[] infoArray = new ConnectionShapePathInfo[count];
            for (int i = 0; i < count; i++)
            {
                GraphicsPath graphicsPath = graphicsPaths[i];
                infoArray[i] = new ConnectionShapePathInfo(graphicsPath, scaleFactor);
            }
            return infoArray;
        }

        private static GraphicsPath CalculateGraphicsPath(ModelShapePath path, Rectangle bounds, float defaultScale)
        {
            try
            {
                GeometryPathToGraphicsPathConverter converter = new GeometryPathToGraphicsPathConverter(path, bounds, defaultScale);
                converter.Walk();
                return converter.GraphicsPath;
            }
            catch
            {
                return new GraphicsPath();
            }
        }

        public static List<GraphicsPath> CalculateGraphicsPaths(ModelShapeCustomGeometry geometry, Rectangle bounds, float defaultScale)
        {
            List<GraphicsPath> list = new List<GraphicsPath>();
            foreach (ModelShapePath path in geometry.Paths)
            {
                list.Add(CalculateGraphicsPath(path, bounds, defaultScale));
            }
            return list;
        }

        private static PathInfo CalculatePathInfo(GraphicsPath graphicsPath, ModelShapePath path, Brush defaultBrush, bool hasPermanentFill, bool blackAndWhitePrintMode)
        {
            try
            {
                return new PathInfo(graphicsPath, !blackAndWhitePrintMode ? GetPathFillBrush(path.FillMode, defaultBrush) : new SolidBrush(Color.White), path.Stroke, hasPermanentFill);
            }
            catch
            {
                return new PathInfo(new GraphicsPath(), null, false);
            }
        }

        public static PathInfo[] CalculatePathsInfo(ModelShapeCustomGeometry geometry, List<GraphicsPath> graphicsPaths, Brush defaultBrush, bool hasPermanentFill) => 
            CalculatePathsInfo(geometry, graphicsPaths, defaultBrush, hasPermanentFill, false);

        public static PathInfo[] CalculatePathsInfo(ModelShapeCustomGeometry geometry, List<GraphicsPath> graphicsPaths, Brush defaultBrush, bool hasPermanentFill, bool blackAndWhitePrintMode)
        {
            PathInfo[] infoArray = new PathInfo[geometry.Paths.Count];
            for (int i = 0; i < geometry.Paths.Count; i++)
            {
                ModelShapePath path = geometry.Paths[i];
                GraphicsPath graphicsPath = graphicsPaths[i];
                infoArray[i] = CalculatePathInfo(graphicsPath, path, defaultBrush, hasPermanentFill, blackAndWhitePrintMode);
            }
            return infoArray;
        }

        public static ShapeLayoutInfo CalculateShapeLayoutInfo(ShapeProperties shapeProperties, ShapeStyle shapeStyle, ModelShapeCustomGeometry geometry, Rectangle bounds, bool preferBitmap) => 
            CalculateShapeLayoutInfo(shapeProperties, shapeStyle, geometry, bounds, null, preferBitmap);

        public static ShapeLayoutInfo CalculateShapeLayoutInfo(ShapeProperties shapeProperties, ShapeStyle shapeStyle, ModelShapeCustomGeometry geometry, Rectangle bounds, Matrix parentTransform, bool preferBitmap)
        {
            ShapeLayoutInfo shapeLayoutInfo = ShapeLayoutInfo.GetShapeLayoutInfo(true, shapeProperties.IsConnectionShape());
            FillShapeLayoutInfo(shapeLayoutInfo, shapeProperties, shapeStyle, geometry, bounds, parentTransform, preferBitmap);
            return shapeLayoutInfo;
        }

        public static RectangleF CalculateTextRect(AdjustableRect shapeTextRectangle, Rectangle bounds, IDocumentModel documentModel) => 
            CalculateTextRect(shapeTextRectangle, bounds, documentModel, false, false);

        public static RectangleF CalculateTextRect(AdjustableRect shapeTextRectangle, Rectangle bounds, IDocumentModel documentModel, bool flipH, bool flipV)
        {
            if ((shapeTextRectangle == null) || shapeTextRectangle.IsEmpty())
            {
                return bounds;
            }
            DocumentModelUnitToLayoutUnitConverter toDocumentLayoutUnitConverter = documentModel.ToDocumentLayoutUnitConverter;
            DocumentModelUnitConverter unitConverter = documentModel.UnitConverter;
            float num = toDocumentLayoutUnitConverter.ToLayoutUnits((float) unitConverter.EmuToModelUnitsL((long) shapeTextRectangle.Top.ValueEMU));
            float a = toDocumentLayoutUnitConverter.ToLayoutUnits((float) unitConverter.EmuToModelUnitsL((long) shapeTextRectangle.Left.ValueEMU));
            float num3 = toDocumentLayoutUnitConverter.ToLayoutUnits((float) unitConverter.EmuToModelUnitsL((long) shapeTextRectangle.Bottom.ValueEMU));
            float b = toDocumentLayoutUnitConverter.ToLayoutUnits((float) unitConverter.EmuToModelUnitsL((long) shapeTextRectangle.Right.ValueEMU));
            if ((flipH | flipV) && !(flipH & flipV))
            {
                SwapTwoFloats(ref a, ref b);
                a = bounds.Width - a;
                b = bounds.Width - b;
            }
            float num5 = bounds.Top + num;
            float num6 = bounds.Left + a;
            float num7 = bounds.Top + num3;
            float num8 = bounds.Left + b;
            if (num6 > num8)
            {
                SwapTwoFloats(ref num6, ref num8);
            }
            if (num5 > num7)
            {
                SwapTwoFloats(ref num5, ref num7);
            }
            return RectangleF.FromLTRB(num6, num5, num8, num7);
        }

        private static ColorBlend CorrectGradientColors(PathFillMode pathFillMode, ColorBlend interpolationColors)
        {
            ColorBlend blend = new ColorBlend(interpolationColors.Colors.Length);
            Color[] colors = interpolationColors.Colors;
            for (int i = 0; i < colors.Length; i++)
            {
                blend.Colors[i] = GetPathFillColor(colors[i], pathFillMode);
                blend.Positions[i] = interpolationColors.Positions[i];
            }
            return blend;
        }

        public static RectangleF CorrectTextRectangleForUprightText(RectangleF textRectangle, float shapeAngle)
        {
            float num = shapeAngle % 180f;
            if (num < 0f)
            {
                num += 180f;
            }
            if ((num >= 45f) && (num < 135f))
            {
                float num2 = (textRectangle.Height - textRectangle.Width) / 2f;
                textRectangle = new RectangleF(textRectangle.X - num2, textRectangle.Y + num2, textRectangle.Height, textRectangle.Width);
            }
            return textRectangle;
        }

        public static PictureImagePathInfo CreateBitmapPathInfo(DrawingBlipFill fill, ModelShapeCustomGeometry geometry, List<GraphicsPath> graphicsPaths, PenInfo penInfo)
        {
            RectangleF pictureBounds;
            GraphicsPath graphicsPath = GdipExtensions.BuildFigure(graphicsPaths, true);
            Pen pen = penInfo?.Pen;
            RectangleF bounds = graphicsPath.GetBoundsExtF(null, penInfo, true);
            using (GraphicsPath path2 = GdipExtensions.BuildFigure(graphicsPaths, false))
            {
                pictureBounds = path2.GetBoundsExtF(null);
            }
            DocumentLayoutUnitConverter layoutUnitConverter = fill.DocumentModel.LayoutUnitConverter;
            PictureImagePathInfo info = new PictureImagePathInfo(graphicsPath, delegate {
                RectangleF ef = new RectangleF(0f, 0f, layoutUnitConverter.LayoutUnitsToPixelsF(bounds.Width), layoutUnitConverter.LayoutUnitsToPixelsF(bounds.Height));
                Bitmap image = new Bitmap(Math.Max(1, (int) Math.Round((double) ef.Width)), Math.Max(1, (int) Math.Round((double) ef.Height)));
                using (Graphics graphics = GdipExtensions.PrepareGraphicsFromImage(image, (GraphicsUnit) layoutUnitConverter.GraphicsPageUnit, layoutUnitConverter.GraphicsPageScale))
                {
                    graphics.TranslateTransform(-bounds.Left, -bounds.Top, MatrixOrder.Append);
                    SetupImageGraphicsClip(graphics, graphicsPaths, geometry.Paths);
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Bitmap nativeImage = (Bitmap) fill.Blip.Image.NativeImage;
                    RectangleF ef2 = ShapeFillAttributeCalculator.CalculatePictureClipBounds(fill.SourceRectangle, pictureBounds);
                    DrawingBlipEffectWalker walker = new DrawingBlipEffectWalker();
                    walker.Walk(fill.Blip.Effects);
                    ColorMatrix colorMatrix = !fill.BlackAndWhitePrintMode ? (walker.HasColorMatrix ? new ColorMatrix(walker.ColorMatrixElements) : null) : ShapeFillRenderVisitor.GetGrayscaleColorMatrix();
                    DrawImage(graphics, nativeImage, Rectangle.Round(ef2), colorMatrix, walker.GetColorMap());
                    graphics.ResetClip();
                    DrawImageOutline(graphics, pen, geometry, graphicsPaths);
                    return image;
                }
            }, Rectangle.Round(bounds));
            SetupMetafileHitTestCollection(info.HitTestInfos, graphicsPaths, geometry.Paths);
            info.RotateWithShape = true;
            info.Stroke = pen != null;
            return info;
        }

        private static Dictionary<OutlineHeadTailType, LineCapInfoCreatorBase> CreateLineCapCreators() => 
            new Dictionary<OutlineHeadTailType, LineCapInfoCreatorBase> { 
                { 
                    OutlineHeadTailType.Oval,
                    new OvalLineCapInfoCreator()
                },
                { 
                    OutlineHeadTailType.Arrow,
                    new ArrowLineCapInfoCreator()
                },
                { 
                    OutlineHeadTailType.Diamond,
                    new DiamondLineCapInfoCreator()
                },
                { 
                    OutlineHeadTailType.StealthArrow,
                    new StealthArrowLineCapInfoCreator()
                },
                { 
                    OutlineHeadTailType.TriangleArrow,
                    new TriangleArrowLineCapInfoCreator()
                }
            };

        public static PictureImagePathInfo CreateMetafilePathInfo(DrawingBlipFill fill, ModelShapeCustomGeometry geometry, List<GraphicsPath> graphicsPaths, PenInfo penInfo)
        {
            RectangleF pictureBounds;
            GraphicsPath graphicsPath = GdipExtensions.BuildFigure(graphicsPaths, true);
            Pen pen = penInfo?.Pen;
            RectangleF bounds = graphicsPath.GetBoundsExtF(null, penInfo, true);
            using (GraphicsPath path2 = GdipExtensions.BuildFigure(graphicsPaths, false))
            {
                pictureBounds = path2.GetBoundsExtF(null);
            }
            DocumentLayoutUnitConverter layoutUnitConverter = fill.DocumentModel.LayoutUnitConverter;
            PictureImagePathInfo info = new PictureImagePathInfo(graphicsPath, delegate {
                RectangleF frameRect = new RectangleF(0f, 0f, Math.Max(1f, layoutUnitConverter.LayoutUnitsToPixelsF(bounds.Width)), Math.Max(1f, layoutUnitConverter.LayoutUnitsToPixelsF(bounds.Height)));
                Metafile image = MetafileCreator.CreateInstance(frameRect, MetafileFrameUnit.Pixel, EmfType.EmfPlusOnly, true);
                using (Graphics graphics = GdipExtensions.PrepareGraphicsFromImage(image, (GraphicsUnit) layoutUnitConverter.GraphicsPageUnit, layoutUnitConverter.GraphicsPageScale))
                {
                    graphics.TranslateTransform(-bounds.Left, -bounds.Top, MatrixOrder.Append);
                    SetupImageGraphicsClip(graphics, graphicsPaths, geometry.Paths);
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Metafile nativeImage = (Metafile) fill.Blip.Image.NativeImage;
                    RectangleF ef2 = ShapeFillAttributeCalculator.CalculatePictureClipBounds(fill.SourceRectangle, pictureBounds);
                    DrawingBlipEffectWalker walker = new DrawingBlipEffectWalker();
                    walker.Walk(fill.Blip.Effects);
                    ColorMatrix colorMatrix = !fill.BlackAndWhitePrintMode ? (walker.HasColorMatrix ? new ColorMatrix(walker.ColorMatrixElements) : null) : ShapeFillRenderVisitor.GetGrayscaleColorMatrix();
                    bool flag = false;
                    if (!nativeImage.GetMetafileHeader().IsEmfPlus())
                    {
                        Metafile metafile3 = GdipExtensions.ConvertToEmfPlus(graphics, nativeImage);
                        if (metafile3 != null)
                        {
                            DrawImage(graphics, metafile3, Rectangle.Round(ef2), colorMatrix);
                            metafile3.Dispose();
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        DrawImage(graphics, nativeImage, Rectangle.Round(ef2), colorMatrix, walker.GetColorMap());
                    }
                    graphics.ResetClip();
                    DrawImageOutline(graphics, pen, geometry, graphicsPaths);
                }
                return image;
            }, Rectangle.Round(bounds));
            SetupMetafileHitTestCollection(info.HitTestInfos, graphicsPaths, geometry.Paths);
            info.RotateWithShape = true;
            info.Stroke = pen != null;
            return info;
        }

        public static RegularPicturePathInfo CreateRegularBitmapPathInfo(DrawingBlipFill fill, ModelShapeCustomGeometry geometry, List<GraphicsPath> graphicsPaths)
        {
            GraphicsPath graphicsPath = GdipExtensions.BuildFigure(graphicsPaths, false);
            RectangleF boundsExtF = graphicsPath.GetBoundsExtF(null);
            RegularPicturePathInfo info = new RegularPicturePathInfo(graphicsPath, fill, Rectangle.Round(boundsExtF));
            SetupMetafileHitTestCollection(info.HitTestInfos, graphicsPaths, geometry.Paths);
            info.RotateWithShape = true;
            info.Stroke = false;
            return info;
        }

        public static Matrix CreateShapeTransform(ShapeProperties shapeProperties, Rectangle bounds, Matrix parentTransform) => 
            CreateShapeTransform(shapeProperties.Transform2D, bounds, parentTransform);

        public static Matrix CreateShapeTransform(Transform2D transform2D, Rectangle bounds, Matrix parentTransform)
        {
            float angle = transform2D.GetRotationAngleInDegrees() % 360f;
            bool flipH = transform2D.FlipH;
            bool flipV = transform2D.FlipV;
            Matrix matrix = parentTransform?.Clone();
            Matrix matrix2 = TransformMatrixExtensions.CreateFlipTransformUnsafe(bounds, flipH, flipV, angle);
            if (matrix == null)
            {
                matrix = matrix2;
            }
            else if (matrix2 != null)
            {
                matrix.Multiply(matrix2);
                matrix2.Dispose();
            }
            return matrix;
        }

        public static Matrix CreateSpecificTextTransform(Matrix shapeTransform, Rectangle bounds, float angle, bool flipH, bool flipV, bool backwardRotation)
        {
            if (shapeTransform == null)
            {
                return null;
            }
            Matrix matrix = shapeTransform.Clone();
            if (flipH | flipV)
            {
                using (Matrix matrix2 = TransformMatrixExtensions.CreateFlipTransformUnsafe(bounds, flipH, flipV))
                {
                    matrix.Multiply(matrix2);
                }
            }
            if (backwardRotation && (angle != 0f))
            {
                matrix.RotateAt(-angle, (PointF) RectangleUtils.CenterPoint(bounds));
            }
            return matrix;
        }

        public static void Draw(ShapeLayoutInfo shapeLayoutInfo, Graphics graphics)
        {
            foreach (PathInfoBase base2 in shapeLayoutInfo.Paths)
            {
                if (!base2.SkipDrawing)
                {
                    base2.Draw(graphics, shapeLayoutInfo.PenInfo, shapeLayoutInfo.ShapeTransform);
                }
            }
        }

        private static void DrawImage(Graphics graphics, System.Drawing.Image image, Rectangle clipBounds, ColorMatrix colorMatrix)
        {
            DrawImage(graphics, image, clipBounds, colorMatrix, null);
        }

        private static void DrawImage(Graphics graphics, System.Drawing.Image image, Rectangle clipBounds, ColorMatrix colorMatrix, ColorMap[] colorMap)
        {
            RectangleF imageBounds = image.GetImageBounds();
            if ((colorMatrix == null) && (colorMap == null))
            {
                graphics.DrawImage(image, clipBounds);
            }
            else
            {
                using (ImageAttributes attributes = new ImageAttributes())
                {
                    if (colorMatrix != null)
                    {
                        attributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Default);
                    }
                    if (colorMap != null)
                    {
                        attributes.SetRemapTable(colorMap);
                    }
                    graphics.DrawImage(image, clipBounds, imageBounds.X, imageBounds.Y, imageBounds.Width, imageBounds.Height, GraphicsUnit.Pixel, attributes);
                }
            }
        }

        private static void DrawImageOutline(Graphics graphics, Pen pen, ModelShapeCustomGeometry geometry, List<GraphicsPath> graphicsPaths)
        {
            if (pen != null)
            {
                for (int i = 0; i < geometry.Paths.Count; i++)
                {
                    if (geometry.Paths[i].Stroke)
                    {
                        if (pen.Alignment != PenAlignment.Outset)
                        {
                            graphics.DrawPath(pen, graphicsPaths[i]);
                        }
                        else
                        {
                            using (GraphicsPath path = GdipExtensions.TransformGraphicsPathForOutsetPen(graphicsPaths[i], pen))
                            {
                                graphics.DrawPath(pen, path);
                            }
                        }
                    }
                }
            }
        }

        public static void FillShapeLayoutInfo(ShapeLayoutInfo shapeLayoutInfo, ShapeProperties shapeProperties, ShapeStyle shapeStyle, ModelShapeCustomGeometry geometry, Rectangle bounds, Matrix parentTransform, bool preferBitmap)
        {
            FillShapeLayoutInfo(shapeLayoutInfo, shapeProperties, shapeStyle, geometry, bounds, parentTransform, null, preferBitmap);
        }

        public static void FillShapeLayoutInfo(ShapeLayoutInfo shapeLayoutInfo, ShapeProperties shapeProperties, ShapeStyle shapeStyle, ModelShapeCustomGeometry geometry, Rectangle bounds, Matrix parentTransform, DrawingPathInfo textPathInfo, bool preferBitmap)
        {
            FillShapeLayoutInfoWithoutEffects(shapeLayoutInfo, shapeProperties, shapeStyle, geometry, bounds, parentTransform, textPathInfo, preferBitmap);
            ApplyEffects(shapeProperties, shapeStyle, shapeLayoutInfo);
        }

        public static void FillShapeLayoutInfo(ShapeLayoutInfo shapeLayoutInfo, ShapeProperties shapeProperties, ShapeStyle shapeStyle, Transform2D actualTransform2D, ModelShapeCustomGeometry geometry, Rectangle bounds, Matrix parentTransform, DrawingPathInfo textPathInfo, bool preferBitmap)
        {
            FillShapeLayoutInfoWithoutEffects(shapeLayoutInfo, shapeProperties, shapeStyle, actualTransform2D, geometry, bounds, parentTransform, textPathInfo, preferBitmap);
            ApplyEffects(shapeProperties, shapeStyle, shapeLayoutInfo);
        }

        public static void FillShapeLayoutInfoWithoutEffects(ShapeLayoutInfo shapeLayoutInfo, ShapeProperties shapeProperties, ShapeStyle shapeStyle, ModelShapeCustomGeometry geometry, Rectangle bounds, Matrix parentTransform, DrawingPathInfo textPathInfo, bool preferBitmap)
        {
            FillShapeLayoutInfoWithoutEffects(shapeLayoutInfo, shapeProperties, shapeStyle, shapeProperties.Transform2D, geometry, bounds, parentTransform, textPathInfo, preferBitmap);
        }

        public static void FillShapeLayoutInfoWithoutEffects(ShapeLayoutInfo shapeLayoutInfo, ShapeProperties shapeProperties, ShapeStyle shapeStyle, Transform2D actualTransform2D, ModelShapeCustomGeometry geometry, Rectangle bounds, Matrix parentTransform, DrawingPathInfo textPathInfo, bool preferBitmap)
        {
            IDocumentModel documentModel = shapeProperties.DocumentModel;
            float defaultScale = documentModel.ToDocumentLayoutUnitConverter.ToLayoutUnits(documentModel.UnitConverter.EmuToModelUnitsF(1));
            List<GraphicsPath> graphicsPaths = CalculateGraphicsPaths(geometry, bounds, defaultScale);
            ApplyScene3DProperties(graphicsPaths, shapeProperties.EffectStyle.Scene3DProperties);
            shapeLayoutInfo.ShapeTransform = CreateShapeTransform(actualTransform2D, bounds, parentTransform);
            shapeLayoutInfo.PenInfo = GetPenInfo(shapeProperties, shapeStyle, graphicsPaths);
            if (RenderAsMetafile(shapeProperties))
            {
                if (!shapeProperties.BlackAndWhitePrintMode)
                {
                    shapeLayoutInfo.Paths.Add(CreateMetafilePathInfo(shapeProperties.Fill as DrawingBlipFill, geometry, graphicsPaths, shapeLayoutInfo.PenInfo));
                }
            }
            else if (preferBitmap && RenderAsBitmap(shapeProperties))
            {
                if (!shapeProperties.BlackAndWhitePrintMode)
                {
                    shapeLayoutInfo.Paths.Add(CreateBitmapPathInfo(shapeProperties.Fill as DrawingBlipFill, geometry, graphicsPaths, shapeLayoutInfo.PenInfo));
                }
            }
            else
            {
                BrushInfo info = GetBrush(shapeProperties, shapeStyle, graphicsPaths, shapeLayoutInfo.ShapeTransform, shapeLayoutInfo.ScaleFactor);
                Brush defaultBrush = info.Brush;
                if (shapeProperties.IsConnectionShape())
                {
                    shapeLayoutInfo.Paths.AddRange(CalculateConnectionShapePathsInfo(geometry, graphicsPaths, shapeLayoutInfo.ScaleFactor));
                }
                else
                {
                    shapeLayoutInfo.Paths.AddRange(CalculatePathsInfo(geometry, graphicsPaths, defaultBrush, info.IsPermanent, shapeProperties.BlackAndWhitePrintMode));
                }
            }
            if (textPathInfo != null)
            {
                shapeLayoutInfo.Paths.Add(textPathInfo);
            }
        }

        internal static Brush GetBrush(IOfficeShape shape, Rectangle bounds)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddRectangle(bounds);
                List<GraphicsPath> graphicsPaths = new List<GraphicsPath>();
                graphicsPaths.Add(path);
                return GetBrush(shape.ShapeProperties, shape.ShapeStyle, graphicsPaths, null, 1f).Brush;
            }
        }

        public static BrushInfo GetBrush(ShapeProperties shapeProperties, ShapeStyle shapeStyle, List<GraphicsPath> graphicsPaths, Matrix shapeTransform) => 
            GetBrush(shapeProperties, shapeStyle, graphicsPaths, shapeTransform, 1f);

        public static BrushInfo GetBrush(ShapeProperties shapeProperties, ShapeStyle shapeStyle, List<GraphicsPath> graphicsPaths, Matrix shapeTransform, float scaleFactor)
        {
            IDrawingFill fill;
            if (shapeProperties.Fill == null)
            {
                return null;
            }
            if (shapeProperties.BlackAndWhitePrintMode)
            {
                return new BrushInfo(new SolidBrush(Color.White), false);
            }
            Brush defaultBrush = null;
            if (!shapeStyle.FillColor.IsEmpty && (shapeStyle.FillReferenceIdx != 0))
            {
                defaultBrush = new SolidBrush(shapeStyle.FillColor.FinalColor);
            }
            Color finalColor = shapeStyle.FillColor.FinalColor;
            if ((shapeProperties.Fill.FillType == DrawingFillType.Automatic) && (shapeStyle.FillReferenceIdx > 0))
            {
                fill = shapeProperties.DocumentModel.OfficeTheme.FormatScheme.GetFill(shapeStyle.FillReferenceIdx);
            }
            else
            {
                fill = shapeProperties.Fill;
                finalColor = Color.Empty;
            }
            ShapeFillRenderVisitor visitor = new ShapeFillRenderVisitor(shapeProperties.ShapeType, finalColor, defaultBrush, graphicsPaths, shapeTransform) {
                ScaleFactor = scaleFactor
            };
            fill.Visit(visitor);
            if (defaultBrush != null)
            {
                defaultBrush.Dispose();
            }
            return new BrushInfo(visitor.Result, visitor.HasPermanentFill);
        }

        private static double[] GetCameraRotationByPreset(PresetCameraType preset)
        {
            switch (preset)
            {
                case PresetCameraType.IsometricTopUp:
                    return new double[] { 314.7, 324.6, 60.2 };

                case PresetCameraType.IsometricBottomDown:
                    return new double[] { 314.7, 35.4, 299.8 };

                case PresetCameraType.IsometricLeftDown:
                {
                    double[] numArray1 = new double[3];
                    numArray1[0] = 45.0;
                    numArray1[1] = 35.0;
                    return numArray1;
                }
                case PresetCameraType.IsometricRightUp:
                {
                    double[] numArray2 = new double[3];
                    numArray2[0] = 315.0;
                    numArray2[1] = 35.0;
                    return numArray2;
                }
                case PresetCameraType.IsometricOffAxis1Left:
                {
                    double[] numArray5 = new double[3];
                    numArray5[0] = 64.0;
                    numArray5[1] = 18.0;
                    return numArray5;
                }
                case PresetCameraType.IsometricOffAxis1Right:
                {
                    double[] numArray6 = new double[3];
                    numArray6[0] = 334.0;
                    numArray6[1] = 18.0;
                    return numArray6;
                }
                case PresetCameraType.IsometricOffAxis1Top:
                    return new double[] { 306.5, 301.3, 57.6 };

                case PresetCameraType.IsometricOffAxis2Left:
                {
                    double[] numArray8 = new double[3];
                    numArray8[0] = 26.0;
                    numArray8[1] = 18.0;
                    return numArray8;
                }
                case PresetCameraType.IsometricOffAxis2Right:
                {
                    double[] numArray9 = new double[3];
                    numArray9[0] = 296.0;
                    numArray9[1] = 18.0;
                    return numArray9;
                }
                case PresetCameraType.IsometricOffAxis2Top:
                    return new double[] { 53.5, 301.3, 302.4 };
            }
            return new double[3];
        }

        private static LineCap GetLineCap(OutlineEndCapStyle capStyle) => 
            (capStyle == OutlineEndCapStyle.Round) ? LineCap.Round : ((capStyle == OutlineEndCapStyle.Square) ? LineCap.Square : LineCap.Flat);

        private static LineCapInfo GetLineCap(OutlineHeadTailType type, OutlineHeadTailSize width, OutlineHeadTailSize length) => 
            (type != OutlineHeadTailType.None) ? lineCapCreators[type].Create(width, length) : null;

        private static Brush GetPathFillBrush(PathFillMode fillMode, Brush defaultBrush)
        {
            if (fillMode == PathFillMode.None)
            {
                return null;
            }
            if (fillMode == PathFillMode.Norm)
            {
                return defaultBrush;
            }
            SolidBrush brush = defaultBrush as SolidBrush;
            if (brush != null)
            {
                return new SolidBrush(GetPathFillColor(brush.Color, fillMode));
            }
            LinearGradientBrush brush2 = defaultBrush as LinearGradientBrush;
            if (brush2 == null)
            {
                PathGradientBrush brush3 = defaultBrush as PathGradientBrush;
                if (brush3 == null)
                {
                    HatchBrush brush4 = defaultBrush as HatchBrush;
                    if (brush4 == null)
                    {
                        return defaultBrush;
                    }
                    return new HatchBrush(brush4.HatchStyle, brush4.ForegroundColor, GetPathFillColor(brush4.BackgroundColor, fillMode));
                }
                ColorBlend blend2 = CorrectGradientColors(fillMode, brush3.InterpolationColors);
                PathGradientBrush brush6 = defaultBrush.Clone() as PathGradientBrush;
                if (brush6 == null)
                {
                    return defaultBrush;
                }
                brush6.InterpolationColors = blend2;
                return brush6;
            }
            LinearGradientBrush brush5 = brush2.Clone() as LinearGradientBrush;
            if (brush5 == null)
            {
                return defaultBrush;
            }
            if (brush2.Blend == null)
            {
                brush5.InterpolationColors = CorrectGradientColors(fillMode, brush2.InterpolationColors);
            }
            else
            {
                for (int i = 0; i < brush5.LinearColors.Length; i++)
                {
                    Color fillColor = brush5.LinearColors[i];
                    brush5.LinearColors[i] = GetPathFillColor(fillColor, fillMode);
                }
            }
            return brush5;
        }

        public static Color GetPathFillColor(Color fillColor, PathFillMode fillMode)
        {
            if (fillMode == PathFillMode.Norm)
            {
                return fillColor;
            }
            if (fillMode == PathFillMode.None)
            {
                return Color.Empty;
            }
            Color empty = Color.Empty;
            switch (fillMode)
            {
                case PathFillMode.Lighten:
                    empty = DrawingValueConverter.GetLightenTransformColor(fillColor, 0.4);
                    break;

                case PathFillMode.LightenLess:
                    empty = DrawingValueConverter.GetLightenTransformColor(fillColor, 0.2);
                    break;

                case PathFillMode.Darken:
                    empty = DrawingValueConverter.GetDarkenTransformColor(fillColor, 0.6);
                    break;

                case PathFillMode.DarkenLess:
                    empty = DrawingValueConverter.GetDarkenTransformColor(fillColor, 0.8);
                    break;

                default:
                    break;
            }
            return Color.FromArgb(fillColor.A, empty);
        }

        internal static Pen GetPen(IOfficeShape shape)
        {
            PenInfo info = GetPenInfo(shape.ShapeProperties, shape.ShapeStyle, new List<GraphicsPath>());
            return info?.Pen;
        }

        public static Pen GetPen(ShapeProperties shapeProperties, ShapeStyle shapeStyle, Rectangle bounds)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddRectangle(bounds);
                List<GraphicsPath> list1 = new List<GraphicsPath>();
                list1.Add(path);
                List<GraphicsPath> graphicsPaths = list1;
                PenInfo info = GetPenInfo(shapeProperties, shapeStyle, graphicsPaths);
                return info?.Pen;
            }
        }

        public static PenInfo GetPenInfo(ShapeProperties shapeProperties, ShapeStyle shapeStyle, List<GraphicsPath> graphicsPaths) => 
            GetPenInfo(shapeProperties.Outline, shapeStyle, graphicsPaths, shapeProperties.ShapeType, shapeProperties.BlackAndWhitePrintMode);

        public static PenInfo GetPenInfo(Outline outline, ShapeStyle shapeStyle, List<GraphicsPath> graphicsPaths, ShapePreset shapeType) => 
            GetPenInfo(outline, shapeStyle, graphicsPaths, shapeType, false);

        public static PenInfo GetPenInfo(Outline outline, ShapeStyle shapeStyle, List<GraphicsPath> graphicsPaths, ShapePreset shapeType, bool blackAndWhitePrintMode)
        {
            Brush result;
            IDrawingFill fill = outline.Fill;
            Color color = (fill.FillType == DrawingFillType.Automatic) ? shapeStyle.LineColor.FinalColor : Color.Empty;
            IDocumentModel documentModel = shapeStyle.DocumentModel;
            Outline themeOutline = documentModel.OfficeTheme.FormatScheme.GetOutline(shapeStyle.LineReferenceIdx);
            if (outline.IsDefault && (themeOutline == null))
            {
                return null;
            }
            OutlineInfo info = ActualOutlineHelper.MergeOutline(outline, themeOutline);
            if ((fill.FillType == DrawingFillType.Automatic) && (themeOutline != null))
            {
                fill = themeOutline.Fill;
            }
            float penWidth = Math.Max(1f, documentModel.ToDocumentLayoutUnitConverter.ToLayoutUnits((float) info.Width));
            SolidBrush defaultBrush = new SolidBrush(color);
            LineCapInfo startCap = GetLineCap(info.HeadType, info.HeadWidth, info.HeadLength);
            LineCapInfo endCap = GetLineCap(info.TailType, info.TailWidth, info.TailLength);
            Action<GraphicsPath> action = delegate (GraphicsPath figure) {
                if ((penWidth > 0f) && GdipExtensions.AllowPathWidening(figure))
                {
                    using (Pen pen = new Pen(Color.Empty, penWidth))
                    {
                        if (startCap != null)
                        {
                            using (CustomLineCap cap = startCap.CreateCap(1f))
                            {
                                pen.CustomStartCap = cap;
                            }
                        }
                        if (endCap != null)
                        {
                            using (CustomLineCap cap2 = endCap.CreateCap(1f))
                            {
                                pen.CustomEndCap = cap2;
                            }
                        }
                        WidenHelper.Apply(figure, pen);
                    }
                }
            };
            bool hasPermanentFill = false;
            if (blackAndWhitePrintMode)
            {
                result = new SolidBrush(Color.Black);
            }
            else
            {
                ShapeFillRenderVisitor visitor = new ShapeFillRenderVisitor(shapeType, color, defaultBrush, graphicsPaths, null) {
                    OutlineFill = true,
                    FigureTransform = action
                };
                fill.Visit(visitor);
                defaultBrush.Dispose();
                result = visitor.Result;
                hasPermanentFill = visitor.HasPermanentFill;
            }
            if (result == null)
            {
                return null;
            }
            Pen pen = new Pen(result, penWidth);
            LineCap lineCap = GetLineCap(info.EndCapStyle);
            pen.StartCap = lineCap;
            pen.EndCap = lineCap;
            SetupOutlineDashing(pen, info.Dashing);
            SetupOutlineDashCap(pen, info.EndCapStyle);
            SetupCompoundType(pen, info.CompoundType);
            SetupLineJoin(pen, shapeType, info.JoinStyle, (float) DrawingValueConverter.FromPercentage(info.MiterLimit));
            return new PenInfo(pen, result, hasPermanentFill) { 
                StartCap = startCap,
                EndCap = endCap,
                Converter = value => documentModel.UnitConverter.ModelUnitsToEmuF(documentModel.ToDocumentLayoutUnitConverter.ToModelUnits(value))
            };
        }

        public static bool HasEffects(ShapeProperties shapeProperties, ShapeStyle shapeStyle)
        {
            IDocumentModel documentModel = shapeProperties.DocumentModel;
            DrawingEffectCollection effects = null;
            ContainerEffect containerEffect = shapeProperties.EffectStyle.ContainerEffect;
            if (containerEffect.ApplyEffectList)
            {
                effects = containerEffect.Effects;
            }
            else
            {
                DrawingEffectStyle effectStyle = documentModel.OfficeTheme.FormatScheme.GetEffectStyle(shapeStyle.EffectReferenceIdx);
                if ((effectStyle != null) && effectStyle.ContainerEffect.ApplyEffectList)
                {
                    effects = effectStyle.ContainerEffect.Effects;
                }
            }
            return ((effects != null) && (effects.Count > 0));
        }

        internal static bool IsMiteredLineJoinShape(ShapePreset preset)
        {
            if (preset > ShapePreset.Moon)
            {
                if (preset <= ShapePreset.EllipseRibbon2)
                {
                    return ((preset == ShapePreset.EllipseRibbon) || (preset == ShapePreset.EllipseRibbon2));
                }
                if (preset != ShapePreset.Plus)
                {
                    switch (preset)
                    {
                        case ShapePreset.MathPlus:
                        case ShapePreset.MathMinus:
                        case ShapePreset.MathMultiply:
                        case ShapePreset.MathEqual:
                        case ShapePreset.MathNotEqual:
                            break;

                        default:
                            goto TR_0000;
                    }
                }
                goto TR_0001;
            }
            else
            {
                if (preset > ShapePreset.QuadArrowCallout)
                {
                    return ((preset == ShapePreset.Cube) || (preset == ShapePreset.Moon));
                }
                switch (preset)
                {
                    case ShapePreset.Star4:
                    case ShapePreset.Star5:
                    case ShapePreset.Star8:
                    case ShapePreset.Star10:
                    case ShapePreset.Star12:
                    case ShapePreset.Star16:
                    case ShapePreset.Star24:
                    case ShapePreset.Star32:
                        break;

                    case ShapePreset.Star6:
                    case ShapePreset.Star7:
                        goto TR_0000;

                    default:
                        switch (preset)
                        {
                            case ShapePreset.Chevron:
                            case ShapePreset.RightArrow:
                            case ShapePreset.UpArrow:
                            case ShapePreset.DownArrow:
                            case ShapePreset.StripedRightArrow:
                            case ShapePreset.NotchedRightArrow:
                            case ShapePreset.BentUpArrow:
                            case ShapePreset.UpDownArrow:
                            case ShapePreset.LeftUpArrow:
                            case ShapePreset.LeftArrowCallout:
                            case ShapePreset.LeftRightArrowCallout:
                            case ShapePreset.UpDownArrowCallout:
                            case ShapePreset.QuadArrowCallout:
                                break;

                            default:
                                goto TR_0000;
                        }
                        break;
                }
                goto TR_0001;
            }
        TR_0000:
            return false;
        TR_0001:
            return true;
        }

        public static bool RenderAsBitmap(DrawingBlipFill blipFill) => 
            (blipFill != null) ? (!DevExpress.Utils.AzureCompatibility.Enable ? (blipFill.Stretch && blipFill.RotateWithShape) : false) : false;

        public static bool RenderAsBitmap(ShapeProperties shapeProperties) => 
            (shapeProperties.Fill.FillType == DrawingFillType.Picture) ? RenderAsBitmap(shapeProperties.Fill as DrawingBlipFill) : false;

        public static bool RenderAsImage(DrawingBlipFill blipFill) => 
            (blipFill != null) ? (!DevExpress.Utils.AzureCompatibility.Enable ? (blipFill.Stretch && blipFill.RotateWithShape) : false) : false;

        public static bool RenderAsImage(ShapeProperties shapeProperties) => 
            (shapeProperties.Fill.FillType == DrawingFillType.Picture) ? RenderAsImage(shapeProperties.Fill as DrawingBlipFill) : false;

        public static bool RenderAsMetafile(DrawingBlipFill blipFill)
        {
            System.Drawing.Image nativeImage;
            if (blipFill == null)
            {
                return false;
            }
            if (DevExpress.Utils.AzureCompatibility.Enable)
            {
                return false;
            }
            OfficeImage image = blipFill.Blip.Image;
            if (image != null)
            {
                nativeImage = image.NativeImage;
            }
            else
            {
                OfficeImage local1 = image;
                nativeImage = null;
            }
            return ((nativeImage is Metafile) && (blipFill.Stretch && blipFill.RotateWithShape));
        }

        public static bool RenderAsMetafile(ShapeProperties shapeProperties) => 
            (shapeProperties.Fill.FillType == DrawingFillType.Picture) ? RenderAsMetafile(shapeProperties.Fill as DrawingBlipFill) : false;

        private static void SetupCompoundType(Pen pen, OutlineCompoundType compoundType)
        {
            switch (compoundType)
            {
                case OutlineCompoundType.Single:
                    break;

                case OutlineCompoundType.Double:
                    pen.CompoundArray = new float[] { 0f, 0.35f, 0.65f, 1f };
                    return;

                case OutlineCompoundType.ThickThin:
                    pen.CompoundArray = new float[] { 0f, 0.6f, 0.8f, 1f };
                    return;

                case OutlineCompoundType.ThinThick:
                    pen.CompoundArray = new float[] { 0f, 0.2f, 0.4f, 1f };
                    return;

                case OutlineCompoundType.Triple:
                    pen.CompoundArray = new float[] { 0f, 0.2f, 0.35f, 0.65f, 0.8f, 1f };
                    break;

                default:
                    return;
            }
        }

        private static void SetupImageGraphicsClip(Graphics graphics, List<GraphicsPath> graphicsPaths, ModelShapePathsList modelPaths)
        {
            bool flag = false;
            for (int i = 0; i < modelPaths.Count; i++)
            {
                if (modelPaths[i].FillMode != PathFillMode.None)
                {
                    CombineMode union = CombineMode.Union;
                    if (!flag)
                    {
                        union = CombineMode.Replace;
                        flag = true;
                    }
                    graphics.SetClip(graphicsPaths[i], union);
                }
            }
        }

        private static void SetupLineJoin(Pen pen, ShapePreset shapeType, LineJoinStyle joinStyle, float miterLimit)
        {
            if ((joinStyle == LineJoinStyle.Round) && IsMiteredLineJoinShape(shapeType))
            {
                joinStyle = LineJoinStyle.Miter;
            }
            SetupLineJoinCore(pen, joinStyle, miterLimit);
        }

        private static void SetupLineJoinCore(Pen pen, LineJoinStyle joinStyle, float miterLimit)
        {
            switch (joinStyle)
            {
                case LineJoinStyle.Bevel:
                    pen.LineJoin = LineJoin.Bevel;
                    return;

                case LineJoinStyle.Miter:
                    pen.LineJoin = LineJoin.Miter;
                    pen.MiterLimit = miterLimit;
                    return;

                case LineJoinStyle.Round:
                    pen.LineJoin = LineJoin.Round;
                    return;
            }
        }

        private static void SetupMetafileHitTestCollection(HitTestInfoCollection hitTestInfoCollection, List<GraphicsPath> graphicsPaths, ModelShapePathsList modelPaths)
        {
            for (int i = 0; i < modelPaths.Count; i++)
            {
                hitTestInfoCollection.Add(new HitTestInfo(graphicsPaths[i], modelPaths[i].FillMode != PathFillMode.None));
            }
        }

        private static void SetupOutlineDashCap(Pen pen, OutlineEndCapStyle endCapStyle)
        {
            switch (endCapStyle)
            {
                case OutlineEndCapStyle.Round:
                    pen.DashCap = DashCap.Round;
                    return;

                case OutlineEndCapStyle.Square:
                case OutlineEndCapStyle.Flat:
                    pen.DashCap = DashCap.Flat;
                    return;
            }
        }

        private static void SetupOutlineDashing(Pen pen, OutlineDashing dashing)
        {
            switch (dashing)
            {
                case OutlineDashing.Solid:
                    pen.DashStyle = DashStyle.Solid;
                    return;

                case OutlineDashing.SystemDash:
                    pen.DashStyle = DashStyle.Dash;
                    return;

                case OutlineDashing.SystemDot:
                    pen.DashStyle = DashStyle.Dot;
                    return;

                case OutlineDashing.SystemDashDot:
                    pen.DashStyle = DashStyle.Custom;
                    pen.DashPattern = new float[] { 3f, 1f, 1f, 1f };
                    return;

                case OutlineDashing.SystemDashDotDot:
                    pen.DashStyle = DashStyle.Custom;
                    pen.DashPattern = new float[] { 3f, 1f, 1f, 1f, 1f, 1f };
                    return;

                case OutlineDashing.Dot:
                    pen.DashStyle = DashStyle.Custom;
                    pen.DashPattern = new float[] { 1f, 3f };
                    return;

                case OutlineDashing.Dash:
                    pen.DashStyle = DashStyle.Custom;
                    pen.DashPattern = new float[] { 4f, 3f };
                    return;

                case OutlineDashing.LongDash:
                    pen.DashStyle = DashStyle.Custom;
                    pen.DashPattern = new float[] { 8f, 3f };
                    return;

                case OutlineDashing.DashDot:
                    pen.DashStyle = DashStyle.Custom;
                    pen.DashPattern = new float[] { 4f, 3f, 1f, 3f };
                    return;

                case OutlineDashing.LongDashDot:
                    pen.DashStyle = DashStyle.Custom;
                    pen.DashPattern = new float[] { 8f, 3f, 1f, 3f };
                    return;

                case OutlineDashing.LongDashDotDot:
                    pen.DashStyle = DashStyle.Custom;
                    pen.DashPattern = new float[] { 8f, 3f, 1f, 3f, 1f, 3f };
                    return;
            }
        }

        private static void SwapTwoFloats(ref float a, ref float b)
        {
            float num = a;
            a = b;
            b = num;
        }
    }
}

