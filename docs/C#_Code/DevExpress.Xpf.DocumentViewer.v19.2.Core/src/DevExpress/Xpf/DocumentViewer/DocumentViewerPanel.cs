namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public class DocumentViewerPanel : VirtualizingPanel, IScrollInfo
    {
        public static readonly DependencyProperty ShowSingleItemProperty;
        private ScrollData scrollData = new ScrollData();

        static DocumentViewerPanel()
        {
            Type type = typeof(DocumentViewerPanel);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentViewerPanel), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            ShowSingleItemProperty = DependencyPropertyRegistrator.Register<DocumentViewerPanel, bool>(System.Linq.Expressions.Expression.Lambda<Func<DocumentViewerPanel, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentViewerPanel.get_ShowSingleItem)), parameters), false, (d, oldValue, newValue) => d.OnShowSingleItemChanged(newValue));
        }

        public DocumentViewerPanel()
        {
            this.IndexCalculator = this.CreateIndexCalculator();
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double num = this.CalcStartPosition(this.ShowSingleItem);
            for (int i = 0; i < this.InternalChildren.Count; i++)
            {
                double y = num;
                UIElement element = this.InternalChildren[i];
                Size desiredSize = element.DesiredSize;
                Rect finalRect = new Rect(new Point(this.CalcPageHorizontalOffset(desiredSize.Width), y), desiredSize);
                num = y + desiredSize.Height;
                element.Arrange(finalRect);
            }
            return base.ArrangeOverride(finalSize);
        }

        private Size CalcExtentSize()
        {
            int count = ItemsControl.GetItemsOwner(this).Items.Count;
            ItemCollection items = ItemsControl.GetItemsOwner(this).Items;
            double num2 = 0.0;
            double num3 = 0.0;
            for (int i = 0; i < count; i++)
            {
                Size renderSize = ((PageWrapper) items[i]).RenderSize;
                this.IndexCalculator.SetItemSize(i, renderSize);
                num2 = Math.Max(renderSize.Width, num2);
                num3 += renderSize.Height;
            }
            if (this.ShowSingleItem)
            {
                num3 = Math.Max(this.ViewportHeight * count, num3);
            }
            return new Size(num2.Round(false), num3.Round(false));
        }

        public double CalcPageHorizontalOffset(double itemWidth)
        {
            double num = Math.Max((double) 0.0, (double) ((this.ViewportWidth - itemWidth) / 2.0));
            if (this.ViewportWidth.LessThan(itemWidth))
            {
                num -= this.HorizontalOffset;
            }
            return num;
        }

        protected Rect CalcRectangleInPage(int pageNumber, Rect rect)
        {
            Rect rect2;
            if (!rect.IsEmpty)
            {
                rect2 = new Rect(rect.X, rect.Y + this.IndexCalculator.IndexToVerticalOffset(pageNumber, false), rect.Width, rect.Height);
            }
            else
            {
                Size realItemSize = this.IndexCalculator.GetRealItemSize(pageNumber);
                rect2 = new Rect(0.0, this.IndexCalculator.IndexToVerticalOffset(pageNumber, false), realItemSize.IsEmpty ? 0.0 : realItemSize.Width, realItemSize.IsEmpty ? 0.0 : realItemSize.Height);
            }
            return rect2;
        }

        internal double CalcStartPosition(bool showSingleItem)
        {
            int index = this.IndexCalculator.VerticalOffsetToIndex(showSingleItem ? this.GetVirtualVerticalOffset() : this.VerticalOffset);
            if (showSingleItem)
            {
                double height = this.IndexCalculator.GetRealItemSize(index).Height;
                if (height.LessThanOrClose(this.ViewportHeight))
                {
                    return ((this.ViewportHeight - height) / 2.0);
                }
                double num4 = this.IndexCalculator.IndexToVerticalOffset(index, false);
                double num5 = (this.GetVirtualVerticalOffset() - num4) / height;
                return (-(height - this.ViewportHeight) * Math.Abs(num5));
            }
            double verticalOffset = this.VerticalOffset;
            for (int i = 0; i < index; i++)
            {
                Size realItemSize = this.IndexCalculator.GetRealItemSize(i);
                verticalOffset -= realItemSize.Height;
            }
            return -verticalOffset;
        }

        private void CalcVisibleItemsRange(double viewportHeight, out int firstIndex, out int lastIndex)
        {
            if (this.ShowSingleItem)
            {
                firstIndex = this.IndexCalculator.VerticalOffsetToIndex(this.GetVirtualVerticalOffset());
                lastIndex = firstIndex;
            }
            else
            {
                int count = ItemsControl.GetItemsOwner(this).Items.Count;
                if (count == 0)
                {
                    firstIndex = 0;
                    lastIndex = 0;
                }
                else
                {
                    int index = this.IndexCalculator.VerticalOffsetToIndex(this.VerticalOffset);
                    viewportHeight += this.VerticalOffset - this.IndexCalculator.IndexToVerticalOffset(index, this.ShowSingleItem);
                    firstIndex = (index < 0) ? 0 : index;
                    lastIndex = count - 1;
                    for (int i = firstIndex; i < lastIndex; i++)
                    {
                        if (viewportHeight.LessThanOrClose(0.0))
                        {
                            lastIndex = i;
                            return;
                        }
                        viewportHeight -= this.IndexCalculator.GetRealItemSize(i).Height;
                    }
                }
            }
        }

        private void CenterRect(Rect rectangle)
        {
            if (this.ShowSingleItem)
            {
                this.CenterRectSingleItemMode(rectangle);
            }
            else if (!this.IsRectangleVisible(rectangle))
            {
                if (rectangle.Height.GreaterThanOrClose(this.ViewportHeight))
                {
                    this.SetVerticalOffset(rectangle.Top);
                }
                else
                {
                    double num = (this.ViewportHeight - rectangle.Height) / 2.0;
                    this.SetVerticalOffset(rectangle.Top - num);
                }
                if (rectangle.Width.GreaterThanOrClose(this.ViewportWidth))
                {
                    this.SetHorizontalOffset(rectangle.Left);
                }
                else
                {
                    double num2 = (this.ViewportWidth - rectangle.Width) / 2.0;
                    this.SetHorizontalOffset(rectangle.Left - num2);
                }
            }
        }

        private void CenterRectSingleItemMode(Rect rectangle)
        {
            if (!this.IsRectangleVisible(rectangle))
            {
                int index = this.IndexCalculator.VerticalOffsetToIndex(rectangle.Top);
                if (this.IndexCalculator.GetRealItemSize(index).Height.LessThanOrClose(this.ViewportHeight))
                {
                    this.SetVerticalOffset(this.IndexCalculator.IndexToVerticalOffset(index, true));
                }
                else
                {
                    double num2 = this.IndexCalculator.IndexToVerticalOffset(index, false);
                    double relativeOffset = (rectangle.Top - num2) / this.IndexCalculator.GetRealItemSize(index).Height;
                    double offset = this.IndexCalculator.IndexToVerticalOffset(index, true) + this.RelativeOffsetToRealOffset(relativeOffset, this.IndexCalculator.GetRealItemSize(index).Height, true);
                    this.SetVerticalOffset(offset);
                    this.SetHorizontalOffset(rectangle.Left);
                }
            }
        }

        private void CleanUpItems(int startIndex, int endIndex)
        {
            IItemContainerGenerator itemContainerGenerator = base.ItemContainerGenerator;
            for (int i = this.InternalChildren.Count - 1; i >= 0; i--)
            {
                GeneratorPosition position = new GeneratorPosition(i, 0);
                int num3 = itemContainerGenerator.IndexFromGeneratorPosition(position);
                if ((num3 < startIndex) || (num3 > endIndex))
                {
                    itemContainerGenerator.Remove(position, 1);
                    base.RemoveInternalChildRange(i, 1);
                }
            }
        }

        protected virtual DevExpress.Xpf.DocumentViewer.IndexCalculator CreateIndexCalculator() => 
            new DevExpress.Xpf.DocumentViewer.IndexCalculator(this);

        private void FixSinglePageOffset(bool showSingleItem)
        {
            int index = this.IndexCalculator.VerticalOffsetToIndex(!showSingleItem ? this.GetVirtualVerticalOffset() : this.VerticalOffset);
            double pageRelativeVerticalOffset = this.GetPageRelativeVerticalOffset(!showSingleItem);
            this.ActualShowSingleItem = showSingleItem;
            double offset = this.IndexCalculator.IndexToVerticalOffset(index, showSingleItem) + this.RelativeOffsetToRealOffset(pageRelativeVerticalOffset, this.IndexCalculator.GetRealItemSize(index).Height, showSingleItem);
            this.SetVerticalOffset(offset);
        }

        public double GetPageRelativeVerticalOffset(bool showSingleItem)
        {
            int index = this.IndexCalculator.VerticalOffsetToIndex(showSingleItem ? this.GetVirtualVerticalOffset() : this.VerticalOffset);
            return ((!showSingleItem || !this.IndexCalculator.GetRealItemSize(index).Height.LessThanOrClose(this.ViewportHeight)) ? (-this.CalcStartPosition(showSingleItem) / this.IndexCalculator.GetRealItemSize(index).Height) : 0.0);
        }

        private double GetSinglePageModeVerticalOffset(double offset)
        {
            int index = this.IndexCalculator.VerticalOffsetToIndex(offset);
            double num2 = this.IndexCalculator.IndexToVerticalOffset(index, false);
            double relativeOffset = (offset - num2) / this.IndexCalculator.GetRealItemSize(index).Height;
            return (this.IndexCalculator.IndexToVerticalOffset(index, true) + this.RelativeOffsetToRealOffset(relativeOffset, this.IndexCalculator.GetRealItemSize(index).Height, true));
        }

        private double GetVirtualOffset(double offset)
        {
            double number = this.ExtentHeight / (this.ExtentHeight - this.ViewportHeight);
            return (!number.IsNotNumber() ? Math.Min((double) (this.ExtentHeight - 1.0), (double) (offset * number)) : (this.ExtentHeight - 1.0));
        }

        public double GetVirtualVerticalOffset() => 
            this.GetVirtualOffset(this.VerticalOffset);

        public void InvalidatePanel()
        {
            base.InvalidateMeasure();
        }

        private void InvalidateScrollInfo(Size viewportSize, Size extentSize)
        {
            if ((this.scrollData.ViewportSize != viewportSize) || (this.scrollData.ExtentSize != extentSize))
            {
                this.scrollData.ViewportSize = viewportSize;
                this.scrollData.ExtentSize = extentSize;
                this.scrollData.VerticalLineSize = extentSize.Height / (ItemsControl.GetItemsOwner(this).Items.Count * 10.0);
                this.scrollData.HorizontalLineSize = extentSize.Width / 50.0;
                this.scrollData.HorizontalOffset = Math.Max(0.0, Math.Min(this.ExtentWidth - this.ViewportWidth, this.HorizontalOffset));
                this.scrollData.VerticalOffset = Math.Max(0.0, Math.Min(this.ExtentHeight - this.ViewportHeight, this.VerticalOffset));
                Action<ScrollViewer> action = <>c.<>9__85_0;
                if (<>c.<>9__85_0 == null)
                {
                    Action<ScrollViewer> local1 = <>c.<>9__85_0;
                    action = <>c.<>9__85_0 = x => x.InvalidateScrollInfo();
                }
                this.ScrollOwner.Do<ScrollViewer>(action);
                base.InvalidateMeasure();
            }
        }

        private bool IsRectangleHorizontalVisible(Rect rect)
        {
            Rect rect2 = new Rect(this.HorizontalOffset, 0.0, this.ViewportWidth, this.ExtentHeight);
            Rect rect3 = Rect.Union(rect2, rect);
            return rect2.Width.Round(false).AreClose(rect3.Width.Round(false));
        }

        private bool IsRectangleVerticalVisible(Rect rect)
        {
            Rect empty = Rect.Empty;
            if (!this.ShowSingleItem)
            {
                empty = new Rect(this.HorizontalOffset, this.VerticalOffset, this.ViewportWidth, this.ViewportHeight);
            }
            else
            {
                int index = this.IndexCalculator.VerticalOffsetToIndex(this.GetVirtualVerticalOffset());
                double y = this.IndexCalculator.IndexToVerticalOffset(index, false);
                double height = this.IndexCalculator.GetRealItemSize(index).Height;
                if (height.GreaterThan(this.ViewportHeight))
                {
                    y += height * this.GetPageRelativeVerticalOffset(true);
                }
                double num4 = height.LessThanOrClose(this.ViewportHeight) ? height : this.ViewportHeight;
                empty = new Rect(this.HorizontalOffset, y, this.ViewportWidth, num4);
            }
            Rect rect3 = Rect.Union(empty, rect);
            return empty.Height.Round(false).AreClose(rect3.Height.Round(false));
        }

        internal bool IsRectangleVisible(Rect rect) => 
            this.IsRectangleVerticalVisible(rect) && this.IsRectangleHorizontalVisible(rect);

        public virtual bool IsRectangleVisible(int pageNumber, Rect rect) => 
            this.IsRectangleVisible(this.CalcRectangleInPage(pageNumber, rect));

        public void LineDown()
        {
            this.SetVerticalOffset(this.VerticalOffset + this.scrollData.VerticalLineSize);
        }

        public void LineLeft()
        {
            this.SetHorizontalOffset(this.HorizontalOffset - this.scrollData.HorizontalLineSize);
        }

        public void LineRight()
        {
            this.SetHorizontalOffset(this.HorizontalOffset + this.scrollData.HorizontalLineSize);
        }

        public void LineUp()
        {
            this.SetVerticalOffset(this.VerticalOffset - this.scrollData.VerticalLineSize);
        }

        public Rect MakeVisible(Visual visual, Rect rectangle)
        {
            this.ScrollToRect(rectangle, ScrollIntoViewMode.TopLeft);
            return rectangle;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            int num;
            int num2;
            UIElementCollection internalChildren = this.InternalChildren;
            this.UpdateScrollData(base.RenderSize);
            this.CalcVisibleItemsRange(availableSize.Height, out num, out num2);
            IItemContainerGenerator itemContainerGenerator = base.ItemContainerGenerator;
            GeneratorPosition position = itemContainerGenerator.GeneratorPositionFromIndex(num);
            int index = (position.Offset == 0) ? position.Index : (position.Index + 1);
            using (itemContainerGenerator.StartAt(position, GeneratorDirection.Forward, true))
            {
                int num4 = num;
                while (num4 <= num2)
                {
                    bool flag;
                    UIElement child = itemContainerGenerator.GenerateNext(out flag) as UIElement;
                    if (child == null)
                    {
                        break;
                    }
                    if (!flag)
                    {
                        child.InvalidateMeasure();
                    }
                    else
                    {
                        if (index >= internalChildren.Count)
                        {
                            base.AddInternalChild(child);
                        }
                        else
                        {
                            base.InsertInternalChild(index, child);
                        }
                        itemContainerGenerator.PrepareItemContainer(child);
                    }
                    child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    num4++;
                    index++;
                }
            }
            this.CleanUpItems(num, num2);
            return base.MeasureOverride(availableSize);
        }

        public void MouseWheelDown()
        {
            if (this.ShowSingleItem)
            {
                int index = this.IndexCalculator.VerticalOffsetToIndex(this.GetVirtualVerticalOffset());
                if (this.IndexCalculator.GetRealItemSize(index).Height.LessThanOrClose(this.ViewportHeight))
                {
                    this.SetVerticalOffset(this.IndexCalculator.IndexToVerticalOffset(index + 1, this.ShowSingleItem));
                    return;
                }
            }
            this.SetVerticalOffset(this.scrollData.VerticalOffset + this.scrollData.WheelSize);
        }

        public void MouseWheelLeft()
        {
            this.SetHorizontalOffset(this.scrollData.HorizontalOffset - this.scrollData.WheelSize);
        }

        public void MouseWheelRight()
        {
            this.SetHorizontalOffset(this.scrollData.HorizontalOffset + this.scrollData.WheelSize);
        }

        public void MouseWheelUp()
        {
            if (this.ShowSingleItem)
            {
                int index = this.IndexCalculator.VerticalOffsetToIndex(this.GetVirtualVerticalOffset());
                if (this.IndexCalculator.GetRealItemSize(index).Height.LessThanOrClose(this.ViewportHeight))
                {
                    this.SetVerticalOffset(this.IndexCalculator.IndexToVerticalOffset(index - 1, this.ShowSingleItem));
                    return;
                }
            }
            this.SetVerticalOffset(this.VerticalOffset - this.scrollData.WheelSize);
        }

        protected override void OnItemsChanged(object sender, ItemsChangedEventArgs args)
        {
            base.OnItemsChanged(sender, args);
            if (!base.Children.Cast<object>().Any<object>())
            {
                Size size = new Size();
                this.scrollData.ExtentSize = size;
                this.IndexCalculator = this.CreateIndexCalculator();
            }
            this.InvalidatePanel();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            this.InvalidatePanel();
        }

        protected virtual void OnShowSingleItemChanged(bool newValue)
        {
            this.FixSinglePageOffset(newValue);
            this.InvalidatePanel();
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

        private double RelativeOffsetToRealOffset(double relativeOffset, double pageHeight, bool showSingleItem)
        {
            if (!showSingleItem)
            {
                return (relativeOffset * pageHeight);
            }
            if (pageHeight.LessThanOrClose(this.ViewportHeight))
            {
                return 0.0;
            }
            double num = pageHeight - this.ViewportHeight;
            double num2 = this.ExtentHeight / (this.ExtentHeight - this.ViewportHeight);
            double num3 = pageHeight / num2;
            if (relativeOffset.GreaterThanOrClose(num / pageHeight))
            {
                return (num3 - 1.0);
            }
            double num4 = pageHeight / num;
            return (num3 * (relativeOffset * num4));
        }

        public virtual void ScrollIntoView(int pageNumber, Rect rect)
        {
            this.ScrollIntoView(pageNumber, rect, ScrollIntoViewMode.TopLeft);
        }

        public virtual void ScrollIntoView(int pageNumber, Rect rect, ScrollIntoViewMode mode)
        {
            this.ScrollToRect(this.CalcRectangleInPage(pageNumber, rect), mode);
        }

        private void ScrollToEdge(Rect rectangle)
        {
            if (this.ShowSingleItem)
            {
                this.ScrollToEdgeSingleItemMode(rectangle);
            }
            else
            {
                if (!this.IsRectangleVerticalVisible(rectangle))
                {
                    if (rectangle.Top.LessThanOrClose(this.VerticalOffset + 10.0))
                    {
                        this.SetVerticalOffset(rectangle.Top - 10.0);
                    }
                    if (rectangle.Bottom.GreaterThanOrClose((this.VerticalOffset + this.ViewportHeight) - 10.0))
                    {
                        this.SetVerticalOffset((rectangle.Bottom - this.ViewportHeight) + 10.0);
                    }
                }
                if (!this.IsRectangleHorizontalVisible(rectangle))
                {
                    if (rectangle.Right.GreaterThanOrClose((this.HorizontalOffset + this.ViewportWidth) - 10.0))
                    {
                        this.SetHorizontalOffset((rectangle.Right - this.ViewportWidth) + 10.0);
                    }
                    if (rectangle.Left.LessThanOrClose(this.HorizontalOffset + 10.0))
                    {
                        this.SetHorizontalOffset(rectangle.Left - 10.0);
                    }
                }
            }
        }

        private void ScrollToEdgeSingleItemMode(Rect rectangle)
        {
            if (!this.IsRectangleVerticalVisible(rectangle))
            {
                int index = this.IndexCalculator.VerticalOffsetToIndex(this.GetVirtualVerticalOffset());
                double num2 = this.IndexCalculator.IndexToVerticalOffset(index, false);
                double height = this.IndexCalculator.GetRealItemSize(index).Height;
                if (height.GreaterThan(this.ViewportHeight))
                {
                    num2 += height * this.GetPageRelativeVerticalOffset(true);
                }
                if (rectangle.Top.LessThanOrClose(num2 + 10.0))
                {
                    this.SetVerticalOffset(this.GetSinglePageModeVerticalOffset(rectangle.Top - 10.0));
                }
                if (rectangle.Bottom.GreaterThanOrClose((num2 + this.ViewportHeight) - 10.0))
                {
                    int num5 = this.IndexCalculator.VerticalOffsetToIndex(rectangle.Bottom);
                    if (num5 == index)
                    {
                        this.SetVerticalOffset(this.GetSinglePageModeVerticalOffset((rectangle.Bottom - this.ViewportHeight) + 10.0));
                    }
                    else
                    {
                        this.SetVerticalOffset(this.IndexCalculator.IndexToVerticalOffset(num5, true));
                        this.ScrollToEdgeSingleItemMode(rectangle);
                    }
                }
            }
            if (!this.IsRectangleHorizontalVisible(rectangle))
            {
                if (rectangle.Right.GreaterThanOrClose((this.HorizontalOffset + this.ViewportWidth) - 10.0))
                {
                    this.SetHorizontalOffset((rectangle.Right - this.ViewportWidth) + 10.0);
                }
                if (rectangle.Left.LessThanOrClose(this.HorizontalOffset + 10.0))
                {
                    this.SetHorizontalOffset(rectangle.Left - 10.0);
                }
            }
        }

        private void ScrollToRect(Rect rectangle)
        {
            this.SetVerticalOffset(this.ShowSingleItem ? this.GetSinglePageModeVerticalOffset(rectangle.Top) : rectangle.Top);
            this.SetHorizontalOffset(rectangle.Left);
        }

        private void ScrollToRect(Rect rectangle, ScrollIntoViewMode mode)
        {
            switch (mode)
            {
                case ScrollIntoViewMode.Center:
                    this.CenterRect(rectangle);
                    return;

                case ScrollIntoViewMode.TopLeft:
                    this.ScrollToRect(rectangle);
                    return;

                case ScrollIntoViewMode.Edge:
                    this.ScrollToEdge(rectangle);
                    return;
            }
        }

        public void SetHorizontalOffset(double offset)
        {
            offset = Math.Max(0.0, Math.Min(offset, this.ExtentWidth - this.ViewportWidth));
            if (!offset.AreClose(this.HorizontalOffset))
            {
                this.scrollData.HorizontalOffset = offset;
                base.InvalidateMeasure();
                Action<ScrollViewer> action = <>c.<>9__73_0;
                if (<>c.<>9__73_0 == null)
                {
                    Action<ScrollViewer> local1 = <>c.<>9__73_0;
                    action = <>c.<>9__73_0 = x => x.InvalidateScrollInfo();
                }
                this.ScrollOwner.Do<ScrollViewer>(action);
            }
        }

        public void SetVerticalOffset(double offset)
        {
            offset = Math.Max(0.0, Math.Min(offset, this.ExtentHeight - this.ViewportHeight));
            if (!offset.AreClose(this.VerticalOffset))
            {
                this.scrollData.VerticalOffset = offset;
                base.InvalidateMeasure();
                Action<ScrollViewer> action = <>c.<>9__74_0;
                if (<>c.<>9__74_0 == null)
                {
                    Action<ScrollViewer> local1 = <>c.<>9__74_0;
                    action = <>c.<>9__74_0 = x => x.InvalidateScrollInfo();
                }
                this.ScrollOwner.Do<ScrollViewer>(action);
            }
        }

        private void UpdateScrollData(Size viewportSize)
        {
            if (viewportSize.Height.IsNotNumber())
            {
                throw new DocumentViewerInfiniteHeightException(DocumentViewerLocalizer.GetString(DocumentViewerStringId.DocumentViewerInfiniteHeightExceptionMessage));
            }
            Size size = new Size(viewportSize.Width.Round(false), viewportSize.Height.Round(false));
            this.InvalidateScrollInfo(size, this.CalcExtentSize());
        }

        public DevExpress.Xpf.DocumentViewer.IndexCalculator IndexCalculator { get; private set; }

        public UIElementCollection InternalChildren =>
            base.InternalChildren;

        public bool ShowSingleItem
        {
            get => 
                (bool) base.GetValue(ShowSingleItemProperty);
            set => 
                base.SetValue(ShowSingleItemProperty, value);
        }

        public bool ActualShowSingleItem { get; private set; }

        public bool CanHorizontallyScroll
        {
            get => 
                this.scrollData.CanHorizontallyScroll;
            set => 
                this.scrollData.CanHorizontallyScroll = value;
        }

        public bool CanVerticallyScroll
        {
            get => 
                this.scrollData.CanVerticallyScroll;
            set => 
                this.scrollData.CanVerticallyScroll = value;
        }

        public ScrollViewer ScrollOwner
        {
            get => 
                this.scrollData.ScrollOwner;
            set => 
                this.scrollData.ScrollOwner = value;
        }

        public double VerticalOffset =>
            this.scrollData.VerticalOffset;

        public double HorizontalOffset =>
            this.scrollData.HorizontalOffset;

        public double ViewportHeight =>
            this.scrollData.ViewportSize.Height;

        public double ViewportWidth =>
            this.scrollData.ViewportSize.Width;

        public double ExtentHeight =>
            this.scrollData.ExtentSize.Height;

        public double ExtentWidth =>
            this.scrollData.ExtentSize.Width;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentViewerPanel.<>c <>9 = new DocumentViewerPanel.<>c();
            public static Action<ScrollViewer> <>9__73_0;
            public static Action<ScrollViewer> <>9__74_0;
            public static Action<ScrollViewer> <>9__85_0;

            internal void <.cctor>b__8_0(DocumentViewerPanel d, bool oldValue, bool newValue)
            {
                d.OnShowSingleItemChanged(newValue);
            }

            internal void <InvalidateScrollInfo>b__85_0(ScrollViewer x)
            {
                x.InvalidateScrollInfo();
            }

            internal void <SetHorizontalOffset>b__73_0(ScrollViewer x)
            {
                x.InvalidateScrollInfo();
            }

            internal void <SetVerticalOffset>b__74_0(ScrollViewer x)
            {
                x.InvalidateScrollInfo();
            }
        }

        private class ScrollData
        {
            public double HorizontalOffset { get; set; }

            public double VerticalOffset { get; set; }

            public Size ViewportSize { get; set; }

            public Size ExtentSize { get; set; }

            public double VerticalLineSize { get; set; }

            public double HorizontalLineSize { get; set; }

            public double WheelSize =>
                50.0;

            public bool CanHorizontallyScroll { get; set; }

            public bool CanVerticallyScroll { get; set; }

            public ScrollViewer ScrollOwner { get; set; }
        }
    }
}

