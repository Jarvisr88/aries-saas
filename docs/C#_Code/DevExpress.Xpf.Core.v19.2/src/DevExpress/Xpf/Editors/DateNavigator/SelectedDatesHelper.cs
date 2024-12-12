namespace DevExpress.Xpf.Editors.DateNavigator
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public static class SelectedDatesHelper
    {
        private static readonly SkipTimeEqualityComparer ComparerInstance = new SkipTimeEqualityComparer();

        public static bool AreEquals(IList<DateTime> list1, IList<DateTime> list2) => 
            ((list1 == null) || (list2 == null)) ? ReferenceEquals(list1, list2) : list1.SequenceEqual<DateTime>(list2, ComparerInstance);

        public static bool AreEquals(DateTime dt1, DateTime dt2) => 
            ComparerInstance.Equals(dt1, dt2);

        public static bool AreEquals(DateTime dt1, DateTime dt2, DateNavigatorCalendarView state) => 
            ComparerInstance.Equals(dt1, dt2, state);

        public static bool Contains(IList<DateTime> selectedDates, DateTime date) => 
            (selectedDates != null) && selectedDates.Contains<DateTime>(date, ComparerInstance);

        public static IList<DateTime> GetSelection(DateTime dt1, DateTime dt2)
        {
            ObservableCollection<DateTime> observables = new ObservableCollection<DateTime>();
            DateTime time = (dt1.CompareTo(dt2) >= 0) ? dt2 : dt1;
            DateTime time2 = (dt1.CompareTo(dt2) < 0) ? dt2 : dt1;
            int num = 0;
            while (true)
            {
                TimeSpan span = (TimeSpan) (time2 - time);
                if (num >= (span.TotalDays + 1.0))
                {
                    return observables;
                }
                observables.Add(time.AddDays((double) num));
                num++;
            }
        }

        public static IList<DateTime> GetSelectionWithoutDisabled(DateTime end, DateTime start, Func<DateTime, bool> isDisabled, bool allowMultipleRange)
        {
            if (allowMultipleRange)
            {
                return new ObservableCollection<DateTime>(from x in GetSelection(start, end)
                    where !isDisabled(x)
                    select x);
            }
            ObservableCollection<DateTime> observables = new ObservableCollection<DateTime>();
            int num = (start.CompareTo(end) >= 0) ? -1 : 1;
            int num2 = (int) Math.Abs((end - start).TotalDays);
            bool flag = false;
            for (int i = 0; i < (num2 + 1); i++)
            {
                DateTime arg = start.AddDays((double) (i * num));
                if (isDisabled(arg))
                {
                    flag = true;
                }
                else
                {
                    if (flag)
                    {
                        observables.Clear();
                        flag = false;
                    }
                    observables.Add(arg);
                }
            }
            return observables;
        }

        public static ObservableCollection<DateTime> Merge(IEnumerable<DateTime> selectedDates, IEnumerable<DateTime> newDates)
        {
            List<DateTime> first = (selectedDates != null) ? new List<DateTime>(selectedDates) : new List<DateTime>();
            List<DateTime> list = new List<DateTime>(first.Union<DateTime>(newDates, ComparerInstance));
            list.Sort();
            return new ObservableCollection<DateTime>(list);
        }

        public static ObservableCollection<DateTime> Merge(IEnumerable<DateTime> selectedDates, DateTime newDate)
        {
            List<DateTime> newDates = new List<DateTime>();
            newDates.Add(newDate);
            return Merge(selectedDates, newDates);
        }

        public static IList<DateTime> Remove(IList<DateTime> selectedDates, DateTime date)
        {
            if (!Contains(selectedDates, date))
            {
                return selectedDates;
            }
            ObservableCollection<DateTime> second = new ObservableCollection<DateTime>();
            second.Add(date);
            return new ObservableCollection<DateTime>(selectedDates.Except<DateTime>(second, ComparerInstance));
        }

        private class SkipTimeEqualityComparer : IEqualityComparer<DateTime>
        {
            public bool Equals(DateTime x, DateTime y) => 
                (x.Year == y.Year) && ((x.Month == y.Month) && (x.Day == y.Day));

            public bool Equals(DateTime x, DateTime y, DateNavigatorCalendarView state) => 
                (state != DateNavigatorCalendarView.Year) ? ((state != DateNavigatorCalendarView.Month) ? this.Equals(x, y) : ((x.Year == y.Year) && (x.Month == y.Month))) : (x.Year == y.Year);

            public int GetHashCode(DateTime obj) => 
                base.GetHashCode();
        }
    }
}

