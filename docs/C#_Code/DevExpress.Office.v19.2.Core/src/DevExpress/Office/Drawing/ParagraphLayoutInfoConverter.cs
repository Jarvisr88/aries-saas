namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    internal abstract class ParagraphLayoutInfoConverter : IDisposable
    {
        private readonly List<ParagraphLayoutInfo> paragraphLayouts;
        private readonly PathInfoCollection textPathInfos;
        private readonly PathInfoCollection linesPathInfos;
        private readonly int[] bucketsLast;
        private readonly List<BucketElement> bucketTextPathInfos;

        protected ParagraphLayoutInfoConverter(List<ParagraphLayoutInfo> paragraphLayouts, DevExpress.Office.Drawing.ShapeStyle shapeStyle, GraphicsWarpTransformer warpTransformer, bool shouldApplyEffects)
        {
            this.ShouldApplyEffects = shouldApplyEffects;
            this.paragraphLayouts = paragraphLayouts;
            this.ShapeStyle = shapeStyle;
            this.textPathInfos = new PathInfoCollection();
            this.linesPathInfos = new PathInfoCollection();
            this.<TextShapeLayoutInfo>k__BackingField = new DevExpress.Office.Drawing.TextShapeLayoutInfo(this);
            this.<WarpTransformer>k__BackingField = warpTransformer;
            this.bucketsLast = new int[this.BucketsCount];
            this.FillParagraphsBounds();
            this.bucketTextPathInfos = new List<BucketElement>();
            this.BucketLinesPathInfos = new List<BucketElement>();
        }

        protected ParagraphLayoutInfoConverter(List<ParagraphLayoutInfo> paragraphLayouts, DevExpress.Office.Drawing.ShapeStyle shapeStyle, GraphicsWarpTransformer warpTransformer, bool shouldApplyEffects, bool blackAndWhitePrintMode) : this(paragraphLayouts, shapeStyle, warpTransformer, shouldApplyEffects)
        {
            BlackAndWhitePrintMode = blackAndWhitePrintMode;
        }

        protected abstract void AddUnderlines(List<UnderlineInfo> underlines, RectangleF paragraphBounds, float excelBaseLine);
        private void ApplyEffects(BucketElement bucketElement, TextRunPathInfo runPathInfo, PathInfoCollection pathInfoList)
        {
            ContainerEffect effects = bucketElement.Effects;
            this.CurrentBucketElement = bucketElement;
            this.TextShapeLayoutInfo.PenInfo = runPathInfo.PenInfo;
            this.TextShapeLayoutInfo.Paths.Add(runPathInfo);
            if (this.ShouldApplyEffects && (effects != null))
            {
                new ShapeEffectRenderingWalker(effects.Effects, this.DocumentModel, this.TextShapeLayoutInfo, BlackAndWhitePrintMode) { RenderDirectly = true }.ApplyEffects();
            }
            pathInfoList.AddRange(this.TextShapeLayoutInfo.Paths);
            this.TextShapeLayoutInfo.Reset();
            this.CurrentBucketElement = null;
        }

        private GraphicsPath ApplyWarp(GraphicsPath graphicsPath, int bucketIndex, RectangleF bucketBounds) => 
            (this.WarpTransformer != null) ? ((graphicsPath.PointCount != 0) ? this.WarpTransformer.TransformGraphics(graphicsPath, bucketBounds, bucketIndex) : graphicsPath) : graphicsPath;

        protected bool AreUnderlinesNotSame(UnderlineInfo a, UnderlineInfo b)
        {
            IDrawingTextCharacterProperties properties = b.RunLayoutInfo.Properties;
            IDrawingTextCharacterProperties properties2 = a.RunLayoutInfo.Properties;
            return ((properties2.Underline != properties.Underline) || (!Equals(properties2.Effects, properties.Effects) || (!Equals(properties2.Fill, properties.Fill) || (!Equals(properties2.UnderlineFill, properties.UnderlineFill) || !Equals(properties2.Outline, properties.Outline)))));
        }

        protected abstract void BeforeCloseBucket(List<BucketElement> bucketLinesPathInfos);
        protected float CalcExcelBaseLine(FontInfo fontInfo) => 
            (float) (fontInfo.CalculatedLineSpacing - fontInfo.Descent);

        protected float CalcGdiBaseLine(FontInfo fontInfo)
        {
            Font font = fontInfo.Font;
            FontFamily fontFamily = font.FontFamily;
            int lineSpacing = fontFamily.GetLineSpacing(font.Style);
            int cellAscent = fontFamily.GetCellAscent(font.Style);
            return (((float) (fontInfo.LineSpacing * cellAscent)) / ((float) lineSpacing));
        }

        protected abstract float CalcRealBaseLine(RunLayoutInfo runLayout, float excelBaseLine);
        protected abstract float CalculateExcelBaseLine(ParagraphLayoutInfo paragraphLayoutInfo);
        private static RectangleF CalculateParagraphBounds(ParagraphLayoutInfo paragraphLayout)
        {
            if (paragraphLayout.RunLayouts.Count == 0)
            {
                return RectangleF.Empty;
            }
            RectangleF bounds = paragraphLayout.RunLayouts[0].Bounds;
            for (int i = 1; i < paragraphLayout.RunLayouts.Count; i++)
            {
                RunLayoutInfo info = paragraphLayout.RunLayouts[i];
                bounds = RectangleF.Union(bounds, info.Bounds);
            }
            if (paragraphLayout.Bullet != null)
            {
                bounds = RectangleF.Union(bounds, paragraphLayout.Bullet.Bounds);
            }
            return bounds;
        }

        private void CloseBucket(int bucketIndex)
        {
            this.BeforeCloseBucket(this.bucketTextPathInfos);
            this.BeforeCloseBucket(this.BucketLinesPathInfos);
            RectangleF bucketBounds = this.GetBucketBounds(this.bucketTextPathInfos);
            RectangleF b = this.GetBucketBounds(this.BucketLinesPathInfos);
            RectangleF ef3 = bucketBounds.IsEmpty ? b : (b.IsEmpty ? bucketBounds : RectangleF.Union(bucketBounds, b));
            this.ConvertBucketPathInfos(this.bucketTextPathInfos, this.textPathInfos, bucketIndex, ef3);
            this.ConvertBucketPathInfos(this.BucketLinesPathInfos, this.linesPathInfos, bucketIndex, ef3);
            this.bucketTextPathInfos.Clear();
            this.BucketLinesPathInfos.Clear();
        }

        public List<PathInfoBase> Convert()
        {
            int index = 0;
            for (int i = 0; i < this.paragraphLayouts.Count; i++)
            {
                ParagraphLayoutInfo paragraphLayout = this.paragraphLayouts[i];
                RectangleF paragraphBounds = CalculateParagraphBounds(paragraphLayout);
                this.ProcessParagraph(paragraphLayout, paragraphBounds);
                if (i == this.bucketsLast[index])
                {
                    this.CloseBucket(index);
                    index++;
                }
            }
            this.textPathInfos.AddRange(this.linesPathInfos);
            this.TextShapeLayoutInfo.Dispose();
            return this.textPathInfos;
        }

        private void ConvertBucketPathInfos(List<BucketElement> bucketPathInfos, PathInfoCollection pathInfoList, int bucketIndex, RectangleF bucketBounds)
        {
            foreach (BucketElement element in bucketPathInfos)
            {
                TextRunPathInfo runPathInfo = element.RunPathInfo;
                GraphicsPath graphicsPath = this.ApplyWarp(runPathInfo.GraphicsPath, bucketIndex, bucketBounds);
                PenInfo penInfo = runPathInfo.PenInfo;
                this.ApplyEffects(element, (runPathInfo is TextLinePathInfo) ? new TextLinePathInfo(graphicsPath, runPathInfo.Fill, penInfo) : new TextRunPathInfo(graphicsPath, runPathInfo.Fill, penInfo), pathInfoList);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && (this.TextShapeLayoutInfo != null))
            {
                this.TextShapeLayoutInfo.Dispose();
            }
        }

        private void FillParagraphsBounds()
        {
            int count = this.paragraphLayouts.Count;
            int bucketsCount = this.BucketsCount;
            int num3 = 0;
            for (int i = 0; i < bucketsCount; i++)
            {
                int num5 = ((count + bucketsCount) - 1) / bucketsCount;
                count -= num5;
                this.bucketsLast[i] = (num3 + num5) - 1;
                num3 += num5;
            }
        }

        private static Brush GetBrushByFill(IDrawingFill fill, RectangleF paragraphBounds, DevExpress.Office.Drawing.ShapeStyle shapeStyle)
        {
            Brush result;
            if (BlackAndWhitePrintMode)
            {
                return new SolidBrush(Color.Black);
            }
            using (GraphicsPath path = new GraphicsPath(FillMode.Winding))
            {
                using (Matrix matrix = new Matrix())
                {
                    path.AddRectangle(paragraphBounds);
                    Brush defaultBrush = null;
                    if (!shapeStyle.FontColor.IsEmpty)
                    {
                        defaultBrush = new SolidBrush(shapeStyle.FontColor.FinalColor);
                    }
                    if (fill.FillType != DrawingFillType.Automatic)
                    {
                        List<GraphicsPath> graphicsPaths = new List<GraphicsPath>();
                        graphicsPaths.Add(path);
                        ShapeFillRenderVisitor visitor = new ShapeFillRenderVisitor(ShapePreset.Rect, Color.Empty, defaultBrush, graphicsPaths, matrix);
                        fill.Visit(visitor);
                        if (defaultBrush != null)
                        {
                            defaultBrush.Dispose();
                        }
                        result = visitor.Result;
                    }
                    else if (shapeStyle.FontColor.IsEmpty || !shapeStyle.FontColor.FinalColor.IsEmpty)
                    {
                        result = defaultBrush;
                    }
                    else
                    {
                        if (defaultBrush != null)
                        {
                            defaultBrush.Dispose();
                        }
                        result = new SolidBrush(shapeStyle.DocumentModel.OfficeTheme.Colors.Dark1.FinalColor);
                    }
                }
            }
            return result;
        }

        protected abstract RectangleF GetBrushPenBounds(RectangleF paragraphBounds, RectangleF runLayoutBounds);
        private RectangleF GetBucketBounds(List<BucketElement> bucket)
        {
            if (bucket.Count == 0)
            {
                return RectangleF.Empty;
            }
            RectangleF bounds = bucket[0].RunPathInfo.GraphicsPath.GetBounds();
            for (int i = 1; i < bucket.Count; i++)
            {
                RectangleF b = bucket[i].RunPathInfo.GraphicsPath.GetBounds();
                if (!b.IsEmpty)
                {
                    bounds = RectangleF.Union(bounds, b);
                }
            }
            return bounds;
        }

        public abstract float GetCurrentBaseLine();
        public RectangleF GetCurrentRunBounds()
        {
            BucketElement currentBucketElement = this.CurrentBucketElement;
            if (currentBucketElement != null)
            {
                return currentBucketElement.Bounds;
            }
            BucketElement local1 = currentBucketElement;
            return Rectangle.Empty;
        }

        private static Brush GetRunBrush(IDrawingTextCharacterProperties runProperties, RectangleF paragraphBounds, DevExpress.Office.Drawing.ShapeStyle shapeStyle)
        {
            IDrawingFill fill = runProperties.Fill;
            return ((fill != null) ? GetBrushByFill(fill, paragraphBounds, shapeStyle) : null);
        }

        private RunGraphicsInfo GetRunGraphicsPaths(RunLayoutInfo runLayout, float excelBaseLine)
        {
            float realBaseLine = this.CalcRealBaseLine(runLayout, excelBaseLine);
            string text = runLayout.Text;
            GraphicsPath textGraphicsPath = this.GetRunTextGraphicsPath(runLayout, text, runLayout.RunFontInfo.Font, realBaseLine);
            return new RunGraphicsInfo(textGraphicsPath, (runLayout.Properties == null) ? new List<GraphicsPath>() : this.GetStrikethroughGraphicsPaths(runLayout, realBaseLine, textGraphicsPath), ((runLayout.Properties == null) || (runLayout.Properties.Underline == DrawingTextUnderlineType.None)) ? null : new UnderlineInfo(runLayout, realBaseLine, textGraphicsPath));
        }

        private static PenInfo GetRunPen(IDrawingTextCharacterProperties properties, RectangleF paragraphBounds, DevExpress.Office.Drawing.ShapeStyle shapeStyle)
        {
            if (properties.Outline.IsDefault)
            {
                return null;
            }
            using (GraphicsPath path = new GraphicsPath(FillMode.Winding))
            {
                path.AddRectangle(paragraphBounds);
                List<GraphicsPath> graphicsPaths = new List<GraphicsPath>();
                graphicsPaths.Add(path);
                return ShapeRenderHelper.GetPenInfo(properties.Outline, shapeStyle, graphicsPaths, ShapePreset.Rect, BlackAndWhitePrintMode);
            }
        }

        protected abstract GraphicsPath GetRunTextGraphicsPath(RunLayoutInfo runLayout, string text, Font font, float realBaseLine);
        protected abstract List<GraphicsPath> GetStrikethroughGraphicsPaths(RunLayoutInfo runLayout, float realBaseLine, GraphicsPath runTextGraphicsPath);
        protected static Brush GetUnderlineBrush(IDrawingTextCharacterProperties runProperties, RectangleF bounds, DevExpress.Office.Drawing.ShapeStyle shapeStyle, Brush runBrush)
        {
            if ((runProperties.UnderlineFill == null) || (runProperties.UnderlineFill.Type == DrawingUnderlineFillType.FollowsText))
            {
                return runBrush;
            }
            IDrawingFill underlineFill = runProperties.UnderlineFill as IDrawingFill;
            return (((underlineFill == null) || (underlineFill.FillType == DrawingFillType.Automatic)) ? GetRunBrush(runProperties, bounds, shapeStyle) : GetBrushByFill(underlineFill, bounds, shapeStyle));
        }

        protected void NormalizeHeight(GraphicsPath graphicsPath, RunLayoutInfo runLayout, float realBaseLine)
        {
            RectangleF bounds = graphicsPath.GetBounds();
            if ((bounds.Width != 0f) && (bounds.Height != 0f))
            {
                float num = this.CalcExcelBaseLine(runLayout.BaseFontInfo);
                PointF[] destPoints = new PointF[] { new PointF(bounds.Left, realBaseLine - num), new PointF(bounds.Right, realBaseLine - num), new PointF(bounds.Left, realBaseLine) };
                graphicsPath.Warp(destPoints, bounds);
            }
        }

        private void ProcessBullet(ParagraphLayoutInfo paragraphLayoutInfo, RectangleF paragraphBounds, float excelBaseLine)
        {
            RunLayoutInfo bullet = paragraphLayoutInfo.Bullet;
            if (bullet != null)
            {
                RunGraphicsInfo runGraphicsPaths = this.GetRunGraphicsPaths(bullet, excelBaseLine);
                Brush fill = (paragraphLayoutInfo.BulletColor == null) ? ((paragraphLayoutInfo.BulletFill != null) ? GetBrushByFill(paragraphLayoutInfo.BulletFill, paragraphBounds, this.ShapeStyle) : new SolidBrush(Color.White)) : new SolidBrush(paragraphLayoutInfo.BulletColor.FinalColor);
                this.bucketTextPathInfos.Add(new BucketElement(new TextRunPathInfo(runGraphicsPaths.TextGraphicsPath, fill, null), bullet.Bounds, null, excelBaseLine));
            }
        }

        private void ProcessParagraph(ParagraphLayoutInfo paragraphLayoutInfo, RectangleF paragraphBounds)
        {
            float excelBaseLine = this.CalculateExcelBaseLine(paragraphLayoutInfo);
            List<UnderlineInfo> underlines = new List<UnderlineInfo>();
            this.ProcessBullet(paragraphLayoutInfo, paragraphBounds, excelBaseLine);
            foreach (RunLayoutInfo info in paragraphLayoutInfo.RunLayouts)
            {
                RectangleF brushPenBounds = this.GetBrushPenBounds(paragraphBounds, info.Bounds);
                RunGraphicsInfo runGraphicsPaths = this.GetRunGraphicsPaths(info, excelBaseLine);
                Brush fill = GetRunBrush(info.Properties, brushPenBounds, this.ShapeStyle);
                PenInfo penInfo = GetRunPen(info.Properties, brushPenBounds, this.ShapeStyle);
                this.bucketTextPathInfos.Add(new BucketElement(new TextRunPathInfo(runGraphicsPaths.TextGraphicsPath, fill, penInfo), info.Bounds, info.Properties.Effects, excelBaseLine));
                if (runGraphicsPaths.StrikeThroughGraphicsPaths != null)
                {
                    foreach (GraphicsPath path in runGraphicsPaths.StrikeThroughGraphicsPaths)
                    {
                        this.BucketLinesPathInfos.Add(new BucketElement(new TextLinePathInfo(path, fill, penInfo), info.Bounds, info.Properties.Effects, excelBaseLine));
                    }
                }
                if ((runGraphicsPaths.UnderlineInfo == null) || ((underlines.Count != 0) && (Sign((float) info.Properties.Baseline) != Sign((float) underlines[underlines.Count - 1].RunLayoutInfo.Properties.Baseline))))
                {
                    this.AddUnderlines(underlines, paragraphBounds, excelBaseLine);
                    underlines.Clear();
                }
                if (runGraphicsPaths.UnderlineInfo != null)
                {
                    runGraphicsPaths.UnderlineInfo.TextBrush = fill;
                    runGraphicsPaths.UnderlineInfo.PenInfo = penInfo;
                    underlines.Add(runGraphicsPaths.UnderlineInfo);
                }
            }
            if (underlines.Count != 0)
            {
                this.AddUnderlines(underlines, paragraphBounds, excelBaseLine);
            }
        }

        private static int Sign(float value) => 
            (value < 0f) ? -1 : 1;

        private bool ShouldApplyEffects { get; set; }

        protected BucketElement CurrentBucketElement { get; private set; }

        protected DevExpress.Office.Drawing.ShapeStyle ShapeStyle { get; private set; }

        protected IDocumentModel DocumentModel =>
            this.ShapeStyle.DocumentModel;

        protected DevExpress.Office.Drawing.TextShapeLayoutInfo TextShapeLayoutInfo { get; }

        protected GraphicsWarpTransformer WarpTransformer { get; }

        protected List<BucketElement> BucketLinesPathInfos { get; private set; }

        private int BucketsCount
        {
            get
            {
                GraphicsWarpTransformer warpTransformer = this.WarpTransformer;
                if (warpTransformer != null)
                {
                    return warpTransformer.PathPairsCount;
                }
                GraphicsWarpTransformer local1 = warpTransformer;
                return 1;
            }
        }

        private static bool BlackAndWhitePrintMode { get; set; }

        protected class BucketElement
        {
            public BucketElement(TextRunPathInfo runPathInfo, RectangleF bounds, ContainerEffect effects, float runBaseLine)
            {
                this.RunPathInfo = runPathInfo;
                this.Bounds = bounds;
                this.Effects = effects;
                this.RunBaseLine = runBaseLine;
            }

            public TextRunPathInfo RunPathInfo { get; private set; }

            public RectangleF Bounds { get; private set; }

            public ContainerEffect Effects { get; private set; }

            public float RunBaseLine { get; private set; }
        }

        private class RunGraphicsInfo
        {
            private readonly GraphicsPath textGraphicsPath;
            private readonly List<GraphicsPath> strikeThroughGraphicsPaths;
            private readonly DevExpress.Office.Drawing.ParagraphLayoutInfoConverter.UnderlineInfo underlineInfo;

            public RunGraphicsInfo(GraphicsPath textGraphicsPath, List<GraphicsPath> strikeThroughGraphicsPaths, DevExpress.Office.Drawing.ParagraphLayoutInfoConverter.UnderlineInfo underlineInfo)
            {
                this.textGraphicsPath = textGraphicsPath;
                this.strikeThroughGraphicsPaths = strikeThroughGraphicsPaths;
                this.underlineInfo = underlineInfo;
            }

            public GraphicsPath TextGraphicsPath =>
                this.textGraphicsPath;

            public List<GraphicsPath> StrikeThroughGraphicsPaths =>
                this.strikeThroughGraphicsPaths;

            public DevExpress.Office.Drawing.ParagraphLayoutInfoConverter.UnderlineInfo UnderlineInfo =>
                this.underlineInfo;
        }

        protected class UnderlineInfo
        {
            public UnderlineInfo(DevExpress.Office.Drawing.RunLayoutInfo runLayoutInfo, float baseline, System.Drawing.Drawing2D.GraphicsPath graphicsPath)
            {
                this.RunLayoutInfo = runLayoutInfo;
                this.Baseline = baseline;
                this.GraphicsPath = graphicsPath;
            }

            public float Baseline { get; private set; }

            public DevExpress.Office.Drawing.RunLayoutInfo RunLayoutInfo { get; private set; }

            public Brush TextBrush { get; set; }

            public DevExpress.Office.Drawing.PenInfo PenInfo { get; set; }

            public System.Drawing.Drawing2D.GraphicsPath GraphicsPath { get; set; }
        }
    }
}

