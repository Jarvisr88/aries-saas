namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Reflection;
    using System.Windows.Forms;

    public class DbDataTableView : IDisposable, ITypedList, ICancelAddNew, IBindingListView
    {
        private bool a;
        private ListChangedEventHandler b;
        private DbDataRowView c;
        private DbDataRowView d;
        private int e;
        internal DbDataTable f;
        internal DataView g;
        internal DbDataRowView h;
        private ArrayList i;
        private ArrayList j;
        private System.Windows.Forms.BindingSource k;
        internal int l;
        private ArrayList m;
        private EventHandlerList n;
        private static readonly object o = new object();
        private static readonly object p = new object();
        private static readonly object q = new object();
        private static readonly object r = new object();
        private static readonly object s = new object();
        private static readonly object t = new object();
        internal ListChangedEventArgs u;
        private MethodInfo v;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                this.Events.AddHandler(t, value);
            }
            remove
            {
                this.Events.RemoveHandler(t, value);
            }
        }

        event ListChangedEventHandler IBindingList.ListChanged
        {
            add
            {
                this.Events.AddHandler(o, value);
            }
            remove
            {
                this.Events.RemoveHandler(o, value);
            }
        }

        public DbDataTableView() : this(null, null)
        {
        }

        public DbDataTableView(DbDataTable table) : this(table, new DataView(table))
        {
        }

        internal DbDataTableView(DbDataTable A_0, DataView A_1)
        {
            this.m = new ArrayList();
            this.f = A_0;
            this.g = A_1;
            if (A_0 != null)
            {
                A_0.a(this);
            }
            this.b = new ListChangedEventHandler(this.b);
            if ((A_0 != null) && (A_1 != null))
            {
                this.a = A_1.GetType().Name == "RelatedView";
                A_1.ListChanged += this.b;
            }
            this.e = -1;
        }

        public DbDataTableView(DbDataTable table, string RowFilter, string Sort, DataViewRowState RowState) : this(table, new DataView(table, RowFilter, Sort, RowState))
        {
        }

        private MethodInfo a()
        {
            if (this.v != null)
            {
                return this.v;
            }
            foreach (MethodInfo info in this.g.GetType().GetMethods(BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (info.Name == "UpdateIndex")
                {
                    ParameterInfo[] parameters = info.GetParameters();
                    if ((parameters != null) && ((parameters.Length == 1) && ReferenceEquals(parameters[0].ParameterType, typeof(bool))))
                    {
                        this.v = info;
                        return info;
                    }
                }
            }
            return null;
        }

        private int a(DbDataRowView A_0)
        {
            if (!ReferenceEquals(A_0.Row, A_0.InnerRowView.Row))
            {
                for (int i = 0; i < this.g.Count; i++)
                {
                    DataRowView view = this.g[i];
                    if (ReferenceEquals(view.Row, A_0.Row))
                    {
                        A_0.InnerRowView = view;
                        return i;
                    }
                }
            }
            return -1;
        }

        internal bool a(DataRow A_0) => 
            (this.j != null) && this.j.Contains(A_0);

        private DbDataRowView a(int A_0)
        {
            if (A_0 < this.RowViewCache.Count)
            {
                return (DbDataRowView) this.i[A_0];
            }
            while (true)
            {
                this.i.Add(null);
                if (A_0 < this.i.Count)
                {
                    return null;
                }
            }
        }

        internal System.Windows.Forms.CurrencyManager a(string A_0) => 
            this.BindingSource.GetRelatedCurrencyManager(A_0);

        private static bool a(DataRow A_0, object[] A_1)
        {
            bool flag = false;
            DataColumnCollection columns = A_0.Table.Columns;
            int count = columns.Count;
            int index = 0;
            while (true)
            {
                if (index < count)
                {
                    DataColumn column1 = columns[index];
                    object obj2 = A_1[index];
                    object obj3 = A_0[index];
                    if ((obj2 == obj3) || ((obj2 != null) && ((obj3 != null) && obj2.Equals(obj3))))
                    {
                        index++;
                        continue;
                    }
                    flag = true;
                }
                return flag;
            }
        }

        internal void a(DataRow A_0, bool A_1)
        {
            if (this.a(A_0) != A_1)
            {
                this.j ??= new ArrayList();
                if (A_1)
                {
                    this.j.Add(A_0);
                }
                else
                {
                    this.j.Remove(A_0);
                }
            }
        }

        private object a(int A_0, bool A_1)
        {
            object c;
            if ((this.c != null) && (this.c.Index == A_0))
            {
                return this.c;
            }
            DataRowView view = null;
            DataRow row = null;
            if ((this.f == null) || (this.g == null))
            {
                return new DbDataRowView(row, this, view, A_0);
            }
            if (this.a || (this.g.RowFilter != ""))
            {
                this.f.b(false);
            }
            else if ((A_0 + 100) >= (this.g.Count - 1))
            {
                this.f.a(A_0 + 100, A_1, false);
            }
            lock (this.f.FetchRowSyncRoot)
            {
                if ((A_0 == (this.g.Count - 1)) && (this.c != null))
                {
                    c = this.c;
                }
                else if (A_0 < 0)
                {
                    c = new DbDataRowView(null, this, null, A_0);
                }
                else
                {
                    if ((A_0 < this.g.Count) && (A_0 >= 0))
                    {
                        view = this.g[A_0];
                        row = view.Row;
                    }
                    DbDataRowView view2 = this.a(A_0);
                    if (view2 == null)
                    {
                        this.i[A_0] = view2 = new DbDataRowView(row, this, view, A_0);
                    }
                    else
                    {
                        if (!ReferenceEquals(row, view2.Row))
                        {
                            view2.Row = row;
                        }
                        if (!ReferenceEquals(view, view2.InnerRowView))
                        {
                            view2.InnerRowView = view;
                        }
                    }
                    view2.IndexInternal = A_0;
                    c = view2;
                }
            }
            return c;
        }

        internal void a(int A_0, int A_1)
        {
            if ((this.l >= 0) && (this.l < this.m.Count))
            {
                ArrayList list = (ArrayList) this.m[this.l];
                if (list.Count > 0)
                {
                    bool flag = false;
                    int oldIndex = -1;
                    int newIndex = (A_0 > A_1) ? (A_0 - 1) : A_0;
                    int num3 = list.Count - 1;
                    while (true)
                    {
                        if (num3 < 0)
                        {
                            list.Clear();
                            if (oldIndex == -1)
                            {
                                if (flag)
                                {
                                    list.Add(new ListChangedEventArgs(ListChangedType.ItemChanged, (A_0 < A_1) ? (A_1 - 1) : A_1, -1));
                                }
                            }
                            else
                            {
                                list.Add(new ListChangedEventArgs(ListChangedType.ItemMoved, newIndex, oldIndex));
                                if (flag)
                                {
                                    list.Add(new ListChangedEventArgs(ListChangedType.ItemChanged, newIndex, -1));
                                    return;
                                }
                            }
                            break;
                        }
                        ListChangedEventArgs args = (ListChangedEventArgs) list[num3];
                        if (args.NewIndex == A_0)
                        {
                            if (args.ListChangedType == ListChangedType.ItemChanged)
                            {
                                flag = true;
                            }
                            if (args.ListChangedType == ListChangedType.ItemMoved)
                            {
                                if ((A_0 > A_1) && (args.OldIndex < A_1))
                                {
                                    A_1++;
                                }
                                else if ((A_0 < A_1) && (args.OldIndex >= A_1))
                                {
                                    A_1--;
                                }
                                A_0 = args.OldIndex;
                                oldIndex = (A_0 < A_1) ? (A_1 - 1) : A_1;
                            }
                        }
                        num3--;
                    }
                }
            }
        }

        internal void a(object A_0, ListChangedEventArgs A_1)
        {
            if (A_1.ListChangedType == ListChangedType.Reset)
            {
                this.i = null;
            }
            if ((this.f == null) || (this.f.disableEvents <= 0))
            {
                if ((A_1.ListChangedType == ListChangedType.ItemAdded) && ((this.f != null) && ((this.g != null) && (A_1.NewIndex < this.g.Count))))
                {
                    DataRowView view = this.g[A_1.NewIndex];
                    if ((view.Row.RowState == DataRowState.Detached) || (view.Row.RowState == DataRowState.Added))
                    {
                        this.f.d(view.Row);
                    }
                }
                if (!DbDataTable.DisableListChangedEvents)
                {
                    ListChangedEventHandler handler = (ListChangedEventHandler) this.Events[o];
                    if (handler != null)
                    {
                        if ((this.f != null) && ((A_1.ListChangedType == ListChangedType.PropertyDescriptorAdded) || ((A_1.ListChangedType == ListChangedType.PropertyDescriptorChanged) || (A_1.ListChangedType == ListChangedType.PropertyDescriptorDeleted))))
                        {
                            this.f.y = true;
                        }
                        this.u = A_1;
                        try
                        {
                            handler(A_0, A_1);
                        }
                        catch (Exception exception)
                        {
                            this.u = null;
                            if ((exception.StackTrace != null) && (exception.StackTrace.IndexOf("at Devart.Common.DbDataTable.") > 0))
                            {
                                throw;
                            }
                        }
                    }
                }
            }
        }

        internal void a(object A_0, PropertyChangedEventArgs A_1)
        {
            PropertyChangedEventHandler handler = (PropertyChangedEventHandler) this.Events[t];
            if (handler != null)
            {
                handler(A_0, A_1);
            }
        }

        internal void b(DataRow A_0)
        {
            if (this.f != null)
            {
                this.f.e(A_0);
            }
        }

        internal void b(int A_0)
        {
            if ((this.l >= 0) && (this.l < this.m.Count))
            {
                ArrayList list = (ArrayList) this.m[this.l];
                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        ListChangedEventArgs args = (ListChangedEventArgs) list[i];
                        if ((args.ListChangedType == ListChangedType.ItemAdded) && (args.OldIndex == -1))
                        {
                            list[i] = new ListChangedEventArgs(ListChangedType.ItemMoved, args.NewIndex, A_0);
                            return;
                        }
                    }
                }
            }
        }

        internal void b(object A_0, ListChangedEventArgs A_1)
        {
            if ((A_1.ListChangedType == ListChangedType.ItemMoved) && (A_1.OldIndex == this.e))
            {
                this.e = A_1.NewIndex;
                this.CurrentRowView.IndexInternal = A_1.NewIndex;
                if (this.CurrentRowView.InnerRowView != null)
                {
                    this.CurrentRowView.Row = this.CurrentRowView.InnerRowView.Row;
                }
            }
            if ((this.f != null) && ((this.f.storeEvents > 0) && (this.l >= 0)))
            {
                while (true)
                {
                    if (this.l < this.m.Count)
                    {
                        ((ArrayList) this.m[this.l]).Add(A_1);
                        break;
                    }
                    this.m.Add(new ArrayList());
                }
            }
            this.a(this, A_1);
        }

        internal void c(DbDataRowView A_0)
        {
            if ((this.f != null) && (this.g != null))
            {
                DataRow row = A_0.Row;
                this.f.b(row);
            }
        }

        internal void c(int A_0)
        {
            if ((this.l >= 0) && (this.l < this.m.Count))
            {
                ArrayList list = (ArrayList) this.m[this.l];
                if (list.Count > 0)
                {
                    for (int i = list.Count - 1; i >= 0; i--)
                    {
                        if (((ListChangedEventArgs) list[i]).ListChangedType == ListChangedType.ItemDeleted)
                        {
                            list[i] = new ListChangedEventArgs(ListChangedType.ItemDeleted, A_0, -1);
                        }
                    }
                }
            }
        }

        internal void d(DbDataRowView A_0)
        {
            if (A_0 == null)
            {
                this.d = null;
                this.e = -1;
            }
            else
            {
                this.d = A_0;
                this.e = A_0.Index;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if ((this.f != null) && (this.g != null))
            {
                this.g.ListChanged -= this.b;
            }
        }

        internal void f()
        {
            if (this.f != null)
            {
                this.f.t();
            }
        }

        ~DbDataTableView()
        {
            this.Dispose(false);
        }

        internal bool i()
        {
            if ((this.l > 0) && ((this.l - 1) < this.m.Count))
            {
                ArrayList list = (ArrayList) this.m[this.l - 1];
                if (list.Count > 0)
                {
                    ListChangedEventArgs args = (ListChangedEventArgs) list[0];
                    list.RemoveAt(0);
                    int index = this.l - 1;
                    while (true)
                    {
                        if (index >= 0)
                        {
                            list = (ArrayList) this.m[index];
                            if (list.Count == 0)
                            {
                                this.m.RemoveAt(index);
                                index--;
                                continue;
                            }
                        }
                        this.a(this, args);
                        return true;
                    }
                }
            }
            return false;
        }

        internal void j()
        {
            MethodInfo info = this.a();
            if (info != null)
            {
                object[] parameters = new object[] { true };
                info.Invoke(this.g, parameters);
            }
        }

        internal bool k()
        {
            if ((this.f == null) || ((this.g == null) || ((this.c == null) || !this.c.e)))
            {
                this.AddNewRow = null;
                return false;
            }
            DataRow row = this.c.Row;
            bool flag = this.g.Count == 1;
            object[] objArray = DbDataRowView.a(row, true);
            int index = this.c.Index;
            try
            {
                this.a(this.c.Index);
                this.i[this.c.Index] = this.c;
                this.f.a(ref row, false);
                this.a(this.c);
                a(this.c.Row, objArray);
            }
            catch
            {
                if ((!this.f.CachedUpdates && (this.g.Count == 0)) & flag)
                {
                    this.f.disableEvents++;
                    this.f.disableUpdateEvents++;
                    this.f.storeEvents++;
                    try
                    {
                        DataRowView view = this.g.AddNew();
                        DataRow row2 = view.Row;
                        int num2 = 0;
                        while (true)
                        {
                            if (num2 >= this.f.Columns.Count)
                            {
                                row = row2;
                                this.c.Row = row2;
                                this.c.InnerRowView = view;
                                break;
                            }
                            row2[num2] = objArray[num2];
                            num2++;
                        }
                    }
                    finally
                    {
                        this.f.disableEvents--;
                        this.f.disableUpdateEvents--;
                        this.f.storeEvents--;
                    }
                    this.b(index);
                    this.f();
                }
                throw;
            }
            finally
            {
                if (!ReferenceEquals(this.c.Row, row))
                {
                    this.c.Row = row;
                }
            }
            this.f.disableEvents++;
            this.f.storeEvents++;
            try
            {
                this.c.InnerRowView.CancelEdit();
            }
            finally
            {
                this.f.disableEvents--;
                this.f.storeEvents--;
            }
            int num1 = this.c.Index;
            int count = this.g.Count;
            this.c.e = false;
            this.AddNewRow = null;
            return true;
        }

        internal void m()
        {
            if (this.c != null)
            {
                this.c.CancelEdit();
                this.AddNewRow = null;
            }
        }

        public void RefreshRow(DbDataRowView rowView)
        {
            if (this.f != null)
            {
                object[] objArray = DbDataRowView.a(rowView.Row, true);
                this.f.RefreshRow(rowView.Row);
                if (a(rowView.Row, objArray))
                {
                    this.a(this, new ListChangedEventArgs(ListChangedType.ItemChanged, rowView.Index, rowView.Index));
                }
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            if (this.f != null)
            {
                this.f.b(false);
            }
            int count = ((ICollection) this).Count;
            for (int i = 0; i < count; i++)
            {
                array.SetValue(((IList) this)[i], (int) (index + i));
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (this.f != null)
            {
                this.f.b(false);
            }
            int count = ((ICollection) this).Count;
            DbDataRowView[] viewArray = new DbDataRowView[count];
            for (int i = 0; i < count; i++)
            {
                viewArray[i] = (DbDataRowView) ((IList) this)[i];
            }
            return viewArray.GetEnumerator();
        }

        int IList.Add(object value) => 
            0;

        void IList.Clear()
        {
        }

        bool IList.Contains(object value) => 
            false;

        int IList.IndexOf(object value) => 
            0;

        void IList.Insert(int index, object value)
        {
        }

        void IList.Remove(object value)
        {
            DbDataRowView view = value as DbDataRowView;
            if (view == null)
            {
                throw new ArgumentException();
            }
            ((IList) this).RemoveAt(view.Index);
        }

        void IList.RemoveAt(int index)
        {
            if (((this.f != null) && (this.g != null)) && (index < this.g.Count))
            {
                if ((this.c != null) && (index == (this.g.Count - 1)))
                {
                    this.m();
                }
                else
                {
                    DataRow row = this.g[index].Row;
                    this.f.disableEvents++;
                    try
                    {
                        this.f.c(row);
                        if (index < this.RowViewCache.Count)
                        {
                            this.RowViewCache.RemoveAt(index);
                        }
                    }
                    finally
                    {
                        this.f.disableEvents--;
                    }
                    this.f();
                }
            }
        }

        void IBindingList.AddIndex(PropertyDescriptor property)
        {
            if (this.g != null)
            {
                ((IBindingList) this.g).AddIndex(property);
            }
        }

        object IBindingList.AddNew()
        {
            DataRowView view;
            DataRow row;
            if (this.c != null)
            {
                this.m();
            }
            if ((this.f != null) && ((this.g != null) && !this.f.FetchComplete))
            {
                if (this.f.AllowCruidDuringFetch)
                {
                    this.f.a(((ICollection) this).Count - 1, false, false);
                }
                else
                {
                    int count = this.g.Count;
                    this.f.b(true);
                    if (this.g.Count > count)
                    {
                        return ((IList) this)[count - 1];
                    }
                }
            }
            int num = (this.f != null) ? this.g.Count : 0;
            if ((this.f == null) || (this.g == null))
            {
                view = null;
                row = null;
            }
            else
            {
                this.f.disableEvents++;
                this.f.storeEvents++;
                try
                {
                    view = this.g.AddNew();
                    row = view.Row;
                }
                finally
                {
                    this.f.disableEvents--;
                    this.f.storeEvents--;
                }
                if (this.RowViewCache.Count == this.g.Count)
                {
                    DbDataRowView view2 = (DbDataRowView) this.RowViewCache[this.g.Count - 1];
                    if ((view2 != null) && (ReferenceEquals(view2.Row, row) && (view2.Index == num)))
                    {
                        this.AddNewRow = view2;
                    }
                    this.RowViewCache.Remove(this.g.Count - 1);
                }
            }
            if (this.c == null)
            {
                this.AddNewRow = new DbDataRowView(row, this, view, num);
            }
            this.c.e = true;
            this.f();
            return this.c;
        }

        void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            if ((this.g != null) && (property is Devart.Common.d))
            {
                this.f.b(true);
                ((IBindingList) this.g).ApplySort(property, direction);
            }
        }

        int IBindingList.Find(PropertyDescriptor property, object key) => 
            (this.g == null) ? 0 : ((IBindingList) this.g).Find(property, key);

        void IBindingList.RemoveIndex(PropertyDescriptor property)
        {
            if (this.g != null)
            {
                ((IBindingList) this.g).RemoveIndex(property);
            }
        }

        void IBindingList.RemoveSort()
        {
            if (this.g != null)
            {
                ((IBindingList) this.g).RemoveSort();
            }
        }

        void IBindingListView.ApplySort(ListSortDescriptionCollection sorts)
        {
            if (this.g != null)
            {
                ((IBindingListView) this.g).ApplySort(sorts);
            }
        }

        void IBindingListView.RemoveFilter()
        {
            if (this.g != null)
            {
                ((IBindingListView) this.g).RemoveFilter();
            }
        }

        void ICancelAddNew.CancelNew(int itemIndex)
        {
            if ((this.c != null) && (this.c.Index == itemIndex))
            {
                this.m();
            }
        }

        void ICancelAddNew.EndNew(int itemIndex)
        {
            if ((this.c != null) && (this.c.Index == itemIndex))
            {
                this.c.EndEdit();
            }
        }

        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors) => 
            (this.f != null) ? this.f.c(listAccessors) : (!Utils.DesignMode ? new PropertyDescriptorCollection(null) : new Devart.Common.p(null));

        string ITypedList.GetListName(PropertyDescriptor[] listAccessors) => 
            (this.f != null) ? this.f.TableName : "";

        int ICollection.Count
        {
            get
            {
                int count = 0;
                try
                {
                    if ((this.f != null) && (this.g != null))
                    {
                        lock (this.f.FetchRowSyncRoot)
                        {
                            if (this.g.RowFilter != "")
                            {
                                this.f.b(false);
                            }
                            if (!this.f.QueryRecordCount)
                            {
                                count += this.g.Count + (((this.f.FetchComplete || ((this.c != null) && this.f.AllowCruidDuringFetch)) || (this.g.RowFilter != "")) ? 0 : 1);
                            }
                            else if (!this.a)
                            {
                                count = (this.f.FetchComplete || (this.g.RowFilter != "")) ? this.g.Count : this.f.RecordCount;
                            }
                            else
                            {
                                this.f.b(false);
                                count = this.g.Count;
                            }
                        }
                    }
                }
                catch
                {
                }
                return count;
            }
        }

        bool ICollection.IsSynchronized =>
            false;

        object ICollection.SyncRoot =>
            this;

        private System.Windows.Forms.BindingSource BindingSource
        {
            get
            {
                if (this.k == null)
                {
                    this.k = new System.Windows.Forms.BindingSource();
                    this.k.DataSource = this.f;
                }
                return this.k;
            }
        }

        bool IList.IsFixedSize =>
            false;

        bool IList.IsReadOnly =>
            false;

        object IList.this[int index]
        {
            get => 
                this.a(index, true);
            set
            {
                if (((this.f != null) && (this.g != null)) && ((index == (this.g.Count - 1)) && (this.c != null)))
                {
                    this.AddNewRow = (DbDataRowView) value;
                }
            }
        }

        bool IBindingList.AllowEdit =>
            (this.g != null) && ((IBindingList) this.g).AllowEdit;

        bool IBindingList.AllowNew =>
            (this.g != null) && ((IBindingList) this.g).AllowNew;

        bool IBindingList.AllowRemove =>
            (this.g != null) && ((IBindingList) this.g).AllowRemove;

        bool IBindingList.IsSorted =>
            (this.g != null) && ((IBindingList) this.g).IsSorted;

        ListSortDirection IBindingList.SortDirection =>
            (this.g == null) ? ListSortDirection.Ascending : ((IBindingList) this.g).SortDirection;

        PropertyDescriptor IBindingList.SortProperty =>
            (this.g == null) ? null : ((IBindingList) this.g).SortProperty;

        bool IBindingList.SupportsChangeNotification =>
            (this.g == null) || ((IBindingList) this.g).SupportsChangeNotification;

        bool IBindingList.SupportsSearching =>
            (this.g != null) && ((IBindingList) this.g).SupportsSearching;

        bool IBindingList.SupportsSorting =>
            (this.g != null) && ((IBindingList) this.g).SupportsSorting;

        protected EventHandlerList Events
        {
            get
            {
                this.n ??= new EventHandlerList();
                return this.n;
            }
        }

        internal DbDataRowView AddNewRow
        {
            get => 
                this.c;
            set
            {
                if (!ReferenceEquals(this.c, value))
                {
                    if ((this.c == null) && (value != null))
                    {
                        this.f.r();
                    }
                    if ((this.c != null) && (value == null))
                    {
                        this.f.n();
                    }
                    this.c = value;
                }
            }
        }

        public DbDataTable DataTable =>
            this.f;

        internal int CurrentIndex =>
            this.e;

        internal DbDataRowView CurrentRowView =>
            this.d;

        private ArrayList RowViewCache
        {
            get
            {
                this.i ??= new ArrayList();
                return this.i;
            }
        }

        internal bool IsDetailView =>
            this.a;

        internal System.Windows.Forms.CurrencyManager CurrencyManager =>
            this.BindingSource.CurrencyManager;

        string IBindingListView.Filter
        {
            get => 
                (this.g == null) ? null : ((IBindingListView) this.g).Filter;
            set
            {
                if (this.g != null)
                {
                    ((IBindingListView) this.g).Filter = value;
                }
            }
        }

        ListSortDescriptionCollection IBindingListView.SortDescriptions =>
            (this.g == null) ? null : ((IBindingListView) this.g).SortDescriptions;

        bool IBindingListView.SupportsAdvancedSorting =>
            (this.g != null) && ((IBindingListView) this.g).SupportsAdvancedSorting;

        bool IBindingListView.SupportsFiltering =>
            (this.g != null) && ((IBindingListView) this.g).SupportsFiltering;
    }
}

