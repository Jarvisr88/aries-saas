namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Reflection;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class ListIHashTree<THashTree> : ListAdapterBase<IEnumerable<object>>, IList, ICollection, IEnumerable where THashTree: IHashTree, IHashTreeIndices
    {
        protected readonly THashTree hashTreeCore;

        protected ListIHashTree(object[] source, THashTree hashTree);
        void ICollection.CopyTo(Array array, int index);
        int IList.IndexOf(object value);

        public THashTree HashTree { get; }

        public abstract object this[int index] { get; set; }

        public abstract int Count { get; }

        bool ICollection.IsSynchronized { get; }

        object ICollection.SyncRoot { get; }
    }
}

