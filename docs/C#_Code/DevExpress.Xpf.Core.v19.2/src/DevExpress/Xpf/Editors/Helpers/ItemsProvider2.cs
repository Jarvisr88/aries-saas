namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Linq;
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class ItemsProvider2 : IItemsProvider2, IItemsProviderCollectionViewSupport
    {
        public event ItemsProviderChangedEventHandler ItemsProviderChanged;

        public ItemsProvider2(IItemsProviderOwner owner)
        {
            this.Owner = owner;
            this.DataController = this.CreateDataController();
            this.DataController.ListChanged += new ListChangedEventHandler(this.DataControllerOnListChanged);
            this.DataController.Refreshed += new EventHandler(this.DataControllerRefreshed);
            this.DataController.RowLoaded += new EventHandler<ItemsProviderRowLoadedEventArgs>(this.DataControllerRowLoaded);
            this.DataController.CurrentChanged += new EventHandler<ItemsProviderCurrentChangedEventArgs>(this.DataControllerCurrentChanged);
            this.DataController.BusyChanged += new EventHandler<ItemsProviderOnBusyChangedEventArgs>(this.DataControllerBusyChanged);
            this.DataController.ViewRefreshed += new EventHandler<ItemsProviderViewRefreshedEventArgs>(this.DataControllerViewRefreshed);
            this.DataController.FindIncrementalCompleted += new EventHandler<ItemsProviderFindIncrementalCompletedEventArgs>(this.DataControllerFindIncrementalCompleted);
        }

        public void BeginInit()
        {
            this.DataController.BeginInit();
        }

        private CriteriaOperator CalcActualFilterCriteria() => 
            CriteriaOperator.Parse(this.DataController.GetSnapshot(this.DataController.CurrentDataViewHandle).FilterCriteria, new object[0]);

        public void CancelAsyncOperations(object handle)
        {
            this.DataController.CancelAsyncOperations(handle);
        }

        protected virtual DevExpress.Xpf.Editors.Helpers.DataController CreateDataController() => 
            new DevExpress.Xpf.Editors.Helpers.DataController(this.Owner);

        public CriteriaOperator CreateDisplayFilterCriteria(string searchText, FilterCondition filterCondition)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return null;
            }
            CriteriaOperator[] operands = new CriteriaOperator[] { new OperandProperty(this.DataController.GetDisplayPropertyName(null)), searchText };
            return new FunctionOperator(this.GetFunctionOperatorType(filterCondition), operands);
        }

        private void DataControllerBusyChanged(object sender, ItemsProviderOnBusyChangedEventArgs e)
        {
            this.RaiseBusyChanged(e);
        }

        private void DataControllerCurrentChanged(object sender, ItemsProviderCurrentChangedEventArgs e)
        {
            this.RaiseCurrentChanged(e.CurrentItem);
        }

        private void DataControllerFindIncrementalCompleted(object sender, ItemsProviderFindIncrementalCompletedEventArgs e)
        {
            this.RaiseFindIncrementalCompleted(e);
        }

        private void DataControllerOnListChanged(object sender, ListChangedEventArgs e)
        {
            ListChangedType listChangedType = e.ListChangedType;
            this.RaiseDataChanged(listChangedType, e.NewIndex, e.PropertyDescriptor);
        }

        private void DataControllerRefreshed(object sender, EventArgs e)
        {
            this.Refreshed();
        }

        private void DataControllerRowLoaded(object sender, ItemsProviderRowLoadedEventArgs e)
        {
            this.RaiseRowLoaded(e);
        }

        private void DataControllerViewRefreshed(object sender, ItemsProviderViewRefreshedEventArgs e)
        {
            this.RaiseViewRefreshed(e);
        }

        public void DestroyVisibleListSource(object handle)
        {
            this.DataController.DestroyVisibleList(handle);
        }

        void IItemsProviderCollectionViewSupport.RaiseCurrentChanged(object currentItem)
        {
        }

        public void DoRefresh()
        {
            this.DataController.Refresh();
        }

        public void EndInit()
        {
            this.DataController.EndInit();
        }

        public int FindItemIndexByText(string text, bool isCaseSensitiveSearch, bool allowTextInputSuggestions, object handle, int startItemIndex = -1, bool searchNext = true, bool ignoreStartIndex = false) => 
            this.DataController.FindItemIndexByText(text, isCaseSensitiveSearch, allowTextInputSuggestions, handle, startItemIndex, searchNext, ignoreStartIndex);

        public int GetControllerIndexByIndex(int index, object handle = null) => 
            this.DataController.GetVisibleIndexByListSourceIndex(index, handle);

        public int GetCount(object handle) => 
            this.DataController.GetVisibleRowCount(handle);

        public string GetDisplayPropertyName(object handle) => 
            this.DataController.GetDisplayPropertyName(handle);

        public object GetDisplayValueByEditValue(object editValue, object handle = null)
        {
            int listSourceIndex = this.DataController.IndexOfValue(editValue, handle);
            return ((listSourceIndex >= 0) ? this.DataController.GetDisplayValueByListSourceIndex(listSourceIndex, handle) : (this.IsInLookUpMode ? null : editValue));
        }

        public object GetDisplayValueByIndex(int index, object handle) => 
            (index >= 0) ? this.DataController.GetDisplayValueByListSourceIndex(index, handle) : null;

        protected virtual FunctionOperatorType GetFunctionOperatorType(FilterCondition filterCondition) => 
            (filterCondition == FilterCondition.Contains) ? FunctionOperatorType.Contains : FunctionOperatorType.StartsWith;

        public int GetIndexByControllerIndex(int controllerIndex, object handle = null) => 
            this.DataController.GetListSourceIndexByVisibleIndex(controllerIndex, handle);

        public int GetIndexByItem(object item, object handle = null) => 
            this.DataController.IndexOf(item, handle);

        public object GetItem(object value, object handle = null)
        {
            int index = this.IndexOfValue(value, handle);
            return ((index < 0) ? null : this.DataController.GetItemByListSourceIndex(index, handle));
        }

        public object GetItemByControllerIndex(int visibleIndex, object handle = null)
        {
            int listSourceIndexByVisibleIndex = this.DataController.GetListSourceIndexByVisibleIndex(visibleIndex, handle);
            return ((listSourceIndexByVisibleIndex < 0) ? null : this.DataController.GetItemByListSourceIndex(listSourceIndexByVisibleIndex, handle));
        }

        public object GetValueByIndex(int index, object handle = null) => 
            (index >= 0) ? this.DataController.GetValueByListSourceIndex(index, handle) : null;

        public object GetValueByRowKey(object rowKey, object handle)
        {
            int listSourceIndex = this.IsServerMode ? (string.IsNullOrEmpty(this.Owner.ValueMember) ? this.DataController.IndexOf(rowKey, handle) : this.DataController.IndexOfValue(rowKey, handle)) : this.DataController.IndexOf(rowKey, handle);
            return this.DataController.GetValueByListSourceIndex(listSourceIndex, handle);
        }

        public object GetValueFromItem(object item, object handle = null) => 
            this.DataController.GetValue(item, !this.IsInLookUpMode, handle);

        public string GetValuePropertyName(object handle) => 
            this.DataController.GetValuePropertyName(handle);

        public IEnumerable GetVisibleListSource(object handle) => 
            this.DataController.GetVisibleList(handle);

        public int IndexOfValue(object value, object handle = null) => 
            this.DataController.IndexOfValue(value, handle);

        public void ProcessCollectionChanged(NotifyItemsProviderChangedEventArgs e)
        {
            if (e.ChangedType != ListChangedType.ItemChanged)
            {
                this.Reset();
            }
            else
            {
                this.ProcessItemChanged(e);
                this.RaiseDataChanged(ListChangedType.ItemChanged, -1, null);
            }
        }

        private void ProcessItemChanged(NotifyItemsProviderChangedEventArgs e)
        {
            int index = this.DataController.IndexOf(e.Item, null);
            if (index >= 0)
            {
                this.DataController.ProcessChangeItem(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
                this.ResetDisplayTextCache();
            }
        }

        public void ProcessSelectionChanged(NotifyItemsProviderSelectionChangedEventArgs e)
        {
            this.RaiseSelectionChanged(this.GetValueFromItem(e.Item, null), e.IsSelected);
        }

        public void RaiseBusyChanged(ItemsProviderOnBusyChangedEventArgs args)
        {
            Guard.ArgumentNotNull(args, "args");
            if (this.ItemsProviderChanged != null)
            {
                this.ItemsProviderChanged(this, args);
            }
        }

        public void RaiseCurrentChanged(object currentItem)
        {
            if (this.ItemsProviderChanged != null)
            {
                this.ItemsProviderChanged(this, new ItemsProviderCurrentChangedEventArgs(currentItem));
            }
        }

        public void RaiseDataChanged(ItemsProviderDataChangedEventArgs args)
        {
            Guard.ArgumentNotNull(args, "args");
            if (this.ItemsProviderChanged != null)
            {
                this.ItemsProviderChanged(this, args);
            }
        }

        public void RaiseDataChanged(ListChangedType changedType = 0, int newIndex = -1, PropertyDescriptor descriptor = null)
        {
            this.RaiseDataChanged(new ItemsProviderDataChangedEventArgs(changedType, newIndex, descriptor));
        }

        private void RaiseFindIncrementalCompleted(ItemsProviderFindIncrementalCompletedEventArgs args)
        {
            Guard.ArgumentNotNull(args, "args");
            if (this.ItemsProviderChanged != null)
            {
                this.ItemsProviderChanged(this, args);
            }
        }

        private void RaiseRefreshed()
        {
            if (this.ItemsProviderChanged != null)
            {
                this.ItemsProviderChanged(this, ItemsProviderRefreshedEventArgs.Instance);
            }
        }

        public void RaiseRowLoaded(ItemsProviderRowLoadedEventArgs args)
        {
            Guard.ArgumentNotNull(args, "args");
            if (this.ItemsProviderChanged != null)
            {
                this.ItemsProviderChanged(this, args);
            }
        }

        public void RaiseSelectionChanged(object editValue, bool isSelected)
        {
            if (this.ItemsProviderChanged != null)
            {
                this.ItemsProviderChanged(this, new ItemsProviderSelectionChangedEventArgs(editValue, isSelected));
            }
        }

        private void RaiseViewRefreshed(ItemsProviderViewRefreshedEventArgs args)
        {
            Guard.ArgumentNotNull(args, "args");
            if (this.ItemsProviderChanged != null)
            {
                this.ItemsProviderChanged(this, args);
            }
        }

        private void Refreshed()
        {
            this.RaiseRefreshed();
        }

        public void RegisterSnapshot(object handle)
        {
            this.DataController.RegisterSnapshot(new DataControllerSnapshotDescriptor(handle));
        }

        public void ReleaseSnapshot(object handle)
        {
            DataControllerSnapshotDescriptor snapshot = this.DataController.GetSnapshot(handle);
            if (snapshot != null)
            {
                this.DataController.ReleaseSnapshot(snapshot);
            }
        }

        public void Reset()
        {
            this.DataController.Reset();
            this.UpdateIsInLookUpMode();
        }

        public void ResetDisplayTextCache()
        {
            this.DataController.ResetDisplayTextCache();
        }

        public void ResetVisibleList(object handle)
        {
            this.DataController.ResetVisibleList(handle);
        }

        public void SetCurrentItem(object currentItem)
        {
            this.CollectionViewSupport.Do<IItemsProviderCollectionViewSupport>(x => x.SetCurrentItem(currentItem));
        }

        public void SetDisplayFilterCriteria(CriteriaOperator criteria, object handle)
        {
            DataControllerSnapshotDescriptor snapshot = this.DataController.GetSnapshot(handle);
            if (snapshot != null)
            {
                snapshot.SetDisplayFilterCriteria(criteria);
            }
        }

        public void SetFilterCriteria(CriteriaOperator criteria, object handle)
        {
            DataControllerSnapshotDescriptor snapshot = this.DataController.GetSnapshot(handle);
            if (snapshot != null)
            {
                snapshot.SetFilterCriteria(criteria);
            }
        }

        public void SyncWithCurrentItem()
        {
            Action<IItemsProviderCollectionViewSupport> action = <>c.<>9__100_0;
            if (<>c.<>9__100_0 == null)
            {
                Action<IItemsProviderCollectionViewSupport> local1 = <>c.<>9__100_0;
                action = <>c.<>9__100_0 = x => x.SyncWithCurrentItem();
            }
            this.CollectionViewSupport.Do<IItemsProviderCollectionViewSupport>(action);
        }

        public void UpdateDisplayMember()
        {
            this.Reset();
        }

        public void UpdateFilterCriteria()
        {
            Func<CriteriaOperator, string> evaluator = <>c.<>9__79_0;
            if (<>c.<>9__79_0 == null)
            {
                Func<CriteriaOperator, string> local1 = <>c.<>9__79_0;
                evaluator = <>c.<>9__79_0 = x => x.ToString();
            }
            this.DataController.SetFilterCriteria(this.Owner.FilterCriteria.With<CriteriaOperator, string>(evaluator));
        }

        public void UpdateIsCaseSensitiveFilter()
        {
            this.Reset();
        }

        private void UpdateIsInLookUpMode()
        {
            this.IsInLookUpMode = this.Owner.IsInLookUpMode;
        }

        public void UpdateItemsSource()
        {
            this.DataController.UpdateItemsSource();
            this.UpdateIsInLookUpMode();
        }

        public void UpdateValueMember()
        {
            this.Reset();
        }

        public IEnumerable VisibleList =>
            (IEnumerable) this.DataController.VisibleList;

        public DevExpress.Xpf.Editors.Helpers.DataController DataController { get; private set; }

        protected IItemsProviderOwner Owner { get; private set; }

        public object CurrentDataViewHandle =>
            this.DataController.CurrentDataViewHandle;

        private IItemsProviderCollectionViewSupport CollectionViewSupport =>
            this.DataController.DefaultDataView as IItemsProviderCollectionViewSupport;

        public int Count =>
            this.DataController.CurrentDataView.VisibleRowCount;

        public IEnumerable<string> AvailableColumns =>
            this.DataController.DefaultDataView.AvailableColumns;

        public CriteriaOperator ActualFilterCriteria =>
            this.CalcActualFilterCriteria();

        public bool IsAsyncServerMode =>
            this.DataController.IsAsyncServerMode;

        public bool IsBusy =>
            this.DataController.IsBusy;

        public bool IsSyncServerMode =>
            this.DataController.IsSyncServerMode;

        public bool IsServerMode =>
            this.IsSyncServerMode || this.IsAsyncServerMode;

        public bool NeedsRefresh =>
            this.DataController.NeedsRefresh;

        public bool IsInLookUpMode { get; private set; }

        public bool HasValueMember =>
            !string.IsNullOrEmpty(this.Owner.ValueMember);

        public IEnumerable VisibleListSource =>
            this.VisibleList;

        public object this[int index] =>
            this.GetItemByControllerIndex(index, null);

        ICollectionViewHelper IItemsProviderCollectionViewSupport.DataSync =>
            null;

        ICollectionView IItemsProviderCollectionViewSupport.ListSource =>
            null;

        bool IItemsProviderCollectionViewSupport.IsSynchronizedWithCurrentItem =>
            false;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ItemsProvider2.<>c <>9 = new ItemsProvider2.<>c();
            public static Func<CriteriaOperator, string> <>9__79_0;
            public static Action<IItemsProviderCollectionViewSupport> <>9__100_0;

            internal void <SyncWithCurrentItem>b__100_0(IItemsProviderCollectionViewSupport x)
            {
                x.SyncWithCurrentItem();
            }

            internal string <UpdateFilterCriteria>b__79_0(CriteriaOperator x) => 
                x.ToString();
        }
    }
}

