namespace DevExpress.Xpf.Editors.DateNavigator.Controls
{
    using System;

    public interface IDateNavigatorCalendarOwner
    {
        int GetCalendarIndex(DateNavigatorCalendar calendar);

        int CalendarCount { get; }
    }
}

