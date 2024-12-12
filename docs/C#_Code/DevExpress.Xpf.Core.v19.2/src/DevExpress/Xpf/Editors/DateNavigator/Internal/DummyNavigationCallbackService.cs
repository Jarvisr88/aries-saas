namespace DevExpress.Xpf.Editors.DateNavigator.Internal
{
    using DevExpress.Xpf.Editors.DateNavigator;
    using System;
    using System.Collections.Generic;

    public class DummyNavigationCallbackService : INavigationCallbackService
    {
        public void ChangeView(DateNavigatorCalendarView state)
        {
        }

        public void Move(DateTime dateTime)
        {
        }

        public void Scroll(TimeSpan offset)
        {
        }

        public void Select(IList<DateTime> selectedDates)
        {
        }

        public void VisibleDateRangeChanged(bool isScrolling)
        {
        }
    }
}

