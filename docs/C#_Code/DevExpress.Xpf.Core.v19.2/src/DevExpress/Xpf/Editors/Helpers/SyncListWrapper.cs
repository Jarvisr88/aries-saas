namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class SyncListWrapper : ITypedList, IBindingList, IList, ICollection, IEnumerable, IDisposable
    {
        private IListServer originalServer;
        private IListServer server;
        private int count;
        private Dictionary<int, SyncRowInfo> rows;
        private IListServerDataView view;
        private string lastFilterCriteria;
        private int lastGroupCount;
        private IList<ServerModeOrderDescriptor[]> lastSorting;
        private IList<ServerModeSummaryDescriptor> lastGroupSummaryInfo;
        private IList<ServerModeSummaryDescriptor> lastTotalSummaryInfo;

        private event ListChangedEventHandler listChanged;

        event ListChangedEventHandler IBindingList.ListChanged
        {
            add
            {
                this.listChanged += value;
            }
            remove
            {
                this.listChanged -= value;
            }
        }

        public SyncListWrapper(IListServer server)
        {
            this.originalServer = server;
        }

        public void ApplySortGroupFilter(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sorting, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo = null, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo = null)
        {
            this.ApplySortGroupFilterToServer(filterCriteria, this.CreateList<ServerModeOrderDescriptor[]>(sorting), groupCount, this.CreateList<ServerModeSummaryDescriptor>(groupSummaryInfo), this.CreateList<ServerModeSummaryDescriptor>(totalSummaryInfo));
        }

        private void ApplySortGroupFilterToServer(CriteriaOperator filterCriteria, IList<ServerModeOrderDescriptor[]> sorting, int groupCount, IList<ServerModeSummaryDescriptor> groupSummaryInfo, IList<ServerModeSummaryDescriptor> totalSummaryInfo)
        {
            if (!this.AreSameSettings(filterCriteria, sorting, groupCount, groupSummaryInfo, totalSummaryInfo))
            {
                this.UpdateLastSortGroupFilterSettings(filterCriteria, sorting, groupCount, groupSummaryInfo, totalSummaryInfo);
                this.Server.Apply(filterCriteria, sorting, groupCount, groupSummaryInfo, totalSummaryInfo);
                this.Reset();
            }
        }

        private bool AreSameSettings(CriteriaOperator filterCriteria, IList<ServerModeOrderDescriptor[]> sorting, int groupCount, IList<ServerModeSummaryDescriptor> groupSummaryInfo, IList<ServerModeSummaryDescriptor> totalSummaryInfo)
        {
            // Unresolved stack state at '000000F2'
        }

        private bool AreSummaryEquals(IList<ServerModeSummaryDescriptor> first, IList<ServerModeSummaryDescriptor> second)
        {
            if (first == null)
            {
                return (second.Count == 0);
            }
            if (first.Count != second.Count)
            {
                return false;
            }
            for (int i = 0; i < first.Count; i++)
            {
                if ((first[i].SummaryType != second[i].SummaryType) || !Equals(first[i].SummaryExpression, second[i].SummaryExpression))
                {
                    return false;
                }
            }
            return true;
        }

        public void CancelRow(int listSourceIndex)
        {
        }

        private IList<T> CreateList<T>(ICollection<T> collection) => 
            (collection as IList<T>) ?? collection.Return<ICollection<T>, List<T>>((<>c__85<T>.<>9__85_0 ??= x => x.ToList<T>()), (<>c__85<T>.<>9__85_1 ??= () => new List<T>()));

        public virtual void Dispose()
        {
            IDisposable server = this.Server as IDisposable;
            if (server != null)
            {
                server.Dispose();
            }
            this.server.InconsistencyDetected -= new EventHandler<ServerModeInconsistencyDetectedEventArgs>(this.ServerOnInconsistencyDetected);
            this.server.ExceptionThrown -= new EventHandler<ServerModeExceptionThrownEventArgs>(this.ServerOnExceptionThrown);
            this.view = null;
            this.originalServer = null;
        }

        private int FindByValue(CriteriaOperator expression, object value, int startIndex = -1, bool searchUp = false) => 
            this.Server.LocateByValue(expression, value, startIndex, searchUp);

        private int FindIncremental(CriteriaOperator expression, string text, int startRow, bool searchUp, bool ignoreStartRow, bool allowLoop) => 
            this.Server.FindIncremental(expression, text, startRow, searchUp, ignoreStartRow, allowLoop);

        public int FindRowByText(CriteriaOperator expression, string text, int startItemIndex, bool searchNext, bool ignoreStartIndex) => 
            this.FindIncremental(expression, text, startItemIndex, !searchNext, ignoreStartIndex, false);

        public int FindRowByValue(CriteriaOperator expression, object value) => 
            this.FindByValue(expression, value, -1, false);

        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors) => 
            ((ITypedList) this.Server).GetItemProperties(listAccessors);

        public string GetListName(PropertyDescriptor[] listAccessors) => 
            string.Empty;

        public object GetLoadedRowKey(int index) => 
            index;

        internal object GetRow(int index)
        {
            SyncRowInfo rowInfo = this.GetRowInfo(index);
            if (rowInfo == null)
            {
                if ((index < 0) || (index >= this.Count))
                {
                    return AsyncServerModeDataController.NoValue;
                }
                rowInfo = new SyncRowInfo(this.Server.GetRowKey(index), this.Server[index]);
                this.rows[index] = rowInfo;
            }
            return rowInfo.Row;
        }

        private SyncRowInfo GetRowInfo(int index)
        {
            SyncRowInfo info;
            return (!this.rows.TryGetValue(index, out info) ? null : info);
        }

        public void Initialize(IListServerDataView view)
        {
            this.view = view;
            this.server = (IListServer) ((IDXCloneable) this.originalServer).DXClone();
            this.rows = new Dictionary<int, SyncRowInfo>();
            this.server.InconsistencyDetected += new EventHandler<ServerModeInconsistencyDetectedEventArgs>(this.ServerOnInconsistencyDetected);
            this.server.ExceptionThrown += new EventHandler<ServerModeExceptionThrownEventArgs>(this.ServerOnExceptionThrown);
        }

        protected void RaiseListChanged(ListChangedEventArgs e)
        {
            if (this.listChanged != null)
            {
                this.listChanged(this, e);
            }
        }

        public void Refresh()
        {
            this.server.Refresh();
            this.Reset();
        }

        public void Reset()
        {
            this.count = this.Server.Count;
            this.rows.Clear();
            this.view.Reset();
        }

        private void ServerOnExceptionThrown(object sender, ServerModeExceptionThrownEventArgs e)
        {
            this.view.NotifyExceptionThrown(e);
        }

        private void ServerOnInconsistencyDetected(object sender, ServerModeInconsistencyDetectedEventArgs e)
        {
            this.Reset();
            this.view.NotifyInconsistencyDetected(e);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        int IList.Add(object value)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IList.Clear()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        bool IList.Contains(object value)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        int IList.IndexOf(object value)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IList.Insert(int index, object value)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IList.Remove(object value)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IList.RemoveAt(int index)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IBindingList.AddIndex(PropertyDescriptor property)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        object IBindingList.AddNew()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        int IBindingList.Find(PropertyDescriptor property, object key)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IBindingList.RemoveIndex(PropertyDescriptor property)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IBindingList.RemoveSort()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        private void UpdateLastSortGroupFilterSettings(CriteriaOperator filterCriteria, IList<ServerModeOrderDescriptor[]> sorting, int groupCount, IList<ServerModeSummaryDescriptor> groupSummaryInfo, IList<ServerModeSummaryDescriptor> totalSummaryInfo)
        {
            this.lastSorting = sorting;
            this.lastGroupCount = groupCount;
            Func<CriteriaOperator, string> evaluator = <>c.<>9__87_0;
            if (<>c.<>9__87_0 == null)
            {
                Func<CriteriaOperator, string> local1 = <>c.<>9__87_0;
                evaluator = <>c.<>9__87_0 = x => x.ToString();
            }
            this.lastFilterCriteria = filterCriteria.With<CriteriaOperator, string>(evaluator);
            this.lastGroupSummaryInfo = groupSummaryInfo;
            this.lastTotalSummaryInfo = totalSummaryInfo;
        }

        public Dictionary<int, SyncRowInfo> Rows =>
            this.rows;

        public IListServerDataView View =>
            this.view;

        public IListServer Server =>
            this.server;

        object IList.this[int index]
        {
            get => 
                this.GetRow(index);
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public virtual int Count =>
            this.count;

        bool IBindingList.AllowEdit =>
            false;

        bool IBindingList.AllowNew =>
            false;

        bool IBindingList.AllowRemove =>
            false;

        bool IBindingList.IsSorted =>
            false;

        ListSortDirection IBindingList.SortDirection =>
            ListSortDirection.Ascending;

        PropertyDescriptor IBindingList.SortProperty =>
            null;

        bool IBindingList.SupportsChangeNotification =>
            false;

        bool IBindingList.SupportsSearching =>
            false;

        bool IBindingList.SupportsSorting =>
            false;

        bool IList.IsFixedSize =>
            false;

        bool IList.IsReadOnly =>
            true;

        bool ICollection.IsSynchronized =>
            true;

        object ICollection.SyncRoot =>
            this;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SyncListWrapper.<>c <>9 = new SyncListWrapper.<>c();
            public static Func<CriteriaOperator, string> <>9__87_0;
            public static Func<CriteriaOperator, string> <>9__89_0;
            public static Func<IList<ServerModeOrderDescriptor[]>, int> <>9__89_1;
            public static Func<int> <>9__89_2;
            public static Func<IList<ServerModeOrderDescriptor[]>, int> <>9__89_3;
            public static Func<int> <>9__89_4;
            public static Func<ServerModeOrderDescriptor[], int> <>9__89_5;
            public static Func<int> <>9__89_6;
            public static Func<ServerModeOrderDescriptor[], int> <>9__89_7;
            public static Func<int> <>9__89_8;

            internal string <AreSameSettings>b__89_0(CriteriaOperator x) => 
                x.ToString();

            internal int <AreSameSettings>b__89_1(IList<ServerModeOrderDescriptor[]> x) => 
                x.Count;

            internal int <AreSameSettings>b__89_2() => 
                -1;

            internal int <AreSameSettings>b__89_3(IList<ServerModeOrderDescriptor[]> x) => 
                x.Count;

            internal int <AreSameSettings>b__89_4() => 
                -1;

            internal int <AreSameSettings>b__89_5(ServerModeOrderDescriptor[] x) => 
                x.Length;

            internal int <AreSameSettings>b__89_6() => 
                -1;

            internal int <AreSameSettings>b__89_7(ServerModeOrderDescriptor[] x) => 
                x.Length;

            internal int <AreSameSettings>b__89_8() => 
                -1;

            internal string <UpdateLastSortGroupFilterSettings>b__87_0(CriteriaOperator x) => 
                x.ToString();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__85<T>
        {
            public static readonly SyncListWrapper.<>c__85<T> <>9;
            public static Func<ICollection<T>, List<T>> <>9__85_0;
            public static Func<List<T>> <>9__85_1;

            static <>c__85()
            {
                SyncListWrapper.<>c__85<T>.<>9 = new SyncListWrapper.<>c__85<T>();
            }

            internal List<T> <CreateList>b__85_0(ICollection<T> x) => 
                x.ToList<T>();

            internal List<T> <CreateList>b__85_1() => 
                new List<T>();
        }
    }
}

