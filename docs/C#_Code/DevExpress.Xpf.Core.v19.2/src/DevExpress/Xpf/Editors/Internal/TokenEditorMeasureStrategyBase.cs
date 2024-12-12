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

    public abstract class TokenEditorMeasureStrategyBase
    {
        private const double TokenMagicHeight = 20.0;

        public TokenEditorMeasureStrategyBase(TokenEditorPanel panel)
        {
            this.OwnerPanel = panel;
            this.Lines = new List<TokenEditorLineInfo>();
        }

        public abstract List<UIElement> Arrange(Size finalSize);
        public double CalcLinesHeight()
        {
            double height = 0.0;
            this.Lines.ForEach(delegate (TokenEditorLineInfo x) {
                height += x.Height;
            });
            return height;
        }

        internal double CalcLinesHeightFromOffset(double offset)
        {
            double num = 0.0;
            TokenEditorLineInfo lineByAbsolutIndex = this.GetLineByAbsolutIndex(this.OwnerPanel.OffsetToIndex(offset));
            if (lineByAbsolutIndex != null)
            {
                for (int i = this.Lines.IndexOf(lineByAbsolutIndex); i < this.LinesCount; i++)
                {
                    num += this.Lines[i].Height;
                }
            }
            return num;
        }

        public double CalcMaxLineHeight()
        {
            double height = 0.0;
            this.Lines.ForEach(delegate (TokenEditorLineInfo x) {
                height = Math.Max(height, x.Height);
            });
            return ((this.Lines.Count > 0) ? height : 20.0);
        }

        protected double CalcResultSize(double maxSize, double calcSize) => 
            Math.Max(0.0, Math.Min(maxSize, calcSize));

        protected double CalcTopPoint(double whole, double height)
        {
            double num = whole - height;
            return ((num > 0.0) ? (num / 2.0) : 0.0);
        }

        protected abstract bool CanArrange(int index, double x);
        public abstract int ConvertToEditableIndex(int visualIndex);
        public abstract int ConvertToVisibleIndex(int editableIndex);
        public void DestroyLines()
        {
            this.ShouldDestroyLines = true;
        }

        public double GetBottomTokenIndex() => 
            (double) this.GetFirstTokenIndexInLine(this.Lines.Count);

        public TokenEditorLineInfo GetContainedLine(int tokenIndex)
        {
            TokenEditorLineInfo info3;
            using (List<TokenEditorLineInfo>.Enumerator enumerator = this.Lines.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        TokenEditorLineInfo current = enumerator.Current;
                        using (List<TokenInfo>.Enumerator enumerator2 = current.Tokens.GetEnumerator())
                        {
                            while (true)
                            {
                                if (!enumerator2.MoveNext())
                                {
                                    break;
                                }
                                TokenInfo info2 = enumerator2.Current;
                                if (info2.VisibleIndex == tokenIndex)
                                {
                                    return current;
                                }
                            }
                        }
                        continue;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return info3;
        }

        private int GetFirstTokenIndexInLine(int index)
        {
            if (this.Lines.Count <= 0)
            {
                return 0;
            }
            Func<TokenEditorLineInfo, TokenInfo> evaluator = <>c.<>9__77_0;
            if (<>c.<>9__77_0 == null)
            {
                Func<TokenEditorLineInfo, TokenInfo> local1 = <>c.<>9__77_0;
                evaluator = <>c.<>9__77_0 = x => (x.Tokens.Count > 0) ? x.Tokens[0] : null;
            }
            TokenInfo info = this.Lines[index].Return<TokenEditorLineInfo, TokenInfo>(evaluator, <>c.<>9__77_1 ??= ((Func<TokenInfo>) (() => null)));
            return ((info != null) ? info.VisibleIndex : 0);
        }

        public int GetIndexOfLine(TokenEditorLineInfo line) => 
            this.Lines.IndexOf(line);

        public TokenEditorLineInfo GetLine(int lineIndex) => 
            (this.Lines.Count <= lineIndex) ? null : this.Lines[lineIndex];

        public TokenEditorLineInfo GetLineByAbsolutIndex(int lineIndex) => 
            (from x in this.Lines
                where x.Index == lineIndex
                select x).FirstOrDefault<TokenEditorLineInfo>();

        protected double GetTokenMaxWidth(double width) => 
            ((this.TokenMaxWidth <= 0.0) || (width <= this.TokenMaxWidth)) ? width : this.TokenMaxWidth;

        public double GetTopTokenIndex() => 
            (double) this.GetFirstTokenIndexInLine(0);

        public void InvalidateLines()
        {
            this.IsLinesValid = false;
        }

        public bool IsMaxVisibleIndex(int index) => 
            this.MaxVisibleIndex == index;

        public bool IsMinVisibleIndex(int index) => 
            index == this.MinVisibleIndex;

        public bool IsNewToken(int index) => 
            index == this.NewTokenVisibleIndex;

        public abstract Size Measure(Size constraint);
        public virtual int MeasureFromEnd(Size constraint, out double offset)
        {
            offset = 0.0;
            return 0;
        }

        protected void NumerateLines(TokenEditorLineInfo offsetLine)
        {
            int index = this.Lines.IndexOf(offsetLine);
            this.NumerateLinesCore(index, this.OffsetLineIndex);
        }

        protected void NumerateLinesCore(int realtiveLineIndex, int offsetLineIndex)
        {
            for (int i = 0; i < this.LinesCount; i++)
            {
                int num2 = i - realtiveLineIndex;
                this.Lines[i].Index = offsetLineIndex + num2;
            }
        }

        public void RenumerateLines()
        {
            int offsetLineIndex = this.OwnerPanel.OffsetToIndex(this.OwnerPanel.VerticalOffset);
            TokenEditorLineInfo lineByAbsolutIndex = this.GetLineByAbsolutIndex(this.OffsetLineIndex);
            if (lineByAbsolutIndex != null)
            {
                this.NumerateLinesCore(this.Lines.IndexOf(lineByAbsolutIndex), offsetLineIndex);
                this.OffsetLineIndex = offsetLineIndex;
            }
        }

        protected bool ShouldMeasureNewToken(int index) => 
            this.IsNewToken(index);

        public abstract System.Windows.Controls.Orientation Orientation { get; }

        public bool ShouldMeasureFromLastToken { get; protected set; }

        public int MaxVisibleIndex =>
            this.TokensCount;

        public int MinVisibleIndex =>
            0;

        public virtual int LinesCount =>
            this.Lines.Count;

        public int OffsetTokenIndex { get; protected set; }

        protected TokenEditorPanel OwnerPanel { get; private set; }

        protected bool ShowNewToken =>
            this.OwnerPanel.CanShowNewToken;

        protected int TokensCount =>
            this.OwnerPanel.Items.Count;

        protected TokenEditorPresenter NewToken =>
            this.OwnerPanel.NewTokenPresenter;

        protected abstract int NewTokenVisibleIndex { get; }

        protected List<TokenEditorLineInfo> Lines { get; set; }

        protected bool IsLinesValid { get; set; }

        protected bool ShouldDestroyLines { get; set; }

        protected int OffsetLineIndex { get; set; }

        protected bool CanArrangeNewToken =>
            this.ShowNewToken || this.NewToken.HasNullText;

        private double TokenMaxWidth =>
            this.OwnerPanel.GetTokenMaxWidth();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TokenEditorMeasureStrategyBase.<>c <>9 = new TokenEditorMeasureStrategyBase.<>c();
            public static Func<TokenEditorLineInfo, TokenInfo> <>9__77_0;
            public static Func<TokenInfo> <>9__77_1;

            internal TokenInfo <GetFirstTokenIndexInLine>b__77_0(TokenEditorLineInfo x) => 
                (x.Tokens.Count > 0) ? x.Tokens[0] : null;

            internal TokenInfo <GetFirstTokenIndexInLine>b__77_1() => 
                null;
        }
    }
}

