namespace DevExpress.Text.Fonts
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class DXLineFormatter
    {
        private const float lineAppearanceFactor = 0.25f;
        private readonly DXSizeF layoutSize;
        private readonly float tabStopSize;
        private readonly uint maxLineCount;
        private readonly DXLineTrimmingInfo trimmingInfo;
        private readonly Lazy<ClusterCollectionView> hyphenRun;
        private readonly ITextLineBuilder lineBuilder;
        private readonly bool noWrap;
        private readonly bool measureTrailingSpaces;

        private DXLineFormatter(DXSizeF layoutSize, PdfFontMetrics metrics, float fontSize, PdfStringFormat stringFormat, float tabStopSize, DXShaper shaper, ITextLineBuilder lineBuilder)
        {
            this.layoutSize = layoutSize;
            this.tabStopSize = tabStopSize;
            PdfStringFormatFlags formatFlags = stringFormat.FormatFlags;
            this.noWrap = formatFlags.HasFlag(PdfStringFormatFlags.NoWrap);
            this.measureTrailingSpaces = formatFlags.HasFlag(PdfStringFormatFlags.MeasureTrailingSpaces);
            bool limitLines = formatFlags.HasFlag(PdfStringFormatFlags.LineLimit);
            switch (stringFormat.Trimming)
            {
                case PdfStringTrimming.None:
                    this.trimmingInfo = new DXLineTrimmingInfo((!limitLines || this.noWrap) ? Trimming.None : Trimming.Word);
                    break;

                case PdfStringTrimming.Character:
                    this.trimmingInfo = new DXLineTrimmingInfo(Trimming.Character);
                    break;

                case PdfStringTrimming.Word:
                    this.trimmingInfo = new DXLineTrimmingInfo(Trimming.Word);
                    break;

                case PdfStringTrimming.EllipsisCharacter:
                    this.trimmingInfo = new DXLineTrimmingInfo(shaper, fontSize, Trimming.Character);
                    break;

                case PdfStringTrimming.EllipsisWord:
                    this.trimmingInfo = new DXLineTrimmingInfo(shaper, fontSize, Trimming.Word);
                    break;

                default:
                    break;
            }
            this.hyphenRun = new Lazy<ClusterCollectionView>(() => new ClusterCollectionView(shaper.GetHyphenRun(fontSize)));
            this.maxLineCount = GetLineCount(limitLines, metrics, fontSize, layoutSize.Height);
            this.lineBuilder = lineBuilder;
            this.StartNewLine();
        }

        private int AppendByClusters(IReadOnlyList<DXCluster> clusters, int i, float ellipsisWidth)
        {
            while (true)
            {
                if (i < clusters.Count)
                {
                    DXCluster cluster = clusters[i];
                    if (this.CurrentLine.EmptyAndNoTrailingSpaces || this.CurrentLine.IsFit(cluster.Width + ellipsisWidth))
                    {
                        this.CurrentLine.AddCluster(cluster);
                        i++;
                        continue;
                    }
                }
                return i;
            }
        }

        private bool AppendRange(Range range, bool isLastWord)
        {
            while (true)
            {
                float width = (range.WidthWithoutTrailingSpaces + (this.ShouldApplyTrimming ? this.trimmingInfo.EllipsisWidth : 0f)) + (range.EndsWithSoftHyphen ? this.HyphenRun.Width : 0f);
                if (this.CurrentLine.IsFit(width) || ((this.trimmingInfo.Trimming == Trimming.None) && this.noWrap))
                {
                    this.CurrentLine.AddRange(range);
                    return false;
                }
                if (this.ShouldApplyTrimming && (this.trimmingInfo.Trimming != Trimming.None))
                {
                    this.Trim(range, isLastWord);
                    this.EndLine(true);
                    return true;
                }
                if (!this.CurrentLine.EmptyAndNoTrailingSpaces)
                {
                    this.EndLine(true);
                }
                else
                {
                    range = this.CurrentLine.AddRangePart(range);
                    if (range != null)
                    {
                        this.EndLine(true);
                    }
                }
                if (range == null)
                {
                    return false;
                }
            }
        }

        private void EndLine(bool appendEmptyLine)
        {
            if (this.CurrentLine.Empty)
            {
                if (appendEmptyLine)
                {
                    this.lineBuilder.Add(new Line(this.layoutSize.Width, this.hyphenRun, this.measureTrailingSpaces));
                }
            }
            else
            {
                if (this.CurrentLine.ShouldAppendSoftHyphen)
                {
                    this.CurrentLine.AppendHyphen();
                }
                this.lineBuilder.Add(this.CurrentLine);
            }
            this.StartNewLine();
        }

        private bool Format(IList<DXCluster> clusters, bool shouldFitIntoSingleLine)
        {
            if (!this.IsLayoutOverflowed())
            {
                if (clusters.Count == 0)
                {
                    this.EndLine(true);
                    return true;
                }
                RangeBuilder builder = new RangeBuilder();
                for (int i = 0; i < clusters.Count; i++)
                {
                    DXCluster tabCluster = clusters[i];
                    bool isLastWord = i == (clusters.Count - 1);
                    if (shouldFitIntoSingleLine && (this.lineBuilder.Count > 0))
                    {
                        return false;
                    }
                    if (tabCluster.IsTabCluster)
                    {
                        if (!builder.Empty)
                        {
                            if (this.AppendRange(builder.GetRange(clusters, false), isLastWord))
                            {
                                return true;
                            }
                            builder = new RangeBuilder();
                        }
                        this.GoToNextTabStop(tabCluster);
                    }
                    else
                    {
                        if (tabCluster.Breakpoint.IsWhitespace && !tabCluster.IsTabCluster)
                        {
                            builder.AppendSpace(i);
                        }
                        else if (!tabCluster.Breakpoint.IsSoftHyphen)
                        {
                            builder.AppendGlyph(i);
                        }
                        if (tabCluster.Breakpoint.BreakConditionAfter != DXBreakCondition.MayNotBreak)
                        {
                            if (this.AppendRange(builder.GetRange(clusters, tabCluster.Breakpoint.IsSoftHyphen), isLastWord))
                            {
                                return true;
                            }
                            if (tabCluster.Breakpoint.BreakConditionAfter == DXBreakCondition.MustBreak)
                            {
                                this.EndLine(true);
                            }
                            builder = new RangeBuilder();
                        }
                    }
                }
                if (builder.Empty || !this.AppendRange(builder.GetRange(clusters, false), true))
                {
                    this.EndLine(false);
                }
            }
            return true;
        }

        private static T FormatParagraph<T>(ParagraphInfo paragraph, DXSizeF layoutSize, PdfFontMetrics metrics, float fontSize, PdfStringFormat stringFormat, float tabStopSize, DXShaper shaper, Func<T> builderFactory, bool useKerning, bool shouldFitIntoSingleLine) where T: class, ITextLineBuilder
        {
            string text = paragraph.GetText(stringFormat.HotkeyPrefix);
            T lineBuilder = builderFactory();
            lineBuilder.SetCurrentParagraph(paragraph);
            if (new DXLineFormatter(layoutSize, metrics, fontSize, stringFormat, tabStopSize, shaper, lineBuilder).Format(shaper.GetTextRuns(text, stringFormat.DirectionRightToLeft, fontSize, useKerning), shouldFitIntoSingleLine) && (!shouldFitIntoSingleLine || (lineBuilder.Count <= 1)))
            {
                return lineBuilder;
            }
            return default(T);
        }

        public static IList<IList<DXCluster>> FormatText(string text, DXSizeF? layoutSize, PdfFontMetrics metrics, float fontSize, PdfStringFormat stringFormat, float tabStopSize, DXShaper shaper, DXKerningMode kerningMode)
        {
            Func<TextLinesBuilder> builderFactory = <>c.<>9__12_0;
            if (<>c.<>9__12_0 == null)
            {
                Func<TextLinesBuilder> local1 = <>c.<>9__12_0;
                builderFactory = <>c.<>9__12_0 = () => new TextLinesBuilder();
            }
            return FormatTextCore<TextLinesBuilder>(text, layoutSize, metrics, fontSize, stringFormat, tabStopSize, shaper, kerningMode, builderFactory).Lines;
        }

        private static T FormatTextCore<T>(string text, DXSizeF? layoutSize, PdfFontMetrics metrics, float fontSize, PdfStringFormat stringFormat, float tabStopSize, DXShaper shaper, DXKerningMode kerningMode, Func<T> builderFactory) where T: class, ITextLineBuilder
        {
            DXSizeF ef;
            if ((layoutSize != null) && (layoutSize.Value.Width > 0f))
            {
                ef = new DXSizeF(layoutSize.Value.Width - ((float) ((stringFormat.TrailingMarginFactor + stringFormat.LeadingMarginFactor) * fontSize)), layoutSize.Value.Height);
            }
            else
            {
                ef = new DXSizeF(float.MaxValue, float.MaxValue);
            }
            IList<ParagraphInfo> list = SplitByParagraphs(text);
            if (list.Count == 1)
            {
                if ((layoutSize == null) || (kerningMode != DXKerningMode.MultilineOnly))
                {
                    return FormatParagraph<T>(list[0], ef, metrics, fontSize, stringFormat, tabStopSize, shaper, builderFactory, kerningMode == DXKerningMode.Always, false);
                }
                T local = FormatParagraph<T>(list[0], ef, metrics, fontSize, stringFormat, tabStopSize, shaper, builderFactory, false, true);
                return ((local == null) ? FormatParagraph<T>(list[0], ef, metrics, fontSize, stringFormat, tabStopSize, shaper, builderFactory, true, false) : local);
            }
            T lineBuilder = builderFactory();
            DXLineFormatter formatter = new DXLineFormatter(ef, metrics, fontSize, stringFormat, tabStopSize, shaper, lineBuilder);
            foreach (ParagraphInfo info in list)
            {
                lineBuilder.SetCurrentParagraph(info);
                string str = info.GetText(stringFormat.HotkeyPrefix);
                formatter.Format(shaper.GetTextRuns(str, stringFormat.DirectionRightToLeft, fontSize, kerningMode != DXKerningMode.None), false);
                if (formatter.IsLayoutOverflowed())
                {
                    break;
                }
            }
            return lineBuilder;
        }

        private static uint GetLineCount(bool limitLines, PdfFontMetrics metrics, float fontSize, float layoutHeight)
        {
            uint num3;
            double lineSpacing = metrics.GetLineSpacing((double) fontSize);
            double num2 = metrics.GetAscent((double) fontSize) + metrics.GetDescent((double) fontSize);
            if (layoutHeight < num2)
            {
                num3 = (limitLines || (layoutHeight < (lineSpacing * 0.25))) ? 0 : 1;
            }
            else
            {
                double num4 = (layoutHeight - num2) / lineSpacing;
                num3 = (uint) Math.Min(4294967293, Math.Floor((double) (num4 + 0.01)));
                if (!limitLines && ((num4 - num3) >= 0.25))
                {
                    num3++;
                }
                num3++;
            }
            return num3;
        }

        private void GoToNextTabStop(DXCluster tabCluster)
        {
            if (!this.CurrentLine.TryAppendTabStop(tabCluster, this.tabStopSize))
            {
                this.EndLine(true);
                this.CurrentLine.TryAppendTabStop(tabCluster, this.tabStopSize);
            }
        }

        private bool IsLayoutOverflowed() => 
            (this.trimmingInfo.Trimming != Trimming.None) && (this.lineBuilder.Count >= this.maxLineCount);

        public static DXTextMeasurementResult MeasureText(string text, DXSizeF? layoutSize, PdfFontMetrics metrics, float fontSize, PdfStringFormat stringFormat, float tabStopSize, DXShaper shaper, DXKerningMode kerningMode)
        {
            float maxWidth = (layoutSize != null) ? layoutSize.Value.Width : float.MaxValue;
            MeasurerLinesBuilder builder = FormatTextCore<MeasurerLinesBuilder>(text, layoutSize, metrics, fontSize, stringFormat, tabStopSize, shaper, kerningMode, () => new MeasurerLinesBuilder(maxWidth));
            double num2 = metrics.GetAscent((double) fontSize) + metrics.GetDescent((double) fontSize);
            return new DXTextMeasurementResult(new DXSizeF(builder.MaxLineWidth, (float) (num2 + (metrics.GetLineSpacing((double) fontSize) * Math.Max(0, builder.Count - 1)))), builder.Count, builder.CharactersCount);
        }

        private static IList<ParagraphInfo> SplitByParagraphs(string text)
        {
            List<ParagraphInfo> list = new List<ParagraphInfo>(1);
            int startPosition = 0;
            int endStringRunLength = 0;
            int num3 = 0;
            while (true)
            {
                while (true)
                {
                    if (num3 >= text.Length)
                    {
                        if (startPosition < text.Length)
                        {
                            list.Add(new ParagraphInfo(startPosition, text.Length - startPosition, endStringRunLength, text));
                        }
                        return list;
                    }
                    char ch = text[num3];
                    if (ch != '\n')
                    {
                        if (ch != '\r')
                        {
                            if (ch != '\u2029')
                            {
                                break;
                            }
                        }
                        else if ((num3 < (text.Length - 1)) && (text[num3 + 1] == '\n'))
                        {
                            num3++;
                            endStringRunLength++;
                        }
                    }
                    list.Add(new ParagraphInfo(startPosition, (((num3 - startPosition) + 1) - endStringRunLength) - 1, endStringRunLength + 1, text));
                    endStringRunLength = 0;
                    startPosition = num3 + 1;
                    break;
                }
                num3++;
            }
        }

        private void StartNewLine()
        {
            this.CurrentLine = new Line(this.layoutSize.Width, this.hyphenRun, this.measureTrailingSpaces);
        }

        private void Trim(Range range, bool isLastWord)
        {
            if (isLastWord && this.CurrentLine.IsFit(range.WidthWithoutTrailingSpaces))
            {
                this.CurrentLine.AddRange(range);
            }
            else if (this.trimmingInfo.Trimming != Trimming.Word)
            {
                if (this.trimmingInfo.Trimming == Trimming.Character)
                {
                    this.TrimByClusters(range);
                }
            }
            else if (this.CurrentLine.EmptyAndNoTrailingSpaces)
            {
                this.TrimByClusters(range);
            }
            else
            {
                this.CurrentLine.TryEndLineWithEllipsis(this.trimmingInfo.EllipsisCollection, false);
            }
        }

        private void TrimByClusters(Range range)
        {
            IReadOnlyList<DXCluster> clusters = range.Clusters;
            int i = this.AppendByClusters(clusters, 0, this.trimmingInfo.EllipsisWidth);
            if (!this.CurrentLine.TryEndLineWithEllipsis(this.trimmingInfo.EllipsisCollection, this.trimmingInfo.Trimming == Trimming.Character) && ((this.AppendByClusters(clusters, i, 0f) == clusters.Count) && (range.EndsWithSoftHyphen && this.CurrentLine.IsFit(this.HyphenRun.Width))))
            {
                this.CurrentLine.AppendHyphen();
            }
        }

        private ClusterCollectionView HyphenRun =>
            this.hyphenRun.Value;

        private bool ShouldApplyTrimming =>
            (this.lineBuilder.Count == (this.maxLineCount - 1)) || this.noWrap;

        private Line CurrentLine { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXLineFormatter.<>c <>9 = new DXLineFormatter.<>c();
            public static Func<DXLineFormatter.TextLinesBuilder> <>9__12_0;

            internal DXLineFormatter.TextLinesBuilder <FormatText>b__12_0() => 
                new DXLineFormatter.TextLinesBuilder();
        }

        private class ClusterCollectionView : IReadOnlyList<DXCluster>, IReadOnlyCollection<DXCluster>, IEnumerable<DXCluster>, IEnumerable
        {
            private readonly IList<DXCluster> source;
            private readonly int startIndex;
            private readonly int endIndex;

            private ClusterCollectionView()
            {
            }

            public ClusterCollectionView(IList<DXCluster> source) : this(source, 0, source.Count)
            {
            }

            public ClusterCollectionView(IList<DXCluster> source, int startIndex, int endIndex)
            {
                this.source = source;
                this.startIndex = startIndex;
                this.endIndex = endIndex;
                for (int i = startIndex; i < endIndex; i++)
                {
                    DXCluster cluster = source[i];
                    this.<Width>k__BackingField = this.Width + cluster.Width;
                }
            }

            [IteratorStateMachine(typeof(<GetEnumerator>d__19))]
            public IEnumerator<DXCluster> GetEnumerator()
            {
                <GetEnumerator>d__19 d__1 = new <GetEnumerator>d__19(0);
                d__1.<>4__this = this;
                return d__1;
            }

            public DXLineFormatter.ClusterCollectionView Split(int startIndex) => 
                (startIndex >= this.endIndex) ? new DXLineFormatter.ClusterCollectionView() : new DXLineFormatter.ClusterCollectionView(this.source, this.startIndex + startIndex, this.endIndex);

            IEnumerator IEnumerable.GetEnumerator() => 
                this.GetEnumerator();

            public static DXLineFormatter.ClusterCollectionView EmptyView { get; }

            public DXCluster this[int index] =>
                this.source[this.startIndex + index];

            public int Count =>
                this.endIndex - this.startIndex;

            public float Width { get; }

            public bool Empty =>
                this.Count == 0;

            [CompilerGenerated]
            private sealed class <GetEnumerator>d__19 : IEnumerator<DXCluster>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private DXCluster <>2__current;
                public DXLineFormatter.ClusterCollectionView <>4__this;
                private int <i>5__1;

                [DebuggerHidden]
                public <GetEnumerator>d__19(int <>1__state)
                {
                    this.<>1__state = <>1__state;
                }

                private bool MoveNext()
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<i>5__1 = this.<>4__this.startIndex;
                    }
                    else
                    {
                        if (num != 1)
                        {
                            return false;
                        }
                        this.<>1__state = -1;
                        int num2 = this.<i>5__1;
                        this.<i>5__1 = num2 + 1;
                    }
                    if (this.<i>5__1 >= this.<>4__this.endIndex)
                    {
                        return false;
                    }
                    this.<>2__current = this.<>4__this.source[this.<i>5__1];
                    this.<>1__state = 1;
                    return true;
                }

                [DebuggerHidden]
                void IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }

                [DebuggerHidden]
                void IDisposable.Dispose()
                {
                }

                DXCluster IEnumerator<DXCluster>.Current =>
                    this.<>2__current;

                object IEnumerator.Current =>
                    this.<>2__current;
            }
        }

        private class DXLineTrimmingInfo
        {
            private readonly Lazy<DXLineFormatter.ClusterCollectionView> ellipsisCollection;

            public DXLineTrimmingInfo(DevExpress.Text.Fonts.DXLineFormatter.Trimming trimming)
            {
                Func<DXLineFormatter.ClusterCollectionView> valueFactory = <>c.<>9__8_0;
                if (<>c.<>9__8_0 == null)
                {
                    Func<DXLineFormatter.ClusterCollectionView> local1 = <>c.<>9__8_0;
                    valueFactory = <>c.<>9__8_0 = () => DXLineFormatter.ClusterCollectionView.EmptyView;
                }
                this.ellipsisCollection = new Lazy<DXLineFormatter.ClusterCollectionView>(valueFactory);
                this.<Trimming>k__BackingField = trimming;
            }

            public DXLineTrimmingInfo(DXShaper shaper, float fontSize, DevExpress.Text.Fonts.DXLineFormatter.Trimming trimming)
            {
                this.ellipsisCollection = new Lazy<DXLineFormatter.ClusterCollectionView>(() => new DXLineFormatter.ClusterCollectionView(shaper.GetEllipsisRun(fontSize)));
                this.<Trimming>k__BackingField = trimming;
            }

            public DXLineFormatter.ClusterCollectionView EllipsisCollection =>
                this.ellipsisCollection.Value;

            public DevExpress.Text.Fonts.DXLineFormatter.Trimming Trimming { get; }

            public float EllipsisWidth =>
                this.EllipsisCollection.Width;

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly DXLineFormatter.DXLineTrimmingInfo.<>c <>9 = new DXLineFormatter.DXLineTrimmingInfo.<>c();
                public static Func<DXLineFormatter.ClusterCollectionView> <>9__8_0;

                internal DXLineFormatter.ClusterCollectionView <.ctor>b__8_0() => 
                    DXLineFormatter.ClusterCollectionView.EmptyView;
            }
        }

        private interface ITextLineBuilder
        {
            void Add(DXLineFormatter.Line line);
            void SetCurrentParagraph(DXLineFormatter.ParagraphInfo paragraph);

            int Count { get; }
        }

        private class Line
        {
            private readonly float maxWidth;
            private readonly Lazy<DXLineFormatter.ClusterCollectionView> hyphenRun;
            private readonly bool measureTrailingSpaces;
            private DXLineFormatter.ClusterCollectionView trailingSpaces = DXLineFormatter.ClusterCollectionView.EmptyView;
            private float currentWidth;
            private int additionalClusters;

            public Line(float maxWidth, Lazy<DXLineFormatter.ClusterCollectionView> hyphenRun, bool measureTrailingSpaces)
            {
                this.maxWidth = maxWidth;
                this.hyphenRun = hyphenRun;
                this.measureTrailingSpaces = measureTrailingSpaces;
            }

            private void Add(DXCluster cluster)
            {
                this.AppendTraillingSpaces();
                this.Clusters.Add(cluster);
                this.currentWidth += cluster.Width;
            }

            public void AddCluster(DXCluster cluster)
            {
                this.Add(cluster);
            }

            private void AddRange(DXLineFormatter.ClusterCollectionView clusters)
            {
                this.Clusters.AddRange(clusters);
                this.currentWidth += clusters.Width;
            }

            public void AddRange(DXLineFormatter.Range range)
            {
                this.AppendTraillingSpaces();
                this.ShouldAppendSoftHyphen = range.EndsWithSoftHyphen;
                this.AddRange(range.Clusters);
                this.trailingSpaces = range.TrailingSpaces;
            }

            public DXLineFormatter.Range AddRangePart(DXLineFormatter.Range range)
            {
                this.AppendTraillingSpaces();
                for (int i = 0; i < range.Clusters.Count; i++)
                {
                    DXCluster cluster = range.Clusters[i];
                    if (!this.Empty && !this.IsFit(cluster.Width))
                    {
                        return range.Split(i);
                    }
                    this.Add(cluster);
                }
                this.trailingSpaces = range.TrailingSpaces;
                return null;
            }

            public void AppendHyphen()
            {
                this.AddRange(this.HyphenRun);
                this.EndWithSoftHyphen = true;
                this.additionalClusters += this.HyphenRun.Count;
                this.ShouldAppendSoftHyphen = false;
            }

            private void AppendTraillingSpaces()
            {
                this.AddRange(this.trailingSpaces);
                this.trailingSpaces = DXLineFormatter.ClusterCollectionView.EmptyView;
            }

            public List<DXCluster> GetClusters()
            {
                if (this.measureTrailingSpaces)
                {
                    this.AppendTraillingSpaces();
                }
                return this.Clusters;
            }

            public DXCluster? GetLineEndCluster()
            {
                if (!this.trailingSpaces.Empty)
                {
                    return new DXCluster?(this.trailingSpaces.Last<DXCluster>());
                }
                int num = (this.Clusters.Count - this.additionalClusters) - 1;
                if (num >= 0)
                {
                    return new DXCluster?(this.Clusters[num]);
                }
                return null;
            }

            public bool IsFit(float width) => 
                (this.Width + width) <= this.maxWidth;

            public bool TryAppendTabStop(DXCluster tabCluster, float tabStopSize)
            {
                float width = (tabStopSize != 0f) ? ((((float) Math.Ceiling((double) (this.Width / tabStopSize))) * tabStopSize) - this.Width) : 0f;
                if (width == 0f)
                {
                    width = tabStopSize;
                }
                if (!this.Empty && !this.IsFit(width))
                {
                    return false;
                }
                DXGlyphOffset offset = new DXGlyphOffset();
                DXGlyph[] glyphs = new DXGlyph[] { new DXGlyph(0, width, offset) };
                this.Add(new DXCluster(glyphs, tabCluster.Text, tabCluster.Breakpoint, tabCluster.BidiLevel, true));
                return true;
            }

            public bool TryEndLineWithEllipsis(DXLineFormatter.ClusterCollectionView ellipsis, bool ignoreHyphen)
            {
                bool flag = !ignoreHyphen && this.ShouldAppendSoftHyphen;
                float num = ellipsis.Width + (flag ? this.HyphenRun.Width : 0f);
                if ((this.currentWidth + num) > this.maxWidth)
                {
                    return false;
                }
                if (flag)
                {
                    this.AppendHyphen();
                }
                this.AddRange(ellipsis);
                this.additionalClusters += ellipsis.Count;
                this.ShouldAppendSoftHyphen = false;
                return true;
            }

            public bool Empty =>
                (!this.measureTrailingSpaces || this.trailingSpaces.Empty) ? (this.Clusters.Count == 0) : false;

            public bool EmptyAndNoTrailingSpaces =>
                this.Empty && this.trailingSpaces.Empty;

            public bool ShouldAppendSoftHyphen { get; private set; }

            private List<DXCluster> Clusters { get; } = new List<DXCluster>()

            private DXLineFormatter.ClusterCollectionView HyphenRun =>
                this.hyphenRun.Value;

            private float Width =>
                this.currentWidth + this.trailingSpaces.Width;

            public bool EndWithSoftHyphen { get; private set; }
        }

        private class MeasurerLinesBuilder : DXLineFormatter.ITextLineBuilder
        {
            private readonly float maxWidth;
            private DXLineFormatter.ParagraphInfo paragraph;

            public MeasurerLinesBuilder(float maxWidth)
            {
                this.maxWidth = maxWidth;
            }

            public unsafe void Add(DXLineFormatter.Line line)
            {
                StringView text;
                int count = this.Count;
                this.Count = count + 1;
                Func<DXCluster, float> selector = <>c.<>9__15_0;
                if (<>c.<>9__15_0 == null)
                {
                    Func<DXCluster, float> local1 = <>c.<>9__15_0;
                    selector = <>c.<>9__15_0 = c => c.Width;
                }
                this.MaxLineWidth = Math.Max(this.MaxLineWidth, Math.Min(line.GetClusters().Sum<DXCluster>(selector), this.maxWidth));
                DXCluster?* nullablePtr1 = &line.GetLineEndCluster();
                if (nullablePtr1 != null)
                {
                    text = nullablePtr1.GetValueOrDefault().Text;
                }
                else
                {
                    DXCluster?* local2 = nullablePtr1;
                    text = null;
                }
                StringView view = text;
                int paragraphsCharactersCount = (view != null) ? (view.Length + view.Offset) : 0;
                if (line.EndWithSoftHyphen)
                {
                    paragraphsCharactersCount++;
                }
                this.CharactersCount = this.paragraph.GetActualCharactersCount(paragraphsCharactersCount);
            }

            public void SetCurrentParagraph(DXLineFormatter.ParagraphInfo paragraph)
            {
                this.paragraph = paragraph;
            }

            public float MaxLineWidth { get; private set; }

            public int Count { get; private set; }

            public int CharactersCount { get; private set; }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly DXLineFormatter.MeasurerLinesBuilder.<>c <>9 = new DXLineFormatter.MeasurerLinesBuilder.<>c();
                public static Func<DXCluster, float> <>9__15_0;

                internal float <Add>b__15_0(DXCluster c) => 
                    c.Width;
            }
        }

        private class ParagraphInfo
        {
            private readonly int startPosition;
            private readonly int length;
            private readonly int endStringRunLength;
            private readonly string sourceString;
            private List<int> removedCharacters;

            public ParagraphInfo(int startPosition, int length, int endStringRunLength, string sourceString)
            {
                this.startPosition = startPosition;
                this.length = length;
                this.endStringRunLength = endStringRunLength;
                this.sourceString = sourceString;
            }

            public int GetActualCharactersCount(int paragraphsCharactersCount)
            {
                if (this.removedCharacters != null)
                {
                    foreach (int num in this.removedCharacters)
                    {
                        if (num >= paragraphsCharactersCount)
                        {
                            break;
                        }
                        paragraphsCharactersCount++;
                    }
                }
                return ((paragraphsCharactersCount != this.length) ? (paragraphsCharactersCount + this.startPosition) : ((paragraphsCharactersCount + this.startPosition) + this.endStringRunLength));
            }

            public string GetText(PdfHotkeyPrefix hotkeyPrefix)
            {
                string text = this.sourceString.Substring(this.startPosition, this.length);
                return ((hotkeyPrefix == PdfHotkeyPrefix.Hide) ? this.RemoveHotkeyPrefixes(text) : text);
            }

            private string RemoveHotkeyPrefixes(string text)
            {
                bool flag = false;
                StringBuilder builder = new StringBuilder(text.Length);
                this.removedCharacters = new List<int>();
                for (int i = 0; i < text.Length; i++)
                {
                    char ch = text[i];
                    if ((ch == '&') && !flag)
                    {
                        flag = true;
                        this.removedCharacters.Add(i);
                    }
                    else
                    {
                        builder.Append(ch);
                        flag = false;
                    }
                }
                return builder.ToString();
            }
        }

        private class Range
        {
            public Range(DXLineFormatter.ClusterCollectionView clusters, DXLineFormatter.ClusterCollectionView trailingSpaces, bool endsWithSoftHyphen = false)
            {
                this.<Clusters>k__BackingField = clusters;
                this.<TrailingSpaces>k__BackingField = trailingSpaces;
                this.<EndsWithSoftHyphen>k__BackingField = endsWithSoftHyphen;
            }

            public DXLineFormatter.Range Split(int i) => 
                new DXLineFormatter.Range(this.Clusters.Split(i), this.TrailingSpaces, this.EndsWithSoftHyphen);

            public float WidthWithoutTrailingSpaces =>
                this.Clusters.Width;

            public DXLineFormatter.ClusterCollectionView Clusters { get; }

            public DXLineFormatter.ClusterCollectionView TrailingSpaces { get; }

            public bool EndsWithSoftHyphen { get; }
        }

        private class RangeBuilder
        {
            private int startRange = -1;
            private int endRange = -1;
            private int endTrailingSpaces = -1;

            public void AppendGlyph(int index)
            {
                if (this.Empty)
                {
                    this.startRange = index;
                }
                this.endTrailingSpaces = -1;
                this.endRange = index;
            }

            public void AppendSpace(int index)
            {
                if (this.Empty)
                {
                    this.startRange = index;
                }
                this.endTrailingSpaces = index;
            }

            public DXLineFormatter.Range GetRange(IList<DXCluster> clusters, bool endsWithSoftHyphen = false) => 
                !this.Empty ? new DXLineFormatter.Range((this.endRange < 0) ? DXLineFormatter.ClusterCollectionView.EmptyView : new DXLineFormatter.ClusterCollectionView(clusters, this.startRange, this.endRange + 1), this.HasTrailingSpaces ? new DXLineFormatter.ClusterCollectionView(clusters, this.endRange + 1, this.endTrailingSpaces + 1) : DXLineFormatter.ClusterCollectionView.EmptyView, endsWithSoftHyphen) : new DXLineFormatter.Range(DXLineFormatter.ClusterCollectionView.EmptyView, DXLineFormatter.ClusterCollectionView.EmptyView, false);

            public bool Empty =>
                this.startRange == -1;

            private bool HasTrailingSpaces =>
                this.endTrailingSpaces != -1;
        }

        private class TextLinesBuilder : DXLineFormatter.ITextLineBuilder
        {
            public void Add(DXLineFormatter.Line line)
            {
                IList<DXCluster> clusters = line.GetClusters();
                DXBidiHelper.Reorder(clusters);
                this.Lines.Add(clusters);
            }

            public void SetCurrentParagraph(DXLineFormatter.ParagraphInfo paragraph)
            {
            }

            public IList<IList<DXCluster>> Lines { get; } = new List<IList<DXCluster>>()

            public int Count =>
                this.Lines.Count;
        }

        private enum Trimming
        {
            None,
            Word,
            Character
        }
    }
}

