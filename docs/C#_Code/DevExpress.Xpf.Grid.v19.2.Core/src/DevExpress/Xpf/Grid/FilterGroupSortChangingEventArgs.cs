namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class FilterGroupSortChangingEventArgs : RoutedEventArgs
    {
        private bool filterChanged;
        private NotifyChangesDictionary<string, CriteriaOperator> splitColumnFilters;
        private string searchString;
        private CriteriaOperator complexFilterCriteria;

        public FilterGroupSortChangingEventArgs(DataControlBase dataControl, IList<SortDefinition> sortInfo, int groupCount)
        {
            this.Source = dataControl;
            if (this.Source.DataView != null)
            {
                this.searchString = this.Source.DataView.SearchString;
            }
            this.ParseCriteriaOperator(this.Source.FilterCriteria);
            this.SortInfo = sortInfo;
            this.GroupCount = groupCount;
        }

        protected internal virtual CriteriaOperator GetFilterCriteria() => 
            CriteriaOperator.And(this.ComplexFilterCriteria, CriteriaOperator.And(this.SplitColumnFilters.Values));

        public bool IsFilteredBy(string fieldName) => 
            this.SplitColumnFilters.ContainsKey(fieldName);

        public bool IsGroupedBy(string fieldName)
        {
            SortDefinition definition;
            return (this.TryGetSortDefinition(fieldName, out definition) && (this.SortInfo.IndexOf(definition) < this.GroupCount));
        }

        public bool IsSortedBy(string fieldName)
        {
            SortDefinition definition;
            return this.TryGetSortDefinition(fieldName, out definition);
        }

        protected virtual void ParseCriteriaOperator(CriteriaOperator criteriaOperator)
        {
            Tuple<CriteriaOperator, IDictionary<string, CriteriaOperator>> tuple = CriteriaColumnAffinityResolver.SplitByColumnNames(criteriaOperator, null);
            this.Setup(tuple.Item1, new NotifyChangesDictionary<string, CriteriaOperator>(tuple.Item2));
        }

        protected void Setup(CriteriaOperator complexFilterCriteria, NotifyChangesDictionary<string, CriteriaOperator> splitColumnFilters)
        {
            this.complexFilterCriteria = complexFilterCriteria;
            this.splitColumnFilters = splitColumnFilters;
        }

        private bool TryGetSortDefinition(string fieldName, out SortDefinition sortDefinition)
        {
            sortDefinition = (from info in this.SortInfo
                where info.PropertyName == fieldName
                select info).FirstOrDefault<SortDefinition>();
            return (sortDefinition != null);
        }

        public DataControlBase Source { get; private set; }

        protected internal bool FilterChanged
        {
            get => 
                this.filterChanged || this.splitColumnFilters.Changed;
            protected set => 
                this.filterChanged = value;
        }

        public CriteriaOperator ComplexFilterCriteria
        {
            get => 
                this.complexFilterCriteria;
            set
            {
                this.complexFilterCriteria = value;
                this.FilterChanged = true;
            }
        }

        public string SearchString
        {
            get => 
                this.searchString;
            set
            {
                if (this.searchString != value)
                {
                    this.searchString = value;
                    this.FilterChanged = true;
                }
            }
        }

        public IDictionary<string, CriteriaOperator> SplitColumnFilters =>
            this.splitColumnFilters;

        public IList<SortDefinition> SortInfo { get; private set; }

        public int GroupCount { get; set; }

        private protected class NotifyChangesDictionary<T1, T2> : IDictionary<T1, T2>, ICollection<KeyValuePair<T1, T2>>, IEnumerable<KeyValuePair<T1, T2>>, IEnumerable
        {
            private readonly IDictionary<T1, T2> source;
            private bool changed;

            public NotifyChangesDictionary(IDictionary<T1, T2> source)
            {
                this.source = source;
            }

            public void Add(KeyValuePair<T1, T2> item)
            {
                this.source.Add(item);
                this.Changed = true;
            }

            public void Add(T1 key, T2 value)
            {
                this.source.Add(key, value);
                this.Changed = true;
            }

            public void Clear()
            {
                this.source.Clear();
                this.Changed = true;
            }

            public bool Contains(KeyValuePair<T1, T2> item) => 
                this.source.Contains(item);

            public bool ContainsKey(T1 key) => 
                this.source.ContainsKey(key);

            public void CopyTo(KeyValuePair<T1, T2>[] array, int arrayIndex)
            {
                this.source.CopyTo(array, arrayIndex);
            }

            public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator() => 
                this.source.GetEnumerator();

            public bool Remove(T1 key)
            {
                this.Changed = true;
                return this.source.Remove(key);
            }

            public bool Remove(KeyValuePair<T1, T2> item)
            {
                this.Changed = true;
                return this.source.Remove(item);
            }

            IEnumerator IEnumerable.GetEnumerator() => 
                this.source.GetEnumerator();

            public bool TryGetValue(T1 key, out T2 value) => 
                this.source.TryGetValue(key, out value);

            public bool Changed
            {
                get => 
                    this.changed;
                private set => 
                    this.changed = value;
            }

            public T2 this[T1 key]
            {
                get => 
                    this.source[key];
                set
                {
                    this.source[key] = value;
                    this.Changed = true;
                }
            }

            public ICollection<T1> Keys =>
                this.source.Keys;

            public ICollection<T2> Values =>
                this.source.Values;

            public int Count =>
                this.source.Count;

            public bool IsReadOnly =>
                this.source.IsReadOnly;
        }
    }
}

