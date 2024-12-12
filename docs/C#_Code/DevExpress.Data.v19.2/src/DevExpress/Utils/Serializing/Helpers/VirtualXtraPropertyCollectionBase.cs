namespace DevExpress.Utils.Serializing.Helpers
{
    using System;
    using System.Collections;
    using System.Reflection;

    public abstract class VirtualXtraPropertyCollectionBase : IXtraPropertyCollection, ICollection, IEnumerable
    {
        private bool enumeratorCreated;

        protected VirtualXtraPropertyCollectionBase()
        {
        }

        public void Add(XtraPropertyInfo prop)
        {
            throw new InvalidOperationException();
        }

        public void AddRange(ICollection props)
        {
            throw new InvalidOperationException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new InvalidOperationException();
        }

        protected abstract CollectionItemInfosEnumeratorBase CreateEnumerator();
        public IEnumerator GetEnumerator()
        {
            if (this.enumeratorCreated)
            {
                throw new InvalidOperationException();
            }
            this.enumeratorCreated = true;
            return this.CreateEnumerator();
        }

        public XtraPropertyInfo this[string name]
        {
            get
            {
                throw new InvalidOperationException();
            }
        }

        public XtraPropertyInfo this[int index]
        {
            get
            {
                throw new InvalidOperationException();
            }
        }

        public bool IsSinglePass =>
            true;

        public virtual int Count
        {
            get
            {
                throw new InvalidOperationException();
            }
        }

        public bool IsSynchronized =>
            false;

        public object SyncRoot =>
            null;
    }
}

