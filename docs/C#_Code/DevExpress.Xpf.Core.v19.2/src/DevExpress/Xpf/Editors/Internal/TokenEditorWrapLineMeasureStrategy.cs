namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    public class TokenEditorWrapLineMeasureStrategy : TokenEditorMeasureStrategyBase
    {
        private const int AdditionalLines = 2;
        private Size prevConstraint;
        private int prevMagic;

        public TokenEditorWrapLineMeasureStrategy(TokenEditorPanel panel) : base(panel)
        {
        }

        public override List<UIElement> Arrange(Size finalSize)
        {
            int startLineIndex = Math.Max(0, base.OffsetLineIndex);
            TokenEditorLineInfo item = (from x in base.Lines
                where x.Index == startLineIndex
                select x).FirstOrDefault<TokenEditorLineInfo>();
            if (item == null)
            {
                return new List<UIElement>();
            }
            List<UIElement> list = new List<UIElement>();
            double y = base.OwnerPanel.IndexToOffset(startLineIndex) - base.OwnerPanel.VerticalOffset;
            double num2 = 0.0;
            for (int i = base.Lines.IndexOf(item); i < base.Lines.Count; i++)
            {
                TokenEditorLineInfo info2 = base.Lines[i];
                double num5 = 0.0;
                foreach (TokenInfo info3 in info2.Tokens)
                {
                    int visibleIndex = info3.VisibleIndex;
                    UIElement container = null;
                    double tokenMaxWidth = 0.0;
                    container = base.OwnerPanel.GetContainer(this.ConvertToEditableIndex(visibleIndex));
                    if (container == null)
                    {
                        break;
                    }
                    if (!base.IsNewToken(visibleIndex))
                    {
                        tokenMaxWidth = base.GetTokenMaxWidth(container.DesiredSize.Width);
                        num2 = Math.Max(num2, container.DesiredSize.Height);
                    }
                    else if (base.ShowNewToken)
                    {
                        tokenMaxWidth = (base.TokensCount > 0) ? container.DesiredSize.Width : finalSize.Width;
                        if (base.OwnerPanel.ShowNewTokenFromEnd)
                        {
                            tokenMaxWidth = Math.Max(tokenMaxWidth, finalSize.Width - num5);
                        }
                        num2 = Math.Max(num2, container.DesiredSize.Height);
                    }
                    Rect finalRect = new Rect(num5, y, tokenMaxWidth, num2);
                    container.Arrange(finalRect);
                    list.Add(container);
                    num5 += finalRect.Width;
                }
                y += num2;
            }
            return list;
        }

        private int CalcFirstVisibleTokenIndex()
        {
            int lineIndex = Math.Max(0, base.OwnerPanel.OffsetToIndex(base.OwnerPanel.VerticalOffset));
            if (lineIndex == 0)
            {
                return base.MinVisibleIndex;
            }
            Func<TokenEditorLineInfo, TokenInfo> evaluator = <>c.<>9__13_0;
            if (<>c.<>9__13_0 == null)
            {
                Func<TokenEditorLineInfo, TokenInfo> local1 = <>c.<>9__13_0;
                evaluator = <>c.<>9__13_0 = x => (x.Tokens.Count > 0) ? x.Tokens[0] : null;
            }
            TokenInfo info = base.GetLineByAbsolutIndex(lineIndex).Return<TokenEditorLineInfo, TokenInfo>(evaluator, <>c.<>9__13_1 ??= ((Func<TokenInfo>) (() => null)));
            return ((info != null) ? info.VisibleIndex : 0);
        }

        private int CalcFirstVisibleTokenIndexByMagic()
        {
            int lineIndex = Math.Max(0, base.OwnerPanel.OffsetToIndex(base.OwnerPanel.PreviousVerticalOffset));
            Func<TokenEditorLineInfo, int> evaluator = <>c.<>9__12_0;
            if (<>c.<>9__12_0 == null)
            {
                Func<TokenEditorLineInfo, int> local1 = <>c.<>9__12_0;
                evaluator = <>c.<>9__12_0 = x => x.Tokens[0].VisibleIndex;
            }
            int num2 = base.GetLineByAbsolutIndex(lineIndex).Return<TokenEditorLineInfo, int>(evaluator, <>c.<>9__12_1 ??= () => -1);
            if (num2 == -1)
            {
                return 0;
            }
            if (base.OwnerPanel.PreviousVerticalOffset <= base.OwnerPanel.VerticalOffset)
            {
                double num3 = ((double) (base.TokensCount - num2)) / Math.Abs((double) (base.OwnerPanel.PreviousVerticalOffset - base.OwnerPanel.ExtentHeight));
                int num5 = (int) (((this.prevMagic == 0) ? ((double) num2) : ((double) this.prevMagic)) + (num3 * Math.Abs((double) (base.OwnerPanel.PreviousVerticalOffset - base.OwnerPanel.VerticalOffset))));
                this.prevMagic = (num5 < base.TokensCount) ? num5 : num2;
                return this.prevMagic;
            }
            double num6 = ((double) num2) / base.OwnerPanel.PreviousVerticalOffset;
            int num8 = (int) (((this.prevMagic == 0) ? ((double) num2) : ((double) this.prevMagic)) - (num6 * Math.Abs((double) (base.OwnerPanel.PreviousVerticalOffset - base.OwnerPanel.VerticalOffset))));
            this.prevMagic = Math.Max(0, num8);
            return this.prevMagic;
        }

        private double CalcMaxLineWidth()
        {
            double num = 0.0;
            foreach (TokenEditorLineInfo info in base.Lines)
            {
                double lineWidth = 0.0;
                info.Tokens.ForEach(delegate (TokenInfo x) {
                    lineWidth += x.Size.Width;
                });
                num = Math.Max(num, lineWidth);
            }
            return num;
        }

        protected override bool CanArrange(int index, double x) => 
            index <= base.MaxVisibleIndex;

        public override int ConvertToEditableIndex(int visualIndex) => 
            base.IsNewToken(visualIndex) ? base.TokensCount : (visualIndex - 1);

        public override int ConvertToVisibleIndex(int editableIndex) => 
            (editableIndex == base.TokensCount) ? this.NewTokenVisibleIndex : (editableIndex + 1);

        private void CreateLines(Size constraint)
        {
            double width;
            int index = base.OffsetTokenIndex - 1;
            List<TokenInfo> tokens = new List<TokenInfo>();
            int num2 = 2;
            double lineHeight = 0.0;
            double width = 0.0;
            if ((index <= -1) || (index > base.TokensCount))
            {
                goto TR_0010;
            }
            else
            {
                while (index > -1)
                {
                    Size size = this.MeasureToken(constraint, index, ref lineHeight);
                    if ((width + size.Width) <= constraint.Width)
                    {
                        tokens.Insert(0, new TokenInfo(index, size));
                    }
                    else
                    {
                        width = size.Width;
                        if (tokens.Count == 0)
                        {
                            tokens.Add(new TokenInfo(index, size));
                            base.Lines.Insert(0, new TokenEditorLineInfo(tokens));
                            tokens = new List<TokenInfo>();
                            width = 0.0;
                        }
                        else
                        {
                            base.Lines.Insert(0, new TokenEditorLineInfo(tokens));
                            List<TokenInfo> list1 = new List<TokenInfo>();
                            list1.Add(new TokenInfo(index, size));
                            tokens = list1;
                        }
                        num2--;
                        if (num2 == 0)
                        {
                            break;
                        }
                    }
                    index--;
                }
            }
            if ((index == -1) && (num2 != 0))
            {
                base.Lines.Insert(0, new TokenEditorLineInfo(tokens));
            }
        TR_0010:
            width = 0.0;
            double num6 = 0.0;
            num2 = 2;
            tokens = new List<TokenInfo>();
            for (index = base.OffsetTokenIndex; index <= base.TokensCount; index++)
            {
                Size size = this.MeasureToken(constraint, index, ref lineHeight);
                if ((width + size.Width) <= constraint.Width)
                {
                    tokens.Add(new TokenInfo(index, size));
                }
                else
                {
                    width = size.Width;
                    num6 += lineHeight;
                    if (tokens.Count == 0)
                    {
                        tokens.Add(new TokenInfo(index, size));
                        base.Lines.Add(new TokenEditorLineInfo(tokens));
                        tokens = new List<TokenInfo>();
                        width = 0.0;
                    }
                    else
                    {
                        base.Lines.Add(new TokenEditorLineInfo(tokens));
                        List<TokenInfo> list2 = new List<TokenInfo>();
                        list2.Add(new TokenInfo(index, size));
                        tokens = list2;
                    }
                    if (num6 > constraint.Height)
                    {
                        num2--;
                    }
                    if (num2 == 0)
                    {
                        break;
                    }
                }
            }
            if (num2 != 0)
            {
                base.Lines.Add(new TokenEditorLineInfo(tokens));
            }
            base.NumerateLines(base.GetContainedLine(base.OffsetTokenIndex));
        }

        private bool IsSizeEmpty(Size size) => 
            (size.Height == 0.0) && (size.Width == 0.0);

        public override Size Measure(Size constraint)
        {
            if (!this.IsSizeEmpty(this.prevConstraint) && (this.prevConstraint != constraint))
            {
                base.ShouldDestroyLines = true;
            }
            this.prevConstraint = constraint;
            base.OffsetLineIndex = base.OwnerPanel.OffsetToIndex(base.OwnerPanel.VerticalOffset);
            double maxSize = 0.0;
            TokenEditorLineInfo lineByAbsolutIndex = base.GetLineByAbsolutIndex(base.OffsetLineIndex);
            bool flag = false;
            if (!base.ShouldDestroyLines)
            {
                flag = this.MeasureOldLines(constraint, base.OffsetLineIndex, lineByAbsolutIndex);
            }
            else
            {
                base.ShouldDestroyLines = false;
                this.OffsetTokenIndex = (lineByAbsolutIndex == null) ? this.CalcFirstVisibleTokenIndexByMagic() : this.CalcFirstVisibleTokenIndex();
                base.Lines = new List<TokenEditorLineInfo>();
            }
            if (base.Lines.Count == 0)
            {
                this.CreateLines(constraint);
            }
            else if (!flag)
            {
                int num3 = 0;
                while (true)
                {
                    if (num3 >= this.LinesCount)
                    {
                        base.NumerateLines(lineByAbsolutIndex);
                        break;
                    }
                    this.MeasureLine(constraint, base.Lines[num3]);
                    maxSize += base.Lines[num3].Height;
                    num3++;
                }
            }
            maxSize = base.CalcLinesHeight();
            double num2 = this.CalcMaxLineWidth();
            return new Size(base.CalcResultSize(num2, constraint.Width), base.CalcResultSize(maxSize, constraint.Height));
        }

        public override int MeasureFromEnd(Size constraint, out double offset)
        {
            Size size = new Size();
            int maxVisibleIndex = base.MaxVisibleIndex;
            List<TokenInfo> tokens = new List<TokenInfo>();
            double width = 0.0;
            double num3 = 0.0;
            List<TokenEditorLineInfo> list2 = new List<TokenEditorLineInfo>();
            while ((maxVisibleIndex > -1) && (num3 < constraint.Height))
            {
                double lineHeight = 0.0;
                size = this.MeasureToken(constraint, maxVisibleIndex, ref lineHeight);
                if ((width + size.Width) > constraint.Width)
                {
                    width = size.Width;
                    num3 += lineHeight;
                    list2.Insert(0, new TokenEditorLineInfo(tokens));
                    tokens = new List<TokenInfo>();
                }
                tokens.Insert(0, new TokenInfo(maxVisibleIndex, size));
                maxVisibleIndex--;
            }
            if (list2.Count > 0)
            {
                TokenEditorLineInfo containedLine = base.GetContainedLine(list2[0].Tokens[0].VisibleIndex);
                base.Lines = list2;
                int offsetLineIndex = (containedLine != null) ? containedLine.Index : (list2[0].Tokens[0].VisibleIndex / 3);
                base.NumerateLinesCore(0, offsetLineIndex);
                base.OffsetLineIndex = base.Lines[0].Index;
            }
            int num4 = base.OwnerPanel.OffsetToIndex(base.OwnerPanel.VerticalOffset);
            offset = Math.Abs((double) (base.OwnerPanel.VerticalOffset - num4));
            return num4;
        }

        private void MeasureLine(Size constraint, TokenEditorLineInfo line)
        {
            foreach (TokenInfo info in line.Tokens)
            {
                double lineHeight = 0.0;
                this.MeasureToken(constraint, info.VisibleIndex, ref lineHeight);
            }
        }

        private List<TokenEditorLineInfo> MeasureLinesOnScrollDown(Size constraint, TokenEditorLineInfo newOffsetLine)
        {
            double num6;
            Size size;
            List<TokenInfo> list2;
            List<TokenEditorLineInfo> lines = new List<TokenEditorLineInfo>();
            int lineIndex = base.OwnerPanel.OffsetToIndex(base.OwnerPanel.PreviousVerticalOffset);
            TokenEditorLineInfo lineByAbsolutIndex = base.GetLineByAbsolutIndex(lineIndex);
            double width = 0.0;
            base.OffsetTokenIndex = this.CalcFirstVisibleTokenIndex();
            int shiftCount = (base.Lines.IndexOf(newOffsetLine) + 1) - 2;
            int num4 = shiftCount;
            double lineHeight = base.CalcMaxLineHeight();
            if ((base.CalcMaxLineHeight() * base.Lines.Count) >= ((2.0 * base.CalcMaxLineHeight()) + constraint.Height))
            {
                lines = this.ShiftLines(shiftCount);
            }
            else
            {
                lines = base.Lines;
                num4 = 2;
            }
            int num9 = 0;
            while (true)
            {
                if (num9 < lines.Count)
                {
                    this.MeasureLine(constraint, lines[num9]);
                    num9++;
                    continue;
                }
                num6 = lineHeight * Math.Abs((int) (lines.IndexOf(newOffsetLine) - lines.Count));
                int index = lines[lines.Count - 1].Tokens.Last<TokenInfo>().VisibleIndex + 1;
                if ((index == -1) || ((num4 <= 0) || (index > base.MaxVisibleIndex)))
                {
                    return lines;
                }
                else
                {
                    size = new Size();
                    list2 = new List<TokenInfo>();
                    while (index <= base.MaxVisibleIndex)
                    {
                        size = this.MeasureToken(constraint, index, ref lineHeight);
                        if ((width + size.Width) <= constraint.Width)
                        {
                            list2.Add(new TokenInfo(index, size));
                        }
                        else
                        {
                            width = size.Width;
                            if (list2.Count == 0)
                            {
                                list2.Add(new TokenInfo(index, size));
                                lines.Add(new TokenEditorLineInfo(list2));
                                list2 = new List<TokenInfo>();
                            }
                            else
                            {
                                lines.Add(new TokenEditorLineInfo(list2));
                                List<TokenInfo> list1 = new List<TokenInfo>();
                                list1.Add(new TokenInfo(index, size));
                                list2 = list1;
                            }
                            num4--;
                            if (num4 <= 0)
                            {
                                break;
                            }
                        }
                        index++;
                    }
                }
                break;
            }
            if (num4 != 0)
            {
                num6 += size.Height;
                lines.Add(new TokenEditorLineInfo(list2));
            }
            return lines;
        }

        private List<TokenEditorLineInfo> MeasureLinesOnScrollUp(Size constraint, TokenEditorLineInfo newOffsetLine)
        {
            int num5;
            int num6;
            List<TokenInfo> list2;
            List<TokenEditorLineInfo> list = new List<TokenEditorLineInfo>();
            int lineIndex = base.OwnerPanel.OffsetToIndex(base.OwnerPanel.PreviousVerticalOffset);
            TokenEditorLineInfo lineByAbsolutIndex = base.GetLineByAbsolutIndex(lineIndex);
            base.OffsetTokenIndex = this.CalcFirstVisibleTokenIndex();
            int num2 = Math.Abs((int) (base.Lines.IndexOf(newOffsetLine) - base.Lines.IndexOf(lineByAbsolutIndex)));
            double num3 = base.CalcMaxLineHeight() * base.Lines.Count;
            double num4 = (2.0 * base.CalcMaxLineHeight()) + constraint.Height;
            list = ((base.GetContainedLine(base.MinVisibleIndex) != null) || (num3 < num4)) ? base.Lines : this.ShiftLines(-num2);
            int num7 = 0;
            while (true)
            {
                if (num7 < list.Count)
                {
                    this.MeasureLine(constraint, list[num7]);
                    num7++;
                    continue;
                }
                num5 = (list.IndexOf(newOffsetLine) > 0) ? num2 : 2;
                num6 = Math.Max(base.MinVisibleIndex, base.Lines[0].Tokens[0].VisibleIndex - 1);
                if ((base.GetContainedLine(num6) != null) || (num5 <= 0))
                {
                    return list;
                }
                else
                {
                    Size size = new Size();
                    list2 = new List<TokenInfo>();
                    double width = 0.0;
                    while (num6 > -1)
                    {
                        double lineHeight = 0.0;
                        size = this.MeasureToken(constraint, num6, ref lineHeight);
                        if ((width + size.Width) <= constraint.Width)
                        {
                            list2.Insert(0, new TokenInfo(num6, size));
                        }
                        else
                        {
                            width = size.Width;
                            if (list2.Count == 0)
                            {
                                list2.Add(new TokenInfo(num6, size));
                                list.Insert(0, new TokenEditorLineInfo(list2));
                                list2 = new List<TokenInfo>();
                                width = 0.0;
                            }
                            else
                            {
                                list.Insert(0, new TokenEditorLineInfo(list2));
                                List<TokenInfo> list1 = new List<TokenInfo>();
                                list1.Add(new TokenInfo(num6, size));
                                list2 = list1;
                            }
                            num5--;
                            if (num5 <= 0)
                            {
                                break;
                            }
                        }
                        num6--;
                    }
                }
                break;
            }
            if ((num6 == -1) && (num5 != 0))
            {
                list.Insert(0, new TokenEditorLineInfo(list2));
            }
            return list;
        }

        private Size MeasureNewToken(Size constraint, ref double wholeHeight)
        {
            base.NewToken.Measure(constraint);
            base.OwnerPanel.AddNewTokenContainerToMeasured();
            wholeHeight = Math.Max(wholeHeight, base.NewToken.DesiredSize.Height);
            return base.NewToken.DesiredSize;
        }

        private bool MeasureOldLines(Size constraint, int newOffsetLineIndex, TokenEditorLineInfo newOffsetLine)
        {
            if (base.IsLinesValid)
            {
                return false;
            }
            base.IsLinesValid = true;
            bool flag = false;
            if (newOffsetLine == null)
            {
                if (this.LinesCount != 0)
                {
                    base.OffsetTokenIndex = this.CalcFirstVisibleTokenIndexByMagic();
                }
                base.Lines = new List<TokenEditorLineInfo>();
            }
            else if (Math.Abs((double) (base.OwnerPanel.PreviousVerticalOffset - base.OwnerPanel.VerticalOffset)) > 0.0)
            {
                flag = true;
                this.prevMagic = 0;
                List<TokenEditorLineInfo> list = new List<TokenEditorLineInfo>();
                list = (base.OwnerPanel.VerticalOffset < base.OwnerPanel.PreviousVerticalOffset) ? this.MeasureLinesOnScrollUp(constraint, newOffsetLine) : this.MeasureLinesOnScrollDown(constraint, newOffsetLine);
                base.Lines = list;
                base.NumerateLines(newOffsetLine);
            }
            if (newOffsetLineIndex == 0)
            {
                base.OffsetTokenIndex = base.MinVisibleIndex;
            }
            return flag;
        }

        private Size MeasureToken(Size constraint, int index, ref double lineHeight)
        {
            if (base.ShouldMeasureNewToken(index))
            {
                this.MeasureNewToken(constraint, ref lineHeight);
                return (base.ShowNewToken ? new Size(base.GetTokenMaxWidth(base.NewToken.DesiredSize.Width), base.NewToken.DesiredSize.Height) : new Size(0.0, base.NewToken.DesiredSize.Height));
            }
            constraint = new Size(base.GetTokenMaxWidth(constraint.Width), constraint.Height);
            return this.MeasureTokenCore(constraint, ref lineHeight, index);
        }

        private Size MeasureTokenCore(Size constraint, ref double wholeHeight, int index)
        {
            UIElement element = base.OwnerPanel.PrepareContainer(this.ConvertToEditableIndex(index));
            if (element == null)
            {
                return new Size();
            }
            element.Measure(constraint);
            wholeHeight = Math.Max(wholeHeight, element.DesiredSize.Height);
            return new Size(base.GetTokenMaxWidth(element.DesiredSize.Width), element.DesiredSize.Height);
        }

        private List<TokenEditorLineInfo> ShiftLines(int shiftCount)
        {
            List<TokenEditorLineInfo> list = new List<TokenEditorLineInfo>();
            for (int i = 0; i < base.Lines.Count; i++)
            {
                int num2 = i - shiftCount;
                if ((num2 > -1) && (num2 < this.LinesCount))
                {
                    list.Add(base.Lines[i]);
                }
            }
            return list;
        }

        public override System.Windows.Controls.Orientation Orientation =>
            System.Windows.Controls.Orientation.Vertical;

        protected override int NewTokenVisibleIndex =>
            0;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TokenEditorWrapLineMeasureStrategy.<>c <>9 = new TokenEditorWrapLineMeasureStrategy.<>c();
            public static Func<TokenEditorLineInfo, int> <>9__12_0;
            public static Func<int> <>9__12_1;
            public static Func<TokenEditorLineInfo, TokenInfo> <>9__13_0;
            public static Func<TokenInfo> <>9__13_1;

            internal TokenInfo <CalcFirstVisibleTokenIndex>b__13_0(TokenEditorLineInfo x) => 
                (x.Tokens.Count > 0) ? x.Tokens[0] : null;

            internal TokenInfo <CalcFirstVisibleTokenIndex>b__13_1() => 
                null;

            internal int <CalcFirstVisibleTokenIndexByMagic>b__12_0(TokenEditorLineInfo x) => 
                x.Tokens[0].VisibleIndex;

            internal int <CalcFirstVisibleTokenIndexByMagic>b__12_1() => 
                -1;
        }
    }
}

