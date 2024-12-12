namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;

    public class LoopedPanel : Panel, IScrollInfo, IScrollSnapPointsInfo
    {
        public static readonly DependencyProperty OrientationProperty;
        public static readonly DependencyProperty IsLoopedProperty;
        private Size extentSize;
        private Size viewportSize;
        private double horizontalOffset;
        private double verticalOffset;
        private ScrollViewer scrollOwner;

        public event EventHandler HorizontalSnapPointsChanged;

        public event EventHandler VerticalSnapPointsChanged;

        static LoopedPanel()
        {
            Type ownerType = typeof(LoopedPanel);
            OrientationProperty = DependencyPropertyManager.Register("Orientation", typeof(System.Windows.Controls.Orientation), ownerType, new PropertyMetadata(System.Windows.Controls.Orientation.Vertical));
            IsLoopedProperty = DependencyPropertyManager.Register("IsLooped", typeof(bool), ownerType, new PropertyMetadata(true, (d, e) => ((LoopedPanel) d).LoopedChanged((bool) e.NewValue)));
        }

        public LoopedPanel()
        {
            this.IndexCalculator = this.CreateIndexCalculator();
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.ItemsContainerGenerator == null)
            {
                return finalSize;
            }
            double itemSize = this.GetItemSize(finalSize);
            this.UpdateScrollData(itemSize);
            double logicalViewport = this.GetLogicalViewport();
            double offset = this.GetOffset(logicalViewport);
            double num4 = this.EnsureLogicalOffset(offset, this.GetLogicalExtent(), logicalViewport);
            int index = this.CalcIndex(num4);
            double num6 = this.IndexCalculator.CalcRelativePosition(num4);
            UIElement container = this.ItemsContainerGenerator.GetContainer(index);
            if (container == null)
            {
                return finalSize;
            }
            int items = 0;
            double num9 = this.GetItemSize(container.DesiredSize) * num6;
            double nextStartPosition = 0.0;
            while (true)
            {
                double y = num9;
                container = this.ItemsContainerGenerator.GetContainer(index);
                if (container != null)
                {
                    Size size = new Size((this.Orientation == System.Windows.Controls.Orientation.Vertical) ? finalSize.Width : container.DesiredSize.Width, (this.Orientation == System.Windows.Controls.Orientation.Vertical) ? container.DesiredSize.Height : finalSize.Height);
                    container.Arrange((this.Orientation == System.Windows.Controls.Orientation.Vertical) ? new Rect(new Point(0.0, y), size) : new Rect(new Point(y, 0.0), size));
                    double num11 = this.GetItemSize(size);
                    GetRelativeViewport(nextStartPosition, itemSize, num11);
                    num9 = y + num11;
                    nextStartPosition += num11;
                    index = this.IncrementIndex(index, true);
                    items++;
                    if (this.CalcStopCriteria(items, y, itemSize))
                    {
                        continue;
                    }
                }
                return finalSize;
            }
        }

        private int CalcIndex(double offset) => 
            this.IndexCalculator.LogicalOffsetToIndex(offset, this.ItemsContainerGenerator.GetItemsCount(), this.IsLooped);

        private bool CalcStopCriteria(int items, double offset, double viewport)
        {
            bool flag = this.IndexCalculator.AreLessOrClose(offset, viewport);
            return (!this.IsLooped ? flag : (flag && (items < this.ItemsContainerGenerator.GetItemsCount())));
        }

        private double CalcViewport(double viewport)
        {
            double logicalViewport = this.GetLogicalViewport();
            double offset = this.GetOffset(logicalViewport);
            double num3 = this.EnsureLogicalOffset(offset, this.GetLogicalExtent(), logicalViewport);
            int index = this.CalcIndex(num3);
            double num5 = this.IndexCalculator.CalcRelativePosition(num3);
            double num6 = 0.0;
            UIElement container = this.ItemsContainerGenerator.GetContainer(index);
            if (container == null)
            {
                return 0.0;
            }
            double num7 = this.GetItemSize(container.DesiredSize) * num5;
            double nextStartPosition = 0.0;
            int items = 0;
            while (true)
            {
                container = this.ItemsContainerGenerator.GetContainer(index);
                if (container != null)
                {
                    double itemSize = this.GetItemSize(container.DesiredSize);
                    nextStartPosition += itemSize;
                    num6 += GetRelativeViewport(nextStartPosition, viewport, itemSize);
                    num7 += itemSize;
                    index = this.IncrementIndex(index, true);
                    items++;
                    if (this.CalcStopCriteria(items, num7, viewport))
                    {
                        continue;
                    }
                }
                return num6;
            }
        }

        private static double ComputeScrollOffset(double viewTop, double viewBottom, double childTop, double childBottom)
        {
            bool flag = (childTop < viewTop) && (childBottom < viewBottom);
            bool flag2 = (childBottom > viewBottom) && (childTop > viewTop);
            bool flag3 = (childBottom - childTop) > (viewBottom - viewTop);
            if (!flag && !flag2)
            {
                return viewTop;
            }
            if ((flag && !flag3) || (flag2 & flag3))
            {
                return childTop;
            }
            return (childBottom - (viewBottom - viewTop));
        }

        protected virtual DevExpress.Xpf.Editors.IndexCalculator CreateIndexCalculator() => 
            new DevExpress.Xpf.Editors.IndexCalculator();

        private double EnsureLogicalOffset(double offset, double extent, double viewport) => 
            (!this.IsLooped || (extent.AreClose(0.0) || viewport.AreClose(0.0))) ? offset : (((Math.Sign(offset) <= 0) || (offset >= extent)) ? ((Math.Sign(offset) > 0) ? (offset % extent) : (extent - (Math.Abs(offset) % extent))) : offset);

        private double EnsureLoopedOffset(double offset, double extent, double viewport) => 
            (!offset.GreaterThanOrClose(0.0) || !offset.LessThan(extent)) ? (offset.GreaterThan(0.0) ? (offset % extent) : (extent - (Math.Abs(offset) % extent))) : offset;

        private double EnsureNormalOffset(double offset, double extent, double viewport)
        {
            Func<IDXItemContainerGenerator, int> evaluator = <>c.<>9__93_0;
            if (<>c.<>9__93_0 == null)
            {
                Func<IDXItemContainerGenerator, int> local1 = <>c.<>9__93_0;
                evaluator = <>c.<>9__93_0 = x => x.GetItemsCount();
            }
            int num = this.ItemsContainerGenerator.Return<IDXItemContainerGenerator, int>(evaluator, <>c.<>9__93_1 ??= () => 0);
            return Math.Min(Math.Max(offset, 0.0), this.IndexCalculator.IndexToLogicalOffset((int) (num - 1)));
        }

        private double EnsureOffset(double offset, double extent, double viewport) => 
            this.IsLooped ? this.EnsureLoopedOffset(offset, extent, viewport) : this.EnsureNormalOffset(offset, extent, viewport);

        public IEnumerable<float> GetIrregularSnapPoints(System.Windows.Controls.Orientation orientation, SnapPointsAlignment alignment)
        {
            throw new NotImplementedException();
        }

        private double GetItemSize(Size itemSize)
        {
            double d = (this.Orientation == System.Windows.Controls.Orientation.Vertical) ? itemSize.Height : itemSize.Width;
            if (double.IsInfinity(d) && (this.ScrollOwner != null))
            {
                d = (this.Orientation == System.Windows.Controls.Orientation.Vertical) ? this.ScrollOwner.ActualHeight : this.ScrollOwner.ActualWidth;
            }
            return d;
        }

        private double GetLogicalExtent() => 
            (this.Orientation == System.Windows.Controls.Orientation.Vertical) ? this.ExtentHeight : this.ExtentWidth;

        private double GetLogicalViewport() => 
            (this.Orientation == System.Windows.Controls.Orientation.Vertical) ? this.ViewportHeight : this.ViewportWidth;

        private double GetOffset(double logicalViewport) => 
            this.IndexCalculator.OffsetToLogicalOffset((this.Orientation == System.Windows.Controls.Orientation.Vertical) ? this.VerticalOffset : this.HorizontalOffset, logicalViewport);

        public virtual float GetRegularSnapPoints(System.Windows.Controls.Orientation orientation, SnapPointsAlignment alignment, out float offset)
        {
            offset = 0f;
            double num = this.IndexCalculator.IndexToLogicalOffset(1);
            switch (alignment)
            {
                case SnapPointsAlignment.Near:
                    offset = 0f;
                    break;

                case SnapPointsAlignment.Center:
                    offset = ((float) num) / 2f;
                    break;

                case SnapPointsAlignment.Far:
                    offset = (float) (((orientation == System.Windows.Controls.Orientation.Vertical) ? this.ViewportHeight : this.ViewportWidth) - (num / 2.0));
                    break;

                default:
                    break;
            }
            return (float) num;
        }

        private static double GetRelativeViewport(double nextStartPosition, double viewport, double itemSize)
        {
            double num = nextStartPosition - viewport;
            return (!num.GreaterThan(itemSize) ? ((nextStartPosition > viewport) ? ((itemSize - num) / itemSize) : 1.0) : 0.0);
        }

        private double GetSize(Size size)
        {
            double itemSize = this.GetItemSize(size);
            return (double.IsInfinity(itemSize) ? 0.0 : itemSize);
        }

        private double GetSize(Size size, System.Windows.Controls.Orientation orientation)
        {
            double d = (orientation == System.Windows.Controls.Orientation.Vertical) ? size.Height : size.Width;
            return (double.IsInfinity(d) ? 0.0 : d);
        }

        private int IncrementIndex(int index, bool direction)
        {
            index += direction ? 1 : -1;
            return (this.IsLooped ? this.IndexToRange(index) : index);
        }

        private int IndexToRange(int index)
        {
            int itemsCount = this.ItemsContainerGenerator.GetItemsCount();
            if (itemsCount == 0)
            {
                return 0;
            }
            if (index >= itemsCount)
            {
                index = index % itemsCount;
            }
            else if (index < 0)
            {
                index = itemsCount - (Math.Abs(index) % itemsCount);
            }
            return index;
        }

        public void InvalidatePanel()
        {
            base.InvalidateMeasure();
        }

        private void InvalidateScrollInfo(Size newViewportSize, Size newExtentSize)
        {
            if ((this.viewportSize != newViewportSize) || (this.extentSize != newExtentSize))
            {
                this.viewportSize = newViewportSize;
                this.extentSize = newExtentSize;
                Action<ScrollViewer> action = <>c.<>9__73_0;
                if (<>c.<>9__73_0 == null)
                {
                    Action<ScrollViewer> local1 = <>c.<>9__73_0;
                    action = <>c.<>9__73_0 = x => x.InvalidateScrollInfo();
                }
                this.ScrollOwner.Do<ScrollViewer>(action);
                base.InvalidateMeasure();
            }
        }

        protected virtual void ItemSizeChanged(Size newValue)
        {
        }

        public void LineDown()
        {
            this.SetVerticalOffset(this.VerticalOffset + this.IndexCalculator.IndexToLogicalOffset(1));
        }

        public void LineLeft()
        {
            this.SetHorizontalOffset(this.HorizontalOffset - this.IndexCalculator.IndexToLogicalOffset(1));
        }

        public void LineRight()
        {
            this.SetHorizontalOffset(this.HorizontalOffset + this.IndexCalculator.IndexToLogicalOffset(1));
        }

        public void LineUp()
        {
            this.SetVerticalOffset(this.VerticalOffset - this.IndexCalculator.IndexToLogicalOffset(1));
        }

        protected virtual void LoopedChanged(bool newValue)
        {
        }

        public unsafe Rect MakeVisible(Visual visual, Rect rectangle)
        {
            if (rectangle.IsEmpty || ((visual == null) || ReferenceEquals(visual, this)))
            {
                return Rect.Empty;
            }
            Rect rect = new Rect(this.HorizontalOffset, this.VerticalOffset, this.ViewportWidth, this.ViewportHeight);
            Rect* rectPtr1 = &rectangle;
            rectPtr1.X += rect.X;
            Rect* rectPtr2 = &rectangle;
            rectPtr2.Y += rect.Y;
            rect.X = ComputeScrollOffset(rect.Left, rect.Right, rectangle.Left, rectangle.Right);
            rect.Y = ComputeScrollOffset(rect.Top, rect.Bottom, rectangle.Top, rectangle.Bottom);
            this.SetHorizontalOffset(rect.X);
            this.SetVerticalOffset(rect.Y);
            rectangle.Intersect(rect);
            Rect* rectPtr3 = &rectangle;
            rectPtr3.X -= rect.X;
            Rect* rectPtr4 = &rectangle;
            rectPtr4.Y -= rect.Y;
            return rectangle;
        }

        public unsafe Rect MakeVisible(UIElement visual, Rect rectangle)
        {
            if (rectangle.IsEmpty || ((visual == null) || ReferenceEquals(visual, this)))
            {
                return Rect.Empty;
            }
            Rect rect = new Rect(this.HorizontalOffset, this.VerticalOffset, this.ViewportWidth, this.ViewportHeight);
            Rect* rectPtr1 = &rectangle;
            rectPtr1.X += rect.X;
            Rect* rectPtr2 = &rectangle;
            rectPtr2.Y += rect.Y;
            rect.X = ComputeScrollOffset(rect.Left, rect.Right, rectangle.Left, rectangle.Right);
            rect.Y = ComputeScrollOffset(rect.Top, rect.Bottom, rectangle.Top, rectangle.Bottom);
            this.SetHorizontalOffset(rect.X);
            this.SetVerticalOffset(rect.Y);
            rectangle.Intersect(rect);
            Rect* rectPtr3 = &rectangle;
            rectPtr3.X -= rect.X;
            Rect* rectPtr4 = &rectangle;
            rectPtr4.Y -= rect.Y;
            return rectangle;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (this.ItemsContainerGenerator == null)
            {
                return availableSize;
            }
            double logicalViewport = this.GetLogicalViewport();
            double offset = this.GetOffset(logicalViewport);
            double itemSize = this.GetItemSize(availableSize);
            double num4 = this.EnsureLogicalOffset(offset, this.GetLogicalExtent(), logicalViewport);
            int index = this.CalcIndex(num4);
            double num6 = (this.Orientation == System.Windows.Controls.Orientation.Vertical) ? this.GetSize(availableSize, System.Windows.Controls.Orientation.Horizontal) : 0.0;
            double num7 = (this.Orientation == System.Windows.Controls.Orientation.Vertical) ? 0.0 : this.GetSize(availableSize, System.Windows.Controls.Orientation.Vertical);
            double num8 = 0.0;
            int items = 0;
            double num10 = 0.0;
            this.ItemsContainerGenerator.StartAt(index);
            while (true)
            {
                num8 = num10;
                bool isNew = false;
                UIElement element = this.ItemsContainerGenerator.Generate(index, out isNew);
                if (element != null)
                {
                    if (isNew)
                    {
                        base.InternalChildren.Add(element);
                    }
                    this.ItemsContainerGenerator.PrepareItemContainer(index, element);
                    element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    double num11 = this.GetItemSize(element.DesiredSize);
                    if (this.Orientation == System.Windows.Controls.Orientation.Vertical)
                    {
                        num6 = Math.Max(element.DesiredSize.Width, num6);
                        num7 += element.DesiredSize.Height;
                    }
                    else
                    {
                        num6 += element.DesiredSize.Width;
                        num7 = Math.Max(element.DesiredSize.Height, num7);
                    }
                    num10 = num8 + num11;
                    index = this.IncrementIndex(index, true);
                    items++;
                    if (this.CalcStopCriteria(items, num8, itemSize))
                    {
                        continue;
                    }
                }
                this.ItemsContainerGenerator.Stop();
                this.ItemsContainerGenerator.RemoveItems();
                return ((this.Orientation == System.Windows.Controls.Orientation.Vertical) ? new Size(num6, double.IsInfinity(availableSize.Height) ? num7 : availableSize.Height) : new Size(double.IsInfinity(availableSize.Width) ? num6 : availableSize.Width, num7));
            }
        }

        public void MouseWheelDown()
        {
            this.SetVerticalOffset(this.VerticalOffset - this.IndexCalculator.IndexToLogicalOffset((double) 1.0));
            Action<DXScrollViewer> action = <>c.<>9__80_0;
            if (<>c.<>9__80_0 == null)
            {
                Action<DXScrollViewer> local1 = <>c.<>9__80_0;
                action = <>c.<>9__80_0 = x => x.IsIntermediate = true;
            }
            (this.ScrollOwner as DXScrollViewer).Do<DXScrollViewer>(action);
            this.ScrollOwner.Do<ScrollViewer>(x => x.ScrollToVerticalOffset(this.verticalOffset));
        }

        public void MouseWheelLeft()
        {
            this.SetHorizontalOffset(this.HorizontalOffset + (((SystemParameters.WheelScrollLines * 3) / 9) * 10));
            Action<DXScrollViewer> action = <>c.<>9__81_0;
            if (<>c.<>9__81_0 == null)
            {
                Action<DXScrollViewer> local1 = <>c.<>9__81_0;
                action = <>c.<>9__81_0 = x => x.IsIntermediate = true;
            }
            (this.ScrollOwner as DXScrollViewer).Do<DXScrollViewer>(action);
            this.ScrollOwner.Do<ScrollViewer>(x => x.ScrollToHorizontalOffset(this.horizontalOffset));
        }

        public void MouseWheelRight()
        {
            this.SetHorizontalOffset(this.HorizontalOffset - (((SystemParameters.WheelScrollLines * 3) / 9) * 10));
            Action<DXScrollViewer> action = <>c.<>9__82_0;
            if (<>c.<>9__82_0 == null)
            {
                Action<DXScrollViewer> local1 = <>c.<>9__82_0;
                action = <>c.<>9__82_0 = x => x.IsIntermediate = true;
            }
            (this.ScrollOwner as DXScrollViewer).Do<DXScrollViewer>(action);
            this.ScrollOwner.Do<ScrollViewer>(x => x.ScrollToHorizontalOffset(this.horizontalOffset));
        }

        public void MouseWheelUp()
        {
            this.SetVerticalOffset(this.VerticalOffset + this.IndexCalculator.IndexToLogicalOffset((double) 1.0));
            Action<DXScrollViewer> action = <>c.<>9__83_0;
            if (<>c.<>9__83_0 == null)
            {
                Action<DXScrollViewer> local1 = <>c.<>9__83_0;
                action = <>c.<>9__83_0 = x => x.IsIntermediate = true;
            }
            (this.ScrollOwner as DXScrollViewer).Do<DXScrollViewer>(action);
            this.ScrollOwner.Do<ScrollViewer>(x => x.ScrollToVerticalOffset(this.verticalOffset));
        }

        private void OnManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            Action<IDXItemContainerGenerator> action = <>c.<>9__51_0;
            if (<>c.<>9__51_0 == null)
            {
                Action<IDXItemContainerGenerator> local1 = <>c.<>9__51_0;
                action = <>c.<>9__51_0 = x => x.StopManipulation();
            }
            this.ItemsContainerGenerator.Do<IDXItemContainerGenerator>(action);
        }

        private void OnManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            Action<IDXItemContainerGenerator> action = <>c.<>9__52_0;
            if (<>c.<>9__52_0 == null)
            {
                Action<IDXItemContainerGenerator> local1 = <>c.<>9__52_0;
                action = <>c.<>9__52_0 = x => x.StartManipulation();
            }
            this.ItemsContainerGenerator.Do<IDXItemContainerGenerator>(action);
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

        private void RaiseHorizontalSnapPointsChanged()
        {
            if (this.HorizontalSnapPointsChanged != null)
            {
                this.HorizontalSnapPointsChanged(this, EventArgs.Empty);
            }
        }

        private void RaiseVerticalSnapPointsChanged()
        {
            if (this.VerticalSnapPointsChanged != null)
            {
                this.VerticalSnapPointsChanged(this, EventArgs.Empty);
            }
        }

        public void SetHorizontalOffset(double offset)
        {
            if (offset != this.HorizontalOffset)
            {
                this.horizontalOffset = this.EnsureOffset(offset, this.ExtentWidth, this.ViewportWidth);
                this.InvalidatePanel();
            }
        }

        public void SetVerticalOffset(double offset)
        {
            double num = this.EnsureOffset(offset, this.ExtentHeight, this.ViewportHeight);
            if (!num.AreClose(this.VerticalOffset))
            {
                this.verticalOffset = num;
                this.InvalidatePanel();
            }
        }

        protected void UpdateScrollData(double viewportSize)
        {
            if (this.ItemsContainerGenerator == null)
            {
                this.InvalidateScrollInfo(Size.Empty, Size.Empty);
            }
            else
            {
                int itemsCount = this.ItemsContainerGenerator.GetItemsCount();
                double index = this.CalcViewport(viewportSize);
                double height = this.IndexCalculator.IndexToLogicalOffset(index);
                Size newViewportSize = (this.Orientation == System.Windows.Controls.Orientation.Vertical) ? new Size(1.0, height) : new Size(height, 1.0);
                double num4 = this.IsLooped ? this.IndexCalculator.IndexToLogicalOffset(itemsCount) : this.IndexCalculator.IndexToLogicalOffset((int) ((2 * itemsCount) - 1));
                if (!this.IsLooped && num4.LessThanOrClose(height))
                {
                    num4 = height + this.IndexCalculator.IndexToLogicalOffset(1);
                }
                this.InvalidateScrollInfo(newViewportSize, (this.Orientation == System.Windows.Controls.Orientation.Vertical) ? new Size(1.0, num4) : new Size(num4, 1.0));
            }
        }

        public IDXItemContainerGenerator ItemsContainerGenerator { get; set; }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            set => 
                base.SetValue(OrientationProperty, value);
        }

        public bool IsLooped
        {
            get => 
                (bool) base.GetValue(IsLoopedProperty);
            set => 
                base.SetValue(IsLoopedProperty, value);
        }

        public DevExpress.Xpf.Editors.IndexCalculator IndexCalculator { get; private set; }

        public bool CanHorizontallyScroll { get; set; }

        public bool CanVerticallyScroll { get; set; }

        public ScrollViewer ScrollOwner
        {
            get => 
                this.scrollOwner;
            set
            {
                if (this.scrollOwner != null)
                {
                    this.scrollOwner.ManipulationStarted -= new EventHandler<ManipulationStartedEventArgs>(this.OnManipulationStarted);
                    this.scrollOwner.ManipulationCompleted -= new EventHandler<ManipulationCompletedEventArgs>(this.OnManipulationCompleted);
                }
                this.scrollOwner = value;
                if (this.scrollOwner != null)
                {
                    this.scrollOwner.ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(this.OnManipulationStarted);
                    this.scrollOwner.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(this.OnManipulationCompleted);
                }
            }
        }

        public double Extent =>
            (this.Orientation == System.Windows.Controls.Orientation.Vertical) ? this.ExtentHeight : this.ExtentWidth;

        public double ExtentHeight =>
            this.extentSize.Height;

        public double ExtentWidth =>
            this.extentSize.Width;

        public double Offset =>
            (this.Orientation == System.Windows.Controls.Orientation.Vertical) ? this.VerticalOffset : this.HorizontalOffset;

        public double HorizontalOffset =>
            this.horizontalOffset;

        public double VerticalOffset =>
            this.verticalOffset;

        public double Viewport =>
            (this.Orientation == System.Windows.Controls.Orientation.Vertical) ? this.ViewportHeight : this.ViewportWidth;

        public double ViewportHeight =>
            this.viewportSize.Height;

        public double ViewportWidth =>
            this.viewportSize.Width;

        public bool AreHorizontalSnapPointsRegular =>
            true;

        public bool AreVerticalSnapPointsRegular =>
            true;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LoopedPanel.<>c <>9 = new LoopedPanel.<>c();
            public static Action<IDXItemContainerGenerator> <>9__51_0;
            public static Action<IDXItemContainerGenerator> <>9__52_0;
            public static Action<ScrollViewer> <>9__73_0;
            public static Action<DXScrollViewer> <>9__80_0;
            public static Action<DXScrollViewer> <>9__81_0;
            public static Action<DXScrollViewer> <>9__82_0;
            public static Action<DXScrollViewer> <>9__83_0;
            public static Func<IDXItemContainerGenerator, int> <>9__93_0;
            public static Func<int> <>9__93_1;

            internal void <.cctor>b__2_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LoopedPanel) d).LoopedChanged((bool) e.NewValue);
            }

            internal int <EnsureNormalOffset>b__93_0(IDXItemContainerGenerator x) => 
                x.GetItemsCount();

            internal int <EnsureNormalOffset>b__93_1() => 
                0;

            internal void <InvalidateScrollInfo>b__73_0(ScrollViewer x)
            {
                x.InvalidateScrollInfo();
            }

            internal void <MouseWheelDown>b__80_0(DXScrollViewer x)
            {
                x.IsIntermediate = true;
            }

            internal void <MouseWheelLeft>b__81_0(DXScrollViewer x)
            {
                x.IsIntermediate = true;
            }

            internal void <MouseWheelRight>b__82_0(DXScrollViewer x)
            {
                x.IsIntermediate = true;
            }

            internal void <MouseWheelUp>b__83_0(DXScrollViewer x)
            {
                x.IsIntermediate = true;
            }

            internal void <OnManipulationCompleted>b__51_0(IDXItemContainerGenerator x)
            {
                x.StopManipulation();
            }

            internal void <OnManipulationStarted>b__52_0(IDXItemContainerGenerator x)
            {
                x.StartManipulation();
            }
        }
    }
}

