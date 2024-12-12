namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Async;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Linq;
    using DevExpress.Data.Utils;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Data;
    using System.Windows.Threading;

    public class DataController
    {
        private bool snapshotsDestroyed;
        public readonly object CurrentDataViewHandle = new object();
        public readonly object DefaultDataViewHandle = new object();
        private readonly Locker endInitLocker = new Locker();
        private readonly HashSet<object> busyHandlers = new HashSet<object>();
        private readonly Locker findIncrementalLocker = new Locker();
        private readonly Dictionary<object, DataControllerSnapshotDescriptor> Snapshots = new Dictionary<object, DataControllerSnapshotDescriptor>();
        internal readonly Dictionary<object, DevExpress.Xpf.Editors.Helpers.CurrentDataView> Views = new Dictionary<object, DevExpress.Xpf.Editors.Helpers.CurrentDataView>();

        public event EventHandler<ItemsProviderOnBusyChangedEventArgs> BusyChanged;

        public event EventHandler<ItemsProviderCurrentChangedEventArgs> CurrentChanged;

        public event EventHandler<ItemsProviderFindIncrementalCompletedEventArgs> FindIncrementalCompleted;

        public event ListChangedEventHandler ListChanged;

        public event EventHandler Refreshed;

        public event EventHandler<ItemsProviderRowLoadedEventArgs> RowLoaded;

        public event EventHandler<ItemsProviderViewRefreshedEventArgs> ViewRefreshed;

        public DataController(IItemsProviderOwner owner)
        {
            this.Owner = owner;
            this.EndInitPostponedAction = new PostponedAction(() => this.endInitLocker.IsLocked);
            Action<DevExpress.Xpf.Editors.Helpers.DataController, object, ListChangedEventArgs> onEventAction = <>c.<>9__60_1;
            if (<>c.<>9__60_1 == null)
            {
                Action<DevExpress.Xpf.Editors.Helpers.DataController, object, ListChangedEventArgs> local1 = <>c.<>9__60_1;
                onEventAction = <>c.<>9__60_1 = (controller, o, e) => controller.ItemsSourceListChanged(o, e);
            }
            this.ListChangedHandler = new ListChangedWeakEventHandler<DevExpress.Xpf.Editors.Helpers.DataController>(this, onEventAction);
            this.InitializeCurrentDataView();
            this.UpdateItemsSource();
        }

        public virtual void BeginInit()
        {
            if (this.busyHandlers.Any<object>())
            {
                this.busyHandlers.Clear();
                this.RaiseOnBusyChanged(new ItemsProviderOnBusyChangedEventArgs(false));
            }
            this.endInitLocker.Lock();
        }

        private string CalcActualFilterString(string filterCriteria, string displayFilterCriteria)
        {
            bool flag2 = !string.IsNullOrEmpty(displayFilterCriteria);
            return (string.IsNullOrEmpty(filterCriteria) ? (flag2 ? displayFilterCriteria : string.Empty) : (flag2 ? CriteriaOperator.And(CriteriaOperator.Parse(filterCriteria, new object[0]), CriteriaOperator.Parse(displayFilterCriteria, new object[0])).ToString() : filterCriteria));
        }

        public void CancelAsyncOperations(object handle)
        {
            object currentDataViewHandle = handle;
            if (handle == null)
            {
                object local1 = handle;
                currentDataViewHandle = this.CurrentDataViewHandle;
            }
            Action<DevExpress.Xpf.Editors.Helpers.CurrentDataView> action = <>c.<>9__143_0;
            if (<>c.<>9__143_0 == null)
            {
                Action<DevExpress.Xpf.Editors.Helpers.CurrentDataView> local2 = <>c.<>9__143_0;
                action = <>c.<>9__143_0 = x => x.CancelAsyncOperations();
            }
            this.Views[currentDataViewHandle].Do<DevExpress.Xpf.Editors.Helpers.CurrentDataView>(action);
        }

        private void CollectionViewCurrentChanged(object sender, ItemsProviderCurrentChangedEventArgs e)
        {
            this.RaiseCurrentChanged(e);
        }

        private DevExpress.Xpf.Editors.Helpers.DefaultDataView CreateDefaultDataView()
        {
            Func<IList, bool> evaluator = <>c.<>9__115_0;
            if (<>c.<>9__115_0 == null)
            {
                Func<IList, bool> local1 = <>c.<>9__115_0;
                evaluator = <>c.<>9__115_0 = x => (x is IListServer) || (x is IAsyncListServer);
            }
            return this.ListSource.If<IList>(evaluator).Return<IList, DevExpress.Xpf.Editors.Helpers.DefaultDataView>(x => this.CreateServerModeDefaultDataView(), new Func<DevExpress.Xpf.Editors.Helpers.DefaultDataView>(this.CreateLocalDefaultDataView));
        }

        private PlainListDataView CreateLocalDefaultDataView()
        {
            Func<string, string> evaluator = <>c.<>9__116_0;
            if (<>c.<>9__116_0 == null)
            {
                Func<string, string> local1 = <>c.<>9__116_0;
                evaluator = <>c.<>9__116_0 = x => x.ToString();
            }
            return new PlainListDataView(this.Owner.SelectItemWithNullValue, this.ListSource, this.Owner.ValueMember, this.Owner.DisplayMember, Enumerable.Empty<GroupingInfo>(), Enumerable.Empty<SortingInfo>(), this.FilterCriteria.With<string, string>(evaluator), true);
        }

        protected virtual DevExpress.Xpf.Editors.Helpers.DefaultDataView CreateServerModeDefaultDataView()
        {
            object listSource = this.ListSource;
            if (listSource is ICollectionViewHelper)
            {
                Func<string, string> func1 = <>c.<>9__117_1;
                if (<>c.<>9__117_1 == null)
                {
                    Func<string, string> local1 = <>c.<>9__117_1;
                    func1 = <>c.<>9__117_1 = fc => fc.ToString();
                }
                return new CollectionViewDefaultDataView(this.Owner.SelectItemWithNullValue, this.Owner.AllowCollectionView, () => this.Owner.IsSynchronizedWithCurrentItem, (IListServer) listSource, this.Owner.ValueMember, this.Owner.DisplayMember, Enumerable.Empty<GroupingInfo>(), Enumerable.Empty<SortingInfo>(), this.FilterCriteria.With<string, string>(func1));
            }
            if (listSource is IAsyncListServer)
            {
                Func<string, string> func2 = <>c.<>9__117_2;
                if (<>c.<>9__117_2 == null)
                {
                    Func<string, string> local2 = <>c.<>9__117_2;
                    func2 = <>c.<>9__117_2 = x => x.ToString();
                }
                return new AsyncListServerDefaultDataView(this.Owner.SelectItemWithNullValue, (IAsyncListServer) listSource, this.Owner.ValueMember, this.Owner.DisplayMember, Enumerable.Empty<GroupingInfo>(), Enumerable.Empty<SortingInfo>(), this.FilterCriteria.With<string, string>(func2));
            }
            if (listSource is IListServer)
            {
                Func<string, string> func3 = <>c.<>9__117_3;
                if (<>c.<>9__117_3 == null)
                {
                    Func<string, string> local3 = <>c.<>9__117_3;
                    func3 = <>c.<>9__117_3 = x => x.ToString();
                }
                return new SyncListServerDefaultDataView(this.Owner.SelectItemWithNullValue, (IListServer) listSource, this.Owner.ValueMember, this.Owner.DisplayMember, Enumerable.Empty<GroupingInfo>(), Enumerable.Empty<SortingInfo>(), this.FilterCriteria.With<string, string>(func3));
            }
            Func<string, string> evaluator = <>c.<>9__117_4;
            if (<>c.<>9__117_4 == null)
            {
                Func<string, string> local4 = <>c.<>9__117_4;
                evaluator = <>c.<>9__117_4 = fc => fc.ToString();
            }
            return new ListServerDataView(false, (IListServer) listSource, this.Owner.ValueMember, this.Owner.DisplayMember, Enumerable.Empty<GroupingInfo>(), Enumerable.Empty<SortingInfo>(), this.FilterCriteria.With<string, string>(evaluator));
        }

        private void DefaultViewFindIncrementalCompleted(object sender, ItemsProviderChangedEventArgs e)
        {
            this.ProcessFindIncrementalCompletedForSnapshots(e);
            this.RaiseFindIncrementalCompleted(this, (ItemsProviderFindIncrementalCompletedEventArgs) e);
        }

        private void DefaultViewInconsistencyDetected(object sender, EventArgs e)
        {
            if (!this.endInitLocker.IsLocked)
            {
                this.ProcessInconsistencyDetectedForSnapshots();
                this.RaiseRefreshed();
            }
        }

        private void DefaultViewListChanged(object sender, ListChangedEventArgs e)
        {
            this.ProcessListChangedForSnapshots(e);
            this.RaiseListChanged(this, e);
        }

        private void DefaultViewOnBusyChanged(object sender, ItemsProviderChangedEventArgs e)
        {
            bool isBusy = ((ItemsProviderOnBusyChangedEventArgs) e).IsBusy;
            this.ProcessBusyChanged(this.DefaultDataViewHandle, isBusy);
        }

        private void DefaultViewRefreshNeeded(object sender, EventArgs e)
        {
            this.ProcessRefreshedForSnapshots();
            this.RaiseRefreshed();
        }

        private void DefaultViewRowLoaded(object sender, ItemsProviderChangedEventArgs e)
        {
            ItemsProviderRowLoadedEventArgs args = (ItemsProviderRowLoadedEventArgs) e;
            this.ProcessRowLoadedForSnapshots(args.Value);
            this.RaiseRowLoaded(e);
        }

        private void DescriptorRefreshed(object sender, EventArgs eventArgs)
        {
            if (!this.endInitLocker.IsLocked)
            {
                DataControllerSnapshotDescriptor descriptor = (DataControllerSnapshotDescriptor) sender;
                this.UpdateSnapshot(descriptor);
                this.RaiseRefreshed();
            }
        }

        public void DestroyVisibleList(object handle)
        {
            object currentDataViewHandle = handle;
            if (handle == null)
            {
                object local1 = handle;
                currentDataViewHandle = this.CurrentDataViewHandle;
            }
            DevExpress.Xpf.Editors.Helpers.CurrentDataView view = this.Views[currentDataViewHandle];
            if (view != null)
            {
                view.DestroyVisibleList();
            }
        }

        public virtual void EndInit()
        {
            this.UpdateDefaultView();
            this.ResetSnapshots();
            this.UpdateSnapshots();
            this.endInitLocker.Unlock();
        }

        public virtual IList ExtractDataSource(object itemsSource)
        {
            if (!this.Owner.AllowCollectionView)
            {
                return DataBindingHelper.ExtractDataSource(itemsSource, this.Owner.AllowLiveDataShaping, true, true);
            }
            ICollectionView dataSource = itemsSource as ICollectionView;
            if (dataSource == null)
            {
                CollectionViewSource source1 = new CollectionViewSource();
                source1.Source = itemsSource;
                dataSource = source1.View;
            }
            return DataBindingHelper.ExtractDataSource(dataSource, this.Owner.AllowLiveDataShaping, false, false);
        }

        public int FindItemIndexByText(string text, bool isCaseSensitiveSearch, bool allowTextInputSuggestions, object handle = null, int startItemIndex = -1, bool searchNext = true, bool ignoreStartIndex = false)
        {
            object currentDataViewHandle = handle;
            if (handle == null)
            {
                object local1 = handle;
                currentDataViewHandle = this.CurrentDataViewHandle;
            }
            DevExpress.Xpf.Editors.Helpers.CurrentDataView view = this.Views[currentDataViewHandle];
            int num = (startItemIndex == -1) ? startItemIndex : this.GetVisibleIndexByListSourceIndex(startItemIndex, handle);
            int index = view.FindItemIndexByText(text, isCaseSensitiveSearch, allowTextInputSuggestions, num, searchNext, ignoreStartIndex);
            if (index == -1)
            {
                return -1;
            }
            DataProxy proxyByIndex = view.GetProxyByIndex(index);
            if (proxyByIndex == null)
            {
                return -1;
            }
            object valueFromProxy = view.GetValueFromProxy(proxyByIndex);
            return this.DefaultDataView.IndexOfValue(valueFromProxy);
        }

        public DevExpress.Xpf.Editors.Helpers.CurrentDataView GetCurrentDataView(object handle) => 
            this.Views[handle];

        public string GetDisplayPropertyName(object handle = null)
        {
            object currentDataViewHandle = handle;
            if (handle == null)
            {
                object local1 = handle;
                currentDataViewHandle = this.CurrentDataViewHandle;
            }
            return this.Views[currentDataViewHandle].DataAccessor.DisplayPropertyName;
        }

        public object GetDisplayValueByListSourceIndex(int listSourceIndex, object handle = null)
        {
            if (listSourceIndex < 0)
            {
                return null;
            }
            int visibleIndexByListSourceIndex = this.GetVisibleIndexByListSourceIndex(listSourceIndex, handle);
            object currentDataViewHandle = handle;
            if (handle == null)
            {
                object local1 = handle;
                currentDataViewHandle = this.CurrentDataViewHandle;
            }
            DevExpress.Xpf.Editors.Helpers.CurrentDataView view = this.Views[currentDataViewHandle];
            DataProxy proxyByIndex = view.GetProxyByIndex(visibleIndexByListSourceIndex);
            return ((proxyByIndex != null) ? view.GetDisplayValueFromProxy(proxyByIndex) : null);
        }

        public object GetItemByListSourceIndex(int index, object handle = null)
        {
            int visibleIndexByListSourceIndex = this.GetVisibleIndexByListSourceIndex(index, handle);
            object currentDataViewHandle = handle;
            if (handle == null)
            {
                object local1 = handle;
                currentDataViewHandle = this.CurrentDataViewHandle;
            }
            DevExpress.Xpf.Editors.Helpers.CurrentDataView view = this.Views[currentDataViewHandle];
            DataProxy proxyByIndex = view.GetProxyByIndex(visibleIndexByListSourceIndex);
            return ((proxyByIndex != null) ? view.GetItemByProxy(proxyByIndex) : null);
        }

        public int GetListSourceIndexByVisibleIndex(int visibleIndex, object handle)
        {
            if (visibleIndex < 0)
            {
                return -1;
            }
            object currentDataViewHandle = handle;
            if (handle == null)
            {
                object local1 = handle;
                currentDataViewHandle = this.CurrentDataViewHandle;
            }
            DevExpress.Xpf.Editors.Helpers.CurrentDataView view = this.Views[currentDataViewHandle];
            if (visibleIndex >= view.VisibleRowCount)
            {
                return -1;
            }
            DataProxy proxy = view[visibleIndex];
            if (proxy == null)
            {
                return -1;
            }
            object valueFromProxy = view.GetValueFromProxy(proxy);
            return this.DefaultDataView.IndexOfValue(valueFromProxy);
        }

        public virtual DataControllerSnapshotDescriptor GetSnapshot(object handle) => 
            this.Snapshots.GetValueOrDefault<object, DataControllerSnapshotDescriptor>(handle);

        public object GetValue(object item, bool fromObject, object handle = null)
        {
            object valueFromProxy;
            object currentDataViewHandle = handle;
            if (handle == null)
            {
                object local1 = handle;
                currentDataViewHandle = this.CurrentDataViewHandle;
            }
            DevExpress.Xpf.Editors.Helpers.CurrentDataView view = this.Views[currentDataViewHandle];
            if (fromObject)
            {
                valueFromProxy = view.GetValueFromProxy(view.CreateProxy(item, -1));
            }
            else
            {
                int index = this.DefaultDataView.IndexOf(item);
                DataProxy proxyByIndex = view.GetProxyByIndex(this.GetVisibleIndexByListSourceIndex(index, handle));
                valueFromProxy = (proxyByIndex != null) ? view.GetValueFromProxy(proxyByIndex) : null;
            }
            return (LookUpPropertyDescriptorBase.IsUnsetValue(valueFromProxy) ? null : valueFromProxy);
        }

        public object GetValueByListSourceIndex(int listSourceIndex, object handle = null)
        {
            if (listSourceIndex < 0)
            {
                return null;
            }
            int visibleIndexByListSourceIndex = this.GetVisibleIndexByListSourceIndex(listSourceIndex, handle);
            object currentDataViewHandle = handle;
            if (handle == null)
            {
                object local1 = handle;
                currentDataViewHandle = this.CurrentDataViewHandle;
            }
            DevExpress.Xpf.Editors.Helpers.CurrentDataView view = this.Views[currentDataViewHandle];
            DataProxy proxyByIndex = view.GetProxyByIndex(visibleIndexByListSourceIndex);
            return ((proxyByIndex != null) ? view.GetValueFromProxy(proxyByIndex) : null);
        }

        public string GetValuePropertyName(object handle)
        {
            object currentDataViewHandle = handle;
            if (handle == null)
            {
                object local1 = handle;
                currentDataViewHandle = this.CurrentDataViewHandle;
            }
            return this.Views[currentDataViewHandle].DataAccessor.ValuePropertyName;
        }

        public int GetVisibleIndexByListSourceIndex(int index, object handle = null)
        {
            if ((index < 0) || (index >= this.DefaultDataView.VisibleRowCount))
            {
                return -1;
            }
            DataProxy proxy = this.DefaultDataView[index];
            if (proxy == null)
            {
                return -1;
            }
            object valueFromProxy = this.DefaultDataView.GetValueFromProxy(proxy);
            object currentDataViewHandle = handle;
            if (handle == null)
            {
                object local1 = handle;
                currentDataViewHandle = this.CurrentDataViewHandle;
            }
            return this.Views[currentDataViewHandle].IndexOfValue(valueFromProxy);
        }

        public IEnumerable GetVisibleList(object handle)
        {
            object currentDataViewHandle = handle;
            if (handle == null)
            {
                object local1 = handle;
                currentDataViewHandle = this.CurrentDataViewHandle;
            }
            Func<DevExpress.Xpf.Editors.Helpers.CurrentDataView, IEnumerable> evaluator = <>c.<>9__140_0;
            if (<>c.<>9__140_0 == null)
            {
                Func<DevExpress.Xpf.Editors.Helpers.CurrentDataView, IEnumerable> local2 = <>c.<>9__140_0;
                evaluator = <>c.<>9__140_0 = x => x.VisibleList as IEnumerable;
            }
            return this.Views[currentDataViewHandle].Return<DevExpress.Xpf.Editors.Helpers.CurrentDataView, IEnumerable>(evaluator, (<>c.<>9__140_1 ??= ((Func<IEnumerable>) (() => null))));
        }

        public int GetVisibleRowCount(object handle)
        {
            object currentDataViewHandle = handle;
            if (handle == null)
            {
                object local1 = handle;
                currentDataViewHandle = this.CurrentDataViewHandle;
            }
            return this.Views[currentDataViewHandle].VisibleRowCount;
        }

        public int IndexOf(object item, object handle = null)
        {
            int index = this.DefaultDataView.IndexOf(item);
            return this.GetVisibleIndexByListSourceIndex(index, handle);
        }

        public int IndexOfValue(object value, object handle = null)
        {
            object currentDataViewHandle = handle;
            if (handle == null)
            {
                object local1 = handle;
                currentDataViewHandle = this.CurrentDataViewHandle;
            }
            return ((this.Views[currentDataViewHandle].IndexOfValue(value) != -1) ? this.DefaultDataView.IndexOfValue(value) : -1);
        }

        private void InitializeCurrentDataView()
        {
            DataControllerSnapshotDescriptor descriptor = new DataControllerSnapshotDescriptor(this.CurrentDataViewHandle);
            descriptor.Refreshed += new EventHandler(this.DescriptorRefreshed);
            this.Snapshots.Add(this.CurrentDataViewHandle, descriptor);
        }

        private void ItemsSourceListChanged(object sender, ListChangedEventArgs e)
        {
            if (!this.endInitLocker.IsLocked)
            {
                if ((this.Dispatcher == null) || this.Dispatcher.CheckAccess())
                {
                    this.ProcessItemsSourceListChanged(e);
                }
                else if (DXGridDataController.DisableThreadingProblemsDetection)
                {
                    this.Dispatcher.BeginInvoke(() => this.ProcessItemsSourceListChanged(e), new object[0]);
                }
                else
                {
                    this.Dispatcher.BeginInvoke(new Action(DevExpress.Xpf.Editors.Helpers.DataController.ThrowCrossThreadException), new object[0]);
                    ThrowCrossThreadException();
                }
            }
        }

        private void ProcessBusyChanged(object handle, bool isBusy)
        {
            bool flag = this.busyHandlers.Any<object>();
            if (isBusy)
            {
                this.busyHandlers.Add(handle);
            }
            else
            {
                this.busyHandlers.Remove(handle);
            }
            bool flag2 = this.busyHandlers.Any<object>();
            if (flag2 != flag)
            {
                this.RaiseOnBusyChanged(new ItemsProviderOnBusyChangedEventArgs(flag2));
            }
        }

        protected internal void ProcessChangeItem(ListChangedEventArgs e)
        {
            this.DefaultDataView.ProcessChangeSource(e);
            this.ProcessListChangedForSnapshots(e);
        }

        private void ProcessFindIncrementalCompletedForSnapshot(DevExpress.Xpf.Editors.Helpers.CurrentDataView view, string text, int startIndex, bool searchNext, bool ignoreStartIndex, object value)
        {
            view.ProcessFindIncrementalCompleted(text, startIndex, searchNext, ignoreStartIndex, value);
        }

        private void ProcessFindIncrementalCompletedForSnapshots(ItemsProviderChangedEventArgs e)
        {
            this.findIncrementalLocker.DoLockedActionIfNotLocked(delegate {
                ItemsProviderFindIncrementalCompletedEventArgs args = (ItemsProviderFindIncrementalCompletedEventArgs) e;
                foreach (KeyValuePair<object, DataControllerSnapshotDescriptor> pair in this.Snapshots)
                {
                    this.ProcessFindIncrementalCompletedForSnapshot(this.Views[pair.Key], args.Text, args.StartIndex, args.SearchNext, args.IgnoreStartIndex, args.Value);
                }
            });
        }

        private void ProcessInconsistencyDetectedForSnapshot(DataControllerSnapshotDescriptor descriptor)
        {
            if (!this.Views[descriptor.Handle].ProcessInconsistencyDetected())
            {
                this.UpdateSnapshot(descriptor);
            }
        }

        private void ProcessInconsistencyDetectedForSnapshots()
        {
            if (!this.DefaultDataView.ProcessInconsistencyDetected())
            {
                this.UpdateDefaultView();
                this.ResetSnapshots();
                this.UpdateSnapshots();
            }
            else
            {
                foreach (KeyValuePair<object, DataControllerSnapshotDescriptor> pair in this.Snapshots)
                {
                    this.ProcessInconsistencyDetectedForSnapshot(pair.Value);
                }
            }
        }

        private void ProcessItemsSourceListChanged(ListChangedEventArgs e)
        {
            this.DefaultDataView.ProcessListChanged(e);
            this.ResetDisplayTextCache();
        }

        private void ProcessListChangedForSnapshot(DataControllerSnapshotDescriptor descriptor, ListChangedEventArgs e)
        {
            if (!this.Views[descriptor.Handle].ProcessChangeSource(e))
            {
                this.UpdateSnapshot(descriptor);
            }
        }

        private void ProcessListChangedForSnapshots(ListChangedEventArgs e)
        {
            foreach (KeyValuePair<object, DataControllerSnapshotDescriptor> pair in this.Snapshots)
            {
                this.ProcessListChangedForSnapshot(pair.Value, e);
            }
        }

        private void ProcessRefreshedForSnapshot(DataControllerSnapshotDescriptor descriptor)
        {
            this.Views[descriptor.Handle].ProcessRefreshed();
        }

        private void ProcessRefreshedForSnapshots()
        {
            foreach (KeyValuePair<object, DataControllerSnapshotDescriptor> pair in this.Snapshots)
            {
                this.ProcessRefreshedForSnapshot(pair.Value);
            }
        }

        private void ProcessRowLoadedForSnapshot(DataControllerSnapshotDescriptor descriptor, object value)
        {
            this.Views[descriptor.Handle].ProcessRowLoaded(value);
        }

        private void ProcessRowLoadedForSnapshots(object value)
        {
            foreach (KeyValuePair<object, DataControllerSnapshotDescriptor> pair in this.Snapshots)
            {
                this.ProcessRowLoadedForSnapshot(pair.Value, value);
            }
        }

        private void RaiseCurrentChanged(ItemsProviderCurrentChangedEventArgs e)
        {
            this.CurrentChanged.Do<EventHandler<ItemsProviderCurrentChangedEventArgs>>(x => x(this, e));
        }

        private void RaiseFindIncrementalCompleted(object sender, ItemsProviderFindIncrementalCompletedEventArgs e)
        {
            if (this.FindIncrementalCompleted != null)
            {
                this.FindIncrementalCompleted(sender, e);
            }
        }

        private void RaiseListChanged(object sender, ListChangedEventArgs e)
        {
            this.ListChanged.Do<ListChangedEventHandler>(x => x(sender, e));
        }

        private void RaiseOnBusyChanged(ItemsProviderOnBusyChangedEventArgs e)
        {
            this.BusyChanged.Do<EventHandler<ItemsProviderOnBusyChangedEventArgs>>(x => x(this, e));
        }

        private void RaiseRefreshed()
        {
            this.Refreshed.Do<EventHandler>(x => x(this, EventArgs.Empty));
        }

        private void RaiseRowLoaded(ItemsProviderChangedEventArgs e)
        {
            if (this.RowLoaded != null)
            {
                this.RowLoaded(this, (ItemsProviderRowLoadedEventArgs) e);
            }
        }

        private void RaiseViewRefreshed(object handle)
        {
            EventHandler<ItemsProviderViewRefreshedEventArgs> viewRefreshed = this.ViewRefreshed;
            if (viewRefreshed != null)
            {
                viewRefreshed(this, new ItemsProviderViewRefreshedEventArgs(handle));
            }
        }

        public void Refresh()
        {
            if (!this.Owner.IsLoaded && this.IsServerMode)
            {
                this.snapshotsDestroyed = true;
                this.UpdateItemsSource();
            }
            else if (this.Owner.IsLoaded && this.NeedsRefresh)
            {
                this.snapshotsDestroyed = false;
                this.UpdateItemsSource();
            }
            else if (!this.DefaultDataView.ProcessRefresh())
            {
                this.Reset();
            }
            else
            {
                this.RefreshSnapshots();
            }
        }

        private void RefreshSnapshot(DataControllerSnapshotDescriptor descriptor)
        {
            DevExpress.Xpf.Editors.Helpers.CurrentDataView view;
            object handle = descriptor.Handle;
            bool flag = false;
            if (this.Views.TryGetValue(handle, out view))
            {
                flag = view.ProcessRefresh();
            }
            if (!flag)
            {
                this.UpdateSnapshot(descriptor);
            }
        }

        private void RefreshSnapshots()
        {
            foreach (KeyValuePair<object, DataControllerSnapshotDescriptor> pair in this.Snapshots)
            {
                this.RefreshSnapshot(pair.Value);
            }
        }

        public virtual void RegisterSnapshot(DataControllerSnapshotDescriptor descriptor)
        {
            if (!this.Snapshots.ContainsKey(descriptor.Handle))
            {
                this.Snapshots.Add(descriptor.Handle, descriptor);
                descriptor.Refreshed += new EventHandler(this.DescriptorRefreshed);
                if (!this.endInitLocker.IsLocked)
                {
                    this.UpdateSnapshot(descriptor);
                }
            }
        }

        public virtual void ReleaseSnapshot(DataControllerSnapshotDescriptor descriptor)
        {
            this.Snapshots.Remove(descriptor.Handle);
            descriptor.Refreshed -= new EventHandler(this.DescriptorRefreshed);
            Action<DevExpress.Xpf.Editors.Helpers.CurrentDataView> action = <>c.<>9__67_0;
            if (<>c.<>9__67_0 == null)
            {
                Action<DevExpress.Xpf.Editors.Helpers.CurrentDataView> local1 = <>c.<>9__67_0;
                action = <>c.<>9__67_0 = x => x.Release();
            }
            this.Views.GetValueOrDefault<object, DevExpress.Xpf.Editors.Helpers.CurrentDataView>(descriptor.Handle).Do<DevExpress.Xpf.Editors.Helpers.CurrentDataView>(action);
            this.Views.Remove(descriptor.Handle);
        }

        public void Reset()
        {
            this.BeginInit();
            this.EndInit();
        }

        public void ResetDisplayTextCache()
        {
            foreach (DevExpress.Xpf.Editors.Helpers.CurrentDataView view in this.Views.Values)
            {
                view.ResetDisplayTextCache();
            }
        }

        private void ResetSnapshot(DataControllerSnapshotDescriptor descriptor)
        {
            DevExpress.Xpf.Editors.Helpers.CurrentDataView view;
            object handle = descriptor.Handle;
            if (this.Views.TryGetValue(handle, out view))
            {
                this.Views.Remove(handle);
                view.Dispose();
            }
        }

        private void ResetSnapshots()
        {
            foreach (KeyValuePair<object, DataControllerSnapshotDescriptor> pair in this.Snapshots)
            {
                this.ResetSnapshot(pair.Value);
            }
        }

        public void ResetVisibleList(object handle)
        {
            object currentDataViewHandle = handle;
            if (handle == null)
            {
                object local1 = handle;
                currentDataViewHandle = this.CurrentDataViewHandle;
            }
            Action<DevExpress.Xpf.Editors.Helpers.CurrentDataView> action = <>c.<>9__142_0;
            if (<>c.<>9__142_0 == null)
            {
                Action<DevExpress.Xpf.Editors.Helpers.CurrentDataView> local2 = <>c.<>9__142_0;
                action = <>c.<>9__142_0 = x => x.ResetVisibleList();
            }
            this.Views[currentDataViewHandle].Do<DevExpress.Xpf.Editors.Helpers.CurrentDataView>(action);
        }

        public void SetFilterCriteria(string criteria)
        {
            if (!Equals(this.FilterCriteria, criteria))
            {
                this.FilterCriteria = criteria;
                if (!this.endInitLocker.IsLocked)
                {
                    this.UpdateFilterCriteria();
                }
            }
        }

        private void SnapshotBusyChanged(object sender, ItemsProviderChangedEventArgs e)
        {
            object handle = ((DevExpress.Xpf.Editors.Helpers.CurrentDataView) sender).Handle;
            this.ProcessBusyChanged(handle, ((ItemsProviderOnBusyChangedEventArgs) e).IsBusy);
        }

        private void SnapshotFindIncrementalCompleted(object sender, ItemsProviderChangedEventArgs e)
        {
            ItemsProviderFindIncrementalCompletedEventArgs args = (ItemsProviderFindIncrementalCompletedEventArgs) e;
            this.DefaultDataView.ProcessFindIncremental(args);
        }

        private void SnapshotInconsistencyDetected(object sender, EventArgs e)
        {
            this.RaiseRefreshed();
        }

        private void SnapshotRefreshNeeded(object sender, EventArgs e)
        {
            DevExpress.Xpf.Editors.Helpers.CurrentDataView view = (DevExpress.Xpf.Editors.Helpers.CurrentDataView) sender;
            this.RaiseViewRefreshed(view.Handle);
        }

        private void SnapshotRowLoaded(object sender, ItemsProviderChangedEventArgs args)
        {
            DevExpress.Xpf.Editors.Helpers.CurrentDataView view = (DevExpress.Xpf.Editors.Helpers.CurrentDataView) sender;
            this.RaiseRowLoaded(args);
        }

        private void SubscribeDefaultDataView(DevExpress.Xpf.Editors.Helpers.DefaultDataView view)
        {
            view.InconsistencyDetected += new EventHandler(this.DefaultViewInconsistencyDetected);
            view.RefreshNeeded += new EventHandler(this.DefaultViewRefreshNeeded);
            view.ListChanged += new ListChangedEventHandler(this.DefaultViewListChanged);
            view.RowLoaded += new ItemsProviderChangedEventHandler(this.DefaultViewRowLoaded);
            view.BusyChanged += new ItemsProviderChangedEventHandler(this.DefaultViewOnBusyChanged);
            view.FindIncrementalCompleted += new ItemsProviderChangedEventHandler(this.DefaultViewFindIncrementalCompleted);
            view.Initialize();
            view.SubscribeToEvents();
            (view.ListSource as IBindingList).Do<IBindingList>(delegate (IBindingList x) {
                x.ListChanged += this.ListChangedHandler.Handler;
            });
            (view as CollectionViewDefaultDataView).Do<CollectionViewDefaultDataView>(delegate (CollectionViewDefaultDataView x) {
                x.CurrentChanged += new EventHandler<ItemsProviderCurrentChangedEventArgs>(this.CollectionViewCurrentChanged);
            });
        }

        private void SubscribeSnapshot(DevExpress.Xpf.Editors.Helpers.CurrentDataView view)
        {
            view.RefreshNeeded += new EventHandler(this.SnapshotRefreshNeeded);
            view.RowLoaded += new ItemsProviderChangedEventHandler(this.SnapshotRowLoaded);
            view.BusyChanged += new ItemsProviderChangedEventHandler(this.SnapshotBusyChanged);
            view.FindIncrementalCompleted += new ItemsProviderChangedEventHandler(this.SnapshotFindIncrementalCompleted);
            view.InconsistencyDetected += new EventHandler(this.SnapshotInconsistencyDetected);
        }

        private static void ThrowCrossThreadException()
        {
            throw new InvalidOperationException("A cross-thread operation is detected. For more information, refer to https://documentation.devexpress.com/WPF/11765/Controls-and-Libraries/Data-Grid/Binding-to-Data/Managing-Multi-Thread-Data-Updates");
        }

        private void UnsubscribeDefaultDataView(DevExpress.Xpf.Editors.Helpers.DefaultDataView view)
        {
            view.RefreshNeeded -= new EventHandler(this.DefaultViewRefreshNeeded);
            view.InconsistencyDetected -= new EventHandler(this.DefaultViewInconsistencyDetected);
            view.ListChanged -= new ListChangedEventHandler(this.DefaultViewListChanged);
            view.RowLoaded -= new ItemsProviderChangedEventHandler(this.DefaultViewRowLoaded);
            view.BusyChanged -= new ItemsProviderChangedEventHandler(this.DefaultViewOnBusyChanged);
            view.FindIncrementalCompleted -= new ItemsProviderChangedEventHandler(this.DefaultViewFindIncrementalCompleted);
            (view.ListSource as IBindingList).Do<IBindingList>(delegate (IBindingList x) {
                x.ListChanged -= this.ListChangedHandler.Handler;
            });
            (view as CollectionViewDefaultDataView).Do<CollectionViewDefaultDataView>(delegate (CollectionViewDefaultDataView x) {
                x.CurrentChanged -= new EventHandler<ItemsProviderCurrentChangedEventArgs>(this.CollectionViewCurrentChanged);
            });
        }

        private void UnsubscribeSnapshot(DevExpress.Xpf.Editors.Helpers.CurrentDataView view)
        {
            view.RefreshNeeded -= new EventHandler(this.SnapshotRefreshNeeded);
            view.RowLoaded -= new ItemsProviderChangedEventHandler(this.SnapshotRowLoaded);
            view.BusyChanged -= new ItemsProviderChangedEventHandler(this.SnapshotBusyChanged);
            view.FindIncrementalCompleted -= new ItemsProviderChangedEventHandler(this.SnapshotFindIncrementalCompleted);
            view.InconsistencyDetected -= new EventHandler(this.SnapshotInconsistencyDetected);
        }

        private void UpdateDefaultView()
        {
            this.DefaultDataView.Do<DevExpress.Xpf.Editors.Helpers.DefaultDataView>(new Action<DevExpress.Xpf.Editors.Helpers.DefaultDataView>(this.UnsubscribeDefaultDataView));
            Action<DevExpress.Xpf.Editors.Helpers.DefaultDataView> action = <>c.<>9__68_0;
            if (<>c.<>9__68_0 == null)
            {
                Action<DevExpress.Xpf.Editors.Helpers.DefaultDataView> local1 = <>c.<>9__68_0;
                action = <>c.<>9__68_0 = x => x.Dispose();
            }
            this.DefaultDataView.Do<DevExpress.Xpf.Editors.Helpers.DefaultDataView>(action);
            this.DefaultDataView = this.CreateDefaultDataView();
            this.SubscribeDefaultDataView(this.DefaultDataView);
        }

        private void UpdateFilterCriteria()
        {
            if (!this.DefaultDataView.ProcessChangeFilter(this.FilterCriteria))
            {
                this.UpdateDefaultView();
                this.ResetSnapshots();
            }
            this.UpdateSnapshots();
        }

        public void UpdateItemsSource()
        {
            if (this.snapshotsDestroyed)
            {
                this.ListSource = Enumerable.Empty<object>().ToList<object>();
            }
            else
            {
                IList list1 = this.ExtractDataSource(this.Owner.ItemsSource);
                IList list3 = list1;
                if (list1 == null)
                {
                    IList local1 = list1;
                    IList list2 = this.ExtractDataSource(this.Owner.Items);
                    list3 = list2;
                    if (list2 == null)
                    {
                        IList local2 = list2;
                        list3 = Enumerable.Empty<object>().ToList<object>();
                    }
                }
                this.ListSource = list3;
            }
            this.Reset();
        }

        private void UpdateSnapshot(DataControllerSnapshotDescriptor descriptor)
        {
            DevExpress.Xpf.Editors.Helpers.CurrentDataView view;
            object handle = descriptor.Handle;
            string filterCriteria = this.CalcActualFilterString(this.FilterCriteria, descriptor.FilterCriteria);
            if (this.Views.TryGetValue(handle, out view))
            {
                if (view.ProcessChangeSortFilter(descriptor.Groups, descriptor.Sorts, filterCriteria, descriptor.DisplayFilterCriteria))
                {
                    return;
                }
                this.UnsubscribeSnapshot(view);
                view.Dispose();
                this.Views.Remove(handle);
            }
            view = this.DefaultDataView.CreateCurrentDataView(descriptor.Handle, descriptor.Groups, descriptor.Sorts, filterCriteria, descriptor.DisplayFilterCriteria, this.Owner.IsCaseSensitiveFilter);
            view.Initialize();
            this.SubscribeSnapshot(view);
            this.Views[handle] = view;
        }

        private void UpdateSnapshots()
        {
            foreach (KeyValuePair<object, DataControllerSnapshotDescriptor> pair in this.Snapshots)
            {
                this.UpdateSnapshot(pair.Value);
            }
            this.RaiseRefreshed();
        }

        public IItemsProviderOwner Owner { get; private set; }

        public IList ListSource { get; private set; }

        public object VisibleList =>
            this.CurrentDataView.VisibleList;

        public DevExpress.Xpf.Editors.Helpers.DefaultDataView DefaultDataView { get; private set; }

        public DevExpress.Xpf.Editors.Helpers.CurrentDataView CurrentDataView =>
            this.Views[this.CurrentDataViewHandle];

        private PostponedAction EndInitPostponedAction { get; set; }

        private ListChangedWeakEventHandler<DevExpress.Xpf.Editors.Helpers.DataController> ListChangedHandler { get; set; }

        public bool IsBusy =>
            this.busyHandlers.Any<object>();

        public bool NeedsRefresh =>
            this.snapshotsDestroyed;

        private System.Windows.Threading.Dispatcher Dispatcher =>
            this.Owner.Dispatcher;

        public string FilterCriteria { get; private set; }

        public bool IsAsyncServerMode =>
            this.DefaultDataView.GetIsAsyncServerMode();

        public bool IsSyncServerMode =>
            !this.IsAsyncServerMode && this.DefaultDataView.GetIsOwnSearchProcessing();

        private bool IsServerMode =>
            this.IsSyncServerMode || this.IsAsyncServerMode;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DevExpress.Xpf.Editors.Helpers.DataController.<>c <>9 = new DevExpress.Xpf.Editors.Helpers.DataController.<>c();
            public static Action<DevExpress.Xpf.Editors.Helpers.DataController, object, ListChangedEventArgs> <>9__60_1;
            public static Action<CurrentDataView> <>9__67_0;
            public static Action<DefaultDataView> <>9__68_0;
            public static Func<IList, bool> <>9__115_0;
            public static Func<string, string> <>9__116_0;
            public static Func<string, string> <>9__117_1;
            public static Func<string, string> <>9__117_2;
            public static Func<string, string> <>9__117_3;
            public static Func<string, string> <>9__117_4;
            public static Func<CurrentDataView, IEnumerable> <>9__140_0;
            public static Func<IEnumerable> <>9__140_1;
            public static Action<CurrentDataView> <>9__142_0;
            public static Action<CurrentDataView> <>9__143_0;

            internal void <.ctor>b__60_1(DevExpress.Xpf.Editors.Helpers.DataController controller, object o, ListChangedEventArgs e)
            {
                controller.ItemsSourceListChanged(o, e);
            }

            internal void <CancelAsyncOperations>b__143_0(CurrentDataView x)
            {
                x.CancelAsyncOperations();
            }

            internal bool <CreateDefaultDataView>b__115_0(IList x) => 
                (x is IListServer) || (x is IAsyncListServer);

            internal string <CreateLocalDefaultDataView>b__116_0(string x) => 
                x.ToString();

            internal string <CreateServerModeDefaultDataView>b__117_1(string fc) => 
                fc.ToString();

            internal string <CreateServerModeDefaultDataView>b__117_2(string x) => 
                x.ToString();

            internal string <CreateServerModeDefaultDataView>b__117_3(string x) => 
                x.ToString();

            internal string <CreateServerModeDefaultDataView>b__117_4(string fc) => 
                fc.ToString();

            internal IEnumerable <GetVisibleList>b__140_0(CurrentDataView x) => 
                x.VisibleList as IEnumerable;

            internal IEnumerable <GetVisibleList>b__140_1() => 
                null;

            internal void <ReleaseSnapshot>b__67_0(CurrentDataView x)
            {
                x.Release();
            }

            internal void <ResetVisibleList>b__142_0(CurrentDataView x)
            {
                x.ResetVisibleList();
            }

            internal void <UpdateDefaultView>b__68_0(DefaultDataView x)
            {
                x.Dispose();
            }
        }
    }
}

