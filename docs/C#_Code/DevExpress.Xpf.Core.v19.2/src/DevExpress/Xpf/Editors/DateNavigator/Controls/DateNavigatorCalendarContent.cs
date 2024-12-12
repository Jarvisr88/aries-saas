namespace DevExpress.Xpf.Editors.DateNavigator.Controls
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.DateNavigator;
    using DevExpress.Xpf.Editors.DateNavigator.Internal;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    [ToolboxItem(false)]
    public class DateNavigatorCalendarContent : ContentControl
    {
        public static readonly DependencyProperty StateProperty = DependencyPropertyManager.Register("State", typeof(DateNavigatorCalendarView), typeof(DateNavigatorCalendarContent), new FrameworkPropertyMetadata(DateNavigatorCalendarView.Month, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty DateTimeProperty = DependencyPropertyManager.Register("DateTime", typeof(System.DateTime), typeof(DateNavigatorCalendarContent), new FrameworkPropertyMetadata(System.DateTime.Now, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(DateNavigatorCalendarContent.OnDateTimePropertyChanged)));

        public DateNavigatorCalendarContent()
        {
            this.CellButtonIndexList = new List<int>();
        }

        protected virtual void AddCellInfo(int row, int col)
        {
            this.AddCellInfo(row, col, System.DateTime.MinValue, string.Empty, false);
        }

        protected virtual void AddCellInfo(int row, int col, System.DateTime current, string content, bool isHidden)
        {
            DateNavigatorCalendarCellButton o = (DateNavigatorCalendarCellButton) this.FindChild(this.CellFirstCol + col, this.CellFirstRow + row, typeof(DateNavigatorCalendarCellButton));
            if (o == null)
            {
                DateNavigatorCalendarCellButton button1 = new DateNavigatorCalendarCellButton();
                button1.CalendarView = this.State;
                o = button1;
                FocusHelper2.SetFocusable(o, false);
                o.Click += new RoutedEventHandler(this.OnCellButtonClick);
                Grid.SetRow(o, this.CellFirstRow + row);
                Grid.SetColumn(o, this.CellFirstCol + col);
                this.ContentGrid.Children.Add(o);
            }
            o.Content = content;
            if (string.IsNullOrEmpty(content))
            {
                UIElementHelper.Collapse(o);
            }
            else
            {
                UIElementHelper.Show(o);
            }
            DateNavigatorCalendar.SetDateTime(o, current);
            o.IsEnabled = !isHidden;
            o.Opacity = isHidden ? ((double) 0) : ((double) 1);
            this.UpdateCellInfo(o, current);
        }

        protected virtual void AddWeekNumber(int row, System.DateTime current, bool showWeekNumbers)
        {
            TextBlock element = (TextBlock) this.FindChild(0, this.CellFirstRow + row, typeof(TextBlock));
            if (!showWeekNumbers)
            {
                if (element != null)
                {
                    this.ContentGrid.Children.Remove(element);
                }
            }
            else
            {
                bool flag = element != null;
                if (!flag)
                {
                    TextBlock block1 = new TextBlock();
                    block1.HorizontalAlignment = HorizontalAlignment.Right;
                    element = block1;
                    Grid.SetRow(element, this.CellFirstRow + row);
                    Grid.SetColumn(element, 0);
                }
                element.Text = this.GetWeekNumber(current).ToString(CultureInfo.CurrentCulture);
                DateNavigatorCalendar.SetWeekFirstDate(element, DateNavigatorCalendar.GetDateTime((DateNavigatorCalendarCellButton) this.FindChild(this.CellFirstCol, this.CellFirstRow + row, typeof(DateNavigatorCalendarCellButton))));
                element.Style = this.Calendar.WeekNumberStyle;
                element.MouseLeftButtonDown += new MouseButtonEventHandler(this.OnWeekNumberMouseLeftButtonDown);
                if (!flag)
                {
                    this.ContentGrid.Children.Add(element);
                }
            }
        }

        protected virtual void CalcDayNumberCells()
        {
            System.DateTime minValue = this.Calendar.GetMinValue();
            int row = 0;
            while (row < 6)
            {
                int num3 = (row == 0) ? this.FirstCellIndex : 0;
                int col = 0;
                while (true)
                {
                    if (col >= 7)
                    {
                        System.DateTime time2 = minValue;
                        System.DateTime? nullable = this.Calendar.MinValue;
                        if ((nullable != null) ? (time2 != nullable.GetValueOrDefault()) : true)
                        {
                            this.AddWeekNumber(row, minValue, this.Calendar.ShowWeekNumbers);
                        }
                        row++;
                        break;
                    }
                    if ((row == 0) && (col < num3))
                    {
                        this.AddCellInfo(row, col);
                    }
                    else
                    {
                        int num4 = ((row * 7) + col) - this.FirstCellIndex;
                        try
                        {
                            minValue = this.FirstVisibleDate.AddDays((double) num4);
                        }
                        catch
                        {
                            double totalDays = (System.DateTime.MaxValue - this.FirstVisibleDate).TotalDays;
                            minValue = System.DateTime.MinValue.AddDays(num4 - totalDays);
                        }
                        this.AddCellInfo(row, col, minValue, minValue.Day.ToString(CultureInfo.CurrentCulture), !this.Calendar.CanAddDate(minValue));
                    }
                    col++;
                }
            }
        }

        protected virtual void CalcFirstVisibleDate()
        {
            this.SetFirstVisibleDate(this.GetFirstVisibleDate(this.DateTime));
        }

        protected virtual void CalcYearInfoCells()
        {
            this.CreateInfoCells(new CreateCellInfo(this.CreateMonthCellInfo));
        }

        protected virtual void CalcYearsGroupInfoCells()
        {
            this.CreateInfoCells(new CreateCellInfo(this.CreateYearsGroupCellInfo));
        }

        protected virtual void CalcYearsInfoCells()
        {
            this.CreateInfoCells(new CreateCellInfo(this.CreateYearCellInfo));
        }

        protected virtual void CreateInfoCells(CreateCellInfo method)
        {
            int row = 0;
            while (row < 3)
            {
                int col = 0;
                while (true)
                {
                    if (col >= 4)
                    {
                        row++;
                        break;
                    }
                    method(row, col);
                    col++;
                }
            }
        }

        protected virtual void CreateMonthCellInfo(int row, int col)
        {
            System.DateTime current = new System.DateTime(this.DateTime.Year, (1 + (row * 4)) + col, 1);
            this.AddCellInfo(row, col, current, this.GetMonthName(current.Month), (current > this.Calendar.GetMaxValue()) || ((current < this.Calendar.GetMinValue()) && (current.Month < this.Calendar.GetMinValue().Month)));
        }

        protected virtual void CreateYearCellInfo(int row, int col)
        {
            int year = ((((this.DateTime.Year / 10) * 10) - 1) + (row * 4)) + col;
            if ((year < CultureInfo.CurrentCulture.Calendar.MinSupportedDateTime.Year) || (year > CultureInfo.CurrentCulture.Calendar.MaxSupportedDateTime.Year))
            {
                this.AddCellInfo(row, col);
            }
            else
            {
                System.DateTime current = new System.DateTime(year, 1, 1);
                this.AddCellInfo(row, col, current, current.ToString("yyyy", CultureInfo.CurrentCulture), !this.Calendar.CanAddDate(current));
            }
        }

        protected virtual void CreateYearsGroupCellInfo(int row, int col)
        {
            int year = (((this.DateTime.Year / 100) * 100) - 10) + (((row * 4) + col) * 10);
            if ((year < CultureInfo.CurrentCulture.Calendar.MinSupportedDateTime.Year) || (year > CultureInfo.CurrentCulture.Calendar.MaxSupportedDateTime.Year))
            {
                this.AddCellInfo(row, col);
            }
            else
            {
                int num3 = year + 9;
                year ??= 1;
                System.DateTime current = new System.DateTime(year, 1, 1);
                this.AddCellInfo(row, col, current, new System.DateTime(year, 1, 1).ToString("yyyy", CultureInfo.CurrentCulture) + "-\n" + new System.DateTime(num3, 1, 1).ToString("yyyy", CultureInfo.CurrentCulture), !this.Calendar.CanAddDate(current));
            }
        }

        protected void FillCellButtonIndexList()
        {
            this.CellButtonIndexList.Clear();
            for (int i = 0; i < this.ContentGrid.Children.Count; i++)
            {
                UIElement element = this.ContentGrid.Children[i];
                if ((element is DateNavigatorCalendarCellButton) && !string.IsNullOrEmpty((string) (element as DateNavigatorCalendarCellButton).Content))
                {
                    this.CellButtonIndexList.Add(i);
                }
            }
        }

        protected internal virtual void FillContent()
        {
            if ((this.ContentGrid != null) && (this.Calendar != null))
            {
                this.FillWeekDaysAbbreviation();
                this.FillContentInfo();
                this.FillCellButtonIndexList();
            }
        }

        protected virtual void FillContentInfo()
        {
            this.CalcFirstVisibleDate();
            switch (this.State)
            {
                case DateNavigatorCalendarView.Month:
                    this.FillMonthInfo();
                    return;

                case DateNavigatorCalendarView.Year:
                    this.FillYearInfo();
                    return;

                case DateNavigatorCalendarView.Years:
                    this.FillYearsInfo();
                    return;

                case DateNavigatorCalendarView.YearsRange:
                    this.FillYearsGroupInfo();
                    return;
            }
        }

        protected virtual void FillMonthInfo()
        {
            this.ContentGrid.BeginInit();
            this.CalcDayNumberCells();
            this.ContentGrid.EndInit();
        }

        protected virtual void FillWeekDaysAbbreviation()
        {
            if (this.State == DateNavigatorCalendarView.Month)
            {
                int col = 2;
                for (int i = 0; i < this.DateTimeFormat.ShortestDayNames.Length; i++)
                {
                    TextBlock element = (TextBlock) this.FindChild(col, 0, typeof(TextBlock));
                    bool flag = element != null;
                    string str = this.DateTimeFormat.ShortestDayNames[(Convert.ToInt32(this.FirstDayOfWeek) + i) % 7];
                    if (!flag)
                    {
                        element = new TextBlock();
                        Grid.SetRow(element, 0);
                        Grid.SetColumn(element, col);
                        this.ContentGrid.Children.Add(element);
                    }
                    element.Text = str;
                    element.Style = this.Calendar.WeekdayAbbreviationStyle;
                    col++;
                }
            }
        }

        protected virtual void FillYearInfo()
        {
            this.ContentGrid.BeginInit();
            this.CalcYearInfoCells();
            this.ContentGrid.EndInit();
        }

        protected virtual void FillYearsGroupInfo()
        {
            this.ContentGrid.BeginInit();
            this.CalcYearsGroupInfoCells();
            this.ContentGrid.EndInit();
        }

        protected virtual void FillYearsInfo()
        {
            this.ContentGrid.BeginInit();
            this.CalcYearsInfoCells();
            this.ContentGrid.EndInit();
        }

        internal DateNavigatorCalendarCellButton FindCellButton(System.DateTime cellDateTime)
        {
            DateNavigatorCalendarCellButton button;
            if (this.ContentGrid == null)
            {
                return null;
            }
            using (IEnumerator enumerator = this.ContentGrid.Children.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        UIElement current = (UIElement) enumerator.Current;
                        if (!(current is DateNavigatorCalendarCellButton) || !this.IsCellButtonDateTime(cellDateTime, DateNavigatorCalendar.GetDateTime(current)))
                        {
                            continue;
                        }
                        button = (DateNavigatorCalendarCellButton) current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return button;
        }

        private UIElement FindChild(int col, int row, Type childType)
        {
            UIElement element2;
            using (IEnumerator enumerator = this.ContentGrid.Children.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        UIElement current = (UIElement) enumerator.Current;
                        if ((Grid.GetColumn((FrameworkElement) current) != col) || (Grid.GetRow((FrameworkElement) current) != row))
                        {
                            continue;
                        }
                        if (childType.IsInstanceOfType(current))
                        {
                            element2 = current;
                            break;
                        }
                        this.ContentGrid.Children.Clear();
                    }
                    return null;
                }
            }
            return element2;
        }

        public DateNavigatorCalendarCellButton GetCellButton(System.DateTime dt)
        {
            int days = -1;
            System.DateTime dateTime = DateNavigatorCalendar.GetDateTime(this.ContentGrid.Children[this.CellButtonIndexList[0]]);
            switch (this.State)
            {
                case DateNavigatorCalendarView.Month:
                    days = (dt - dateTime).Days;
                    break;

                case DateNavigatorCalendarView.Year:
                    days = dt.Month - dateTime.Month;
                    break;

                case DateNavigatorCalendarView.Years:
                    days = dt.Year - dateTime.Year;
                    break;

                case DateNavigatorCalendarView.YearsRange:
                    days = (dt.Year / 10) - (dateTime.Year / 10);
                    break;

                default:
                    break;
            }
            return (DateNavigatorCalendarCellButton) this.ContentGrid.Children[this.CellButtonIndexList[days]];
        }

        protected internal virtual string GetCurrentDateText() => 
            DateNavigatorHelper.GetDateText(this.State, this.DateTime);

        public void GetDateRange(bool excludeInactiveContent, out System.DateTime firstDate, out System.DateTime lastDate)
        {
            if (excludeInactiveContent)
            {
                firstDate = this.DateTime;
                switch (this.State)
                {
                    case DateNavigatorCalendarView.Month:
                        lastDate = new System.DateTime(this.DateTime.Year, this.DateTime.Month, System.DateTime.DaysInMonth(this.DateTime.Year, this.DateTime.Month));
                        return;

                    case DateNavigatorCalendarView.Year:
                        lastDate = new System.DateTime(this.DateTime.Year, 12, 1);
                        return;

                    case DateNavigatorCalendarView.Years:
                        lastDate = new System.DateTime(((this.DateTime.Year / 10) * 10) + 9, 1, 1);
                        return;

                    case DateNavigatorCalendarView.YearsRange:
                        lastDate = new System.DateTime(((this.DateTime.Year / 100) * 100) + 0x63, 1, 1);
                        return;
                }
                throw new Exception();
            }
            if (this.ContentGrid == null)
            {
                base.ApplyTemplate();
            }
            firstDate = DateNavigatorCalendar.GetDateTime(this.ContentGrid.Children[this.CellButtonIndexList[0]]);
            lastDate = DateNavigatorCalendar.GetDateTime(this.ContentGrid.Children[this.CellButtonIndexList[this.CellButtonIndexList.Count - 1]]);
            if ((lastDate.Year == System.DateTime.MinValue.Year) && (firstDate.Year == System.DateTime.MaxValue.Year))
            {
                lastDate = System.DateTime.MaxValue;
            }
            switch (this.State)
            {
                case DateNavigatorCalendarView.Year:
                    lastDate = new System.DateTime(lastDate.Year, lastDate.Month, System.DateTime.DaysInMonth(lastDate.Year, lastDate.Month));
                    return;

                case DateNavigatorCalendarView.Years:
                    lastDate = new System.DateTime(lastDate.Year, 12, 0x1f);
                    return;

                case DateNavigatorCalendarView.YearsRange:
                    lastDate = new System.DateTime(lastDate.Year + 9, 12, 0x1f);
                    return;
            }
        }

        private IEnumerable<System.DateTime> GetDatesForCalendar(IEnumerable<System.DateTime> dates)
        {
            System.DateTime minValue;
            System.DateTime maxValue;
            if ((this.Calendar == null) || (dates == null))
            {
                return new List<System.DateTime>();
            }
            this.GetDateRange(false, out minValue, out maxValue);
            return (from dt in dates
                where (dt >= minValue) && (dt <= maxValue)
                select dt);
        }

        protected int GetFirstDayOffset(System.DateTime firstMonthDate) => 
            (this.FirstDayOfWeek == firstMonthDate.DayOfWeek) ? 7 : (((7 + firstMonthDate.DayOfWeek) - this.FirstDayOfWeek) % 7);

        protected System.DateTime GetFirstMonthDate(System.DateTime value) => 
            new System.DateTime(value.Year, value.Month, 1, value.Hour, value.Minute, value.Second, value.Millisecond);

        protected System.DateTime GetFirstVisibleDate(System.DateTime value)
        {
            System.DateTime firstMonthDate = this.GetFirstMonthDate(value);
            TimeSpan span = TimeSpan.FromDays((double) -this.GetFirstDayOffset(firstMonthDate));
            if ((firstMonthDate.Ticks + span.Ticks) < 0L)
            {
                return System.DateTime.MinValue;
            }
            try
            {
                return (firstMonthDate + span);
            }
            catch (ArgumentOutOfRangeException)
            {
                return this.Calendar.GetMinValue();
            }
        }

        public FrameworkElement GetFocusedCell()
        {
            if (this.ContentGrid != null)
            {
                using (IEnumerator enumerator = this.ContentGrid.Children.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        UIElement current = (UIElement) enumerator.Current;
                        if (DateNavigatorCalendar.GetCellState(current).HasFlag(DateNavigatorCalendarCellState.Focused))
                        {
                            return (current as FrameworkElement);
                        }
                    }
                }
            }
            return null;
        }

        protected virtual string GetMonthName(int month) => 
            CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[month - 1];

        protected internal object GetTemplateChildCore(string name) => 
            base.GetTemplateChild(name);

        protected internal System.DateTime GetWeekFirstDateByDate(System.DateTime dt) => 
            DateNavigatorCalendar.GetWeekFirstDate((TextBlock) this.FindChild(0, Grid.GetRow(this.GetCellButton(dt)), typeof(TextBlock)));

        protected virtual int GetWeekNumber(System.DateTime date) => 
            this.DateTimeFormat.Calendar.GetWeekOfYear(date, this.GetWeekNumberRule(), this.FirstDayOfWeek);

        protected virtual CalendarWeekRule GetWeekNumberRule() => 
            (this.Calendar.WeekNumberRule == null) ? this.DateTimeFormat.CalendarWeekRule : this.Calendar.WeekNumberRule.Value;

        private bool IsCellButtonDateTime(System.DateTime dt, System.DateTime cellButtonDateTime)
        {
            switch (this.State)
            {
                case DateNavigatorCalendarView.Month:
                    return (dt == cellButtonDateTime);

                case DateNavigatorCalendarView.Year:
                    return ((dt.Year == cellButtonDateTime.Year) && (dt.Month == cellButtonDateTime.Month));

                case DateNavigatorCalendarView.Years:
                    return (dt.Year == cellButtonDateTime.Year);

                case DateNavigatorCalendarView.YearsRange:
                    return ((dt.Year / 10) == (cellButtonDateTime.Year / 10));
            }
            throw new Exception();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.ContentGrid = base.GetTemplateChild("PART_ContentGrid") as Grid;
            if (this.Calendar != null)
            {
                this.Calendar.OnApplyContentTemplate(this);
            }
            this.FillContent();
            if (this.State == DateNavigatorCalendarView.Month)
            {
                this.UpdateSelectedDates();
            }
        }

        protected virtual void OnCellButtonClick(object sender, RoutedEventArgs e)
        {
            DateNavigatorCalendarCellButton button = (DateNavigatorCalendarCellButton) sender;
            button.ReleaseMouseCapture();
            this.Calendar.OnButtonClick(DateNavigatorCalendar.GetDateTime(button), DateNavigatorCalendarButtonKind.Date);
        }

        protected virtual void OnDateTimeChanged()
        {
            if (this.Calendar != null)
            {
                this.Calendar.DateTime = this.DateTime;
            }
        }

        protected static void OnDateTimePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((DateNavigatorCalendarContent) obj).OnDateTimeChanged();
        }

        protected virtual void OnWeekNumberMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Calendar.OnButtonClick(DateNavigatorCalendar.GetWeekFirstDate((TextBlock) sender), DateNavigatorCalendarButtonKind.WeekNumber);
        }

        public virtual void SetFirstVisibleDate(System.DateTime firstVisibleDate)
        {
            this.FirstCellIndex = 0;
            this.FirstVisibleDate = firstVisibleDate;
            if (this.FirstVisibleDate == System.DateTime.MinValue)
            {
                this.FirstCellIndex = this.GetFirstDayOffset(this.FirstVisibleDate) - (this.FirstVisibleDate - this.FirstVisibleDate).Days;
            }
        }

        protected virtual void UpdateCellInfo(DependencyObject obj, System.DateTime current)
        {
            switch (this.State)
            {
                case DateNavigatorCalendarView.Month:
                    this.UpdateMonthInfoCell(obj, current);
                    return;

                case DateNavigatorCalendarView.Year:
                    this.UpdateYearInfoCell(obj, current);
                    return;

                case DateNavigatorCalendarView.Years:
                    this.UpdateYearsInfoCell(obj, current);
                    return;

                case DateNavigatorCalendarView.YearsRange:
                    this.UpdateYearsGroupInfoCell(obj, current);
                    return;
            }
        }

        protected internal void UpdateCellInfos()
        {
            if (this.ContentGrid != null)
            {
                foreach (DependencyObject obj2 in this.ContentGrid.Children)
                {
                    this.UpdateCellInfo(obj2, DateNavigatorCalendar.GetDateTime(obj2));
                }
            }
        }

        private void UpdateCellState(DependencyObject obj, System.DateTime current)
        {
            DateNavigatorCalendar.SetCellHoliday(obj, false);
            DateNavigatorCalendar.SetCellSpecial(obj, false);
            DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator = DevExpress.Xpf.Editors.DateNavigator.DateNavigator.GetNavigator(this);
            if (navigator != null)
            {
                DateNavigatorCalendar.SetCellFocused(obj, DateComparer.Equals(this.State, current, navigator.FocusedDate));
                DateNavigatorCalendar.SetCellDisabled(obj, !DateNavigatorHelper.HasEnabledDatesInView(navigator, current));
            }
        }

        protected void UpdateDates(IList<System.DateTime> dates, DateNavigatorCalendarCellState flag)
        {
            if (this.ContentGrid != null)
            {
                IList<System.DateTime> selectedDates = this.GetDatesForCalendar(dates).ToList<System.DateTime>();
                foreach (UIElement element in this.ContentGrid.Children)
                {
                    if (element is DateNavigatorCalendarCellButton)
                    {
                        DateNavigatorCalendar.SetCellStateFlag(element, flag, SelectedDatesHelper.Contains(selectedDates, DateNavigatorCalendar.GetDateTime(element)));
                    }
                }
            }
        }

        protected internal void UpdateDisabledDates()
        {
            DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator = DevExpress.Xpf.Editors.DateNavigator.DateNavigator.GetNavigator(this);
            if ((this.ContentGrid != null) && (navigator != null))
            {
                IDateCalculationService service = (IDateCalculationService) ((IServiceProvider) navigator).GetService(typeof(IDateCalculationService));
                if (service != null)
                {
                    foreach (UIElement element in this.ContentGrid.Children)
                    {
                        if (element is DateNavigatorCalendarCellButton)
                        {
                            DateNavigatorCalendar.SetCellStateFlag(element, DateNavigatorCalendarCellState.Disabled, service.IsDisabled(DateNavigatorCalendar.GetDateTime(element)));
                        }
                    }
                }
            }
        }

        protected internal void UpdateHolidays()
        {
            DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator = DevExpress.Xpf.Editors.DateNavigator.DateNavigator.GetNavigator(this);
            if ((this.ContentGrid != null) && (navigator != null))
            {
                IDateCalculationService service = (IDateCalculationService) ((IServiceProvider) navigator).GetService(typeof(IDateCalculationService));
                if (service != null)
                {
                    foreach (UIElement element in this.ContentGrid.Children)
                    {
                        if (element is DateNavigatorCalendarCellButton)
                        {
                            DateNavigatorCalendar.SetCellStateFlag(element, DateNavigatorCalendarCellState.Holiday, !service.IsWorkday(DateNavigatorCalendar.GetDateTime(element)) && this.Calendar.HighlightHolidays);
                        }
                    }
                }
            }
        }

        protected virtual void UpdateMonthInfoCell(DependencyObject obj, System.DateTime current)
        {
            if (this.Calendar != null)
            {
                DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator = DevExpress.Xpf.Editors.DateNavigator.DateNavigator.GetNavigator(this);
                System.DateTime time = (navigator != null) ? navigator.GetToday() : System.DateTime.Today;
                DateNavigatorCalendar.SetCellInactive(obj, current.Month != this.DateTime.Month);
                DateNavigatorCalendar.SetCellToday(obj, current.Date == time);
                if (navigator != null)
                {
                    System.DateTime focusedDate = navigator.FocusedDate;
                    IDateCalculationService service = (IDateCalculationService) ((IServiceProvider) navigator).GetService(typeof(IDateCalculationService));
                    DateNavigatorCalendar.SetCellFocused(obj, DateComparer.Equals(this.State, current, focusedDate));
                    if (service != null)
                    {
                        DateNavigatorCalendar.SetCellSpecial(obj, service.IsSpecialDate(current.Date) && this.Calendar.HighlightSpecialDates);
                        DateNavigatorCalendar.SetCellHoliday(obj, !service.IsWorkday(current) && this.Calendar.HighlightHolidays);
                        DateNavigatorCalendar.SetCellDisabled(obj, service.IsDisabled(current));
                    }
                    else
                    {
                        List<System.DateTime> specialDateList = DateNavigatorHelper.GetSpecialDateList(navigator);
                        List<System.DateTime> disabledDateList = DateNavigatorHelper.GetDisabledDateList(navigator);
                        DateNavigatorCalendar.SetCellSpecial(obj, SelectedDatesHelper.Contains(specialDateList, current.Date) && this.Calendar.HighlightSpecialDates);
                        DateNavigatorCalendar.SetCellDisabled(obj, SelectedDatesHelper.Contains(disabledDateList, current.Date));
                    }
                    IValueEditingService service2 = (IValueEditingService) ((IServiceProvider) navigator).GetService(typeof(IValueEditingService));
                    if (service2 != null)
                    {
                        DateNavigatorCalendar.SetCellSelected(obj, SelectedDatesHelper.Contains(service2.SelectedDates, current.Date));
                    }
                }
            }
        }

        protected internal void UpdateSelectedDates()
        {
            DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator = DevExpress.Xpf.Editors.DateNavigator.DateNavigator.GetNavigator(this);
            if ((this.ContentGrid != null) && (navigator != null))
            {
                IValueEditingService service = (IValueEditingService) ((IServiceProvider) navigator).GetService(typeof(IValueEditingService));
                if (service != null)
                {
                    this.UpdateDates(service.SelectedDates, DateNavigatorCalendarCellState.Selected);
                }
            }
        }

        protected internal void UpdateSpecialDates()
        {
            DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator = DevExpress.Xpf.Editors.DateNavigator.DateNavigator.GetNavigator(this);
            if ((this.ContentGrid != null) && (navigator != null))
            {
                IDateCalculationService service = (IDateCalculationService) ((IServiceProvider) navigator).GetService(typeof(IDateCalculationService));
                foreach (UIElement element in this.ContentGrid.Children)
                {
                    if (element is DateNavigatorCalendarCellButton)
                    {
                        DateNavigatorCalendar.SetCellStateFlag(element, DateNavigatorCalendarCellState.Special, service.IsSpecialDate(DateNavigatorCalendar.GetDateTime(element)) && this.Calendar.HighlightSpecialDates);
                    }
                }
            }
        }

        protected internal void UpdateToday()
        {
            DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator = DevExpress.Xpf.Editors.DateNavigator.DateNavigator.GetNavigator(this);
            if ((this.ContentGrid != null) && (navigator != null))
            {
                System.DateTime today = navigator.GetToday();
                foreach (UIElement element in this.ContentGrid.Children)
                {
                    if (element is DateNavigatorCalendarCellButton)
                    {
                        DateNavigatorCalendar.SetCellStateFlag(element, DateNavigatorCalendarCellState.Today, DateNavigatorCalendar.GetDateTime(element) == today);
                    }
                }
            }
        }

        protected virtual void UpdateYearInfoCell(DependencyObject obj, System.DateTime current)
        {
            this.UpdateCellState(obj, current);
        }

        protected virtual void UpdateYearsGroupInfoCell(DependencyObject obj, System.DateTime current)
        {
            if ((current.Year / 100) != (this.DateTime.Year / 100))
            {
                DateNavigatorCalendar.SetCellInactive(obj, true);
            }
            this.UpdateCellState(obj, current);
        }

        protected virtual void UpdateYearsInfoCell(DependencyObject obj, System.DateTime current)
        {
            if ((current.Year / 10) != (this.DateTime.Year / 10))
            {
                DateNavigatorCalendar.SetCellInactive(obj, true);
            }
            this.UpdateCellState(obj, current);
        }

        public System.DateTime DateTime
        {
            get => 
                (System.DateTime) base.GetValue(DateTimeProperty);
            set => 
                base.SetValue(DateTimeProperty, value);
        }

        public DateNavigatorCalendarView State
        {
            get => 
                (DateNavigatorCalendarView) base.GetValue(StateProperty);
            set => 
                base.SetValue(StateProperty, value);
        }

        public DateEdit OwnerDateEdit =>
            (DateEdit) BaseEdit.GetOwnerEdit(this);

        public DateNavigatorCalendar Calendar =>
            DateNavigatorCalendar.GetCalendar(this);

        protected int FirstCellIndex { get; set; }

        protected System.DateTime FirstVisibleDate { get; set; }

        protected DateTimeFormatInfo DateTimeFormat =>
            CultureInfo.CurrentCulture.DateTimeFormat;

        protected DayOfWeek FirstDayOfWeek =>
            ((this.Calendar == null) || (this.Calendar.FirstDayOfWeek == null)) ? this.DateTimeFormat.FirstDayOfWeek : this.Calendar.FirstDayOfWeek.Value;

        protected List<int> CellButtonIndexList { get; private set; }

        protected virtual int CellFirstRow =>
            (this.State != DateNavigatorCalendarView.Month) ? 0 : 2;

        protected virtual int CellFirstCol =>
            (this.State != DateNavigatorCalendarView.Month) ? 0 : 2;

        public Grid ContentGrid { get; private set; }

        protected delegate void CreateCellInfo(int row, int col);
    }
}

