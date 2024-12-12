namespace DevExpress.Data.Filtering.Helpers
{
    using System;
    using System.Collections;

    public class CollectionContextsEnumerator : IEnumerator
    {
        public readonly EvaluatorContextDescriptor Descriptor;
        public readonly IEnumerator DataSource;

        public CollectionContextsEnumerator(EvaluatorContextDescriptor descriptor, IEnumerator dataSource);
        public virtual bool MoveNext();
        public virtual void Reset();

        public virtual object Current { get; }
    }
}

