namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media.Animation;
    using System.Windows.Threading;

    public class SmoothScroller : Decorator, IManipulationClientEx, IManipulationClient
    {
        private static readonly TimeSpan defaultUpdateInterval = new TimeSpan(0, 0, 0, 0, 200);
        public static readonly RoutedCommand ScrollUpCommand = new RoutedCommand("ScrollUp", typeof(SmoothScroller));
        public static readonly RoutedCommand ScrollDownCommand = new RoutedCommand("ScrollDown", typeof(SmoothScroller));
        public static readonly DependencyProperty AllowScrollUpProperty;
        public static readonly DependencyProperty AllowScrollDownProperty;
        public static readonly DependencyProperty ScrollSpeedProperty;
        public static readonly DependencyProperty AccelerationProperty;
        public static readonly DependencyProperty DecelerationProperty;
        public static readonly DependencyProperty ChildOffsetProperty;
        public static readonly DependencyProperty TopBottomSpaceProperty;
        public static readonly DependencyProperty OrientationProperty;
        private DispatcherTimer timer;
        private ScrollingAction action = ScrollingAction.NotScrolling;
        private Storyboard storyBoard;
        private DevExpress.Xpf.Core.ManipulationHelper manipulationHelper;

        static SmoothScroller()
        {
            CommandManager.RegisterClassCommandBinding(typeof(SmoothScroller), new CommandBinding(ScrollUpCommand, new ExecutedRoutedEventHandler(SmoothScroller.OnScrollUp), new CanExecuteRoutedEventHandler(SmoothScroller.OnCanExecuteScrollUp)));
            CommandManager.RegisterClassCommandBinding(typeof(SmoothScroller), new CommandBinding(ScrollDownCommand, new ExecutedRoutedEventHandler(SmoothScroller.OnScrollDown), new CanExecuteRoutedEventHandler(SmoothScroller.OnCanExecuteScrollDown)));
            UIElement.ClipToBoundsProperty.OverrideMetadata(typeof(SmoothScroller), new FrameworkPropertyMetadata(true));
            AllowScrollUpProperty = DependencyPropertyManager.Register("AllowScrollUp", typeof(bool), typeof(SmoothScroller), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(SmoothScroller.OnAllowScrollUpChanged)));
            AllowScrollDownProperty = DependencyPropertyManager.Register("AllowScrollDown", typeof(bool), typeof(SmoothScroller), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(SmoothScroller.OnAllowScrollDownChanged)));
            ScrollSpeedProperty = DependencyPropertyManager.Register("ScrollSpeed", typeof(double), typeof(SmoothScroller), new FrameworkPropertyMetadata(200.0));
            AccelerationProperty = DependencyPropertyManager.Register("Acceleration", typeof(double), typeof(SmoothScroller), new FrameworkPropertyMetadata(200.0));
            DecelerationProperty = DependencyPropertyManager.Register("Deceleration", typeof(double), typeof(SmoothScroller), new FrameworkPropertyMetadata(200.0));
            ChildOffsetProperty = DependencyPropertyManager.Register("ChildOffset", typeof(double), typeof(SmoothScroller), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsArrange, new PropertyChangedCallback(SmoothScroller.OnChildOffsetChanged), new CoerceValueCallback(SmoothScroller.CoerceChildOffset)));
            TopBottomSpaceProperty = DependencyPropertyManager.Register("TopBottomSpace", typeof(double), typeof(SmoothScroller), new FrameworkPropertyMetadata(0.0, (d, e) => ((SmoothScroller) d).OnTopBottomSpaceChanged()));
            OrientationProperty = DependencyPropertyManager.Register("Orientation", typeof(System.Windows.Controls.Orientation), typeof(SmoothScroller), new FrameworkPropertyMetadata(System.Windows.Controls.Orientation.Vertical, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(SmoothScroller.OnOrientationChanged)));
        }

        public SmoothScroller()
        {
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
        }

        private bool AreCloseToCurrentContentOffset(double value) => 
            Math.Abs((double) (this.ChildOffset - value)) < 1.0;

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            if (this.Child != null)
            {
                Size size = this.sizeHelper.CreateSize(Math.Max(this.sizeHelper.GetDefineSize(this.Child.DesiredSize), this.sizeHelper.GetDefineSize(arrangeSize) - this.TopBottomSpace), this.sizeHelper.GetSecondarySize(arrangeSize));
                this.Child.Arrange(new Rect(this.sizeHelper.CreatePoint(-this.ChildOffset, 0.0), size));
            }
            return arrangeSize;
        }

        private double CoerceChildOffset(double newValue)
        {
            double num = (this.sizeHelper.GetDefineSize(this.Child.DesiredSize) + this.TopBottomSpace) - ((this.Orientation == System.Windows.Controls.Orientation.Vertical) ? base.ActualHeight : base.ActualWidth);
            return (((newValue < 0.0) || (num < 0.0)) ? 0.0 : ((newValue <= num) ? newValue : num));
        }

        private static object CoerceChildOffset(DependencyObject d, object value) => 
            ((SmoothScroller) d).CoerceChildOffset((double) value);

        IInputElement IManipulationClient.GetManipulationContainer() => 
            this;

        Vector IManipulationClient.GetMaxScrollValue() => 
            new Vector(0.0, this.ContentHeight - this.sizeHelper.GetDefineSize(base.DesiredSize));

        Vector IManipulationClient.GetMinScrollValue() => 
            new Vector(0.0, 0.0);

        Vector IManipulationClient.GetScrollValue()
        {
            Point point = new Point(0.0, this.ChildOffset);
            return new Vector(this.sizeHelper.GetSecondaryPoint(point), this.sizeHelper.GetDefinePoint(point));
        }

        void IManipulationClient.Scroll(Vector scrollValue)
        {
            this.ChildOffset = this.sizeHelper.GetDefinePoint(new Point(scrollValue.X, scrollValue.Y));
        }

        Vector IManipulationClientEx.GetMinScrollDelta() => 
            this.GetMinScrollDeltaOverride();

        private double GetCurrentAnimationSpeed(double currentValue, double animationSpeed)
        {
            double totalSeconds = this.Animation.Duration.TimeSpan.TotalSeconds;
            double accelerationRatio = 1.0;
            double decelerationRatio = 1.0;
            accelerationRatio = this.Animation.AccelerationRatio;
            decelerationRatio = this.Animation.DecelerationRatio;
            double num4 = ((accelerationRatio * animationSpeed) * totalSeconds) / 2.0;
            double num5 = (((1.0 - accelerationRatio) - decelerationRatio) * animationSpeed) * totalSeconds;
            double num6 = ((decelerationRatio * animationSpeed) * totalSeconds) / 2.0;
            double num7 = Math.Abs((double) (currentValue - this.Animation.From.Value));
            return ((num7 >= num4) ? ((num7 >= (num4 + num5)) ? (animationSpeed * Math.Sqrt(((num7 - num4) - num5) / num6)) : animationSpeed) : (animationSpeed * Math.Sqrt(num7 / num4)));
        }

        protected virtual Vector GetMinScrollDeltaOverride() => 
            new Vector(0.0, 0.0);

        private void Initialize()
        {
            base.ClearValue(AllowScrollDownProperty);
            base.ClearValue(AllowScrollUpProperty);
            Binding binding = new Binding(ChildOffsetProperty.GetName());
            binding.Source = this;
            binding.Converter = new ContentOffsetToAllowScrollUpConverter();
            base.SetBinding(AllowScrollUpProperty, binding);
            this.UpdateAllowScrollDownBinding();
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if (this.Child == null)
            {
                return new Size();
            }
            this.Child.Measure(this.sizeHelper.CreateSize(double.PositiveInfinity, this.sizeHelper.GetSecondarySize(constraint)));
            if (this.ContentHeight < this.sizeHelper.GetDefineSize(constraint))
            {
                this.ChildOffset = 0.0;
            }
            else if ((this.ContentHeight - this.ChildOffset) < this.sizeHelper.GetDefineSize(constraint))
            {
                this.ChildOffset = this.ContentHeight - this.sizeHelper.GetDefineSize(constraint);
            }
            if (constraint.Height == double.PositiveInfinity)
            {
                constraint.Height = this.Child.DesiredSize.Height;
            }
            if (constraint.Width == double.PositiveInfinity)
            {
                constraint.Width = this.Child.DesiredSize.Width;
            }
            return constraint;
        }

        public static void OnAllowScrollDownChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CommandManager.InvalidateRequerySuggested();
            ((SmoothScroller) sender).UpdateAllowScrolling();
        }

        public static void OnAllowScrollUpChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CommandManager.InvalidateRequerySuggested();
            ((SmoothScroller) sender).UpdateAllowScrolling();
        }

        public static void OnCanExecuteScrollDown(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ((SmoothScroller) sender).AllowScrollDown;
        }

        public static void OnCanExecuteScrollUp(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ((SmoothScroller) sender).AllowScrollUp;
        }

        private void OnChildOffsetChanged()
        {
            this.UpdateAllowScrollDown();
        }

        private static void OnChildOffsetChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((SmoothScroller) sender).OnChildOffsetChanged();
        }

        private void OnIntActualHeightChanged()
        {
            this.UpdateAllowScrollDown();
        }

        private void OnIntActualWidthChanged()
        {
            this.UpdateAllowScrollDown();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Initialize();
        }

        protected override void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
            base.OnManipulationDelta(e);
            this.ManipulationHelper.OnManipulationDelta(e);
            double num = (this.Orientation == System.Windows.Controls.Orientation.Vertical) ? e.CumulativeManipulation.Translation.Y : e.CumulativeManipulation.Translation.X;
            this.ShouldStartManipulationInertia |= num > 0.0;
        }

        protected override void OnManipulationInertiaStarting(ManipulationInertiaStartingEventArgs e)
        {
            if (!this.ShouldStartManipulationInertia)
            {
                e.Cancel();
            }
            else
            {
                base.OnManipulationInertiaStarting(e);
                this.ManipulationHelper.OnManipulationInertiaStarting(e);
            }
        }

        protected override void OnManipulationStarting(ManipulationStartingEventArgs e)
        {
            base.OnManipulationStarting(e);
            this.ManipulationHelper.OnManipulationStarting(e);
            this.ShouldStartManipulationInertia = false;
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            if (e.Delta > 0)
            {
                this.ScrollUp();
            }
            else
            {
                this.ScrollDown();
            }
        }

        private static void OnOrientationChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((SmoothScroller) sender).UpdateAllowScrollDownBinding();
        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            base.OnPreviewMouseWheel(e);
        }

        public static void OnScrollDown(object sender, ExecutedRoutedEventArgs e)
        {
            ((SmoothScroller) sender).PerformScrollDownCommand();
        }

        public static void OnScrollUp(object sender, ExecutedRoutedEventArgs e)
        {
            ((SmoothScroller) sender).PerformScrollUpCommand();
        }

        private void OnStoryboardCompleted(object sender, EventArgs e)
        {
            this.StopAnimation();
        }

        private void OnTopBottomSpaceChanged()
        {
            this.UpdateAllowScrollDown();
        }

        protected virtual void PerformScrollDownCommand()
        {
            if (this.action == ScrollingAction.ScrollUp)
            {
                this.StopTimer();
                this.StopScrollUp();
            }
            if (this.action != ScrollingAction.ScrollDown)
            {
                this.StartScrollDown();
                this.action = ScrollingAction.ScrollDown;
            }
            if (this.timer != null)
            {
                this.StopTimer();
            }
            this.StartScrollDownTimer();
        }

        protected virtual void PerformScrollUpCommand()
        {
            if (this.action == ScrollingAction.ScrollDown)
            {
                this.StopTimer();
                this.StopScrollDown();
            }
            if (this.action != ScrollingAction.ScrollUp)
            {
                this.StartScrollUp();
                this.action = ScrollingAction.ScrollUp;
            }
            if (this.timer != null)
            {
                this.StopTimer();
            }
            this.StartScrollUpTimer();
        }

        private void ScrollDown()
        {
            this.StopAnimation();
            this.ChildOffset += 30.0;
            if (this.ChildOffset > Math.Max((double) 0.0, (double) (this.ContentHeight - this.actualHeight)))
            {
                this.ChildOffset = Math.Max((double) 0.0, (double) (this.ContentHeight - this.actualHeight));
            }
        }

        private void ScrollUp()
        {
            this.StopAnimation();
            this.ChildOffset -= 30.0;
            this.ChildOffset = Math.Max(this.ChildOffset, 0.0);
        }

        private void StartAnimation(double to, double duration, double accelerationRatio, double decelerationRatio)
        {
            this.storyBoard = new Storyboard();
            this.storyBoard.Completed += new EventHandler(this.OnStoryboardCompleted);
            Storyboard.SetTargetProperty(this.storyBoard, new PropertyPath(ChildOffsetProperty.GetName(), new object[0]));
            DoubleAnimation animation = new DoubleAnimation {
                From = new double?(this.ChildOffset),
                To = new double?(to),
                Duration = new Duration(TimeSpan.FromSeconds(duration)),
                AccelerationRatio = accelerationRatio,
                DecelerationRatio = decelerationRatio
            };
            this.storyBoard.Children.Add(animation);
            this.storyBoard.Begin(this, HandoffBehavior.Compose, true);
        }

        private void StartScroll(double to, double distance)
        {
            this.StopAnimation();
            if (distance > 0.0)
            {
                double num;
                double num4;
                double num5;
                double num2 = (this.ScrollSpeed * this.ScrollSpeed) / (2.0 * this.Acceleration);
                double num3 = (this.ScrollSpeed * this.ScrollSpeed) / (2.0 * this.Deceleration);
                if (distance > (num2 + num3))
                {
                    num4 = this.ScrollSpeed / this.Acceleration;
                    num5 = this.ScrollSpeed / this.Deceleration;
                    num = (num4 + (((distance - num2) - num3) / this.ScrollSpeed)) + num5;
                }
                else
                {
                    num4 = Math.Sqrt(((2.0 * distance) * this.Deceleration) / ((this.Acceleration * this.Deceleration) + (this.Acceleration * this.Acceleration)));
                    num5 = Math.Sqrt(((2.0 * distance) * this.Acceleration) / ((this.Acceleration * this.Deceleration) + (this.Deceleration * this.Deceleration)));
                    num = num4 + num5;
                }
                this.StartAnimation(to, num, num4 / num, num5 / num);
            }
        }

        public void StartScrollDown()
        {
            double to = this.ContentHeight - this.actualHeight;
            this.StartScroll(to, to - this.ChildOffset);
        }

        private void StartScrollDownTimer()
        {
            this.timer = new DispatcherTimer(DispatcherPriority.Render);
            this.timer.Interval = defaultUpdateInterval;
            this.timer.Tick += new EventHandler(this.TimerTickScrollDown);
            this.timer.Start();
        }

        public void StartScrollUp()
        {
            this.StartScroll(0.0, this.ChildOffset);
        }

        private void StartScrollUpTimer()
        {
            this.timer = new DispatcherTimer(DispatcherPriority.Render);
            this.timer.Interval = defaultUpdateInterval;
            this.timer.Tick += new EventHandler(this.TimerTickScrollUp);
            this.timer.Start();
        }

        private void StopAnimation()
        {
            double childOffset = this.ChildOffset;
            if (this.storyBoard != null)
            {
                this.storyBoard.Completed -= new EventHandler(this.OnStoryboardCompleted);
                this.storyBoard.Remove(this);
                this.storyBoard = null;
            }
            this.ChildOffset = childOffset;
        }

        public void StopScroll(double distanceSign)
        {
            this.StopAnimation();
            if ((this.Animation != null) && (!this.AreCloseToCurrentContentOffset(this.Animation.To.Value) && !this.AreCloseToCurrentContentOffset(this.Animation.From.Value)))
            {
                double currentAnimationSpeed = this.GetCurrentAnimationSpeed(this.ChildOffset, this.ScrollSpeed);
                double num2 = (currentAnimationSpeed * currentAnimationSpeed) / (2.0 * this.Deceleration);
                double duration = currentAnimationSpeed / this.Deceleration;
                double num4 = Math.Abs((double) (this.ChildOffset - this.Animation.To.Value));
                if (num4 < num2)
                {
                    num2 = num4;
                    duration = (2.0 * num4) / currentAnimationSpeed;
                }
                this.StartAnimation(Math.Ceiling((double) (this.ChildOffset + (distanceSign * num2))), duration, 0.0, 1.0);
            }
        }

        public void StopScrollDown()
        {
            this.StopScroll(1.0);
        }

        public void StopScrollUp()
        {
            this.StopScroll(-1.0);
        }

        private void StopTimer()
        {
            if (this.timer != null)
            {
                this.timer.Stop();
            }
            this.timer = null;
        }

        private void TimerTickScrollDown(object sender, EventArgs e)
        {
            this.StopScrollDown();
            this.action = ScrollingAction.NotScrolling;
            this.StopTimer();
        }

        private void TimerTickScrollUp(object sender, EventArgs e)
        {
            this.action = ScrollingAction.NotScrolling;
            this.StopScrollUp();
            this.StopTimer();
        }

        private void UpdateAllowScrollDown()
        {
        }

        private void UpdateAllowScrollDownBinding()
        {
            MultiBinding binding1 = new MultiBinding();
            binding1.Converter = new ContentOffsetToAllowScrollDownConverter();
            MultiBinding binding = binding1;
            Binding item = new Binding(ChildOffsetProperty.GetName());
            item.Source = this;
            binding.Bindings.Add(item);
            Binding binding3 = new Binding(TopBottomSpaceProperty.GetName());
            binding3.Source = this;
            binding.Bindings.Add(binding3);
            Binding binding4 = new Binding((this.Orientation == System.Windows.Controls.Orientation.Vertical) ? "ActualHeight" : "ActualWidth");
            binding4.Source = this.Child;
            binding.Bindings.Add(binding4);
            Binding binding5 = new Binding((this.Orientation == System.Windows.Controls.Orientation.Vertical) ? "ActualHeight" : "ActualWidth");
            binding5.Source = this;
            binding.Bindings.Add(binding5);
            base.SetBinding(AllowScrollDownProperty, binding);
        }

        protected virtual void UpdateAllowScrolling()
        {
        }

        private SizeHelperBase sizeHelper =>
            SizeHelperBase.GetDefineSizeHelper(this.Orientation);

        private double actualHeight =>
            (this.Orientation == System.Windows.Controls.Orientation.Vertical) ? base.ActualHeight : base.ActualWidth;

        private DoubleAnimation Animation =>
            (this.storyBoard != null) ? ((DoubleAnimation) this.storyBoard.Children[0]) : null;

        protected internal double ContentHeight =>
            this.sizeHelper.GetDefineSize(this.Child.DesiredSize) + this.TopBottomSpace;

        public bool AllowScrollUp
        {
            get => 
                (bool) base.GetValue(AllowScrollUpProperty);
            set => 
                base.SetValue(AllowScrollUpProperty, value);
        }

        public bool AllowScrollDown
        {
            get => 
                (bool) base.GetValue(AllowScrollDownProperty);
            set => 
                base.SetValue(AllowScrollDownProperty, value);
        }

        public double ScrollSpeed
        {
            get => 
                (double) base.GetValue(ScrollSpeedProperty);
            set => 
                base.SetValue(ScrollSpeedProperty, value);
        }

        public double Acceleration
        {
            get => 
                (double) base.GetValue(AccelerationProperty);
            set => 
                base.SetValue(AccelerationProperty, value);
        }

        public double Deceleration
        {
            get => 
                (double) base.GetValue(DecelerationProperty);
            set => 
                base.SetValue(DecelerationProperty, value);
        }

        public double ChildOffset
        {
            get => 
                (double) base.GetValue(ChildOffsetProperty);
            set => 
                base.SetValue(ChildOffsetProperty, value);
        }

        public double TopBottomSpace
        {
            get => 
                (double) base.GetValue(TopBottomSpaceProperty);
            set => 
                base.SetValue(TopBottomSpaceProperty, value);
        }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            set => 
                base.SetValue(OrientationProperty, value);
        }

        private DevExpress.Xpf.Core.ManipulationHelper ManipulationHelper
        {
            get
            {
                this.manipulationHelper ??= new DevExpress.Xpf.Core.ManipulationHelper(this);
                return this.manipulationHelper;
            }
        }

        private bool ShouldStartManipulationInertia { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SmoothScroller.<>c <>9 = new SmoothScroller.<>c();

            internal void <.cctor>b__12_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SmoothScroller) d).OnTopBottomSpaceChanged();
            }
        }

        private class ContentOffsetToAllowScrollDownConverter : MarkupExtension, IMultiValueConverter
        {
            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                double num = (double) values[0];
                double num2 = (double) values[1];
                double num4 = (double) values[3];
                return (num < Math.Max(0.0, Math.Floor((double) ((((values[2] != DependencyProperty.UnsetValue) ? ((double) values[2]) : 0.0) + num2) - num4))));
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }

            public override object ProvideValue(IServiceProvider serviceProvider) => 
                this;
        }

        private class ContentOffsetToAllowScrollUpConverter : MarkupExtension, IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
                ((double) value) > 0.0;

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }

            public override object ProvideValue(IServiceProvider serviceProvider) => 
                this;
        }

        private enum ScrollingAction
        {
            ScrollUp,
            ScrollDown,
            NotScrolling
        }
    }
}

