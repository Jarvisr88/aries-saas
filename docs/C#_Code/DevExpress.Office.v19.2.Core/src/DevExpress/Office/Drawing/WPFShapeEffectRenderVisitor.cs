namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Extensions;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Effects;

    public class WPFShapeEffectRenderVisitor : IDrawingEffectVisitor
    {
        private ReflectionEffect reflectionInfo;
        private OuterShadowEffect shadow;
        private readonly IDocumentModel documentModel;
        private readonly IShapeGeometryInfo geometryInfo;
        private Brush parentFillBrush;
        private Pen parentOutlinePen;
        private Matrix rotateTransform;
        private double penThickness;

        public WPFShapeEffectRenderVisitor(IDocumentModel documentModel, IShapeGeometryInfo geometryInfo, Pen outlinePen, Matrix rotateTransform)
        {
            Guard.ArgumentNotNull(documentModel, "documentModel");
            Guard.ArgumentNotNull(geometryInfo, "geometry");
            this.documentModel = documentModel;
            this.geometryInfo = geometryInfo;
            this.parentOutlinePen = outlinePen;
            this.penThickness = (outlinePen == null) ? 0.0 : outlinePen.Thickness;
            this.rotateTransform = rotateTransform;
            this.Effects = new List<DrawingVisual>();
        }

        public DrawingVisual CalculateGroupReflection(ContainerVisual parent, Rect groupBounds, Rect widenedGroupGeometryBounds, Matrix rotateTransform) => 
            (this.reflectionInfo != null) ? this.CalculateReflectionCore(parent, groupBounds, widenedGroupGeometryBounds, rotateTransform) : null;

        public DrawingVisual CalculateGroupShadow(ContainerVisual parent, Rect parentBounds, Matrix rotateTransform) => 
            (this.shadow != null) ? this.CalculateShadowCore(parent, parentBounds, rotateTransform) : null;

        public DrawingVisual CalculateReflection(ContainerVisual parent)
        {
            if (this.reflectionInfo == null)
            {
                return null;
            }
            Rect widenedGeometryBounds = this.geometryInfo.GetTransformedWidenedBounds(this.parentOutlinePen, this.rotateTransform, 0.0);
            return this.CalculateReflectionCore(parent, this.Bounds, widenedGeometryBounds, this.rotateTransform);
        }

        private DrawingVisual CalculateReflectionCore(ContainerVisual parent, Rect shapeBounds, Rect widenedGeometryBounds, Matrix rotateTransform)
        {
            Rect descendantBounds = parent.DescendantBounds;
            Rect rectangle = descendantBounds;
            DrawingVisual visual = new DrawingVisual();
            float blurRadius = this.GetBlurRadius(this.reflectionInfo.OuterShadowEffectInfo.BlurRadius);
            if (blurRadius > 0f)
            {
                System.Windows.Media.Effects.BlurEffect effect1 = new System.Windows.Media.Effects.BlurEffect();
                effect1.Radius = blurRadius;
                visual.Effect = effect1;
                rectangle.Inflate((double) blurRadius, (double) blurRadius);
            }
            VisualBrush brush = this.CreateVisualContainerBrush(parent);
            using (DrawingContext context = visual.RenderOpen())
            {
                context.DrawRectangle(Brushes.Transparent, null, rectangle);
                context.DrawRectangle(brush, null, descendantBounds);
            }
            visual.OpacityMask = this.GetReflectionOpacityBrush(this.reflectionInfo.ReflectionOpacity, shapeBounds, rotateTransform);
            OuterShadowTransformationInfo info = this.CreateShadowTransformationInfo(this.reflectionInfo.OuterShadowEffectInfo, widenedGeometryBounds);
            visual.Transform = this.GetOuterShadowTransformation(info, rotateTransform);
            return visual;
        }

        public DrawingVisual CalculateShadow(ContainerVisual parent)
        {
            if (this.shadow == null)
            {
                return null;
            }
            Rect transformedBounds = this.geometryInfo.GetTransformedWidenedBounds(this.parentOutlinePen, this.rotateTransform, 0.0);
            return this.CalculateShadowCore(parent, transformedBounds, this.rotateTransform);
        }

        private DrawingVisual CalculateShadowCore(ContainerVisual parent, Rect transformedBounds, Matrix rotateTransform)
        {
            Color color = this.shadow.Color.FinalColor.ToWpfColor();
            if (this.BlackAndWhitePrintMode)
            {
                color = Color.FromArgb(color.A, 0, 0, 0);
            }
            SolidColorBrush brush = new SolidColorBrush(color);
            brush.Freeze();
            Rect descendantBounds = parent.DescendantBounds;
            Rect rectangle = descendantBounds;
            OuterShadowTransformationInfo info = this.CreateShadowTransformationInfo(this.shadow.Info, transformedBounds);
            DrawingVisual visual = new DrawingVisual();
            float blurRadius = this.GetBlurRadius(this.shadow.Info.BlurRadius);
            float num2 = Math.Min(Math.Abs(info.ScaleX), Math.Abs(info.ScaleY));
            if ((blurRadius > 0f) && (num2 > 0f))
            {
                blurRadius /= num2;
                System.Windows.Media.Effects.BlurEffect effect1 = new System.Windows.Media.Effects.BlurEffect();
                effect1.Radius = blurRadius;
                visual.Effect = effect1;
                rectangle.Inflate((double) blurRadius, (double) blurRadius);
            }
            using (DrawingContext context = visual.RenderOpen())
            {
                context.DrawRectangle(Brushes.Transparent, null, rectangle);
                context.DrawRectangle(brush, null, descendantBounds);
            }
            visual.OpacityMask = this.CreateVisualContainerBrush(parent);
            visual.Transform = this.GetOuterShadowTransformation(info, rotateTransform);
            return visual;
        }

        public void Clear()
        {
            this.Effects.Clear();
            this.reflectionInfo = null;
            this.shadow = null;
            this.IsConnectorShape = false;
            this.ParentIsRendered = false;
        }

        private Color CreateFillLayerColor(Color shadowColor, byte fillAlpha) => 
            Color.FromArgb(Convert.ToByte((float) ((fillAlpha - (((float) (fillAlpha * fillAlpha)) / 255f)) * (((float) shadowColor.A) / 255f))), shadowColor.R, shadowColor.G, shadowColor.B);

        private InnerShadowBrushesInfo CreateInnerShadowBrushes(Color shadowColor)
        {
            InnerShadowBrushesInfo brushesInfo = new InnerShadowBrushesInfo();
            if (this.parentFillBrush is SolidColorBrush)
            {
                byte a = ((SolidColorBrush) this.parentFillBrush).Color.A;
                this.PrepareSolidColorBrushes(brushesInfo, shadowColor, a);
            }
            else if (this.parentFillBrush is ImageBrush)
            {
                byte fillAlpha = Convert.ToByte((double) (((ImageBrush) this.parentFillBrush).Opacity * 255.0));
                this.PrepareSolidColorBrushes(brushesInfo, shadowColor, fillAlpha);
            }
            else if (this.parentFillBrush is GradientBrush)
            {
                this.PrepareGradientBrushes(brushesInfo, (GradientBrush) this.parentFillBrush, shadowColor);
            }
            else
            {
                brushesInfo.ShadowBrush = this.CreateSolidColorBrush(shadowColor);
            }
            return brushesInfo;
        }

        private System.Windows.Media.Geometry CreateInnerShadowGeometry(OffsetShadowInfo shadowOffset, float blurRadius)
        {
            Rect bounds = this.Bounds;
            bounds.Inflate((double) blurRadius, (double) blurRadius);
            System.Windows.Media.Geometry geometry = new RectangleGeometry(bounds);
            float num = this.ToLayoutUnitConverter.ToLayoutUnits((float) this.UnitConverter.EmuToModelUnitsL(shadowOffset.Distance));
            double a = DrawingValueConverter.DegreeToRadian(DrawingValueConverter.FromPositiveFixedAngle(shadowOffset.Direction));
            float num4 = (float) (num * Math.Sin(a));
            System.Windows.Media.Geometry geometry2 = this.Geometry.Clone();
            geometry2.Transform = new TranslateTransform((double) -((float) (num * Math.Cos(a))), (double) -num4);
            return System.Windows.Media.Geometry.Combine(geometry, geometry2, GeometryCombineMode.Exclude, Transform.Identity);
        }

        private Color CreateShadowLayerColor(Color shadowColor, byte fillAlpha, byte fillLayerAlpha)
        {
            byte num = Convert.ToByte((float) (255f - (((float) ((0xff - fillLayerAlpha) * (0xff - fillAlpha))) / 255f)));
            return Color.FromArgb(Convert.ToByte((float) (255f - (((0xff - fillAlpha) * (255f - (fillAlpha * (((float) shadowColor.A) / 255f)))) / ((float) (0xff - num))))), shadowColor.R, shadowColor.G, shadowColor.B);
        }

        private OuterShadowTransformationInfo CreateShadowTransformationInfo(OuterShadowEffectInfo shadowInfo, Rect bounds) => 
            new OuterShadowTransformationCalculator(this.UnitConverter, this.ToLayoutUnitConverter).Calculate(bounds.ToRectangle(), shadowInfo);

        private SolidColorBrush CreateSolidColorBrush(Color color)
        {
            SolidColorBrush brush = new SolidColorBrush(color);
            brush.Freeze();
            return brush;
        }

        private VisualBrush CreateVisualContainerBrush(ContainerVisual container) => 
            new VisualBrush(container) { 
                Stretch = Stretch.None,
                Viewbox = container.DescendantBounds,
                ViewboxUnits = BrushMappingMode.Absolute
            };

        void IDrawingEffectVisitor.AlphaCeilingEffectVisit()
        {
        }

        void IDrawingEffectVisitor.AlphaFloorEffectVisit()
        {
        }

        void IDrawingEffectVisitor.GrayScaleEffectVisit()
        {
        }

        void IDrawingEffectVisitor.Visit(AlphaBiLevelEffect drawingEffect)
        {
        }

        void IDrawingEffectVisitor.Visit(AlphaInverseEffect drawingEffect)
        {
        }

        void IDrawingEffectVisitor.Visit(AlphaModulateEffect drawingEffect)
        {
        }

        void IDrawingEffectVisitor.Visit(AlphaModulateFixedEffect drawingEffect)
        {
        }

        void IDrawingEffectVisitor.Visit(AlphaOutsetEffect drawingEffect)
        {
        }

        void IDrawingEffectVisitor.Visit(AlphaReplaceEffect drawingEffect)
        {
        }

        void IDrawingEffectVisitor.Visit(BiLevelEffect drawingEffect)
        {
        }

        void IDrawingEffectVisitor.Visit(BlendEffect drawingEffect)
        {
        }

        void IDrawingEffectVisitor.Visit(DevExpress.Office.Drawing.BlurEffect drawingEffect)
        {
            if ((this.parentFillBrush != null) || (this.parentOutlinePen != null))
            {
                float blurRadius = this.GetBlurRadius(drawingEffect.Radius);
                if (blurRadius != 0f)
                {
                    bool grow = drawingEffect.Grow;
                    this.BlurEffectSize = 1.7f * blurRadius;
                    this.GlowCroppedByBlur = !grow;
                    Rect boundsWithPenWidth = this.GetBoundsWithPenWidth();
                    if (grow)
                    {
                        boundsWithPenWidth.Inflate((double) blurRadius, (double) blurRadius);
                    }
                    DrawingVisual visual = new DrawingVisual {
                        Clip = new RectangleGeometry(boundsWithPenWidth)
                    };
                    System.Windows.Media.Effects.BlurEffect effect1 = new System.Windows.Media.Effects.BlurEffect();
                    effect1.Radius = blurRadius;
                    visual.Effect = effect1;
                    int count = this.Effects.Count;
                    if (!this.ParentIsRendered || (count <= 0))
                    {
                        this.DrawParent(visual, boundsWithPenWidth);
                    }
                    else
                    {
                        DrawingVisual item = this.Effects[count - 1];
                        using (DrawingContext context = visual.RenderOpen())
                        {
                            context.DrawRectangle(Brushes.Transparent, null, boundsWithPenWidth);
                        }
                        this.Effects.Remove(item);
                        visual.Children.Add(item);
                    }
                    this.Effects.Add(visual);
                }
            }
        }

        void IDrawingEffectVisitor.Visit(ColorChangeEffect drawingEffect)
        {
        }

        void IDrawingEffectVisitor.Visit(ContainerEffect drawingEffect)
        {
        }

        void IDrawingEffectVisitor.Visit(DuotoneEffect drawingEffect)
        {
        }

        void IDrawingEffectVisitor.Visit(DevExpress.Office.Drawing.Effect drawingEffect)
        {
        }

        void IDrawingEffectVisitor.Visit(FillEffect drawingEffect)
        {
        }

        void IDrawingEffectVisitor.Visit(FillOverlayEffect drawingEffect)
        {
        }

        void IDrawingEffectVisitor.Visit(GlowEffect glowInfo)
        {
            if ((this.parentFillBrush != null) || (this.parentOutlinePen != null))
            {
                float blurRadius = this.GetBlurRadius(glowInfo.Radius);
                Color glowColor = glowInfo.Color.FinalColor.ToWpfColor();
                if ((blurRadius != 0f) && (glowColor.A != 0))
                {
                    if (this.BlackAndWhitePrintMode)
                    {
                        glowColor = Color.FromArgb(glowColor.A, 0xff, 0xff, 0xff);
                    }
                    Rect boundsWithPenWidth = this.GetBoundsWithPenWidth();
                    double num2 = blurRadius + this.BlurEffectSize;
                    boundsWithPenWidth.Inflate((double) (num2 + this.GlowEffectSize), (double) (num2 + this.GlowEffectSize));
                    num2 += this.GlowEffectSize * 1.8;
                    num2 -= Math.Min(num2, (double) (this.SoftEdgesSize / 4f));
                    this.GlowEffectSize += blurRadius;
                    DrawingVisual container = new DrawingVisual {
                        Clip = this.GetGlowClipGeometry(blurRadius)
                    };
                    using (DrawingContext context = container.RenderOpen())
                    {
                        context.DrawRectangle(Brushes.Transparent, null, boundsWithPenWidth);
                        this.geometryInfo.DrawGlow(context, this.parentOutlinePen, this.parentFillBrush, glowColor, num2);
                    }
                    DrawingVisual item = new DrawingVisual();
                    using (DrawingContext context2 = item.RenderOpen())
                    {
                        context2.DrawRectangle(this.CreateVisualContainerBrush(container), null, boundsWithPenWidth);
                    }
                    System.Windows.Media.Effects.BlurEffect effect1 = new System.Windows.Media.Effects.BlurEffect();
                    effect1.Radius = blurRadius / 2f;
                    item.Effect = effect1;
                    this.Effects.Insert(0, item);
                }
            }
        }

        void IDrawingEffectVisitor.Visit(HSLEffect drawingEffect)
        {
        }

        void IDrawingEffectVisitor.Visit(InnerShadowEffect shadowInfo)
        {
            if ((this.parentFillBrush != null) && !this.IsConnectorShape)
            {
                Color shadowColor = shadowInfo.Color.FinalColor.ToWpfColor();
                if (shadowColor.A != 0)
                {
                    if (this.BlackAndWhitePrintMode)
                    {
                        shadowColor = Color.FromArgb(shadowColor.A, 0, 0, 0);
                    }
                    float blurRadius = this.GetBlurRadius(shadowInfo.BlurRadius);
                    System.Windows.Media.Geometry geometry = this.Geometry;
                    System.Windows.Media.Geometry geometry2 = this.CreateInnerShadowGeometry(shadowInfo.OffsetShadow, blurRadius);
                    DrawingVisual item = new DrawingVisual();
                    DrawingVisual visual = new DrawingVisual();
                    this.DrawParent(visual);
                    item.Children.Add(visual);
                    InnerShadowBrushesInfo info = this.CreateInnerShadowBrushes(shadowColor);
                    if (info.FillBrush != null)
                    {
                        if (info.IsGradientFill)
                        {
                            visual.OpacityMask = info.FillBrush;
                        }
                        else
                        {
                            DrawingVisual visual4 = new DrawingVisual();
                            using (DrawingContext context = visual4.RenderOpen())
                            {
                                this.geometryInfo.Draw(context, null, info.FillBrush);
                            }
                            visual.Children.Add(visual4);
                        }
                    }
                    DrawingVisual visual3 = new DrawingVisual {
                        Clip = geometry
                    };
                    using (DrawingContext context2 = visual3.RenderOpen())
                    {
                        context2.DrawGeometry(info.ShadowBrush, null, geometry2);
                    }
                    if (blurRadius > 0f)
                    {
                        System.Windows.Media.Effects.BlurEffect effect1 = new System.Windows.Media.Effects.BlurEffect();
                        effect1.Radius = blurRadius;
                        visual3.Effect = effect1;
                    }
                    visual.Children.Add(visual3);
                    if (this.parentOutlinePen != null)
                    {
                        DrawingVisual visual5 = new DrawingVisual();
                        using (DrawingContext context3 = visual5.RenderOpen())
                        {
                            this.geometryInfo.Draw(context3, this.parentOutlinePen, null);
                        }
                        visual.Children.Add(visual5);
                    }
                    this.Effects.Add(item);
                }
            }
        }

        void IDrawingEffectVisitor.Visit(LuminanceEffect drawingEffect)
        {
        }

        void IDrawingEffectVisitor.Visit(OuterShadowEffect shadow)
        {
            this.shadow = shadow;
        }

        void IDrawingEffectVisitor.Visit(PresetShadowEffect drawingEffect)
        {
        }

        void IDrawingEffectVisitor.Visit(ReflectionEffect reflectionInfo)
        {
            this.reflectionInfo = reflectionInfo;
        }

        void IDrawingEffectVisitor.Visit(RelativeOffsetEffect drawingEffect)
        {
        }

        void IDrawingEffectVisitor.Visit(SoftEdgeEffect softEdgesInfo)
        {
            float blurRadius = this.GetBlurRadius(softEdgesInfo.Radius);
            if (blurRadius != 0f)
            {
                this.SoftEdgesSize = blurRadius;
                System.Windows.Media.Geometry widenedByPenGeometry = this.geometryInfo.GetWidenedByPenGeometry(this.parentOutlinePen);
                System.Windows.Media.Geometry geometry = System.Windows.Media.Geometry.Combine(widenedByPenGeometry, widenedByPenGeometry.GetWidenedPathGeometry(new Pen(Brushes.Black, 1.8 * blurRadius)), GeometryCombineMode.Exclude, Transform.Identity);
                DrawingVisual visual = new DrawingVisual();
                using (DrawingContext context = visual.RenderOpen())
                {
                    context.DrawGeometry(Brushes.Black, null, geometry);
                }
                System.Windows.Media.Effects.BlurEffect effect1 = new System.Windows.Media.Effects.BlurEffect();
                effect1.Radius = blurRadius;
                visual.Effect = effect1;
                VisualBrush brush = new VisualBrush(visual) {
                    Stretch = Stretch.None
                };
                int count = this.Effects.Count;
                if (this.ParentIsRendered && (count > 0))
                {
                    this.Effects[count - 1].OpacityMask = brush;
                }
                else
                {
                    DrawingVisual visual3 = new DrawingVisual {
                        OpacityMask = brush
                    };
                    this.DrawParent(visual3);
                    this.Effects.Add(visual3);
                }
            }
        }

        void IDrawingEffectVisitor.Visit(SolidColorReplacementEffect drawingEffect)
        {
        }

        void IDrawingEffectVisitor.Visit(TintEffect drawingEffect)
        {
        }

        void IDrawingEffectVisitor.Visit(TransformEffect drawingEffect)
        {
        }

        private void DrawParent(DrawingVisual visual)
        {
            this.DrawParent(visual, Rect.Empty);
        }

        private void DrawParent(DrawingVisual visual, Rect bounds)
        {
            using (DrawingContext context = visual.RenderOpen())
            {
                context.DrawRectangle(Brushes.Transparent, null, bounds);
                this.geometryInfo.Draw(context, this.parentOutlinePen, this.parentFillBrush);
            }
            this.ParentIsRendered = true;
        }

        private float GetBlurRadius(long radius) => 
            Math.Max(0f, this.ToLayoutUnitConverter.ToLayoutUnits((float) this.UnitConverter.EmuToModelUnitsL(radius)));

        private Rect GetBoundsWithPenWidth()
        {
            Rect bounds = this.Bounds;
            double width = this.penThickness / 2.0;
            bounds.Inflate(width, width);
            return bounds;
        }

        private System.Windows.Media.Geometry GetGlowClipGeometry(float blurRadius)
        {
            if (!this.GlowCroppedByBlur)
            {
                return null;
            }
            Rect bounds = this.Bounds;
            double width = (blurRadius + this.penThickness) / 2.0;
            bounds.Inflate(width, width);
            return new RectangleGeometry(bounds);
        }

        private Transform GetOuterShadowTransformation(OuterShadowTransformationInfo info, Matrix rotateTransform)
        {
            Matrix matrix = new Matrix((double) info.ScaleX, (double) info.SkewY, (double) info.SkewX, (double) info.ScaleY, (double) info.OffsetX, (double) info.OffsetY);
            if (!rotateTransform.IsIdentity)
            {
                matrix = Matrix.Multiply(rotateTransform, matrix);
                rotateTransform.Invert();
                matrix = Matrix.Multiply(matrix, rotateTransform);
            }
            return new MatrixTransform(matrix);
        }

        private Brush GetReflectionOpacityBrush(ReflectionOpacityInfo opacityInfo, Rect bounds, Matrix rotateTransform)
        {
            double num5 = DrawingValueConverter.FromPositiveFixedAngle(opacityInfo.FadeDirection);
            LinearGradientBrush brush = new LinearGradientBrush();
            Color color = new Color {
                A = Convert.ToByte((double) (DrawingValueConverter.FromPercentage(opacityInfo.StartOpacity) * 255.0))
            };
            brush.GradientStops.Add(new GradientStop(color, DrawingValueConverter.FromPercentage(opacityInfo.StartPosition)));
            color = new Color {
                A = Convert.ToByte((double) (DrawingValueConverter.FromPercentage(opacityInfo.EndOpacity) * 255.0))
            };
            brush.GradientStops.Add(new GradientStop(color, DrawingValueConverter.FromPercentage(opacityInfo.EndPosition)));
            rotateTransform.Invert();
            MatrixTransform transform = new MatrixTransform(rotateTransform);
            Rect rect = transform.TransformBounds(bounds);
            double height = bounds.Height;
            double width = bounds.Width;
            double num8 = (rect.Height - height) / (2.0 * height);
            double num9 = (rect.Width - width) / (2.0 * width);
            brush.StartPoint = new Point(0.5 + num9, 1.0 + num8);
            brush.EndPoint = new Point(0.5 - num9, -num8);
            brush.Transform = transform;
            return brush;
        }

        public Rect GetTransformedGeometryBounds(Matrix transform) => 
            this.geometryInfo.GetTransformedBounds(transform);

        public Rect GetTransformedGeometryBoundsWithEffects(Matrix transform) => 
            this.geometryInfo.GetTransformedWidenedBounds(this.parentOutlinePen, transform, 2.0 * this.GlowEffectSize);

        private void PrepareGradientBrushes(InnerShadowBrushesInfo brushesInfo, GradientBrush parentFillBrush, Color shadowColor)
        {
            System.Windows.Media.GradientStopCollection stops = new System.Windows.Media.GradientStopCollection();
            bool flag = false;
            int count = parentFillBrush.GradientStops.Count;
            for (int i = 0; i < count; i++)
            {
                GradientStop stop = parentFillBrush.GradientStops[i];
                Color color = stop.Color;
                byte a = color.A;
                if (a < 0xff)
                {
                    flag = true;
                }
                stops.Add(new GradientStop(Color.FromArgb(a, 0xff, 0xff, 0xff), stop.Offset));
            }
            if (flag)
            {
                GradientBrush brush = parentFillBrush.Clone();
                brush.GradientStops = stops;
                brush.Freeze();
                brushesInfo.FillBrush = brush;
                brushesInfo.IsGradientFill = true;
            }
            brushesInfo.ShadowBrush = this.CreateSolidColorBrush(shadowColor);
        }

        private void PrepareSolidColorBrushes(InnerShadowBrushesInfo brushesInfo, Color shadowColor, byte fillAlpha)
        {
            Color color;
            if (fillAlpha >= 0xff)
            {
                color = shadowColor;
            }
            else
            {
                Color color2 = this.CreateFillLayerColor(shadowColor, fillAlpha);
                brushesInfo.FillBrush = this.CreateSolidColorBrush(color2);
                color = this.CreateShadowLayerColor(shadowColor, fillAlpha, color2.A);
            }
            brushesInfo.ShadowBrush = this.CreateSolidColorBrush(color);
        }

        private float BlurEffectSize { get; set; }

        private bool GlowCroppedByBlur { get; set; }

        private double GlowEffectSize { get; set; }

        private float SoftEdgesSize { get; set; }

        public List<DrawingVisual> Effects { get; private set; }

        public bool ParentIsRendered { get; private set; }

        public bool IsConnectorShape { get; set; }

        public bool HasShadow =>
            this.shadow != null;

        public bool HasReflection =>
            this.reflectionInfo != null;

        public Brush ParentFillBrush
        {
            get => 
                this.parentFillBrush;
            set => 
                this.parentFillBrush = value;
        }

        protected System.Windows.Media.Geometry Geometry =>
            this.geometryInfo.Geometry;

        protected Rect Bounds =>
            this.geometryInfo.Bounds;

        public bool BlackAndWhitePrintMode { get; set; }

        private DocumentModelUnitConverter UnitConverter =>
            this.documentModel.UnitConverter;

        private DocumentModelUnitToLayoutUnitConverter ToLayoutUnitConverter =>
            this.documentModel.ToDocumentLayoutUnitConverter;

        private class InnerShadowBrushesInfo
        {
            public Brush FillBrush { get; set; }

            public Brush ShadowBrush { get; set; }

            public bool IsGradientFill { get; set; }
        }
    }
}

