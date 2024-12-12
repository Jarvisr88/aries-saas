namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Async;
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

    public class AsyncListWrapper : ITypedList, IBindingList, IList, ICollection, IEnumerable, IAsyncResultReceiver, IAsyncCommandVisitor, IDisposable
    {
        private IAsyncListServer originalServer;
        private IAsyncListServer server;
        private int count;
        private AsyncRowsInfo rows;
        private IAsyncListServerDataView view;
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

        public AsyncListWrapper(IAsyncListServer server)
        {
            this.originalServer = server;
            this.State = AsyncState.Invalid;
        }

        public CommandApply ApplySortGroupFilter(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sorting, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo, params DictionaryEntry[] tags) => 
            this.ApplySortGroupFilterToServer(filterCriteria, this.CreateList<ServerModeOrderDescriptor[]>(sorting), groupCount, this.CreateList<ServerModeSummaryDescriptor>(groupSummaryInfo), this.CreateList<ServerModeSummaryDescriptor>(totalSummaryInfo), tags);

        private CommandApply ApplySortGroupFilterToServer(CriteriaOperator filterCriteria, IList<ServerModeOrderDescriptor[]> sorting, int groupCount, IList<ServerModeSummaryDescriptor> groupSummaryInfo, IList<ServerModeSummaryDescriptor> totalSummaryInfo, params DictionaryEntry[] tags)
        {
            if (this.AreSameSettings(filterCriteria, sorting, groupCount, groupSummaryInfo, totalSummaryInfo))
            {
                return null;
            }
            this.UpdateLastSortGroupFilterSettings(filterCriteria, sorting, groupCount, groupSummaryInfo, totalSummaryInfo);
            CommandApply apply = this.Server.Apply(filterCriteria, sorting, groupCount, groupSummaryInfo, totalSummaryInfo, tags);
            this.Reset();
            return apply;
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

        internal void CancelAllGetRows()
        {
            List<KeyValuePair<int, AsyncRowInfo>> loadingRows = this.rows.GetLoadingRows();
            if (loadingRows != null)
            {
                for (int i = 0; i < loadingRows.Count; i++)
                {
                    KeyValuePair<int, AsyncRowInfo> pair = loadingRows[i];
                    if (pair.Value.IsLoading)
                    {
                        this.rows.Remove(pair.Key);
                        this.Server.Cancel(pair.Value.LoadingCommand);
                    }
                }
            }
        }

        public void CancelRow(int listSourceIndex)
        {
            AsyncRowInfo rowInfo = this.GetRowInfo(listSourceIndex);
            if ((rowInfo != null) && rowInfo.IsLoading)
            {
                this.server.Cancel(rowInfo.LoadingCommand);
                this.rows.Remove(listSourceIndex);
            }
        }

        internal void ClearInvalidRowsCache()
        {
            this.rows.CheckRemoveInvalidRows(true);
        }

        private IList<T> CreateList<T>(ICollection<T> collection) => 
            (collection as IList<T>) ?? collection.Return<ICollection<T>, List<T>>((<>c__117<T>.<>9__117_0 ??= x => x.ToList<T>()), (<>c__117<T>.<>9__117_1 ??= () => new List<T>()));

        void IAsyncCommandVisitor.Canceled(Command canceled)
        {
            if (canceled is CommandGetUniqueColumnValues)
            {
                OperationCompleted completedDelegate = AsyncOperationCompletedHelper2.GetCompletedDelegate(canceled);
                if (completedDelegate != null)
                {
                    completedDelegate(null);
                }
            }
        }

        void IAsyncCommandVisitor.Visit(CommandApply result)
        {
            this.State = AsyncState.Valid;
            this.view.NotifyApply();
        }

        void IAsyncCommandVisitor.Visit(CommandFindIncremental result)
        {
            OperationCompleted completedDelegate = AsyncOperationCompletedHelper2.GetCompletedDelegate(result);
            if (completedDelegate != null)
            {
                completedDelegate(result);
            }
        }

        void IAsyncCommandVisitor.Visit(CommandGetAllFilteredAndSortedRows command)
        {
        }

        void IAsyncCommandVisitor.Visit(CommandGetGroupInfo result)
        {
        }

        void IAsyncCommandVisitor.Visit(CommandGetRow result)
        {
            if (this.State == AsyncState.Valid)
            {
                this.rows.CheckRemoveInvalidRows(false);
                int index = result.Index;
                this.rows.OnLoaded(index, result.Row, result.RowKey);
                OperationCompleted completedDelegate = AsyncOperationCompletedHelper2.GetCompletedDelegate(result);
                if (completedDelegate != null)
                {
                    completedDelegate(result);
                }
            }
        }

        void IAsyncCommandVisitor.Visit(CommandGetRowIndexByKey result)
        {
            OperationCompleted completedDelegate = AsyncOperationCompletedHelper2.GetCompletedDelegate(result);
            if (completedDelegate != null)
            {
                completedDelegate(result);
            }
        }

        void IAsyncCommandVisitor.Visit(CommandGetTotals result)
        {
            if (this.count != result.Count)
            {
                this.count = result.Count;
                this.view.NotifyCountChanged();
            }
        }

        void IAsyncCommandVisitor.Visit(CommandGetUniqueColumnValues result)
        {
            OperationCompleted completedDelegate = AsyncOperationCompletedHelper2.GetCompletedDelegate(result);
            if (completedDelegate != null)
            {
                completedDelegate(result.Values);
            }
        }

        void IAsyncCommandVisitor.Visit(CommandLocateByValue result)
        {
            OperationCompleted completedDelegate = AsyncOperationCompletedHelper2.GetCompletedDelegate(result);
            if (completedDelegate != null)
            {
                completedDelegate(result);
            }
        }

        void IAsyncCommandVisitor.Visit(CommandPrefetchRows command)
        {
        }

        void IAsyncCommandVisitor.Visit(CommandRefresh result)
        {
        }

        void IAsyncResultReceiver.BusyChanged(bool busy)
        {
            this.View.BusyChanged(busy);
        }

        void IAsyncResultReceiver.Notification(NotificationExceptionThrown exception)
        {
            this.View.NotifyExceptionThrown(new ServerModeExceptionThrownEventArgs(exception.Notification));
        }

        void IAsyncResultReceiver.Notification(NotificationInconsistencyDetected notification)
        {
            this.View.NotifyInconsistencyDetected(new ServerModeInconsistencyDetectedEventArgs(notification.Notification));
        }

        void IAsyncResultReceiver.PropertyDescriptorsRenewed()
        {
        }

        void IAsyncResultReceiver.Refreshing(CommandRefresh refreshCommand)
        {
        }

        public virtual void Dispose()
        {
            IDisposable server = this.Server as IDisposable;
            if (server != null)
            {
                server.Dispose();
            }
            this.view = null;
            this.originalServer = null;
            this.SetInvalidState();
        }

        private void FindByValue(CriteriaOperator expression, object value, int startIndex = -1, bool searchUp = false, OperationCompleted completed = null)
        {
            DictionaryEntry[] tags = new DictionaryEntry[] { AsyncOperationCompletedHelper2.GetCommandParameter(completed) };
            this.Server.LocateByValue(expression, value, startIndex, searchUp, tags);
        }

        private void FindIncremental(CriteriaOperator expression, string text, int startRow, bool searchUp, bool ignoreStartRow, bool allowLoop, OperationCompleted completed)
        {
            this.Server.WeakCancel<CommandFindIncremental>();
            DictionaryEntry[] tags = new DictionaryEntry[] { AsyncOperationCompletedHelper2.GetCommandParameter(completed) };
            this.Server.FindIncremental(expression, text, startRow, searchUp, ignoreStartRow, allowLoop, tags);
        }

        public void FindRowByText(CriteriaOperator expression, string text, int startItemIndex, bool searchNext, bool ignoreStartIndex, OperationCompleted completed)
        {
            if (this.IsReady)
            {
                this.FindIncremental(expression, text, startItemIndex, !searchNext, ignoreStartIndex, false, completed);
            }
        }

        public void FindRowByValue(CriteriaOperator expression, object value, OperationCompleted completed)
        {
            if (this.IsReady)
            {
                this.FindByValue(expression, value, -1, false, completed);
            }
        }

        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            ITypedList server = (ITypedList) this.Server;
            this.SetReceiver(this);
            return server.GetItemProperties(listAccessors);
        }

        public string GetListName(PropertyDescriptor[] listAccessors) => 
            string.Empty;

        internal object GetLoadedRowKey(int index)
        {
            AsyncRowInfo rowInfo = this.GetRowInfo(index);
            return rowInfo?.Key;
        }

        internal AsyncRowInfo GetLoadedValidRowInfo(int index)
        {
            AsyncRowInfo rowInfo = this.GetRowInfo(index);
            return (((rowInfo == null) || (!rowInfo.IsLoaded || !rowInfo.IsValid)) ? null : rowInfo);
        }

        internal object GetRow(int index, OperationCompleted completed)
        {
            AsyncRowInfo rowInfo = this.GetRowInfo(index);
            if (rowInfo == null)
            {
                if (this.State != AsyncState.Invalid)
                {
                    if ((index < 0) || (index >= this.Count))
                    {
                        return AsyncServerModeDataController.NoValue;
                    }
                    DictionaryEntry[] tags = new DictionaryEntry[] { AsyncOperationCompletedHelper2.GetCommandParameter(completed) };
                    this.rows.Add(index, new AsyncRowInfo(this.Server.GetRow(index, tags)));
                }
                return AsyncServerModeDataController.NoValue;
            }
            if (rowInfo.IsLoading)
            {
                if (completed != null)
                {
                    AsyncOperationCompletedHelper2.AppendCompletedDelegate(rowInfo.LoadingCommand, completed);
                }
                return ((rowInfo.Row != null) ? rowInfo.Row : AsyncServerModeDataController.NoValue);
            }
            if (this.State != AsyncState.Invalid)
            {
                if (rowInfo.IsValid)
                {
                    return rowInfo.Row;
                }
                DictionaryEntry[] tags = new DictionaryEntry[] { AsyncOperationCompletedHelper2.GetCommandParameter(completed) };
                rowInfo.MakeLoading(this.Server.GetRow(index, tags));
            }
            return rowInfo.Row;
        }

        private AsyncRowInfo GetRowInfo(int index) => 
            (index < this.Count) ? this.rows.GetRow(index) : null;

        internal object GetRowInfo(int index, OperationCompleted completed)
        {
            AsyncRowInfo rowInfo = this.GetLoadedValidRowInfo(index);
            return ((rowInfo == null) ? ((this.GetRow(index, delegate (object a) {
                rowInfo = this.GetLoadedValidRowInfo(index);
                if (completed != null)
                {
                    completed(rowInfo);
                }
            }) != AsyncServerModeDataController.NoValue) ? this.GetLoadedValidRowInfo(index) : AsyncServerModeDataController.NoValue) : rowInfo);
        }

        public void Initialize(IAsyncListServerDataView view)
        {
            this.view = view;
            this.server = (IAsyncListServer) ((IDXCloneable) this.originalServer).DXClone();
            this.rows = new AsyncRowsInfo();
            this.SetInvalidState();
        }

        public virtual void Invalidate()
        {
            if (this.State == AsyncState.Invalid)
            {
                this.State = AsyncState.Requested;
                this.Server.GetTotals(new DictionaryEntry[0]);
            }
        }

        public bool IsRowLoaded(int index)
        {
            if (!this.IsReady)
            {
                return false;
            }
            AsyncRowInfo rowInfo = this.GetRowInfo(index);
            return ((rowInfo != null) && rowInfo.IsLoaded);
        }

        protected void RaiseListChanged(ListChangedEventArgs e)
        {
            if (this.listChanged != null)
            {
                this.listChanged(this, e);
            }
        }

        public void Reset()
        {
            this.SetInvalidState();
            this.Invalidate();
        }

        public virtual void ResetValidate()
        {
            this.State = AsyncState.Invalid;
            this.rows.MakeAllRowsInvalid();
        }

        private void SetInvalidState()
        {
            this.ResetValidate();
        }

        public void SetReceiver(IAsyncResultReceiver receiver)
        {
            this.Server.SetReceiver(receiver);
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
            Func<CriteriaOperator, string> evaluator = <>c.<>9__118_0;
            if (<>c.<>9__118_0 == null)
            {
                Func<CriteriaOperator, string> local1 = <>c.<>9__118_0;
                evaluator = <>c.<>9__118_0 = x => x.ToString();
            }
            this.lastFilterCriteria = filterCriteria.With<CriteriaOperator, string>(evaluator);
            this.lastGroupSummaryInfo = groupSummaryInfo;
            this.lastTotalSummaryInfo = totalSummaryInfo;
        }

        public AsyncRowsInfo Rows =>
            this.rows;

        public AsyncState State { get; set; }

        public bool IsReady =>
            this.State == AsyncState.Valid;

        public IAsyncListServerDataView View =>
            this.view;

        public IAsyncListServer Server =>
            this.server;

        object IList.this[int index]
        {
            get => 
                this.GetRow(index, null);
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public virtual int Count
        {
            get
            {
                this.Invalidate();
                return this.count;
            }
        }

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
            public static readonly DevExpress.Xpf.Editors.Helpers.AsyncListWrapper.<>c <>9 = new DevExpress.Xpf.Editors.Helpers.AsyncListWrapper.<>c();
            public static Func<CriteriaOperator, string> <>9__118_0;
            public static Func<CriteriaOperator, string> <>9__120_0;
            public static Func<IList<ServerModeOrderDescriptor[]>, int> <>9__120_1;
            public static Func<int> <>9__120_2;
            public static Func<IList<ServerModeOrderDescriptor[]>, int> <>9__120_3;
            public static Func<int> <>9__120_4;
            public static Func<ServerModeOrderDescriptor[], int> <>9__120_5;
            public static Func<int> <>9__120_6;
            public static Func<ServerModeOrderDescriptor[], int> <>9__120_7;
            public static Func<int> <>9__120_8;

            internal string <AreSameSettings>b__120_0(CriteriaOperator x) => 
                x.ToString();

            internal int <AreSameSettings>b__120_1(IList<ServerModeOrderDescriptor[]> x) => 
                x.Count;

            internal int <AreSameSettings>b__120_2() => 
                -1;

            internal int <AreSameSettings>b__120_3(IList<ServerModeOrderDescriptor[]> x) => 
                x.Count;

            internal int <AreSameSettings>b__120_4() => 
                -1;

            internal int <AreSameSettings>b__120_5(ServerModeOrderDescriptor[] x) => 
                x.Length;

            internal int <AreSameSettings>b__120_6() => 
                -1;

            internal int <AreSameSettings>b__120_7(ServerModeOrderDescriptor[] x) => 
                x.Length;

            internal int <AreSameSettings>b__120_8() => 
                -1;

            internal string <UpdateLastSortGroupFilterSettings>b__118_0(CriteriaOperator x) => 
                x.ToString();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__117<T>
        {
            public static readonly DevExpress.Xpf.Editors.Helpers.AsyncListWrapper.<>c__117<T> <>9;
            public static Func<ICollection<T>, List<T>> <>9__117_0;
            public static Func<List<T>> <>9__117_1;

            static <>c__117()
            {
                DevExpress.Xpf.Editors.Helpers.AsyncListWrapper.<>c__117<T>.<>9 = new DevExpress.Xpf.Editors.Helpers.AsyncListWrapper.<>c__117<T>();
            }

            internal List<T> <CreateList>b__117_0(ICollection<T> x) => 
                x.ToList<T>();

            internal List<T> <CreateList>b__117_1() => 
                new List<T>();
        }
    }
}

