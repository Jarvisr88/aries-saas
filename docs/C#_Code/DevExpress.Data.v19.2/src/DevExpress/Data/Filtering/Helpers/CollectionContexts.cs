namespace DevExpress.Data.Filtering.Helpers
{
    using System;
    using System.Collections;

    public class CollectionContexts : IEnumerable
    {
        public readonly EvaluatorContextDescriptor Descriptor;
        public readonly IEnumerable DataSource;

        public CollectionContexts(EvaluatorContextDescriptor descriptor, IEnumerable dataSource);
        public virtual IEnumerator GetEnumerator();
    }
}

