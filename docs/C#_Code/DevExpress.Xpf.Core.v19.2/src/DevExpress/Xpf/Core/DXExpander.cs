namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    public class DXExpander : Decorator
    {
        private static readonly object getExpandCollapseInfo = new object();
        public static readonly DependencyProperty AllowAddingEventProperty = DependencyPropertyManager.Register("AllowAddingEvent", typeof(bool), typeof(DXExpander), new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty DurationProperty = DependencyPropertyManager.Register("Duration", typeof(double), typeof(DXExpander), new FrameworkPropertyMetadata(250.0));
        public static readonly DependencyProperty IsExpandedProperty = DependencyPropertyManager.Register("IsExpanded", typeof(bool), typeof(DXExpander), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(DXExpander.OnIsExpandedChanged)));
        public static readonly DependencyProperty StretchChildProperty = DependencyPropertyManager.Register("StretchChild", typeof(bool), typeof(DXExpander), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(DXExpander.OnStretchChildChanged)));
        public static readonly DependencyProperty HorizontalExpandProperty = DependencyPropertyManager.Register("HorizontalExpand", typeof(HorizontalExpandMode), typeof(DXExpander), new FrameworkPropertyMetadata(HorizontalExpandMode.None, FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty VerticalExpandProperty = DependencyPropertyManager.Register("VerticalExpand", typeof(VerticalExpandMode), typeof(DXExpander), new FrameworkPropertyMetadata(VerticalExpandMode.FromTopToBottom, FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty AnimationProgressProperty = DependencyPropertyManager.Register("AnimationProgress", typeof(double), typeof(DXExpander), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(DXExpander.OnAnimationProgressChanged), new CoerceValueCallback(DXExpander.OnCoerceAnimationProgress)));
        public static readonly DependencyProperty ExpandStoryboardProperty = DependencyPropertyManager.Register("ExpandStoryboard", typeof(Storyboard), typeof(DXExpander), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty CollapseStoryboardProperty = DependencyPropertyManager.Register("CollapseStoryboard", typeof(Storyboard), typeof(DXExpander), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
        internal static readonly DependencyPropertyKey IsRevealedPropertyKey;
        public static readonly DependencyProperty IsRevealedProperty;
        public static readonly DependencyProperty ExpandingProperty;
        public static readonly DependencyProperty CollapsingProperty;
        public static readonly DependencyProperty TracksRevealingProperty;
        public static readonly DependencyProperty AllowTracksRevealingProperty;
        private Storyboard currentAnimation;
        private EventHandlerList events;
        private bool loaded;
        private LayoutCalculator layoutCalculator;

        public event ExpandCollapseInfoEventHandler GetExpandCollapseInfo
        {
            add
            {
                this.Events.AddHandler(getExpandCollapseInfo, value);
            }
            remove
            {
                this.Events.RemoveHandler(getExpandCollapseInfo, value);
            }
        }

        static DXExpander()
        {
            UIElement.ClipToBoundsProperty.OverrideMetadata(typeof(DXExpander), new FrameworkPropertyMetadata(true));
            IsRevealedPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("IsRevealed", typeof(bool), typeof(DXExpander), new FrameworkPropertyMetadata(true));
            IsRevealedProperty = IsRevealedPropertyKey.DependencyProperty;
            TracksRevealingProperty = DependencyPropertyManager.RegisterAttached("TracksRevealing", typeof(bool), typeof(DXExpander), new FrameworkPropertyMetadata(false));
            ExpandingProperty = DependencyPropertyManager.Register("Expanding", typeof(bool), typeof(DXExpander), new FrameworkPropertyMetadata(false));
            CollapsingProperty = DependencyPropertyManager.Register("Collapsing", typeof(bool), typeof(DXExpander), new FrameworkPropertyMetadata(false));
            AllowTracksRevealingProperty = DependencyPropertyManager.RegisterAttached("AllowTracksRevealing", typeof(bool), typeof(DXExpander), new FrameworkPropertyMetadata(true));
            RegisterDataObjectBaseResetEventHandler();
        }

        public DXExpander()
        {
            this.UpdateLayoutCalculator();
            base.Loaded += new RoutedEventHandler(this.Reveal_Loaded);
        }

        protected override Size ArrangeOverride(Size arrangeSize) => 
            this.layoutCalculator.ArrangeOverride(arrangeSize);

        private void BeginAnimation(Storyboard sb)
        {
            this.currentAnimation = sb;
            this.currentAnimation.Begin(this, true);
        }

        public static bool GetAllowTracksRevealing(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (bool) element.GetValue(AllowTracksRevealingProperty);
        }

        internal UIElement GetChild() => 
            this.Child;

        public static bool GetIsRevealed(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (bool) element.GetValue(IsRevealedProperty);
        }

        public static bool GetTracksRevealing(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (bool) element.GetValue(TracksRevealingProperty);
        }

        protected override Size MeasureOverride(Size constraint) => 
            this.layoutCalculator.MeasureOverride(constraint);

        private static void OnAnimationProgressChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((DXExpander) sender).UpdateAnimationProperties((double) e.OldValue);
        }

        private static object OnCoerceAnimationProgress(DependencyObject d, object baseValue)
        {
            double num = (double) baseValue;
            return ((num >= 0.0) ? ((num <= 1.0) ? baseValue : 1.0) : 0.0);
        }

        private static void OnDataObjectBaseReset(object sender, RoutedEventArgs e)
        {
            DXExpander expander = sender as DXExpander;
            if (expander != null)
            {
                expander.SkipToFillCurrentAnimation();
            }
        }

        private static void OnIsExpandedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((DXExpander) sender).SetupAnimation((bool) e.NewValue);
        }

        private static void OnStretchChildChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((DXExpander) sender).UpdateLayoutCalculator();
        }

        protected internal ExpandCollapseInfoEventArgs RaiseGetExpandCollapseInfo(Size size)
        {
            ExpandCollapseInfoEventArgs args1 = new ExpandCollapseInfoEventArgs();
            args1.Expander = this;
            args1.Size = size;
            ExpandCollapseInfoEventArgs e = args1;
            ExpandCollapseInfoEventHandler handler = this.Events[getExpandCollapseInfo] as ExpandCollapseInfoEventHandler;
            if (handler == null)
            {
                return null;
            }
            handler(this, e);
            return e;
        }

        private static void RegisterDataObjectBaseResetEventHandler()
        {
            EventManager.RegisterClassHandler(typeof(DXExpander), DataObjectBase.ResetEvent, new RoutedEventHandler(DXExpander.OnDataObjectBaseReset));
        }

        private void Reveal_Loaded(object sender, RoutedEventArgs e)
        {
            this.loaded = true;
        }

        public static void SetAllowTracksRevealing(DependencyObject element, bool value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(AllowTracksRevealingProperty, value);
        }

        internal static void SetIsRevealed(DependencyObject element, bool value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(IsRevealedPropertyKey, value);
        }

        public static void SetTracksRevealing(DependencyObject element, bool value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(TracksRevealingProperty, value);
        }

        private void SetupAnimation(bool isExpanded)
        {
            if (!this.loaded)
            {
                this.AnimationProgress = isExpanded ? ((double) 1) : ((double) 0);
            }
            else
            {
                double animationProgress = this.AnimationProgress;
                if (isExpanded)
                {
                    animationProgress = 1.0 - animationProgress;
                }
                Storyboard sb = isExpanded ? this.ExpandStoryboard : this.CollapseStoryboard;
                if (sb != null)
                {
                    this.BeginAnimation(sb);
                }
                else
                {
                    DoubleAnimation element = new DoubleAnimation {
                        To = new double?(isExpanded ? 1.0 : 0.0),
                        Duration = TimeSpan.FromMilliseconds(this.Duration * animationProgress),
                        FillBehavior = FillBehavior.HoldEnd
                    };
                    Storyboard storyboard2 = new Storyboard();
                    Storyboard.SetTarget(element, this);
                    Storyboard.SetTargetProperty(element, new PropertyPath(AnimationProgressProperty));
                    storyboard2.Children.Add(element);
                    this.BeginAnimation(storyboard2);
                }
            }
        }

        private void SkipToFillCurrentAnimation()
        {
            if (this.currentAnimation != null)
            {
                this.currentAnimation.SkipToFill(this);
                this.currentAnimation = null;
            }
        }

        private void UpdateAnimationProperties(double oldValue)
        {
            if (this.GetChild() != null)
            {
                this.Collapsing = (0.0 < this.AnimationProgress) && (this.AnimationProgress < oldValue);
                this.Expanding = (oldValue < this.AnimationProgress) && (this.AnimationProgress < 1.0);
            }
        }

        private void UpdateLayoutCalculator()
        {
            this.layoutCalculator = LayoutCalculator.CreateInstance(this, this.StretchChild);
        }

        protected EventHandlerList Events
        {
            get
            {
                this.events ??= new EventHandlerList();
                return this.events;
            }
        }

        [Description("Gets or sets the expand animation storyboard. This is a dependency property.")]
        public Storyboard ExpandStoryboard
        {
            get => 
                (Storyboard) base.GetValue(ExpandStoryboardProperty);
            set => 
                base.SetValue(ExpandStoryboardProperty, value);
        }

        [Description("Gets or sets the collapse animation storyboard. This is a dependency property.")]
        public Storyboard CollapseStoryboard
        {
            get => 
                (Storyboard) base.GetValue(CollapseStoryboardProperty);
            set => 
                base.SetValue(CollapseStoryboardProperty, value);
        }

        [Description("Gets or sets whether the DXExpander is expanded. This is a dependency property.")]
        public bool IsExpanded
        {
            get => 
                (bool) base.GetValue(IsExpandedProperty);
            set => 
                base.SetValue(IsExpandedProperty, value);
        }

        [Description("Gets or sets whether the DXExpander is currently expanding. This is a dependency property.")]
        public bool Expanding
        {
            get => 
                (bool) base.GetValue(ExpandingProperty);
            set => 
                base.SetValue(ExpandingProperty, value);
        }

        [Description("Gets or sets whether the DXExpander is currently collapsing. This is a dependency property.")]
        public bool Collapsing
        {
            get => 
                (bool) base.GetValue(CollapsingProperty);
            set => 
                base.SetValue(CollapsingProperty, value);
        }

        [Description("Gets or sets whether the DXExpander's content is expanded and collapsed with a stretching animation effect. This is a dependency property.")]
        public bool StretchChild
        {
            get => 
                (bool) base.GetValue(StretchChildProperty);
            set => 
                base.SetValue(StretchChildProperty, value);
        }

        [Description("Gets or sets the duration of the expand/collapse animation. This is a dependency property.")]
        public double Duration
        {
            get => 
                (double) base.GetValue(DurationProperty);
            set => 
                base.SetValue(DurationProperty, value);
        }

        [Description("Gets or sets the horizontal animation style. This is a dependency property.")]
        public HorizontalExpandMode HorizontalExpand
        {
            get => 
                (HorizontalExpandMode) base.GetValue(HorizontalExpandProperty);
            set => 
                base.SetValue(HorizontalExpandProperty, value);
        }

        [Description("Gets or sets the vertical animation style. This is a dependency property.")]
        public VerticalExpandMode VerticalExpand
        {
            get => 
                (VerticalExpandMode) base.GetValue(VerticalExpandProperty);
            set => 
                base.SetValue(VerticalExpandProperty, value);
        }

        [Description("Gets or sets the progress of animation play. This is a dependency property.")]
        public double AnimationProgress
        {
            get => 
                (double) base.GetValue(AnimationProgressProperty);
            set => 
                base.SetValue(AnimationProgressProperty, value);
        }

        [Description("For internal use.")]
        public bool AllowAddingEvent
        {
            get => 
                (bool) base.GetValue(AllowAddingEventProperty);
            set => 
                base.SetValue(AllowAddingEventProperty, value);
        }

        internal Size ChildVisibleSize =>
            ExpandHelper.GetVisibleSize(this.GetChild());

        private enum AlignmentMode
        {
            Near,
            Center,
            Far,
            Stretch
        }

        private enum ExpandMode
        {
            None,
            FromNearToFar,
            FromFarToNear,
            FromCenterToEdge
        }

        private class LayoutCalculator
        {
            private double lastAnimationProgress;

            internal LayoutCalculator(DevExpress.Xpf.Core.DXExpander expander)
            {
                this.DXExpander = expander;
            }

            public Size ArrangeOverride(Size arrangeSize)
            {
                FrameworkElement child = this.DXExpander.GetChild() as FrameworkElement;
                if (child == null)
                {
                    return new Size();
                }
                Size currentSize = this.GetCurrentSize();
                double width = (this.DXExpander.HorizontalAlignment == HorizontalAlignment.Stretch) ? arrangeSize.Width : Math.Max(child.MinWidth, currentSize.Width);
                double height = (this.DXExpander.VerticalAlignment == VerticalAlignment.Stretch) ? arrangeSize.Height : Math.Max(child.MinHeight, currentSize.Height);
                Size size = new Size(width, height);
                child.Arrange(new Rect(this.CalculatePosition(size, arrangeSize, this.DXExpander.AnimationProgress, (DevExpress.Xpf.Core.DXExpander.ExpandMode) this.DXExpander.HorizontalExpand, SizeHelperBase.GetDefineSizeHelper(Orientation.Horizontal)), this.CalculatePosition(size, arrangeSize, this.DXExpander.AnimationProgress, (DevExpress.Xpf.Core.DXExpander.ExpandMode) this.DXExpander.VerticalExpand, SizeHelperBase.GetDefineSizeHelper(Orientation.Vertical)), width, height));
                double num5 = this.CalculateSize(arrangeSize, (this.DXExpander.HorizontalAlignment == HorizontalAlignment.Stretch) ? arrangeSize : this.GetExpandedSize(), this.DXExpander.AnimationProgress, (DevExpress.Xpf.Core.DXExpander.ExpandMode) this.DXExpander.HorizontalExpand, (DevExpress.Xpf.Core.DXExpander.AlignmentMode) this.DXExpander.HorizontalAlignment, SizeHelperBase.GetDefineSizeHelper(Orientation.Horizontal));
                double num6 = this.CalculateSize(arrangeSize, (this.DXExpander.VerticalAlignment == VerticalAlignment.Stretch) ? arrangeSize : this.GetExpandedSize(), this.DXExpander.AnimationProgress, (DevExpress.Xpf.Core.DXExpander.ExpandMode) this.DXExpander.VerticalExpand, (DevExpress.Xpf.Core.DXExpander.AlignmentMode) this.DXExpander.VerticalAlignment, SizeHelperBase.GetDefineSizeHelper(Orientation.Vertical));
                this.UpdateIsRevealed(num6);
                if (this.DXExpander.HorizontalAlignment == HorizontalAlignment.Stretch)
                {
                    num5 = arrangeSize.Width;
                }
                if (this.DXExpander.VerticalAlignment == VerticalAlignment.Stretch)
                {
                    num6 = arrangeSize.Height;
                }
                return new Size(Math.Max(this.DXExpander.MinWidth, num5), Math.Max(this.DXExpander.MinHeight, num6));
            }

            private double CalculatePosition(Size size, Size arrangeSize, double percent, DevExpress.Xpf.Core.DXExpander.ExpandMode expander, SizeHelperBase sizeHelper)
            {
                double width = (this.DXExpander.HorizontalAlignment == HorizontalAlignment.Stretch) ? arrangeSize.Width : this.DXExpander.DesiredSize.Width;
                Size size2 = new Size(width, (this.DXExpander.VerticalAlignment == VerticalAlignment.Stretch) ? arrangeSize.Height : this.DXExpander.DesiredSize.Height);
                double num3 = sizeHelper.GetDefineSize(size2) - sizeHelper.GetDefineSize(size);
                return ((expander != DevExpress.Xpf.Core.DXExpander.ExpandMode.FromFarToNear) ? ((expander != DevExpress.Xpf.Core.DXExpander.ExpandMode.FromCenterToEdge) ? 0.0 : (num3 * 0.5)) : num3);
            }

            protected virtual double CalculateSize(Size constraintSize, Size originalSize, double percent, DevExpress.Xpf.Core.DXExpander.ExpandMode expand, DevExpress.Xpf.Core.DXExpander.AlignmentMode alignment, SizeHelperBase sizeHelper)
            {
                double defineSize = sizeHelper.GetDefineSize(originalSize);
                Size size = new Size(this.DXExpander.MinWidth, this.DXExpander.MinHeight);
                Size size2 = new Size(this.DXExpander.MaxWidth, this.DXExpander.MaxHeight);
                if (expand == DevExpress.Xpf.Core.DXExpander.ExpandMode.None)
                {
                    return (((alignment != DevExpress.Xpf.Core.DXExpander.AlignmentMode.Stretch) || double.IsInfinity(sizeHelper.GetDefineSize(constraintSize))) ? defineSize : sizeHelper.GetDefineSize(constraintSize));
                }
                double d = sizeHelper.GetDefineSize(this.DXExpander.ChildVisibleSize);
                if (!double.IsNaN(d))
                {
                    defineSize = d;
                }
                return this.GetSize(sizeHelper.GetDefineSize(size), defineSize, sizeHelper.GetDefineSize(size2), percent);
            }

            public static DevExpress.Xpf.Core.DXExpander.LayoutCalculator CreateInstance(DevExpress.Xpf.Core.DXExpander expander, bool stretch) => 
                stretch ? new DevExpress.Xpf.Core.DXExpander.StretchLayoutCalculator(expander) : new DevExpress.Xpf.Core.DXExpander.LayoutCalculator(expander);

            protected virtual Size GetCurrentSize() => 
                this.DXExpander.GetChild().DesiredSize;

            protected virtual Size GetExpandedSize() => 
                this.DXExpander.GetChild().DesiredSize;

            protected double GetSize(double minWidth, double width, double maxWidth, double percent)
            {
                if (!double.IsNaN(maxWidth) && !double.IsInfinity(maxWidth))
                {
                    width = maxWidth;
                }
                double d = minWidth + ((width - minWidth) * percent);
                return (double.IsNaN(d) ? 0.0 : d);
            }

            protected virtual void MeasureChild(UIElement child, Size constraint)
            {
                child.Measure(constraint);
            }

            public Size MeasureOverride(Size constraint)
            {
                UIElement child = this.DXExpander.GetChild();
                if (child == null)
                {
                    return new Size();
                }
                this.MeasureChild(child, constraint);
                Size expandedSize = this.GetExpandedSize();
                double d = this.CalculateSize(constraint, expandedSize, this.DXExpander.AnimationProgress, (DevExpress.Xpf.Core.DXExpander.ExpandMode) this.DXExpander.HorizontalExpand, (DevExpress.Xpf.Core.DXExpander.AlignmentMode) this.DXExpander.HorizontalAlignment, HorizontalSizeHelper.Instance);
                double num2 = this.CalculateSize(constraint, expandedSize, this.DXExpander.AnimationProgress, (DevExpress.Xpf.Core.DXExpander.ExpandMode) this.DXExpander.VerticalExpand, (DevExpress.Xpf.Core.DXExpander.AlignmentMode) this.DXExpander.VerticalAlignment, VerticalSizeHelper.Instance);
                if (double.IsInfinity(d) || double.IsNaN(d))
                {
                    d = 0.0;
                }
                if (double.IsInfinity(num2) || double.IsNaN(num2))
                {
                    num2 = 0.0;
                }
                return new Size(Math.Max(this.DXExpander.MinWidth, d), Math.Max(this.DXExpander.MinHeight, num2));
            }

            private void UpdateIsRevealed(double height)
            {
                if ((bool) this.DXExpander.GetValue(DevExpress.Xpf.Core.DXExpander.AllowTracksRevealingProperty))
                {
                    if (this.DXExpander.AnimationProgress >= this.lastAnimationProgress)
                    {
                        VisualTreeEnumerator enumerator = new VisualTreeEnumerator(this.DXExpander);
                        while (enumerator.MoveNext())
                        {
                            if (!DevExpress.Xpf.Core.DXExpander.GetTracksRevealing(enumerator.Current))
                            {
                                continue;
                            }
                            Rect relativeElementRect = LayoutHelper.GetRelativeElementRect((FrameworkElement) enumerator.Current, this.DXExpander);
                            DevExpress.Xpf.Core.DXExpander.SetIsRevealed(enumerator.Current, (relativeElementRect.Top < height) || (this.DXExpander.AnimationProgress == 1.0));
                        }
                    }
                    this.lastAnimationProgress = this.DXExpander.AnimationProgress;
                }
            }

            protected DevExpress.Xpf.Core.DXExpander DXExpander { get; set; }
        }

        private class StretchLayoutCalculator : DXExpander.LayoutCalculator
        {
            private Size expanderSize;

            internal StretchLayoutCalculator(DXExpander expander) : base(expander)
            {
            }

            protected override double CalculateSize(Size constraintSize, Size originalSize, double percent, DXExpander.ExpandMode expander, DXExpander.AlignmentMode alignment, SizeHelperBase sizeHelper)
            {
                Size size = new Size(base.DXExpander.MinWidth, base.DXExpander.MinHeight);
                Size size2 = new Size(base.DXExpander.MaxWidth, base.DXExpander.MaxHeight);
                ExpandCollapseInfoEventArgs args = base.DXExpander.RaiseGetExpandCollapseInfo(originalSize);
                if (args != null)
                {
                    originalSize = args.Size;
                }
                return ((expander == DXExpander.ExpandMode.None) ? ((alignment == DXExpander.AlignmentMode.Stretch) ? sizeHelper.GetDefineSize(constraintSize) : sizeHelper.GetDefineSize(originalSize)) : base.GetSize(sizeHelper.GetDefineSize(size), sizeHelper.GetDefineSize(originalSize), sizeHelper.GetDefineSize(size2), percent));
            }

            protected override Size GetCurrentSize() => 
                base.DXExpander.DesiredSize;

            protected override Size GetExpandedSize() => 
                this.expanderSize;

            private double GetFinalSize(Size size1, Size size2, SizeHelperBase sizeHelper) => 
                double.IsInfinity(sizeHelper.GetDefineSize(size1)) ? sizeHelper.GetDefineSize(size2) : sizeHelper.GetDefineSize(size1);

            protected override void MeasureChild(UIElement child, Size constraint)
            {
                Size size = new Size(base.DXExpander.MaxWidth, base.DXExpander.MaxHeight);
                Size size2 = new Size(this.GetFinalSize(constraint, size, HorizontalSizeHelper.Instance), this.GetFinalSize(constraint, size, VerticalSizeHelper.Instance));
                double width = this.CalculateSize(constraint, (base.DXExpander.HorizontalExpand == HorizontalExpandMode.None) ? constraint : size2, base.DXExpander.AnimationProgress, (DXExpander.ExpandMode) base.DXExpander.HorizontalExpand, (DXExpander.AlignmentMode) base.DXExpander.HorizontalAlignment, SizeHelperBase.GetDefineSizeHelper(Orientation.Horizontal));
                child.Measure(new Size(width, this.CalculateSize(constraint, (base.DXExpander.VerticalExpand == VerticalExpandMode.None) ? constraint : size2, base.DXExpander.AnimationProgress, (DXExpander.ExpandMode) base.DXExpander.VerticalExpand, (DXExpander.AlignmentMode) base.DXExpander.VerticalAlignment, SizeHelperBase.GetDefineSizeHelper(Orientation.Vertical))));
                this.expanderSize = new Size(this.GetFinalSize(constraint, child.DesiredSize, HorizontalSizeHelper.Instance), this.GetFinalSize(constraint, child.DesiredSize, VerticalSizeHelper.Instance));
            }
        }
    }
}

