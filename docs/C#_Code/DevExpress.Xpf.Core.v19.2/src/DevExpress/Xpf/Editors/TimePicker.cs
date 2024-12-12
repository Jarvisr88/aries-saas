namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [LicenseProvider(typeof(DX_WPFEditors_LicenseProvider)), DXToolboxBrowsable(DXToolboxItemKind.Free)]
    public class TimePicker : Control
    {
        public static readonly DependencyProperty DateTimeProperty;
        public static readonly DependencyProperty MinValueProperty;
        public static readonly DependencyProperty MaxValueProperty;
        public static readonly RoutedEvent DateTimeChangedEvent;

        public event TimePickerDateTimeChangedEventHandler DateTimeChanged
        {
            add
            {
                base.AddHandler(DateTimeChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(DateTimeChangedEvent, value);
            }
        }

        static TimePicker()
        {
            Type forType = typeof(TimePicker);
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(forType));
            DateTimeProperty = DependencyProperty.Register("DateTime", typeof(System.DateTime), forType, new FrameworkPropertyMetadata(System.DateTime.Now, (d, e) => ((TimePicker) d).OnDateTimeChangedInternal((System.DateTime) e.OldValue, (System.DateTime) e.NewValue), (CoerceValueCallback) ((d, e) => ((TimePicker) d).CoerceDateTime((System.DateTime) e))));
            MinValueProperty = DependencyProperty.Register("MinValue", typeof(System.DateTime?), forType, new FrameworkPropertyMetadata(null, (d, e) => ((TimePicker) d).OnMinValueChanged((System.DateTime?) e.OldValue, (System.DateTime?) e.NewValue)));
            MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(System.DateTime?), forType, new FrameworkPropertyMetadata(null, (d, e) => ((TimePicker) d).OnMaxValueChanged((System.DateTime?) e.OldValue, (System.DateTime?) e.NewValue)));
            DateTimeChangedEvent = EventManager.RegisterRoutedEvent("DateTimeChanged", RoutingStrategy.Direct, typeof(TimePickerDateTimeChangedEventHandler), forType);
        }

        protected virtual System.DateTime CoerceDateTime(System.DateTime value)
        {
            System.DateTime? minValue;
            System.DateTime time;
            if (this.MinValue != null)
            {
                time = value;
                minValue = this.MinValue;
                if ((minValue != null) ? (time < minValue.GetValueOrDefault()) : false)
                {
                    return this.MinValue.Value;
                }
            }
            if (this.MaxValue != null)
            {
                time = value;
                minValue = this.MaxValue;
                if ((minValue != null) ? (time > minValue.GetValueOrDefault()) : false)
                {
                    return this.MaxValue.Value;
                }
            }
            return value;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.OwnerDateEdit != null)
            {
                this.OwnerDateEdit.OnApplyTimePickerTemplate(this);
            }
        }

        protected virtual void OnDateTimeChanged(System.DateTime oldValue, System.DateTime newValue)
        {
        }

        private void OnDateTimeChangedInternal(System.DateTime oldValue, System.DateTime newValue)
        {
            base.RaiseEvent(new TimePickerDateTimeChangedEventArgs(oldValue, newValue));
            this.OnDateTimeChanged(oldValue, newValue);
        }

        protected virtual void OnMaxValueChanged(System.DateTime? oldValue, System.DateTime? newValue)
        {
            if (newValue != null)
            {
                System.DateTime dateTime = this.DateTime;
                System.DateTime? nullable = newValue;
                if ((nullable != null) ? (dateTime > nullable.GetValueOrDefault()) : false)
                {
                    this.DateTime = newValue.Value;
                }
            }
        }

        protected virtual void OnMinValueChanged(System.DateTime? oldValue, System.DateTime? newValue)
        {
            if (newValue != null)
            {
                System.DateTime dateTime = this.DateTime;
                System.DateTime? nullable = newValue;
                if ((nullable != null) ? (dateTime < nullable.GetValueOrDefault()) : false)
                {
                    this.DateTime = newValue.Value;
                }
            }
        }

        internal void SetDate(System.DateTime dateTime)
        {
            this.DateTime = new System.DateTime(dateTime.Year, dateTime.Month, dateTime.Day, this.DateTime.Hour, this.DateTime.Minute, this.DateTime.Second);
        }

        public System.DateTime DateTime
        {
            get => 
                (System.DateTime) base.GetValue(DateTimeProperty);
            set => 
                base.SetValue(DateTimeProperty, value);
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

        protected internal DateEdit OwnerDateEdit =>
            BaseEdit.GetOwnerEdit(this) as DateEdit;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TimePicker.<>c <>9 = new TimePicker.<>c();

            internal void <.cctor>b__4_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TimePicker) d).OnDateTimeChangedInternal((DateTime) e.OldValue, (DateTime) e.NewValue);
            }

            internal object <.cctor>b__4_1(DependencyObject d, object e) => 
                ((TimePicker) d).CoerceDateTime((DateTime) e);

            internal void <.cctor>b__4_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TimePicker) d).OnMinValueChanged((DateTime?) e.OldValue, (DateTime?) e.NewValue);
            }

            internal void <.cctor>b__4_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TimePicker) d).OnMaxValueChanged((DateTime?) e.OldValue, (DateTime?) e.NewValue);
            }
        }
    }
}

