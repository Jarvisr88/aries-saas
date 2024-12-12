namespace DevExpress.Data.Helpers
{
    using DevExpress.Utils.Filtering.Internal;
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public sealed class ListIDateIntervalsHashTree : ListIHashTree<IDateIntervalsHashTree>
    {
        public ListIDateIntervalsHashTree(object[] source, IDateIntervalsHashTree hashTree = null, bool? rootVisible = new bool?());

        public sealed override object this[int index] { get; set; }

        public sealed override int Count { get; }
    }
}

