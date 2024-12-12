namespace DevExpress.Utils.Serializing.Helpers
{
    using System;
    using System.Collections;

    public abstract class CollectionItemInfosEnumeratorBase : IEnumerator
    {
        protected XtraPropertyInfo currentInfo;

        protected CollectionItemInfosEnumeratorBase()
        {
        }

        public bool MoveNext()
        {
            this.currentInfo = null;
            return this.MoveNextCore();
        }

        protected abstract bool MoveNextCore();
        public virtual void Reset()
        {
            this.currentInfo = null;
        }

        public object Current =>
            this.currentInfo;
    }
}

