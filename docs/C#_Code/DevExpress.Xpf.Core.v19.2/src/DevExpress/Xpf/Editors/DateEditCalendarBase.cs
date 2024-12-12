namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public abstract class DateEditCalendarBase : ContentControl, IDateEditCalendarBase
    {
        public static readonly DependencyProperty DateTimeProperty;
        public static readonly DependencyProperty MinValueProperty;
        public static readonly DependencyProperty MaxValueProperty;
        public static readonly DependencyProperty ShowWeekNumbersProperty;
        public static readonly DependencyProperty ShowClearButtonProperty;
        public static readonly DependencyProperty ShowTodayProperty;
        public static readonly DependencyProperty MaskProperty;

        static DateEditCalendarBase()
        {
            Type ownerType = typeof(DateEditCalendarBase);
            DateTimeProperty = DependencyPropertyManager.RegisterAttached("DateTime", typeof(System.DateTime), ownerType, new FrameworkPropertyMetadata(System.DateTime.MinValue, new PropertyChangedCallback(DateEditCalendarBase.OnDateTimePropertyChanged)));
            MinValueProperty = DependencyPropertyManager.Register("MinValue", typeof(System.DateTime?), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((DateEditCalendarBase) d).MinValueChanged()));
            MaxValueProperty = DependencyPropertyManager.Register("MaxValue", typeof(System.DateTime?), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((DateEditCalendarBase) d).MaxValueChanged()));
            ShowWeekNumbersProperty = DependencyPropertyManager.RegisterAttached("ShowWeekNumbers", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
            ShowClearButtonProperty = DependencyPropertyManager.Register("ShowClearButton", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsParentMeasure, (d, e) => ((DateEditCalendarBase) d).OnShowClearButtonChanged()));
            ShowTodayProperty = DependencyPropertyManager.Register("ShowToday", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsParentMeasure, (d, e) => ((DateEditCalendarBase) d).OnShowTodayChanged()));
            MaskProperty = DependencyPropertyManager.Register("Mask", typeof(string), ownerType, new PropertyMetadata("d", (d, e) => ((DateEditCalendarBase) d).OnMaskChanged((string) e.NewValue)));
        }

        protected DateEditCalendarBase()
        {
        }

        bool IDateEditCalendarBase.ProcessKeyDown(KeyEventArgs e) => 
            this.ProcessKeyDown(e);

        public static object GetDateTime(DependencyObject obj) => 
            obj.GetValue(DateTimeProperty);

        protected virtual void MaxValueChanged()
        {
        }

        protected virtual void MinValueChanged()
        {
        }

        protected virtual void OnDateTimeChanged()
        {
        }

        private static void OnDateTimePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DateEditCalendarBase)
            {
                ((DateEditCalendarBase) d).OnDateTimeChanged();
            }
        }

        protected virtual void OnMaskChanged(string newValue)
        {
        }

        protected virtual void OnShowClearButtonChanged()
        {
        }

        protected virtual void OnShowTodayChanged()
        {
        }

        protected internal virtual bool ProcessKeyDown(KeyEventArgs e) => 
            false;

        public static void SetDateTime(DependencyObject obj, object value)
        {
            obj.SetValue(DateTimeProperty, value);
        }

        protected virtual void ShowWeekNumbersPropertySet(bool value)
        {
        }

        public System.DateTime? MinValue
        {
            get => 
                (System.DateTime?) base.GetValue(MinValueProperty);
            set => 
                base.SetValue(MinValueProperty, value);
        }

        public System.DateTime? MaxValue
        {
            get => 
                (System.DateTime?) base.GetValue(MaxValueProperty);
            set => 
                base.SetValue(MaxValueProperty, value);
        }

        public System.DateTime DateTime
        {
            get => 
                (System.DateTime) base.GetValue(DateTimeProperty);
            set => 
                base.SetValue(DateTimeProperty, value);
        }

        public bool ShowWeekNumbers
        {
            get => 
                (bool) base.GetValue(ShowWeekNumbersProperty);
            set
            {
                base.SetValue(ShowWeekNumbersProperty, value);
                this.ShowWeekNumbersPropertySet(value);
            }
        }

        public bool ShowToday
        {
            get => 
                (bool) base.GetValue(ShowTodayProperty);
            set => 
                base.SetValue(ShowTodayProperty, value);
        }

        public bool ShowClearButton
        {
            get => 
                (bool) base.GetValue(ShowClearButtonProperty);
            set => 
                base.SetValue(ShowClearButtonProperty, value);
        }

        public string Mask
        {
            get => 
                (string) base.GetValue(MaskProperty);
            set => 
                base.SetValue(MaskProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DateEditCalendarBase.<>c <>9 = new DateEditCalendarBase.<>c();

            internal void <.cctor>b__7_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateEditCalendarBase) d).MinValueChanged();
            }

            internal void <.cctor>b__7_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateEditCalendarBase) d).MaxValueChanged();
            }

            internal void <.cctor>b__7_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateEditCalendarBase) d).OnShowClearButtonChanged();
            }

            internal void <.cctor>b__7_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateEditCalendarBase) d).OnShowTodayChanged();
            }

            internal void <.cctor>b__7_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateEditCalendarBase) d).OnMaskChanged((string) e.NewValue);
            }
        }
    }
}

