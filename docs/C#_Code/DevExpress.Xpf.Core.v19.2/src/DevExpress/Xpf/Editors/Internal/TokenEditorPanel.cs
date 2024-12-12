namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.EditStrategy;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;

    public class TokenEditorPanel : Panel, IScrollInfo
    {
        private const double Epsilon = 1E-09;
        private const double TokenMagicWidth = 50.0;
        private const int TokensInLine = 2;
        public static readonly DependencyProperty DisplayItemsProperty;
        public static readonly DependencyProperty EnableTokenWrappingProperty;
        public static readonly DependencyProperty TokenContainerTemplateProperty;
        public static readonly DependencyProperty EmptyTokenContainerTemplateProperty;
        private TokenEditor owner;
        private double lineStep = 20.0;
        private Locker horizontalOffsetCorrectionLocker = new Locker();
        private Point prevMovePoint;
        private bool isLeftScrollDirection;
        private bool isUpScrollDirection;
        private Locker correctOffsetLocker = new Locker();
        private Dictionary<int, UIElement> measuredItems = new Dictionary<int, UIElement>();
        private Dictionary<int, UIElement> previousMeasuredItems = new Dictionary<int, UIElement>();
        private Stack<UIElement> recycled = new Stack<UIElement>();
        private ScrollData scrollData = new ScrollData();

        static TokenEditorPanel()
        {
            Type ownerType = typeof(TokenEditorPanel);
            DisplayItemsProperty = DependencyProperty.Register("DisplayItems", typeof(List<CustomItem>), ownerType, new FrameworkPropertyMetadata((d, e) => ((TokenEditorPanel) d).OnDisplayItemsChanged((List<CustomItem>) e.OldValue, (List<CustomItem>) e.NewValue)));
            EnableTokenWrappingProperty = DependencyProperty.Register("EnableTokenWrapping", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure));
            TokenContainerTemplateProperty = DependencyProperty.Register("TokenContainerTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((TokenEditorPanel) d).OnTokenContainerTemplateChanged(), (d, e) => ((TokenEditorPanel) d).CoerceTokenContainerTemplate(e)));
            EmptyTokenContainerTemplateProperty = DependencyProperty.Register("EmptyTokenContainerTemplate", typeof(ControlTemplate), ownerType);
        }

        public TokenEditorPanel()
        {
            this.Items = new List<CustomItem>();
            base.DefaultStyleKey = typeof(TokenEditorPanel);
            base.Loaded += new RoutedEventHandler(this.TokenEditorPanel_Loaded);
            this.MakeVisibleLocker = new Locker();
            this.MakeVisibleLocker.LockOnce();
            base.SetValue(KeyboardNavigation.TabNavigationProperty, KeyboardNavigationMode.None);
        }

        internal void AddNewTokenContainerToMeasured()
        {
            this.measuredItems.Add(this.Items.Count, this.NewTokenPresenter);
        }

        private bool AreClose(double value1, double value2) => 
            Math.Abs((double) (value1 - value2)) < 1E-09;

        protected override Size ArrangeOverride(Size finalSize)
        {
            if ((this.Owner != null) && (this.MeasureStrategy != null))
            {
                this.UpdateScrollData(finalSize);
                List<UIElement> arranged = this.MeasureStrategy.Arrange(finalSize);
                this.EndArrange(arranged);
                base.Clip = new RectangleGeometry(new Rect(0.0, 0.0, finalSize.Width, finalSize.Height));
            }
            return finalSize;
        }

        public void BringIntoView(TokenEditorPresenter token)
        {
            if ((token != null) && !this.LockBringIntoView)
            {
                this.MakeVisibleLocker.Unlock();
                this.MakeVisible(token, new Rect(0.0, 0.0, token.ActualWidth, token.ActualHeight));
            }
        }

        internal bool BringIntoViewByIndex(int index)
        {
            TokenEditorPresenter tokenByVisibleIndex = this.GetTokenByVisibleIndex(index);
            this.BringIntoView(tokenByVisibleIndex);
            return (tokenByVisibleIndex != null);
        }

        private Size CalcExtentSize() => 
            (this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? this.CalcHorizontalExtentSize() : this.CalcVerticalExtentSize();

        private Size CalcHorizontalExtentSize()
        {
            double width = 0.0;
            for (int i = 0; i <= this.MeasureStrategy.MaxVisibleIndex; i++)
            {
                width += this.GetTokenSize(i);
            }
            return new Size(width, this.ViewportHeight);
        }

        private double CalcLocation(Track track, double wholeRange, double viewport, double offset, out double thumbLength)
        {
            double num = wholeRange + viewport;
            thumbLength = (track.ActualWidth * viewport) / num;
            double num2 = track.ActualWidth - thumbLength;
            double num3 = (num2 * offset) / wholeRange;
            double num4 = num2 - num3;
            return (track.IsDirectionReversed ? num4 : num3);
        }

        internal void CalcRelativeOffsetAndIndex(ref double x, ref int index)
        {
            index = this.OffsetToIndex(this.HorizontalOffset);
            x = this.IndexToOffset(index) - this.HorizontalOffset;
            Func<int, int> keySelector = <>c.<>9__218_1;
            if (<>c.<>9__218_1 == null)
            {
                Func<int, int> local1 = <>c.<>9__218_1;
                keySelector = <>c.<>9__218_1 = k => k;
            }
            List<int> list = (from k in this.measuredItems.Keys select this.MeasureStrategy.ConvertToVisibleIndex(k)).OrderBy<int, int>(keySelector).ToList<int>();
            int num = (list.Count > 0) ? list[0] : 0;
            for (int i = num; i < index; i++)
            {
                x -= this.GetTokenSize(i);
            }
            index = num;
        }

        private void CalcStartMeasureIndex()
        {
            this.StartMeasureIndex = this.OffsetToIndex((this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? this.HorizontalOffset : this.VerticalOffset);
            this.PreviousTokenStartPosition = this.IndexToOffset(this.StartMeasureIndex);
        }

        private bool CalcStopCriteria(int index, double wholeWidth, double viewport) => 
            (wholeWidth < viewport) && (index < this.Items.Count);

        private Size CalcVerticalExtentSize()
        {
            int num = 0;
            double height = 0.0;
            double num3 = this.MeasureStrategy.CalcMaxLineHeight();
            TokenEditorLineInfo line = this.MeasureStrategy.GetLine(0);
            double num4 = 0.0;
            num4 = (line == null) ? this.ViewportHeight : this.IndexToOffset(line.Index);
            double num5 = (num3 > 0.0) ? (num4 / num3) : 0.0;
            height = (num5 * num3) + this.MeasureStrategy.CalcLinesHeight();
            int num6 = this.MeasureStrategy.GetLine(this.MeasureStrategy.LinesCount - 1).Return<TokenEditorLineInfo, int>(<>c.<>9__198_0 ??= x => ((x.Tokens.Count > 0) ? x.Tokens[x.Tokens.Count - 1].VisibleIndex : 0), <>c.<>9__198_1 ??= () => 0);
            double num7 = Math.Ceiling((double) (((double) (this.Items.Count - num6)) / 2.0));
            height += num7 * num3;
            num = ((int) (num5 + num7)) + this.MeasureStrategy.LinesCount;
            this.MaxLineIndex = num;
            return new Size(this.ViewportWidth, height);
        }

        private Size CalcViewport(Size size) => 
            size;

        internal void Clear()
        {
            this.NewTokenPresenter = null;
            base.InternalChildren.Clear();
            this.measuredItems.Clear();
            this.previousMeasuredItems.Clear();
            this.recycled.Clear();
        }

        private void ClearMeasuredItems()
        {
            Func<KeyValuePair<int, UIElement>, KeyValuePair<int, UIElement>> selector = <>c.<>9__174_1;
            if (<>c.<>9__174_1 == null)
            {
                Func<KeyValuePair<int, UIElement>, KeyValuePair<int, UIElement>> local1 = <>c.<>9__174_1;
                selector = <>c.<>9__174_1 = x => x;
            }
            KeyValuePair<int, UIElement> pair = (from x in this.measuredItems
                where x.Value == this.NewTokenPresenter
                select x).Select<KeyValuePair<int, UIElement>, KeyValuePair<int, UIElement>>(selector).FirstOrDefault<KeyValuePair<int, UIElement>>();
            if (pair.Value != null)
            {
                this.measuredItems.Remove(pair.Key);
            }
            this.previousMeasuredItems.AddRange<int, UIElement>(this.measuredItems);
            this.measuredItems.Clear();
        }

        public void ClearNewTokenValue()
        {
            this.UpdateNewTokenPresenter();
            base.InvalidateMeasure();
        }

        private object CoerceTokenContainerTemplate(object baseValue) => 
            (baseValue == null) ? this.TokenContainerTemplate : baseValue;

        private bool ContainsBounds(Rect bounds)
        {
            Rect rect = new Rect(0.0, 0.0, base.ActualWidth, base.ActualHeight);
            return (this.LessOrClose(rect.X, bounds.X) && (this.LessOrClose(rect.Y, bounds.Y) && (this.GreaterOrClose(rect.Right, bounds.Right) && this.GreaterOrClose(rect.Bottom, bounds.Bottom))));
        }

        internal int ConvertToEditableIndex(int visibleIndex) => 
            this.MeasureStrategy.ConvertToEditableIndex(visibleIndex);

        internal int ConvertToVisibleIndex(int editableIndex) => 
            this.MeasureStrategy.ConvertToVisibleIndex(editableIndex);

        private void CreateNewTokenEditor()
        {
            TokenEditorPresenter presenter1 = new TokenEditorPresenter();
            presenter1.BorderTemplate = this.EmptyTokenContainerTemplate;
            presenter1.ShowBorder = false;
            presenter1.ShowButtons = true;
            presenter1.NullText = this.Owner.GetNullText();
            presenter1.IsNewTokenEditorPresenter = true;
            presenter1.Item = new CustomItem();
            presenter1.IsTextEditable = this.Owner.ShowNewToken;
            this.NewTokenPresenter = presenter1;
            if (this.Owner.TokenStyle != null)
            {
                this.NewTokenPresenter.Style = this.Owner.TokenStyle;
            }
            base.InternalChildren.Add(this.NewTokenPresenter);
            this.NewTokenPresenter.UpdateEditor();
        }

        private TokenEditorPresenter CreateTokenPresenter()
        {
            TokenEditorPresenter presenter1 = new TokenEditorPresenter();
            presenter1.IsTabStop = false;
            presenter1.BorderTemplate = this.Owner.TokenBorderTemplate;
            presenter1.ShowButtons = this.Owner.ShowTokenButtons;
            presenter1.IsNewTokenEditorPresenter = false;
            presenter1.IsTextEditable = this.Owner.CanActivateToken();
            TokenEditorPresenter presenter = presenter1;
            if (this.Owner.TokenStyle != null)
            {
                presenter.Style = this.Owner.TokenStyle;
            }
            return presenter;
        }

        private void EndArrange(List<UIElement> arranged)
        {
            for (int i = 0; i < base.InternalChildren.Count; i++)
            {
                UIElement item = base.InternalChildren[i];
                if (!arranged.Contains(item))
                {
                    item.Arrange(new Rect(-1.0, 0.0, 0.0, 0.0));
                }
            }
        }

        private void EndMeasure()
        {
            foreach (int num in this.previousMeasuredItems.Keys)
            {
                UIElement objA = this.previousMeasuredItems[num];
                if (!ReferenceEquals(objA, this.NewTokenPresenter))
                {
                    ((TokenEditorPresenter) objA).Clear();
                    this.recycled.Push(objA);
                }
            }
            this.previousMeasuredItems.Clear();
        }

        private double EnsureHorizontalOffset(double offset)
        {
            offset = Math.Max(0.0, offset);
            if ((offset + this.ViewportWidth) > this.ExtentWidth)
            {
                offset = this.GetMaxHorizontalOffset();
            }
            return offset;
        }

        public void EnsureOffset()
        {
            if (this.MeasureStrategy != null)
            {
                if (this.Orientation != System.Windows.Controls.Orientation.Horizontal)
                {
                    if (this.VerticalOffset > this.GetMaxVerticalOffset())
                    {
                        this.SetVerticalOffset(this.GetMaxVerticalOffset());
                    }
                }
                else if (this.HorizontalOffset > this.GetMaxHorizontalOffset())
                {
                    this.SetHorizontalOffset(this.GetMaxHorizontalOffset());
                }
            }
        }

        private double EnsureVerticalOffset(double offset)
        {
            offset = Math.Max(0.0, offset);
            if ((offset + this.ViewportHeight) > this.ExtentHeight)
            {
                offset = this.GetMaxVerticalOffset();
            }
            return offset;
        }

        private double Floor(double value)
        {
            double num = Math.Round(value);
            return (!this.AreClose(num, value) ? Math.Floor(value) : num);
        }

        private UIElement GenerateContainer(int index, CustomItem value)
        {
            UIElement element = null;
            if (this.previousMeasuredItems.TryGetValue(index, out element))
            {
                this.previousMeasuredItems.Remove(index);
            }
            else if (this.recycled.Count > 0)
            {
                element = this.recycled.Pop();
            }
            else
            {
                element = this.CreateTokenPresenter();
                base.InternalChildren.Add(element);
            }
            return element;
        }

        public UIElement GetContainer(int editableIndex) => 
            this.measuredItems.ContainsKey(editableIndex) ? this.measuredItems[editableIndex] : null;

        internal IList<UIElement> GetContainers() => 
            (this.measuredItems.Values != null) ? this.measuredItems.Values.ToList<UIElement>() : new List<UIElement>();

        internal int GetEditableIndex(TokenEditorPresenter container)
        {
            if (container == null)
            {
                return -1;
            }
            if (container.Equals(this.NewTokenPresenter))
            {
                return this.Items.Count;
            }
            if (!this.measuredItems.ContainsValue(container))
            {
                return -1;
            }
            Func<KeyValuePair<int, UIElement>, int> selector = <>c.<>9__205_1;
            if (<>c.<>9__205_1 == null)
            {
                Func<KeyValuePair<int, UIElement>, int> local1 = <>c.<>9__205_1;
                selector = <>c.<>9__205_1 = x => x.Key;
            }
            return (from x in this.measuredItems
                where x.Value.Equals(container)
                select x).Select<KeyValuePair<int, UIElement>, int>(selector).FirstOrDefault<int>();
        }

        internal int GetFirstTokenInLine(int lineIndex)
        {
            Func<TokenEditorLineInfo, int> evaluator = <>c.<>9__228_0;
            if (<>c.<>9__228_0 == null)
            {
                Func<TokenEditorLineInfo, int> local1 = <>c.<>9__228_0;
                evaluator = <>c.<>9__228_0 = x => (x.Tokens.Count > 0) ? x.Tokens[0].VisibleIndex : -1;
            }
            return this.MeasureStrategy.GetLineByAbsolutIndex(lineIndex).Return<TokenEditorLineInfo, int>(evaluator, (<>c.<>9__228_1 ??= () => -1));
        }

        internal int GetFirstTokenInLineByTokenIndex(int index, bool up)
        {
            Func<TokenEditorLineInfo, int> evaluator = <>c.<>9__227_0;
            if (<>c.<>9__227_0 == null)
            {
                Func<TokenEditorLineInfo, int> local1 = <>c.<>9__227_0;
                evaluator = <>c.<>9__227_0 = x => x.Index;
            }
            int num = this.MeasureStrategy.GetContainedLine(index).Return<TokenEditorLineInfo, int>(evaluator, <>c.<>9__227_1 ??= () => -1);
            if (num > -1)
            {
                TokenEditorLineInfo lineByAbsolutIndex = this.MeasureStrategy.GetLineByAbsolutIndex(num + (up ? -1 : 1));
                if (lineByAbsolutIndex != null)
                {
                    return ((lineByAbsolutIndex.Tokens.Count > 0) ? lineByAbsolutIndex.Tokens[0].VisibleIndex : -1);
                }
            }
            return -1;
        }

        internal List<int> GetIndexesInLine(int index)
        {
            List<int> list = new List<int>();
            TokenEditorLineInfo containedLine = this.MeasureStrategy.GetContainedLine(index);
            if (containedLine != null)
            {
                foreach (TokenInfo info2 in containedLine.Tokens)
                {
                    list.Add(info2.VisibleIndex);
                }
            }
            return list;
        }

        private List<int> GetIndexesInLine(int index, bool after)
        {
            List<int> list = new List<int>();
            TokenEditorLineInfo containedLine = this.MeasureStrategy.GetContainedLine(index);
            if (containedLine != null)
            {
                foreach (TokenInfo info2 in containedLine.Tokens)
                {
                    if ((info2.VisibleIndex >= index) & after)
                    {
                        list.Add(info2.VisibleIndex);
                        continue;
                    }
                    if ((info2.VisibleIndex <= index) && !after)
                    {
                        list.Add(info2.VisibleIndex);
                    }
                }
            }
            return list;
        }

        internal List<int> GetIndexesInLineAfterToken(int index) => 
            this.GetIndexesInLine(index, true);

        internal List<int> GetIndexesInLineBeforeToken(int index) => 
            this.GetIndexesInLine(index, false);

        public List<UIElement> GetInplaceEditorContainers() => 
            this.measuredItems.Values.ToList<UIElement>();

        private TokenEditorLineInfo GetLine(int lineIndex) => 
            this.MeasureStrategy.GetLineByAbsolutIndex(lineIndex);

        private double GetLineHeight(int lineIndex)
        {
            Func<TokenEditorLineInfo, double> evaluator = <>c.<>9__158_0;
            if (<>c.<>9__158_0 == null)
            {
                Func<TokenEditorLineInfo, double> local1 = <>c.<>9__158_0;
                evaluator = <>c.<>9__158_0 = x => x.Height;
            }
            return this.GetLine(lineIndex).Return<TokenEditorLineInfo, double>(evaluator, () => this.MeasureStrategy.CalcMaxLineHeight());
        }

        internal TokenEditorLineInfo GetLineRelativeToken(int index, bool up)
        {
            TokenEditorLineInfo containedLine = this.MeasureStrategy.GetContainedLine(index);
            int num = up ? -1 : 1;
            if (containedLine == null)
            {
                return null;
            }
            int lineIndex = Math.Min(Math.Max(this.MinVisibleIndex, this.MeasureStrategy.GetIndexOfLine(containedLine) + num), this.MaxVisibleIndex);
            return this.MeasureStrategy.GetLine(lineIndex);
        }

        private double GetMaxHorizontalOffset() => 
            Math.Max((double) 0.0, (double) (this.ExtentWidth - this.ViewportWidth));

        private int GetMaxIndex() => 
            (this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? this.MeasureStrategy.MaxVisibleIndex : this.MaxLineIndex;

        private double GetMaxVerticalOffset() => 
            Math.Max((double) 0.0, (double) (this.ExtentHeight - this.ViewportHeight));

        internal TokenEditorPresenter GetTokenByEditableIndex(int index)
        {
            UIElement element = null;
            this.measuredItems.TryGetValue(index, out element);
            return (element as TokenEditorPresenter);
        }

        internal TokenEditorPresenter GetTokenByVisibleIndex(int visibleIndex) => 
            this.GetTokenByEditableIndex(this.MeasureStrategy.ConvertToEditableIndex(visibleIndex));

        internal double GetTokenMaxWidth() => 
            this.Owner.TokenMaxWidth;

        private double GetTokenSize(int visibleIndex) => 
            (this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? this.GetTokenWidth(visibleIndex) : this.GetLineHeight(visibleIndex);

        private double GetTokenWidth(int visibleIndex)
        {
            if (this.IsNewTokenIndex(visibleIndex) && !this.CanShowNewToken)
            {
                return 0.0;
            }
            Func<TokenEditorPresenter, double> evaluator = <>c.<>9__160_0;
            if (<>c.<>9__160_0 == null)
            {
                Func<TokenEditorPresenter, double> local1 = <>c.<>9__160_0;
                evaluator = <>c.<>9__160_0 = x => x.DesiredSize.Width;
            }
            return this.GetTokenByVisibleIndex(visibleIndex).Return<TokenEditorPresenter, double>(evaluator, (<>c.<>9__160_1 ??= () => 50.0));
        }

        internal int GetVisibleIndex(TokenEditorPresenter container) => 
            this.MeasureStrategy.ConvertToVisibleIndex(this.GetEditableIndex(container));

        internal Dictionary<int, UIElement> GetVisibleTokens() => 
            this.measuredItems;

        private bool GreaterOrClose(double p1, double p2) => 
            (p1 > p2) || this.AreClose(p1, p2);

        public double IndexToOffset(int index)
        {
            double num = 0.0;
            for (int i = 0; i < index; i++)
            {
                num += this.GetTokenSize(i);
            }
            return num;
        }

        private void InvalidateScrollData(Size viewport, Size extent)
        {
            this.scrollData.ViewportSize = viewport;
            this.scrollData.ExtentSize = extent;
            Action<ScrollViewer> action = <>c.<>9__191_0;
            if (<>c.<>9__191_0 == null)
            {
                Action<ScrollViewer> local1 = <>c.<>9__191_0;
                action = <>c.<>9__191_0 = x => x.InvalidateScrollInfo();
            }
            this.ScrollOwner.Do<ScrollViewer>(action);
        }

        private void InvalidateScrollInfo()
        {
            Action<ScrollViewer> action = <>c.<>9__192_0;
            if (<>c.<>9__192_0 == null)
            {
                Action<ScrollViewer> local1 = <>c.<>9__192_0;
                action = <>c.<>9__192_0 = x => x.InvalidateScrollInfo();
            }
            this.ScrollOwner.Do<ScrollViewer>(action);
        }

        private bool IsChildRectIntoOwner(TokenEditorPresenter token, Rect ownerRect)
        {
            Rect relativeElementRect = LayoutHelper.GetRelativeElementRect(token, this);
            return ownerRect.Contains(relativeElementRect);
        }

        private bool IsEditorActivated(UIElement container) => 
            ((TokenEditorPresenter) container).IsEditorActivated;

        internal bool IsEndLineVisible()
        {
            TokenEditorLineInfo containedLine = this.MeasureStrategy.GetContainedLine(this.MeasureStrategy.MaxVisibleIndex);
            if (containedLine == null)
            {
                return false;
            }
            Rect relativeElementRect = LayoutHelper.GetRelativeElementRect(this.GetTokenByVisibleIndex(containedLine.Tokens[0].VisibleIndex), this);
            return this.ContainsBounds(relativeElementRect);
        }

        internal bool IsFirstLineVisible()
        {
            Func<TokenEditorLineInfo, int> evaluator = <>c.<>9__222_0;
            if (<>c.<>9__222_0 == null)
            {
                Func<TokenEditorLineInfo, int> local1 = <>c.<>9__222_0;
                evaluator = <>c.<>9__222_0 = x => x.Index;
            }
            return (this.MeasureStrategy.GetLineByAbsolutIndex(this.OffsetToIndex(this.VerticalOffset)).Return<TokenEditorLineInfo, int>(evaluator, (<>c.<>9__222_1 ??= () => -1)) == 0);
        }

        internal bool IsFirstToken(TokenEditorPresenter token)
        {
            int index = this.MeasureStrategy.ConvertToVisibleIndex(this.GetEditableIndex(token));
            return this.MeasureStrategy.IsMinVisibleIndex(index);
        }

        internal bool IsLastToken(TokenEditorPresenter token)
        {
            int index = this.MeasureStrategy.ConvertToVisibleIndex(this.GetEditableIndex(token));
            return this.MeasureStrategy.IsMaxVisibleIndex(index);
        }

        internal bool IsNewTokenIndex(int visibleIndex) => 
            this.MeasureStrategy.ConvertToEditableIndex(visibleIndex) == this.GetEditableIndex(this.NewTokenPresenter);

        private bool IsTokenActivated(int index)
        {
            UIElement container = this.measuredItems.ContainsKey(index) ? this.measuredItems[index] : null;
            return this.IsEditorActivated(container);
        }

        internal bool IsTokenInEndLine(TokenEditorPresenter token)
        {
            int index = this.MeasureStrategy.ConvertToVisibleIndex(this.GetEditableIndex(token));
            if (index <= -1)
            {
                return false;
            }
            TokenEditorLineInfo lineByAbsolutIndex = this.MeasureStrategy.GetLineByAbsolutIndex(this.OffsetToIndex(this.VerticalOffset + this.ViewportHeight));
            return ((lineByAbsolutIndex != null) && (this.IsEndLineVisible() && ((from x in lineByAbsolutIndex.Tokens
                where x.VisibleIndex == index
                select x).FirstOrDefault<TokenInfo>() != null)));
        }

        internal bool IsTokenInFirstLine(TokenEditorPresenter token)
        {
            int index = this.MeasureStrategy.ConvertToVisibleIndex(this.GetEditableIndex(token));
            if (index <= -1)
            {
                return false;
            }
            TokenEditorLineInfo lineByAbsolutIndex = this.MeasureStrategy.GetLineByAbsolutIndex(this.OffsetToIndex(this.VerticalOffset));
            return ((lineByAbsolutIndex != null) && (this.IsFirstLineVisible() && ((from x in lineByAbsolutIndex.Tokens
                where x.VisibleIndex == index
                select x).FirstOrDefault<TokenInfo>() != null)));
        }

        private bool IsValueEquals(TokenItemData data, CustomItem item)
        {
            object editValue = item.EditValue;
            return (((editValue == null) || !(editValue is LookUpEditableItem)) ? ((item.EditValue == data.Value) && (item.DisplayText == data.DisplayText)) : ((((LookUpEditableItem) editValue).EditValue == data.Value) && (item.DisplayText == data.DisplayText)));
        }

        private bool LessOrClose(double p1, double p2) => 
            (p1 < p2) || this.AreClose(p1, p2);

        public void LineDown()
        {
            this.SetVerticalOffset(this.VerticalOffset + this.lineStep);
        }

        public void LineLeft()
        {
            this.SetHorizontalOffset(this.HorizontalOffset - 50.0);
        }

        public void LineRight()
        {
            this.SetHorizontalOffset(this.HorizontalOffset + 50.0);
        }

        public void LineUp()
        {
            this.SetVerticalOffset(this.VerticalOffset - this.lineStep);
        }

        public Rect MakeVisible(Visual visual, Rect rectangle)
        {
            if (!this.MakeVisibleLocker.IsLocked)
            {
                this.MakeVisibleLocker.LockOnce();
                TokenEditorPresenter element = LayoutHelper.FindParentObject<TokenEditorPresenter>(visual);
                if (element != null)
                {
                    Rect relativeElementRect = LayoutHelper.GetRelativeElementRect(element, this);
                    return ((this.Orientation != System.Windows.Controls.Orientation.Horizontal) ? this.MakeVisibleVertical(relativeElementRect) : this.MakeVisibleHorizontal(relativeElementRect));
                }
            }
            return Rect.Empty;
        }

        private Rect MakeVisibleHorizontal(Rect rect)
        {
            if (rect.X < 0.0)
            {
                this.SetHorizontalOffset((this.HorizontalOffset - Math.Abs(rect.X)) + 1E-09);
            }
            else if (rect.Right > this.Owner.ActualWidth)
            {
                this.SetHorizontalOffset(this.HorizontalOffset + (rect.Right - this.Owner.ActualWidth));
                base.UpdateLayout();
                base.InvalidateMeasure();
            }
            return rect;
        }

        private Rect MakeVisibleVertical(Rect rect)
        {
            if (rect.Y < 0.0)
            {
                this.SetVerticalOffset((this.VerticalOffset - Math.Abs(rect.Y)) + 1E-09);
            }
            else if (rect.Bottom > this.Owner.ActualHeight)
            {
                this.SetVerticalOffset(this.VerticalOffset + (rect.Bottom - this.Owner.ActualHeight));
                base.UpdateLayout();
                base.InvalidateMeasure();
            }
            return rect;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size size;
            if ((this.Owner == null) || (this.MeasureStrategy == null))
            {
                return new Size(0.0, 0.0);
            }
            if (this.NewTokenPresenter == null)
            {
                this.CreateNewTokenEditor();
            }
            try
            {
                this.StartMeasure(constraint);
                size = this.MeasureStrategy.Measure(constraint);
            }
            finally
            {
                if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
                {
                    this.TryCorrectHorizontalOffset();
                }
                else
                {
                    this.TryCorrectVerticalOffset();
                }
                this.EndMeasure();
            }
            return size;
        }

        public int OffsetToIndex(double offset)
        {
            int maxIndex = this.GetMaxIndex();
            for (int i = 0; i <= maxIndex; i++)
            {
                offset -= this.GetTokenSize(i);
                if (offset <= 0.0)
                {
                    return i;
                }
            }
            return maxIndex;
        }

        internal void OnActiveTokenEditValueChanged(TokenEditorPresenter token)
        {
            this.BringIntoView(token);
            if (this.MeasureStrategy != null)
            {
                this.MeasureStrategy.DestroyLines();
            }
        }

        private void OnDisplayItemsChanged(List<CustomItem> oldValue, List<CustomItem> newValue)
        {
            this.UpdateItems(oldValue, newValue);
            base.InvalidateMeasure();
        }

        private void OnItemTemplateChanged(ControlTemplate newValue)
        {
            base.InvalidateMeasure();
        }

        private void OnShowTokenButtonsChanged()
        {
            base.InvalidateMeasure();
        }

        public void OnThumbDragDelta(DragDeltaEventArgs e)
        {
            if (this.Orientation == System.Windows.Controls.Orientation.Vertical)
            {
                this.TryHandlesVerticalChange(e);
            }
        }

        internal void OnThumbMouseMove(MouseEventArgs e)
        {
            if (this.Owner != null)
            {
                ScrollBar scrollBar = ((IScrollBarThumbDragDeltaListener) this.Owner).ScrollBar;
                if (scrollBar != null)
                {
                    Track relativeTo = scrollBar.Track;
                    Thumb thumb = relativeTo.Thumb;
                    if ((this.Orientation == System.Windows.Controls.Orientation.Horizontal) && (e.MouseDevice.LeftButton == MouseButtonState.Pressed))
                    {
                        Point position = e.GetPosition(relativeTo);
                        Point point2 = e.GetPosition(thumb);
                        double thumbLength = 0.0;
                        double num2 = this.CalcLocation(relativeTo, this.ExtentWidth - this.ViewportWidth, this.ViewportWidth, this.HorizontalOffset, out thumbLength);
                        if (position.X < num2)
                        {
                            point2.X += (num2 - position.X) / 5.0;
                        }
                        else if (position.X > (num2 + thumbLength))
                        {
                            point2.X -= (position.X - (num2 + thumbLength)) / 5.0;
                        }
                        typeof(Thumb).GetField("_originThumbPoint", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(thumb, point2);
                    }
                }
            }
        }

        private void OnTokenContainerTemplateChanged()
        {
            for (int i = 1; i < base.InternalChildren.Count; i++)
            {
                TokenEditorPresenter presenter = base.InternalChildren[i] as TokenEditorPresenter;
                presenter.BorderTemplate = this.TokenContainerTemplate;
            }
            base.InvalidateMeasure();
        }

        private void OnTokenEditorPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.prevMovePoint = e.GetPosition(this);
        }

        private void OnTokenEditorPreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(this);
            this.isLeftScrollDirection = position.X < this.prevMovePoint.X;
            this.isUpScrollDirection = position.Y < this.prevMovePoint.Y;
            this.prevMovePoint = position;
        }

        public void PageDown()
        {
            this.SetVerticalOffset(this.VerticalOffset + this.ViewportHeight);
        }

        public void PageLeft()
        {
            this.SetHorizontalOffset(this.HorizontalOffset - this.ViewportWidth);
        }

        public void PageRight()
        {
            this.SetHorizontalOffset(this.HorizontalOffset + this.ViewportWidth);
        }

        public void PageUp()
        {
            this.SetVerticalOffset(this.VerticalOffset - this.ViewportHeight);
        }

        public UIElement PrepareContainer(int editableIndex)
        {
            CustomItem item = this.Items[editableIndex];
            UIElement container = this.GenerateContainer(editableIndex, item);
            this.PrepareContainer(container, this.Items[editableIndex]);
            this.measuredItems.Add(editableIndex, container);
            return container;
        }

        private void PrepareContainer(UIElement container, CustomItem item)
        {
            TokenEditorPresenter presenter = (TokenEditorPresenter) container;
            if (!presenter.IsEditorActivated)
            {
                presenter.Item = item;
                presenter.UpdateEditorEditValue();
            }
        }

        internal void RemoveFromMeasure(int index)
        {
            int key = this.MeasureStrategy.ConvertToEditableIndex(index);
            if (this.measuredItems.ContainsKey(key))
            {
                this.recycled.Push(this.measuredItems[key]);
                this.measuredItems.Remove(key);
            }
        }

        public void ScrollDown()
        {
            this.ScrollVertical(false);
        }

        public void ScrollLeft(int index)
        {
            if (!this.BringIntoViewByIndex(index))
            {
                this.SetHorizontalOffset(this.IndexToOffset(index));
            }
        }

        public void ScrollRight(int index)
        {
            if (!this.BringIntoViewByIndex(index))
            {
                this.ScrollToHorizontalEnd();
            }
        }

        private void ScrollToEnd()
        {
            if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
            {
                this.ScrollToHorizontalEnd();
            }
            else
            {
                this.ScrollToVerticalEnd();
            }
        }

        public void ScrollToHorizontalEnd()
        {
            this.ShouldEnsureHorizontalOffset = true;
            this.ShouldScrollToEnd = true;
            base.InvalidateMeasure();
        }

        public void ScrollToNewToken()
        {
            if (this.ShowNewTokenFromEnd)
            {
                this.ScrollToEnd();
            }
            else
            {
                this.ScrollToStart();
            }
        }

        private void ScrollToStart()
        {
            if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
            {
                this.SetHorizontalOffset(0.0);
            }
            else
            {
                this.SetVerticalOffset(0.0);
            }
        }

        internal void ScrollToVerticalEnd()
        {
            this.ShouldScrollToEnd = true;
            base.InvalidateMeasure();
        }

        internal void ScrollToVerticalStart()
        {
            this.SetVerticalOffset(0.0);
        }

        public void ScrollUp()
        {
            this.ScrollVertical(true);
        }

        private void ScrollVertical(bool up)
        {
            int num = up ? -1 : 1;
            TokenEditorLineInfo lineByAbsolutIndex = this.MeasureStrategy.GetLineByAbsolutIndex(this.OffsetToIndex(this.VerticalOffset) + num);
            if ((lineByAbsolutIndex != null) && (lineByAbsolutIndex.Tokens.Count > 0))
            {
                this.BringIntoViewByIndex(lineByAbsolutIndex.Tokens[0].VisibleIndex);
            }
        }

        public void SetHorizontalOffset(double offset)
        {
            this.PreviousHorizontalOffset = this.HorizontalOffset;
            this.SetHorizontalOffsetInternal(offset);
            base.InvalidateMeasure();
        }

        private void SetHorizontalOffsetInternal(double offset)
        {
            this.scrollData.HorizontalOffset = this.EnsureHorizontalOffset(offset);
            this.InvalidateScrollInfo();
        }

        public void SetVerticalOffset(double offset)
        {
            this.MeasureStrategy.InvalidateLines();
            this.PreviousVerticalOffset = this.VerticalOffset;
            this.ShouldCalculateFirstVisibleIndex = true;
            this.SetVerticalOffsetInternal(offset);
            base.InvalidateMeasure();
        }

        private void SetVerticalOffsetInternal(double offset)
        {
            this.scrollData.VerticalOffset = this.EnsureVerticalOffset(offset);
            this.InvalidateScrollInfo();
        }

        internal bool ShouldScroll(int index)
        {
            TokenEditorPresenter tokenByEditableIndex = this.GetTokenByEditableIndex(this.MeasureStrategy.ConvertToEditableIndex(index));
            if (tokenByEditableIndex == null)
            {
                return true;
            }
            Rect rect2 = new Rect(0.0, 0.0, base.ActualWidth, base.ActualHeight);
            return !rect2.Contains(LayoutHelper.GetRelativeElementRect(tokenByEditableIndex, this));
        }

        private void StartMeasure(Size constraint)
        {
            if (this.ShouldScrollToEnd)
            {
                this.ClearMeasuredItems();
                this.ShouldScrollToEnd = false;
                double offset = 0.0;
                int index = this.MeasureStrategy.MeasureFromEnd(constraint, out offset);
                double num3 = this.IndexToOffset(index) + offset;
                if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
                {
                    this.SetHorizontalOffset(num3);
                }
                else
                {
                    this.scrollData.ExtentSize = this.CalcExtentSize();
                    this.scrollData.VerticalOffset = this.EnsureVerticalOffset(this.GetMaxVerticalOffset());
                    this.PreviousVerticalOffset = this.scrollData.VerticalOffset;
                    this.InvalidateScrollInfo();
                    this.MeasureStrategy.RenumerateLines();
                    this.MeasureStrategy.DestroyLines();
                }
            }
            this.CalcStartMeasureIndex();
            this.ClearMeasuredItems();
        }

        void IScrollInfo.MouseWheelDown()
        {
            this.LineDown();
        }

        void IScrollInfo.MouseWheelLeft()
        {
            this.LineLeft();
        }

        void IScrollInfo.MouseWheelRight()
        {
            this.LineRight();
        }

        void IScrollInfo.MouseWheelUp()
        {
            this.LineUp();
        }

        private void TokenEditorPanel_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateMeasureStrategy();
            this.correctOffsetLocker.LockOnce();
            this.MeasureStrategy.DestroyLines();
            this.SetHorizontalOffset(0.0);
            this.SetVerticalOffset(0.0);
            base.InvalidateMeasure();
        }

        private void TryCorrectHorizontalOffset()
        {
            int num = this.OffsetToIndex(this.HorizontalOffset);
            if ((this.correctOffsetLocker.IsLocked || ((this.StartMeasureIndex == num) && (this.IndexToOffset(this.StartMeasureIndex) == this.PreviousTokenStartPosition))) || (Math.Abs((double) (this.PreviousHorizontalOffset - this.HorizontalOffset)) >= this.ViewportWidth))
            {
                this.correctOffsetLocker.Unlock();
            }
            else
            {
                this.correctOffsetLocker.LockOnce();
                this.scrollData.ExtentSize = this.CalcExtentSize();
                this.scrollData.HorizontalOffset = this.EnsureHorizontalOffset(this.IndexToOffset(this.StartMeasureIndex) + (this.HorizontalOffset - this.PreviousTokenStartPosition));
                this.PreviousHorizontalOffset = this.scrollData.HorizontalOffset;
                this.InvalidateScrollInfo();
            }
        }

        private void TryCorrectVerticalOffset()
        {
            TokenEditorLineInfo containedLine = this.MeasureStrategy.GetContainedLine(this.MinVisibleIndex);
            if (this.MeasureStrategy.OffsetTokenIndex > this.Items.Count)
            {
                this.MeasureStrategy.DestroyLines();
                this.SetVerticalOffset(0.0);
            }
            else if (containedLine != null)
            {
                if (containedLine.Index != 0)
                {
                    this.scrollData.ExtentSize = this.CalcExtentSize();
                    this.MeasureStrategy.DestroyLines();
                    double num2 = this.MeasureStrategy.CalcMaxLineHeight();
                    this.scrollData.VerticalOffset = (containedLine.Index >= 0) ? this.EnsureVerticalOffset(this.VerticalOffset - num2) : this.EnsureVerticalOffset(this.VerticalOffset + num2);
                    this.InvalidateScrollInfo();
                }
            }
            else
            {
                TokenEditorLineInfo line = this.MeasureStrategy.GetLine(0);
                if ((line != null) && (line.Index <= 0))
                {
                    this.scrollData.ExtentSize = this.CalcExtentSize();
                    this.scrollData.VerticalOffset = this.EnsureVerticalOffset(this.VerticalOffset + this.MeasureStrategy.CalcMaxLineHeight());
                    this.PreviousVerticalOffset = this.VerticalOffset;
                    this.InvalidateScrollInfo();
                    this.MeasureStrategy.RenumerateLines();
                }
            }
            int num = this.OffsetToIndex(this.VerticalOffset);
            if ((this.correctOffsetLocker.IsLocked || ((this.StartMeasureIndex == num) && (this.IndexToOffset(this.StartMeasureIndex) == this.PreviousTokenStartPosition))) || (Math.Abs((double) (this.PreviousVerticalOffset - this.VerticalOffset)) >= this.ViewportHeight))
            {
                this.correctOffsetLocker.Unlock();
            }
            else
            {
                this.correctOffsetLocker.LockOnce();
                this.scrollData.ExtentSize = this.CalcExtentSize();
                this.scrollData.VerticalOffset = this.EnsureVerticalOffset(this.IndexToOffset(this.StartMeasureIndex) + (this.VerticalOffset - this.PreviousTokenStartPosition));
                this.PreviousVerticalOffset = this.scrollData.VerticalOffset;
                this.InvalidateScrollInfo();
                this.MeasureStrategy.RenumerateLines();
            }
        }

        private void TryHandlesHorizontalChange(DragDeltaEventArgs e)
        {
            if (((e.HorizontalChange < 0.0) && !this.isLeftScrollDirection) || (this.isLeftScrollDirection && (e.HorizontalChange > 0.0)))
            {
                e.Handled = true;
                this.SetHorizontalOffset(this.isLeftScrollDirection ? (this.HorizontalOffset - 1.0) : (this.HorizontalOffset + 1.0));
            }
        }

        private void TryHandlesVerticalChange(DragDeltaEventArgs e)
        {
            if (((e.VerticalChange < 0.0) && !this.isUpScrollDirection) || (this.isUpScrollDirection && (e.VerticalChange > 0.0)))
            {
                e.Handled = true;
            }
        }

        private void UpdateItems(List<CustomItem> oldValue, List<CustomItem> newValue)
        {
            List<CustomItem> list = new List<CustomItem>();
            for (int i = 0; i < newValue.Count; i++)
            {
                CustomItem item = newValue[i];
                object editValue = item.EditValue;
                if (editValue is LookUpEditableItem)
                {
                    editValue = ((LookUpEditableItem) item.EditValue).EditValue;
                }
                if ((editValue != null) || !string.IsNullOrEmpty(item.DisplayText))
                {
                    CustomItem item1 = new CustomItem();
                    item1.EditValue = editValue;
                    item1.DisplayText = item.DisplayText;
                    list.Add(item1);
                }
            }
            if ((((this.Items == null) && (list != null)) || ((this.Items != null) && (list == null))) || (this.Items.Count != list.Count))
            {
                Action<TokenEditorMeasureStrategyBase> action = <>c.<>9__165_0;
                if (<>c.<>9__165_0 == null)
                {
                    Action<TokenEditorMeasureStrategyBase> local1 = <>c.<>9__165_0;
                    action = <>c.<>9__165_0 = x => x.DestroyLines();
                }
                this.MeasureStrategy.Do<TokenEditorMeasureStrategyBase>(action);
            }
            this.Items = list;
        }

        public void UpdateMeasureStrategy()
        {
            if (this.Owner != null)
            {
                this.MeasureStrategy = this.Owner.EnableTokenWrapping ? (this.Owner.ShowNewTokenFromEnd ? ((TokenEditorMeasureStrategyBase) new TokenEditorWrapLineFromEndMeasureStrategy(this)) : ((TokenEditorMeasureStrategyBase) new TokenEditorWrapLineMeasureStrategy(this))) : (this.Owner.ShowNewTokenFromEnd ? ((TokenEditorMeasureStrategyBase) new TokenEditorLineFromEndMeasureStrategy(this)) : ((TokenEditorMeasureStrategyBase) new TokenEditorLineMeasureStrategy(this)));
            }
            this.ShouldEnsureHorizontalOffset = true;
        }

        private void UpdateNewTokenPresenter()
        {
            if (this.NewTokenPresenter != null)
            {
                this.NewTokenPresenter.Item = new CustomItem();
                this.NewTokenPresenter.NullText = this.Owner.GetNullText();
                this.NewTokenPresenter.UpdateEditorEditValue();
            }
        }

        private void UpdateScrollData(Size size)
        {
            Size viewport = this.CalcViewport(size);
            Size extent = this.CalcExtentSize();
            if (this.ShouldEnsureHorizontalOffset)
            {
                this.ShouldEnsureHorizontalOffset = false;
                this.scrollData.HorizontalOffset = this.EnsureHorizontalOffset(this.HorizontalOffset);
            }
            if (extent.Width <= viewport.Width)
            {
                this.scrollData.HorizontalOffset = 0.0;
            }
            if (extent.Height <= viewport.Height)
            {
                this.scrollData.VerticalOffset = 0.0;
            }
            this.InvalidateScrollData(viewport, extent);
        }

        public List<CustomItem> DisplayItems
        {
            get => 
                (List<CustomItem>) base.GetValue(DisplayItemsProperty);
            set => 
                base.SetValue(DisplayItemsProperty, value);
        }

        public bool EnableTokenWrapping
        {
            get => 
                (bool) base.GetValue(EnableTokenWrappingProperty);
            set => 
                base.SetValue(EnableTokenWrappingProperty, value);
        }

        public ControlTemplate TokenContainerTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(TokenContainerTemplateProperty);
            set => 
                base.SetValue(TokenContainerTemplateProperty, value);
        }

        public ControlTemplate EmptyTokenContainerTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(EmptyTokenContainerTemplateProperty);
            set => 
                base.SetValue(EmptyTokenContainerTemplateProperty, value);
        }

        public int StartMeasureIndex { get; private set; }

        public bool CanShowNewToken =>
            this.Owner.ShowNewToken;

        public int LinesCount
        {
            get
            {
                Func<TokenEditorMeasureStrategyBase, int> evaluator = <>c.<>9__28_0;
                if (<>c.<>9__28_0 == null)
                {
                    Func<TokenEditorMeasureStrategyBase, int> local1 = <>c.<>9__28_0;
                    evaluator = <>c.<>9__28_0 = x => x.LinesCount;
                }
                return this.MeasureStrategy.Return<TokenEditorMeasureStrategyBase, int>(evaluator, (<>c.<>9__28_1 ??= () => 1));
            }
        }

        public int MinVisibleIndex
        {
            get
            {
                Func<TokenEditorMeasureStrategyBase, int> evaluator = <>c.<>9__30_0;
                if (<>c.<>9__30_0 == null)
                {
                    Func<TokenEditorMeasureStrategyBase, int> local1 = <>c.<>9__30_0;
                    evaluator = <>c.<>9__30_0 = x => x.MinVisibleIndex;
                }
                return this.MeasureStrategy.Return<TokenEditorMeasureStrategyBase, int>(evaluator, (<>c.<>9__30_1 ??= () => -1));
            }
        }

        public int MaxVisibleIndex
        {
            get
            {
                Func<TokenEditorMeasureStrategyBase, int> evaluator = <>c.<>9__32_0;
                if (<>c.<>9__32_0 == null)
                {
                    Func<TokenEditorMeasureStrategyBase, int> local1 = <>c.<>9__32_0;
                    evaluator = <>c.<>9__32_0 = x => x.MaxVisibleIndex;
                }
                return this.MeasureStrategy.Return<TokenEditorMeasureStrategyBase, int>(evaluator, (<>c.<>9__32_1 ??= () => -1));
            }
        }

        public double ExtentHeight =>
            this.scrollData.ExtentSize.Height;

        public double ExtentWidth =>
            this.scrollData.ExtentSize.Width;

        public double HorizontalOffset =>
            this.scrollData.HorizontalOffset;

        public double VerticalOffset =>
            this.scrollData.VerticalOffset;

        public double ViewportWidth =>
            this.scrollData.ViewportSize.Width;

        public double ViewportHeight =>
            this.scrollData.ViewportSize.Height;

        public ScrollViewer ScrollOwner
        {
            get => 
                this.scrollData.ScrollOwner;
            set => 
                this.scrollData.ScrollOwner = value;
        }

        public double PreviousHorizontalOffset { get; private set; }

        public double PreviousVerticalOffset { get; private set; }

        public bool ShowNewTokenFromEnd =>
            this.Owner.ShowNewTokenFromEnd;

        public System.Windows.Controls.Orientation Orientation =>
            this.MeasureStrategy.Orientation;

        public bool HasMeasuredTokens =>
            this.measuredItems.Count > 0;

        public bool LockBringIntoView { get; set; }

        internal TokenEditor Owner
        {
            get => 
                this.owner;
            set
            {
                this.owner = value;
                this.owner.PreviewMouseDown += new MouseButtonEventHandler(this.OnTokenEditorPreviewMouseDown);
                this.owner.PreviewMouseMove += new MouseEventHandler(this.OnTokenEditorPreviewMouseMove);
                this.UpdateMeasureStrategy();
            }
        }

        internal TokenEditorPresenter NewTokenPresenter { get; set; }

        internal List<CustomItem> Items { get; set; }

        internal TokenEditorMeasureStrategyBase MeasureStrategy { get; private set; }

        private bool ShouldScrollToEnd { get; set; }

        private bool ShouldCorrectHorizontalOffset { get; set; }

        private bool ShouldEnsureHorizontalOffset { get; set; }

        private bool ShouldCalculateFirstVisibleIndex { get; set; }

        private int MaxLineIndex { get; set; }

        private Locker MakeVisibleLocker { get; set; }

        private double PreviousTokenStartPosition { get; set; }

        bool IScrollInfo.CanHorizontallyScroll { get; set; }

        bool IScrollInfo.CanVerticallyScroll { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TokenEditorPanel.<>c <>9 = new TokenEditorPanel.<>c();
            public static Func<TokenEditorMeasureStrategyBase, int> <>9__28_0;
            public static Func<int> <>9__28_1;
            public static Func<TokenEditorMeasureStrategyBase, int> <>9__30_0;
            public static Func<int> <>9__30_1;
            public static Func<TokenEditorMeasureStrategyBase, int> <>9__32_0;
            public static Func<int> <>9__32_1;
            public static Func<TokenEditorLineInfo, double> <>9__158_0;
            public static Func<TokenEditorPresenter, double> <>9__160_0;
            public static Func<double> <>9__160_1;
            public static Action<TokenEditorMeasureStrategyBase> <>9__165_0;
            public static Func<KeyValuePair<int, UIElement>, KeyValuePair<int, UIElement>> <>9__174_1;
            public static Action<ScrollViewer> <>9__191_0;
            public static Action<ScrollViewer> <>9__192_0;
            public static Func<TokenEditorLineInfo, int> <>9__198_0;
            public static Func<int> <>9__198_1;
            public static Func<KeyValuePair<int, UIElement>, int> <>9__205_1;
            public static Func<int, int> <>9__218_1;
            public static Func<TokenEditorLineInfo, int> <>9__222_0;
            public static Func<int> <>9__222_1;
            public static Func<TokenEditorLineInfo, int> <>9__227_0;
            public static Func<int> <>9__227_1;
            public static Func<TokenEditorLineInfo, int> <>9__228_0;
            public static Func<int> <>9__228_1;

            internal void <.cctor>b__7_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TokenEditorPanel) d).OnDisplayItemsChanged((List<CustomItem>) e.OldValue, (List<CustomItem>) e.NewValue);
            }

            internal void <.cctor>b__7_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TokenEditorPanel) d).OnTokenContainerTemplateChanged();
            }

            internal object <.cctor>b__7_2(DependencyObject d, object e) => 
                ((TokenEditorPanel) d).CoerceTokenContainerTemplate(e);

            internal int <CalcRelativeOffsetAndIndex>b__218_1(int k) => 
                k;

            internal int <CalcVerticalExtentSize>b__198_0(TokenEditorLineInfo x) => 
                (x.Tokens.Count > 0) ? x.Tokens[x.Tokens.Count - 1].VisibleIndex : 0;

            internal int <CalcVerticalExtentSize>b__198_1() => 
                0;

            internal KeyValuePair<int, UIElement> <ClearMeasuredItems>b__174_1(KeyValuePair<int, UIElement> x) => 
                x;

            internal int <get_LinesCount>b__28_0(TokenEditorMeasureStrategyBase x) => 
                x.LinesCount;

            internal int <get_LinesCount>b__28_1() => 
                1;

            internal int <get_MaxVisibleIndex>b__32_0(TokenEditorMeasureStrategyBase x) => 
                x.MaxVisibleIndex;

            internal int <get_MaxVisibleIndex>b__32_1() => 
                -1;

            internal int <get_MinVisibleIndex>b__30_0(TokenEditorMeasureStrategyBase x) => 
                x.MinVisibleIndex;

            internal int <get_MinVisibleIndex>b__30_1() => 
                -1;

            internal int <GetEditableIndex>b__205_1(KeyValuePair<int, UIElement> x) => 
                x.Key;

            internal int <GetFirstTokenInLine>b__228_0(TokenEditorLineInfo x) => 
                (x.Tokens.Count > 0) ? x.Tokens[0].VisibleIndex : -1;

            internal int <GetFirstTokenInLine>b__228_1() => 
                -1;

            internal int <GetFirstTokenInLineByTokenIndex>b__227_0(TokenEditorLineInfo x) => 
                x.Index;

            internal int <GetFirstTokenInLineByTokenIndex>b__227_1() => 
                -1;

            internal double <GetLineHeight>b__158_0(TokenEditorLineInfo x) => 
                x.Height;

            internal double <GetTokenWidth>b__160_0(TokenEditorPresenter x) => 
                x.DesiredSize.Width;

            internal double <GetTokenWidth>b__160_1() => 
                50.0;

            internal void <InvalidateScrollData>b__191_0(ScrollViewer x)
            {
                x.InvalidateScrollInfo();
            }

            internal void <InvalidateScrollInfo>b__192_0(ScrollViewer x)
            {
                x.InvalidateScrollInfo();
            }

            internal int <IsFirstLineVisible>b__222_0(TokenEditorLineInfo x) => 
                x.Index;

            internal int <IsFirstLineVisible>b__222_1() => 
                -1;

            internal void <UpdateItems>b__165_0(TokenEditorMeasureStrategyBase x)
            {
                x.DestroyLines();
            }
        }

        private class ScrollData
        {
            public double HorizontalOffset { get; set; }

            public double VerticalOffset { get; set; }

            public Size ViewportSize { get; set; }

            public Size ExtentSize { get; set; }

            public double WheelSize =>
                50.0;

            public bool CanHorizontallyScroll { get; set; }

            public bool CanVerticallyScroll { get; set; }

            public ScrollViewer ScrollOwner { get; set; }
        }
    }
}

