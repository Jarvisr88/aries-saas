namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public interface IDateIntervalsHashTree : IIntervalsHashTree<DateTime, DateHash>, IHashTreeIndices, IHashTree<DateHash>, IHashTree
    {
        event EventHandler RangeChanged;

        void ExpandMonths();
        void ExpandYears();
        DateTime GetDate(int index);
        Interval<int> GetRange();
        IReadOnlyCollection<Interval<DateTime>> GetRangeIntervals();
        void SelectRange(int startIndex, int endIndex);
    }
}

