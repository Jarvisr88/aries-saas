namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Windows.Forms;

    internal class r : IDisposable, ITypedList, ICancelAddNew, IBindingListView
    {
        private DbDataTableView a;
        private ListChangedEventHandler b;
        private bool c;
        private PropertyChangedEventHandler d;

        private event ListChangedEventHandler System.ComponentModel.IBindingList.ListChanged
        {
            add
            {
                this.b += A_0;
            }
            remove
            {
                this.b -= A_0;
            }
        }

        public r(DbDataTableView A_0)
        {
            this.a(A_0);
            if (A_0 == null)
            {
                throw new ArgumentNullException();
            }
        }

        public void a()
        {
            GC.SuppressFinalize(this);
        }

        public void a(bool A_0)
        {
            if (this.c != A_0)
            {
                this.c = A_0;
                if (this.c)
                {
                    this.a(this.a);
                }
            }
        }

        private void a(ListSortDescriptionCollection A_0)
        {
            ((IBindingListView) this.a).ApplySort(A_0);
        }

        public void a(PropertyChangedEventHandler A_0)
        {
            this.d -= A_0;
            if ((this.d == null) && (this.a != null))
            {
                this.a.PropertyChanged -= new PropertyChangedEventHandler(this.a);
            }
        }

        private bool a(object A_0) => 
            ((IList) this.a).Contains(A_0);

        private string a(PropertyDescriptor[] A_0) => 
            ((ITypedList) this.a).GetListName(A_0);

        private void a(DbDataTableView A_0)
        {
            if (this.a != null)
            {
                this.a.ListChanged -= new ListChangedEventHandler(this.a);
                if (this.d != null)
                {
                    this.a.PropertyChanged -= new PropertyChangedEventHandler(this.a);
                }
            }
            this.a = this.b(A_0);
            if (this.a != null)
            {
                this.a.ListChanged += new ListChangedEventHandler(this.a);
                if (this.d != null)
                {
                    this.a.PropertyChanged += new PropertyChangedEventHandler(this.a);
                }
            }
        }

        private void a(PropertyDescriptor A_0)
        {
            ((IBindingList) this.a).RemoveIndex(A_0);
        }

        private bool a(DbDataTableView A_0, DbDataTableView A_1) => 
            this.k() ? (((A_0.g != null) || (A_1.g != null)) ? (((A_0.DataTable == null) && (A_1.DataTable == null)) || ((A_0.g != null) && ((A_1.g != null) && ((A_0.DataTable != null) && ((A_1.DataTable != null) && (ReferenceEquals(A_0.f, A_1.f) && (ReferenceEquals(A_0.g.GetType(), A_1.g.GetType()) && ((A_0.g.GetType().Name != "RelatedView") || ReferenceEquals(A_0.g, A_1.g))))))))) : true) : ReferenceEquals(A_0, A_1);

        private void a(Array A_0, int A_1)
        {
            ((ICollection) this.a).CopyTo(A_0, A_1);
        }

        private void a(PropertyDescriptor A_0, ListSortDirection A_1)
        {
            ((IBindingList) this.a).ApplySort(A_0, A_1);
        }

        private int a(PropertyDescriptor A_0, object A_1) => 
            ((IBindingList) this.a).Find(A_0, A_1);

        internal void a(object A_0, ListChangedEventArgs A_1)
        {
            if ((this.b != null) && ((this.a.f == null) || (this.a.f.disableEvents <= 0)))
            {
                try
                {
                    this.b(A_0, A_1);
                }
                catch
                {
                }
            }
        }

        internal void a(object A_0, PropertyChangedEventArgs A_1)
        {
            if (this.d != null)
            {
                this.d(A_0, A_1);
            }
        }

        private DbDataTableView b(DbDataTableView A_0)
        {
            DbDataTableView view;
            if (!this.k())
            {
                view = A_0;
            }
            else if ((A_0.g == null) || (A_0.DataTable == null))
            {
                view = A_0;
            }
            else if (ReferenceEquals(A_0.g.GetType(), typeof(DataView)))
            {
                view = new DbDataTableView(A_0.f, new DataView(A_0.f));
            }
            else
            {
                DbDataRowView h = A_0.h;
                DataRelation key = null;
                foreach (DictionaryEntry entry in h.RelationViews)
                {
                    if (ReferenceEquals(((Devart.Common.w) entry.Value).a, A_0))
                    {
                        key = (DataRelation) entry.Key;
                        break;
                    }
                }
                view = new DbDataTableView(A_0.f, h.InnerRowView.CreateChildView(key));
            }
            return view;
        }

        private PropertyDescriptorCollection b(PropertyDescriptor[] A_0) => 
            ((ITypedList) this.a).GetItemProperties(A_0);

        public void b(PropertyChangedEventHandler A_0)
        {
            if ((this.d == null) && (this.a != null))
            {
                this.a.PropertyChanged += new PropertyChangedEventHandler(this.a);
            }
            this.d += A_0;
        }

        private void b(PropertyDescriptor A_0)
        {
            ((IBindingList) this.a).AddIndex(A_0);
        }

        private void b(int A_0)
        {
            ((ICancelAddNew) this.a).CancelNew(A_0);
        }

        private int b(object A_0) => 
            ((IList) this.a).IndexOf(A_0);

        internal CurrencyManager b(string A_0) => 
            this.a.a(A_0);

        private void b(int A_0, object A_1)
        {
            ((IList) this.a).Insert(A_0, A_1);
        }

        internal void c(DbDataTableView A_0)
        {
            if (A_0 == null)
            {
                throw new ArgumentNullException();
            }
            if (!this.a(this.a, A_0))
            {
                this.a(A_0);
                this.a(this, new ListChangedEventArgs(ListChangedType.Reset, null));
                this.a(this, new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, null));
            }
        }

        private void c(int A_0)
        {
            ((IList) this.a).RemoveAt(A_0);
        }

        private int c(object A_0) => 
            ((IList) this.a).Add(A_0);

        private void d(int A_0)
        {
            ((ICancelAddNew) this.a).EndNew(A_0);
        }

        private void d(object A_0)
        {
            ((IList) this.a).Remove(A_0);
        }

        private IEnumerator g() => 
            ((IEnumerable) this.a).GetEnumerator();

        public DbDataTableView h() => 
            this.a;

        public bool k() => 
            this.c;

        private void o()
        {
            ((IBindingListView) this.a).RemoveFilter();
        }

        private object p() => 
            ((IBindingList) this.a).AddNew();

        private void u()
        {
            ((IList) this.a).Clear();
        }

        internal CurrencyManager w() => 
            this.a.CurrencyManager;

        private void x()
        {
            ((IBindingList) this.a).RemoveSort();
        }

        [__DynamicallyInvokable]
        private int System.Collections.ICollection.Count =>
            ((ICollection) this.a).Count;

        [__DynamicallyInvokable]
        private bool System.Collections.ICollection.IsSynchronized =>
            ((ICollection) this.a).IsSynchronized;

        [__DynamicallyInvokable]
        private object System.Collections.ICollection.SyncRoot =>
            ((ICollection) this.a).SyncRoot;

        [__DynamicallyInvokable]
        private bool System.Collections.IList.IsFixedSize =>
            ((IList) this.a).IsFixedSize;

        [__DynamicallyInvokable]
        private bool System.Collections.IList.IsReadOnly =>
            ((IList) this.a).IsReadOnly;

        [__DynamicallyInvokable]
        private object this[int index]
        {
            get => 
                ((IList) this.a)[A_0];
            set => 
                ((IList) this.a)[A_0] = value;
        }

        private bool System.ComponentModel.IBindingList.AllowEdit =>
            ((IBindingList) this.a).AllowEdit;

        private bool System.ComponentModel.IBindingList.AllowNew =>
            ((IBindingList) this.a).AllowNew;

        private bool System.ComponentModel.IBindingList.AllowRemove =>
            ((IBindingList) this.a).AllowRemove;

        private bool System.ComponentModel.IBindingList.IsSorted =>
            ((IBindingList) this.a).IsSorted;

        private ListSortDirection System.ComponentModel.IBindingList.SortDirection =>
            ((IBindingList) this.a).SortDirection;

        private PropertyDescriptor System.ComponentModel.IBindingList.SortProperty =>
            ((IBindingList) this.a).SortProperty;

        private bool System.ComponentModel.IBindingList.SupportsChangeNotification =>
            ((IBindingList) this.a).SupportsChangeNotification;

        private bool System.ComponentModel.IBindingList.SupportsSearching =>
            ((IBindingList) this.a).SupportsSearching;

        private bool System.ComponentModel.IBindingList.SupportsSorting =>
            ((IBindingList) this.a).SupportsSorting;

        private string System.ComponentModel.IBindingListView.Filter
        {
            get => 
                ((IBindingListView) this.a).Filter;
            set => 
                ((IBindingListView) this.a).Filter = value;
        }

        private ListSortDescriptionCollection System.ComponentModel.IBindingListView.SortDescriptions =>
            ((IBindingListView) this.a).SortDescriptions;

        private bool System.ComponentModel.IBindingListView.SupportsAdvancedSorting =>
            ((IBindingListView) this.a).SupportsAdvancedSorting;

        private bool System.ComponentModel.IBindingListView.SupportsFiltering =>
            ((IBindingListView) this.a).SupportsFiltering;
    }
}

