namespace DevExpress.Xpf.Editors.DateNavigator
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.DateNavigator.Controls;
    using DevExpress.Xpf.Editors.DateNavigator.Internal;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Threading;

    [LicenseProvider(typeof(DX_WPFEditors_LicenseProvider)), DXToolboxBrowsable(DXToolboxItemKind.Free)]
    public class DateNavigator : Control, IDateNavigatorContentContainer, IServiceProvider, ILogicalOwner, IInputElement, IDateEditCalendarBase
    {
        public static readonly DependencyProperty ActualFirstDayOfWeekProperty;
        public static readonly DependencyProperty CalendarViewProperty;
        public static readonly DependencyProperty MaxSelectionLengthProperty;
        public static readonly DependencyProperty CalendarStyleProperty;
        public static readonly DependencyProperty ColumnCountProperty;
        public static readonly DependencyProperty CurrentDateTextProperty;
        public static readonly DependencyProperty ExactWorkdaysProperty;
        public static readonly DependencyProperty FirstDayOfWeekProperty;
        public static readonly DependencyProperty FocusedDateProperty;
        public static readonly DependencyProperty HighlightHolidaysProperty;
        public static readonly DependencyProperty HighlightSpecialDatesProperty;
        public static readonly DependencyProperty HolidaysProperty;
        public static readonly DependencyProperty IsMultiSelectProperty;
        public static readonly DependencyProperty AllowMultipleRangesProperty;
        public static readonly DependencyProperty NavigatorProperty;
        public static readonly DependencyProperty RowCountProperty;
        public static readonly DependencyProperty SelectedDatesProperty;
        public static readonly DependencyProperty ShowTodayButtonProperty;
        public static readonly DependencyProperty ShowWeekNumbersProperty;
        public static readonly DependencyProperty SpecialDatesProperty;
        public static readonly DependencyProperty StyleSettingsProperty;
        public static readonly DependencyProperty WeekNumberRuleProperty;
        public static readonly DependencyProperty WorkdaysProperty;
        public static readonly DependencyProperty DisplayModeProperty;
        public static readonly DependencyProperty DisabledDatesProperty;
        public static readonly DependencyProperty MinValueProperty;
        public static readonly DependencyProperty MaxValueProperty;
        public static readonly DependencyProperty CalendarPaddingProperty;
        public static readonly DependencyProperty FirstVisibleDateProperty;
        public static readonly DependencyProperty LastVisibleDateProperty;
        public static readonly DependencyProperty ShowClearButtonProperty;
        private static readonly DependencyPropertyKey ActualFirstDayOfWeekPropertyKey;
        private static readonly DependencyPropertyKey CurrentDateTextPropertyKey;
        internal static readonly DependencyPropertyKey DisplayModePropertyKey;
        private static readonly DependencyPropertyKey FirstVisibleDatePropertyKey;
        private static readonly DependencyPropertyKey LastVisibleDatePropertyKey;
        public static readonly RoutedEvent RequestCellStateEvent;
        public static readonly RoutedEvent VisibleDateRangeChangedEvent;
        protected DateTime? shiftSelectionFirstDate;
        private ButtonBase arrowLeft;
        private ButtonBase arrowRight;
        private readonly Dictionary<DateNavigatorCalendarView, DateNavigatorContent> contents = new Dictionary<DateNavigatorCalendarView, DateNavigatorContent>();
        private ButtonBase currentDateButton;
        private readonly Locker lockerDateTime = new Locker();
        private readonly Locker lockerSelectedDateList = new Locker();
        private readonly Locker lockerSelectedDates = new Locker();
        private readonly Locker lockerSyncSelectedDatesWithSelectedDateList = new Locker();
        private readonly DispatcherTimer updateTodayTimer = new DispatcherTimer();
        private WeakReference optionsProviderServiceReference;
        private ButtonBase todayButton;
        private ButtonBase clearButton;
        private DateTime today;
        private readonly List<object> logicalChildren = new List<object>();

        event RoutedEventHandler ILogicalOwner.Loaded
        {
            add
            {
                base.Loaded += value;
            }
            remove
            {
                base.Loaded -= value;
            }
        }

        public event DateNavigatorRequestCellStateEventHandler RequestCellState
        {
            add
            {
                base.AddHandler(RequestCellStateEvent, value);
            }
            remove
            {
                base.RemoveHandler(RequestCellStateEvent, value);
            }
        }

        public event EventHandler SelectedDatesChanged;

        public event VisibleDateRangeChangedEventHandler VisibleDateRangeChanged
        {
            add
            {
                base.AddHandler(VisibleDateRangeChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(VisibleDateRangeChangedEvent, value);
            }
        }

        static DateNavigator()
        {
            Type ownerType = typeof(DevExpress.Xpf.Editors.DateNavigator.DateNavigator);
            ActualFirstDayOfWeekPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualFirstDayOfWeek", typeof(DayOfWeek), ownerType, new PropertyMetadata(null));
            ActualFirstDayOfWeekProperty = ActualFirstDayOfWeekPropertyKey.DependencyProperty;
            MaxSelectionLengthProperty = DependencyPropertyManager.Register("MaxSelectionLength", typeof(int), ownerType, new FrameworkPropertyMetadata(-1, FrameworkPropertyMetadataOptions.None, (d, e) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).MaxSelectionLengthChanged((int) e.OldValue, (int) e.NewValue)));
            CalendarViewProperty = DependencyPropertyManager.Register("CalendarView", typeof(DateNavigatorCalendarView), ownerType, new FrameworkPropertyMetadata(DateNavigatorCalendarView.Month, FrameworkPropertyMetadataOptions.None, (d, e) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).CalendarViewChanged((DateNavigatorCalendarView) e.OldValue, (DateNavigatorCalendarView) e.NewValue)));
            CalendarStyleProperty = DependencyPropertyManager.Register("CalendarStyle", typeof(Style), ownerType, new PropertyMetadata(null, (d, e) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedCalendarStyle((Style) e.OldValue)));
            ColumnCountProperty = DependencyPropertyManager.Register("ColumnCount", typeof(int), ownerType, new PropertyMetadata(0), new ValidateValueCallback(DevExpress.Xpf.Editors.DateNavigator.DateNavigator.ValidatePropertyValueColumnCount));
            CurrentDateTextPropertyKey = DependencyPropertyManager.RegisterReadOnly("CurrentDateText", typeof(string), ownerType, new PropertyMetadata(null));
            CurrentDateTextProperty = CurrentDateTextPropertyKey.DependencyProperty;
            ExactWorkdaysProperty = DependencyPropertyManager.Register("ExactWorkdays", typeof(IList<DateTime>), ownerType, new PropertyMetadata((d, e) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedExactWorkdays((IList<DateTime>) e.OldValue)));
            FirstDayOfWeekProperty = DependencyPropertyManager.Register("FirstDayOfWeek", typeof(DayOfWeek?), ownerType, new PropertyMetadata(null, (d, e) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedFirstDayOfWeek((DayOfWeek?) e.OldValue)));
            FocusedDateProperty = DependencyPropertyManager.Register("FocusedDate", typeof(DateTime), ownerType, new FrameworkPropertyMetadata(DateTime.Today, FrameworkPropertyMetadataOptions.None, (d, e) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).FocusedDateChanged((DateTime) e.OldValue, (DateTime) e.NewValue), (d, value) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).CoerceFocusedDate(value)));
            HighlightHolidaysProperty = DependencyPropertyManager.Register("HighlightHolidays", typeof(bool), ownerType, new PropertyMetadata(true, (d, e) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedHighlightHolidays((bool) e.OldValue)));
            HighlightSpecialDatesProperty = DependencyPropertyManager.Register("HighlightSpecialDates", typeof(bool), ownerType, new PropertyMetadata(true, (d, e) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedHighlightSpecialDates((bool) e.OldValue)));
            HolidaysProperty = DependencyPropertyManager.Register("Holidays", typeof(IList<DateTime>), ownerType, new PropertyMetadata((d, e) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedHolidays((IList<DateTime>) e.OldValue)));
            IsMultiSelectProperty = DependencyPropertyManager.Register("IsMultiSelect", typeof(bool), ownerType, new PropertyMetadata(true, (d, e) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).IsMultiSelectChanged((bool) e.OldValue, (bool) e.NewValue)));
            AllowMultipleRangesProperty = DependencyPropertyManager.Register("AllowMultipleRanges", typeof(bool), ownerType, new PropertyMetadata(true, (d, e) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).AllowMultipleRangesChanged((bool) e.OldValue, (bool) e.NewValue)));
            NavigatorProperty = DependencyPropertyManager.RegisterAttached("Navigator", ownerType, ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
            RowCountProperty = DependencyPropertyManager.Register("RowCount", typeof(int), ownerType, new PropertyMetadata(0), new ValidateValueCallback(DevExpress.Xpf.Editors.DateNavigator.DateNavigator.ValidatePropertyValueRowCount));
            SelectedDatesProperty = DependencyPropertyManager.Register("SelectedDates", typeof(IList<DateTime>), ownerType, new PropertyMetadata(null, (d, e) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedSelectedDates((IList<DateTime>) e.OldValue, (IList<DateTime>) e.NewValue), (d, value) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).CoerceSelectedDates(value)));
            ShowTodayButtonProperty = DependencyPropertyManager.Register("ShowTodayButton", typeof(bool), ownerType, new PropertyMetadata(true));
            ShowWeekNumbersProperty = DependencyPropertyManager.Register("ShowWeekNumbers", typeof(bool), ownerType, new PropertyMetadata(true, (d, e) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedShowWeekNumbers()));
            SpecialDatesProperty = DependencyPropertyManager.Register("SpecialDates", typeof(IList<DateTime>), ownerType, new PropertyMetadata((d, e) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedSpecialDates((IList<DateTime>) e.OldValue)));
            StyleSettingsProperty = DependencyPropertyManager.Register("StyleSettings", typeof(DateNavigatorStyleSettingsBase), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (d, e) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).StyleSettingsChanged((DateNavigatorStyleSettingsBase) e.OldValue, (DateNavigatorStyleSettingsBase) e.NewValue), (d, value) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).CreateDefaultStyleSettings(value)));
            WeekNumberRuleProperty = DependencyPropertyManager.Register("WeekNumberRule", typeof(CalendarWeekRule?), ownerType, new PropertyMetadata(null, (d, e) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedWeekNumberRule((CalendarWeekRule?) e.OldValue)));
            WorkdaysProperty = DependencyPropertyManager.Register("Workdays", typeof(IList<DayOfWeek>), ownerType, new PropertyMetadata((d, e) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedWorkdays((IList<DayOfWeek>) e.OldValue)));
            DisabledDatesProperty = DependencyPropertyManager.Register("DisabledDates", typeof(IList<DateTime>), ownerType, new PropertyMetadata((d, e) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedDisabledDates((IList<DateTime>) e.OldValue)));
            MinValueProperty = DependencyPropertyManager.Register("MinValue", typeof(DateTime?), ownerType, new PropertyMetadata((d, e) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedMinValue((DateTime?) e.OldValue)));
            MaxValueProperty = DependencyPropertyManager.Register("MaxValue", typeof(DateTime?), ownerType, new PropertyMetadata((d, e) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedMaxValue((DateTime?) e.OldValue)));
            CalendarPaddingProperty = DependencyPropertyManager.Register("CalendarPadding", typeof(Thickness), ownerType, new FrameworkPropertyMetadata(new Thickness(5.0)));
            ShowClearButtonProperty = DependencyPropertyManager.Register("ShowClearButton", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure));
            FirstVisibleDatePropertyKey = DependencyPropertyManager.RegisterReadOnly("FirstVisibleDate", typeof(DateTime), ownerType, new PropertyMetadata((d, e) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedFirstVisibleDate((DateTime) e.OldValue)));
            FirstVisibleDateProperty = FirstVisibleDatePropertyKey.DependencyProperty;
            LastVisibleDatePropertyKey = DependencyPropertyManager.RegisterReadOnly("LastVisibleDate", typeof(DateTime), ownerType, new PropertyMetadata((d, e) => ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedLastVisibleDate((DateTime) e.OldValue)));
            LastVisibleDateProperty = LastVisibleDatePropertyKey.DependencyProperty;
            DisplayModePropertyKey = DependencyPropertyManager.RegisterReadOnly("DisplayMode", typeof(DateNavigatorDisplayMode), ownerType, new FrameworkPropertyMetadata(DateNavigatorDisplayMode.Classic));
            DisplayModeProperty = DisplayModePropertyKey.DependencyProperty;
            RequestCellStateEvent = EventManager.RegisterRoutedEvent("RequestCellState", RoutingStrategy.Direct, typeof(DateNavigatorRequestCellStateEventHandler), ownerType);
            VisibleDateRangeChangedEvent = EventManager.RegisterRoutedEvent("VisibleDateRangeChanged", RoutingStrategy.Direct, typeof(VisibleDateRangeChangedEventHandler), ownerType);
        }

        public DateNavigator()
        {
            this.today = this.GetToday();
            this.SetDefaultStyleKey(typeof(DevExpress.Xpf.Editors.DateNavigator.DateNavigator));
            this.SelectionManager = this.CreateSelectionManager();
            SetNavigator(this, this);
            base.SetCurrentValue(StyleSettingsProperty, this.CreateDefaultStyleSettings(null));
            base.SetCurrentValue(SpecialDatesProperty, new ObservableCollection<DateTime>());
            base.SetCurrentValue(SelectedDatesProperty, new ObservableCollection<DateTime>());
            base.SetCurrentValue(ExactWorkdaysProperty, new ObservableCollection<DateTime>());
            base.SetCurrentValue(HolidaysProperty, new ObservableCollection<DateTime>());
            ObservableCollection<DayOfWeek> collection1 = new ObservableCollection<DayOfWeek>();
            collection1.Add(DayOfWeek.Monday);
            collection1.Add(DayOfWeek.Tuesday);
            collection1.Add(DayOfWeek.Wednesday);
            collection1.Add(DayOfWeek.Thursday);
            collection1.Add(DayOfWeek.Friday);
            base.SetCurrentValue(WorkdaysProperty, collection1);
            base.SetCurrentValue(DisabledDatesProperty, new ObservableCollection<DateTime>());
            this.updateTodayTimer.Interval = TimeSpan.FromSeconds(1.0);
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            base.Unloaded += new RoutedEventHandler(this.OnUnloaded);
            this.UpdateActualFirstDayOfWeek();
            this.UpdateActualWorkdaysProperties();
        }

        protected virtual void AllowMultipleRangesChanged(bool oldValue, bool newValue)
        {
            this.StyleSettings.RegisterNavigationService();
        }

        protected internal virtual void ArrowClick(bool isRight)
        {
            if (this.ActiveContent != null)
            {
                if (isRight)
                {
                    this.NavigationService.ScrollNextPage();
                }
                else
                {
                    this.NavigationService.ScrollPreviousPage();
                }
                this.InvalidateVisibleDateRange();
            }
        }

        private void BeginAnimation(DateNavigatorContent value, DoubleAnimation element, string strPropertyPath)
        {
            Storyboard.SetTarget(element, value);
            Storyboard.SetTargetProperty(element, new PropertyPath(strPropertyPath, new object[0]));
            new Storyboard { Children = { element } }.Begin();
        }

        public void CalculateVisibleDateRange(bool excludeInactiveDates, out DateTime startDate, out DateTime endDate)
        {
            if (this.ActiveContent != null)
            {
                ((IDateNavigatorContent) this.ActiveContent).GetDateRange(excludeInactiveDates, out startDate, out endDate);
            }
            else
            {
                DateTime time1 = new DateTime();
                startDate = endDate = time1;
            }
        }

        protected virtual void CalendarViewChanged(DateNavigatorCalendarView oldState, DateNavigatorCalendarView newState)
        {
            this.NavigationService.ToView(newState);
            this.ShowContent(oldState, newState, true);
            this.NavigationCallbackService.ChangeView(newState);
            this.UpdateCalendarsCellStates();
        }

        protected virtual object CoerceFocusedDate(object value)
        {
            DateTime date = (DateTime) value;
            return (((this.DateCalculationService == null) || !this.DateCalculationService.IsDisabled(date)) ? date : this.FocusedDate);
        }

        protected virtual object CoerceSelectedDates(object value)
        {
            IList<DateTime> list = value as IList<DateTime>;
            if (list == null)
            {
                return null;
            }
            foreach (DateTime time in (from x in list
                where this.DateCalculationService.IsDisabled(x)
                select x).ToList<DateTime>())
            {
                list.Remove(time);
            }
            return list;
        }

        private DateTime CreateDate(int year, int month, int day) => 
            (year >= 1) ? ((year <= 0x270f) ? new DateTime(year, month, day) : new DateTime(0x270f, 12, 0x1f)) : new DateTime(1, 1, 1);

        protected virtual object CreateDefaultStyleSettings(object value) => 
            (value != null) ? value : new DateNavigatorOutlookStyleSettings();

        protected virtual DoubleAnimation CreateDoubleAnimation(double from, double to) => 
            new DoubleAnimation { 
                From = new double?(from),
                To = new double?(to),
                Duration = new Duration(TimeSpan.FromMilliseconds(500.0)),
                FillBehavior = FillBehavior.HoldEnd
            };

        protected virtual DevExpress.Xpf.Editors.DateNavigator.SelectionManager CreateSelectionManager() => 
            new DevExpress.Xpf.Editors.DateNavigator.SelectionManager(this);

        protected virtual void DateTimeChanged(DateTime? oldValue, DateTime? newValue)
        {
            this.ValueEditingStrategy.DateTimeChanged(oldValue, newValue);
        }

        void ILogicalOwner.AddChild(object child)
        {
            this.logicalChildren.Add(child);
            base.AddLogicalChild(child);
        }

        void ILogicalOwner.RemoveChild(object child)
        {
            this.logicalChildren.Remove(child);
            base.RemoveLogicalChild(child);
        }

        DateNavigatorContent IDateNavigatorContentContainer.GetContent(DateNavigatorCalendarView state) => 
            this.GetContent(state);

        bool IDateEditCalendarBase.ProcessKeyDown(KeyEventArgs e)
        {
            this.NavigationService.ProcessKeyDown(e);
            return e.Handled;
        }

        protected virtual void FocusedDateChanged(DateTime oldValue, DateTime newValue)
        {
            this.ValueEditingStrategy.FocusedDateChanged(oldValue, newValue);
            this.NavigationService.Move(newValue);
            this.NavigationCallbackService.Move(newValue);
            this.InvalidateFocusedDate();
            this.InvalidateVisibleDateRange();
        }

        protected virtual DayOfWeek GetActualFirstDayOfWeek() => 
            (this.OptionsProviderService == null) ? ((this.FirstDayOfWeek != null) ? this.FirstDayOfWeek.Value : CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek) : this.OptionsProviderService.FirstDayOfWeek;

        private DateNavigatorContent GetContent(DateNavigatorCalendarView view)
        {
            DateNavigatorContent content;
            return (!this.contents.TryGetValue(view, out content) ? null : content);
        }

        protected virtual string GetCurrentDateText()
        {
            DateTime time;
            DateTime time2;
            if (this.ActiveContent == null)
            {
                return "";
            }
            this.CalculateVisibleDateRange(true, out time, out time2);
            return DateNavigatorHelper.GetDateText(this.CalendarView, time, time2);
        }

        private void GetDateRange(DateTime dt1, DateTime dt2, out DateTime rangeStart, out DateTime rangeFinish)
        {
            if (dt1 <= dt2)
            {
                rangeStart = dt1;
                rangeFinish = dt2;
            }
            else
            {
                rangeStart = dt2;
                rangeFinish = dt1;
            }
        }

        internal DateTime GetDateTimeForContent(DateNavigatorCalendarView state, DateTime dt)
        {
            switch (state)
            {
                case DateNavigatorCalendarView.Month:
                    return new DateTime(dt.Year, dt.Month, 1);

                case DateNavigatorCalendarView.Year:
                    return new DateTime(dt.Year, 1, 1);

                case DateNavigatorCalendarView.Years:
                    return new DateTime(Math.Max((dt.Year / 10) * 10, 1), 1, 1);

                case DateNavigatorCalendarView.YearsRange:
                    return new DateTime(Math.Max((dt.Year / 100) * 100, 1), 1, 1);
            }
            throw new Exception();
        }

        public static DevExpress.Xpf.Editors.DateNavigator.DateNavigator GetNavigator(DependencyObject obj) => 
            (DevExpress.Xpf.Editors.DateNavigator.DateNavigator) DependencyObjectHelper.GetValueWithInheritance(obj, NavigatorProperty);

        internal DateTime GetToday()
        {
            IOptionsProviderService optionsProviderService = this.OptionsProviderService;
            if (optionsProviderService != null)
            {
                return optionsProviderService.Today;
            }
            IOptionsProviderService local1 = optionsProviderService;
            return DateTime.Today;
        }

        protected virtual void HandleSpecialDatesChanges()
        {
            this.UpdateActualWorkdaysProperties();
            this.UpdateSpecialDateList();
            this.UpdateMonthContentCalendarsSpecialDates();
        }

        protected void HitTest(MouseEventArgs e, out DateTime? buttonDate, out DateNavigatorCalendarButtonKind buttonKind)
        {
            UIElement element = base.InputHitTest(e.GetPosition(this)) as UIElement;
            ((IDateNavigatorContent) this.ActiveContent).HitTest(element, out buttonDate, out buttonKind);
        }

        private void InitButton(ref ButtonBase button, string templatePartName, RoutedEventHandler clickHandler)
        {
            if (button != null)
            {
                button.Click -= clickHandler;
            }
            button = base.GetTemplateChild(templatePartName) as ButtonBase;
            if (button != null)
            {
                button.Click += clickHandler;
            }
        }

        protected internal virtual void InvalidateFocusedDate()
        {
            IDateNavigatorContent activeContent = this.ActiveContent;
            if (activeContent != null)
            {
                activeContent.FocusedDate = this.ValueEditingService.FocusedDate;
            }
        }

        protected internal virtual void InvalidateSelection()
        {
            IDateNavigatorContent activeContent = this.ActiveContent;
            if (activeContent != null)
            {
                activeContent.UpdateCalendarsSelectedDates();
            }
        }

        protected internal virtual void InvalidateToday()
        {
            IDateNavigatorContent activeContent = this.ActiveContent;
            if (activeContent != null)
            {
                activeContent.UpdateCalendarToday();
            }
        }

        protected virtual void InvalidateVisibleDateRange()
        {
            if (this.ActiveContent != null)
            {
                DateTime time;
                DateTime time2;
                ((IDateNavigatorContent) this.ActiveContent).GetDateRange(false, out time, out time2);
                if (!time.Equals(this.FirstVisibleDate) || !time2.Equals(this.LastVisibleDate))
                {
                    DateTime firstVisibleDate = this.FirstVisibleDate;
                    this.FirstVisibleDate = time;
                    this.LastVisibleDate = time2;
                    this.RaiseVisibleDateRangeChanged(firstVisibleDate, this.LastVisibleDate);
                }
            }
        }

        protected internal bool IsDateVisibleAndActive(DateTime date) => 
            ((IDateNavigatorContent) this.ActiveContent).GetCalendar(date) != null;

        protected virtual void IsMultiSelectChanged(bool oldValue, bool newValue)
        {
            this.StyleSettings.RegisterNavigationService();
        }

        protected void LockSelectedDatesChanged(bool isLock)
        {
            if (isLock)
            {
                this.lockerSelectedDates.Lock();
            }
            else
            {
                this.lockerSelectedDates.Unlock();
            }
        }

        protected virtual void MakeZoomInOutAnimation(DateNavigatorContent control, bool show)
        {
            if (control != null)
            {
                DoubleAnimation animation;
                DoubleAnimation animation2;
                DoubleAnimation animation3;
                if (show)
                {
                    animation = this.CreateDoubleAnimation(0.0, 1.0);
                    animation2 = this.CreateDoubleAnimation(0.0, 1.0);
                    animation3 = this.CreateDoubleAnimation(0.0, 1.0);
                }
                else
                {
                    animation = this.CreateDoubleAnimation(1.0, 0.0);
                    animation2 = this.CreateDoubleAnimation(1.0, 0.0);
                    animation3 = this.CreateDoubleAnimation(1.0, 0.0);
                }
                ScaleTransform transform = new ScaleTransform();
                control.RenderTransform = transform;
                transform.CenterX = control.ActualWidth / 2.0;
                transform.CenterY = control.ActualHeight / 2.0;
                this.BeginAnimation(control, animation, "(UIElement.RenderTransform).(ScaleTransform.ScaleX)");
                this.BeginAnimation(control, animation2, "(UIElement.RenderTransform).(ScaleTransform.ScaleY)");
                this.BeginAnimation(control, animation3, "(UIElement.Opacity)");
            }
        }

        protected virtual void MaxSelectionLengthChanged(int oldValue, int newValue)
        {
            this.ValueEditingStrategy.UpdateSelection();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            foreach (DateNavigatorContent content in this.contents.Values)
            {
                content.CalendarButtonClick -= new DateNavigatorCalendarButtonClickEventHandler(this.OnCalendarButtonClick);
                content.DateRangeChanged -= new DateNavigatorContentDateRangeChangedEventHandler(this.OnContentDateRangeChanged);
            }
            this.contents.Clear();
            foreach (DateNavigatorCalendarView view in typeof(DateNavigatorCalendarView).GetValues())
            {
                DateNavigatorContent templateChild = (DateNavigatorContent) base.GetTemplateChild("PART_Content" + view.ToString());
                templateChild.CalendarButtonClick += new DateNavigatorCalendarButtonClickEventHandler(this.OnCalendarButtonClick);
                templateChild.DateRangeChanged += new DateNavigatorContentDateRangeChangedEventHandler(this.OnContentDateRangeChanged);
                this.contents.Add(view, templateChild);
            }
            this.InitButton(ref this.arrowLeft, "PART_ArrowLeft", new RoutedEventHandler(this.OnArrowLeftClick));
            this.InitButton(ref this.arrowRight, "PART_ArrowRight", new RoutedEventHandler(this.OnArrowRightClick));
            this.InitButton(ref this.currentDateButton, "PART_CurrentDateButton", new RoutedEventHandler(this.OnCurrentDateButtonClick));
            this.InitButton(ref this.todayButton, "PART_TodayButton", new RoutedEventHandler(this.OnTodayButtonClick));
            this.InitButton(ref this.clearButton, "PART_ClearButton", new RoutedEventHandler(this.OnClearButtonClick));
            this.ShowContent(this.CalendarView, this.CalendarView, false);
            if (this.OwnerDateEdit != null)
            {
                this.OwnerDateEdit.OnApplyCalendarTemplate(this);
            }
        }

        protected internal virtual void OnArrowLeftClick(object sender, RoutedEventArgs e)
        {
            this.ArrowClick(false);
        }

        protected internal virtual void OnArrowRightClick(object sender, RoutedEventArgs e)
        {
            this.ArrowClick(true);
        }

        protected virtual void OnCalendarButtonClick(object sender, DateNavigatorCalendarButtonClickEventArgs e)
        {
            base.Focus();
            this.NavigationService.ProcessMouseDown(e.ButtonDate, e.ButtonKind);
        }

        protected virtual void OnClearButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.OwnerDateEdit != null)
            {
                this.OwnerDateEdit.ClearError();
                this.OwnerDateEdit.IsPopupOpen = false;
                this.OwnerDateEdit.SetCurrentValue(BaseEdit.EditValueProperty, this.OwnerDateEdit.NullValue);
            }
        }

        protected virtual void OnContentDateRangeChanged(object sender, DateNavigatorContentDateRangeChangedEventArgs e)
        {
            if (((DateNavigatorContent) sender).State == this.CalendarView)
            {
                this.NavigationCallbackService.VisibleDateRangeChanged(e.IsScrolling);
                this.UpdateCurrentDateText();
            }
        }

        protected internal virtual void OnCurrentDateButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.CalendarView != DateNavigatorCalendarView.YearsRange)
            {
                this.NavigationService.ToView((this.CalendarView + 1) % this.contents.Count);
            }
        }

        protected virtual void OnDisabledDatesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (DateTime date in e.NewItems)
                {
                    (this.DateCalculationService as DateNavigatorWorkdayCalculator).Do<DateNavigatorWorkdayCalculator>(x => x.InvalidateDateCache(date));
                }
            }
            if (e.OldItems != null)
            {
                foreach (DateTime time1 in e.OldItems)
                {
                    (this.DateCalculationService as DateNavigatorWorkdayCalculator).Do<DateNavigatorWorkdayCalculator>(x => x.InvalidateDateCache(time1));
                }
            }
            this.UpdateCalendarsDisabledDates();
        }

        protected virtual void OnExactWorkdaysChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (DateTime date in e.NewItems)
                {
                    (this.DateCalculationService as DateNavigatorWorkdayCalculator).Do<DateNavigatorWorkdayCalculator>(x => x.InvalidateDateCache(date));
                }
            }
            if (e.OldItems != null)
            {
                foreach (DateTime time1 in e.OldItems)
                {
                    (this.DateCalculationService as DateNavigatorWorkdayCalculator).Do<DateNavigatorWorkdayCalculator>(x => x.InvalidateDateCache(time1));
                }
            }
            this.UpdateCalendarsHolidays();
        }

        protected virtual void OnHolidaysChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (DateTime date in e.NewItems)
                {
                    (this.DateCalculationService as DateNavigatorWorkdayCalculator).Do<DateNavigatorWorkdayCalculator>(x => x.InvalidateDateCache(date));
                }
            }
            if (e.OldItems != null)
            {
                foreach (DateTime time1 in e.OldItems)
                {
                    (this.DateCalculationService as DateNavigatorWorkdayCalculator).Do<DateNavigatorWorkdayCalculator>(x => x.InvalidateDateCache(time1));
                }
            }
            this.UpdateCalendarsHolidays();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            this.updateTodayTimer.Tick += new EventHandler(this.UpdateTodayTimerOnTick);
            this.updateTodayTimer.Start();
            this.InvalidateVisibleDateRange();
        }

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            base.OnLostMouseCapture(e);
            if (e.OriginalSource == this)
            {
                DateTime? nullable;
                DateNavigatorCalendarButtonKind kind;
                this.HitTest(e, out nullable, out kind);
                this.NavigationService.ProcessMouseUp(nullable, kind);
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            base.Focus();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            base.ReleaseMouseCapture();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            DateTime? nullable;
            DateNavigatorCalendarButtonKind kind;
            base.OnMouseMove(e);
            this.HitTest(e, out nullable, out kind);
            this.NavigationService.ProcessMouseMove(nullable, kind);
        }

        protected virtual void OnOptionsProviderServiceOptionsChanged(object sender, EventArgs e)
        {
            this.UpdateActualFirstDayOfWeek();
            this.UpdateActualWorkdaysProperties();
            this.UpdateCalendarsHolidays();
            this.UpdateCalendarsCellStates();
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            if (!e.Handled)
            {
                this.NavigationService.ProcessKeyDown(e);
            }
        }

        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
            base.OnPreviewKeyUp(e);
            if (!e.Handled)
            {
                this.NavigationService.ProcessKeyUp(e);
            }
        }

        protected override void OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnPreviewLostKeyboardFocus(e);
            this.SelectionManager.Post();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            this.InvalidateVisibleDateRange();
        }

        protected virtual void OnSelectedDatesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Action<DateNavigatorWorkdayCalculator> action = <>c.<>9__241_0;
            if (<>c.<>9__241_0 == null)
            {
                Action<DateNavigatorWorkdayCalculator> local1 = <>c.<>9__241_0;
                action = <>c.<>9__241_0 = x => x.InvalidateDatesCache();
            }
            (this.DateCalculationService as DateNavigatorWorkdayCalculator).Do<DateNavigatorWorkdayCalculator>(action);
            if (!this.IsSelectedDatesChangedLocked)
            {
                this.NavigationService.CheckSelectedDates();
            }
            this.ValueEditingStrategy.SelectedDatesChanged(this.SelectedDates);
            this.RaiseSelectedDatesChanged();
        }

        protected virtual void OnSpecialDatesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.HandleSpecialDatesChanges();
        }

        protected virtual void OnTodayButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.CalendarView != DateNavigatorCalendarView.Month)
            {
                this.NavigationService.ToView(DateNavigatorCalendarView.Month);
            }
            this.NavigationService.Move(this.GetToday());
            this.NavigationService.Select(this.GetToday(), true);
        }

        private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            this.updateTodayTimer.Tick -= new EventHandler(this.UpdateTodayTimerOnTick);
            this.updateTodayTimer.Stop();
        }

        protected virtual void OnWorkdaysChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Action<DateNavigatorWorkdayCalculator> action = <>c.<>9__244_0;
            if (<>c.<>9__244_0 == null)
            {
                Action<DateNavigatorWorkdayCalculator> local1 = <>c.<>9__244_0;
                action = <>c.<>9__244_0 = x => x.InvalidateDatesCache();
            }
            (this.DateCalculationService as DateNavigatorWorkdayCalculator).Do<DateNavigatorWorkdayCalculator>(action);
            this.UpdateCalendarsHolidays();
        }

        protected virtual void PropertyChangedCalendarStyle(Style oldValue)
        {
            foreach (IDateNavigatorContent content in this.contents.Values)
            {
                if (content != null)
                {
                    content.UpdateCalendarsStyle();
                }
            }
        }

        protected virtual void PropertyChangedDisabledDates(IList<DateTime> oldValue)
        {
            Action<DateNavigatorWorkdayCalculator> action = <>c.<>9__260_0;
            if (<>c.<>9__260_0 == null)
            {
                Action<DateNavigatorWorkdayCalculator> local1 = <>c.<>9__260_0;
                action = <>c.<>9__260_0 = x => x.InvalidateDatesCache();
            }
            (this.DateCalculationService as DateNavigatorWorkdayCalculator).Do<DateNavigatorWorkdayCalculator>(action);
            this.UpdateCollectionChangedSubscription(oldValue, this.DisabledDates, new NotifyCollectionChangedEventHandler(this.OnDisabledDatesChanged));
            this.UpdateCalendarsDisabledDates();
        }

        protected virtual void PropertyChangedExactWorkdays(IList<DateTime> oldValue)
        {
            Action<DateNavigatorWorkdayCalculator> action = <>c.<>9__247_0;
            if (<>c.<>9__247_0 == null)
            {
                Action<DateNavigatorWorkdayCalculator> local1 = <>c.<>9__247_0;
                action = <>c.<>9__247_0 = x => x.InvalidateDatesCache();
            }
            (this.DateCalculationService as DateNavigatorWorkdayCalculator).Do<DateNavigatorWorkdayCalculator>(action);
            this.UpdateCollectionChangedSubscription(oldValue, this.ExactWorkdays, new NotifyCollectionChangedEventHandler(this.OnExactWorkdaysChanged));
            this.UpdateCalendarsHolidays();
        }

        protected virtual void PropertyChangedFirstDayOfWeek(DayOfWeek? oldValue)
        {
            this.UpdateActualFirstDayOfWeek();
        }

        protected virtual void PropertyChangedFirstVisibleDate(DateTime oldValue)
        {
        }

        protected virtual void PropertyChangedHighlightHolidays(bool oldValue)
        {
        }

        protected virtual void PropertyChangedHighlightSpecialDates(bool oldValue)
        {
            if (this.OptionsProviderService != null)
            {
                this.OptionsProviderService.HighlightSpecialDates = this.HighlightSpecialDates;
            }
        }

        protected virtual void PropertyChangedHolidays(IList<DateTime> oldValue)
        {
            Action<DateNavigatorWorkdayCalculator> action = <>c.<>9__251_0;
            if (<>c.<>9__251_0 == null)
            {
                Action<DateNavigatorWorkdayCalculator> local1 = <>c.<>9__251_0;
                action = <>c.<>9__251_0 = x => x.InvalidateDatesCache();
            }
            (this.DateCalculationService as DateNavigatorWorkdayCalculator).Do<DateNavigatorWorkdayCalculator>(action);
            this.UpdateCollectionChangedSubscription(oldValue, this.Holidays, new NotifyCollectionChangedEventHandler(this.OnHolidaysChanged));
            this.UpdateCalendarsHolidays();
        }

        protected virtual void PropertyChangedLastVisibleDate(DateTime oldValue)
        {
        }

        protected virtual void PropertyChangedMaxValue(DateTime? oldValue)
        {
            this.UpdateCalendarsDisabledDates();
        }

        protected virtual void PropertyChangedMinValue(DateTime? oldValue)
        {
            this.UpdateCalendarsDisabledDates();
        }

        protected virtual void PropertyChangedSelectedDates(IList<DateTime> oldValue, IList<DateTime> newValue)
        {
            if (!this.IsSelectedDatesChangedLocked)
            {
                this.NavigationService.CheckSelectedDates();
            }
            this.UpdateCollectionChangedSubscription(oldValue, newValue, new NotifyCollectionChangedEventHandler(this.OnSelectedDatesChanged));
            this.ValueEditingStrategy.SelectedDatesChanged(newValue);
            this.NavigationCallbackService.Select(newValue);
            this.InvalidateSelection();
            this.RaiseSelectedDatesChanged();
        }

        protected virtual void PropertyChangedShowWeekNumbers()
        {
        }

        protected virtual void PropertyChangedSpecialDates(IList<DateTime> oldValue)
        {
            Action<DateNavigatorWorkdayCalculator> action = <>c.<>9__257_0;
            if (<>c.<>9__257_0 == null)
            {
                Action<DateNavigatorWorkdayCalculator> local1 = <>c.<>9__257_0;
                action = <>c.<>9__257_0 = x => x.InvalidateDatesCache();
            }
            (this.DateCalculationService as DateNavigatorWorkdayCalculator).Do<DateNavigatorWorkdayCalculator>(action);
            this.UpdateCollectionChangedSubscription(oldValue, this.SpecialDates, new NotifyCollectionChangedEventHandler(this.OnSpecialDatesChanged));
            this.HandleSpecialDatesChanges();
        }

        protected virtual void PropertyChangedWeekNumberRule(CalendarWeekRule? oldValue)
        {
        }

        protected virtual void PropertyChangedWorkdays(IList<DayOfWeek> oldValue)
        {
            Action<DateNavigatorWorkdayCalculator> action = <>c.<>9__259_0;
            if (<>c.<>9__259_0 == null)
            {
                Action<DateNavigatorWorkdayCalculator> local1 = <>c.<>9__259_0;
                action = <>c.<>9__259_0 = x => x.InvalidateDatesCache();
            }
            (this.DateCalculationService as DateNavigatorWorkdayCalculator).Do<DateNavigatorWorkdayCalculator>(action);
            this.UpdateCollectionChangedSubscription(oldValue, this.Workdays, new NotifyCollectionChangedEventHandler(this.OnWorkdaysChanged));
            this.UpdateCalendarsHolidays();
        }

        protected internal virtual DateNavigatorCellState RaiseRequestCellState(DateTime dateTime, DateNavigatorCellState defaultState)
        {
            DateNavigatorRequestCellStateEventArgs args1 = new DateNavigatorRequestCellStateEventArgs(dateTime);
            args1.CellState = defaultState;
            DateNavigatorRequestCellStateEventArgs e = args1;
            base.RaiseEvent(e);
            return e.CellState;
        }

        protected virtual void RaiseSelectedDatesChanged()
        {
            if (this.SelectedDatesChanged != null)
            {
                this.SelectedDatesChanged(this, EventArgs.Empty);
            }
        }

        protected virtual void RaiseVisibleDateRangeChanged(DateTime firstVisibleDateOldValue, DateTime lastVisibleDateOldValue)
        {
            base.RaiseEvent(new VisibleDateRangeChangedEventArgs(firstVisibleDateOldValue, this.FirstVisibleDate, lastVisibleDateOldValue, this.LastVisibleDate));
        }

        public void RefreshCellStates()
        {
            Action<DateNavigatorWorkdayCalculator> action = <>c.<>9__202_0;
            if (<>c.<>9__202_0 == null)
            {
                Action<DateNavigatorWorkdayCalculator> local1 = <>c.<>9__202_0;
                action = <>c.<>9__202_0 = x => x.InvalidateDatesCache();
            }
            (this.DateCalculationService as DateNavigatorWorkdayCalculator).Do<DateNavigatorWorkdayCalculator>(action);
            this.UpdateCalendarsDisabledDates();
            this.UpdateMonthContentCalendarsSpecialDates();
            this.UpdateCalendarsHolidays();
            this.UpdateCalendarsCellStates();
        }

        protected DateTime ScrollDateTime(DateTime dt, bool isRight)
        {
            int num = isRight ? 1 : -1;
            int calendarCount = ((IDateNavigatorContent) this.ActiveContent).CalendarCount;
            dt = new DateTime(dt.Year, dt.Month, 1);
            switch (this.CalendarView)
            {
                case DateNavigatorCalendarView.Month:
                {
                    int num3 = ((dt.Year - 1) * 12) + dt.Month;
                    return (((num3 + (calendarCount * num)) >= 1) ? (((num3 + (calendarCount * num)) <= 0x1d4b4) ? dt.AddMonths(calendarCount * num) : dt.AddMonths(0x1d4b4 - num3)) : dt.AddMonths(-(num3 - 1)));
                }
                case DateNavigatorCalendarView.Year:
                    return this.CreateDate(dt.Year + (calendarCount * num), dt.Month, dt.Day);

                case DateNavigatorCalendarView.Years:
                    return this.CreateDate(dt.Year + ((calendarCount * 10) * num), dt.Month, dt.Day);

                case DateNavigatorCalendarView.YearsRange:
                    return this.CreateDate(dt.Year + ((calendarCount * 100) * num), dt.Month, dt.Day);
            }
            throw new Exception();
        }

        internal bool SetActiveContentDateTime(DateTime dt, bool scrollIfValueInactive)
        {
            IDateNavigatorContent activeContent = this.ActiveContent;
            if (activeContent == null)
            {
                return false;
            }
            DateTime startDate = activeContent.StartDate;
            this.ActiveContent.SetDateTime(dt, scrollIfValueInactive);
            return !Equals(startDate, activeContent.StartDate);
        }

        public static void SetNavigator(DependencyObject obj, DevExpress.Xpf.Editors.DateNavigator.DateNavigator value)
        {
            obj.SetValue(NavigatorProperty, value);
        }

        protected internal void SetSelectedDates(IList<DateTime> value)
        {
            this.LockSelectedDatesChanged(true);
            try
            {
                if ((this.SelectedDates == null) || (value == null))
                {
                    this.SelectedDates = value;
                }
                else if (!SelectedDatesHelper.AreEquals(value, this.SelectedDates))
                {
                    this.SelectedDates = value;
                }
            }
            finally
            {
                this.LockSelectedDatesChanged(false);
            }
        }

        protected internal virtual void ShowContent(DateNavigatorCalendarView oldState, DateNavigatorCalendarView state, bool makeAnimation)
        {
            this.ValueEditingStrategy.ResetFocusedCellButtonFocusedState(oldState);
            if (this.ActiveContent != null)
            {
                if (!makeAnimation)
                {
                    if (this.ActiveContent != null)
                    {
                        this.ActiveContent.Opacity = 1.0;
                        this.ActiveContent.IsHitTestVisible = true;
                        this.NavigationService.BringToView();
                    }
                }
                else
                {
                    ((IDateNavigatorContent) this.ActiveContent).VisibilityChanged();
                    DateTime dt = (state > this.CalendarView) ? this.ActiveContent.DateTime : this.FocusedDate;
                    DateNavigatorContent content = this.GetContent(state);
                    if (content == null)
                    {
                        return;
                    }
                    content.SetDateTime(this.GetDateTimeForContent(state, dt), true);
                    this.MakeZoomInOutAnimation(this.GetContent(oldState), false);
                    this.GetContent(oldState).IsHitTestVisible = false;
                    this.GetContent(state).IsHitTestVisible = true;
                    this.MakeZoomInOutAnimation(this.GetContent(state), true);
                    if (this.CalendarView == DateNavigatorCalendarView.Month)
                    {
                        this.UpdateMonthContentCalendarsSelectedDates();
                    }
                }
                this.ActiveContent.FocusedDate = this.FocusedDate;
                this.ValueEditingStrategy.SetFocusedCellButtonFocusedState();
                this.UpdateCurrentDateText();
                this.NavigationCallbackService.VisibleDateRangeChanged(true);
                if ((this.CalendarView == DateNavigatorCalendarView.Month) && base.IsLoaded)
                {
                    this.InvalidateVisibleDateRange();
                }
            }
        }

        protected virtual void StyleSettingsChanged(DateNavigatorStyleSettingsBase oldSettings, DateNavigatorStyleSettingsBase newSettings)
        {
            ILogicalOwner owner = this;
            if (oldSettings != null)
            {
                owner.RemoveChild(oldSettings);
            }
            if (newSettings != null)
            {
                owner.AddChild(newSettings);
                newSettings.Initialize(this);
            }
            this.UpdateOptionsProviderServiceChangesSubscription();
        }

        object IServiceProvider.GetService(Type serviceType) => 
            ((IServiceProvider) this.ServiceContainer).GetService(serviceType);

        protected void UpdateActualFirstDayOfWeek()
        {
            this.ActualFirstDayOfWeek = this.GetActualFirstDayOfWeek();
        }

        protected virtual void UpdateActualWorkdaysProperties()
        {
            this.AreWorkdaysCollectionsInvalid = true;
        }

        protected void UpdateCalendarsCellStates()
        {
            if (this.ActiveContent != null)
            {
                ((IDateNavigatorContent) this.ActiveContent).UpdateCalendarsCellStates();
            }
        }

        protected void UpdateCalendarsDisabledDates()
        {
            this.UpdateActualWorkdaysProperties();
            if (this.ActiveContent != null)
            {
                ((IDateNavigatorContent) this.ActiveContent).UpdateCalendarsDisabledDates();
            }
        }

        protected void UpdateCalendarsHolidays()
        {
            this.UpdateActualWorkdaysProperties();
            if (this.ActiveContent != null)
            {
                ((IDateNavigatorContent) this.ActiveContent).UpdateCalendarsHolidays();
            }
        }

        private void UpdateCollectionChangedSubscription(object oldValue, object newValue, NotifyCollectionChangedEventHandler collectionChangedHandler)
        {
            if (oldValue is INotifyCollectionChanged)
            {
                (oldValue as INotifyCollectionChanged).CollectionChanged -= collectionChangedHandler;
            }
            if (newValue is INotifyCollectionChanged)
            {
                (newValue as INotifyCollectionChanged).CollectionChanged += collectionChangedHandler;
            }
        }

        protected internal void UpdateCurrentDateText()
        {
            this.CurrentDateText = this.GetCurrentDateText();
        }

        protected virtual void UpdateMonthContentCalendarsSelectedDates()
        {
            if (this.contents.ContainsKey(DateNavigatorCalendarView.Month))
            {
                this.contents[DateNavigatorCalendarView.Month].UpdateCalendarsSelectedDates();
            }
        }

        protected virtual void UpdateMonthContentCalendarsSpecialDates()
        {
            if (this.contents.ContainsKey(DateNavigatorCalendarView.Month))
            {
                this.contents[DateNavigatorCalendarView.Month].UpdateCalendarsSpecialDates();
            }
        }

        protected virtual void UpdateOptionsProviderServiceChangesSubscription()
        {
            if (this.OptionsProviderService != null)
            {
                this.OptionsProviderService.OptionsChanged -= new EventHandler(this.OnOptionsProviderServiceOptionsChanged);
            }
            IOptionsProviderService target = (IOptionsProviderService) ((IServiceProvider) this).GetService(typeof(IOptionsProviderService));
            if (target == null)
            {
                this.optionsProviderServiceReference = null;
            }
            else
            {
                this.optionsProviderServiceReference = new WeakReference(target);
                target.OptionsChanged += new EventHandler(this.OnOptionsProviderServiceOptionsChanged);
                this.UpdateOptionsProviderServiceProperties();
            }
            this.UpdateActualFirstDayOfWeek();
            this.UpdateActualWorkdaysProperties();
        }

        protected virtual void UpdateOptionsProviderServiceProperties()
        {
            if (this.OptionsProviderService != null)
            {
                this.OptionsProviderService.HighlightSpecialDates = this.HighlightSpecialDates;
                if (Equals(this.FocusedDate, FocusedDateProperty.DefaultMetadata.DefaultValue))
                {
                    base.SetCurrentValue(FocusedDateProperty, this.GetToday());
                }
            }
        }

        protected void UpdateSpecialDateList()
        {
            this.SpecialDateList = new List<DateTime>();
            if (this.SpecialDates != null)
            {
                foreach (DateTime date in this.SpecialDates)
                {
                    (this.DateCalculationService as DateNavigatorWorkdayCalculator).Do<DateNavigatorWorkdayCalculator>(x => x.InvalidateDateCache(date));
                    this.SpecialDateList.Add(date.Date);
                }
            }
        }

        private void UpdateTodayTimerOnTick(object sender, EventArgs eventArgs)
        {
            DateTime today = this.GetToday();
            if (this.today != today)
            {
                this.InvalidateToday();
                this.today = today;
            }
        }

        private static bool ValidatePropertyValueColumnCount(object value) => 
            ((int) value) >= 0;

        private static bool ValidatePropertyValueRowCount(object value) => 
            ((int) value) >= 0;

        protected internal DevExpress.Xpf.Editors.DateNavigator.SelectionManager SelectionManager { get; private set; }

        public DayOfWeek ActualFirstDayOfWeek
        {
            get => 
                (DayOfWeek) base.GetValue(ActualFirstDayOfWeekProperty);
            protected set => 
                base.SetValue(ActualFirstDayOfWeekPropertyKey, value);
        }

        [Description("")]
        public int MaxSelectionLength
        {
            get => 
                (int) base.GetValue(MaxSelectionLengthProperty);
            set => 
                base.SetValue(MaxSelectionLengthProperty, value);
        }

        [Description("")]
        public int ColumnCount
        {
            get => 
                (int) base.GetValue(ColumnCountProperty);
            set => 
                base.SetValue(ColumnCountProperty, value);
        }

        [Description("")]
        public string CurrentDateText
        {
            get => 
                (string) base.GetValue(CurrentDateTextProperty);
            protected set => 
                base.SetValue(CurrentDateTextPropertyKey, value);
        }

        [Description("")]
        public IList<DateTime> ExactWorkdays
        {
            get => 
                (IList<DateTime>) base.GetValue(ExactWorkdaysProperty);
            set => 
                base.SetValue(ExactWorkdaysProperty, value);
        }

        public DayOfWeek? FirstDayOfWeek
        {
            get => 
                (DayOfWeek?) base.GetValue(FirstDayOfWeekProperty);
            set => 
                base.SetValue(FirstDayOfWeekProperty, value);
        }

        [Description("")]
        public DateTime FocusedDate
        {
            get => 
                (DateTime) base.GetValue(FocusedDateProperty);
            set => 
                base.SetValue(FocusedDateProperty, value);
        }

        [Description("")]
        public IList<DateTime> SpecialDates
        {
            get => 
                (IList<DateTime>) base.GetValue(SpecialDatesProperty);
            set => 
                base.SetValue(SpecialDatesProperty, value);
        }

        public bool HighlightHolidays
        {
            get => 
                (bool) base.GetValue(HighlightHolidaysProperty);
            set => 
                base.SetValue(HighlightHolidaysProperty, value);
        }

        public bool HighlightSpecialDates
        {
            get => 
                (bool) base.GetValue(HighlightSpecialDatesProperty);
            set => 
                base.SetValue(HighlightSpecialDatesProperty, value);
        }

        [Description("")]
        public IList<DateTime> Holidays
        {
            get => 
                (IList<DateTime>) base.GetValue(HolidaysProperty);
            set => 
                base.SetValue(HolidaysProperty, value);
        }

        [Description("")]
        public bool IsMultiSelect
        {
            get => 
                (bool) base.GetValue(IsMultiSelectProperty);
            set => 
                base.SetValue(IsMultiSelectProperty, value);
        }

        [Description("")]
        public bool AllowMultipleRanges
        {
            get => 
                (bool) base.GetValue(AllowMultipleRangesProperty);
            set => 
                base.SetValue(AllowMultipleRangesProperty, value);
        }

        [Description("")]
        public int RowCount
        {
            get => 
                (int) base.GetValue(RowCountProperty);
            set => 
                base.SetValue(RowCountProperty, value);
        }

        [Description("")]
        public IList<DateTime> SelectedDates
        {
            get => 
                (IList<DateTime>) base.GetValue(SelectedDatesProperty);
            set => 
                base.SetValue(SelectedDatesProperty, value);
        }

        [Description("")]
        public bool ShowTodayButton
        {
            get => 
                (bool) base.GetValue(ShowTodayButtonProperty);
            set => 
                base.SetValue(ShowTodayButtonProperty, value);
        }

        [Description("")]
        public bool ShowWeekNumbers
        {
            get => 
                (bool) base.GetValue(ShowWeekNumbersProperty);
            set => 
                base.SetValue(ShowWeekNumbersProperty, value);
        }

        [Description("")]
        public DateNavigatorStyleSettingsBase StyleSettings
        {
            get => 
                (DateNavigatorStyleSettingsBase) base.GetValue(StyleSettingsProperty);
            set => 
                base.SetValue(StyleSettingsProperty, value);
        }

        [Description("")]
        public CalendarWeekRule? WeekNumberRule
        {
            get => 
                (CalendarWeekRule?) base.GetValue(WeekNumberRuleProperty);
            set => 
                base.SetValue(WeekNumberRuleProperty, value);
        }

        [Description("")]
        public IList<DayOfWeek> Workdays
        {
            get => 
                (IList<DayOfWeek>) base.GetValue(WorkdaysProperty);
            set => 
                base.SetValue(WorkdaysProperty, value);
        }

        [Description("")]
        public Style CalendarStyle
        {
            get => 
                (Style) base.GetValue(CalendarStyleProperty);
            set => 
                base.SetValue(CalendarStyleProperty, value);
        }

        [Description("")]
        public DateNavigatorCalendarView CalendarView
        {
            get => 
                (DateNavigatorCalendarView) base.GetValue(CalendarViewProperty);
            set => 
                base.SetValue(CalendarViewProperty, value);
        }

        public DateNavigatorDisplayMode DisplayMode
        {
            get => 
                (DateNavigatorDisplayMode) base.GetValue(DisplayModeProperty);
            internal set => 
                base.SetValue(DisplayModePropertyKey, value);
        }

        public IList<DateTime> DisabledDates
        {
            get => 
                (IList<DateTime>) base.GetValue(DisabledDatesProperty);
            set => 
                base.SetValue(DisabledDatesProperty, value);
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

        public Thickness CalendarPadding
        {
            get => 
                (Thickness) base.GetValue(CalendarPaddingProperty);
            set => 
                base.SetValue(CalendarPaddingProperty, value);
        }

        public DateTime FirstVisibleDate
        {
            get => 
                (DateTime) base.GetValue(FirstVisibleDateProperty);
            private set => 
                base.SetValue(FirstVisibleDatePropertyKey, value);
        }

        public DateTime LastVisibleDate
        {
            get => 
                (DateTime) base.GetValue(LastVisibleDateProperty);
            private set => 
                base.SetValue(LastVisibleDatePropertyKey, value);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShowClearButton
        {
            get => 
                (bool) base.GetValue(ShowClearButtonProperty);
            set => 
                base.SetValue(ShowClearButtonProperty, value);
        }

        string IDateEditCalendarBase.Mask { get; set; }

        bool IDateEditCalendarBase.ShowToday
        {
            get => 
                this.ShowTodayButton;
            set => 
                this.ShowTodayButton = value;
        }

        DateTime IDateEditCalendarBase.DateTime
        {
            get => 
                this.SelectedDates.First<DateTime>();
            set
            {
                List<DateTime> list1 = new List<DateTime>();
                list1.Add(value);
                this.SetSelectedDates(list1);
                this.FocusedDate = value;
            }
        }

        protected internal DateEdit OwnerDateEdit =>
            BaseEdit.GetOwnerEdit(this) as DateEdit;

        protected internal DateNavigatorContent ActiveContent =>
            this.GetContent(this.CalendarView);

        protected internal bool AreWorkdaysCollectionsInvalid { get; set; }

        protected internal List<DateTime> SpecialDateList { get; private set; }

        protected bool IsDateTimeChangedLocked =>
            this.lockerDateTime.IsLocked;

        protected bool IsSelectedDateListChangedLocked =>
            this.lockerSelectedDateList.IsLocked;

        protected bool IsSelectedDatesChangedLocked =>
            this.lockerSelectedDates.IsLocked;

        protected bool IsSyncSelectedDatesWithSelectedDateListLocked =>
            this.lockerSyncSelectedDatesWithSelectedDateList.IsLocked;

        protected internal IOptionsProviderService OptionsProviderService =>
            ((this.optionsProviderServiceReference == null) || !this.optionsProviderServiceReference.IsAlive) ? null : ((IOptionsProviderService) this.optionsProviderServiceReference.Target);

        private DateNavigatorStyleSettingsBase ServiceContainer =>
            (this.StyleSettings != null) ? this.StyleSettings : new DateNavigatorStyleSettings();

        private DevExpress.Xpf.Editors.DateNavigator.ValueEditingStrategy ValueEditingStrategy =>
            (DevExpress.Xpf.Editors.DateNavigator.ValueEditingStrategy) this.ServiceContainer.GetService<IValueEditingService>();

        internal IValueEditingService ValueEditingService =>
            this.ServiceContainer.GetService<IValueEditingService>();

        internal INavigationService NavigationService =>
            this.ServiceContainer.GetService<INavigationService>();

        internal INavigationCallbackService NavigationCallbackService =>
            this.ServiceContainer.GetService<INavigationCallbackService>();

        internal IValueValidatingService ValueValidatingService =>
            this.ServiceContainer.GetService<IValueValidatingService>();

        internal IDateCalculationService DateCalculationService =>
            this.ServiceContainer.GetService<IDateCalculationService>();

        protected override IEnumerator LogicalChildren =>
            this.logicalChildren.GetEnumerator();

        bool ILogicalOwner.IsLoaded =>
            base.IsLoaded;

        double ILogicalOwner.ActualWidth =>
            base.ActualWidth;

        double ILogicalOwner.ActualHeight =>
            base.ActualHeight;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DevExpress.Xpf.Editors.DateNavigator.DateNavigator.<>c <>9 = new DevExpress.Xpf.Editors.DateNavigator.DateNavigator.<>c();
            public static Action<DateNavigatorWorkdayCalculator> <>9__202_0;
            public static Action<DateNavigatorWorkdayCalculator> <>9__241_0;
            public static Action<DateNavigatorWorkdayCalculator> <>9__244_0;
            public static Action<DateNavigatorWorkdayCalculator> <>9__247_0;
            public static Action<DateNavigatorWorkdayCalculator> <>9__251_0;
            public static Action<DateNavigatorWorkdayCalculator> <>9__257_0;
            public static Action<DateNavigatorWorkdayCalculator> <>9__259_0;
            public static Action<DateNavigatorWorkdayCalculator> <>9__260_0;

            internal void <.cctor>b__39_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).MaxSelectionLengthChanged((int) e.OldValue, (int) e.NewValue);
            }

            internal void <.cctor>b__39_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).CalendarViewChanged((DateNavigatorCalendarView) e.OldValue, (DateNavigatorCalendarView) e.NewValue);
            }

            internal void <.cctor>b__39_10(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).IsMultiSelectChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal void <.cctor>b__39_11(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).AllowMultipleRangesChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal void <.cctor>b__39_12(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedSelectedDates((IList<DateTime>) e.OldValue, (IList<DateTime>) e.NewValue);
            }

            internal object <.cctor>b__39_13(DependencyObject d, object value) => 
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).CoerceSelectedDates(value);

            internal void <.cctor>b__39_14(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedShowWeekNumbers();
            }

            internal void <.cctor>b__39_15(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedSpecialDates((IList<DateTime>) e.OldValue);
            }

            internal void <.cctor>b__39_16(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).StyleSettingsChanged((DateNavigatorStyleSettingsBase) e.OldValue, (DateNavigatorStyleSettingsBase) e.NewValue);
            }

            internal object <.cctor>b__39_17(DependencyObject d, object value) => 
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).CreateDefaultStyleSettings(value);

            internal void <.cctor>b__39_18(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedWeekNumberRule((CalendarWeekRule?) e.OldValue);
            }

            internal void <.cctor>b__39_19(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedWorkdays((IList<DayOfWeek>) e.OldValue);
            }

            internal void <.cctor>b__39_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedCalendarStyle((Style) e.OldValue);
            }

            internal void <.cctor>b__39_20(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedDisabledDates((IList<DateTime>) e.OldValue);
            }

            internal void <.cctor>b__39_21(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedMinValue((DateTime?) e.OldValue);
            }

            internal void <.cctor>b__39_22(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedMaxValue((DateTime?) e.OldValue);
            }

            internal void <.cctor>b__39_23(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedFirstVisibleDate((DateTime) e.OldValue);
            }

            internal void <.cctor>b__39_24(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedLastVisibleDate((DateTime) e.OldValue);
            }

            internal void <.cctor>b__39_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedExactWorkdays((IList<DateTime>) e.OldValue);
            }

            internal void <.cctor>b__39_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedFirstDayOfWeek((DayOfWeek?) e.OldValue);
            }

            internal void <.cctor>b__39_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).FocusedDateChanged((DateTime) e.OldValue, (DateTime) e.NewValue);
            }

            internal object <.cctor>b__39_6(DependencyObject d, object value) => 
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).CoerceFocusedDate(value);

            internal void <.cctor>b__39_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedHighlightHolidays((bool) e.OldValue);
            }

            internal void <.cctor>b__39_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedHighlightSpecialDates((bool) e.OldValue);
            }

            internal void <.cctor>b__39_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DateNavigator.DateNavigator) d).PropertyChangedHolidays((IList<DateTime>) e.OldValue);
            }

            internal void <OnSelectedDatesChanged>b__241_0(DateNavigatorWorkdayCalculator x)
            {
                x.InvalidateDatesCache();
            }

            internal void <OnWorkdaysChanged>b__244_0(DateNavigatorWorkdayCalculator x)
            {
                x.InvalidateDatesCache();
            }

            internal void <PropertyChangedDisabledDates>b__260_0(DateNavigatorWorkdayCalculator x)
            {
                x.InvalidateDatesCache();
            }

            internal void <PropertyChangedExactWorkdays>b__247_0(DateNavigatorWorkdayCalculator x)
            {
                x.InvalidateDatesCache();
            }

            internal void <PropertyChangedHolidays>b__251_0(DateNavigatorWorkdayCalculator x)
            {
                x.InvalidateDatesCache();
            }

            internal void <PropertyChangedSpecialDates>b__257_0(DateNavigatorWorkdayCalculator x)
            {
                x.InvalidateDatesCache();
            }

            internal void <PropertyChangedWorkdays>b__259_0(DateNavigatorWorkdayCalculator x)
            {
                x.InvalidateDatesCache();
            }

            internal void <RefreshCellStates>b__202_0(DateNavigatorWorkdayCalculator x)
            {
                x.InvalidateDatesCache();
            }
        }

        protected enum NavigationDirection
        {
            Left,
            Up,
            Right,
            Down
        }
    }
}

