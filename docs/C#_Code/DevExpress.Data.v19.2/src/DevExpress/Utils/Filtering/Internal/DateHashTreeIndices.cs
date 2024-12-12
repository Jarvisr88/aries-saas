namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal sealed class DateHashTreeIndices : IntervalsHashTreeIndices<DateTime, DateHash>, IDateIntervalsHashTree, IIntervalsHashTree<DateTime, DateHash>, IHashTreeIndices, IHashTree<DateHash>, IHashTree
    {
        private int expandState;
        private DevExpress.Utils.Filtering.Internal.Interval<DateTime> range;
        internal static readonly object rangeChanged = new object();
        private const int YEAR = 0x40000000;
        private const int MONTH = 0x20000000;
        internal readonly string[] monthStrings;
        internal readonly string[] dayStrings;
        internal IDictionary<int, string> yearStrings;
        private bool isRangeSelector;
        internal static readonly DevExpress.Utils.Filtering.Internal.Interval<DateTime>[] AllDates = new DevExpress.Utils.Filtering.Internal.Interval<DateTime>[] { DateInterval.AllDates };

        event EventHandler IDateIntervalsHashTree.RangeChanged
        {
            add
            {
                base.events.Do<EventHandlerList>(e => e.AddHandler(rangeChanged, value));
            }
            remove
            {
                base.events.Do<EventHandlerList>(e => e.RemoveHandler(rangeChanged, value));
            }
        }

        public DateHashTreeIndices(object[] values, bool rootVisible, EventHandlerList events = null, bool isRangeSelector = false) : base(values, !isRangeSelector & rootVisible, events)
        {
            this.expandState = 0x40000000;
            this.range = DateInterval.AllDates;
            this.monthStrings = new string[13];
            this.dayStrings = new string[0x20];
            this.isRangeSelector = isRangeSelector;
        }

        protected sealed override void AddNodes(object[] values, bool rootVisible)
        {
            int key = 0;
            if (rootVisible)
            {
                base.hashKeys.Add(key, -2128831035);
                base.hashes.Add(key++, DateHash.All);
            }
            List<object> list = null;
            List<object> list2 = null;
            List<object> list3 = null;
            int? nullable = null;
            int? nullable2 = null;
            int origin = 3;
            for (int i = 0; i < values.Length; i++)
            {
                if (UniqueValues.IsNotLoaded(values[i]))
                {
                    base.hashKeys.Add(key, FNV1a.NotLoaded);
                    base.hashes.Add(key++, DateHash.NotLoaded);
                    base.AddNotLoaded(key);
                }
                else if (UniqueValues.IsNullOrDBNull(values[i]))
                {
                    base.hashKeys.Add(key, FNV1a.NullObject);
                    base.hashes.Add(key++, DateHash.Empty);
                    list = new List<object> {
                        BaseRowsKeeper.NullObject
                    };
                    base.AddNullObject(key);
                }
                else
                {
                    DateTime time = (DateTime) values[i];
                    int year = time.Year;
                    int num5 = FNV1a.Next(-2128831035, year);
                    if ((nullable == null) || !Equals(year, nullable.Value))
                    {
                        base.hashKeys.Add(key, num5);
                        base.hashes.Add(key++, new DateHash(year));
                        base.AddEntry(key, num5, -2128831035, 0x40000001);
                        list = new List<object> {
                            year
                        };
                        if ((nullable != null) && (list2 != null))
                        {
                            base.hashValues.Add(FNV1a.Next(-2128831035, nullable.Value), list2.ToArray());
                        }
                        if ((nullable2 != null) && (list3 != null))
                        {
                            base.hashValues.Add(FNV1a.Next(FNV1a.Next(-2128831035, nullable.Value), nullable2.Value), list3.ToArray());
                        }
                        nullable = new int?(year);
                        nullable2 = null;
                        list2 = new List<object>(12);
                        list3 = null;
                        origin |= 3;
                    }
                    int month = time.Month;
                    int num7 = FNV1a.Next(num5, month);
                    if ((nullable2 == null) || !Equals(month, nullable2.Value))
                    {
                        base.hashKeys.Add(key, num7);
                        base.hashes.Add(key++, new DateHash(year, month, 0, origin));
                        base.AddEntry(key, num7, num5, 0x20000000);
                        list2.Add(month);
                        if ((nullable2 != null) && (list3 != null))
                        {
                            base.hashValues.Add(FNV1a.Next(FNV1a.Next(-2128831035, nullable.Value), nullable2.Value), list3.ToArray());
                        }
                        nullable2 = new int?(month);
                        list3 = new List<object>(0x1f);
                        origin |= 1;
                    }
                    int day = time.Day;
                    DateHash dayHash = new DateHash(year, month, day, origin);
                    if (!this.SkipExistingNode(key - 1, dayHash))
                    {
                        base.hashes.Add(key++, dayHash);
                        list3.Add(day);
                        base.Offset(num7);
                        origin = 0;
                    }
                }
            }
            if (list != null)
            {
                base.hashValues.Add(-2128831035, list.ToArray());
            }
            if ((nullable != null) && (list2 != null))
            {
                base.hashValues.Add(FNV1a.Next(-2128831035, nullable.Value), list2.ToArray());
            }
            if ((nullable2 != null) && (list3 != null))
            {
                base.hashValues.Add(FNV1a.Next(FNV1a.Next(-2128831035, nullable.Value), nullable2.Value), list3.ToArray());
            }
        }

        protected sealed override void AfterUpdate()
        {
            this.InitializeYearStrings();
        }

        protected sealed override void BeforeUpdate()
        {
            Action<IDictionary<int, string>> @do = <>c.<>9__36_0;
            if (<>c.<>9__36_0 == null)
            {
                Action<IDictionary<int, string>> local1 = <>c.<>9__36_0;
                @do = <>c.<>9__36_0 = x => x.Clear();
            }
            this.yearStrings.Do<IDictionary<int, string>>(@do);
        }

        protected sealed override IntervalsHashTreeIndices<DateTime, DateHash>.IIntervalsChecks CreateIntervalsChecks() => 
            new DateIntervalsChecks(this);

        void IDateIntervalsHashTree.ExpandMonths()
        {
            Func<HashTreeIndices.Entry, bool> rangeFilter = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<HashTreeIndices.Entry, bool> local1 = <>c.<>9__6_0;
                rangeFilter = <>c.<>9__6_0 = entry => entry.CheckState(0x20000000);
            }
            if (this.ExpandRange(rangeFilter) && this.isRangeSelector)
            {
                this.expandState ^= 0x20000000;
                base.ResetVisibleIndicesMapping();
                base.RaiseVisualUpdateRequired();
            }
        }

        void IDateIntervalsHashTree.ExpandYears()
        {
            Func<HashTreeIndices.Entry, bool> rangeFilter = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<HashTreeIndices.Entry, bool> local1 = <>c.<>9__5_0;
                rangeFilter = <>c.<>9__5_0 = entry => entry.CheckState(0x40000000);
            }
            if (this.ExpandRange(rangeFilter) && this.isRangeSelector)
            {
                this.expandState ^= 0x40000000;
                base.ResetVisibleIndicesMapping();
                base.RaiseVisualUpdateRequired();
            }
        }

        DateTime IDateIntervalsHashTree.GetDate(int index) => 
            base.hashes[index].GetDate();

        DevExpress.Utils.Filtering.Internal.Interval<int> IDateIntervalsHashTree.GetRange()
        {
            int num;
            int num2;
            if (!DateInterval.AllDates.Equals(this.range) && base.TryGetCheckedRange(out num, out num2))
            {
                return new DevExpress.Utils.Filtering.Internal.Interval<int>(new int?(num), new int?(num2));
            }
            int? begin = null;
            begin = null;
            return new DevExpress.Utils.Filtering.Internal.Interval<int>(begin, begin);
        }

        IReadOnlyCollection<DevExpress.Utils.Filtering.Internal.Interval<DateTime>> IDateIntervalsHashTree.GetRangeIntervals()
        {
            if (DateInterval.AllDates.Equals(this.range))
            {
                return new DevExpress.Utils.Filtering.Internal.Interval<DateTime>[0];
            }
            return new DevExpress.Utils.Filtering.Internal.Interval<DateTime>[] { this.range };
        }

        void IDateIntervalsHashTree.SelectRange(int startIndex, int endIndex)
        {
            int num = (startIndex >= 0) ? base.GetIndex(startIndex) : 0;
            DevExpress.Utils.Filtering.Internal.Interval<DateTime> range = this.GetRange(num, (endIndex < base.VisibleCount) ? base.GetIndex(endIndex) : (base.hashes.Count - 1));
            if (!this.range.Equals(range))
            {
                this.range = range;
                this.RaiseRangeChanged();
            }
        }

        protected sealed override bool GetIsVisible(HashTreeIndices.Entry entry, int index)
        {
            if (this.isRangeSelector && (entry != null))
            {
                if ((entry.Key == -2128831035) || ((entry.Key == FNV1a.NotLoaded) || (entry.Key == FNV1a.NullObject)))
                {
                    return false;
                }
                if (entry.CheckState(0x20000000))
                {
                    return this.AreYearsExpanded;
                }
            }
            return base.GetIsVisible(entry, index);
        }

        private DevExpress.Utils.Filtering.Internal.Interval<DateTime> GetRange(int startIndex, int endIndex)
        {
            DevExpress.Utils.Filtering.Internal.Interval<DateTime> interval;
            DevExpress.Utils.Filtering.Internal.Interval<DateTime> interval2;
            return (((startIndex != endIndex) || !base.hashes[startIndex].TryGetInterval(out interval)) ? ((!base.hashes[startIndex].TryGetInterval(out interval) || !base.hashes[endIndex].TryGetInterval(out interval2)) ? DateInterval.AllDates : new DateRangeInterval(interval.Begin, interval2.Begin)) : new DateRangeInterval(interval.Begin, new DateTime?(interval.Begin.Value.AddDays(1.0))));
        }

        protected sealed override int GroupKey(DateHash hash) => 
            hash.GroupKey;

        protected override void Initialize()
        {
            this.InitializeYearStrings();
            this.InitializeMonthAndDayStings();
        }

        private void InitializeMonthAndDayStings()
        {
            for (int i = 1; i < this.monthStrings.Length; i++)
            {
                this.monthStrings[i] = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
            }
            for (int j = 1; j < this.dayStrings.Length; j++)
            {
                this.dayStrings[j] = j.ToString();
            }
        }

        private void InitializeYearStrings()
        {
            object[] objArray;
            if (base.hashValues.TryGetValue(-2128831035, out objArray))
            {
                IDictionary<int, string> yearStrings = this.yearStrings;
                if (this.yearStrings == null)
                {
                    IDictionary<int, string> local1 = this.yearStrings;
                    yearStrings = new Dictionary<int, string>(objArray.Length + 1);
                }
                this.yearStrings = yearStrings;
                for (int i = 0; i < objArray.Length; i++)
                {
                    object obj2 = objArray[i];
                    if (obj2 != BaseRowsKeeper.NullObject)
                    {
                        this.yearStrings[(int) obj2] = obj2.ToString();
                    }
                }
            }
        }

        protected sealed override bool Match(DateHash hash, DevExpress.Utils.Filtering.Internal.Interval<DateTime> range) => 
            hash.Match(range);

        protected sealed override bool Match(DateHash hash, DevExpress.Utils.Filtering.Internal.Interval<DateTime>[] intervals) => 
            hash.Match(intervals);

        protected sealed override int ParentKey(DateHash hash) => 
            hash.ParentKey;

        protected sealed override object[] Path(DateHash hash) => 
            hash.Path;

        private void RaiseRangeChanged()
        {
            Func<EventHandlerList, EventHandler> get = <>c.<>9__19_0;
            if (<>c.<>9__19_0 == null)
            {
                Func<EventHandlerList, EventHandler> local1 = <>c.<>9__19_0;
                get = <>c.<>9__19_0 = e => e[rangeChanged] as EventHandler;
            }
            EventHandler handler = base.events.Get<EventHandlerList, EventHandler>(get, null);
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected sealed override bool ResetCore() => 
            this.ResetRange() || base.ResetCore();

        private bool ResetRange()
        {
            bool flag = !DateInterval.AllDates.Equals(this.range);
            this.range = DateInterval.AllDates;
            if (flag)
            {
                this.RaiseRangeChanged();
            }
            return flag;
        }

        private bool SkipExistingNode(int index, DateHash dayHash)
        {
            DateHash hash;
            return (base.hashes.TryGetValue(index, out hash) ? (hash.IsDay && dayHash.Equals(hash)) : false);
        }

        protected sealed override string Text(DateHash hash) => 
            hash.GetText(this);

        protected sealed override string Text(DateHash hash, int level) => 
            hash.GetText(this, level);

        protected sealed override bool TryGetInterval(DateHash hash, out DevExpress.Utils.Filtering.Internal.Interval<DateTime> interval) => 
            hash.TryGetInterval(out interval);

        protected sealed override void Update(object[] values, bool isRangeSelector)
        {
            bool flag;
            this.isRangeSelector = flag = isRangeSelector;
            base.Update(values, flag);
        }

        protected sealed override int ValueKey(DateHash hash, out int key) => 
            hash.DayKey(out key);

        private bool AreYearsExpanded =>
            (this.expandState & 0x40000000) == 0x40000000;

        private bool AreMonthsExpanded =>
            (this.expandState & 0x20000000) == 0x20000000;

        protected sealed override DevExpress.Utils.Filtering.Internal.Interval<DateTime>[] AllIntervals =>
            AllDates;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DateHashTreeIndices.<>c <>9 = new DateHashTreeIndices.<>c();
            public static Func<HashTreeIndices.Entry, bool> <>9__5_0;
            public static Func<HashTreeIndices.Entry, bool> <>9__6_0;
            public static Func<EventHandlerList, EventHandler> <>9__19_0;
            public static Action<IDictionary<int, string>> <>9__36_0;

            internal void <BeforeUpdate>b__36_0(IDictionary<int, string> x)
            {
                x.Clear();
            }

            internal bool <DevExpress.Utils.Filtering.Internal.IDateIntervalsHashTree.ExpandMonths>b__6_0(HashTreeIndices.Entry entry) => 
                entry.CheckState(0x20000000);

            internal bool <DevExpress.Utils.Filtering.Internal.IDateIntervalsHashTree.ExpandYears>b__5_0(HashTreeIndices.Entry entry) => 
                entry.CheckState(0x40000000);

            internal EventHandler <RaiseRangeChanged>b__19_0(EventHandlerList e) => 
                e[DateHashTreeIndices.rangeChanged] as EventHandler;
        }

        private sealed class DateIntervalsChecks : IntervalsHashTreeIndices<DateTime, DateHash>.IntervalsChecks
        {
            public DateIntervalsChecks(DateHashTreeIndices indices) : base(indices)
            {
            }

            protected sealed override int GetDepth() => 
                2;
        }
    }
}

