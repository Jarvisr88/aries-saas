namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class VirtualServerModeCore : IVirtualListServerWithTotalSummary, IVirtualListServer, IList, ICollection, IEnumerable, IBindingList, ITypedList, IXtraBusySupport, IXtraMoreRows, IXtraSourceError, IXtraGetUniqueValues, IXtraRefreshable, IColumnsServerActions
    {
        private IBindingList _ActiveBindingList;
        private IList _ActiveActualList;
        private Action DeactivateAction;
        private IList _PendingList;
        private bool _MoreRowsAvailable;
        private bool _PendingMoreRowsAvailable;
        private int _CanForwardListChanged;
        private int _insideApply;
        private int _busy;

        public event EventHandler<EventArgs> CancelGetUniqueValuesRequested;

        public event EventHandler<VirtualServerModeCanPerformColumnServerActionEventArgs> CanPerformColumnServerAction;

        public event EventHandler<VirtualServerModeConfigurationChangedEventArgs> ConfigurationChanged;

        public event EventHandler<ErrorEventArgs> ErrorOccurred;

        public event EventHandler<GetUniqueValuesEventArgs> GetUniqueValuesRequested;

        public event EventHandler IsBusyChanged;

        public event ListChangedEventHandler ListChanged;

        public event EventHandler MoreRowsRequested;

        public event EventHandler RefreshRequested;

        public event EventHandler RowsLoaded;

        public event EventHandler<VirtualServerModeTotalSummaryReadyEventArgs> TotalSummaryReady;

        public event EventHandler<UniqueValuesReadyEventArgs> UniqueValuesReady;

        public VirtualServerModeCore();
        public VirtualServerModeCore(Type t);
        private void ActivateAndNotify(IList list, bool moreRowsAvailable, bool forceReset, bool propsAreCompatible);
        private void ActivateCore(IList list, bool moreRowsAvailable);
        private void ActivatePending(bool isReset, bool propsAreCompatible);
        public int Add(object value);
        public void AddIndex(PropertyDescriptor property);
        public object AddNew();
        public void ApplySort(PropertyDescriptor property, ListSortDirection direction);
        private static bool AreCompatiblePropsDefault(IList list1, IList list2);
        public void AssignList(IList newDataSource, bool moreRowsAvailable, bool isReset);
        private void BindingListListChanged(object sender, ListChangedEventArgs e);
        public void BusyOff();
        public void BusyOn();
        public bool CanCalculateTotalSummary(ServerModeSummaryDescriptor totalSummary);
        private void CheckCanAcceptSourceModification();
        public void Clear();
        public bool Contains(object value);
        public void CopyTo(Array array, int index);
        bool IColumnsServerActions.AllowAction(string fieldName, ColumnServerActionType action);
        void IXtraGetUniqueValues.CancelGetUniqueValues();
        void IXtraGetUniqueValues.GetUniqueValues(GetUniqueValuesEventArgs e);
        void IXtraMoreRows.MoreRows();
        void IVirtualListServer.ChangeConfiguration(VirtualServerModeConfigurationInfo configuration);
        public int Find(PropertyDescriptor property, object key);
        public IEnumerator GetEnumerator();
        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors);
        public string GetListName(PropertyDescriptor[] listAccessors);
        public int IndexOf(object value);
        public void Insert(int index, object value);
        public void ListChangedForwardingDisable();
        public void ListChangedForwardingEnable();
        public void NotifyError(Exception ex);
        public void NotifyRowsLoaded();
        public virtual void NotifyTotalSummaryReady(VirtualServerModeTotalSummaryReadyEventArgs e);
        public virtual void NotifyUniqueValuesReady(UniqueValuesReadyEventArgs e);
        protected virtual void RaiseCancelGetUniqueValuesRequested(EventArgs e);
        protected virtual void RaiseCanPerformColumnServerAction(VirtualServerModeCanPerformColumnServerActionEventArgs e);
        protected virtual void RaiseConfigurationChanged(VirtualServerModeConfigurationChangedEventArgs configuration);
        protected virtual void RaiseErrorOccurred(ErrorEventArgs e);
        protected virtual void RaiseGetUniqueValuesRequested(GetUniqueValuesEventArgs e);
        protected virtual void RaiseIsBusyChanged();
        protected virtual void RaiseListChanged(ListChangedEventArgs e);
        protected virtual void RaiseMoreRowsRequested();
        protected virtual void RaiseRefreshRequested();
        protected virtual void RaiseRowsLoaded(EventArgs e);
        public void Refresh();
        public void Remove(object value);
        public void RemoveAt(int index);
        public void RemoveIndex(PropertyDescriptor property);
        public void RemoveSort();

        public bool CanForwardListChanged { get; }

        public IBindingList ActiveBindingList { get; }

        public IList ActiveActualList { get; }

        protected bool InsideApply { get; }

        public bool IsFixedSize { get; }

        public bool IsReadOnly { get; }

        public object this[int index] { get; set; }

        public int Count { get; }

        public bool IsSynchronized { get; }

        public object SyncRoot { get; }

        public bool AllowEdit { get; }

        public bool AllowNew { get; }

        public bool AllowRemove { get; }

        public bool IsSorted { get; }

        public ListSortDirection SortDirection { get; }

        public PropertyDescriptor SortProperty { get; }

        public bool SupportsChangeNotification { get; }

        public bool SupportsSearching { get; }

        public bool SupportsSorting { get; }

        public bool IsBusy { get; }

        public bool IsBusySupported { get; }

        public bool IsMoreRowsSupported { get; }

        public bool IsMoreRowsAvailable { get; set; }

        public bool RefreshSupported { get; }

        public bool TotalSummarySupported { get; set; }

        public bool UniqueValuesSupported { get; set; }
    }
}

