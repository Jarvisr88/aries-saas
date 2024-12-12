namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class GradientMultiSliderThumb : Control
    {
        private const double RemoveThumbHeight = 50.0;
        protected static readonly DependencyPropertyKey ActualOffsetPropertyKey;
        public static readonly DependencyProperty ActualOffsetProperty;
        public static readonly DependencyProperty OffsetProperty;
        public static readonly DependencyProperty ColorProperty;
        public static readonly DependencyProperty IsSelectedProperty;
        public static readonly RoutedEvent ThumbPositionChangedEvent;
        public static readonly RoutedEvent ThumbColorChangedEvent;
        private GradientMultiSlider ownerSlider;

        public event RoutedEventHandler ThumbColorChanged
        {
            add
            {
                base.AddHandler(ThumbColorChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(ThumbColorChangedEvent, value);
            }
        }

        public event RoutedEventHandler ThumbPositionChanged
        {
            add
            {
                base.AddHandler(ThumbPositionChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(ThumbPositionChangedEvent, value);
            }
        }

        static GradientMultiSliderThumb()
        {
            Type ownerType = typeof(GradientMultiSliderThumb);
            OffsetProperty = DependencyPropertyManager.Register("Offset", typeof(double), ownerType, new PropertyMetadata(0.0, (obj, args) => ((GradientMultiSliderThumb) obj).OnOffsetChanged((double) args.NewValue)));
            ColorProperty = DependencyPropertyManager.Register("Color", typeof(System.Windows.Media.Color), ownerType, new PropertyMetadata(Colors.Black, (obj, args) => ((GradientMultiSliderThumb) obj).OnColorChanged((System.Windows.Media.Color) args.NewValue)));
            ActualOffsetPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualOffset", typeof(double), ownerType, new PropertyMetadata(0.0, (obj, args) => ((GradientMultiSliderThumb) obj).OnActualOffsetChanged((double) args.NewValue)));
            ActualOffsetProperty = ActualOffsetPropertyKey.DependencyProperty;
            IsSelectedProperty = DependencyPropertyManager.Register("IsSelected", typeof(bool), ownerType, new PropertyMetadata(false, (obj, args) => ((GradientMultiSliderThumb) obj).OnSelectedChanged((bool) args.NewValue)));
            ThumbPositionChangedEvent = EventManager.RegisterRoutedEvent("ThumbPositionChangedEvent", RoutingStrategy.Direct, typeof(RoutedEventArgs), ownerType);
            ThumbColorChangedEvent = EventManager.RegisterRoutedEvent("ThumbColorChangedEvent", RoutingStrategy.Direct, typeof(RoutedEventArgs), ownerType);
        }

        public GradientMultiSliderThumb()
        {
            this.SetDefaultStyleKey(typeof(GradientMultiSliderThumb));
            this.OffsetLocker = new Locker();
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            this.IsDragging = false;
        }

        private double CalcActualOffset(double newValue) => 
            (this.SliderWidth * newValue) - (this.ThumbWidth / 2.0);

        private double CalcOffset(double newValue) => 
            !this.SliderWidth.AreClose(0.0) ? ((newValue + (this.ThumbWidth / 2.0)) / this.SliderWidth) : 0.0;

        private double GetThumbWidth()
        {
            FrameworkElement rootElement = LayoutHelper.FindElementByType(this, typeof(Canvas));
            if (rootElement == null)
            {
                return base.ActualWidth;
            }
            Func<UIElement, double> selector = <>c.<>9__60_0;
            if (<>c.<>9__60_0 == null)
            {
                Func<UIElement, double> local1 = <>c.<>9__60_0;
                selector = <>c.<>9__60_0 = i => i.DesiredSize.Width;
            }
            return rootElement.VisualChildren(false).OfType<UIElement>().Max<UIElement>(selector);
        }

        protected virtual void OnActualOffsetChanged(double newValue)
        {
            this.OffsetLocker.DoLockedActionIfNotLocked(() => this.Offset = this.CalcOffset(newValue));
        }

        protected virtual void OnColorChanged(System.Windows.Media.Color newValue)
        {
            this.RaiseThumbColorChangedEvent();
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.UpdateActualOffset();
        }

        protected virtual void OnOffsetChanged(double newValue)
        {
            this.UpdateActualOffset();
            this.RaiseThumbPositionChangedEvent();
        }

        protected virtual void OnOwnerSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdateActualOffset();
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);
            if (this.OwnerSlider != null)
            {
                this.OwnerSlider.SelectThumb(this);
            }
            this.IsDragging = true;
            base.CaptureMouse();
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);
            this.IsDragging = false;
            base.ReleaseMouseCapture();
            if (this.IgnoreThumb)
            {
                this.OwnerSlider.RemoveThumb(this);
            }
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);
            if (this.IsDragging)
            {
                double y = e.GetPosition(this.OwnerSlider.GradientRectangle).Y;
                if (y.LessThanOrClose(50.0) && this.IgnoreThumb)
                {
                    base.Opacity = 1.0;
                    this.IgnoreThumb = false;
                    this.OwnerSlider.UpdateBrush(false);
                }
                else if (y.GreaterThan(50.0) && (!this.IgnoreThumb && (this.OwnerSlider.Thumbs.Count > 2)))
                {
                    base.Opacity = 0.0;
                    this.IgnoreThumb = true;
                    this.OwnerSlider.UpdateBrush(false);
                }
                double num2 = Math.Max(0.0, Math.Min(e.GetPosition(this.OwnerSlider.GradientRectangle).X, this.SliderWidth));
                this.ActualOffset = num2 - (this.ThumbWidth / 2.0);
            }
        }

        protected virtual void OnSelectedChanged(bool newValue)
        {
            this.UpdateActualOffset();
        }

        private void RaiseThumbColorChangedEvent()
        {
            base.RaiseEvent(new RoutedEventArgs(ThumbColorChangedEvent));
        }

        private void RaiseThumbPositionChangedEvent()
        {
            base.RaiseEvent(new RoutedEventArgs(ThumbPositionChangedEvent));
        }

        private void UpdateActualOffset()
        {
            this.OffsetLocker.DoLockedActionIfNotLocked(() => this.ActualOffset = this.CalcActualOffset(this.Offset));
        }

        public double ActualOffset
        {
            get => 
                (double) base.GetValue(ActualOffsetProperty);
            protected set => 
                base.SetValue(ActualOffsetPropertyKey, value);
        }

        public double Offset
        {
            get => 
                (double) base.GetValue(OffsetProperty);
            set => 
                base.SetValue(OffsetProperty, value);
        }

        public System.Windows.Media.Color Color
        {
            get => 
                (System.Windows.Media.Color) base.GetValue(ColorProperty);
            set => 
                base.SetValue(ColorProperty, value);
        }

        public bool IsSelected
        {
            get => 
                (bool) base.GetValue(IsSelectedProperty);
            set => 
                base.SetValue(IsSelectedProperty, value);
        }

        public GradientMultiSlider OwnerSlider
        {
            get => 
                this.ownerSlider;
            set
            {
                this.ownerSlider.Do<GradientMultiSlider>(delegate (GradientMultiSlider x) {
                    x.SizeChanged -= new SizeChangedEventHandler(this.OnOwnerSizeChanged);
                });
                this.ownerSlider = value;
                this.ownerSlider.Do<GradientMultiSlider>(delegate (GradientMultiSlider x) {
                    x.SizeChanged += new SizeChangedEventHandler(this.OnOwnerSizeChanged);
                });
            }
        }

        public bool IgnoreThumb { get; set; }

        private Locker OffsetLocker { get; set; }

        private bool IsDragging { get; set; }

        private double SliderWidth
        {
            get
            {
                Func<GradientMultiSlider, Rectangle> evaluator = <>c.<>9__44_0;
                if (<>c.<>9__44_0 == null)
                {
                    Func<GradientMultiSlider, Rectangle> local1 = <>c.<>9__44_0;
                    evaluator = <>c.<>9__44_0 = x => x.GradientRectangle;
                }
                Func<Rectangle, double> func2 = <>c.<>9__44_1;
                if (<>c.<>9__44_1 == null)
                {
                    Func<Rectangle, double> local2 = <>c.<>9__44_1;
                    func2 = <>c.<>9__44_1 = x => x.ActualWidth;
                }
                return this.OwnerSlider.With<GradientMultiSlider, Rectangle>(evaluator).Return<Rectangle, double>(func2, (<>c.<>9__44_2 ??= () => 0.0));
            }
        }

        private double ThumbWidth =>
            this.GetThumbWidth();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GradientMultiSliderThumb.<>c <>9 = new GradientMultiSliderThumb.<>c();
            public static Func<GradientMultiSlider, Rectangle> <>9__44_0;
            public static Func<Rectangle, double> <>9__44_1;
            public static Func<double> <>9__44_2;
            public static Func<UIElement, double> <>9__60_0;

            internal void <.cctor>b__8_0(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((GradientMultiSliderThumb) obj).OnOffsetChanged((double) args.NewValue);
            }

            internal void <.cctor>b__8_1(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((GradientMultiSliderThumb) obj).OnColorChanged((Color) args.NewValue);
            }

            internal void <.cctor>b__8_2(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((GradientMultiSliderThumb) obj).OnActualOffsetChanged((double) args.NewValue);
            }

            internal void <.cctor>b__8_3(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((GradientMultiSliderThumb) obj).OnSelectedChanged((bool) args.NewValue);
            }

            internal Rectangle <get_SliderWidth>b__44_0(GradientMultiSlider x) => 
                x.GradientRectangle;

            internal double <get_SliderWidth>b__44_1(Rectangle x) => 
                x.ActualWidth;

            internal double <get_SliderWidth>b__44_2() => 
                0.0;

            internal double <GetThumbWidth>b__60_0(UIElement i) => 
                i.DesiredSize.Width;
        }
    }
}

