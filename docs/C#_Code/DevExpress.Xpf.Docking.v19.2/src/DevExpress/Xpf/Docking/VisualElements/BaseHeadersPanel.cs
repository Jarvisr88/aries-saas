namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.UIAutomation;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    [DXToolboxBrowsable(false)]
    public abstract class BaseHeadersPanel : psvPanel
    {
        public static readonly DependencyProperty OrientationProperty;
        public static readonly DependencyProperty TabHeaderLayoutTypeProperty;
        public static readonly DependencyProperty IsAutoFillHeadersProperty;
        public static readonly DependencyProperty ScrollOffsetProperty;
        public static readonly DependencyProperty ScrollIndexProperty;
        public static readonly DependencyProperty SelectedIndexProperty;
        public static readonly DependencyProperty ClipMarginProperty;
        public static readonly DependencyProperty TransparencySizeProperty;
        public static readonly DependencyProperty FixedMultiLineTabHeadersProperty;
        public static readonly DependencyProperty AutoScrollOnOverflowProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty LayoutItemProperty;
        private double offset;
        private bool canAnimate;
        private bool hasScroll;
        private DoubleAnimation ScrollAnimation;
        private int lockStartAnimation;

        static BaseHeadersPanel()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<BaseHeadersPanel> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<BaseHeadersPanel>();
            registrator.OverrideMetadataNotDataBindable<bool>(Panel.IsItemsHostProperty, true, null, null);
            registrator.Register<Thickness>("ClipMargin", ref ClipMarginProperty, new Thickness(0.0, -2.0, 0.0, -2.0), FrameworkPropertyMetadataOptions.AffectsArrange, null, null);
            registrator.Register<System.Windows.Controls.Orientation>("Orientation", ref OrientationProperty, System.Windows.Controls.Orientation.Horizontal, (dObj, e) => ((BaseHeadersPanel) dObj).OnOrientationChanged((System.Windows.Controls.Orientation) e.NewValue), null);
            registrator.Register<DevExpress.Xpf.Layout.Core.TabHeaderLayoutType>("TabHeaderLayoutType", ref TabHeaderLayoutTypeProperty, DevExpress.Xpf.Layout.Core.TabHeaderLayoutType.Default, (dObj, e) => ((BaseHeadersPanel) dObj).OnTabHeaderLayoutChanged((DevExpress.Xpf.Layout.Core.TabHeaderLayoutType) e.NewValue), null);
            registrator.Register<bool>("IsAutoFillHeaders", ref IsAutoFillHeadersProperty, false, (dObj, e) => ((BaseHeadersPanel) dObj).OnIsAutoFillHeadersChanged((bool) e.NewValue), null);
            registrator.Register<double>("ScrollOffset", ref ScrollOffsetProperty, 0.0, (dObj, e) => ((BaseHeadersPanel) dObj).OnScrollOffsetChanged((double) e.NewValue), null);
            registrator.Register<int>("ScrollIndex", ref ScrollIndexProperty, 0, (dObj, e) => ((BaseHeadersPanel) dObj).OnScrollIndexChanged((int) e.NewValue, (int) e.OldValue), null);
            registrator.Register<int>("SelectedIndex", ref SelectedIndexProperty, -1, (dObj, e) => ((BaseHeadersPanel) dObj).OnSelectedIndexChanged((int) e.NewValue, (int) e.OldValue), null);
            registrator.Register<double>("TransparencySize", ref TransparencySizeProperty, 7.0, null, null);
            registrator.Register<bool>("FixedMultiLineTabHeaders", ref FixedMultiLineTabHeadersProperty, false, (dObj, e) => ((BaseHeadersPanel) dObj).OnFixedMultiLineTabHeadersChanged((bool) e.NewValue), null);
            registrator.Register<DevExpress.Xpf.Docking.AutoScrollOnOverflow>("AutoScrollOnOverflow", ref AutoScrollOnOverflowProperty, DevExpress.Xpf.Docking.AutoScrollOnOverflow.AnyItem, null, null);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseHeadersPanel), "d");
            System.Linq.Expressions.Expression[] arguments = new System.Linq.Expressions.Expression[] { expression };
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<BaseHeadersPanel>.New().AddOwner<BaseLayoutItem>(System.Linq.Expressions.Expression.Lambda<Func<BaseHeadersPanel, BaseLayoutItem>>(System.Linq.Expressions.Expression.Call(null, (MethodInfo) methodof(DockLayoutManager.GetLayoutItem), arguments), parameters), out LayoutItemProperty, DockLayoutManager.LayoutItemProperty, null, (d, oldValue, newValue) => d.OnLayoutItemChanged(oldValue, newValue));
        }

        public BaseHeadersPanel()
        {
            DockPane.SetHitTestType(this, HitTestType.PageHeaders);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            bool isHorizontal = this.IsHorizontal;
            foreach (UIElement element in base.InternalChildren)
            {
                ITabHeader header = element as ITabHeader;
                if (header != null)
                {
                    Rect arrangeRect = header.ArrangeRect;
                    if (arrangeRect.IsEmpty)
                    {
                        element.Arrange(new Rect(0.0, 0.0, 0.0, 0.0));
                        continue;
                    }
                    if (!header.IsPinned)
                    {
                        RectHelper.Offset(ref arrangeRect, isHorizontal ? this.ScrollOffset : 0.0, isHorizontal ? 0.0 : this.ScrollOffset);
                    }
                    if (this.StretchItems)
                    {
                        if (isHorizontal)
                        {
                            arrangeRect.Height = finalSize.Height;
                        }
                        else
                        {
                            arrangeRect.Width = finalSize.Width;
                        }
                    }
                    element.Arrange(arrangeRect);
                }
            }
            this.UpdateOpacityMask(finalSize);
            return finalSize;
        }

        protected virtual ITabHeaderLayoutCalculator GetCalculator(DevExpress.Xpf.Layout.Core.TabHeaderLayoutType type) => 
            HeaderLayoutCalculatorFactory.GetCalculator(type);

        protected virtual Thickness GetClipMargin() => 
            this.ClipMargin.Multiply(ScreenHelper.DpiThicknessCorrection);

        protected override Geometry GetLayoutClip(Size layoutSlotSize)
        {
            Thickness clipMargin = this.GetClipMargin();
            Rect rect = new Rect(clipMargin.Left, clipMargin.Top, layoutSlotSize.Width - (clipMargin.Left + clipMargin.Right), layoutSlotSize.Height - (clipMargin.Top + clipMargin.Bottom));
            RectangleGeometry geometry1 = new RectangleGeometry();
            geometry1.Rect = rect;
            return geometry1;
        }

        protected virtual ITabHeaderLayoutOptions GetOptions(Size availableSize)
        {
            BaseLayoutItem layoutItem = DockLayoutManager.GetLayoutItem(this);
            return new TabHeaderOptions(availableSize, this.IsHorizontal, this.IsAutoFillHeaders, this.ScrollIndex, (layoutItem != null) && ((layoutItem.CaptionLocation == CaptionLocation.Right) || (layoutItem.CaptionLocation == CaptionLocation.Bottom)), this.FixedMultiLineTabHeaders, this.offset);
        }

        public static void Invalidate(DependencyObject dObj)
        {
            BaseHeadersPanel panel = dObj as BaseHeadersPanel;
            if ((panel == null) && (dObj != null))
            {
                panel = LayoutHelper.FindParentObject<BaseHeadersPanel>(dObj);
            }
            if (panel != null)
            {
                panel.InvalidateMeasure();
            }
        }

        protected virtual ITabHeaderLayoutResult Measure(ITabHeaderLayoutCalculator calculator, ITabHeaderLayoutOptions options) => 
            HeadersPanelHelper.Measure(base.InternalChildren, calculator, options);

        protected override Size MeasureOverride(Size availableSize)
        {
            base.MeasureOverride(availableSize);
            ITabHeaderLayoutCalculator calculator = this.GetCalculator(this.TabHeaderLayoutType);
            ITabHeaderLayoutResult result = this.Measure(calculator, this.GetOptions(availableSize));
            LayoutGroup layoutItem = DockLayoutManager.GetLayoutItem(this) as LayoutGroup;
            this.hasScroll = false;
            if ((layoutItem != null) && !result.IsEmpty)
            {
                this.hasScroll = layoutItem.TabHeaderHasScroll = result.HasScroll;
                if (!result.HasScroll)
                {
                    this.offset = 0.0;
                    layoutItem.TabHeaderScrollIndex = -1;
                }
                else
                {
                    layoutItem.TabHeaderMaxScrollIndex = result.ScrollResult.MaxIndex;
                    layoutItem.TabHeaderScrollIndex = result.ScrollResult.Index;
                    layoutItem.TabHeaderCanScrollPrev = result.ScrollResult.CanScrollPrev;
                    layoutItem.TabHeaderCanScrollNext = result.ScrollResult.CanScrollNext;
                    this.offset -= result.ScrollResult.ScrollOffset;
                    if (this.AllowAnimation && this.canAnimate)
                    {
                        this.canAnimate = false;
                        this.StartScrollAnimation(result, layoutItem);
                    }
                }
            }
            return result.Size;
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new BaseHeadersPanelAutomationPeer(this);

        protected virtual void OnFixedMultiLineTabHeadersChanged(bool newValue)
        {
            base.InvalidateMeasure();
        }

        protected virtual void OnIsAutoFillHeadersChanged(bool isAutoFill)
        {
            base.InvalidateMeasure();
            base.InvalidateArrange();
        }

        protected virtual void OnLayoutItemChanged(BaseLayoutItem oldValue, BaseLayoutItem newValue)
        {
            psvItemsControl itemsOwner = ItemsControl.GetItemsOwner(this) as psvItemsControl;
            if (itemsOwner == null)
            {
                psvItemsControl local1 = itemsOwner;
            }
            else
            {
                itemsOwner.EnsureItemsPanel();
            }
        }

        protected virtual void OnOrientationChanged(System.Windows.Controls.Orientation orientation)
        {
            base.InvalidateMeasure();
        }

        private void OnScrollAnimationCompleted(object sender, EventArgs e)
        {
            this.ScrollAnimation.Completed -= new EventHandler(this.OnScrollAnimationCompleted);
            this.ScrollAnimation = null;
            LayoutGroup layoutItem = DockLayoutManager.GetLayoutItem(this) as LayoutGroup;
            if (layoutItem != null)
            {
                layoutItem.IsAnimated = false;
            }
            this.lockStartAnimation--;
        }

        protected virtual void OnScrollIndexChanged(int index, int oldIndex)
        {
            this.canAnimate = true;
            base.InvalidateMeasure();
        }

        protected virtual void OnScrollOffsetChanged(double scrollOffset)
        {
            base.InvalidateArrange();
        }

        protected virtual void OnSelectedIndexChanged(int index, int oldIndex)
        {
            if ((base.InternalChildren.Count != 0) && ((this.SelectedIndex >= 0) && (this.SelectedIndex <= (base.InternalChildren.Count - 1))))
            {
                ITabHeader header = base.InternalChildren[this.SelectedIndex] as ITabHeader;
                if ((header != null) && !header.IsPinned)
                {
                    LayoutGroup layoutItem = DockLayoutManager.GetLayoutItem(this) as LayoutGroup;
                    if (layoutItem != null)
                    {
                        BaseLayoutItem item = (this.AutoScrollOnOverflow != DevExpress.Xpf.Docking.AutoScrollOnOverflow.AnyItem) ? layoutItem.ItemFromTabIndex((header.ScrollIndex < 0) ? index : header.ScrollIndex) : layoutItem.ItemFromIndex(index);
                        layoutItem.EnsureTabHeaderScrollIndex(item);
                    }
                }
            }
        }

        protected virtual void OnTabHeaderLayoutChanged(DevExpress.Xpf.Layout.Core.TabHeaderLayoutType type)
        {
            base.InvalidateMeasure();
            base.InvalidateArrange();
        }

        private void ResetOpacityMask(UIElement element)
        {
            element.OpacityMask = null;
            element.Opacity = 1.0;
            element.IsHitTestVisible = true;
        }

        private void StartScrollAnimation(ITabHeaderLayoutResult result, LayoutGroup group)
        {
            if (this.lockStartAnimation <= 0)
            {
                this.lockStartAnimation++;
                group.IsAnimated = true;
                DoubleAnimation animation1 = new DoubleAnimation();
                animation1.From = new double?(result.ScrollResult.ScrollOffset);
                animation1.To = 0.0;
                animation1.Duration = new Duration(TimeSpan.FromMilliseconds(150.0));
                this.ScrollAnimation = animation1;
                this.ScrollAnimation.Completed += new EventHandler(this.OnScrollAnimationCompleted);
                base.BeginAnimation(ScrollOffsetProperty, this.ScrollAnimation);
            }
        }

        private void UpdateOpacityMask(Rect available)
        {
            bool isHorizontal = this.IsHorizontal;
            OrientationHelper helper = this._OrientationHelper;
            foreach (UIElement element in base.InternalChildren)
            {
                ITabHeader header = element as ITabHeader;
                if (header != null)
                {
                    Rect arrangeRect = header.ArrangeRect;
                    if (!arrangeRect.IsEmpty && !header.IsPinned)
                    {
                        RectHelper.Offset(ref arrangeRect, this.IsHorizontal ? this.ScrollOffset : 0.0, isHorizontal ? 0.0 : this.ScrollOffset);
                        Rect rect = Rect.Intersect(arrangeRect, available);
                        Interval interval = new Interval(helper.GetValue(rect.Location), helper.GetLength(rect));
                        Interval interval2 = new Interval(helper.GetValue(arrangeRect.Location), helper.GetLength(arrangeRect));
                        double length = interval2.Length;
                        if (interval.Length != length)
                        {
                            if (rect.IsEmpty || (interval.Length < this.TransparencySize))
                            {
                                element.Opacity = 0.0;
                                element.IsHitTestVisible = false;
                                continue;
                            }
                            Interval interval3 = new Interval(0.0, interval.Start - interval2.Start);
                            Interval interval4 = new Interval(interval3.End, this.TransparencySize);
                            Interval interval6 = new Interval(new Interval(interval.End - interval2.Start, interval2.End - interval.End).Start - this.TransparencySize, this.TransparencySize);
                            LinearGradientBrush brush1 = new LinearGradientBrush();
                            brush1.StartPoint = helper.GetPoint(0.0, 0.5);
                            brush1.EndPoint = helper.GetPoint(1.0, 0.5);
                            LinearGradientBrush brush = brush1;
                            if (interval.Start != interval2.Start)
                            {
                                brush.GradientStops.Add(new GradientStop(Colors.Transparent, 0.0));
                                brush.GradientStops.Add(new GradientStop(Colors.Transparent, interval3.Length / length));
                            }
                            brush.GradientStops.Add(new GradientStop(Colors.Black, (interval3.Length + interval4.Length) / length));
                            if (interval.End != interval2.End)
                            {
                                brush.GradientStops.Add(new GradientStop(Colors.Black, interval6.Start / length));
                                brush.GradientStops.Add(new GradientStop(Colors.Transparent, interval6.End / length));
                                brush.GradientStops.Add(new GradientStop(Colors.Transparent, 1.0));
                            }
                            element.OpacityMask = brush;
                        }
                    }
                }
            }
        }

        private void UpdateOpacityMask(Size finalSize)
        {
            double num = 0.0;
            double num2 = 0.0;
            OrientationHelper helper = this._OrientationHelper;
            double length = helper.GetLength(finalSize);
            foreach (UIElement element in base.InternalChildren)
            {
                this.ResetOpacityMask(element);
                ITabHeader header = element as ITabHeader;
                if ((header != null) && header.IsPinned)
                {
                    Rect arrangeRect = header.ArrangeRect;
                    if (!arrangeRect.IsEmpty)
                    {
                        double num4 = helper.GetLength(arrangeRect);
                        if (header.PinLocation == TabHeaderPinLocation.Far)
                        {
                            length = Math.Min(length, helper.GetValue(arrangeRect.Location));
                        }
                        else
                        {
                            num2 = Math.Max(num2, helper.GetValue(arrangeRect.BottomRight));
                        }
                        num += num4;
                    }
                }
            }
            if ((num > 0.0) && this.hasScroll)
            {
                Point location = helper.GetPoint(num2, 0.0);
                Rect available = new Rect(location, helper.GetSize(((length > num2) ? length : helper.GetLength(finalSize)) - num2, helper.GetDim(finalSize)));
                this.UpdateOpacityMask(available);
            }
        }

        public DevExpress.Xpf.Layout.Core.TabHeaderLayoutType TabHeaderLayoutType
        {
            get => 
                (DevExpress.Xpf.Layout.Core.TabHeaderLayoutType) base.GetValue(TabHeaderLayoutTypeProperty);
            set => 
                base.SetValue(TabHeaderLayoutTypeProperty, value);
        }

        public bool IsAutoFillHeaders
        {
            get => 
                (bool) base.GetValue(IsAutoFillHeadersProperty);
            set => 
                base.SetValue(IsAutoFillHeadersProperty, value);
        }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            set => 
                base.SetValue(OrientationProperty, value);
        }

        public int ScrollIndex
        {
            get => 
                (int) base.GetValue(ScrollIndexProperty);
            set => 
                base.SetValue(ScrollIndexProperty, value);
        }

        public int SelectedIndex
        {
            get => 
                (int) base.GetValue(SelectedIndexProperty);
            set => 
                base.SetValue(SelectedIndexProperty, value);
        }

        public double ScrollOffset
        {
            get => 
                (double) base.GetValue(ScrollOffsetProperty);
            set => 
                base.SetValue(ScrollOffsetProperty, value);
        }

        public Thickness ClipMargin
        {
            get => 
                (Thickness) base.GetValue(ClipMarginProperty);
            set => 
                base.SetValue(ClipMarginProperty, value);
        }

        public double TransparencySize
        {
            get => 
                (double) base.GetValue(TransparencySizeProperty);
            set => 
                base.SetValue(TransparencySizeProperty, value);
        }

        public bool FixedMultiLineTabHeaders
        {
            get => 
                (bool) base.GetValue(FixedMultiLineTabHeadersProperty);
            set => 
                base.SetValue(FixedMultiLineTabHeadersProperty, value);
        }

        public bool IsHorizontal =>
            this.Orientation == System.Windows.Controls.Orientation.Horizontal;

        public DevExpress.Xpf.Docking.AutoScrollOnOverflow AutoScrollOnOverflow
        {
            get => 
                (DevExpress.Xpf.Docking.AutoScrollOnOverflow) base.GetValue(AutoScrollOnOverflowProperty);
            set => 
                base.SetValue(AutoScrollOnOverflowProperty, value);
        }

        internal bool AllowAnimation { get; set; }

        protected virtual bool StretchItems =>
            false;

        private OrientationHelper _OrientationHelper =>
            OrientationHelper.GetInstance(this.IsHorizontal);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BaseHeadersPanel.<>c <>9 = new BaseHeadersPanel.<>c();

            internal void <.cctor>b__11_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseHeadersPanel) dObj).OnOrientationChanged((Orientation) e.NewValue);
            }

            internal void <.cctor>b__11_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseHeadersPanel) dObj).OnTabHeaderLayoutChanged((TabHeaderLayoutType) e.NewValue);
            }

            internal void <.cctor>b__11_2(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseHeadersPanel) dObj).OnIsAutoFillHeadersChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__11_3(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseHeadersPanel) dObj).OnScrollOffsetChanged((double) e.NewValue);
            }

            internal void <.cctor>b__11_4(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseHeadersPanel) dObj).OnScrollIndexChanged((int) e.NewValue, (int) e.OldValue);
            }

            internal void <.cctor>b__11_5(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseHeadersPanel) dObj).OnSelectedIndexChanged((int) e.NewValue, (int) e.OldValue);
            }

            internal void <.cctor>b__11_6(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseHeadersPanel) dObj).OnFixedMultiLineTabHeadersChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__11_7(BaseHeadersPanel d, BaseLayoutItem oldValue, BaseLayoutItem newValue)
            {
                d.OnLayoutItemChanged(oldValue, newValue);
            }
        }

        private class Interval : Tuple<double, double>
        {
            public Interval(double start, double length) : base(start, length)
            {
            }

            public double Start =>
                base.Item1;

            public double End =>
                this.Start + this.Length;

            public double Length =>
                base.Item2;

            [Obsolete("Use Start property instead")]
            public double Item1 { get; set; }

            [Obsolete("Use Length property instead")]
            public double Item2 { get; set; }
        }

        private class OrientationHelper
        {
            private static readonly BaseHeadersPanel.OrientationHelper Vertical;
            private static readonly BaseHeadersPanel.OrientationHelper Horizontal;

            static OrientationHelper()
            {
                BaseHeadersPanel.OrientationHelper helper1 = new BaseHeadersPanel.OrientationHelper();
                helper1.IsHorz = false;
                Vertical = helper1;
                BaseHeadersPanel.OrientationHelper helper2 = new BaseHeadersPanel.OrientationHelper();
                helper2.IsHorz = true;
                Horizontal = helper2;
            }

            private OrientationHelper()
            {
            }

            public double GetDim(Size size) => 
                this.IsHorz ? size.Height : size.Width;

            public static BaseHeadersPanel.OrientationHelper GetInstance(bool horz) => 
                horz ? Horizontal : Vertical;

            public double GetLength(Rect rect) => 
                TabHeaderHelper.GetLength(this.IsHorz, rect);

            public double GetLength(Size size) => 
                TabHeaderHelper.GetLength(this.IsHorz, size);

            public Point GetPoint(double x, double y) => 
                this.IsHorz ? new Point(x, y) : new Point(y, x);

            public Size GetSize(double length, double dim) => 
                TabHeaderHelper.GetSize(this.IsHorz, length, dim);

            public double GetValue(Point point) => 
                this.IsHorz ? point.X : point.Y;

            public bool IsHorz { get; private set; }
        }
    }
}

