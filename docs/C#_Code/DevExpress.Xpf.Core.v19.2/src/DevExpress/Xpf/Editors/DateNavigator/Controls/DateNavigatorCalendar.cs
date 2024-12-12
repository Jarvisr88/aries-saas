namespace DevExpress.Xpf.Editors.DateNavigator.Controls
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.DateNavigator;
    using DevExpress.Xpf.Editors.Validation.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;

    public class DateNavigatorCalendar : ContentControl
    {
        protected static readonly DependencyPropertyKey DateTimeTextPropertyKey;
        public static readonly DependencyProperty DateTimeTextProperty;
        protected static readonly DependencyPropertyKey CurrentDateTextPropertyKey;
        public static readonly DependencyProperty CurrentDateTextProperty;
        public static readonly DependencyProperty WeekdayAbbreviationStyleProperty;
        public static readonly DependencyProperty WeekNumberStyleProperty;
        public static readonly DependencyProperty CellButtonStyleProperty;
        public static readonly DependencyProperty MonthInfoTemplateProperty;
        public static readonly DependencyProperty YearInfoTemplateProperty;
        public static readonly DependencyProperty YearsInfoTemplateProperty;
        public static readonly DependencyProperty YearsRangeInfoTemplateProperty;
        public static readonly DependencyProperty CalendarTransferStyleProperty;
        public static readonly DependencyProperty DateTimeProperty;
        public static readonly DependencyProperty MinValueProperty;
        public static readonly DependencyProperty MaxValueProperty;
        public static readonly DependencyProperty CalendarProperty;
        public static readonly DependencyProperty ShowWeekNumbersProperty;
        public static readonly DependencyProperty StateProperty;
        public static readonly DependencyProperty ResizeToMaxContentProperty;
        public static readonly DependencyProperty CellStateProperty;
        public static readonly DependencyProperty WeekNumberRuleProperty;
        public static readonly DependencyProperty HighlightSpecialDatesProperty;
        public static readonly DependencyProperty HighlightHolidaysProperty;
        public static readonly DependencyProperty FirstDayOfWeekProperty;
        public static readonly DependencyProperty WeekFirstDateProperty;
        internal static readonly DependencyPropertyKey PositionPropertyKey;
        public static readonly DependencyProperty PositionProperty;
        internal static readonly DependencyPropertyKey HeaderTypePropertyKey;
        public static readonly DependencyProperty HeaderTypeProperty;
        private WeakReference ownerReference;
        private bool wasLayoutUpdated;
        private DateNavigatorCalendarNavigatorBase navigator;

        public event DateNavigatorCalendarButtonClickEventHandler ButtonClick;

        static DateNavigatorCalendar()
        {
            Type ownerType = typeof(DateNavigatorCalendar);
            DateTimeProperty = DependencyPropertyManager.RegisterAttached("DateTime", typeof(System.DateTime), ownerType, new FrameworkPropertyMetadata(System.DateTime.MinValue, new PropertyChangedCallback(DateNavigatorCalendar.OnDateTimePropertyChanged)));
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(DateNavigatorCalendar), new FrameworkPropertyMetadata(ownerType));
            CurrentDateTextPropertyKey = DependencyPropertyManager.RegisterReadOnly("CurrentDateText", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.None));
            CurrentDateTextProperty = CurrentDateTextPropertyKey.DependencyProperty;
            DateTimeTextPropertyKey = DependencyPropertyManager.RegisterReadOnly("DateTimeText", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.None));
            DateTimeTextProperty = DateTimeTextPropertyKey.DependencyProperty;
            WeekdayAbbreviationStyleProperty = DependencyPropertyManager.Register("WeekdayAbbreviationStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
            CellButtonStyleProperty = DependencyPropertyManager.Register("CellButtonStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
            WeekNumberStyleProperty = DependencyPropertyManager.Register("WeekNumberStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
            MonthInfoTemplateProperty = DependencyPropertyManager.Register("MonthInfoTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
            YearInfoTemplateProperty = DependencyPropertyManager.Register("YearInfoTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
            YearsInfoTemplateProperty = DependencyPropertyManager.Register("YearsInfoTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
            YearsRangeInfoTemplateProperty = DependencyPropertyManager.Register("YearsRangeInfoTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
            CalendarTransferStyleProperty = DependencyPropertyManager.Register("CalendarTransferStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
            MinValueProperty = DependencyPropertyManager.Register("MinValue", typeof(System.DateTime?), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));
            MaxValueProperty = DependencyPropertyManager.Register("MaxValue", typeof(System.DateTime?), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));
            CalendarProperty = DependencyPropertyManager.RegisterAttached("Calendar", typeof(DateNavigatorCalendar), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
            ShowWeekNumbersProperty = DependencyPropertyManager.RegisterAttached("ShowWeekNumbers", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback(DateNavigatorCalendar.OnShowWeekNumbersChanged)));
            StateProperty = DependencyPropertyManager.Register("State", typeof(DateNavigatorCalendarView), ownerType, new FrameworkPropertyMetadata(new PropertyChangedCallback(DateNavigatorCalendar.OnStatePropertyChanged)));
            ResizeToMaxContentProperty = DependencyPropertyManager.Register("ResizeToMaxContent", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, new PropertyChangedCallback(DateNavigatorCalendar.OnResizeToMaxContentPropertyChanged)));
            CellStateProperty = DependencyPropertyManager.RegisterAttached("CellState", typeof(DateNavigatorCalendarCellState), ownerType, new PropertyMetadata(null));
            WeekNumberRuleProperty = DependencyPropertyManager.Register("WeekNumberRule", typeof(CalendarWeekRule?), typeof(DateNavigatorCalendar), new PropertyMetadata(null, (d, e) => ((DateNavigatorCalendar) d).PropertyChangedWeekNumberRule((CalendarWeekRule?) e.OldValue)));
            HighlightSpecialDatesProperty = DependencyPropertyManager.Register("HighlightSpecialDates", typeof(bool), ownerType, new PropertyMetadata(true, (d, e) => ((DateNavigatorCalendar) d).PropertyChangedHighlightSpecialDates((bool) e.OldValue)));
            HighlightHolidaysProperty = DependencyPropertyManager.Register("HighlightHolidays", typeof(bool), ownerType, new PropertyMetadata(true, (d, e) => ((DateNavigatorCalendar) d).PropertyChangedHighlightHolidays((bool) e.OldValue)));
            FirstDayOfWeekProperty = DependencyPropertyManager.Register("FirstDayOfWeek", typeof(DayOfWeek?), ownerType, new PropertyMetadata(null, (d, e) => ((DateNavigatorCalendar) d).PropertyChangedFirstDayOfWeek((DayOfWeek?) e.OldValue)));
            WeekFirstDateProperty = DependencyPropertyManager.RegisterAttached("WeekFirstDate", typeof(System.DateTime), ownerType, new PropertyMetadata(System.DateTime.MinValue));
            PositionPropertyKey = DependencyPropertyManager.RegisterReadOnly("Position", typeof(DateNavigatorCalendarPosition), ownerType, new FrameworkPropertyMetadata((obj, args) => ((DateNavigatorCalendar) obj).OnPositionChanged()));
            PositionProperty = PositionPropertyKey.DependencyProperty;
            HeaderTypePropertyKey = DependencyPropertyManager.RegisterReadOnly("HeaderType", typeof(DateNavigatorCalendarHeaderType), ownerType, new FrameworkPropertyMetadata(DateNavigatorCalendarHeaderType.None));
            HeaderTypeProperty = HeaderTypePropertyKey.DependencyProperty;
        }

        public DateNavigatorCalendar(IDateNavigatorCalendarOwner owner)
        {
            this.ownerReference = new WeakReference(owner);
            this.UpdateDateTimeText();
            SetCalendar(this, this);
            base.LayoutUpdated += new EventHandler(this.OnLayoutUpdated);
            this.ArrowRightCommand = DelegateCommandFactory.Create(new Action(this.OnArrowRightClick), new Func<bool>(this.CanArrowRightClick));
            this.ArrowLeftCommand = DelegateCommandFactory.Create(new Action(this.OnArrowLeftClick), new Func<bool>(this.CanArrowLeftClick));
            this.HeaderClickCommand = DelegateCommandFactory.Create(new Action(this.OnHeaderClick));
        }

        protected internal virtual System.DateTime AddDate(System.DateTime dt, int years, int months, int days)
        {
            System.DateTime time = new System.DateTime();
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

        protected internal virtual bool CanAddDate(System.DateTime date)
        {
            switch (this.State)
            {
                case DateNavigatorCalendarView.Month:
                {
                    System.DateTime time2 = new System.DateTime(date.Year, date.Month, date.Day, 0x17, 0x3b, 0x3b);
                    System.DateTime? minValue = this.MinValue;
                    if ((minValue != null) ? (time2 < minValue.GetValueOrDefault()) : false)
                    {
                        return false;
                    }
                    time2 = new System.DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                    minValue = this.MaxValue;
                    if ((minValue != null) ? (time2 > minValue.GetValueOrDefault()) : false)
                    {
                        return false;
                    }
                    date = new System.DateTime(date.Year, date.Month, 1);
                    System.DateTime time = new System.DateTime(this.DateTime.Year, this.DateTime.Month, 1);
                    return (((this.Position != DateNavigatorCalendarPosition.First) || (date <= time)) ? (((this.Position != DateNavigatorCalendarPosition.Intermediate) || !(date != time)) ? ((this.Position != DateNavigatorCalendarPosition.Last) || (date >= time)) : false) : false);
                }
                case DateNavigatorCalendarView.Years:
                    return (((date >= this.GetMinValue()) || (date.Year >= this.GetMinValue().Year)) ? ((date <= this.GetMaxValue()) && (((this.Position != DateNavigatorCalendarPosition.First) || (date.Year <= (this.DateTime.Year + 9))) ? (((this.Position != DateNavigatorCalendarPosition.Intermediate) || ((date.Year >= this.DateTime.Year) && (date.Year <= (this.DateTime.Year + 9)))) ? ((this.Position != DateNavigatorCalendarPosition.Last) || (date.Year >= this.DateTime.Year)) : false) : false)) : false);

                case DateNavigatorCalendarView.YearsRange:
                {
                    int num = ((date.Year / 10) * 10) + 9;
                    return (((date >= this.GetMinValue()) || (num >= this.GetMinValue().Year)) ? ((date <= this.GetMaxValue()) && (((this.Position != DateNavigatorCalendarPosition.First) || (date.Year <= (this.DateTime.Year + 0x63))) ? (((this.Position != DateNavigatorCalendarPosition.Intermediate) || ((date.Year >= this.DateTime.Year) && (date.Year <= (this.DateTime.Year + 0x63)))) ? ((this.Position != DateNavigatorCalendarPosition.Last) || (date.Year >= this.DateTime.Year)) : false) : false)) : false);
                }
            }
            return true;
        }

        private bool CanArrowLeftClick()
        {
            DevExpress.Xpf.Editors.DateNavigator.DateNavigator input = DevExpress.Xpf.Editors.DateNavigator.DateNavigator.GetNavigator(this);
            Func<DevExpress.Xpf.Editors.DateNavigator.DateNavigator, bool> evaluator = <>c.<>9__71_0;
            if (<>c.<>9__71_0 == null)
            {
                Func<DevExpress.Xpf.Editors.DateNavigator.DateNavigator, bool> local1 = <>c.<>9__71_0;
                evaluator = <>c.<>9__71_0 = x => x.MinValue != null;
            }
            return (input.Return<DevExpress.Xpf.Editors.DateNavigator.DateNavigator, bool>(evaluator, (<>c.<>9__71_1 ??= () => false)) ? (input.LastVisibleDate.CompareTo(input.MinValue.Value) >= 0) : true);
        }

        private bool CanArrowRightClick()
        {
            DevExpress.Xpf.Editors.DateNavigator.DateNavigator input = DevExpress.Xpf.Editors.DateNavigator.DateNavigator.GetNavigator(this);
            Func<DevExpress.Xpf.Editors.DateNavigator.DateNavigator, bool> evaluator = <>c.<>9__69_0;
            if (<>c.<>9__69_0 == null)
            {
                Func<DevExpress.Xpf.Editors.DateNavigator.DateNavigator, bool> local1 = <>c.<>9__69_0;
                evaluator = <>c.<>9__69_0 = x => x.MaxValue != null;
            }
            return (input.Return<DevExpress.Xpf.Editors.DateNavigator.DateNavigator, bool>(evaluator, (<>c.<>9__69_1 ??= () => false)) ? (input.FirstVisibleDate.CompareTo(input.MaxValue.Value) <= 0) : true);
        }

        protected virtual bool CanShift(int dir, System.DateTime currDate)
        {
            System.DateTime time = this.UpdateDate(this.ActiveContent.State, currDate, dir);
            return !this.IsSamePage(this.ActiveContent.State, currDate, time);
        }

        protected virtual bool CanShiftLeft(System.DateTime dt) => 
            this.CanShift(-1, dt);

        protected virtual bool CanShiftRight(System.DateTime dt) => 
            this.CanShift(1, dt);

        protected virtual DateNavigatorCalendarNavigatorBase CreateNavigator(DateNavigatorCalendar dateEditCalendar) => 
            new DateNavigatorCalendarNavigator(this);

        protected virtual void FindButtonsInTemplate()
        {
        }

        public static DateNavigatorCalendar GetCalendar(DependencyObject obj) => 
            (DateNavigatorCalendar) DependencyObjectHelper.GetValueWithInheritance(obj, CalendarProperty);

        public DateNavigatorCalendarCellButton GetCellButton(System.DateTime dt) => 
            this.Content.GetCellButton(dt);

        public static DateNavigatorCalendarCellState GetCellState(DependencyObject obj) => 
            (DateNavigatorCalendarCellState) obj.GetValue(CellStateProperty);

        public void GetDateRange(bool excludeInactiveContent, out System.DateTime firstDate, out System.DateTime lastDate)
        {
            if (this.Content.ReadLocalValue(Control.TemplateProperty) == DependencyProperty.UnsetValue)
            {
                this.wasLayoutUpdated = true;
                this.UpdateContent();
            }
            this.Content.GetDateRange(excludeInactiveContent, out firstDate, out lastDate);
        }

        public static System.DateTime GetDateTime(DependencyObject obj) => 
            (System.DateTime) obj.GetValue(DateTimeProperty);

        protected internal System.DateTime GetMaxValue() => 
            (this.MaxValue == null) ? System.DateTime.MaxValue : this.MaxValue.Value;

        protected internal System.DateTime GetMinValue() => 
            (this.MinValue == null) ? System.DateTime.MinValue : this.MinValue.Value;

        protected virtual string GetMonthName(int month) => 
            CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[month - 1];

        private int GetRoundDay(int year, int month, int day)
        {
            int num = System.DateTime.DaysInMonth(year, month);
            return ((day > num) ? num : day);
        }

        protected virtual ControlTemplate GetTemplate(DateNavigatorCalendarView state)
        {
            switch (state)
            {
                case DateNavigatorCalendarView.Month:
                    return this.MonthInfoTemplate;

                case DateNavigatorCalendarView.Year:
                    return this.YearInfoTemplate;

                case DateNavigatorCalendarView.Years:
                    return this.YearsInfoTemplate;

                case DateNavigatorCalendarView.YearsRange:
                    return this.YearsRangeInfoTemplate;
            }
            return null;
        }

        protected virtual string GetTodayText()
        {
            object[] args = new object[] { System.DateTime.Today };
            return string.Format(CultureInfo.CurrentCulture, "{0:d MMMM yyyy}", args);
        }

        public static System.DateTime GetWeekFirstDate(DependencyObject obj) => 
            (System.DateTime) obj.GetValue(WeekFirstDateProperty);

        protected internal System.DateTime GetWeekFirstDateByDate(System.DateTime dt) => 
            this.Content.GetWeekFirstDateByDate(dt);

        protected internal virtual void HitTest(UIElement element, out System.DateTime? buttonDate, out DateNavigatorCalendarButtonKind buttonKind)
        {
            DateNavigatorCalendarCellButton button = LayoutHelper.FindParentObject<DateNavigatorCalendarCellButton>(element);
            if ((button != null) && LayoutHelper.IsChildElement(this, button))
            {
                buttonDate = new System.DateTime?(GetDateTime(button));
                buttonKind = DateNavigatorCalendarButtonKind.Date;
            }
            else
            {
                TextBlock block = element as TextBlock;
                if ((block != null) && (block.ReadLocalValue(WeekFirstDateProperty) != DependencyProperty.UnsetValue))
                {
                    buttonDate = new System.DateTime?(GetWeekFirstDate(block));
                    buttonKind = DateNavigatorCalendarButtonKind.WeekNumber;
                }
                else
                {
                    buttonDate = 0;
                    buttonKind = DateNavigatorCalendarButtonKind.None;
                }
            }
        }

        protected bool IsSamePage(DateNavigatorCalendarView state, System.DateTime dt1, System.DateTime dt2)
        {
            switch (state)
            {
                case DateNavigatorCalendarView.Month:
                    return ((dt1.Month == dt2.Month) && (dt1.Year == dt2.Year));

                case DateNavigatorCalendarView.Year:
                    return (dt1.Year == dt2.Year);

                case DateNavigatorCalendarView.Years:
                    return ((dt1.Year / 10) == (dt2.Year / 10));

                case DateNavigatorCalendarView.YearsRange:
                    return ((dt1.Year / 100) == (dt2.Year / 100));
            }
            return false;
        }

        protected internal virtual void OnApplyContentTemplate(DateNavigatorCalendarContent content)
        {
            this.UpdateCurrentDateText();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Content = base.GetTemplateChild("PART_Content") as DateNavigatorCalendarContent;
            this.UpdateContentPaddingPanel();
            this.FindButtonsInTemplate();
            if (this.ResizeToMaxContent)
            {
                this.UpdateContent();
            }
        }

        private void OnArrowLeftClick()
        {
            DevExpress.Xpf.Editors.DateNavigator.DateNavigator.GetNavigator(this).ArrowClick(false);
        }

        private void OnArrowRightClick()
        {
            DevExpress.Xpf.Editors.DateNavigator.DateNavigator.GetNavigator(this).ArrowClick(true);
        }

        protected internal virtual void OnButtonClick(System.DateTime buttonDate, DateNavigatorCalendarButtonKind buttonKind)
        {
            this.RaiseButtonClick(buttonDate, buttonKind);
        }

        protected virtual void OnDateTimeChanged()
        {
            this.UpdateDateTimeText();
            if ((this.Content != null) && (this.Content.DateTime != this.DateTime))
            {
                this.Content.DateTime = this.DateTime;
                this.Content.FillContent();
            }
            this.UpdateCurrentDateText();
        }

        private static void OnDateTimePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is DateNavigatorCalendar)
            {
                ((DateNavigatorCalendar) obj).OnDateTimeChanged();
            }
        }

        protected virtual void OnDayCellButtonClick(Button button)
        {
            if (this.OwnerDateEdit == null)
            {
                if (button != null)
                {
                    this.DateTime = GetDateTime(button);
                }
            }
            else
            {
                if (!this.OwnerDateEdit.IsReadOnly)
                {
                    this.OwnerDateEdit.SetDateTime(GetDateTime(button), UpdateEditorSource.ValueChanging);
                }
                if ((this.OwnerDateEdit.TimePicker == null) || this.OwnerDateEdit.ClosePopupOnDateNavigatorDateSelected)
                {
                    this.OwnerDateEdit.IsPopupOpen = false;
                }
            }
        }

        private void OnHeaderClick()
        {
            DevExpress.Xpf.Editors.DateNavigator.DateNavigator.GetNavigator(this).OnCurrentDateButtonClick(this, null);
        }

        protected virtual void OnLayoutUpdated(object sender, EventArgs e)
        {
            base.LayoutUpdated -= new EventHandler(this.OnLayoutUpdated);
            this.wasLayoutUpdated = true;
            if (!this.ResizeToMaxContent)
            {
                this.UpdateContent();
            }
        }

        protected virtual void OnMonthCellButtonClick(Button button)
        {
            this.SetMonth(GetDateTime(button));
        }

        protected virtual void OnPositionChanged()
        {
            if (this.Content != null)
            {
                this.Content.FillContent();
            }
        }

        protected virtual void OnResizeToMaxContentChanged()
        {
            this.UpdateContentPaddingPanel();
        }

        private static void OnResizeToMaxContentPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((DateNavigatorCalendar) obj).OnResizeToMaxContentChanged();
        }

        protected virtual void OnShowWeekNumbersChanged()
        {
            if (this.Content != null)
            {
                this.Content.FillContent();
            }
        }

        private static void OnShowWeekNumbersChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is DateNavigatorCalendar)
            {
                ((DateNavigatorCalendar) obj).OnShowWeekNumbersChanged();
            }
        }

        protected virtual void OnStateChanged()
        {
            this.UpdateContent();
        }

        private static void OnStatePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((DateNavigatorCalendar) obj).OnStateChanged();
        }

        protected virtual void OnYearCellButtonClick(Button button)
        {
            this.SetYear(GetDateTime(button));
        }

        protected virtual void OnYearsGroupCellButtonClick(Button button)
        {
            this.SetYearGroup(GetDateTime(button));
        }

        protected virtual void PropertyChangedFirstDayOfWeek(DayOfWeek? oldValue)
        {
            this.RecreateContent();
        }

        protected virtual void PropertyChangedHighlightHolidays(bool oldValue)
        {
            this.UpdateHolidays();
        }

        protected virtual void PropertyChangedHighlightSpecialDates(bool oldValue)
        {
            this.UpdateSpecialDates();
        }

        protected virtual void PropertyChangedWeekNumberRule(CalendarWeekRule? oldValue)
        {
            this.RecreateContent();
        }

        protected virtual void RaiseButtonClick(System.DateTime buttonDate, DateNavigatorCalendarButtonKind buttonKind)
        {
            if (this.ButtonClick != null)
            {
                this.ButtonClick(this, new DateNavigatorCalendarButtonClickEventArgs(buttonDate, buttonKind));
            }
        }

        protected virtual void RecreateContent()
        {
            if (this.Content != null)
            {
                this.Content.FillContent();
            }
        }

        private static void SetCalendar(DependencyObject obj, DateNavigatorCalendar value)
        {
            obj.SetValue(CalendarProperty, value);
        }

        protected internal static void SetCellDisabled(DependencyObject obj, bool value)
        {
            SetCellStateFlag(obj, DateNavigatorCalendarCellState.Disabled, value);
        }

        protected internal static void SetCellFocused(DependencyObject obj, bool value)
        {
            SetCellStateFlag(obj, DateNavigatorCalendarCellState.Focused, value);
        }

        protected internal static void SetCellHoliday(DependencyObject obj, bool value)
        {
            SetCellStateFlag(obj, DateNavigatorCalendarCellState.Holiday, value);
        }

        protected internal static void SetCellInactive(DependencyObject obj, bool value)
        {
            SetCellStateFlag(obj, DateNavigatorCalendarCellState.Inactive, value);
        }

        protected internal static void SetCellSelected(DependencyObject obj, bool value)
        {
            SetCellStateFlag(obj, DateNavigatorCalendarCellState.Selected, value);
        }

        protected internal static void SetCellSpecial(DependencyObject obj, bool value)
        {
            SetCellStateFlag(obj, DateNavigatorCalendarCellState.Special, value);
        }

        public static void SetCellState(DependencyObject obj, DateNavigatorCalendarCellState value)
        {
            obj.SetValue(CellStateProperty, value);
        }

        protected internal static void SetCellStateFlag(DependencyObject obj, DateNavigatorCalendarCellState flag, bool value)
        {
            DateNavigatorCalendarCellState cellState = GetCellState(obj);
            cellState = !value ? (cellState & ~flag) : (cellState | flag);
            obj.SetValue(CellStateProperty, cellState);
        }

        protected internal static void SetCellToday(DependencyObject obj, bool value)
        {
            SetCellStateFlag(obj, DateNavigatorCalendarCellState.Today, value);
        }

        public static void SetDateTime(DependencyObject obj, object value)
        {
            obj.SetValue(DateTimeProperty, value);
        }

        protected void SetMonth(System.DateTime newDateTime)
        {
            this.DateTime = new System.DateTime(this.DateTime.Year, newDateTime.Month, this.GetRoundDay(this.DateTime.Year, newDateTime.Month, this.DateTime.Day), this.DateTime.Hour, this.DateTime.Minute, this.DateTime.Second, this.DateTime.Millisecond);
        }

        protected internal virtual void SetNewContent(System.DateTime dt, DateNavigatorCalendarTransferType transferType)
        {
        }

        public void SetNewDateTime(System.DateTime dateTime)
        {
            this.ActiveContent.DateTime = dateTime;
            this.SetNewContent(dateTime, DateNavigatorCalendarTransferType.None);
        }

        public static void SetWeekFirstDate(DependencyObject obj, object value)
        {
            obj.SetValue(WeekFirstDateProperty, value);
        }

        protected void SetYear(System.DateTime newDateTime)
        {
            this.DateTime = new System.DateTime(newDateTime.Year, this.DateTime.Month, this.GetRoundDay(newDateTime.Year, this.DateTime.Month, this.DateTime.Day), this.DateTime.Hour, this.DateTime.Minute, this.DateTime.Second, this.DateTime.Millisecond);
        }

        protected void SetYearGroup(System.DateTime newDateTime)
        {
            int num = Math.Max(newDateTime.Year, 1);
            this.DateTime = new System.DateTime((this.DateTime.Year % 10) + num, this.DateTime.Month, this.GetRoundDay((this.DateTime.Year % 10) + num, this.DateTime.Month, this.DateTime.Day), this.DateTime.Hour, this.DateTime.Minute, this.DateTime.Second, this.DateTime.Millisecond);
        }

        protected internal void UpdateCellStates()
        {
            if (this.Content != null)
            {
                this.Content.UpdateCellInfos();
            }
        }

        protected internal virtual void UpdateContent()
        {
            if ((this.Content != null) && (this.wasLayoutUpdated || this.ResizeToMaxContent))
            {
                this.Content.DateTime = this.DateTime;
                this.Content.State = this.State;
                this.Content.Template = this.GetTemplate(this.State);
            }
        }

        private void UpdateContentPaddingPanel()
        {
            Panel templateChild = base.GetTemplateChild("PART_ContentPaddingPanel") as Panel;
            if (templateChild != null)
            {
                if (!this.ResizeToMaxContent)
                {
                    templateChild.Children.Clear();
                }
                else if (templateChild.Children.Count == 0)
                {
                    foreach (DateNavigatorCalendarView view in typeof(DateNavigatorCalendarView).GetValues())
                    {
                        DateNavigatorCalendarContent content1 = new DateNavigatorCalendarContent();
                        content1.IsEnabled = false;
                        content1.Opacity = 0.0;
                        content1.State = view;
                        DateNavigatorCalendarContent element = content1;
                        Binding binding = new Binding(view.ToString() + "InfoTemplate");
                        binding.Source = this;
                        element.SetBinding(Control.TemplateProperty, binding);
                        templateChild.Children.Add(element);
                    }
                }
            }
        }

        protected internal void UpdateCurrentDateText()
        {
            if (this.Content != null)
            {
                this.CurrentDateText = this.Content.GetCurrentDateText();
            }
        }

        protected internal virtual System.DateTime UpdateDate(DateNavigatorCalendarView state, System.DateTime dt, int dir)
        {
            int years = 0;
            int months = 0;
            if (state == DateNavigatorCalendarView.Month)
            {
                months = dir;
            }
            else if (state == DateNavigatorCalendarView.Year)
            {
                years = dir;
            }
            else if (state == DateNavigatorCalendarView.Years)
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
            else if (state == DateNavigatorCalendarView.YearsRange)
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

        protected internal void UpdateDisabledDates()
        {
            if (this.Content != null)
            {
                this.Content.UpdateDisabledDates();
            }
        }

        protected internal void UpdateHolidays()
        {
            if (this.Content != null)
            {
                this.Content.UpdateHolidays();
            }
        }

        protected internal void UpdateSelectedDates()
        {
            if (this.Content != null)
            {
                this.Content.UpdateSelectedDates();
            }
        }

        protected internal void UpdateSpecialDates()
        {
            if (this.Content != null)
            {
                this.Content.UpdateSpecialDates();
            }
        }

        protected internal void UpdateToday()
        {
            if (this.Content != null)
            {
                this.Content.UpdateToday();
            }
        }

        public ICommand ArrowRightCommand { get; private set; }

        public ICommand ArrowLeftCommand { get; private set; }

        public ICommand HeaderClickCommand { get; private set; }

        public bool ShowWeekNumbers
        {
            get => 
                (bool) base.GetValue(ShowWeekNumbersProperty);
            set => 
                base.SetValue(ShowWeekNumbersProperty, value);
        }

        public DateNavigatorCalendarView State
        {
            get => 
                (DateNavigatorCalendarView) base.GetValue(StateProperty);
            set => 
                base.SetValue(StateProperty, value);
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

        public ControlTemplate YearsRangeInfoTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(YearsRangeInfoTemplateProperty);
            set => 
                base.SetValue(YearsRangeInfoTemplateProperty, value);
        }

        public Style CalendarTransferStyle
        {
            get => 
                (Style) base.GetValue(CalendarTransferStyleProperty);
            set => 
                base.SetValue(CalendarTransferStyleProperty, value);
        }

        public Style WeekNumberStyle
        {
            get => 
                (Style) base.GetValue(WeekNumberStyleProperty);
            set => 
                base.SetValue(WeekNumberStyleProperty, value);
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

        public bool ResizeToMaxContent
        {
            get => 
                (bool) base.GetValue(ResizeToMaxContentProperty);
            set => 
                base.SetValue(ResizeToMaxContentProperty, value);
        }

        protected IDateNavigatorCalendarOwner Owner =>
            this.ownerReference.IsAlive ? ((IDateNavigatorCalendarOwner) this.ownerReference.Target) : null;

        public DateNavigatorCalendarPosition Position
        {
            get => 
                (DateNavigatorCalendarPosition) base.GetValue(PositionProperty);
            internal set => 
                base.SetValue(PositionPropertyKey, value);
        }

        public CalendarWeekRule? WeekNumberRule
        {
            get => 
                (CalendarWeekRule?) base.GetValue(WeekNumberRuleProperty);
            set => 
                base.SetValue(WeekNumberRuleProperty, value);
        }

        public bool HighlightSpecialDates
        {
            get => 
                (bool) base.GetValue(HighlightSpecialDatesProperty);
            set => 
                base.SetValue(HighlightSpecialDatesProperty, value);
        }

        public bool HighlightHolidays
        {
            get => 
                (bool) base.GetValue(HighlightHolidaysProperty);
            set => 
                base.SetValue(HighlightHolidaysProperty, value);
        }

        public DayOfWeek? FirstDayOfWeek
        {
            get => 
                (DayOfWeek?) base.GetValue(FirstDayOfWeekProperty);
            set => 
                base.SetValue(FirstDayOfWeekProperty, value);
        }

        protected internal DateNavigatorCalendarContent ActiveContent =>
            null;

        protected internal DateNavigatorCalendarContent Content { get; private set; }

        public DateEdit OwnerDateEdit =>
            (DateEdit) BaseEdit.GetOwnerEdit(this);

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

        public DateNavigatorCalendarHeaderType HeaderType
        {
            get => 
                (DateNavigatorCalendarHeaderType) base.GetValue(HeaderTypeProperty);
            internal set => 
                base.SetValue(HeaderTypePropertyKey, value);
        }

        public DateNavigatorCalendar Calendar
        {
            get => 
                (DateNavigatorCalendar) base.GetValue(CalendarProperty);
            set => 
                base.SetValue(CalendarProperty, value);
        }

        protected DateNavigatorCalendarNavigatorBase Navigator
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
            public static readonly DateNavigatorCalendar.<>c <>9 = new DateNavigatorCalendar.<>c();
            public static Func<DevExpress.Xpf.Editors.DateNavigator.DateNavigator, bool> <>9__69_0;
            public static Func<bool> <>9__69_1;
            public static Func<DevExpress.Xpf.Editors.DateNavigator.DateNavigator, bool> <>9__71_0;
            public static Func<bool> <>9__71_1;

            internal void <.cctor>b__29_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateNavigatorCalendar) d).PropertyChangedWeekNumberRule((CalendarWeekRule?) e.OldValue);
            }

            internal void <.cctor>b__29_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateNavigatorCalendar) d).PropertyChangedHighlightSpecialDates((bool) e.OldValue);
            }

            internal void <.cctor>b__29_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateNavigatorCalendar) d).PropertyChangedHighlightHolidays((bool) e.OldValue);
            }

            internal void <.cctor>b__29_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateNavigatorCalendar) d).PropertyChangedFirstDayOfWeek((DayOfWeek?) e.OldValue);
            }

            internal void <.cctor>b__29_4(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((DateNavigatorCalendar) obj).OnPositionChanged();
            }

            internal bool <CanArrowLeftClick>b__71_0(DevExpress.Xpf.Editors.DateNavigator.DateNavigator x) => 
                x.MinValue != null;

            internal bool <CanArrowLeftClick>b__71_1() => 
                false;

            internal bool <CanArrowRightClick>b__69_0(DevExpress.Xpf.Editors.DateNavigator.DateNavigator x) => 
                x.MaxValue != null;

            internal bool <CanArrowRightClick>b__69_1() => 
                false;
        }
    }
}

