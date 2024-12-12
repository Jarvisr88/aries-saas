namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TimePickerClockEditorBehavior : Behavior<ButtonEdit>
    {
        public static readonly DependencyProperty MinValueProperty;
        public static readonly DependencyProperty MaxValueProperty;

        static TimePickerClockEditorBehavior()
        {
            Type ownerType = typeof(TimePickerClockEditorBehavior);
            MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(DateTime?), ownerType, new PropertyMetadata(null, (d, args) => ((TimePickerClockEditorBehavior) d).OnMaxValueChanged()));
            MinValueProperty = DependencyProperty.Register("MinValue", typeof(DateTime?), ownerType, new PropertyMetadata(null, (d, args) => ((TimePickerClockEditorBehavior) d).OnMinValueChanged()));
        }

        private void AssociatedObjectOnValidate(object sender, ValidationEventArgs e)
        {
            DateTime? maxValue;
            DateTime time2;
            DateTime time = (DateTime) e.Value;
            if (this.MaxValue != null)
            {
                maxValue = this.MaxValue;
                time2 = time;
                if ((maxValue != null) ? (maxValue.GetValueOrDefault() < time2) : false)
                {
                    goto TR_0003;
                }
            }
            if (this.MinValue == null)
            {
                return;
            }
            else
            {
                maxValue = this.MinValue;
                time2 = time;
                if (!((maxValue != null) ? (maxValue.GetValueOrDefault() > time2) : false))
                {
                    return;
                }
            }
        TR_0003:
            e.IsValid = false;
            if (this.MinValue != null)
            {
                e.ErrorContent = (this.MaxValue != null) ? string.Format(EditorLocalizer.GetString(EditorStringId.TimePicker_ValidationErrorInRange), this.MinValue.Value, this.MaxValue.Value) : string.Format(EditorLocalizer.GetString(EditorStringId.TimePicker_ValidationErrorGreaterThan), this.MinValue.Value);
            }
            else
            {
                e.ErrorContent = string.Format(EditorLocalizer.GetString(EditorStringId.TimePicker_ValidationErrorLessThan), this.MaxValue.Value);
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            base.AssociatedObject.Validate += new ValidateEventHandler(this.AssociatedObjectOnValidate);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            base.AssociatedObject.Validate -= new ValidateEventHandler(this.AssociatedObjectOnValidate);
        }

        private void OnMaxValueChanged()
        {
            Action<ButtonEdit> action = <>c.<>9__13_0;
            if (<>c.<>9__13_0 == null)
            {
                Action<ButtonEdit> local1 = <>c.<>9__13_0;
                action = <>c.<>9__13_0 = x => x.DoValidate();
            }
            base.AssociatedObject.Do<ButtonEdit>(action);
        }

        private void OnMinValueChanged()
        {
            Action<ButtonEdit> action = <>c.<>9__12_0;
            if (<>c.<>9__12_0 == null)
            {
                Action<ButtonEdit> local1 = <>c.<>9__12_0;
                action = <>c.<>9__12_0 = x => x.DoValidate();
            }
            base.AssociatedObject.Do<ButtonEdit>(action);
        }

        public DateTime? MinValue
        {
            get => 
                (DateTime?) base.GetValue(MinValueProperty);
            set => 
                base.SetValue(MinValueProperty, value);
        }

        public DateTime? MaxValue
        {
            get => 
                (DateTime?) base.GetValue(MaxValueProperty);
            set => 
                base.SetValue(MaxValueProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TimePickerClockEditorBehavior.<>c <>9 = new TimePickerClockEditorBehavior.<>c();
            public static Action<ButtonEdit> <>9__12_0;
            public static Action<ButtonEdit> <>9__13_0;

            internal void <.cctor>b__2_0(DependencyObject d, DependencyPropertyChangedEventArgs args)
            {
                ((TimePickerClockEditorBehavior) d).OnMaxValueChanged();
            }

            internal void <.cctor>b__2_1(DependencyObject d, DependencyPropertyChangedEventArgs args)
            {
                ((TimePickerClockEditorBehavior) d).OnMinValueChanged();
            }

            internal void <OnMaxValueChanged>b__13_0(ButtonEdit x)
            {
                x.DoValidate();
            }

            internal void <OnMinValueChanged>b__12_0(ButtonEdit x)
            {
                x.DoValidate();
            }
        }
    }
}

