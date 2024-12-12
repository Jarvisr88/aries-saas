namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public interface ICheckedGroupValues
    {
        event EventHandler<GroupValuesCheckedEventArgs> Checked;

        IEnumerable<CriteriaOperator> GetDelayedFilters<T>(string[] grouping, Type[] groupingTypes);
        ICheckedValuesEnumerator GetEnumerator();
        IEnumerable<int> GetIndices();
        void Initialize(ICheckedGroup value);
        void Prepare();

        bool HasDelayedFilters { get; }
    }
}

