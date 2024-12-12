namespace DevExpress.Xpf.Editors.Popups.Calendar
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    public class DateEditCalendar : DateEditCalendarBase
    {
        protected static readonly DependencyPropertyKey DateTimeTextPropertyKey;
        public static readonly DependencyProperty DateTimeTextProperty;
        protected static readonly DependencyPropertyKey CurrentDateTextPropertyKey;
        public static readonly DependencyProperty CurrentDateTextProperty;
        public static readonly DependencyProperty WeekdayAbbreviationStyleProperty;
        public static readonly DependencyProperty WeekNumbersStyleProperty;
        public static readonly DependencyProperty CellButtonStyleProperty;
        public static readonly DependencyProperty MonthInfoTemplateProperty;
        public static readonly DependencyProperty YearInfoTemplateProperty;
        public static readonly DependencyProperty YearsInfoTemplateProperty;
        public static readonly DependencyProperty YearsGroupInfoTemplateProperty;
        public static readonly DependencyProperty CalendarTransferStyleProperty;
        public static readonly DependencyProperty CellInactiveProperty;
        public static readonly DependencyProperty CellTodayProperty;
        public static readonly DependencyProperty CellFocusedProperty;
        public static readonly DependencyProperty CalendarProperty;
        private DateEditCalendarContent prevContent;
        private Button todayButton;
        private Button currentDateButton;
        private Button clearButton;
        private RepeatButton leftArrowButton;
        private RepeatButton rightArrowButton;
        private DateEditCalendarNavigatorBase navigator;

        static DateEditCalendar()
        {
            Type defaultValue = typeof(DateEditCalendar);
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(DateEditCalendar), new FrameworkPropertyMetadata(defaultValue));
            CurrentDateTextPropertyKey = DependencyPropertyManager.RegisterReadOnly("CurrentDateText", typeof(string), defaultValue, new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.None));
            CurrentDateTextProperty = CurrentDateTextPropertyKey.DependencyProperty;
            DateTimeTextPropertyKey = DependencyPropertyManager.RegisterReadOnly("DateTimeText", typeof(string), defaultValue, new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.None));
            DateTimeTextProperty = DateTimeTextPropertyKey.DependencyProperty;
            WeekdayAbbreviationStyleProperty = DependencyPropertyManager.Register("WeekdayAbbreviationStyle", typeof(Style), defaultValue, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
            CellButtonStyleProperty = DependencyPropertyManager.Register("CellButtonStyle", typeof(Style), defaultValue, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
            WeekNumbersStyleProperty = DependencyPropertyManager.Register("WeekNumbersStyle", typeof(Style), defaultValue, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
            MonthInfoTemplateProperty = DependencyPropertyManager.Register("MonthInfoTemplate", typeof(ControlTemplate), defaultValue, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
            YearInfoTemplateProperty = DependencyPropertyManager.Register("YearInfoTemplate", typeof(ControlTemplate), defaultValue, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
            YearsInfoTemplateProperty = DependencyPropertyManager.Register("YearsInfoTemplate", typeof(ControlTemplate), defaultValue, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
            YearsGroupInfoTemplateProperty = DependencyPropertyManager.Register("YearsGroupInfoTemplate", typeof(ControlTemplate), defaultValue, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
            CalendarTransferStyleProperty = DependencyPropertyManager.Register("CalendarTransferStyle", typeof(Style), defaultValue, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
            CellInactiveProperty = DependencyPropertyManager.RegisterAttached("CellInactive", typeof(bool), defaultValue, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
            CellTodayProperty = DependencyPropertyManager.RegisterAttached("CellToday", typeof(bool), defaultValue, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
            CellFocusedProperty = DependencyPropertyManager.RegisterAttached("CellFocused", typeof(bool), defaultValue, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
            CalendarProperty = DependencyPropertyManager.RegisterAttached("Calendar", typeof(DateEditCalendar), defaultValue, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
        }

        public DateEditCalendar()
        {
            this.UpdateDateTimeText();
            SetCalendar(this, this);
        }

        protected internal virtual DateTime AddDate(DateTime dt, int years, int months, int days)
        {
            DateTime time = new DateTime();
            if ((dt == time) && ((years < 0) || ((months < 0) || (days < 0))))
            {
                dt = this.GetMinValue();
            }
            else
            {
                try
                {
                    dt = dt.AddDays((double) days);
                    dt = dt.AddMonths(months);
                    dt = dt.AddYears(years);
                }
                catch
                {
                    if (((years < 0) || ((years == 0) && (months < 0))) || ((years == 0) && ((months == 0) && (days < 0))))
                    {
                        dt = this.GetMinValue();
                    }
                    else
                    {
                        dt = this.GetMaxValue();
                    }
                }
            }
            if (dt < this.GetMinValue())
            {
                dt = this.GetMinValue();
            }
            if (dt > this.GetMaxValue())
            {
                dt = this.GetMaxValue();
            }
            return dt;
        }

        protected virtual bool CanShift(int dir, DateTime currDate)
        {
            DateTime time = this.UpdateDate(this.ActiveContent.State, currDate, dir);
            return !this.IsSamePage(this.ActiveContent.State, currDate, time);
        }

        protected virtual bool CanShiftLeft(DateTime dt) => 
            this.CanShift(-1, dt);

        protected virtual bool CanShiftRight(DateTime dt) => 
            this.CanShift(1, dt);

        protected virtual DateEditCalendarNavigatorBase CreateNavigator(DateEditCalendar dateEditCalendar) => 
            new DateEditCalendarNavigator(this);

        protected virtual void FindButtonsInTemplate()
        {
            this.TodayButton = (Button) base.GetTemplateChild("PART_Today");
            this.UpdateTodayButtonVisibility();
            this.CurrentDateButton = (Button) base.GetTemplateChild("PART_CurrentDate");
            this.ClearButton = (Button) base.GetTemplateChild("PART_Clear");
            this.UpdateClearButtonVisibility();
            this.LeftArrowButton = (RepeatButton) base.GetTemplateChild("PART_LeftArrow");
            this.RightArrowButton = (RepeatButton) base.GetTemplateChild("PART_RightArrow");
        }

        public static DateEditCalendar GetCalendar(DependencyObject obj) => 
            (DateEditCalendar) DependencyObjectHelper.GetValueWithInheritance(obj, CalendarProperty);

        public static bool GetCellFocused(DependencyObject obj) => 
            (bool) obj.GetValue(CellFocusedProperty);

        public static bool GetCellInactive(DependencyObject obj) => 
            (bool) obj.GetValue(CellInactiveProperty);

        public static bool GetCellToday(DependencyObject obj) => 
            (bool) obj.GetValue(CellTodayProperty);

        protected internal DateTime GetMaxValue() => 
            (base.MaxValue == null) ? new DateTime(DateTime.MaxValue.Ticks, DateTimeKind.Local) : base.MaxValue.Value;

        protected internal DateTime GetMinValue() => 
            (base.MinValue == null) ? new DateTime(DateTime.MinValue.Ticks, DateTimeKind.Local) : base.MinValue.Value;

        protected virtual string GetMonthName(int month) => 
            CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[month - 1];

        private int GetRoundDay(int year, int month, int day)
        {
            int num = DateTime.DaysInMonth(year, month);
            return ((day > num) ? num : day);
        }

        protected virtual ControlTemplate GetTemplate(DateEditCalendarState state)
        {
            switch (state)
            {
                case DateEditCalendarState.Month:
                    return this.MonthInfoTemplate;

                case DateEditCalendarState.Year:
                    return this.YearInfoTemplate;

                case DateEditCalendarState.Years:
                    return this.YearsInfoTemplate;

                case DateEditCalendarState.YearsGroup:
                    return this.YearsGroupInfoTemplate;
            }
            return null;
        }

        protected virtual string GetTodayText() => 
            DateTime.Today.ToString(CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern, CultureInfo.CurrentCulture);

        protected bool IsSamePage(DateEditCalendarState state, DateTime dt1, DateTime dt2)
        {
            switch (state)
            {
                case DateEditCalendarState.Month:
                    return ((dt1.Month == dt2.Month) && (dt1.Year == dt2.Year));

                case DateEditCalendarState.Year:
                    return (dt1.Year == dt2.Year);

                case DateEditCalendarState.Years:
                    return ((dt1.Year / 10) == (dt2.Year / 10));

                case DateEditCalendarState.YearsGroup:
                    return ((dt1.Year / 100) == (dt2.Year / 100));
            }
            return false;
        }

        protected override void MaxValueChanged()
        {
            this.UpdateActiveContent();
        }

        protected override void MinValueChanged()
        {
            this.UpdateActiveContent();
        }

        protected virtual void NavigateLeft()
        {
            if (this.CanShiftLeft(base.DateTime))
            {
                base.DateTime = this.UpdateDate(this.ActiveContent.State, base.DateTime, -1);
                this.SetNewContent(base.DateTime, this.ActiveContent.State, this.GetTemplate(this.ActiveContent.State), DateEditCalendarTransferType.ShiftRight);
            }
        }

        protected virtual void NavigateRight()
        {
            if (this.CanShiftRight(base.DateTime))
            {
                base.DateTime = this.UpdateDate(this.ActiveContent.State, base.DateTime, 1);
                this.SetNewContent(base.DateTime, this.ActiveContent.State, this.GetTemplate(this.ActiveContent.State), DateEditCalendarTransferType.ShiftLeft);
            }
        }

        protected internal virtual void OnApplyContentTemplate(DateEditCalendarContent content)
        {
            if (ReferenceEquals(this.ActiveContent, content))
            {
                this.CurrentDateText = content.GetCurrentDateText();
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.OwnerDateEdit != null)
            {
                this.OwnerDateEdit.OnApplyCalendarTemplate(this);
            }
            this.FindButtonsInTemplate();
            this.CalendarTransfer = base.GetTemplateChild("PART_CalendarTransferContent") as DateEditCalendarTransferControl;
            base.DateTime = this.UpdateDate(DateEditCalendarState.Month, base.DateTime, 0);
            this.SetNewContent(base.DateTime, DateEditCalendarState.Month, this.MonthInfoTemplate, DateEditCalendarTransferType.None);
        }

        protected internal virtual void OnCellButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.ActiveContent.State == DateEditCalendarState.Month)
            {
                this.OnDayCellButtonClick(sender as Button);
            }
            else if (this.ActiveContent.State == DateEditCalendarState.Year)
            {
                this.OnMonthCellButtonClick(sender as Button);
            }
            else if (this.ActiveContent.State == DateEditCalendarState.Years)
            {
                this.OnYearCellButtonClick(sender as Button);
            }
            else
            {
                this.OnYearsGroupCellButtonClick(sender as Button);
            }
        }

        protected virtual void OnClearButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.OwnerDateEdit != null)
            {
                this.OwnerDateEdit.ClearError();
                this.OwnerDateEdit.SetCurrentValue(BaseEdit.EditValueProperty, this.OwnerDateEdit.NullValue);
                this.OwnerDateEdit.IsPopupOpen = false;
            }
        }

        protected virtual void OnCurrentDateButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.ActiveContent.State == DateEditCalendarState.Month)
            {
                this.SetNewContent(base.DateTime, DateEditCalendarState.Year, this.YearInfoTemplate, DateEditCalendarTransferType.ZoomOut);
            }
            else if (this.ActiveContent.State == DateEditCalendarState.Year)
            {
                this.SetNewContent(base.DateTime, DateEditCalendarState.Years, this.YearsInfoTemplate, DateEditCalendarTransferType.ZoomOut);
            }
            else if (this.ActiveContent.State == DateEditCalendarState.Years)
            {
                this.SetNewContent(base.DateTime, DateEditCalendarState.YearsGroup, this.YearsGroupInfoTemplate, DateEditCalendarTransferType.ZoomOut);
            }
        }

        protected override void OnDateTimeChanged()
        {
            this.UpdateDateTimeText();
            if (this.OwnerDateEdit == null)
            {
                this.SetNewContent(base.DateTime, DateEditCalendarState.Month, this.MonthInfoTemplate, DateEditCalendarTransferType.None);
            }
        }

        protected virtual void OnDayCellButtonClick(Button button)
        {
            DateTime editValue = new DateTime(((DateTime) GetDateTime(button)).Ticks, base.DateTime.Kind);
            if (button != null)
            {
                base.DateTime = editValue;
            }
            Func<DateEdit, bool> evaluator = <>c.<>9__129_0;
            if (<>c.<>9__129_0 == null)
            {
                Func<DateEdit, bool> local1 = <>c.<>9__129_0;
                evaluator = <>c.<>9__129_0 = x => x.IsReadOnly;
            }
            if (!this.OwnerDateEdit.Return<DateEdit, bool>(evaluator, (<>c.<>9__129_1 ??= () => true)))
            {
                this.OwnerDateEdit.SetDateTime(editValue, UpdateEditorSource.ValueChanging);
                this.OwnerDateEdit.ClosePopup();
            }
        }

        protected virtual void OnLeftArrowButtonClick(object sender, RoutedEventArgs e)
        {
            this.NavigateLeft();
        }

        protected virtual void OnMonthCellButtonClick(Button button)
        {
            this.SetMonth((DateTime) GetDateTime(button));
            this.SetNewContent(base.DateTime, DateEditCalendarState.Month, this.MonthInfoTemplate, DateEditCalendarTransferType.ZoomIn);
        }

        protected virtual void OnRightArrowButtonClick(object sender, RoutedEventArgs e)
        {
            this.NavigateRight();
        }

        protected override void OnShowClearButtonChanged()
        {
            this.UpdateClearButtonVisibility();
        }

        protected override void OnShowTodayChanged()
        {
            this.UpdateTodayButtonVisibility();
        }

        protected virtual void OnTodayButtonClick(object sender, RoutedEventArgs e)
        {
            DateEditCalendarTransferType transferType = (this.ActiveContent.State != DateEditCalendarState.Month) ? DateEditCalendarTransferType.ZoomIn : (((base.DateTime.Month != DateTime.Now.Month) || (base.DateTime.Year != DateTime.Now.Year)) ? DateEditCalendarTransferType.ShiftLeft : DateEditCalendarTransferType.None);
            this.SetNewContent(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, base.DateTime.Hour, base.DateTime.Minute, base.DateTime.Second, base.DateTime.Millisecond, base.DateTime.Kind), DateEditCalendarState.Month, this.MonthInfoTemplate, transferType);
            DateTime dateTime = this.ActiveContent.DateTime;
            base.DateTime = new DateTime(dateTime.Ticks, dateTime.Kind);
        }

        protected virtual void OnYearCellButtonClick(Button button)
        {
            this.SetYear((DateTime) GetDateTime(button));
            this.SetNewContent(base.DateTime, DateEditCalendarState.Year, this.YearInfoTemplate, DateEditCalendarTransferType.ZoomIn);
        }

        protected virtual void OnYearsGroupCellButtonClick(Button button)
        {
            this.SetYearGroup((DateTime) GetDateTime(button));
            this.SetNewContent(base.DateTime, DateEditCalendarState.Years, this.YearsInfoTemplate, DateEditCalendarTransferType.ZoomIn);
        }

        protected internal override bool ProcessKeyDown(KeyEventArgs e) => 
            !this.CalendarTransfer.IsAnimationInProgress ? this.Navigator.ProcessKeyDown(e) : true;

        private static void SetCalendar(DependencyObject obj, DateEditCalendar value)
        {
            obj.SetValue(CalendarProperty, value);
        }

        public static void SetCellFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(CellFocusedProperty, value);
        }

        public static void SetCellInactive(DependencyObject obj, bool value)
        {
            obj.SetValue(CellInactiveProperty, value);
        }

        public static void SetCellToday(DependencyObject obj, bool value)
        {
            obj.SetValue(CellTodayProperty, value);
        }

        protected void SetMonth(DateTime newDateTime)
        {
            base.DateTime = new DateTime(base.DateTime.Year, newDateTime.Month, this.GetRoundDay(base.DateTime.Year, newDateTime.Month, base.DateTime.Day), base.DateTime.Hour, base.DateTime.Minute, base.DateTime.Second, base.DateTime.Millisecond, base.DateTime.Kind);
        }

        protected internal virtual void SetNewContent(DateTime dt, DateEditCalendarTransferType transferType)
        {
            this.SetNewContent(dt, this.ActiveContent.State, this.GetTemplate(this.ActiveContent.State), transferType);
        }

        protected internal virtual void SetNewContent(DateTime dt, DateEditCalendarState state, ControlTemplate template, DateEditCalendarTransferType transferType)
        {
            this.prevContent = this.ActiveContent;
            if (this.CalendarTransfer != null)
            {
                DateEditCalendarContent content = new DateEditCalendarContent();
                this.CalendarTransfer.TransferType = transferType;
                content.State = state;
                content.Template = template;
                content.DateTime = dt;
                this.CalendarTransfer.Content = content;
                if (this.LeftArrowButton != null)
                {
                    this.LeftArrowButton.IsEnabled = this.CanShiftLeft(dt);
                }
                if (this.RightArrowButton != null)
                {
                    this.RightArrowButton.IsEnabled = this.CanShiftRight(dt);
                }
            }
        }

        public void SetNewDateTime(DateTime dateTime)
        {
            this.ActiveContent.DateTime = dateTime;
            this.SetNewContent(dateTime, DateEditCalendarTransferType.None);
        }

        protected void SetYear(DateTime newDateTime)
        {
            base.DateTime = new DateTime(newDateTime.Year, base.DateTime.Month, this.GetRoundDay(newDateTime.Year, base.DateTime.Month, base.DateTime.Day), base.DateTime.Hour, base.DateTime.Minute, base.DateTime.Second, base.DateTime.Millisecond, base.DateTime.Kind);
        }

        protected void SetYearGroup(DateTime newDateTime)
        {
            int num = Math.Max(newDateTime.Year, 1);
            base.DateTime = new DateTime((base.DateTime.Year % 10) + num, base.DateTime.Month, this.GetRoundDay((base.DateTime.Year % 10) + num, base.DateTime.Month, base.DateTime.Day), base.DateTime.Hour, base.DateTime.Minute, base.DateTime.Second, base.DateTime.Millisecond, base.DateTime.Kind);
        }

        protected override void ShowWeekNumbersPropertySet(bool value)
        {
        }

        protected virtual void SubscribeClearButtonEvent()
        {
            this.ClearButton.Click += new RoutedEventHandler(this.OnClearButtonClick);
        }

        protected virtual void SubscribeCurrentDateButtonEvent()
        {
            this.CurrentDateButton.Click += new RoutedEventHandler(this.OnCurrentDateButtonClick);
        }

        protected virtual void SubscribeLeftArrowButtonEvent()
        {
            this.LeftArrowButton.Click += new RoutedEventHandler(this.OnLeftArrowButtonClick);
        }

        protected virtual void SubscribeRightArrowButtonEvent()
        {
            this.RightArrowButton.Click += new RoutedEventHandler(this.OnRightArrowButtonClick);
        }

        protected virtual void SubscribeTodayButtonEvents()
        {
            this.TodayButton.Click += new RoutedEventHandler(this.OnTodayButtonClick);
        }

        protected virtual void UnsubscribeClearButtonEvent()
        {
            this.ClearButton.Click -= new RoutedEventHandler(this.OnClearButtonClick);
        }

        protected virtual void UnsubscribeCurrentDateButtonEvent()
        {
            this.CurrentDateButton.Click -= new RoutedEventHandler(this.OnCurrentDateButtonClick);
        }

        protected virtual void UnsubscribeLeftArrowButtonEvent()
        {
            this.LeftArrowButton.Click -= new RoutedEventHandler(this.OnLeftArrowButtonClick);
        }

        protected virtual void UnsubscribeRightArrowButtonEvent()
        {
            this.RightArrowButton.Click -= new RoutedEventHandler(this.OnRightArrowButtonClick);
        }

        protected virtual void UnsubscribeTodayButtonEvent()
        {
            this.TodayButton.Click -= new RoutedEventHandler(this.OnTodayButtonClick);
        }

        protected virtual void UpdateActiveContent()
        {
            if (this.ActiveContent != null)
            {
                this.SetNewContent(base.DateTime, DateEditCalendarTransferType.None);
            }
        }

        private void UpdateButtonVisibility(Button button, bool show)
        {
            if (button != null)
            {
                button.Visibility = show ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void UpdateClearButtonVisibility()
        {
            this.UpdateButtonVisibility(this.ClearButton, base.ShowClearButton);
        }

        protected internal virtual DateTime UpdateDate(DateEditCalendarState state, DateTime dt, int dir)
        {
            int years = 0;
            int months = 0;
            if (state == DateEditCalendarState.Month)
            {
                months = dir;
            }
            else if (state == DateEditCalendarState.Year)
            {
                years = dir;
            }
            else if (state == DateEditCalendarState.Years)
            {
                if (((dt.Year == 10) && (dir < 0)) || ((dt.Year == 1) && (dir > 0)))
                {
                    years = dir * 9;
                }
                else
                {
                    years = dir * 10;
                }
            }
            else if (state == DateEditCalendarState.YearsGroup)
            {
                if (((dt.Year == 100) && (dir < 0)) || ((dt.Year == 1) && (dir > 0)))
                {
                    years = dir * 0x63;
                }
                else
                {
                    years = dir * 100;
                }
            }
            return this.AddDate(dt, years, months, 0);
        }

        protected virtual void UpdateDateTimeText()
        {
            this.DateTimeText = this.GetTodayText();
        }

        private void UpdateTodayButtonVisibility()
        {
            this.UpdateButtonVisibility(this.TodayButton, base.ShowToday);
        }

        public ControlTemplate MonthInfoTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(MonthInfoTemplateProperty);
            set => 
                base.SetValue(MonthInfoTemplateProperty, value);
        }

        public ControlTemplate YearInfoTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(YearInfoTemplateProperty);
            set => 
                base.SetValue(YearInfoTemplateProperty, value);
        }

        public ControlTemplate YearsInfoTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(YearsInfoTemplateProperty);
            set => 
                base.SetValue(YearsInfoTemplateProperty, value);
        }

        public ControlTemplate YearsGroupInfoTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(YearsGroupInfoTemplateProperty);
            set => 
                base.SetValue(YearsGroupInfoTemplateProperty, value);
        }

        public Style CalendarTransferStyle
        {
            get => 
                (Style) base.GetValue(CalendarTransferStyleProperty);
            set => 
                base.SetValue(CalendarTransferStyleProperty, value);
        }

        public Style WeekNumbersStyle
        {
            get => 
                (Style) base.GetValue(WeekNumbersStyleProperty);
            set => 
                base.SetValue(WeekNumbersStyleProperty, value);
        }

        public Style WeekdayAbbreviationStyle
        {
            get => 
                (Style) base.GetValue(WeekdayAbbreviationStyleProperty);
            set => 
                base.SetValue(WeekdayAbbreviationStyleProperty, value);
        }

        public Style CellButtonStyle
        {
            get => 
                (Style) base.GetValue(CellButtonStyleProperty);
            set => 
                base.SetValue(CellButtonStyleProperty, value);
        }

        public string CurrentDateText
        {
            get => 
                (string) base.GetValue(CurrentDateTextProperty);
            protected internal set => 
                base.SetValue(CurrentDateTextPropertyKey, value);
        }

        public string DateTimeText
        {
            get => 
                (string) base.GetValue(DateTimeTextProperty);
            protected internal set => 
                base.SetValue(DateTimeTextPropertyKey, value);
        }

        protected internal DateEditCalendarContent ActiveContent =>
            (this.CalendarTransfer == null) ? null : (this.CalendarTransfer.Content as DateEditCalendarContent);

        protected internal DateEditCalendarContent PrevContent =>
            this.prevContent;

        public DateEdit OwnerDateEdit =>
            BaseEdit.GetOwnerEdit(this) as DateEdit;

        protected Button TodayButton
        {
            get => 
                this.todayButton;
            set
            {
                if (this.TodayButton != null)
                {
                    this.UnsubscribeTodayButtonEvent();
                }
                this.todayButton = value;
                if (this.TodayButton != null)
                {
                    this.SubscribeTodayButtonEvents();
                }
            }
        }

        protected internal Button CurrentDateButton
        {
            get => 
                this.currentDateButton;
            set
            {
                if (this.CurrentDateButton != null)
                {
                    this.UnsubscribeCurrentDateButtonEvent();
                }
                this.currentDateButton = value;
                if (this.CurrentDateButton != null)
                {
                    this.SubscribeCurrentDateButtonEvent();
                }
            }
        }

        protected internal Button ClearButton
        {
            get => 
                this.clearButton;
            set
            {
                if (this.ClearButton != null)
                {
                    this.UnsubscribeClearButtonEvent();
                }
                this.clearButton = value;
                if (this.ClearButton != null)
                {
                    this.SubscribeClearButtonEvent();
                }
            }
        }

        protected internal RepeatButton LeftArrowButton
        {
            get => 
                this.leftArrowButton;
            set
            {
                if (this.LeftArrowButton != null)
                {
                    this.UnsubscribeLeftArrowButtonEvent();
                }
                this.leftArrowButton = value;
                if (this.LeftArrowButton != null)
                {
                    this.SubscribeLeftArrowButtonEvent();
                }
            }
        }

        protected internal RepeatButton RightArrowButton
        {
            get => 
                this.rightArrowButton;
            set
            {
                if (this.RightArrowButton != null)
                {
                    this.UnsubscribeRightArrowButtonEvent();
                }
                this.rightArrowButton = value;
                if (this.RightArrowButton != null)
                {
                    this.SubscribeRightArrowButtonEvent();
                }
            }
        }

        public DateEditCalendar Calendar
        {
            get => 
                (DateEditCalendar) base.GetValue(CalendarProperty);
            set => 
                base.SetValue(CalendarProperty, value);
        }

        protected internal DateEditCalendarTransferControl CalendarTransfer { get; private set; }

        protected DateEditCalendarNavigatorBase Navigator
        {
            get
            {
                this.navigator ??= this.CreateNavigator(this);
                return this.navigator;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DateEditCalendar.<>c <>9 = new DateEditCalendar.<>c();
            public static Func<DateEdit, bool> <>9__129_0;
            public static Func<bool> <>9__129_1;

            internal bool <OnDayCellButtonClick>b__129_0(DateEdit x) => 
                x.IsReadOnly;

            internal bool <OnDayCellButtonClick>b__129_1() => 
                true;
        }
    }
}

