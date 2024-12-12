namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Automation;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Validation.Native;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Data;
    using System.Windows.Input;

    [LicenseProvider(typeof(DX_WPFEditors_LicenseProvider)), DXToolboxBrowsable(DXToolboxItemKind.Free)]
    public class DateEdit : PopupBaseEdit
    {
        public static readonly DependencyProperty MinValueProperty;
        public static readonly DependencyProperty MaxValueProperty;
        public static readonly DependencyProperty ShowWeekNumbersProperty;
        public static readonly DependencyProperty DateTimeProperty;
        public static readonly DependencyProperty ShowClearButtonProperty;
        public static readonly DependencyProperty ShowTodayProperty;
        public static readonly DependencyProperty AllowRoundOutOfRangeValueProperty;
        internal static readonly DependencyPropertyKey DateEditPopupContentTypePropertyKey;
        public static readonly DependencyProperty DateEditPopupContentTypeProperty;
        private IDateEditCalendarBase calendar;
        private DevExpress.Xpf.Editors.TimePicker timePicker;

        static DateEdit()
        {
            Type ownerType = typeof(DateEdit);
            AllowRoundOutOfRangeValueProperty = DependencyPropertyManager.Register("AllowRoundOutOfRangeValue", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, (d, e) => ((DateEdit) d).AllowRoundOutOfRangeValueChanged((bool) e.NewValue)));
            MinValueProperty = DependencyPropertyManager.RegisterAttached("MinValue", typeof(System.DateTime?), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(DateEdit.OnMinValueChanged)));
            MaxValueProperty = DependencyPropertyManager.RegisterAttached("MaxValue", typeof(System.DateTime?), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(DateEdit.OnMaxValueChanged)));
            ShowWeekNumbersProperty = DependencyPropertyManager.Register("ShowWeekNumbers", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(DateEdit.OnShowWeekNumbersChanged)));
            ShowClearButtonProperty = DateEditCalendarBase.ShowClearButtonProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(DateEdit.OnShowClearButtonChanged)));
            ShowTodayProperty = DateEditCalendarBase.ShowTodayProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(DateEdit.OnShowTodayChanged)));
            DateEditPopupContentTypePropertyKey = DependencyPropertyManager.RegisterReadOnly("DateEditPopupContentType", typeof(DevExpress.Xpf.Editors.DateEditPopupContentType), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.DateEditPopupContentType.Calendar, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(DateEdit.OnDateEditPopupContentTypeChanged)));
            DateEditPopupContentTypeProperty = DateEditPopupContentTypePropertyKey.DependencyProperty;
            DateTimeProperty = DependencyPropertyManager.Register("DateTime", typeof(System.DateTime), ownerType, new FrameworkPropertyMetadata(System.DateTime.Today, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(DateEdit.OnDateTimePropertyChanged), new CoerceValueCallback(DateEdit.OnCoerceDateTimeProperty), true, UpdateSourceTrigger.LostFocus));
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(ownerType));
            BaseEdit.DisplayFormatStringProperty.AddOwner(typeof(DateEdit), new FrameworkPropertyMetadata("d"));
            TextEdit.MaskTypeProperty.AddOwner(typeof(DateEdit), new FrameworkPropertyMetadata(MaskType.DateTime));
            TextEdit.MaskProperty.AddOwner(typeof(DateEdit), new FrameworkPropertyMetadata("d"));
            BaseEdit.AllowNullInputProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(true));
            PopupBaseEdit.ShowSizeGripProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(false));
        }

        protected override void AcceptPopupValue()
        {
            if (base.IsReadOnly || !base.PropertyProvider.GetService<BaseEditingSettingsService>().AllowKeyHandling)
            {
                base.AcceptPopupValue();
            }
            else
            {
                Func<DevExpress.Xpf.Editors.TimePicker, System.DateTime> evaluator = <>c.<>9__77_0;
                if (<>c.<>9__77_0 == null)
                {
                    Func<DevExpress.Xpf.Editors.TimePicker, System.DateTime> local1 = <>c.<>9__77_0;
                    evaluator = <>c.<>9__77_0 = x => x.DateTime;
                }
                System.DateTime time = this.TimePicker.Return<DevExpress.Xpf.Editors.TimePicker, System.DateTime>(evaluator, () => this.DateTime);
                Func<IDateEditCalendarBase, System.DateTime> func2 = <>c.<>9__77_2;
                if (<>c.<>9__77_2 == null)
                {
                    Func<IDateEditCalendarBase, System.DateTime> local2 = <>c.<>9__77_2;
                    func2 = <>c.<>9__77_2 = x => x.DateTime;
                }
                System.DateTime time2 = this.Calendar.Return<IDateEditCalendarBase, System.DateTime>(func2, () => this.DateTime);
                switch (this.DateEditPopupContentType)
                {
                    case DevExpress.Xpf.Editors.DateEditPopupContentType.DateTimePicker:
                    case DevExpress.Xpf.Editors.DateEditPopupContentType.Navigator:
                        this.SetDateTime(time2.Date.Add(this.DateTime.TimeOfDay), UpdateEditorSource.ValueChanging);
                        break;

                    case DevExpress.Xpf.Editors.DateEditPopupContentType.TimePicker:
                    case DevExpress.Xpf.Editors.DateEditPopupContentType.NavigatorWithTimePicker:
                        this.SetDateTime(new System.DateTime(time2.Year, time2.Month, time2.Day, time.Hour, time.Minute, time.Second), UpdateEditorSource.ValueChanging);
                        break;

                    default:
                        break;
                }
                base.AcceptPopupValue();
            }
        }

        protected virtual void AllowRoundOutOfRangeValueChanged(bool value)
        {
            this.EditStrategy.RoundToBoundsChanged(value);
        }

        protected override void BeforePreviewKeyDown(KeyEventArgs e)
        {
            base.BeforePreviewKeyDown(e);
            e.Handled = ((this.Calendar != null) && base.IsPopupOpen) && this.Calendar.ProcessKeyDown(e);
        }

        protected virtual object CoerceDateTimeProperty(object value) => 
            this.EditStrategy.CoerceDateTime(value);

        protected override bool? CoerceShowSizeGrip(bool? show) => 
            false;

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new DateEditPropertyProvider(this);

        protected override EditStrategyBase CreateEditStrategy() => 
            new DateEditStrategy(this);

        protected internal override BaseEditStyleSettings CreateStyleSettings() => 
            CompatibilitySettings.UseDateNavigatorInDateEdit ? ((BaseEditStyleSettings) new DateEditNavigatorStyleSettings()) : ((BaseEditStyleSettings) new DateEditCalendarStyleSettings());

        protected internal override TextInputSettingsBase CreateTextInputSettings() => 
            new TextInputMaskSettings(this);

        protected internal override MaskType[] GetSupportedMaskTypes() => 
            new MaskType[] { MaskType.DateTime, MaskType.DateTimeAdvancingCaret };

        protected override bool IsClosePopupWithAcceptGesture(Key key, ModifierKeys modifiers) => 
            base.IsClosePopupWithAcceptGesture(key, modifiers) || this.EditStrategy.IsClosePopupWithAcceptGesture(key, modifiers);

        protected override bool IsClosePopupWithCancelGesture(Key key, ModifierKeys modifiers) => 
            base.IsClosePopupWithCancelGesture(key, modifiers) || this.EditStrategy.IsClosePopupWithCancelGesture(key, modifiers);

        protected override void OnAllowNullInputChanged()
        {
            base.OnAllowNullInputChanged();
            this.UpdateCalendarShowClearButton();
        }

        protected internal virtual void OnApplyCalendarTemplate(IDateEditCalendarBase calendar)
        {
            this.Calendar = calendar;
        }

        protected internal virtual void OnApplyTimePickerTemplate(DevExpress.Xpf.Editors.TimePicker picker)
        {
            this.TimePicker = picker;
        }

        protected virtual void OnCalendarChanged()
        {
            if (this.Calendar != null)
            {
                this.FlushPendingEditActions(UpdateEditorSource.ValueChanging);
                this.UpdateCalendarValues();
                this.Calendar.ShowWeekNumbers = this.ShowWeekNumbers;
                this.UpdateCalendarShowClearButton();
                this.UpdateCalendarShowToday();
            }
        }

        protected static object OnCoerceDateTimeProperty(DependencyObject obj, object value) => 
            ((DateEdit) obj).CoerceDateTimeProperty(value);

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new DateEditAutomationPeer(this);

        protected virtual void OnDateEditPopupContentTypeChanged()
        {
        }

        protected static void OnDateEditPopupContentTypeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((DateEdit) obj).OnDateEditPopupContentTypeChanged();
        }

        protected virtual void OnDateTimeChanged(System.DateTime oldDateTime, System.DateTime dateTime)
        {
            this.EditStrategy.DateTimeChanged(oldDateTime, dateTime);
        }

        protected static void OnDateTimePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((DateEdit) obj).OnDateTimeChanged((System.DateTime) e.OldValue, (System.DateTime) e.NewValue);
        }

        protected virtual void OnMaxValueChanged(System.DateTime? value)
        {
            this.EditStrategy.MaxValueChanged(value);
            if (this.Calendar != null)
            {
                this.Calendar.MaxValue = this.MaxValue;
            }
        }

        protected static void OnMaxValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((DateEdit) obj).OnMaxValueChanged((System.DateTime?) e.NewValue);
        }

        protected virtual void OnMinValueChanged(System.DateTime? value)
        {
            this.EditStrategy.MinValueChanged(value);
            if (this.Calendar != null)
            {
                this.Calendar.MinValue = this.MinValue;
            }
        }

        protected static void OnMinValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((DateEdit) obj).OnMinValueChanged((System.DateTime?) e.NewValue);
        }

        protected virtual void OnShowClearButtonChanged()
        {
            this.UpdateCalendarShowClearButton();
        }

        protected static void OnShowClearButtonChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((DateEdit) obj).OnShowClearButtonChanged();
        }

        protected virtual void OnShowTodayChanged()
        {
            this.UpdateCalendarShowToday();
        }

        protected static void OnShowTodayChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((DateEdit) obj).OnShowTodayChanged();
        }

        protected virtual void OnShowWeekNumbersChanged()
        {
            if (this.Calendar != null)
            {
                this.Calendar.ShowWeekNumbers = this.ShowWeekNumbers;
            }
        }

        protected static void OnShowWeekNumbersChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((DateEdit) obj).OnShowWeekNumbersChanged();
        }

        protected virtual void OnTimePickerChanged()
        {
            if (this.TimePicker != null)
            {
                this.UpdateTimePickerValues();
            }
        }

        internal void SetDateTime(System.DateTime editValue, UpdateEditorSource updateEditorSource)
        {
            this.EditStrategy.SetDateTime(editValue, updateEditorSource);
        }

        private void UpdateCalendarShowClearButton()
        {
            this.EditStrategy.UpdateCalendarShowClearButton();
        }

        private void UpdateCalendarShowToday()
        {
            if (this.Calendar != null)
            {
                this.Calendar.ShowToday = this.ShowToday;
            }
        }

        private void UpdateCalendarValues()
        {
            System.DateTime today = this.EditStrategy.CreateValueConverter(this.EditStrategy.EditValue).Value;
            if (this.EditStrategy.IsNullValue(this.EditStrategy.EditValue))
            {
                today = System.DateTime.Today;
            }
            if ((this.MinValue != null) && (today < this.MinValue.Value))
            {
                today = this.MinValue.Value;
            }
            if ((this.MaxValue != null) && (today > this.MaxValue.Value))
            {
                today = this.MaxValue.Value;
            }
            this.Calendar.Mask = base.Mask;
            this.Calendar.DateTime = today;
            this.Calendar.MinValue = ((this.MinValue == null) || (this.TimePicker == null)) ? this.MinValue : new System.DateTime(this.MinValue.Value.Year, this.MinValue.Value.Month, this.MinValue.Value.Day);
            this.Calendar.MaxValue = ((this.MaxValue == null) || (this.TimePicker == null)) ? this.MaxValue : new System.DateTime(this.MaxValue.Value.Year, this.MaxValue.Value.Month, this.MaxValue.Value.Day);
        }

        private void UpdateTimePickerValues()
        {
            System.DateTime today = this.EditStrategy.CreateValueConverter(this.EditStrategy.EditValue).Value;
            if (this.EditStrategy.IsNullValue(this.EditStrategy.EditValue))
            {
                today = System.DateTime.Today;
            }
            if ((this.MinValue != null) && (today < this.MinValue.Value))
            {
                today = this.MinValue.Value;
            }
            if ((this.MaxValue != null) && (today > this.MaxValue.Value))
            {
                today = this.MaxValue.Value;
            }
            this.TimePicker.DateTime = today;
            this.TimePicker.MinValue = this.MinValue;
            this.TimePicker.MaxValue = this.MaxValue;
        }

        public override FrameworkElement PopupElement =>
            (FrameworkElement) this.Calendar;

        [Description("Gets or sets the editor's date/time value. This is a dependency property."), Category("Common Properties")]
        public System.DateTime DateTime
        {
            get => 
                (System.DateTime) base.GetValue(DateTimeProperty);
            set => 
                base.SetValue(DateTimeProperty, value);
        }

        [Category("Behavior"), Description("Gets or sets whether the dropdown calendar displays the Clear button. This is a dependency property.")]
        public bool ShowClearButton
        {
            get => 
                (bool) base.GetValue(ShowClearButtonProperty);
            set => 
                base.SetValue(ShowClearButtonProperty, value);
        }

        [Description("Gets or sets whether to display Today on the dropdown calendar. This is a dependency property."), Category("Behavior")]
        public bool ShowToday
        {
            get => 
                (bool) base.GetValue(ShowTodayProperty);
            set => 
                base.SetValue(ShowTodayProperty, value);
        }

        [Category("Behavior"), Description("Gets or sets whether to display week numbers in the dropdown calendar. This is a dependency property.")]
        public bool ShowWeekNumbers
        {
            get => 
                (bool) base.GetValue(ShowWeekNumbersProperty);
            set => 
                base.SetValue(ShowWeekNumbersProperty, value);
        }

        [Category("Behavior"), Description("Gets or sets the editor's minimum value. This is a dependency property.")]
        public System.DateTime? MinValue
        {
            get => 
                (System.DateTime?) base.GetValue(MinValueProperty);
            set => 
                base.SetValue(MinValueProperty, value);
        }

        [Description("Gets or sets the editor's maximum value. This is a dependency property."), Category("Behavior")]
        public System.DateTime? MaxValue
        {
            get => 
                (System.DateTime?) base.GetValue(MaxValueProperty);
            set => 
                base.SetValue(MaxValueProperty, value);
        }

        public bool AllowRoundOutOfRangeValue
        {
            get => 
                (bool) base.GetValue(AllowRoundOutOfRangeValueProperty);
            set => 
                base.SetValue(AllowRoundOutOfRangeValueProperty, value);
        }

        public DevExpress.Xpf.Editors.DateEditPopupContentType DateEditPopupContentType
        {
            get => 
                (DevExpress.Xpf.Editors.DateEditPopupContentType) base.GetValue(DateEditPopupContentTypeProperty);
            internal set => 
                base.SetValue(DateEditPopupContentTypePropertyKey, value);
        }

        [TypeConverter(typeof(NullableBoolConverter)), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool? ShowSizeGrip
        {
            get => 
                (bool?) base.GetValue(PopupBaseEdit.ShowSizeGripProperty);
            set => 
                base.SetValue(PopupBaseEdit.ShowSizeGripProperty, value);
        }

        protected internal DateEditSettings Settings =>
            (DateEditSettings) base.Settings;

        protected internal Type StyleSettingsType =>
            typeof(DateEditStyleSettingsBase);

        protected internal override MaskType DefaultMaskType =>
            MaskType.DateTime;

        protected internal bool ClosePopupOnDateNavigatorDateSelected
        {
            get
            {
                Func<DateEditNavigatorWithTimePickerStyleSettings, bool> evaluator = <>c.<>9__65_0;
                if (<>c.<>9__65_0 == null)
                {
                    Func<DateEditNavigatorWithTimePickerStyleSettings, bool> local1 = <>c.<>9__65_0;
                    evaluator = <>c.<>9__65_0 = x => x.ClosePopupOnDateNavigatorDateSelected;
                }
                return (base.StyleSettings as DateEditNavigatorWithTimePickerStyleSettings).Return<DateEditNavigatorWithTimePickerStyleSettings, bool>(evaluator, (<>c.<>9__65_1 ??= () => true));
            }
        }

        protected internal IDateEditCalendarBase Calendar
        {
            get => 
                this.calendar;
            set
            {
                if (!ReferenceEquals(this.Calendar, value))
                {
                    this.calendar = value;
                    this.OnCalendarChanged();
                }
            }
        }

        protected internal DevExpress.Xpf.Editors.TimePicker TimePicker
        {
            get => 
                this.timePicker;
            set
            {
                if (!ReferenceEquals(this.timePicker, value))
                {
                    this.timePicker = value;
                    this.OnTimePickerChanged();
                }
            }
        }

        protected DateEditStrategy EditStrategy =>
            base.EditStrategy as DateEditStrategy;

        protected internal override bool ShouldApplyPopupSize =>
            false;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DateEdit.<>c <>9 = new DateEdit.<>c();
            public static Func<DateEditNavigatorWithTimePickerStyleSettings, bool> <>9__65_0;
            public static Func<bool> <>9__65_1;
            public static Func<TimePicker, DateTime> <>9__77_0;
            public static Func<IDateEditCalendarBase, DateTime> <>9__77_2;

            internal void <.cctor>b__9_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateEdit) d).AllowRoundOutOfRangeValueChanged((bool) e.NewValue);
            }

            internal DateTime <AcceptPopupValue>b__77_0(TimePicker x) => 
                x.DateTime;

            internal DateTime <AcceptPopupValue>b__77_2(IDateEditCalendarBase x) => 
                x.DateTime;

            internal bool <get_ClosePopupOnDateNavigatorDateSelected>b__65_0(DateEditNavigatorWithTimePickerStyleSettings x) => 
                x.ClosePopupOnDateNavigatorDateSelected;

            internal bool <get_ClosePopupOnDateNavigatorDateSelected>b__65_1() => 
                true;
        }
    }
}

