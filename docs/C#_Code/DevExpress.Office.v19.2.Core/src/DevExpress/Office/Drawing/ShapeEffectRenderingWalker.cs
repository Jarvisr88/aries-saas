namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Layout;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;

    public class ShapeEffectRenderingWalker : IDrawingEffectVisitor
    {
        public static Dictionary<Type, int> EffectPriority = CreatePriorities();
        private readonly DrawingEffectCollection effects;
        private readonly IShapeLayoutInfo shapeLayoutInfo;
        private readonly DocumentModelUnitConverter unitConverter;
        private readonly DocumentModelUnitToLayoutUnitConverter toLayoutUnitConverter;
        private readonly DocumentLayoutUnitConverter layoutUnitConverter;
        private bool blackAndWhitePrintMode;

        public ShapeEffectRenderingWalker(ShapePropertiesBase shapeProperties, IShapeLayoutInfo shapeLayoutInfo)
        {
            Guard.ArgumentNotNull(shapeProperties, "shapeProperties");
            Guard.ArgumentNotNull(shapeLayoutInfo, "shapeLayoutInfo");
            this.effects = shapeProperties.EffectStyle.ContainerEffect.Effects;
            this.shapeLayoutInfo = shapeLayoutInfo;
            this.unitConverter = shapeProperties.DocumentModel.UnitConverter;
            this.toLayoutUnitConverter = shapeProperties.DocumentModel.ToDocumentLayoutUnitConverter;
            this.layoutUnitConverter = shapeProperties.DocumentModel.LayoutUnitConverter;
            this.blackAndWhitePrintMode = shapeProperties.BlackAndWhitePrintMode;
        }

        public ShapeEffectRenderingWalker(DrawingEffectCollection effects, IDocumentModel documentModel, IShapeLayoutInfo shapeLayoutInfo) : this(effects, documentModel, shapeLayoutInfo, false)
        {
        }

        public ShapeEffectRenderingWalker(DrawingEffectCollection effects, IDocumentModel documentModel, IShapeLayoutInfo shapeLayoutInfo, bool blackAndWhitePrintMode)
        {
            Guard.ArgumentNotNull(effects, "effects");
            Guard.ArgumentNotNull(documentModel, "documentModel");
            Guard.ArgumentNotNull(shapeLayoutInfo, "shapeLayoutInfo");
            this.effects = effects;
            this.shapeLayoutInfo = shapeLayoutInfo;
            this.unitConverter = documentModel.UnitConverter;
            this.toLayoutUnitConverter = documentModel.ToDocumentLayoutUnitConverter;
            this.layoutUnitConverter = documentModel.LayoutUnitConverter;
            this.blackAndWhitePrintMode = blackAndWhitePrintMode;
        }

        public void AlphaCeilingEffectVisit()
        {
        }

        public void AlphaFloorEffectVisit()
        {
        }

        public void ApplyEffects()
        {
            List<IDrawingEffect> list = new List<IDrawingEffect>(this.effects.InnerList);
            list.Sort(new ShapeEffectComparer());
            foreach (IDrawingEffect effect in list)
            {
                effect.Visit(this);
            }
        }

        [SecuritySafeCritical]
        public static void ApplyShadow(Bitmap shadowBitmap, Color color)
        {
            BitmapData bitmapdata = shadowBitmap.LockBits(new Rectangle(0, 0, shadowBitmap.Width, shadowBitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int length = bitmapdata.Stride * shadowBitmap.Height;
            byte[] values = new byte[length];
            Marshal.Copy(bitmapdata.Scan0, values, 0, length);
            GdipExtensions.IterateBitmapBytes(length, delegate (int i) {
                byte num = values[i + 3];
                if (num > 0)
                {
                    values[i] = color.B;
                    values[i + 1] = color.G;
                    values[i + 2] = color.R;
                    values[i + 3] = Convert.ToByte((float) ((((float) color.A) / 255f) * num));
                }
            });
            Marshal.Copy(values, 0, bitmapdata.Scan0, length);
            shadowBitmap.UnlockBits(bitmapdata);
        }

        private void ApplyShift(GraphicsPath graphicsPath, float offsetX, float offsetY)
        {
            using (Matrix matrix = new Matrix())
            {
                matrix.Translate(offsetX, offsetY);
                graphicsPath.Transform(matrix);
            }
        }

        private GraphicsPath CollectFigure(GraphicsPathCollectFlags flags) => 
            this.shapeLayoutInfo.Paths.CollectFigure(flags);

        private float ConvertRadiusToLayoutUnits(long radius) => 
            this.toLayoutUnitConverter.ToLayoutUnits((float) this.unitConverter.EmuToModelUnitsL(radius));

        private int ConvertToPixels(int valueInLayoutUnits) => 
            this.layoutUnitConverter.LayoutUnitsToPixels(valueInLayoutUnits);

        private Matrix CreateEffectTransformCore(OuterShadowTransformationInfo info) => 
            new Matrix(info.ScaleX, info.SkewY, info.SkewX, info.ScaleY, info.OffsetX, info.OffsetY);

        private Bitmap CreateInnerShadowFigureBitmap(PathInfoBase[] pathInfos, Rectangle sourceBounds, Matrix shapeTransform)
        {
            Bitmap image = new Bitmap(this.ConvertToPixels(sourceBounds.Width), this.ConvertToPixels(sourceBounds.Height));
            using (Graphics graphics = GdipExtensions.PrepareGraphicsFromImage(image, (GraphicsUnit) this.layoutUnitConverter.GraphicsPageUnit, this.layoutUnitConverter.GraphicsPageScale))
            {
                graphics.TranslateTransform((float) -sourceBounds.Left, (float) -sourceBounds.Top);
                foreach (PathInfoBase base2 in pathInfos)
                {
                    base2.SkipDrawing = true;
                    base2.Draw(graphics, null, shapeTransform);
                }
            }
            return image;
        }

        private Bitmap CreateInnerShadowOpacityMask(List<GraphicsPath> shadowPaths, Rectangle sourceBounds, Color shadowColor, int blurRadius)
        {
            sourceBounds.Inflate(blurRadius, blurRadius);
            Bitmap image = new Bitmap(this.ConvertToPixels(sourceBounds.Width), this.ConvertToPixels(sourceBounds.Height));
            using (Graphics graphics = GdipExtensions.PrepareGraphicsFromImage(image, (GraphicsUnit) this.layoutUnitConverter.GraphicsPageUnit, this.layoutUnitConverter.GraphicsPageScale))
            {
                graphics.TranslateTransform((float) -sourceBounds.Left, (float) -sourceBounds.Top, MatrixOrder.Append);
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.Clear(shadowColor);
                using (Brush brush = new SolidBrush(Color.Empty))
                {
                    foreach (GraphicsPath path in shadowPaths)
                    {
                        graphics.FillPath(brush, path);
                        path.Dispose();
                    }
                }
            }
            return image;
        }

        private OuterShadowTransformationInfo CreateOuterShadowTransformationInfo(Rectangle shapeBounds, OuterShadowEffectInfo shadowInfo, float scaleFactor) => 
            new OuterShadowTransformationCalculator(this.unitConverter, this.toLayoutUnitConverter, scaleFactor).Calculate(shapeBounds, shadowInfo);

        private Bitmap CreatePrintBitmapCore(Rectangle figureBounds, Matrix shadowTransform, bool rotateWithShape, Action<Graphics, bool> drawContent)
        {
            Bitmap image = new Bitmap(this.ConvertToPixels(figureBounds.Width), this.ConvertToPixels(figureBounds.Height));
            using (Graphics graphics = GdipExtensions.PrepareGraphicsFromImage(image, (GraphicsUnit) this.layoutUnitConverter.GraphicsPageUnit, this.layoutUnitConverter.GraphicsPageScale))
            {
                graphics.Transform = shadowTransform;
                graphics.TranslateTransform((float) -figureBounds.Left, (float) -figureBounds.Top, MatrixOrder.Append);
                drawContent(graphics, !rotateWithShape);
            }
            return image;
        }

        private static Dictionary<Type, int> CreatePriorities() => 
            new Dictionary<Type, int> { 
                { 
                    typeof(InnerShadowEffect),
                    0
                },
                { 
                    typeof(PresetShadowEffect),
                    1
                },
                { 
                    typeof(SoftEdgeEffect),
                    2
                },
                { 
                    typeof(BlurEffect),
                    3
                },
                { 
                    typeof(GlowEffect),
                    4
                },
                { 
                    typeof(OuterShadowEffect),
                    5
                },
                { 
                    typeof(ReflectionEffect),
                    6
                }
            };

        private Bitmap CreateShadowPrint(Rectangle figureBounds, Matrix shadowTransform, Color shadowColor, bool rotateWithShape)
        {
            Bitmap shadowBitmap = this.CreatePrintBitmapCore(figureBounds, shadowTransform, rotateWithShape, new Action<Graphics, bool>(this.shapeLayoutInfo.DrawShadow));
            ApplyShadow(shadowBitmap, shadowColor);
            return shadowBitmap;
        }

        private int GetBlurRadius(long radius)
        {
            float radiusInLayoutUnits = this.ConvertRadiusToLayoutUnits(radius);
            return this.GetBlurRadius(radiusInLayoutUnits);
        }

        private int GetBlurRadius(float radiusInLayoutUnits) => 
            (radiusInLayoutUnits > 0f) ? ((int) ((Math.Round((double) radiusInLayoutUnits) / 2.0) + 1.0)) : 0;

        private Bitmap GetCroppedBitmap(Bitmap figureBitmap, Rectangle bounds, Rectangle boundsWithoutTransform)
        {
            Bitmap image = new Bitmap(this.ConvertToPixels(bounds.Width), this.ConvertToPixels(bounds.Height), PixelFormat.Format32bppArgb);
            using (Graphics graphics = GdipExtensions.PrepareGraphicsFromImage(image, (GraphicsUnit) this.layoutUnitConverter.GraphicsPageUnit, this.layoutUnitConverter.GraphicsPageScale))
            {
                graphics.TranslateTransform((float) -bounds.X, (float) -bounds.Y);
                using (Matrix matrix = graphics.Transform)
                {
                    Matrix shapeTransform = this.shapeLayoutInfo.ShapeTransform;
                    if (shapeTransform != null)
                    {
                        graphics.MultiplyTransform(shapeTransform);
                    }
                    graphics.SetClip(boundsWithoutTransform);
                    graphics.Transform = matrix;
                }
                graphics.DrawImage(figureBitmap, bounds.Location);
            }
            figureBitmap.Dispose();
            return image;
        }

        private GraphicsPath[] GetInnerShadowPaths(GraphicsPathCollectFlags flags)
        {
            GraphicsPath path = new GraphicsPath(FillMode.Alternate);
            GraphicsPath path2 = new GraphicsPath(FillMode.Alternate);
            PathInfoCollection paths = this.shapeLayoutInfo.Paths;
            foreach (PathInfoBase base2 in paths)
            {
                GraphicsPath graphicsPath = base2.GraphicsPath;
                if ((graphicsPath != null) && ((graphicsPath.PointCount != 0) && !paths.ShouldSkipPathInfo(base2, flags)))
                {
                    path.AddPath(graphicsPath, false);
                    if (base2.Stroke)
                    {
                        path2.AddPath(graphicsPath, false);
                    }
                }
            }
            return new GraphicsPath[] { path, path2 };
        }

        public void GrayScaleEffectVisit()
        {
        }

        private Func<Bitmap> PrepareEffectRenderer(Func<Bitmap> effectRenderer)
        {
            if (!this.RenderDirectly)
            {
                return effectRenderer;
            }
            Bitmap result = effectRenderer();
            return () => result;
        }

        private List<GraphicsPath> PrepareInnerShadowPaths(OffsetShadowInfo shadowOffset, PathInfoBase[] pathInfos, Matrix shapeTransform)
        {
            float num = this.toLayoutUnitConverter.ToLayoutUnits((float) this.unitConverter.EmuToModelUnitsL(shadowOffset.Distance));
            double d = DrawingValueConverter.DegreeToRadian(DrawingValueConverter.FromPositiveFixedAngle(shadowOffset.Direction));
            float num3 = (float) (num * Math.Cos(d));
            float num4 = (float) (num * Math.Sin(d));
            List<GraphicsPath> list = new List<GraphicsPath>();
            foreach (PathInfoBase base2 in pathInfos)
            {
                GraphicsPath graphicsPath = (GraphicsPath) base2.GraphicsPath.Clone();
                graphicsPath.CloseFigure();
                this.ApplyShift(graphicsPath, -num3, -num4);
                if (shapeTransform != null)
                {
                    graphicsPath.Transform(shapeTransform);
                }
                list.Add(graphicsPath);
            }
            return list;
        }

        public void Visit(AlphaBiLevelEffect drawingEffect)
        {
        }

        public void Visit(AlphaInverseEffect drawingEffect)
        {
        }

        public void Visit(AlphaModulateEffect drawingEffect)
        {
        }

        public void Visit(AlphaModulateFixedEffect drawingEffect)
        {
        }

        public void Visit(AlphaOutsetEffect drawingEffect)
        {
        }

        public void Visit(AlphaReplaceEffect drawingEffect)
        {
        }

        public void Visit(BiLevelEffect drawingEffect)
        {
        }

        public void Visit(BlendEffect drawingEffect)
        {
        }

        public void Visit(BlurEffect drawingEffect)
        {
            float num = this.ConvertRadiusToLayoutUnits(drawingEffect.Radius);
            int blurRadius = this.GetBlurRadius((float) (num * this.shapeLayoutInfo.ScaleFactor));
            if (blurRadius != 0)
            {
                Rectangle sourceBounds = this.shapeLayoutInfo.GetBlurEffectBounds(null, true, EffectAdditionalSize.Default);
                sourceBounds.Inflate(blurRadius, blurRadius);
                if (!sourceBounds.IsEmpty)
                {
                    bool isCropped = !drawingEffect.Grow;
                    Rectangle boundsWithoutTransform = isCropped ? this.shapeLayoutInfo.GetBlurEffectBounds(null, false, EffectAdditionalSize.Default) : Rectangle.Empty;
                    Func<Bitmap> effectRenderer = delegate {
                        Bitmap image = new Bitmap(this.ConvertToPixels(sourceBounds.Width), this.ConvertToPixels(sourceBounds.Height));
                        using (Graphics graphics = GdipExtensions.PrepareGraphicsFromImage(image, (GraphicsUnit) this.layoutUnitConverter.GraphicsPageUnit, this.layoutUnitConverter.GraphicsPageScale))
                        {
                            graphics.TranslateTransform((float) -sourceBounds.X, (float) -sourceBounds.Y);
                            this.shapeLayoutInfo.DrawBlurEffect(graphics);
                        }
                        image = BlurEffectRenderer.ApplyBlur(image, this.ConvertToPixels(blurRadius));
                        if (isCropped)
                        {
                            image = this.GetCroppedBitmap(image, sourceBounds, boundsWithoutTransform);
                        }
                        return image;
                    };
                    BlurPathInfo item = new BlurPathInfo(new GraphicsPath(), this.PrepareEffectRenderer(effectRenderer), sourceBounds) {
                        BlurRadiusInLayoutUnits = num,
                        IsCropped = isCropped,
                        BoundsWithoutTransform = boundsWithoutTransform
                    };
                    this.shapeLayoutInfo.Paths.Insert(0, item);
                }
            }
        }

        public void Visit(ColorChangeEffect drawingEffect)
        {
        }

        public void Visit(ContainerEffect drawingEffect)
        {
        }

        public void Visit(DuotoneEffect drawingEffect)
        {
        }

        public void Visit(Effect drawingEffect)
        {
        }

        public void Visit(FillEffect drawingEffect)
        {
        }

        public void Visit(FillOverlayEffect drawingEffect)
        {
        }

        public void Visit(GlowEffect drawingEffect)
        {
            Color color = this.blackAndWhitePrintMode ? Color.White : drawingEffect.Color.FinalColor;
            float radiusInLayoutUnits = this.ConvertRadiusToLayoutUnits(drawingEffect.Radius);
            int blurRadius = this.GetBlurRadius(radiusInLayoutUnits);
            if ((blurRadius != 0) && (color.A != 0))
            {
                bool processSoftEdgesSize = this.shapeLayoutInfo.GetType() == typeof(ShapeLayoutInfo);
                EffectAdditionalSize additionalSize = new EffectAdditionalSize((float) blurRadius, processSoftEdgesSize);
                Rectangle sourceBounds = this.shapeLayoutInfo.GetGlowBounds(additionalSize, null);
                if (!sourceBounds.IsEmpty)
                {
                    sourceBounds.Inflate(blurRadius, blurRadius);
                    additionalSize.ProcessGlowSize = false;
                    Func<Bitmap> effectRenderer = delegate {
                        Bitmap image = new Bitmap(this.ConvertToPixels(sourceBounds.Width), this.ConvertToPixels(sourceBounds.Height));
                        using (Graphics graphics = GdipExtensions.PrepareGraphicsFromImage(image, (GraphicsUnit) this.layoutUnitConverter.GraphicsPageUnit, this.layoutUnitConverter.GraphicsPageScale))
                        {
                            graphics.TranslateTransform((float) -sourceBounds.Left, (float) -sourceBounds.Top, MatrixOrder.Append);
                            this.shapeLayoutInfo.DrawGlow(graphics, color, additionalSize, null);
                        }
                        return AlphaBlurEffectRenderer.GlowBlur(image, this.ConvertToPixels(blurRadius), color);
                    };
                    GlowPathInfo item = new GlowPathInfo(new GraphicsPath(), this.PrepareEffectRenderer(effectRenderer), sourceBounds) {
                        BlurRadiusInLayoutUnits = radiusInLayoutUnits,
                        RotateWithShape = true
                    };
                    this.shapeLayoutInfo.Paths.Insert(0, item);
                }
            }
        }

        public void Visit(HSLEffect drawingEffect)
        {
        }

        public void Visit(InnerShadowEffect innerShadow)
        {
            ShapeLayoutInfo shapeLayoutInfo = this.shapeLayoutInfo as ShapeLayoutInfo;
            if (shapeLayoutInfo != null)
            {
                Matrix shapeTransform = shapeLayoutInfo.ShapeTransform;
                GraphicsPath[] innerShadowPaths = this.GetInnerShadowPaths(GraphicsPathCollectFlags.ExcludeConnectors | GraphicsPathCollectFlags.FigureWithoutEffects | GraphicsPathCollectFlags.ExcludeInvisiblePaths);
                GraphicsPath graphicsPath = innerShadowPaths[0];
                GraphicsPath path2 = innerShadowPaths[1];
                Rectangle sourceBounds = graphicsPath.GetBoundsExt(shapeTransform);
                graphicsPath.Dispose();
                if (sourceBounds.IsEmpty)
                {
                    path2.Dispose();
                }
                else
                {
                    Func<Bitmap> effectRenderer = delegate {
                        PathInfoBase[] pathInfos = shapeLayoutInfo.GetPathInfos();
                        Bitmap figure = this.CreateInnerShadowFigureBitmap(pathInfos, sourceBounds, shapeTransform);
                        Color baseColor = this.blackAndWhitePrintMode ? Color.FromArgb(innerShadow.Color.FinalColor.A, 0, 0, 0) : innerShadow.Color.FinalColor;
                        Color shadowColor = Color.FromArgb(0xff, baseColor);
                        int blurRadius = (int) Math.Round((double) (this.GetBlurRadius(innerShadow.BlurRadius) * shapeLayoutInfo.ScaleFactor));
                        List<GraphicsPath> shadowPaths = this.PrepareInnerShadowPaths(innerShadow.OffsetShadow, pathInfos, shapeTransform);
                        Bitmap bitmap = this.CreateInnerShadowOpacityMask(shadowPaths, sourceBounds, shadowColor, blurRadius);
                        if (blurRadius > 0)
                        {
                            Size sourceSizeInPixels = new Size(this.ConvertToPixels(sourceBounds.Width), this.ConvertToPixels(sourceBounds.Height));
                            bitmap = AlphaBlurEffectRenderer.InnerShadowBlur(bitmap, sourceSizeInPixels, this.ConvertToPixels(blurRadius), shadowColor);
                        }
                        InnerShadowRenderer.ApplyShadow(figure, bitmap, baseColor);
                        bitmap.Dispose();
                        return figure;
                    };
                    InnerShadowPathInfo item = new InnerShadowPathInfo(path2, this.PrepareEffectRenderer(effectRenderer), sourceBounds);
                    shapeLayoutInfo.Paths.Insert(0, item);
                }
            }
        }

        public void Visit(LuminanceEffect drawingEffect)
        {
        }

        public void Visit(OuterShadowEffect shadow)
        {
            OuterShadowTransformationInfo transformInfo;
            Rectangle shadowBounds;
            Color shadowColor;
            bool rotateWithShape;
            int blurRadius;
            OuterShadowEffectInfo shadowInfo = shadow.Info;
            Rectangle shapeBounds = this.shapeLayoutInfo.CalculateShadowTransformBounds(shadowInfo.RotateWithShape, EffectAdditionalSize.Empty);
            if (!shapeBounds.IsEmpty)
            {
                GraphicsPath path;
                Rectangle boundsExt;
                rotateWithShape = shadow.Info.RotateWithShape;
                transformInfo = this.CreateOuterShadowTransformationInfo(shapeBounds, shadowInfo, !rotateWithShape ? this.shapeLayoutInfo.ScaleFactor : 1f);
                using (Matrix matrix = this.CreateEffectTransformCore(transformInfo))
                {
                    path = this.shapeLayoutInfo.GetPreparedForShadowFigure(matrix, rotateWithShape, EffectAdditionalSize.Default);
                    if (path != null)
                    {
                        boundsExt = path.GetBoundsExt();
                        if ((boundsExt.Width == 0) || (boundsExt.Height == 0))
                        {
                            path.Dispose();
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                shadowColor = this.blackAndWhitePrintMode ? Color.FromArgb(shadow.Color.FinalColor.A, 0, 0, 0) : shadow.Color.FinalColor;
                blurRadius = (int) Math.Round((double) (this.GetBlurRadius(shadow.Info.BlurRadius) * this.shapeLayoutInfo.ScaleFactor));
                shadowBounds = boundsExt;
                if (blurRadius > 0)
                {
                    boundsExt.Inflate(blurRadius, blurRadius);
                }
                Func<Bitmap> effectRenderer = delegate {
                    using (Matrix matrix = this.CreateEffectTransformCore(transformInfo))
                    {
                        Bitmap bitmap = this.CreateShadowPrint(shadowBounds, matrix, shadowColor, rotateWithShape);
                        if (blurRadius > 0)
                        {
                            bitmap = AlphaBlurEffectRenderer.ShadowBlur(bitmap, this.ConvertToPixels(blurRadius), shadowColor);
                        }
                        return bitmap;
                    }
                };
                Func<HitTestInfoCollection> hitTestInfoProvider = delegate {
                    using (Matrix matrix = this.CreateEffectTransformCore(transformInfo))
                    {
                        return this.shapeLayoutInfo.CreateShadowHitTestInfos(matrix, rotateWithShape);
                    }
                };
                OuterShadowPathInfo item = new OuterShadowPathInfo(path, this.PrepareEffectRenderer(effectRenderer), hitTestInfoProvider, boundsExt) {
                    BlurRadius = blurRadius,
                    RotateWithShape = rotateWithShape,
                    Stroke = this.shapeLayoutInfo.PenInfo != null
                };
                this.shapeLayoutInfo.Paths.Insert(0, item);
            }
        }

        public void Visit(PresetShadowEffect presetShadow)
        {
            foreach (IDrawingEffect effect in PresetShadowGenerator.GenerateShadowEffect(presetShadow))
            {
                effect.Visit(this);
            }
        }

        public void Visit(ReflectionEffect reflection)
        {
            OuterShadowTransformationInfo transformInfo;
            Rectangle shadowBounds;
            bool rotateWithShape;
            int blurRadius;
            OuterShadowEffectInfo outerShadowEffectInfo = reflection.OuterShadowEffectInfo;
            Rectangle shapeBounds = this.shapeLayoutInfo.CalculateReflectionTransformBounds(outerShadowEffectInfo.RotateWithShape, EffectAdditionalSize.Empty);
            if (!shapeBounds.IsEmpty)
            {
                GraphicsPath path;
                Rectangle boundsExt;
                rotateWithShape = outerShadowEffectInfo.RotateWithShape;
                transformInfo = this.CreateOuterShadowTransformationInfo(shapeBounds, outerShadowEffectInfo, !rotateWithShape ? this.shapeLayoutInfo.ScaleFactor : 1f);
                using (Matrix matrix = this.CreateEffectTransformCore(transformInfo))
                {
                    path = this.shapeLayoutInfo.GetPreparedForReflectionFigure(matrix, rotateWithShape, EffectAdditionalSize.Default);
                    if (path != null)
                    {
                        boundsExt = path.GetBoundsExt();
                        if ((boundsExt.Width == 0) || (boundsExt.Height == 0))
                        {
                            path.Dispose();
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                blurRadius = (int) Math.Round((double) (this.GetBlurRadius(outerShadowEffectInfo.BlurRadius) * this.shapeLayoutInfo.ScaleFactor));
                shadowBounds = boundsExt;
                if (blurRadius > 0)
                {
                    boundsExt.Inflate(blurRadius, blurRadius);
                }
                Func<Bitmap> effectRenderer = delegate {
                    using (Matrix matrix = this.CreateEffectTransformCore(transformInfo))
                    {
                        Bitmap bitmap = this.CreatePrintBitmapCore(shadowBounds, matrix, rotateWithShape, new Action<Graphics, bool>(this.shapeLayoutInfo.DrawReflection));
                        if (blurRadius > 0)
                        {
                            bitmap = BlurEffectRenderer.ApplyReflectionBlur(bitmap, this.ConvertToPixels(blurRadius));
                        }
                        ReflectionRenderer.ProcessReflection(bitmap, reflection.ReflectionOpacity);
                        return bitmap;
                    }
                };
                Func<HitTestInfoCollection> hitTestInfoProvider = delegate {
                    using (Matrix matrix = this.CreateEffectTransformCore(transformInfo))
                    {
                        return this.shapeLayoutInfo.CreateReflectionHitTestInfos(matrix, rotateWithShape);
                    }
                };
                ReflectionPathInfo item = new ReflectionPathInfo(path, this.PrepareEffectRenderer(effectRenderer), hitTestInfoProvider, boundsExt) {
                    RotateWithShape = rotateWithShape,
                    Stroke = this.shapeLayoutInfo.PenInfo != null
                };
                this.shapeLayoutInfo.Paths.Insert(0, item);
            }
        }

        public void Visit(RelativeOffsetEffect drawingEffect)
        {
        }

        public void Visit(SoftEdgeEffect drawingEffect)
        {
            ShapeLayoutInfo shapeLayoutInfo = this.shapeLayoutInfo as ShapeLayoutInfo;
            if (shapeLayoutInfo != null)
            {
                float radius = this.ConvertRadiusToLayoutUnits(drawingEffect.Radius);
                if (radius != 0f)
                {
                    Rectangle sourceBounds = shapeLayoutInfo.GetSoftEdgeBounds();
                    int penWidth = (int) shapeLayoutInfo.PenWidth;
                    sourceBounds.Inflate(penWidth, penWidth);
                    Func<Bitmap> effectRenderer = delegate {
                        int width = this.ConvertToPixels(sourceBounds.Width);
                        int height = this.ConvertToPixels(sourceBounds.Height);
                        Bitmap image = new Bitmap(width, height);
                        using (Graphics graphics = GdipExtensions.PrepareGraphicsFromImage(image, (GraphicsUnit) this.layoutUnitConverter.GraphicsPageUnit, this.layoutUnitConverter.GraphicsPageScale))
                        {
                            graphics.TranslateTransform((float) -sourceBounds.X, (float) -sourceBounds.Y);
                            shapeLayoutInfo.DrawBackgroundBitmap(graphics, GraphicsPathCollectFlags.ExcludeBlur | GraphicsPathCollectFlags.ExcludeSoftEdges);
                        }
                        Bitmap opacityMask = new SoftEdgesOpacityMaskCreator(shapeLayoutInfo).Create(image, sourceBounds, radius * shapeLayoutInfo.ScaleFactor, this.layoutUnitConverter);
                        return (opacityMask == null) ? new Bitmap(width, height) : SoftEdgesBlurRenderer.ApplyBlur(image, opacityMask, this.ConvertToPixels(this.GetBlurRadius((float) (radius * shapeLayoutInfo.ScaleFactor))));
                    };
                    SoftEdgePathInfo item = new SoftEdgePathInfo(new GraphicsPath(), this.PrepareEffectRenderer(effectRenderer), sourceBounds) {
                        BlurRadiusInLayoutUnits = radius
                    };
                    shapeLayoutInfo.Paths.Insert(0, item);
                }
            }
        }

        public void Visit(SolidColorReplacementEffect drawingEffect)
        {
        }

        public void Visit(TintEffect drawingEffect)
        {
        }

        public void Visit(TransformEffect drawingEffect)
        {
        }

        public bool RenderDirectly { get; set; }
    }
}

