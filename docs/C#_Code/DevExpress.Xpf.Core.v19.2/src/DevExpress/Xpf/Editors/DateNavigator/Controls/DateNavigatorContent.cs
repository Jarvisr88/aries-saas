namespace DevExpress.Xpf.Editors.DateNavigator.Controls
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.DateNavigator;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;

    public class DateNavigatorContent : Control, IDateNavigatorContentPanelOwner, IDateNavigatorContent, IDateNavigatorCalendarOwner
    {
        private System.DateTime dateTime;
        public static readonly DependencyProperty ColumnCountProperty;
        public static readonly DependencyProperty FocusedDateProperty;
        public static readonly DependencyProperty PanelOwnerProperty;
        public static readonly DependencyProperty StateProperty;
        public static readonly DependencyProperty RowCountProperty;
        private readonly Locker supportInitializeLocker = new Locker();

        private event DateNavigatorCalendarButtonClickEventHandler calendarButtonClick;

        private event DateNavigatorContentDateRangeChangedEventHandler dateRangeChanged;

        event DateNavigatorCalendarButtonClickEventHandler IDateNavigatorContent.CalendarButtonClick
        {
            add
            {
                this.calendarButtonClick += value;
            }
            remove
            {
                this.calendarButtonClick -= value;
            }
        }

        event DateNavigatorContentDateRangeChangedEventHandler IDateNavigatorContent.DateRangeChanged
        {
            add
            {
                this.dateRangeChanged += value;
            }
            remove
            {
                this.dateRangeChanged -= value;
            }
        }

        static DateNavigatorContent()
        {
            Type ownerType = typeof(DateNavigatorContent);
            ColumnCountProperty = DependencyPropertyManager.Register("ColumnCount", typeof(int), ownerType, new PropertyMetadata(0), new ValidateValueCallback(DateNavigatorContent.ValidatePropertyValueColumnCount));
            FocusedDateProperty = DependencyPropertyManager.Register("FocusedDate", typeof(System.DateTime), ownerType, new PropertyMetadata((d, e) => ((DateNavigatorContent) d).PropertyChangedFocusedDate((System.DateTime) e.OldValue)));
            PanelOwnerProperty = DependencyPropertyManager.RegisterAttached("PanelOwner", typeof(IDateNavigatorContentPanelOwner), ownerType, new PropertyMetadata(null));
            RowCountProperty = DependencyPropertyManager.Register("RowCount", typeof(int), ownerType, new PropertyMetadata(0), new ValidateValueCallback(DateNavigatorContent.ValidatePropertyValueRowCount));
            StateProperty = DependencyPropertyManager.Register("State", typeof(DateNavigatorCalendarView), ownerType, new PropertyMetadata((d, e) => ((DateNavigatorContent) d).PropertyChangedState()));
        }

        public DateNavigatorContent()
        {
            this.SetDefaultStyleKey(typeof(DateNavigatorContent));
        }

        public override void BeginInit()
        {
            if (!this.IsInSupportInitializing)
            {
                base.BeginInit();
            }
            this.supportInitializeLocker.Lock();
        }

        private bool CheckCalendarDateRange(int calendarIndex, System.DateTime dt)
        {
            System.DateTime time;
            System.DateTime time2;
            this.GetItem(calendarIndex).GetDateRange(false, out time, out time2);
            return ((time <= dt) && (dt <= time2));
        }

        protected void ClearCalendarCellButtonFocusedState(System.DateTime dt)
        {
            DateNavigatorCalendarCellButton calendarCellButton = this.GetCalendarCellButton(dt);
            if (calendarCellButton != null)
            {
                DateNavigatorCalendar.SetCellFocused(calendarCellButton, false);
            }
        }

        int IDateNavigatorCalendarOwner.GetCalendarIndex(DateNavigatorCalendar calendar) => 
            this.Panel.Children.IndexOf(calendar);

        DateNavigatorCalendar IDateNavigatorContent.GetCalendar(System.DateTime dt) => 
            this.GetCalendar(dt, true);

        DateNavigatorCalendar IDateNavigatorContent.GetCalendar(int index) => 
            this.GetItem(index);

        DateNavigatorCalendar IDateNavigatorContent.GetCalendar(System.DateTime dt, bool excludeInactiveContent) => 
            this.GetCalendar(dt, excludeInactiveContent);

        void IDateNavigatorContent.GetDateRange(bool excludeInactiveContent, out System.DateTime firstDate, out System.DateTime lastDate)
        {
            this.GetDateRange(excludeInactiveContent, out firstDate, out lastDate);
        }

        System.DateTime IDateNavigatorContent.GetWeekFirstDateByDate(System.DateTime dt) => 
            this.GetCalendar(dt, false).GetWeekFirstDateByDate(dt);

        void IDateNavigatorContent.HitTest(UIElement element, out System.DateTime? buttonDate, out DateNavigatorCalendarButtonKind buttonKind)
        {
            buttonDate = 0;
            buttonKind = DateNavigatorCalendarButtonKind.None;
            if (element != null)
            {
                DateNavigatorCalendar calendar = LayoutHelper.FindParentObject<DateNavigatorCalendar>(element);
                if ((calendar != null) && LayoutHelper.IsChildElement(this, calendar))
                {
                    calendar.HitTest(element, out buttonDate, out buttonKind);
                }
            }
        }

        void IDateNavigatorContent.UpdateCalendarsCellStates()
        {
            for (int i = 0; i < this.ItemCount; i++)
            {
                this.UpdateCalendarCellStates(this.GetItem(i));
            }
        }

        void IDateNavigatorContent.UpdateCalendarsDisabledDates()
        {
            for (int i = 0; i < this.ItemCount; i++)
            {
                this.UpdateCalendarDisabledDates(this.GetItem(i));
            }
        }

        void IDateNavigatorContent.UpdateCalendarsHolidays()
        {
            for (int i = 0; i < this.ItemCount; i++)
            {
                this.GetItem(i).UpdateHolidays();
            }
        }

        void IDateNavigatorContent.UpdateCalendarsSelectedDates()
        {
            if (this.State == DateNavigatorCalendarView.Month)
            {
                for (int i = 0; i < this.ItemCount; i++)
                {
                    this.UpdateCalendarSelectedDates(this.GetItem(i));
                }
            }
        }

        void IDateNavigatorContent.UpdateCalendarsSpecialDates()
        {
            if (this.State != DateNavigatorCalendarView.Month)
            {
                throw new Exception();
            }
            for (int i = 0; i < this.ItemCount; i++)
            {
                this.UpdateCalendarSpecialDates(this.GetItem(i));
            }
        }

        void IDateNavigatorContent.UpdateCalendarsStyle()
        {
            DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator = this.Navigator;
            if (navigator != null)
            {
                for (int i = 0; i < this.ItemCount; i++)
                {
                    this.GetItem(i).Style = navigator.CalendarStyle;
                }
            }
        }

        void IDateNavigatorContent.UpdateCalendarToday()
        {
            if (this.State == DateNavigatorCalendarView.Month)
            {
                for (int i = 0; i < this.ItemCount; i++)
                {
                    this.GetItem(i).UpdateToday();
                }
            }
        }

        void IDateNavigatorContent.VisibilityChanged()
        {
            if (this.Panel != null)
            {
                this.Panel.InvalidateMeasure();
                this.Panel.UpdateLayout();
            }
        }

        UIElement IDateNavigatorContentPanelOwner.CreateItem()
        {
            DateNavigatorCalendar calendar = new DateNavigatorCalendar(this);
            calendar.ButtonClick += new DateNavigatorCalendarButtonClickEventHandler(this.OnCalendarButtonClick);
            calendar.DateTime = this.GetItemDateTime(this.BaseDateTime, this.ItemCount);
            calendar.State = this.State;
            DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator = this.Navigator;
            if ((navigator != null) && (navigator.CalendarStyle != null))
            {
                calendar.Style = navigator.CalendarStyle;
            }
            DevExpress.Xpf.Editors.DateNavigator.DateNavigator.SetNavigator(calendar, navigator);
            if ((this.ItemCount == 0) && (this.State == DateNavigatorCalendarView.Month))
            {
                calendar.ResizeToMaxContent = true;
            }
            return calendar;
        }

        Size IDateNavigatorContentPanelOwner.GetItemSize()
        {
            DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator = this.Navigator;
            if (navigator != null)
            {
                DateNavigatorContent content = ((IDateNavigatorContentContainer) navigator).GetContent(DateNavigatorCalendarView.Month);
                if (content != null)
                {
                    return content.GetItem(0).DesiredSize;
                }
            }
            return this.GetItem(0).DesiredSize;
        }

        void IDateNavigatorContentPanelOwner.ItemCountChanged()
        {
            this.RaiseDateRangeChanged(false);
        }

        void IDateNavigatorContentPanelOwner.UninitializeItem(UIElement item)
        {
            ((DateNavigatorCalendar) item).ButtonClick -= new DateNavigatorCalendarButtonClickEventHandler(this.OnCalendarButtonClick);
        }

        void IDateNavigatorContentPanelOwner.UpdateItemPositions(int columnCount, int rowCount)
        {
            if (this.ItemCount == 1)
            {
                this.GetItem(0).Position = DateNavigatorCalendarPosition.Single;
                this.GetItem(0).HeaderType = DateNavigatorCalendarHeaderType.Both;
            }
            else
            {
                this.GetItem(0).HeaderType = (columnCount == 1) ? DateNavigatorCalendarHeaderType.Both : DateNavigatorCalendarHeaderType.First;
                this.GetItem(0).Position = DateNavigatorCalendarPosition.First;
                this.GetItem(this.ItemCount - 1).HeaderType = ((this.ItemCount != columnCount) || (rowCount != 1)) ? DateNavigatorCalendarHeaderType.None : DateNavigatorCalendarHeaderType.Last;
                this.GetItem(this.ItemCount - 1).Position = DateNavigatorCalendarPosition.Last;
                for (int i = 1; i < (this.ItemCount - 1); i++)
                {
                    this.GetItem(i).HeaderType = (i == (columnCount - 1)) ? DateNavigatorCalendarHeaderType.Last : DateNavigatorCalendarHeaderType.None;
                    this.GetItem(i).Position = DateNavigatorCalendarPosition.Intermediate;
                }
            }
        }

        public override void EndInit()
        {
            this.supportInitializeLocker.Unlock();
            if (!this.IsInSupportInitializing)
            {
                base.EndInit();
            }
        }

        protected System.DateTime GetBaseDateTime(DateNavigatorCalendarView state, System.DateTime dt)
        {
            switch (state)
            {
                case DateNavigatorCalendarView.Month:
                    return new System.DateTime(dt.Year, dt.Month, 1);

                case DateNavigatorCalendarView.Year:
                    return new System.DateTime(dt.Year, 1, 1);

                case DateNavigatorCalendarView.Years:
                    return new System.DateTime(Math.Max((dt.Year / 10) * 10, 1), 1, 1);

                case DateNavigatorCalendarView.YearsRange:
                    return new System.DateTime(Math.Max((dt.Year / 100) * 100, 1), 1, 1);
            }
            throw new Exception();
        }

        protected virtual DateNavigatorCalendar GetCalendar(System.DateTime dt, bool excludeInactiveContent)
        {
            if (this.ItemCount == 0)
            {
                return null;
            }
            int itemIndex = -1;
            System.DateTime dateTime = this.GetItem(0).DateTime;
            switch (this.State)
            {
                case DateNavigatorCalendarView.Month:
                    itemIndex = (((dt.Year - dateTime.Year) * 12) + dt.Month) - dateTime.Month;
                    break;

                case DateNavigatorCalendarView.Year:
                    itemIndex = dt.Year - dateTime.Year;
                    break;

                case DateNavigatorCalendarView.Years:
                    itemIndex = (dt.Year / 10) - (dateTime.Year / 10);
                    break;

                case DateNavigatorCalendarView.YearsRange:
                    itemIndex = (dt.Year / 100) - (dateTime.Year / 100);
                    break;

                default:
                    break;
            }
            DateNavigatorCalendar item = ((itemIndex < 0) || (itemIndex >= this.ItemCount)) ? null : this.GetItem(itemIndex);
            if ((item == null) && !excludeInactiveContent)
            {
                if (this.CheckCalendarDateRange(0, dt))
                {
                    item = this.GetItem(0);
                }
                else if ((this.ItemCount > 1) && this.CheckCalendarDateRange(this.ItemCount - 1, dt))
                {
                    item = this.GetItem(this.ItemCount - 1);
                }
            }
            return item;
        }

        protected DateNavigatorCalendarCellButton GetCalendarCellButton(System.DateTime dt) => 
            this.GetCalendar(dt, false)?.GetCellButton(dt);

        private void GetDateRange(bool excludeInactiveContent, out System.DateTime firstDate, out System.DateTime lastDate)
        {
            if (this.ItemCount == 0)
            {
                firstDate = this.BaseDateTime;
                lastDate = this.BaseDateTime;
            }
            else
            {
                System.DateTime time;
                this.GetItem(0).GetDateRange(excludeInactiveContent, out firstDate, out time);
                this.GetItem(this.ItemCount - 1).GetDateRange(excludeInactiveContent, out time, out lastDate);
            }
        }

        protected System.DateTime GetDateTimeForRightBringToView()
        {
            switch (this.State)
            {
                case DateNavigatorCalendarView.Month:
                    return this.BaseDateTime.AddMonths(-(this.ItemCount - 1));

                case DateNavigatorCalendarView.Year:
                    return this.BaseDateTime.AddYears(-(this.ItemCount - 1));

                case DateNavigatorCalendarView.Years:
                    return this.BaseDateTime.AddYears(-(this.ItemCount - 1) * 10);

                case DateNavigatorCalendarView.YearsRange:
                    return this.BaseDateTime.AddYears(-(this.ItemCount - 1) * 100);
            }
            throw new Exception();
        }

        protected internal DateNavigatorCalendar GetItem(int itemIndex) => 
            (DateNavigatorCalendar) this.Panel.Children[itemIndex];

        private System.DateTime GetItemDateTime(System.DateTime baseDateTime, int i)
        {
            System.DateTime time;
            if ((this.ItemCount > 0) && (i > 0))
            {
                baseDateTime = this.GetItem(0).DateTime;
            }
            try
            {
                switch (this.State)
                {
                    case DateNavigatorCalendarView.Month:
                        time = baseDateTime.AddMonths(i);
                        break;

                    case DateNavigatorCalendarView.Year:
                        time = baseDateTime.AddYears(i);
                        break;

                    case DateNavigatorCalendarView.Years:
                        time = new System.DateTime(Math.Max(((baseDateTime.Year / 10) * 10) + (i * 10), 1), 1, 1);
                        break;

                    case DateNavigatorCalendarView.YearsRange:
                        time = new System.DateTime(Math.Max(((baseDateTime.Year / 100) * 100) + (i * 100), 1), 1, 1);
                        break;

                    default:
                        time = baseDateTime;
                        break;
                }
            }
            catch
            {
                if (this.State != DateNavigatorCalendarView.Month)
                {
                    time = baseDateTime;
                }
                else
                {
                    int num = i / 12;
                    int month = (((baseDateTime.Month - 1) + i) % 12) + 1;
                    time = new System.DateTime(System.DateTime.MinValue.Year + num, month, baseDateTime.Day);
                }
            }
            return time;
        }

        public static IDateNavigatorContentPanelOwner GetPanelOwner(DependencyObject obj) => 
            (IDateNavigatorContentPanelOwner) obj.GetValue(PanelOwnerProperty);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Panel = base.GetTemplateChild("PART_Panel") as System.Windows.Controls.Panel;
            if (this.Panel != null)
            {
                SetPanelOwner(this.Panel, this);
            }
        }

        protected virtual void OnCalendarButtonClick(object sender, DateNavigatorCalendarButtonClickEventArgs e)
        {
            this.RaiseCalendarButtonClick(e);
        }

        protected virtual void PropertyChangedFocusedDate(System.DateTime oldValue)
        {
            this.ClearCalendarCellButtonFocusedState(oldValue);
            this.SetCalendarCellButtonFocusedState(this.FocusedDate);
        }

        protected virtual void PropertyChangedState()
        {
            for (int i = 0; i < this.ItemCount; i++)
            {
                this.GetItem(i).State = this.State;
            }
            this.UpdateDateTime(false);
        }

        protected virtual void RaiseCalendarButtonClick(DateNavigatorCalendarButtonClickEventArgs e)
        {
            if (this.calendarButtonClick != null)
            {
                this.calendarButtonClick(this, e);
            }
        }

        protected void RaiseDateRangeChanged(bool isScrolling)
        {
            if (this.dateRangeChanged != null)
            {
                this.dateRangeChanged(this, new DateNavigatorContentDateRangeChangedEventArgs(isScrolling));
            }
        }

        protected void SetCalendarCellButtonFocusedState(System.DateTime dt)
        {
            DateNavigatorCalendarCellButton calendarCellButton = this.GetCalendarCellButton(dt);
            if (calendarCellButton != null)
            {
                DateNavigatorCalendar.SetCellFocused(calendarCellButton, true);
            }
        }

        public void SetDateTime(System.DateTime value, bool scrollIfValueInactive)
        {
            if ((value != this.dateTime) || (!this.IsInSupportInitializing && (this.GetCalendar(value, scrollIfValueInactive) == null)))
            {
                this.dateTime = value;
                this.UpdateDateTime(scrollIfValueInactive);
            }
        }

        public static void SetPanelOwner(DependencyObject obj, IDateNavigatorContentPanelOwner value)
        {
            obj.SetValue(PanelOwnerProperty, value);
        }

        protected void UpdateCalendarCellStates(DateNavigatorCalendar calendar)
        {
            calendar.UpdateCellStates();
        }

        protected void UpdateCalendarDisabledDates(DateNavigatorCalendar calendar)
        {
            calendar.UpdateDisabledDates();
        }

        protected void UpdateCalendarSelectedDates(DateNavigatorCalendar calendar)
        {
            calendar.UpdateSelectedDates();
        }

        protected void UpdateCalendarSpecialDates(DateNavigatorCalendar calendar)
        {
            calendar.UpdateSpecialDates();
        }

        protected void UpdateDateTime(bool scrollIfValueInactive)
        {
            if (this.ItemCount != 0)
            {
                System.DateTime baseDateTime;
                if (this.IsInSupportInitializing)
                {
                    baseDateTime = this.BaseDateTime;
                }
                else
                {
                    if (this.GetCalendar(this.DateTime, scrollIfValueInactive) != null)
                    {
                        return;
                    }
                    baseDateTime = (this.DateTime >= this.GetItem(0).DateTime) ? this.GetDateTimeForRightBringToView() : this.BaseDateTime;
                }
                for (int i = 0; i < this.ItemCount; i++)
                {
                    DateNavigatorCalendar item = this.GetItem(i);
                    System.DateTime dateTime = item.DateTime;
                    item.DateTime = this.GetItemDateTime(baseDateTime, i);
                    if ((i == (this.ItemCount - 1)) && (dateTime != item.DateTime))
                    {
                        this.RaiseDateRangeChanged(true);
                    }
                }
            }
        }

        private static bool ValidatePropertyValueColumnCount(object value) => 
            ((int) value) >= 0;

        private static bool ValidatePropertyValueRowCount(object value) => 
            ((int) value) >= 0;

        public int ColumnCount
        {
            get => 
                (int) base.GetValue(ColumnCountProperty);
            set => 
                base.SetValue(ColumnCountProperty, value);
        }

        public System.DateTime DateTime =>
            this.dateTime;

        public System.DateTime FocusedDate
        {
            get => 
                (System.DateTime) base.GetValue(FocusedDateProperty);
            set => 
                base.SetValue(FocusedDateProperty, value);
        }

        public DevExpress.Xpf.Editors.DateNavigator.DateNavigator Navigator =>
            DevExpress.Xpf.Editors.DateNavigator.DateNavigator.GetNavigator(this);

        public int RowCount
        {
            get => 
                (int) base.GetValue(RowCountProperty);
            set => 
                base.SetValue(RowCountProperty, value);
        }

        public DateNavigatorCalendarView State
        {
            get => 
                (DateNavigatorCalendarView) base.GetValue(StateProperty);
            set => 
                base.SetValue(StateProperty, value);
        }

        protected System.DateTime BaseDateTime =>
            this.GetBaseDateTime(this.State, this.DateTime);

        protected internal int ItemCount =>
            (this.Panel != null) ? this.Panel.Children.Count : 0;

        protected bool IsInSupportInitializing =>
            this.supportInitializeLocker.IsLocked;

        protected System.Windows.Controls.Panel Panel { get; private set; }

        bool IDateNavigatorContentPanelOwner.IsHidden
        {
            get
            {
                DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator = this.Navigator;
                return ((navigator != null) && (this.State != navigator.CalendarView));
            }
        }

        int IDateNavigatorContent.CalendarCount =>
            this.ItemCount;

        System.DateTime IDateNavigatorContent.EndDate
        {
            get
            {
                System.DateTime time;
                System.DateTime time2;
                this.GetDateRange(true, out time, out time2);
                return time2;
            }
        }

        System.DateTime IDateNavigatorContent.StartDate
        {
            get
            {
                System.DateTime time;
                System.DateTime time2;
                this.GetDateRange(true, out time, out time2);
                return time;
            }
        }

        int IDateNavigatorCalendarOwner.CalendarCount =>
            this.ItemCount;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DateNavigatorContent.<>c <>9 = new DateNavigatorContent.<>c();

            internal void <.cctor>b__6_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateNavigatorContent) d).PropertyChangedFocusedDate((DateTime) e.OldValue);
            }

            internal void <.cctor>b__6_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateNavigatorContent) d).PropertyChangedState();
            }
        }
    }
}

