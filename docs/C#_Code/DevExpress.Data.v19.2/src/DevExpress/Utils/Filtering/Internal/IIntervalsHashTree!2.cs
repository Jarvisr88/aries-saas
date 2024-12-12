namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public interface IIntervalsHashTree<T, THash> : IHashTreeIndices, IHashTree<THash>, IHashTree where T: struct where THash: struct
    {
        event EventHandler CheckedChanged;

        event EventHandler VisualUpdateRequired;

        bool Filter(Func<THash, bool> filter);
        IEnumerable<int> GetCheckedIndices();
        IEnumerable<int> GetFilteredIndices();
        IReadOnlyCollection<Interval<T>> GetIntervals();
        string GetText(int index);
        string GetText(THash date, int level);
        void Initialize(Interval<T> range);
        void Initialize(Interval<T>[] intervals);
        void Invert();
        bool? IsChecked(int index);
        void Prepare();
        void Reset();
        void Toggle(int index);
        void ToggleAll();
        void Update(object[] dates, Interval<T>[] intervals, Interval<T> range, bool isRangeSelector);

        int Count { get; }

        THash this[int index] { get; }

        bool FilteredOut { get; }

        bool HasChecks { get; }
    }
}

