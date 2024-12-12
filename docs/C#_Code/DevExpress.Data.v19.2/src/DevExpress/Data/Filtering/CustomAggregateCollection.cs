namespace DevExpress.Data.Filtering
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CustomAggregateCollection : ICollection<ICustomAggregate>, IEnumerable<ICustomAggregate>, IEnumerable
    {
        private readonly Dictionary<string, ICustomAggregate> customAggregateByName;

        public CustomAggregateCollection();
        public void Add(ICustomAggregate customAggregate);
        public void Add(IEnumerable<ICustomAggregate> items);
        public void Clear();
        public bool Contains(ICustomAggregate item);
        public void CopyTo(ICustomAggregate[] array, int arrayIndex);
        public ICustomAggregate GetCustomAggregate(string aggregateName);
        public IEnumerator<ICustomAggregate> GetEnumerator();
        public bool Remove(ICustomAggregate item);
        IEnumerator IEnumerable.GetEnumerator();

        public int Count { get; }

        public bool IsReadOnly { get; }
    }
}

