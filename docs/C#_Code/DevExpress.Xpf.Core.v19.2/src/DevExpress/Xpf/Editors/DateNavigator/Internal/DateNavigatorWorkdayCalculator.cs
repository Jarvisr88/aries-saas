namespace DevExpress.Xpf.Editors.DateNavigator.Internal
{
    using DevExpress.Xpf.Editors.DateNavigator;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class DateNavigatorWorkdayCalculator : IDateCalculationService
    {
        private readonly DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator;
        private readonly Dictionary<DateTime, DateNavigatorCellState> dateStatesCache;

        public DateNavigatorWorkdayCalculator(DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator)
        {
            this.navigator = navigator;
            this.dateStatesCache = new Dictionary<DateTime, DateNavigatorCellState>();
        }

        private DateNavigatorCellState GetCellState(DateTime date)
        {
            DateNavigatorCellState none = DateNavigatorCellState.None;
            if ((this.ActualDisabledDates != null) && SelectedDatesHelper.Contains(this.ActualDisabledDates, date))
            {
                none |= DateNavigatorCellState.IsDisabled;
            }
            if ((this.ActualHolidays != null) && SelectedDatesHelper.Contains(this.ActualHolidays, date))
            {
                none |= DateNavigatorCellState.IsHoliday;
            }
            else if (((this.ActualWorkdays == null) || this.ActualWorkdays.All<DayOfWeek>(x => (x != date.DayOfWeek))) && ((date.DayOfWeek == DayOfWeek.Saturday) || (date.DayOfWeek == DayOfWeek.Sunday)))
            {
                none |= DateNavigatorCellState.IsHoliday;
            }
            if ((this.ActualHolidays == null) && ((this.ActualWorkdays != null) && this.ActualWorkdays.Any<DayOfWeek>(x => (x == date.DayOfWeek))))
            {
                none &= ~DateNavigatorCellState.IsHoliday;
            }
            else if ((this.ActualWorkdays != null) && this.ActualWorkdays.All<DayOfWeek>(x => (x != date.DayOfWeek)))
            {
                none |= DateNavigatorCellState.IsHoliday;
            }
            if ((this.ActualExactWorkdays != null) && SelectedDatesHelper.Contains(this.ActualExactWorkdays, date))
            {
                none |= DateNavigatorCellState.IsHoliday;
            }
            if ((this.ActualSpecialDates != null) && SelectedDatesHelper.Contains(this.ActualSpecialDates, date))
            {
                none |= DateNavigatorCellState.IsSpecialDate;
            }
            return none;
        }

        private bool HasFlag(DateTime date, DateNavigatorCellState flag)
        {
            if (!this.dateStatesCache.ContainsKey(date))
            {
                this.dateStatesCache[date] = this.navigator.RaiseRequestCellState(date, this.GetCellState(date));
            }
            return this.dateStatesCache[date].HasFlag(flag);
        }

        protected internal void InvalidateDateCache(DateTime date)
        {
            if (SelectedDatesHelper.Contains(this.dateStatesCache.Keys.ToList<DateTime>(), date))
            {
                this.dateStatesCache.Remove(new DateTime(date.Year, date.Month, date.Day));
            }
        }

        protected internal void InvalidateDatesCache()
        {
            this.dateStatesCache.Clear();
        }

        public bool IsDisabled(DateTime date)
        {
            this.UpdateActualWorkdaysCollections();
            return (((this.navigator.MinValue == null) || (this.navigator.MinValue.Value.CompareTo(date) <= 0)) ? (((this.navigator.MaxValue != null) && (this.navigator.MaxValue.Value.CompareTo(date) < 0)) || this.HasFlag(date, DateNavigatorCellState.IsDisabled)) : true);
        }

        public bool IsSpecialDate(DateTime date)
        {
            this.UpdateActualWorkdaysCollections();
            return this.HasFlag(date, DateNavigatorCellState.IsSpecialDate);
        }

        public bool IsWorkday(DateTime date)
        {
            this.UpdateActualWorkdaysCollections();
            return !this.HasFlag(date, DateNavigatorCellState.IsHoliday);
        }

        protected virtual void UpdateActualWorkdaysCollections()
        {
            if (this.navigator.AreWorkdaysCollectionsInvalid)
            {
                this.ActualExactWorkdays = this.navigator.OptionsProviderService.ExactWorkdays;
                this.ActualHolidays = this.navigator.OptionsProviderService.Holidays;
                this.ActualWorkdays = this.navigator.OptionsProviderService.Workdays;
                this.ActualDisabledDates = this.navigator.OptionsProviderService.DisabledDates;
                this.ActualSpecialDates = this.navigator.OptionsProviderService.SpecialDates;
                this.navigator.AreWorkdaysCollectionsInvalid = false;
            }
        }

        protected IList<DateTime> ActualExactWorkdays { get; private set; }

        protected IList<DateTime> ActualHolidays { get; private set; }

        protected IList<DayOfWeek> ActualWorkdays { get; private set; }

        protected IList<DateTime> ActualDisabledDates { get; private set; }

        protected IList<DateTime> ActualSpecialDates { get; private set; }
    }
}

