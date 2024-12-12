namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class LazyDataProxyListWrapper : IEnumerable<DataProxy>, IEnumerable
    {
        private readonly IList listSource;
        private IList<DataProxy> view;
        private readonly DataAccessor dataAccessor;
        private int initialCount;

        public LazyDataProxyListWrapper(DataAccessor dataAccessor, IList listSource)
        {
            this.dataAccessor = dataAccessor;
            this.listSource = listSource;
            this.initialCount = listSource.Count;
        }

        public IEnumerator<DataProxy> GetEnumerator() => 
            new LazyDataProxyListEnumerator(this);

        public int IndexOf(DataProxy item) => 
            this.View.IndexOf(item);

        public void Insert(int index, DataProxy item)
        {
            this.View.Insert(index, item);
            this.initialCount++;
        }

        public void RemoveAt(int index)
        {
            this.View.RemoveAt(index);
            this.initialCount--;
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        public IList<DataProxy> View
        {
            get
            {
                IList<DataProxy> view = this.view;
                if (this.view == null)
                {
                    IList<DataProxy> local1 = this.view;
                    view = this.view = new List<DataProxy>(new DataProxy[this.initialCount]);
                }
                return view;
            }
        }

        public int Count =>
            this.initialCount;

        public DataProxy this[int index]
        {
            get
            {
                DataProxy local1 = this.View[index];
                DataProxy local3 = local1;
                if (local1 == null)
                {
                    DataProxy local2 = local1;
                    local3 = this.View[index] = this.dataAccessor.CreateProxy(this.listSource[index], index);
                }
                return local3;
            }
            set => 
                this.View[index] = value;
        }
    }
}

