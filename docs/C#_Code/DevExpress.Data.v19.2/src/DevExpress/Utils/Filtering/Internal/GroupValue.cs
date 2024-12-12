namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class GroupValue : GroupValueBase, IGroupValue, IGrouping<int, object>, IEnumerable<object>, IEnumerable
    {
        private readonly int key;
        private readonly int parent;
        private readonly int index;
        private readonly int level;

        internal GroupValue(int key, int parent, int index, int level);
        public sealed override string ToString();

        public sealed override int Key { get; }

        public sealed override int Level { get; }

        public sealed override object Value { get; }
    }
}

