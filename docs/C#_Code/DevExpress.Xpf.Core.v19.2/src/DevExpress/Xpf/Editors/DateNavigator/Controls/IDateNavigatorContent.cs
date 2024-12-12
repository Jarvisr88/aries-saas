namespace DevExpress.Xpf.Editors.DateNavigator.Controls
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public interface IDateNavigatorContent
    {
        event DateNavigatorCalendarButtonClickEventHandler CalendarButtonClick;

        event DateNavigatorContentDateRangeChangedEventHandler DateRangeChanged;

        DateNavigatorCalendar GetCalendar(System.DateTime dt);
        DateNavigatorCalendar GetCalendar(int index);
        DateNavigatorCalendar GetCalendar(System.DateTime dt, bool excludeInactiveContent);
        void GetDateRange(bool excludeInactiveContent, out System.DateTime firstDate, out System.DateTime lastDate);
        System.DateTime GetWeekFirstDateByDate(System.DateTime dt);
        void HitTest(UIElement element, out System.DateTime? buttonDate, out DateNavigatorCalendarButtonKind buttonKind);
        void UpdateCalendarsCellStates();
        void UpdateCalendarsDisabledDates();
        void UpdateCalendarsHolidays();
        void UpdateCalendarsSelectedDates();
        void UpdateCalendarsSpecialDates();
        void UpdateCalendarsStyle();
        void UpdateCalendarToday();
        void VisibilityChanged();

        int CalendarCount { get; }

        System.DateTime DateTime { get; }

        System.DateTime EndDate { get; }

        System.DateTime FocusedDate { get; set; }

        System.DateTime StartDate { get; }
    }
}

