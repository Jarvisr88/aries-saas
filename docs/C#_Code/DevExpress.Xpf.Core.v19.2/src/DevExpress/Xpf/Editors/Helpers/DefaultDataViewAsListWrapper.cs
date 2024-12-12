namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class DefaultDataViewAsListWrapper : IList, ICollection, IEnumerable
    {
        private readonly DefaultDataView view;

        public DefaultDataViewAsListWrapper(DefaultDataView view)
        {
            this.view = view;
        }

        void ICollection.CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Func<DataProxy, object> selector = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<DataProxy, object> local1 = <>c.<>9__4_0;
                selector = <>c.<>9__4_0 = item => item.f_component;
            }
            return this.view.Select<DataProxy, object>(selector).GetEnumerator();
        }

        int IList.Add(object value)
        {
            throw new NotImplementedException();
        }

        void IList.Clear()
        {
            throw new NotImplementedException();
        }

        bool IList.Contains(object value)
        {
            throw new NotImplementedException();
        }

        int IList.IndexOf(object value)
        {
            throw new NotImplementedException();
        }

        void IList.Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        void IList.Remove(object value)
        {
            throw new NotImplementedException();
        }

        void IList.RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public object this[int index] =>
            this.view.GetItemByIndex(index);

        int ICollection.Count =>
            this.view.VisibleRowCount;

        object ICollection.SyncRoot =>
            this;

        bool ICollection.IsSynchronized =>
            true;

        object IList.this[int index]
        {
            get => 
                this.view[index].f_component;
            set
            {
                throw new NotImplementedException();
            }
        }

        bool IList.IsReadOnly =>
            true;

        bool IList.IsFixedSize =>
            true;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DefaultDataViewAsListWrapper.<>c <>9 = new DefaultDataViewAsListWrapper.<>c();
            public static Func<DataProxy, object> <>9__4_0;

            internal object <System.Collections.IEnumerable.GetEnumerator>b__4_0(DataProxy item) => 
                item.f_component;
        }
    }
}

