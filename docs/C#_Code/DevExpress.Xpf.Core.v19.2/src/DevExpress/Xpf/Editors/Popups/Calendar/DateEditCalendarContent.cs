namespace DevExpress.Xpf.Editors.Popups.Calendar
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [ToolboxItem(false)]
    public class DateEditCalendarContent : ContentControl
    {
        public static readonly DependencyProperty StateProperty = DependencyPropertyManager.Register("State", typeof(DateEditCalendarState), typeof(DateEditCalendarContent), new FrameworkPropertyMetadata(DateEditCalendarState.Month, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty DateTimeProperty = DependencyPropertyManager.Register("DateTime", typeof(System.DateTime), typeof(DateEditCalendarContent), new FrameworkPropertyMetadata(System.DateTime.Now, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(DateEditCalendarContent.OnDateTimePropertyChanged)));
        private bool makePrevZoomOut;
        private bool makeCurrZoomOut;
        private bool makePrevZoomIn;
        private bool makeCurrZoomIn;
        private TransferContentControl currTransferContent;
        private TransferContentControl prevTransferContent;
        private Grid contentGrid;

        protected virtual void AddCellInfo(int row, int col)
        {
            this.AddCellInfo(row, col, System.DateTime.MinValue, string.Empty, false);
        }

        protected virtual void AddCellInfo(int row, int col, System.DateTime current, string content, bool isHidden)
        {
            CalendarCellButton button1 = new CalendarCellButton();
            button1.Content = content;
            button1.Focusable = false;
            CalendarCellButton element = button1;
            if (string.IsNullOrEmpty(content))
            {
                UIElementHelper.Collapse(element);
            }
            DateEditCalendarBase.SetDateTime(element, current);
            element.Click += new RoutedEventHandler(this.OnCellButtonClick);
            Grid.SetRow(element, this.CellFirstRow + row);
            Grid.SetColumn(element, this.CellFirstCol + col);
            if (isHidden)
            {
                element.Opacity = 0.0;
                element.IsEnabled = false;
            }
            this.ContentGrid.Children.Add(element);
            this.UpdateCellInfo(element, current);
        }

        protected virtual void AddWeekNumber(int row, System.DateTime current)
        {
            TextBlock element = new TextBlock {
                Text = this.GetWeekNumber(current).ToString(CultureInfo.CurrentCulture),
                Style = this.Calendar.WeekNumbersStyle
            };
            Grid.SetRow(element, this.CellFirstRow + row);
            Grid.SetColumn(element, 0);
            this.ContentGrid.Children.Add(element);
        }

        protected virtual void ApplyDelayedAnimations()
        {
            if (this.makeCurrZoomOut)
            {
                this.Calendar.CalendarTransfer.ZoomOutAnimation(this.currTransferContent);
            }
            if (this.makePrevZoomOut)
            {
                this.Calendar.CalendarTransfer.PrevZoomOutAnimation(this.prevTransferContent);
            }
            if (this.makeCurrZoomIn)
            {
                this.Calendar.CalendarTransfer.ZoomInAnimation(this.currTransferContent);
            }
            if (this.makePrevZoomIn)
            {
                this.Calendar.CalendarTransfer.PrevZoomInAnimation(this.prevTransferContent);
            }
            this.makeCurrZoomOut = false;
            this.makePrevZoomOut = false;
            this.makeCurrZoomIn = false;
            this.makePrevZoomIn = false;
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            Size size = base.ArrangeOverride(arrangeBounds);
            this.ApplyDelayedAnimations();
            return size;
        }

        protected virtual void CalcDayNumberCells()
        {
            int num2;
            int num3;
            System.DateTime minValue = this.Calendar.GetMinValue();
            int row = 0;
            goto TR_0012;
        TR_0005:
            if (this.Calendar.ShowWeekNumbers)
            {
                System.DateTime time2 = minValue;
                System.DateTime? nullable = this.Calendar.MinValue;
                if ((nullable != null) ? (time2 != nullable.GetValueOrDefault()) : true)
                {
                    this.AddWeekNumber(row, minValue);
                }
            }
            row++;
            goto TR_0012;
        TR_0006:
            num2++;
        TR_000F:
            while (true)
            {
                if (num2 < 7)
                {
                    if (row != 0)
                    {
                        break;
                    }
                    if (num2 >= num3)
                    {
                        break;
                    }
                    this.AddCellInfo(row, num2);
                }
                else
                {
                    goto TR_0005;
                }
                goto TR_0006;
            }
            try
            {
                minValue = this.FirstVisibleDate.AddDays((double) (((row * 7) + num2) - this.FirstCellIndex));
                this.AddCellInfo(row, num2, minValue, minValue.Day.ToString(CultureInfo.CurrentCulture), !this.CanAddDate(minValue));
                goto TR_0006;
            }
            catch
            {
                row = 6;
            }
            goto TR_0005;
        TR_0012:
            while (true)
            {
                if (row >= 6)
                {
                    return;
                }
                num3 = (row == 0) ? this.FirstCellIndex : 0;
                num2 = 0;
                break;
            }
            goto TR_000F;
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

        protected virtual bool CanAddDate(System.DateTime date)
        {
            System.DateTime time = date;
            System.DateTime? minValue = this.Calendar.MinValue;
            if ((minValue != null) ? (time < minValue.GetValueOrDefault()) : false)
            {
                return false;
            }
            time = date;
            minValue = this.Calendar.MaxValue;
            return !((minValue != null) ? (time > minValue.GetValueOrDefault()) : false);
        }

        protected virtual void ClearSelection()
        {
            if (this.ContentGrid != null)
            {
                foreach (UIElement element in this.ContentGrid.Children)
                {
                    DateEditCalendar.SetCellFocused(element, false);
                    DateEditCalendar.SetCellToday(element, false);
                }
            }
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
            if ((year > 0) && (year < 0x2710))
            {
                System.DateTime current = new System.DateTime(year, 1, 1);
                this.AddCellInfo(row, col, current, current.ToString("yyyy", CultureInfo.CurrentCulture), ((current >= this.Calendar.GetMinValue()) || (current.Year >= this.Calendar.GetMinValue().Year)) ? (current > this.Calendar.GetMaxValue()) : true);
            }
        }

        protected virtual void CreateYearsGroupCellInfo(int row, int col)
        {
            int year = (((this.DateTime.Year / 100) * 100) - 10) + (((row * 4) + col) * 10);
            if ((year >= 0) && (year < 0x2710))
            {
                int num3 = year + 9;
                year ??= 1;
                System.DateTime time = new System.DateTime(year, 1, 1);
                (new System.DateTime(year, 1, 1).ToString("yyyy", CultureInfo.CurrentCulture) + "-\n" + new System.DateTime(num3, 1, 1).ToString("yyyy", CultureInfo.CurrentCulture)).AddCellInfo((int) time, col, (System.DateTime) row, (string) this, ((time >= this.Calendar.GetMinValue()) || (num3 >= this.Calendar.GetMinValue().Year)) ? (time > this.Calendar.GetMaxValue()) : true);
            }
        }

        protected virtual void FillContent()
        {
            if (this.ContentGrid != null)
            {
                this.FillWeekDaysAbbreviation();
                this.FillContentInfo();
            }
        }

        protected virtual void FillContentInfo()
        {
            this.CalcFirstVisibleDate();
            switch (this.State)
            {
                case DateEditCalendarState.Month:
                    this.FillMonthInfo();
                    return;

                case DateEditCalendarState.Year:
                    this.FillYearInfo();
                    return;

                case DateEditCalendarState.Years:
                    this.FillYearsInfo();
                    return;

                case DateEditCalendarState.YearsGroup:
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
            if (this.State == DateEditCalendarState.Month)
            {
                int num = 2;
                for (int i = 0; i < this.DateTimeFormat.ShortestDayNames.Length; i++)
                {
                    string str = this.DateTimeFormat.ShortestDayNames[(Convert.ToInt32(this.DateTimeFormat.FirstDayOfWeek) + i) % 7];
                    TextBlock element = new TextBlock {
                        Text = str
                    };
                    Grid.SetRow(element, 0);
                    Grid.SetColumn(element, num);
                    element.Style = this.Calendar.WeekdayAbbreviationStyle;
                    this.ContentGrid.Children.Add(element);
                    num++;
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

        protected internal virtual string GetCurrentDateText()
        {
            if (this.State == DateEditCalendarState.Month)
            {
                return string.Format("{0:" + CultureInfo.CurrentCulture.DateTimeFormat.YearMonthPattern + "}", this.DateTime);
            }
            if (this.State == DateEditCalendarState.Year)
            {
                return this.DateTime.ToString("yyyy", CultureInfo.CurrentCulture);
            }
            if (this.State == DateEditCalendarState.Years)
            {
                object[] objArray1 = new object[] { new System.DateTime(Math.Max((this.DateTime.Year / 10) * 10, 1), 1, 1), new System.DateTime(((this.DateTime.Year / 10) * 10) + 9, 1, 1) };
                return string.Format(CultureInfo.CurrentCulture, "{0:yyyy}-{1:yyyy}", objArray1);
            }
            object[] args = new object[] { new System.DateTime(Math.Max((this.DateTime.Year / 100) * 100, 1), 1, 1), new System.DateTime(((this.DateTime.Year / 100) * 100) + 0x63, 1, 1) };
            return string.Format(CultureInfo.CurrentCulture, "{0:yyyy}-{1:yyyy}", args);
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
                        if (DateEditCalendar.GetCellFocused(current))
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

        protected virtual int GetWeekNumber(System.DateTime date) => 
            this.DateTimeFormat.Calendar.GetWeekOfYear(date, this.GetWeekNumberRule(), this.FirstDayOfWeek);

        protected virtual CalendarWeekRule GetWeekNumberRule() => 
            this.DateTimeFormat.CalendarWeekRule;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.Calendar != null)
            {
                this.Calendar.OnApplyContentTemplate(this);
            }
            this.FillContent();
        }

        protected virtual void OnCellButtonClick(object sender, RoutedEventArgs e)
        {
            if ((this.Calendar != null) && ReferenceEquals(this.Calendar.ActiveContent, this))
            {
                this.ClearSelection();
                this.UpdateClickedButton(sender as Button);
                this.Calendar.OnCellButtonClick(sender, e);
            }
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
            ((DateEditCalendarContent) obj).OnDateTimeChanged();
        }

        internal void SetDelayedNone(TransferContentControl control)
        {
            this.prevTransferContent = control;
            this.prevTransferContent.Visibility = Visibility.Hidden;
        }

        internal void SetDelayedPrevZoomIn(TransferContentControl control)
        {
            this.prevTransferContent = control;
            this.makePrevZoomIn = true;
        }

        internal void SetDelayedPrevZoomOut(TransferContentControl control)
        {
            this.prevTransferContent = control;
            this.makePrevZoomOut = true;
        }

        internal void SetDelayedZoomIn(TransferContentControl control)
        {
            this.currTransferContent = control;
            this.makeCurrZoomIn = true;
        }

        internal void SetDelayedZoomOut(TransferContentControl control)
        {
            this.currTransferContent = control;
            this.makeCurrZoomOut = true;
        }

        public virtual void SetFirstVisibleDate(System.DateTime firstVisibleDate)
        {
            this.FirstCellIndex = 0;
            this.FirstVisibleDate = firstVisibleDate;
            if (this.FirstVisibleDate == System.DateTime.MinValue)
            {
                this.FirstCellIndex = this.GetFirstDayOffset(this.FirstVisibleDate);
            }
        }

        protected virtual void UpdateCellInfo(DependencyObject obj, System.DateTime current)
        {
            switch (this.State)
            {
                case DateEditCalendarState.Month:
                    this.UpdateMonthInfoCell(obj, current);
                    return;

                case DateEditCalendarState.Year:
                    this.UpdateYearInfoCell(obj, current);
                    return;

                case DateEditCalendarState.Years:
                    this.UpdateYearsInfoCell(obj, current);
                    return;

                case DateEditCalendarState.YearsGroup:
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
                    this.UpdateCellInfo(obj2, (System.DateTime) DateEditCalendarBase.GetDateTime(obj2));
                }
            }
        }

        protected virtual void UpdateClickedButton(Button button)
        {
            if (button != null)
            {
                DateEditCalendar.SetCellFocused(button, true);
            }
        }

        protected virtual void UpdateMonthInfoCell(DependencyObject obj, System.DateTime current)
        {
            if (this.Calendar != null)
            {
                if (current.Month != this.DateTime.Month)
                {
                    DateEditCalendar.SetCellInactive(obj, true);
                }
                if (!this.Calendar.CalendarTransfer.HasExecutingAnimations)
                {
                    if ((current.Month == System.DateTime.Now.Month) && ((current.Year == System.DateTime.Now.Year) && (current.Day == System.DateTime.Now.Day)))
                    {
                        DateEditCalendar.SetCellToday(obj, true);
                    }
                    if ((current.Month == this.DateTime.Month) && ((current.Year == this.DateTime.Year) && (current.Day == this.DateTime.Day)))
                    {
                        DateEditCalendar.SetCellFocused(obj, true);
                    }
                }
            }
        }

        protected virtual void UpdateYearInfoCell(DependencyObject obj, System.DateTime current)
        {
            if ((current.Month == this.DateTime.Month) && (current.Year == this.DateTime.Year))
            {
                DateEditCalendar.SetCellFocused(obj, true);
            }
        }

        protected virtual void UpdateYearsGroupInfoCell(DependencyObject obj, System.DateTime current)
        {
            if ((current.Year / 100) != (this.DateTime.Year / 100))
            {
                DateEditCalendar.SetCellInactive(obj, true);
            }
            if ((current.Year / 10) == (this.DateTime.Year / 10))
            {
                DateEditCalendar.SetCellFocused(obj, true);
            }
        }

        protected virtual void UpdateYearsInfoCell(DependencyObject obj, System.DateTime current)
        {
            if ((current.Year / 10) != (this.DateTime.Year / 10))
            {
                DateEditCalendar.SetCellInactive(obj, true);
            }
            if (current.Year == this.DateTime.Year)
            {
                DateEditCalendar.SetCellFocused(obj, true);
            }
        }

        public System.DateTime DateTime
        {
            get => 
                (System.DateTime) base.GetValue(DateTimeProperty);
            set => 
                base.SetValue(DateTimeProperty, value);
        }

        public DateEditCalendarState State
        {
            get => 
                (DateEditCalendarState) base.GetValue(StateProperty);
            set => 
                base.SetValue(StateProperty, value);
        }

        public DateEdit OwnerDateEdit =>
            (DateEdit) BaseEdit.GetOwnerEdit(this);

        public DateEditCalendar Calendar =>
            DateEditCalendar.GetCalendar(this);

        protected int FirstCellIndex { get; set; }

        protected System.DateTime FirstVisibleDate { get; set; }

        protected DateTimeFormatInfo DateTimeFormat =>
            CultureInfo.CurrentCulture.DateTimeFormat;

        protected DayOfWeek FirstDayOfWeek =>
            this.DateTimeFormat.FirstDayOfWeek;

        protected virtual int CellFirstRow =>
            (this.State != DateEditCalendarState.Month) ? 0 : 2;

        protected virtual int CellFirstCol =>
            (this.State != DateEditCalendarState.Month) ? 0 : 2;

        public Grid ContentGrid
        {
            get
            {
                this.contentGrid ??= (base.GetTemplateChild("PART_ContentGrid") as Grid);
                return this.contentGrid;
            }
        }

        protected delegate void CreateCellInfo(int row, int col);
    }
}

