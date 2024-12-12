namespace DevExpress.Xpf.Editors.DateNavigator.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors.DateNavigator;
    using DevExpress.Xpf.Editors.DateNavigator.Controls;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class DateNavigatorHelper
    {
        private static DateTime CoerceDateTime(DateTime dateTime) => 
            (dateTime <= CultureInfo.CurrentCulture.Calendar.MaxSupportedDateTime) ? ((dateTime >= CultureInfo.CurrentCulture.Calendar.MinSupportedDateTime) ? dateTime : CultureInfo.CurrentCulture.Calendar.MinSupportedDateTime) : CultureInfo.CurrentCulture.Calendar.MaxSupportedDateTime;

        public static DateNavigatorCalendarCellButton GetCalendarCellButton(DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator, DateTime dt)
        {
            if (navigator.ActiveContent == null)
            {
                return null;
            }
            return ((IDateNavigatorContent) navigator.ActiveContent).GetCalendar(dt, false)?.GetCellButton(dt);
        }

        public static string GetDateText(DateNavigatorCalendarView state, DateTime dt)
        {
            if (state == DateNavigatorCalendarView.Month)
            {
                return CoerceDateTime(dt).ToString(CultureInfo.CurrentCulture.DateTimeFormat.YearMonthPattern);
            }
            if (state == DateNavigatorCalendarView.Year)
            {
                return CoerceDateTime(dt).ToString("yyyy", CultureInfo.CurrentCulture);
            }
            if (state == DateNavigatorCalendarView.Years)
            {
                object[] objArray1 = new object[] { CoerceDateTime(new DateTime(Math.Max((dt.Year / 10) * 10, 1), 1, 1)), CoerceDateTime(new DateTime(((dt.Year / 10) * 10) + 9, 1, 1)) };
                return string.Format(CultureInfo.CurrentCulture, "{0:yyyy}-{1:yyyy}", objArray1);
            }
            object[] args = new object[] { CoerceDateTime(new DateTime(Math.Max((dt.Year / 100) * 100, 1), 1, 1)), CoerceDateTime(new DateTime(((dt.Year / 100) * 100) + 0x63, 1, 1)) };
            return string.Format(CultureInfo.CurrentCulture, "{0:yyyy}-{1:yyyy}", args);
        }

        public static string GetDateText(DateNavigatorCalendarView state, DateTime startDate, DateTime endDate)
        {
            if (SelectedDatesHelper.AreEquals(startDate, endDate, state))
            {
                if (state == DateNavigatorCalendarView.Month)
                {
                    return startDate.ToString(CultureInfo.CurrentCulture.DateTimeFormat.YearMonthPattern);
                }
                object[] objArray1 = new object[] { startDate };
                return string.Format(CultureInfo.CurrentCulture, "{0:yyyy}", objArray1);
            }
            if (state == DateNavigatorCalendarView.Month)
            {
                return (startDate.ToString(CultureInfo.CurrentCulture.DateTimeFormat.YearMonthPattern) + " - " + endDate.ToString(CultureInfo.CurrentCulture.DateTimeFormat.YearMonthPattern));
            }
            object[] args = new object[] { startDate, endDate };
            return string.Format(CultureInfo.CurrentCulture, "{0:yyyy} - {1:yyyy}", args);
        }

        public static List<DateTime> GetDisabledDateList(DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator) => 
            navigator.DisabledDates.ToList<DateTime>();

        public static IList<DateTime> GetSelectedDateList(DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator) => 
            navigator.ValueEditingService.SelectedDates;

        public static List<DateTime> GetSpecialDateList(DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator) => 
            navigator.SpecialDateList;

        private static bool HasEnabledDatesInMonth(DateTime date, IDateCalculationService calculationService)
        {
            if (calculationService == null)
            {
                return true;
            }
            int num = DateTime.DaysInMonth(date.Year, date.Month);
            for (int i = 0; i < num; i++)
            {
                if (!calculationService.IsDisabled(new DateTime(date.Year, date.Month, i + 1)))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool HasEnabledDatesInView(DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator, DateTime date)
        {
            if (navigator == null)
            {
                return true;
            }
            switch (navigator.CalendarView)
            {
                case DateNavigatorCalendarView.Month:
                {
                    Func<bool> fallback = <>c.<>9__10_1;
                    if (<>c.<>9__10_1 == null)
                    {
                        Func<bool> local1 = <>c.<>9__10_1;
                        fallback = <>c.<>9__10_1 = () => false;
                    }
                    return !navigator.DateCalculationService.Return<IDateCalculationService, bool>(x => x.IsDisabled(date), fallback);
                }
                case DateNavigatorCalendarView.Year:
                    return HasEnabledDatesInMonth(date, navigator.DateCalculationService);

                case DateNavigatorCalendarView.Years:
                    return HasEnabledDatesInYear(date, navigator.DateCalculationService);

                case DateNavigatorCalendarView.YearsRange:
                    return HasEnabledDatesInYearsRange(date, navigator.DateCalculationService);
            }
            throw new ArgumentOutOfRangeException();
        }

        private static bool HasEnabledDatesInYear(DateTime date, IDateCalculationService calculationService)
        {
            if (calculationService == null)
            {
                return true;
            }
            for (int i = 0; i < 12; i++)
            {
                if (HasEnabledDatesInMonth(new DateTime(date.Year, i + 1, 1), calculationService))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool HasEnabledDatesInYearsRange(DateTime date, IDateCalculationService calculationService)
        {
            if (calculationService == null)
            {
                return true;
            }
            for (int i = 0; i < 10; i++)
            {
                if (HasEnabledDatesInYear(new DateTime(date.Year + i, date.Month, date.Day), calculationService))
                {
                    return true;
                }
            }
            return false;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DateNavigatorHelper.<>c <>9 = new DateNavigatorHelper.<>c();
            public static Func<bool> <>9__10_1;

            internal bool <HasEnabledDatesInView>b__10_1() => 
                false;
        }
    }
}

