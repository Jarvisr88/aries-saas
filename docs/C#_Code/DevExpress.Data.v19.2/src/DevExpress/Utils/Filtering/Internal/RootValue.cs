namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class RootValue : GroupValueBase, IGroupValue, IGrouping<int, object>, IEnumerable<object>, IEnumerable
    {
        public sealed override string ToString();

        public sealed override int Key { get; }

        public sealed override int Level { get; }

        public sealed override object Value { get; }
    }
}

