namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.XtraReports.Parameters;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class DateRangeEdit : PopupBaseEdit
    {
        private const double popupMinHeight = 260.0;
        private readonly Locker minMaxLocker = new Locker();
        public static readonly DependencyProperty MinValueProperty;
        public static readonly DependencyProperty MaxValueProperty;
        private DevExpress.Xpf.Printing.Native.DateRangeControl dateRangeControl;

        static DateRangeEdit()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DateRangeEdit), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<DateRangeEdit> registrator1 = DependencyPropertyRegistrator<DateRangeEdit>.New().Register<DateTime>(System.Linq.Expressions.Expression.Lambda<Func<DateRangeEdit, DateTime>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DateRangeEdit.get_MinValue)), parameters), out MinValueProperty, DateTime.MinValue.Date, d => d.OnMinValueChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DateRangeEdit), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator1.Register<DateTime>(System.Linq.Expressions.Expression.Lambda<Func<DateRangeEdit, DateTime>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DateRangeEdit.get_MaxValue)), expressionArray2), out MaxValueProperty, DateTime.MaxValue.Date, d => d.OnMaxValueChanged(), frameworkOptions).OverrideMetadata(PopupBaseEdit.PopupFooterButtonsProperty, PopupFooterButtons.OkCancel, null, FrameworkPropertyMetadataOptions.None);
        }

        public DateRangeEdit()
        {
            base.DefaultStyleKey = typeof(DateRangeEdit);
        }

        protected override void AcceptPopupValue()
        {
            base.AcceptPopupValue();
            this.EditStrategy.SyncWithRangeControl();
        }

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new DateRangeEditPropertyProvider(this);

        protected override EditStrategyBase CreateEditStrategy() => 
            new DateRangeEditStrategy(this);

        protected override object GetDefaultEditValue() => 
            new Range<DateTime>(DateTime.Today, DateTime.Today);

        private DevExpress.Xpf.Printing.Native.DateRangeControl GetRangeControl()
        {
            Predicate<FrameworkElement> predicate = <>c.<>9__34_0;
            if (<>c.<>9__34_0 == null)
            {
                Predicate<FrameworkElement> local1 = <>c.<>9__34_0;
                predicate = <>c.<>9__34_0 = element => (element is DevExpress.Xpf.Printing.Native.DateRangeControl) && (element.Name == "PART_PopupContent");
            }
            return (DevExpress.Xpf.Printing.Native.DateRangeControl) LayoutHelper.FindElement(base.Popup.Child as FrameworkElement, predicate);
        }

        protected override bool IsClosePopupWithAcceptGesture(Key key, ModifierKeys modifiers) => 
            false;

        private void OnMaxValueChanged()
        {
            this.minMaxLocker.DoLockedActionIfNotLocked(delegate {
                if (this.MinValue > this.MaxValue)
                {
                    this.MinValue = this.MaxValue;
                }
            });
            this.UpdateRangeControlMinMaxValues();
        }

        private void OnMinValueChanged()
        {
            this.minMaxLocker.DoLockedActionIfNotLocked(delegate {
                if (this.MaxValue < this.MinValue)
                {
                    this.MaxValue = this.MinValue;
                }
            });
            this.UpdateRangeControlMinMaxValues();
        }

        protected override void OnPopupClosed()
        {
            base.OnPopupClosed();
            this.DateRangeControl = null;
        }

        protected override void OnPopupOpened()
        {
            this.DateRangeControl = this.GetRangeControl();
            this.UpdateDateRangeValues();
            base.OnPopupOpened();
            this.EditStrategy.OnPopupOpened();
        }

        private void UpdateDateRangeValues()
        {
            if (this.DateRangeControl != null)
            {
                this.UpdateRangeControlMinMaxValues();
                if (base.EditValue != null)
                {
                    this.DateRangeControl.Range = (Range<DateTime>) base.EditValue;
                }
            }
        }

        internal void UpdateOkButtonIsEnabled(bool isEnabled)
        {
            base.PropertyProvider.PopupViewModel.OkButtonIsEnabled = isEnabled;
        }

        private void UpdateRangeControlMinMaxValues()
        {
            if (this.DateRangeControl != null)
            {
                this.DateRangeControl.MinValue = this.MinValue.Date;
                this.DateRangeControl.MaxValue = this.MaxValue.Date;
            }
        }

        public DateTime MinValue
        {
            get => 
                (DateTime) base.GetValue(MinValueProperty);
            set => 
                base.SetValue(MinValueProperty, value);
        }

        public DateTime MaxValue
        {
            get => 
                (DateTime) base.GetValue(MaxValueProperty);
            set => 
                base.SetValue(MaxValueProperty, value);
        }

        public override FrameworkElement PopupElement =>
            this.DateRangeControl;

        protected internal DateRangeEditStrategy EditStrategy =>
            (DateRangeEditStrategy) base.EditStrategy;

        protected internal DevExpress.Xpf.Printing.Native.DateRangeControl DateRangeControl
        {
            get => 
                this.dateRangeControl;
            set
            {
                if (!ReferenceEquals(value, this.dateRangeControl))
                {
                    this.dateRangeControl = value;
                    this.UpdateDateRangeValues();
                }
            }
        }

        protected override System.Type StyleSettingsType =>
            typeof(DateRangeEditStyleSettings);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DateRangeEdit.<>c <>9 = new DateRangeEdit.<>c();
            public static Predicate<FrameworkElement> <>9__34_0;

            internal void <.cctor>b__20_0(DateRangeEdit d)
            {
                d.OnMinValueChanged();
            }

            internal void <.cctor>b__20_1(DateRangeEdit d)
            {
                d.OnMaxValueChanged();
            }

            internal bool <GetRangeControl>b__34_0(FrameworkElement element) => 
                (element is DateRangeControl) && (element.Name == "PART_PopupContent");
        }
    }
}

