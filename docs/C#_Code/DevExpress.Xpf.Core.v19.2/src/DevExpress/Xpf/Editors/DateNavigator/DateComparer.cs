namespace DevExpress.Xpf.Editors.DateNavigator
{
    using System;

    public static class DateComparer
    {
        public static bool Equals(DateNavigatorCalendarView view, DateTime x, DateTime y)
        {
            switch (view)
            {
                case DateNavigatorCalendarView.Month:
                    return (x.Date == y.Date);

                case DateNavigatorCalendarView.Year:
                    return ((x.Year == y.Year) && (x.Month == y.Month));

                case DateNavigatorCalendarView.Years:
                    return (x.Year == y.Year);

                case DateNavigatorCalendarView.YearsRange:
                    return ((x.Year / 10) == (y.Year / 10));
            }
            throw new Exception();
        }
    }
}

