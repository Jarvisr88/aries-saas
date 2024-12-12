namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Themes;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class TimePickerClock : Control
    {
        public static readonly DependencyProperty DateTimeProperty;
        public static readonly DependencyProperty HoursProperty;
        public static readonly DependencyProperty MinutesProperty;
        public static readonly DependencyProperty SecondsProperty;
        public static readonly DependencyProperty LongTickOffsetProperty;
        public static readonly DependencyProperty ShortTickOffsetProperty;
        public static readonly DependencyProperty LongTickSizeProperty;
        public static readonly DependencyProperty ShortTickSizeProperty;
        public static readonly DependencyProperty LongTickFrequencyProperty;
        public static readonly DependencyProperty ShortTickFrequencyProperty;
        public static readonly DependencyProperty HoursRotateProperty;
        internal static readonly DependencyPropertyKey HoursRotatePropertyKey;
        public static readonly DependencyProperty MinutesRotateProperty;
        internal static readonly DependencyPropertyKey MinutesRotatePropertyKey;
        public static readonly DependencyProperty SecondsRotateProperty;
        internal static readonly DependencyPropertyKey SecondsRotatePropertyKey;
        private readonly Locker syncValuesLocker = new Locker();
        private Canvas clockCanvas;
        private Line hoursArrowPath;
        private Line minutesArrowPath;
        private Line secondsArrowPath;
        private Brush tickBrush;

        static TimePickerClock()
        {
            Type forType = typeof(TimePickerClock);
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(forType));
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(TimePickerClock), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            DateTimeProperty = DependencyPropertyRegistrator.Register<TimePickerClock, System.DateTime>(System.Linq.Expressions.Expression.Lambda<Func<TimePickerClock, System.DateTime>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TimePickerClock.get_DateTime)), parameters), System.DateTime.Now, (owner, oldValue, newValue) => owner.OnDateTimeChanged(oldValue, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TimePickerClock), "owner");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            HoursProperty = DependencyPropertyRegistrator.Register<TimePickerClock, int>(System.Linq.Expressions.Expression.Lambda<Func<TimePickerClock, int>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TimePickerClock.get_Hours)), expressionArray2), 0, (owner, oldValue, newValue) => owner.OnHoursChanged(oldValue, newValue), (DependencyPropertyRegistratorCoerceCallback<TimePickerClock, int>) ((owner, value) => owner.CoerceHours(value)));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TimePickerClock), "owner");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            MinutesProperty = DependencyPropertyRegistrator.Register<TimePickerClock, int>(System.Linq.Expressions.Expression.Lambda<Func<TimePickerClock, int>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TimePickerClock.get_Minutes)), expressionArray3), 0, (owner, oldValue, newValue) => owner.OnMinutesChanged(oldValue, newValue), (DependencyPropertyRegistratorCoerceCallback<TimePickerClock, int>) ((owner, value) => owner.CoerceMinutes(value)));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TimePickerClock), "owner");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            SecondsProperty = DependencyPropertyRegistrator.Register<TimePickerClock, int>(System.Linq.Expressions.Expression.Lambda<Func<TimePickerClock, int>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TimePickerClock.get_Seconds)), expressionArray4), 0, (owner, oldValue, newValue) => owner.OnSecondsChanged(oldValue, newValue), (DependencyPropertyRegistratorCoerceCallback<TimePickerClock, int>) ((owner, value) => owner.CoerceSeconds(value)));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TimePickerClock), "owner");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            LongTickOffsetProperty = DependencyPropertyRegistrator.Register<TimePickerClock, double>(System.Linq.Expressions.Expression.Lambda<Func<TimePickerClock, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TimePickerClock.get_LongTickOffset)), expressionArray5), 5.0, (owner, oldValue, newValue) => owner.OnLongTickOffsetChanged(oldValue, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TimePickerClock), "owner");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            ShortTickOffsetProperty = DependencyPropertyRegistrator.Register<TimePickerClock, double>(System.Linq.Expressions.Expression.Lambda<Func<TimePickerClock, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TimePickerClock.get_ShortTickOffset)), expressionArray6), 5.0, (owner, oldValue, newValue) => owner.OnShortTickOffsetChanged(oldValue, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TimePickerClock), "owner");
            ParameterExpression[] expressionArray7 = new ParameterExpression[] { expression };
            LongTickSizeProperty = DependencyPropertyRegistrator.Register<TimePickerClock, double>(System.Linq.Expressions.Expression.Lambda<Func<TimePickerClock, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TimePickerClock.get_LongTickSize)), expressionArray7), 5.0, (owner, oldValue, newValue) => owner.OnLongTickSizeChanged(oldValue, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TimePickerClock), "owner");
            ParameterExpression[] expressionArray8 = new ParameterExpression[] { expression };
            ShortTickSizeProperty = DependencyPropertyRegistrator.Register<TimePickerClock, double>(System.Linq.Expressions.Expression.Lambda<Func<TimePickerClock, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TimePickerClock.get_ShortTickSize)), expressionArray8), 5.0, (owner, oldValue, newValue) => owner.OnShortTickSizeChanged(oldValue, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TimePickerClock), "owner");
            ParameterExpression[] expressionArray9 = new ParameterExpression[] { expression };
            LongTickFrequencyProperty = DependencyPropertyRegistrator.Register<TimePickerClock, int>(System.Linq.Expressions.Expression.Lambda<Func<TimePickerClock, int>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TimePickerClock.get_LongTickFrequency)), expressionArray9), 30, (owner, oldValue, newValue) => owner.OnLongTickFrequencyChanged(oldValue, newValue), (DependencyPropertyRegistratorCoerceCallback<TimePickerClock, int>) ((owner, value) => owner.CoerceLongTickFrequency(value)));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TimePickerClock), "owner");
            ParameterExpression[] expressionArray10 = new ParameterExpression[] { expression };
            ShortTickFrequencyProperty = DependencyPropertyRegistrator.Register<TimePickerClock, int>(System.Linq.Expressions.Expression.Lambda<Func<TimePickerClock, int>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TimePickerClock.get_ShortTickFrequency)), expressionArray10), 6, (owner, oldValue, newValue) => owner.OnShortTickFrequencyChanged(oldValue, newValue), (DependencyPropertyRegistratorCoerceCallback<TimePickerClock, int>) ((owner, value) => owner.CoerceShortTickFrequency(value)));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TimePickerClock), "owner");
            ParameterExpression[] expressionArray11 = new ParameterExpression[] { expression };
            HoursRotatePropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<TimePickerClock, int>(System.Linq.Expressions.Expression.Lambda<Func<TimePickerClock, int>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TimePickerClock.get_HoursRotate)), expressionArray11), 0, (owner, oldValue, newValue) => owner.OnHoursRotateChanged(oldValue, newValue));
            HoursRotateProperty = HoursRotatePropertyKey.DependencyProperty;
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TimePickerClock), "owner");
            ParameterExpression[] expressionArray12 = new ParameterExpression[] { expression };
            MinutesRotatePropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<TimePickerClock, int>(System.Linq.Expressions.Expression.Lambda<Func<TimePickerClock, int>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TimePickerClock.get_MinutesRotate)), expressionArray12), 0, (owner, oldValue, newValue) => owner.OnMinutesRotateChanged(oldValue, newValue));
            MinutesRotateProperty = MinutesRotatePropertyKey.DependencyProperty;
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TimePickerClock), "owner");
            ParameterExpression[] expressionArray13 = new ParameterExpression[] { expression };
            SecondsRotatePropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<TimePickerClock, int>(System.Linq.Expressions.Expression.Lambda<Func<TimePickerClock, int>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TimePickerClock.get_SecondsRotate)), expressionArray13), 0, (owner, oldValue, newValue) => owner.OnSecondsRotateChanged(oldValue, newValue));
            SecondsRotateProperty = SecondsRotatePropertyKey.DependencyProperty;
        }

        public TimePickerClock()
        {
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
        }

        protected virtual int CoerceHours(int value) => 
            value % 12;

        protected virtual int CoerceLongTickFrequency(int value) => 
            Math.Min(360, Math.Max(value, 0));

        protected virtual int CoerceMinutes(int value) => 
            value % 60;

        protected virtual int CoerceSeconds(int value) => 
            value % 60;

        protected virtual int CoerceShortTickFrequency(int value) => 
            Math.Min(360, Math.Max(value, 0));

        protected virtual void GenerateLongTicks(double radius)
        {
            int num = 360 / this.LongTickFrequency;
            for (int i = 0; i < num; i++)
            {
                Line line1 = new Line();
                line1.StrokeThickness = 1.0;
                line1.Stroke = this.TickBrush;
                Line element = line1;
                double d = (((i * this.LongTickFrequency) - 90.0) * 3.1415926535897931) / 180.0;
                element.X2 = ((radius - this.LongTickOffset) * Math.Cos(d)) + radius;
                element.Y2 = ((radius - this.LongTickOffset) * Math.Sin(d)) + radius;
                element.X1 = (((radius - this.LongTickOffset) - this.LongTickSize) * Math.Cos(d)) + radius;
                element.Y1 = (((radius - this.LongTickOffset) - this.LongTickSize) * Math.Sin(d)) + radius;
                this.clockCanvas.Children.Add(element);
            }
        }

        protected virtual void GenerateShortTicks(double radius)
        {
            int num = 360 / this.ShortTickFrequency;
            for (int i = 0; i < num; i++)
            {
                Line line1 = new Line();
                line1.StrokeThickness = 1.0;
                line1.Stroke = this.TickBrush;
                Line element = line1;
                int num3 = (i * this.ShortTickFrequency) - 90;
                if ((num3 % this.LongTickFrequency) != 0)
                {
                    double d = (num3 * 3.1415926535897931) / 180.0;
                    element.X2 = ((radius - this.ShortTickOffset) * Math.Cos(d)) + radius;
                    element.Y2 = ((radius - this.ShortTickOffset) * Math.Sin(d)) + radius;
                    element.X1 = (((radius - this.ShortTickOffset) - this.ShortTickSize) * Math.Cos(d)) + radius;
                    element.Y1 = (((radius - this.ShortTickOffset) - this.ShortTickSize) * Math.Sin(d)) + radius;
                    this.clockCanvas.Children.Add(element);
                }
            }
        }

        protected virtual void InitializeElements()
        {
            UIElement templateChild = (UIElement) base.GetTemplateChild("PART_ClockCenterDot");
            TimePickerThemeKeyExtension resourceKey = new TimePickerThemeKeyExtension();
            resourceKey.ResourceKey = TimePickerThemeKeys.ClockShaftSize;
            double num = (double) base.FindResource(resourceKey);
            Point point = new Point(base.Width / 2.0, base.Height / 2.0);
            if (templateChild != null)
            {
                Canvas.SetLeft(templateChild, point.X - (num / 2.0));
                Canvas.SetTop(templateChild, point.Y - (num / 2.0));
            }
            this.InvalidateTicks();
            if (this.hoursArrowPath != null)
            {
                this.hoursArrowPath.X1 = point.X;
                this.hoursArrowPath.Y1 = point.Y;
            }
            if (this.minutesArrowPath != null)
            {
                this.minutesArrowPath.X1 = point.X;
                this.minutesArrowPath.Y1 = point.Y;
            }
            if (this.secondsArrowPath != null)
            {
                this.secondsArrowPath.X1 = point.X;
                this.secondsArrowPath.Y1 = point.Y;
            }
            this.UpdateArrows(TimePickerArrow.Seconds | TimePickerArrow.Minutes | TimePickerArrow.Hours);
        }

        private void InvalidateTicks()
        {
            if ((this.clockCanvas != null) && !double.IsNaN(base.Height))
            {
                this.clockCanvas.Children.Clear();
                this.GenerateLongTicks(base.Height / 2.0);
                this.GenerateShortTicks(base.Height / 2.0);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.clockCanvas = (Canvas) base.GetTemplateChild("PART_ClockCanvas");
            this.hoursArrowPath = (Line) base.GetTemplateChild("PART_ClockHoursArrow");
            this.minutesArrowPath = (Line) base.GetTemplateChild("PART_ClockMinutesArrow");
            this.secondsArrowPath = (Line) base.GetTemplateChild("PART_ClockSecondsArrow");
            this.InitializeElements();
        }

        protected virtual void OnDateTimeChanged(System.DateTime oldValue, System.DateTime newValue)
        {
            this.Hours = (newValue.Hour >= 12) ? (newValue.Hour - 12) : newValue.Hour;
            this.Minutes = newValue.Minute;
            this.Seconds = newValue.Second;
            this.UpdateArrows(TimePickerArrow.Seconds | TimePickerArrow.Minutes | TimePickerArrow.Hours);
        }

        protected virtual void OnHoursChanged(int oldValue, int newValue)
        {
            this.HoursRotate = (newValue * 30) + (this.Minutes / 2);
        }

        protected virtual void OnHoursRotateChanged(int oldValue, int newValue)
        {
            this.syncValuesLocker.DoLockedActionIfNotLocked(() => this.Hours = newValue / 30);
            this.UpdateArrows(TimePickerArrow.Hours);
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.UpdateArrows(TimePickerArrow.Seconds | TimePickerArrow.Minutes | TimePickerArrow.Hours);
        }

        protected virtual void OnLongTickFrequencyChanged(int oldValue, int newValue)
        {
            this.InvalidateTicks();
        }

        protected virtual void OnLongTickOffsetChanged(double oldValue, double newValue)
        {
            this.InvalidateTicks();
        }

        protected virtual void OnLongTickSizeChanged(double oldValue, double newValue)
        {
            this.InvalidateTicks();
        }

        protected virtual void OnMinutesChanged(int oldValue, int newValue)
        {
            this.MinutesRotate = newValue * 6;
            this.HoursRotate = (this.Hours * 30) + (newValue / 2);
        }

        protected virtual void OnMinutesRotateChanged(int oldValue, int newValue)
        {
            this.syncValuesLocker.DoLockedActionIfNotLocked(() => this.Minutes = newValue / 6);
            this.UpdateArrows(TimePickerArrow.Minutes);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ReferenceEquals(e.Property, Control.ForegroundProperty))
            {
                this.tickBrush = null;
                this.InvalidateTicks();
            }
            if (ReferenceEquals(e.Property, FrameworkElement.HeightProperty))
            {
                this.UpdateArrows(TimePickerArrow.Seconds | TimePickerArrow.Minutes | TimePickerArrow.Hours);
                this.InvalidateTicks();
            }
        }

        protected virtual void OnSecondsChanged(int oldValue, int newValue)
        {
            this.SecondsRotate = newValue * 6;
            this.HoursRotate = (this.Hours * 30) + (this.Minutes / 2);
        }

        protected virtual void OnSecondsRotateChanged(int oldValue, int newValue)
        {
            this.syncValuesLocker.DoLockedActionIfNotLocked(() => this.Seconds = newValue / 6);
            this.UpdateArrows(TimePickerArrow.Seconds);
        }

        protected virtual void OnShortTickFrequencyChanged(int oldValue, int newValue)
        {
            this.InvalidateTicks();
        }

        protected virtual void OnShortTickOffsetChanged(double oldValue, double newValue)
        {
            this.InvalidateTicks();
        }

        protected virtual void OnShortTickSizeChanged(double oldValue, double newValue)
        {
            this.InvalidateTicks();
        }

        private void UpdateArrows(TimePickerArrow arrows = 7)
        {
            if (!double.IsNaN(base.Height))
            {
                double num = base.Height / 2.0;
                if (arrows.HasFlag(TimePickerArrow.Hours) && (this.hoursArrowPath != null))
                {
                    double num2 = num * 0.6;
                    double d = ((this.HoursRotate - 90) * 3.1415926535897931) / 180.0;
                    this.hoursArrowPath.X2 = (num2 * Math.Cos(d)) + num;
                    this.hoursArrowPath.Y2 = (num2 * Math.Sin(d)) + num;
                }
                if (arrows.HasFlag(TimePickerArrow.Minutes) && (this.minutesArrowPath != null))
                {
                    double num4 = num * 0.7;
                    double d = ((this.MinutesRotate - 90) * 3.1415926535897931) / 180.0;
                    this.minutesArrowPath.X2 = (num4 * Math.Cos(d)) + num;
                    this.minutesArrowPath.Y2 = (num4 * Math.Sin(d)) + num;
                }
                if (arrows.HasFlag(TimePickerArrow.Seconds) && (this.secondsArrowPath != null))
                {
                    double num6 = num * 0.8;
                    double d = ((this.SecondsRotate - 90) * 3.1415926535897931) / 180.0;
                    this.secondsArrowPath.X2 = (num6 * Math.Cos(d)) + num;
                    this.secondsArrowPath.Y2 = (num6 * Math.Sin(d)) + num;
                }
            }
        }

        public System.DateTime DateTime
        {
            get => 
                (System.DateTime) base.GetValue(DateTimeProperty);
            set => 
                base.SetValue(DateTimeProperty, value);
        }

        public int Hours
        {
            get => 
                (int) base.GetValue(HoursProperty);
            set => 
                base.SetValue(HoursProperty, value);
        }

        public int Minutes
        {
            get => 
                (int) base.GetValue(MinutesProperty);
            set => 
                base.SetValue(MinutesProperty, value);
        }

        public int Seconds
        {
            get => 
                (int) base.GetValue(SecondsProperty);
            set => 
                base.SetValue(SecondsProperty, value);
        }

        public int HoursRotate
        {
            get => 
                (int) base.GetValue(HoursRotateProperty);
            internal set => 
                base.SetValue(HoursRotatePropertyKey, value);
        }

        public int MinutesRotate
        {
            get => 
                (int) base.GetValue(MinutesRotateProperty);
            internal set => 
                base.SetValue(MinutesRotatePropertyKey, value);
        }

        public int SecondsRotate
        {
            get => 
                (int) base.GetValue(SecondsRotateProperty);
            internal set => 
                base.SetValue(SecondsRotatePropertyKey, value);
        }

        public double LongTickOffset
        {
            get => 
                (double) base.GetValue(LongTickOffsetProperty);
            set => 
                base.SetValue(LongTickOffsetProperty, value);
        }

        public double ShortTickOffset
        {
            get => 
                (double) base.GetValue(ShortTickOffsetProperty);
            set => 
                base.SetValue(ShortTickOffsetProperty, value);
        }

        public double LongTickSize
        {
            get => 
                (double) base.GetValue(LongTickSizeProperty);
            set => 
                base.SetValue(LongTickSizeProperty, value);
        }

        public double ShortTickSize
        {
            get => 
                (double) base.GetValue(ShortTickSizeProperty);
            set => 
                base.SetValue(ShortTickSizeProperty, value);
        }

        public int LongTickFrequency
        {
            get => 
                (int) base.GetValue(LongTickFrequencyProperty);
            set => 
                base.SetValue(LongTickFrequencyProperty, value);
        }

        public int ShortTickFrequency
        {
            get => 
                (int) base.GetValue(ShortTickFrequencyProperty);
            set => 
                base.SetValue(ShortTickFrequencyProperty, value);
        }

        private Brush TickBrush
        {
            get
            {
                if (this.tickBrush == null)
                {
                    this.tickBrush = base.Foreground.Clone();
                    this.tickBrush.Opacity = 0.5;
                }
                return this.tickBrush;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TimePickerClock.<>c <>9 = new TimePickerClock.<>c();

            internal void <.cctor>b__16_0(TimePickerClock owner, DateTime oldValue, DateTime newValue)
            {
                owner.OnDateTimeChanged(oldValue, newValue);
            }

            internal void <.cctor>b__16_1(TimePickerClock owner, int oldValue, int newValue)
            {
                owner.OnHoursChanged(oldValue, newValue);
            }

            internal void <.cctor>b__16_10(TimePickerClock owner, double oldValue, double newValue)
            {
                owner.OnShortTickSizeChanged(oldValue, newValue);
            }

            internal void <.cctor>b__16_11(TimePickerClock owner, int oldValue, int newValue)
            {
                owner.OnLongTickFrequencyChanged(oldValue, newValue);
            }

            internal object <.cctor>b__16_12(TimePickerClock owner, int value) => 
                owner.CoerceLongTickFrequency(value);

            internal void <.cctor>b__16_13(TimePickerClock owner, int oldValue, int newValue)
            {
                owner.OnShortTickFrequencyChanged(oldValue, newValue);
            }

            internal object <.cctor>b__16_14(TimePickerClock owner, int value) => 
                owner.CoerceShortTickFrequency(value);

            internal void <.cctor>b__16_15(TimePickerClock owner, int oldValue, int newValue)
            {
                owner.OnHoursRotateChanged(oldValue, newValue);
            }

            internal void <.cctor>b__16_16(TimePickerClock owner, int oldValue, int newValue)
            {
                owner.OnMinutesRotateChanged(oldValue, newValue);
            }

            internal void <.cctor>b__16_17(TimePickerClock owner, int oldValue, int newValue)
            {
                owner.OnSecondsRotateChanged(oldValue, newValue);
            }

            internal object <.cctor>b__16_2(TimePickerClock owner, int value) => 
                owner.CoerceHours(value);

            internal void <.cctor>b__16_3(TimePickerClock owner, int oldValue, int newValue)
            {
                owner.OnMinutesChanged(oldValue, newValue);
            }

            internal object <.cctor>b__16_4(TimePickerClock owner, int value) => 
                owner.CoerceMinutes(value);

            internal void <.cctor>b__16_5(TimePickerClock owner, int oldValue, int newValue)
            {
                owner.OnSecondsChanged(oldValue, newValue);
            }

            internal object <.cctor>b__16_6(TimePickerClock owner, int value) => 
                owner.CoerceSeconds(value);

            internal void <.cctor>b__16_7(TimePickerClock owner, double oldValue, double newValue)
            {
                owner.OnLongTickOffsetChanged(oldValue, newValue);
            }

            internal void <.cctor>b__16_8(TimePickerClock owner, double oldValue, double newValue)
            {
                owner.OnShortTickOffsetChanged(oldValue, newValue);
            }

            internal void <.cctor>b__16_9(TimePickerClock owner, double oldValue, double newValue)
            {
                owner.OnLongTickSizeChanged(oldValue, newValue);
            }
        }
    }
}

