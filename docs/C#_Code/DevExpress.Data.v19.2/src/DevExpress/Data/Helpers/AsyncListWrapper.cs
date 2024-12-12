namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Async;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class AsyncListWrapper : IBindingList, IList, ICollection, IEnumerable, ITypedList, IAsyncResultReceiver, IAsyncCommandVisitor, IDisposable
    {
        private IAsyncListServer server;
        private int count;
        private AsyncRowsInfo rows;
        private AsyncState state;
        private AsyncServerModeDataController controller;
        private int lastCount;
        private bool? isGroupExpandInProgress;

        event ListChangedEventHandler IBindingList.ListChanged;

        public AsyncListWrapper(AsyncServerModeDataController controller, IAsyncListServer server);
        internal void ApplySort();
        internal void CancelAllGetRows();
        internal void ClearInvalidRowsCache();
        void IAsyncCommandVisitor.Canceled(Command canceled);
        void IAsyncCommandVisitor.Visit(CommandApply result);
        void IAsyncCommandVisitor.Visit(CommandFindIncremental result);
        void IAsyncCommandVisitor.Visit(CommandGetAllFilteredAndSortedRows command);
        void IAsyncCommandVisitor.Visit(CommandGetGroupInfo result);
        void IAsyncCommandVisitor.Visit(CommandGetRow result);
        void IAsyncCommandVisitor.Visit(CommandGetRowIndexByKey result);
        void IAsyncCommandVisitor.Visit(CommandGetTotals result);
        void IAsyncCommandVisitor.Visit(CommandGetUniqueColumnValues result);
        void IAsyncCommandVisitor.Visit(CommandLocateByValue result);
        void IAsyncCommandVisitor.Visit(CommandPrefetchRows command);
        void IAsyncCommandVisitor.Visit(CommandRefresh result);
        void IAsyncResultReceiver.BusyChanged(bool busy);
        void IAsyncResultReceiver.Notification(NotificationExceptionThrown exception);
        void IAsyncResultReceiver.Notification(NotificationInconsistencyDetected notification);
        void IAsyncResultReceiver.PropertyDescriptorsRenewed();
        void IAsyncResultReceiver.Refreshing(CommandRefresh refreshCommand);
        public virtual void Dispose();
        internal static string ExtractErrorText(Exception e);
        internal void FindIncremental(CriteriaOperator expression, string text, int startRow, bool searchUp, bool ignoreStartRow, bool allowLoop, OperationCompleted completed);
        internal int FindRowByValue(DataColumnInfo colInfo, object value);
        internal object GetLoadedRowKey(int index);
        internal AsyncRowInfo GetLoadedValidRowInfo(int index);
        internal object GetRow(int index, OperationCompleted completed);
        private AsyncRowInfo GetRowInfo(int index);
        internal object GetRowInfo(int index, OperationCompleted completed);
        public virtual void Invalidate();
        public bool IsRowLoaded(int index);
        protected virtual void NotifyRowReceived(int rowIndex);
        public virtual void ResetValidate();
        private void SetInvalidState();
        void ICollection.CopyTo(Array array, int index);
        IEnumerator IEnumerable.GetEnumerator();
        int IList.Add(object value);
        void IList.Clear();
        bool IList.Contains(object value);
        int IList.IndexOf(object value);
        void IList.Insert(int index, object value);
        void IList.Remove(object value);
        void IList.RemoveAt(int index);
        void IBindingList.AddIndex(PropertyDescriptor property);
        object IBindingList.AddNew();
        void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction);
        int IBindingList.Find(PropertyDescriptor property, object key);
        void IBindingList.RemoveIndex(PropertyDescriptor property);
        void IBindingList.RemoveSort();
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors);
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors);

        public AsyncServerModeDataController Controller { get; }

        public IAsyncListServer Server { get; }

        public virtual bool IsValidState { get; }

        object IList.this[int index] { get; set; }

        public virtual int Count { get; }

        protected IDataControllerVisualClient VisualClient { get; }

        bool IBindingList.AllowEdit { get; }

        bool IBindingList.AllowNew { get; }

        bool IBindingList.AllowRemove { get; }

        bool IBindingList.IsSorted { get; }

        ListSortDirection IBindingList.SortDirection { get; }

        PropertyDescriptor IBindingList.SortProperty { get; }

        bool IBindingList.SupportsChangeNotification { get; }

        bool IBindingList.SupportsSearching { get; }

        bool IBindingList.SupportsSorting { get; }

        bool IList.IsFixedSize { get; }

        bool IList.IsReadOnly { get; }

        bool ICollection.IsSynchronized { get; }

        object ICollection.SyncRoot { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AsyncListWrapper.<>c <>9;
            public static Func<Exception, string> <>9__92_0;

            static <>c();
            internal string <ExtractErrorText>b__92_0(Exception ie);
        }
    }
}

