namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal abstract class GroupValueBase : IEnumerable<object>, IEnumerable
    {
        internal IGroupValuesSource source;

        protected GroupValueBase();
        public sealed override bool Equals(object obj);
        public sealed override int GetHashCode();
        IEnumerator<object> IEnumerable<object>.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator();

        public abstract int Key { get; }

        public abstract int Level { get; }

        public abstract object Value { get; }
    }
}

