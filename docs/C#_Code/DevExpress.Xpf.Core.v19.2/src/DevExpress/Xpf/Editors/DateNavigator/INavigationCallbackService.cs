namespace DevExpress.Xpf.Editors.DateNavigator
{
    using System;
    using System.Collections.Generic;

    public interface INavigationCallbackService
    {
        void ChangeView(DateNavigatorCalendarView state);
        void Move(DateTime dateTime);
        void Scroll(TimeSpan offset);
        void Select(IList<DateTime> selectedDates);
        void VisibleDateRangeChanged(bool isScrolling);
    }
}

