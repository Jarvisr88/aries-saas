namespace DevExpress.Data.Helpers
{
    using DevExpress.Utils.Filtering.Internal;
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public sealed class ListIDateRangeSelectorHashTree : ListIHashTree<IDateIntervalsHashTree>
    {
        public ListIDateRangeSelectorHashTree(object[] source, IDateIntervalsHashTree hashTree = null);

        public sealed override object this[int index] { get; set; }

        public sealed override int Count { get; }
    }
}

