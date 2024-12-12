namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.DrawingML;
    using DevExpress.Office.Layout;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    internal abstract class TextLayoutCalculator : IDisposable, IDrawingTextRunVisitor
    {
        private static readonly char[] wordSeparators = new char[] { ' ', '-', '\t' };

        protected TextLayoutCalculator(TextRendererContext textRendererContext) : this(textRendererContext.TextRectangle, textRendererContext.Paragraphs, textRendererContext.BodyProperties, textRendererContext.ShapeStyle, textRendererContext.ShouldApplyEffects, textRendererContext.BlackAndWhitePrintMode, textRendererContext.ListStyles)
        {
        }

        protected TextLayoutCalculator(RectangleF textRectangle, DrawingTextParagraphCollection paragraphs, DrawingTextBodyProperties bodyProperties, DevExpress.Office.Drawing.ShapeStyle shapeStyle, bool shouldApplyEffects, bool blackAndWhitePrintMode, DrawingTextListStyles listStyles)
        {
            this.Paragraphs = paragraphs;
            this.DocumentModel = paragraphs.DocumentModel;
            this.BodyProperties = this.GetBodyProperties(bodyProperties);
            this.ShapeStyle = shapeStyle;
            this.StringFormat = new System.Drawing.StringFormat(System.Drawing.StringFormat.GenericTypographic);
            System.Drawing.StringFormat stringFormat = this.StringFormat;
            this.StringFormat.FormatFlags = stringFormat.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
            this.TextRectangle = textRectangle;
            this.NormalizeTextRectangle();
            this.CalculateOnlyTextRectangle();
            this.ColumnIndex = 0;
            this.ColumnsBounds = this.CalculateColumnsBounds(this.TextOnlyRectangle, bodyProperties.NumberOfColumns, this.DocumentModel.ToDocumentLayoutUnitConverter.ToLayoutUnits(bodyProperties.SpaceBetweenColumns));
            this.TabList = new List<float>();
            this.DefaultTabSize = this.DocumentModel.ToDocumentLayoutUnitConverter.ToLayoutUnits((float) 1440f);
            this.BulletCalculator = new DevExpress.Office.Drawing.BulletCalculator(shapeStyle);
            this.BulletIndent = this.DocumentModel.ToDocumentLayoutUnitConverter.ToLayoutUnits((float) 555f);
            this.ShouldApplyEffects = shouldApplyEffects;
            this.WarpGeometry = shouldApplyEffects ? this.GetWarpGeometry() : null;
            this.BlackAndWhitePrintMode = blackAndWhitePrintMode;
            this.ListStyles = listStyles;
        }

        private void AddBullet(DrawingTextParagraph paragraph)
        {
            IDrawingTextParagraphProperties paragraphProperties = paragraph.ParagraphProperties;
            IDrawingTextBullets bullets = paragraphProperties.Bullets;
            this.BulletCalculator.CalcBulletParams(bullets, paragraphProperties.HasTextIndentLevel ? paragraphProperties.TextIndentLevel : 0);
            if (this.BulletCalculator.HasBullet)
            {
                float fontSize;
                IDrawingTextCharacterProperties runProperties = this.GetRunProperties((paragraph.Runs.Count > 0) ? paragraph.Runs[0].RunProperties : null);
                string fontName = !string.IsNullOrEmpty(this.BulletCalculator.Typeface) ? this.BulletCalculator.Typeface : DrawingTextCharacterPropertiesHelper.GetFontTypeFace(runProperties, this.ShapeStyle);
                switch (this.BulletCalculator.SizeType)
                {
                    case DrawingTextSpacingValueType.Automatic:
                        fontSize = this.GetFontSize(runProperties);
                        break;

                    case DrawingTextSpacingValueType.Percent:
                        fontSize = (this.GetFontSize(runProperties) * this.BulletCalculator.SizeValue) / 100000f;
                        break;

                    case DrawingTextSpacingValueType.Points:
                        fontSize = this.DocumentModel.LayoutUnitConverter.PointsToFontUnitsF(((float) this.BulletCalculator.SizeValue) / 100f);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
                FontCache fontCache = this.DocumentModel.FontCache;
                if (fontCache != null)
                {
                    FontInfo bulletFont = fontCache[fontCache.CalcNormalFontIndex(fontName, (int) (fontSize * 2f), false, false, false, false, 0)];
                    this.AddBulletCore(bulletFont, this.BulletCalculator.Text, this.BulletCalculator.Color, runProperties?.Fill);
                }
            }
        }

        protected abstract void AddBulletCore(FontInfo bulletFont, string text, DrawingColor color, IDrawingFill fill);
        private void AddEmptyParagraph(DrawingTextParagraph paragraph)
        {
            IDrawingTextCharacterProperties runProperties = this.GetRunProperties(paragraph.EndRunProperties);
            if (runProperties != null)
            {
                FontInfo[] fontsByRun = this.GetFontsByRun(runProperties);
                this.AddWordRun(runProperties, fontsByRun[0], fontsByRun[1], string.Empty);
            }
        }

        private void AddIndent()
        {
            if (this.CurrentLine.ParagraphProperties.HasIndent)
            {
                float indent = this.DocumentModel.ToDocumentLayoutUnitConverter.ToLayoutUnits(this.CurrentLine.ParagraphProperties.Indent);
                this.AddIndent(indent);
            }
        }

        protected abstract void AddIndent(float indent);
        protected abstract void AddRunLayout(IDrawingTextCharacterProperties runProperties, FontInfo runFontInfo, FontInfo baseFontInfo, string text, float growthDimension);
        private void AddSpacing()
        {
            if (this.ParagraphLayoutInfos.Count != 0)
            {
                ParagraphLayoutInfo paragraphLayoutInfo = this.ParagraphLayoutInfos[this.ParagraphLayoutInfos.Count - 1];
                if (paragraphLayoutInfo.RunLayouts.Count != 0)
                {
                    float normalSpacing = this.CalcSpacing(paragraphLayoutInfo);
                    this.AddSpacingBefore(paragraphLayoutInfo, normalSpacing);
                    this.AddSpacingAfter(paragraphLayoutInfo, normalSpacing);
                }
            }
        }

        protected abstract void AddSpacing(float spacing);
        private void AddSpacingAfter(ParagraphLayoutInfo lastParagraph, float normalSpacing)
        {
            IDrawingTextSpacing spaceAfter = lastParagraph.ParagraphProperties.SpaceAfter;
            if (spaceAfter.Type != DrawingTextSpacingValueType.Automatic)
            {
                float spacingValue = this.GetSpacingValue(normalSpacing, spaceAfter);
                this.AddSpacing(spacingValue);
            }
        }

        private void AddSpacingBefore(ParagraphLayoutInfo lastParagraph, float normalSpacing)
        {
            if (this.ParagraphLayoutInfos.Count != 1)
            {
                IDrawingTextSpacing spaceBefore = lastParagraph.ParagraphProperties.SpaceBefore;
                if (spaceBefore.Type != DrawingTextSpacingValueType.Automatic)
                {
                    float spacingValue = this.GetSpacingValue(normalSpacing, spaceBefore);
                    this.AddSpacing(spacingValue);
                    foreach (RunLayoutInfo info in lastParagraph.RunLayouts)
                    {
                        this.OffsetRunLayoutSpacing(info, spacingValue);
                    }
                }
            }
        }

        private float AddTab()
        {
            float growthDimension = this.GetGrowthDimension();
            float num2 = 0f;
            if ((this.TabList.Count <= 0) || ((this.LastCustomTabIndex >= this.TabList.Count) || (this.TabList[this.TabList.Count - 1] < growthDimension)))
            {
                num2 = this.DefaultParargraphTabSize - (growthDimension % this.DefaultParargraphTabSize);
            }
            else
            {
                for (int i = this.LastCustomTabIndex; i < this.TabList.Count; i++)
                {
                    if (this.TabList[i] >= growthDimension)
                    {
                        num2 = this.TabList[i] - growthDimension;
                        this.NextCustomTabIndex = i + 1;
                        break;
                    }
                }
            }
            return num2;
        }

        private void AddWordRun(IDrawingTextCharacterProperties properties, FontInfo runFontInfo, FontInfo baseFontInfo, string text)
        {
            float num = this.DocumentModel.LayoutUnitConverter.PointsToLayoutUnitsF((float) properties.Spacing) / 100f;
            float num2 = this.GetRunGrowthDimension(text, runFontInfo) + (num * text.Length);
            this.CurrentWordSize += num2;
            RunLayoutInfo item = new RunLayoutInfo(properties, runFontInfo, baseFontInfo, text);
            this.CurrentWordRuns.Add(item);
        }

        private bool AllowWordWrap()
        {
            bool flag = this.BodyProperties.TextWrapping != DrawingTextWrappingType.None;
            return ((this.ColumnIndex < (this.BodyProperties.NumberOfColumns - 1)) | flag);
        }

        public virtual void ApplyGraphicsTransformation(Graphics graphics)
        {
            this.ApplyGraphicsTransformationCore(graphics);
        }

        protected void ApplyGraphicsTransformationCore(Graphics graphics)
        {
            float horizontalTransform = this.GetHorizontalTransform();
            graphics.TranslateTransform(horizontalTransform, this.GetVerticalTransform());
        }

        protected abstract void ApplyParagraphAlignment(RunLayoutInfo runLayout, IDrawingTextParagraphProperties paragraphProperties);
        protected abstract float CalcSpacing(ParagraphLayoutInfo paragraphLayoutInfo);
        protected abstract RectangleF[] CalculateColumnsBounds(RectangleF textOnlyRectangle, int columnsCount, float spaceBetween);
        protected virtual void CalculateOnlyTextRectangle()
        {
            DocumentModelUnitToLayoutUnitConverter toDocumentLayoutUnitConverter = this.DocumentModel.ToDocumentLayoutUnitConverter;
            float left = 0f;
            float top = 0f;
            float right = 0f;
            float bottom = 0f;
            DrawingTextInset inset = this.BodyProperties.Inset;
            if (inset != null)
            {
                left = toDocumentLayoutUnitConverter.ToLayoutUnits(inset.Left);
                top = toDocumentLayoutUnitConverter.ToLayoutUnits(inset.Top);
                right = this.TextRectangle.Width - toDocumentLayoutUnitConverter.ToLayoutUnits(inset.Right);
                bottom = this.TextRectangle.Height - toDocumentLayoutUnitConverter.ToLayoutUnits(inset.Bottom);
            }
            if (left > right)
            {
                left = right;
                right = left;
            }
            if (top > bottom)
            {
                top = bottom;
                bottom = top;
            }
            this.TextOnlyRectangle = RectangleF.FromLTRB(left, top, right, bottom);
        }

        private void CalculateParagraphLayout(DrawingTextParagraph paragraph)
        {
            this.FillTabs(this.CurrentLine.ParagraphProperties);
            this.AddIndent();
            this.StartLine();
            if (paragraph.Runs.Count == 0)
            {
                this.AddEmptyParagraph(paragraph);
            }
            else
            {
                this.AddBullet(paragraph);
                foreach (IDrawingTextRun run in paragraph.Runs)
                {
                    run.Visit(this);
                    if (this.StopCalculateLayout)
                    {
                        break;
                    }
                }
            }
            this.CloseWord();
            this.CloseLine();
            this.AddSpacing();
        }

        private void CalculateRunLayoutInfo(IDrawingTextRun run)
        {
            string text = run.Text;
            if (string.IsNullOrEmpty(text))
            {
                this.CloseWord();
            }
            else
            {
                IDrawingTextCharacterProperties runProperties = this.GetRunProperties(run.RunProperties);
                if (runProperties.Caps != DrawingTextCapsType.None)
                {
                    text = text.ToUpperInvariant();
                }
                FontInfo[] fontsByRun = this.GetFontsByRun(runProperties);
                this.ProcessRunByWords(runProperties, fontsByRun[0], fontsByRun[1], text);
            }
        }

        private void CalculateRunLayoutInfoCore(IDrawingTextCharacterProperties properties, FontInfo runFontInfo, FontInfo baseFontInfo, string text)
        {
            int startIndex = 0;
            float num2 = this.DocumentModel.LayoutUnitConverter.PointsToLayoutUnitsF((float) properties.Spacing) / 100f;
            float growthDimension = ((text.Length == 0) ? 0f : this.GetRunGrowthDimension(text[0].ToString(), runFontInfo)) + num2;
            if (this.AllowWordWrap() && (!this.IsCurrentLineEmpty && !this.CanAddSymbolToCurrentLine(growthDimension)))
            {
                this.CloseLine();
                if (this.StopCalculateLayout)
                {
                    return;
                }
                this.StartLine();
            }
            for (int i = 1; i < text.Length; i++)
            {
                string str = text[i].ToString();
                float num5 = this.GetRunGrowthDimension(str, runFontInfo) + num2;
                if (!this.AllowWordWrap() || this.CanAddSymbolToCurrentLine(growthDimension + num5))
                {
                    growthDimension += num5;
                }
                else
                {
                    this.AddRunLayout(properties, runFontInfo, baseFontInfo, text.Substring(startIndex, i - startIndex), growthDimension);
                    this.CloseLine();
                    if (this.StopCalculateLayout)
                    {
                        return;
                    }
                    this.StartLine();
                    startIndex = i;
                    growthDimension = num5;
                }
            }
            this.AddRunLayout(properties, runFontInfo, baseFontInfo, text.Substring(startIndex, text.Length - startIndex), growthDimension);
        }

        private void CalculateTextBounds()
        {
            this.TextBounds = RectangleF.Empty;
            foreach (ParagraphLayoutInfo info in this.ParagraphLayoutInfos)
            {
                foreach (RunLayoutInfo info2 in info.RunLayouts)
                {
                    RectangleF textBounds = this.TextBounds;
                    this.TextBounds = textBounds.IsEmpty ? info2.Bounds : RectangleF.Union(info2.Bounds, this.TextBounds);
                }
            }
        }

        public void CalculateTextLayout()
        {
            this.SetupStartingPosition();
            this.StopCalculateLayout = false;
            this.CurrentLine = new ParagraphLayoutInfo();
            this.ParagraphLayoutInfos = new List<ParagraphLayoutInfo>();
            this.CurrentWordRuns = new List<RunLayoutInfo>();
            this.CurrentWordSize = 0f;
            foreach (DrawingTextParagraph paragraph in this.Paragraphs)
            {
                this.CurrentLine.ParagraphProperties = this.GetParagraphProperties(paragraph.ParagraphProperties);
                this.CalculateParagraphLayout(paragraph);
                if (this.StopCalculateLayout)
                {
                    break;
                }
            }
            this.CalculateTextBounds();
        }

        protected abstract bool CanAddSymbolToCurrentLine(float growthDimension);
        private void CloseLine()
        {
            this.LastCustomTabIndex = 0;
            this.NextCustomTabIndex = 0;
            if (!this.StopCalculateLayout)
            {
                if (this.CurrentLine.Bullet != null)
                {
                    this.ApplyParagraphAlignment(this.CurrentLine.Bullet, this.CurrentLine.ParagraphProperties);
                }
                foreach (RunLayoutInfo info in this.CurrentLine.RunLayouts)
                {
                    this.ApplyParagraphAlignment(info, this.CurrentLine.ParagraphProperties);
                }
                this.CloseLineCore();
                this.ParagraphLayoutInfos.Add(this.CurrentLine);
                this.CurrentLine = new ParagraphLayoutInfo(this.CurrentLine.ParagraphProperties);
            }
        }

        protected abstract void CloseLineCore();
        private void CloseWord()
        {
            if (!this.StopCalculateLayout)
            {
                this.CloseWordCore();
                this.CurrentWordSize = 0f;
                this.LastCustomTabIndex = this.NextCustomTabIndex;
                this.CurrentWordRuns.Clear();
            }
        }

        private void CloseWordCore()
        {
            if (this.CurrentWordRuns.Count != 0)
            {
                if (this.AllowWordWrap() && (!this.CanAddSymbolToCurrentLine(this.CurrentWordSize) && !this.IsCurrentLineEmpty))
                {
                    if ((this.CurrentWordRuns.Count == 1) && ((this.CurrentWordRuns[0].Text == " ") && this.DisallowSpacesOnNewLine))
                    {
                        return;
                    }
                    this.CloseLine();
                    if (this.StopCalculateLayout)
                    {
                        return;
                    }
                    this.StartLine();
                }
                if (((this.CurrentWordRuns.Count == 1) && (!this.AllowWordWrap() || this.CanAddSymbolToCurrentLine(this.CurrentWordSize))) && (this.CurrentWordRuns[0].Text != "\t"))
                {
                    RunLayoutInfo info = this.CurrentWordRuns[0];
                    this.AddRunLayout(info.Properties, info.RunFontInfo, info.BaseFontInfo, info.Text, this.CurrentWordSize);
                }
                else
                {
                    foreach (RunLayoutInfo info2 in this.CurrentWordRuns)
                    {
                        this.CalculateRunLayoutInfoCore(info2.Properties, info2.RunFontInfo, info2.BaseFontInfo, info2.Text);
                    }
                }
            }
        }

        protected Graphics CreateTempGraphics()
        {
            using (Bitmap bitmap = new Bitmap(1, 1))
            {
                return GdipExtensions.PrepareGraphicsFromImage(bitmap, (GraphicsUnit) this.LayoutUnitConverter.GraphicsPageUnit, this.LayoutUnitConverter.GraphicsPageScale);
            }
        }

        void IDrawingTextRunVisitor.Visit(DrawingTextField item)
        {
            this.CalculateRunLayoutInfo(item);
        }

        void IDrawingTextRunVisitor.Visit(DrawingTextLineBreak item)
        {
            this.CloseWord();
            this.CloseLine();
        }

        void IDrawingTextRunVisitor.Visit(DrawingTextRun item)
        {
            this.CalculateRunLayoutInfo(item);
        }

        public virtual void Dispose()
        {
            if (this.StringFormat != null)
            {
                this.StringFormat.Dispose();
                this.StringFormat = null;
            }
        }

        private void FillTabs(IDrawingTextParagraphProperties paragraphProperties)
        {
            this.DefaultParargraphTabSize = paragraphProperties.HasDefaultTabSize ? this.DocumentModel.ToDocumentLayoutUnitConverter.ToLayoutUnits(paragraphProperties.DefaultTabSize) : this.DefaultTabSize;
            this.TabList.Clear();
            if (paragraphProperties.TabStopList != null)
            {
                foreach (DrawingTextTabStop stop in paragraphProperties.TabStopList)
                {
                    this.TabList.Add(this.DocumentModel.ToDocumentLayoutUnitConverter.ToLayoutUnits(stop.Position));
                }
            }
        }

        private DrawingTextBodyProperties GetBodyProperties(DrawingTextBodyProperties bodyProperties) => 
            bodyProperties;

        private FontInfo[] GetFontsByRun(IDrawingTextCharacterProperties runProperties)
        {
            FontCache fontCache = this.DocumentModel.FontCache;
            if (fontCache == null)
            {
                return new FontInfo[2];
            }
            string fontTypeFace = DrawingTextCharacterPropertiesHelper.GetFontTypeFace(runProperties, this.ShapeStyle);
            float fontSize = this.GetFontSize(runProperties);
            int num2 = fontCache.CalcNormalFontIndex(fontTypeFace, (int) (fontSize * 2f), runProperties.Bold, runProperties.Italic, false, false, 0);
            int num3 = num2;
            if (runProperties.Baseline != 0)
            {
                num3 = fontCache.CalcNormalFontIndex(fontTypeFace, (int) (((fontSize * 2f) * 2f) / 3f), runProperties.Bold, runProperties.Italic, false, false, 0);
            }
            else if (runProperties.Caps == DrawingTextCapsType.Small)
            {
                num3 = fontCache.CalcNormalFontIndex(fontTypeFace, (int) (((fontSize * 2f) * 4f) / 5f), runProperties.Bold, runProperties.Italic, false, false, 0);
            }
            return new FontInfo[] { fontCache[num3], fontCache[num2] };
        }

        private float GetFontSize(IDrawingTextCharacterProperties runProperties) => 
            this.DocumentModel.LayoutUnitConverter.PointsToFontUnitsF(((runProperties == null) || !runProperties.Options.HasFontSize) ? 11f : (((float) runProperties.FontSize) / 100f));

        protected abstract float GetGrowthDimension();
        protected virtual float GetHorizontalTransform() => 
            this.BodyProperties.AnchorCenter ? ((this.TextOnlyRectangle.Left + ((this.TextOnlyRectangle.Width - this.TextBounds.Width) / 2f)) - this.TextBounds.Left) : this.TextOnlyRectangle.Left;

        public abstract ParagraphLayoutInfoConverter GetLayoutInfoConverter();
        private IDrawingTextParagraphProperties GetParagraphProperties(DrawingTextParagraphProperties paragraphProperties) => 
            paragraphProperties;

        public virtual Size GetResultBitmapSize() => 
            Size.Round(this.TextRectangle.Size);

        protected float GetRunGrowthDimension(string text, FontInfo fontInfo) => 
            (text == "\t") ? this.AddTab() : this.GetRunGrowthDimensionCore(text, fontInfo);

        protected abstract float GetRunGrowthDimensionCore(string text, FontInfo fontInfo);
        private IDrawingTextCharacterProperties GetRunProperties(DrawingTextCharacterProperties runProperties)
        {
            if (runProperties == null)
            {
                return null;
            }
            IDrawingTextCharacterProperties properties = runProperties;
            if ((runProperties.Fill.FillType == DrawingFillType.Automatic) && (this.ListStyles != null))
            {
                int textIndentLevel = this.CurrentLine.ParagraphProperties.TextIndentLevel;
                if (!this.ListStyles[textIndentLevel].IsDefault)
                {
                    TextRendererDrawingTextCharacterProperties properties1 = new TextRendererDrawingTextCharacterProperties(runProperties);
                    properties1.Fill = this.ListStyles[textIndentLevel].DefaultCharacterProperties.Fill;
                    properties = properties1;
                }
            }
            return properties;
        }

        private float GetSpacingValue(float normalSpacing, IDrawingTextSpacing spacing) => 
            (spacing.Type == DrawingTextSpacingValueType.Points) ? this.DocumentModel.LayoutUnitConverter.PointsToLayoutUnitsF(((float) spacing.Value) / 100f) : ((normalSpacing * spacing.Value) / 100000f);

        public Rectangle GetTextBounds()
        {
            if (this.WarpGeometry != null)
            {
                return Rectangle.Round(this.TextRectangle);
            }
            RectangleF textBounds = this.TextBounds;
            textBounds.Offset(this.GetHorizontalTransform() + this.TextRectangle.X, this.GetVerticalTransform() + this.TextRectangle.Y);
            return Rectangle.Round(textBounds);
        }

        protected virtual float GetVerticalTransform()
        {
            float top;
            switch (this.BodyProperties.Anchor)
            {
                case DrawingTextAnchoringType.Bottom:
                case DrawingTextAnchoringType.Distributed:
                case DrawingTextAnchoringType.Justified:
                    top = (this.TextOnlyRectangle.Bottom - this.TextBounds.Height) - this.TextBounds.Top;
                    break;

                case DrawingTextAnchoringType.Center:
                    top = (this.TextOnlyRectangle.Top + ((this.TextOnlyRectangle.Height - this.TextBounds.Height) / 2f)) - this.TextBounds.Top;
                    break;

                case DrawingTextAnchoringType.Top:
                    top = this.TextOnlyRectangle.Top;
                    break;

                default:
                    top = this.TextOnlyRectangle.Top;
                    break;
            }
            return top;
        }

        private ModelShapeCustomGeometry GetWarpGeometry()
        {
            DrawingPresetTextWarp presetTextWarp = this.BodyProperties.PresetTextWarp;
            switch (presetTextWarp)
            {
                case DrawingPresetTextWarp.NoShape:
                case DrawingPresetTextWarp.ArchDown:
                case DrawingPresetTextWarp.ArchUp:
                case DrawingPresetTextWarp.Button:
                    goto TR_0000;

                case DrawingPresetTextWarp.ArchDownPour:
                case DrawingPresetTextWarp.ArchUpPour:
                    break;

                default:
                    if (presetTextWarp != DrawingPresetTextWarp.Circle)
                    {
                        break;
                    }
                    goto TR_0000;
            }
            DocumentModelUnitConverter unitConverter = this.DocumentModel.UnitConverter;
            DocumentModelUnitToLayoutUnitConverter toDocumentLayoutUnitConverter = this.DocumentModel.ToDocumentLayoutUnitConverter;
            return GeometryCalculator.Calculate(PresetTextWarpGenerators.GetCustomGeometryByPreset(this.BodyProperties.PresetTextWarp), unitConverter.ModelUnitsToEmuD(toDocumentLayoutUnitConverter.ToModelUnits(this.TextRectangle.Width)), unitConverter.ModelUnitsToEmuD(toDocumentLayoutUnitConverter.ToModelUnits(this.TextRectangle.Height)), this.BodyProperties.PresetAdjustValues);
        TR_0000:
            return null;
        }

        protected virtual void NormalizeTextRectangle()
        {
        }

        protected abstract void OffsetRunLayoutSpacing(RunLayoutInfo runLayout, float spacing);
        private void ProcessRunByWords(IDrawingTextCharacterProperties runProperties, FontInfo runFontInfo, FontInfo baseFontInfo, string text)
        {
            int num2;
            int startIndex = 0;
            while ((num2 = text.IndexOfAny(wordSeparators, startIndex)) != -1)
            {
                if (num2 > startIndex)
                {
                    this.AddWordRun(runProperties, runFontInfo, baseFontInfo, text.Substring(startIndex, num2 - startIndex));
                }
                this.CloseWord();
                string str = text[num2].ToString();
                this.AddWordRun(runProperties, runFontInfo, baseFontInfo, str);
                if (str != "\t")
                {
                    this.CloseWord();
                }
                startIndex = num2 + 1;
                if (this.StopCalculateLayout)
                {
                    return;
                }
            }
            if ((startIndex <= (text.Length - 1)) || (startIndex == 0))
            {
                this.AddWordRun(runProperties, runFontInfo, baseFontInfo, text.Substring(startIndex));
            }
        }

        protected abstract void SetupStartingPosition();
        private void StartLine()
        {
            float marL = this.CurrentLine.ParagraphProperties.HasLeftMargin ? this.DocumentModel.ToDocumentLayoutUnitConverter.ToLayoutUnits(this.CurrentLine.ParagraphProperties.Margin.Left) : 0f;
            this.StartLineCore(marL);
        }

        protected abstract void StartLineCore(float marL);
        protected bool TryGotoNextColumn()
        {
            if (this.ColumnIndex == (this.ColumnsBounds.Length - 1))
            {
                this.StopCalculateLayout = true;
                return false;
            }
            int columnIndex = this.ColumnIndex;
            this.ColumnIndex = columnIndex + 1;
            this.SetupStartingPosition();
            return true;
        }

        internal RectangleF TextRectangle { get; set; }

        protected RectangleF TextOnlyRectangle { get; set; }

        protected RectangleF ColumnRectangle =>
            this.ColumnsBounds[this.ColumnIndex];

        protected bool StopCalculateLayout { get; set; }

        protected ParagraphLayoutInfo CurrentLine { get; set; }

        public List<ParagraphLayoutInfo> ParagraphLayoutInfos { get; private set; }

        protected DrawingTextParagraphCollection Paragraphs { get; private set; }

        protected IDocumentModel DocumentModel { get; private set; }

        protected DrawingTextBodyProperties BodyProperties { get; private set; }

        protected internal DocumentLayoutUnitConverter LayoutUnitConverter =>
            this.DocumentModel.LayoutUnitConverter;

        public RectangleF TextBounds { get; protected set; }

        protected bool IsCurrentLineEmpty =>
            this.CurrentLine.RunLayouts.Count == 0;

        protected DevExpress.Office.Drawing.ShapeStyle ShapeStyle { get; private set; }

        protected System.Drawing.StringFormat StringFormat { get; set; }

        protected float X { get; set; }

        protected float Y { get; set; }

        private float CurrentWordSize { get; set; }

        private List<RunLayoutInfo> CurrentWordRuns { get; set; }

        private List<float> TabList { get; set; }

        private float DefaultTabSize { get; set; }

        private float DefaultParargraphTabSize { get; set; }

        private int LastCustomTabIndex { get; set; }

        private int NextCustomTabIndex { get; set; }

        private RectangleF[] ColumnsBounds { get; set; }

        private int ColumnIndex { get; set; }

        private DevExpress.Office.Drawing.BulletCalculator BulletCalculator { get; set; }

        protected float BulletIndent { get; set; }

        protected bool ShouldApplyEffects { get; private set; }

        protected internal ModelShapeCustomGeometry WarpGeometry { get; private set; }

        protected bool BlackAndWhitePrintMode { get; set; }

        protected DrawingTextListStyles ListStyles { get; set; }

        protected bool DisallowSpacesOnNewLine { get; set; }
    }
}

