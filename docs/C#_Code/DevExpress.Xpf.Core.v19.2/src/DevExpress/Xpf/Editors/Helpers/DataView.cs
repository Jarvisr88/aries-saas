namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public abstract class DataView : IEnumerable<DataProxy>, IEnumerable, IDataControllerAdapter, IDisposable
    {
        private DataProxyViewCache view;
        private readonly object listSource;
        private readonly string valueMember;
        private readonly string displayMember;
        private readonly DataControllerItemsCache itemsCache;
        private DevExpress.Xpf.Editors.Helpers.DataAccessor dataAccessor;
        private bool disposed;

        public event ItemsProviderChangedEventHandler BusyChanged;

        public event ItemsProviderChangedEventHandler FindIncrementalCompleted;

        public event EventHandler InconsistencyDetected;

        public event ListChangedEventHandler ListChanged;

        public event EventHandler RefreshNeeded;

        public event ItemsProviderChangedEventHandler RowLoaded;

        protected DataView(bool selectNullValue, object listSource, string valueMember, string displayMember)
        {
            this.selectNullValue = selectNullValue;
            this.listSource = listSource;
            this.valueMember = valueMember;
            this.displayMember = displayMember;
            this.itemsCache = this.CreateItemsCache(selectNullValue);
        }

        private object CalcDescriptorsFromOriginalSource(object originalSource)
        {
            BindingListAdapter adapter = originalSource as BindingListAdapter;
            if (adapter != null)
            {
                return adapter.OriginalDataSource;
            }
            DefaultDataView view = originalSource as DefaultDataView;
            return ((view == null) ? originalSource : this.CalcDescriptorsFromOriginalSource(view.OriginalSource));
        }

        protected virtual DataProxyViewCache CreateDataProxyViewCache(object source) => 
            new LocalDataProxyViewCache(this.DataAccessor, (IList) source);

        protected virtual DevExpress.Xpf.Editors.Helpers.DataAccessor CreateEditorsDataAccessor()
        {
            DevExpress.Xpf.Editors.Helpers.DataAccessor accessor = this.CreateEditorsDataAccessorInstance();
            this.FetchDescriptors(accessor);
            return accessor;
        }

        protected virtual DevExpress.Xpf.Editors.Helpers.DataAccessor CreateEditorsDataAccessorInstance() => 
            new DevExpress.Xpf.Editors.Helpers.DataAccessor();

        protected virtual DataControllerItemsCache CreateItemsCache(bool selectNullValue) => 
            new DataControllerItemsCache(this, selectNullValue);

        public DataProxy CreateProxy(object item, int index) => 
            this.DataAccessor.CreateProxy(item, index);

        protected virtual object DataControllerAdapterGetRowInternal(int visibleIndex) => 
            (visibleIndex > -1) ? this.view[visibleIndex].f_component : null;

        int IDataControllerAdapter.GetListSourceIndex(object value) => 
            this.FindListSourceIndexByValue(value);

        object IDataControllerAdapter.GetRow(int listSourceIndex) => 
            this.DataControllerAdapterGetRowInternal(listSourceIndex);

        object IDataControllerAdapter.GetRowValue(int visibleIndex)
        {
            DataProxy proxyByIndex = this.GetProxyByIndex(visibleIndex);
            return this.GetValueFromProxy(proxyByIndex);
        }

        object IDataControllerAdapter.GetRowValue(object item)
        {
            DataProxy proxy = this.DataAccessor.CreateProxy(item, -1);
            return this.GetValueFromProxy(proxy);
        }

        public void Dispose()
        {
            if (!this.disposed)
            {
                this.DisposeInternal();
            }
            this.disposed = true;
        }

        protected virtual void DisposeInternal()
        {
        }

        private void FetchDescriptors(DevExpress.Xpf.Editors.Helpers.DataAccessor accessor)
        {
            accessor.BeginInit();
            this.FetchDescriptorsInternal(accessor);
            accessor.EndInit();
        }

        protected virtual void FetchDescriptorsInternal(DevExpress.Xpf.Editors.Helpers.DataAccessor accessor)
        {
            accessor.ResetDescriptors();
            accessor.GenerateDefaultDescriptors(this.valueMember, this.displayMember, new Func<string, PropertyDescriptor>(this.GetDescriptorFromCollection));
        }

        protected virtual int FindListSourceIndexByValue(object value) => 
            -1;

        protected virtual PropertyDescriptor GetDescriptorFromCollection(string name)
        {
            ITypedList originalSource = this.OriginalSource as ITypedList;
            if (originalSource != null)
            {
                PropertyDescriptor descriptor = originalSource.GetItemProperties(null).Find(name, true);
                if (descriptor != null)
                {
                    return new EditorsDataControllerTypedListDescriptor(descriptor);
                }
            }
            object obj2 = this.CalcDescriptorsFromOriginalSource(this.OriginalSource);
            if (obj2 is IEnumerable)
            {
                Func<Type, bool> predicate = <>c.<>9__50_0;
                if (<>c.<>9__50_0 == null)
                {
                    Func<Type, bool> local1 = <>c.<>9__50_0;
                    predicate = <>c.<>9__50_0 = t => t.IsGenericType && (t.GetGenericTypeDefinition() == typeof(IEnumerable<>));
                }
                Func<Type, Type> selector = <>c.<>9__50_1;
                if (<>c.<>9__50_1 == null)
                {
                    Func<Type, Type> local2 = <>c.<>9__50_1;
                    selector = <>c.<>9__50_1 = t => t.GetGenericArguments()[0];
                }
                Type componentType = obj2.GetType().GetInterfaces().Where<Type>(predicate).Select<Type, Type>(selector).FirstOrDefault<Type>();
                if (componentType != null)
                {
                    PropertyDescriptor descriptor2 = TypeDescriptor.GetProperties(componentType).Find(name, false);
                    if (descriptor2 != null)
                    {
                        return descriptor2;
                    }
                }
            }
            return null;
        }

        public object GetDisplayValueFromItem(object item)
        {
            int index = this.itemsCache.IndexByItem(item);
            DataProxy proxyByIndex = this.GetProxyByIndex(index);
            return this.GetDisplayValueFromProxy(proxyByIndex);
        }

        public object GetDisplayValueFromProxy(DataProxy proxy) => 
            this.DataAccessor.GetDisplayValue(proxy);

        public virtual IEnumerator<DataProxy> GetEnumerator() => 
            this.view.GetEnumerator();

        protected internal virtual bool GetIsAsyncServerMode() => 
            false;

        protected static bool GetIsCurrentViewFIltered(IList<GroupingInfo> groups, IList<SortingInfo> sorts, string filterCriteria)
        {
            Func<IList<SortingInfo>, bool> evaluator = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<IList<SortingInfo>, bool> local1 = <>c.<>9__0_0;
                evaluator = <>c.<>9__0_0 = x => x.Count > 0;
            }
            if (sorts.If<IList<SortingInfo>>(evaluator).ReturnSuccess<IList<SortingInfo>>())
            {
                return true;
            }
            Func<IList<GroupingInfo>, bool> func2 = <>c.<>9__0_1;
            if (<>c.<>9__0_1 == null)
            {
                Func<IList<GroupingInfo>, bool> local2 = <>c.<>9__0_1;
                func2 = <>c.<>9__0_1 = x => x.Count > 0;
            }
            return (groups.If<IList<GroupingInfo>>(func2).ReturnSuccess<IList<GroupingInfo>>() || !string.IsNullOrEmpty(filterCriteria));
        }

        protected internal virtual bool GetIsOwnSearchProcessing() => 
            false;

        public object GetItemByProxy(DataProxy proxy) => 
            proxy.f_component;

        public DataProxy GetProxyByIndex(int index) => 
            ((index < 0) || (index >= this.view.Count)) ? null : this.view[index];

        public object GetValueFromItem(object item)
        {
            int index = this.itemsCache.IndexByItem(item);
            DataProxy proxyByIndex = this.GetProxyByIndex(index);
            return this.GetValueFromProxy(proxyByIndex);
        }

        public object GetValueFromProxy(DataProxy proxy) => 
            this.DataAccessor.GetValue(proxy);

        public int IndexOf(object item) => 
            this.itemsCache.IndexByItem(item);

        public int IndexOfValue(object value) => 
            this.itemsCache.IndexOfValue(value);

        public virtual void Initialize()
        {
            this.UpdateEditorsDataAccessor();
            this.InitializeView(this.listSource);
        }

        protected virtual void InitializeView(object source)
        {
        }

        private bool ProcessAddItem(ListChangedEventArgs e) => 
            this.ProcessAddItem(e.NewIndex);

        public virtual bool ProcessAddItem(int index) => 
            false;

        protected internal bool ProcessChangeItem(ListChangedEventArgs e) => 
            this.ProcessChangeItem(e.NewIndex);

        public virtual bool ProcessChangeItem(int index) => 
            false;

        public virtual bool ProcessChangeSortFilter(IList<GroupingInfo> groups, IList<SortingInfo> sorts, string filterCriteria, string displayFilterCriteria) => 
            false;

        protected internal virtual bool ProcessChangeSource(ListChangedEventArgs e)
        {
            bool flag = false;
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                flag = this.ProcessAddItem(e);
            }
            else if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                flag = this.ProcessChangeItem(e);
            }
            else if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                flag = this.ProcessDeleteItem(e);
            }
            else if (e.ListChangedType == ListChangedType.ItemMoved)
            {
                flag = this.ProcessMoveItem(e);
            }
            else if ((e.ListChangedType == ListChangedType.PropertyDescriptorAdded) || ((e.ListChangedType == ListChangedType.PropertyDescriptorDeleted) || ((e.ListChangedType == ListChangedType.PropertyDescriptorChanged) || (e.ListChangedType == ListChangedType.Reset))))
            {
                flag = this.ProcessResetItem(e);
            }
            return flag;
        }

        private bool ProcessDeleteItem(ListChangedEventArgs e) => 
            this.ProcessDeleteItem(e.NewIndex);

        public virtual bool ProcessDeleteItem(int index) => 
            false;

        private bool ProcessMoveItem(ListChangedEventArgs e) => 
            this.ProcessMoveItem(e.OldIndex, e.NewIndex);

        public virtual bool ProcessMoveItem(int oldIndex, int newIndex) => 
            false;

        public virtual bool ProcessRefresh() => 
            false;

        public virtual bool ProcessReset() => 
            false;

        private bool ProcessResetItem(ListChangedEventArgs e) => 
            this.ProcessReset();

        protected virtual void RaiseFindIncrementalCompleted(ItemsProviderFindIncrementalCompletedEventArgs e)
        {
            ItemsProviderChangedEventHandler findIncrementalCompleted = this.FindIncrementalCompleted;
            if (findIncrementalCompleted != null)
            {
                findIncrementalCompleted(this, e);
            }
        }

        protected void RaiseInconsistencyDetected()
        {
            this.InconsistencyDetected.Do<EventHandler>(x => x(this, EventArgs.Empty));
        }

        protected virtual void RaiseListChanged(ListChangedEventArgs e)
        {
            if (this.ListChanged != null)
            {
                this.ListChanged(this, e);
            }
        }

        protected virtual void RaiseOnBusyChanged(ItemsProviderOnBusyChangedEventArgs e)
        {
            if (this.BusyChanged != null)
            {
                this.BusyChanged(this, e);
            }
        }

        protected void RaiseRefreshNeeded()
        {
            this.RefreshNeeded.Do<EventHandler>(x => x(this, EventArgs.Empty));
        }

        protected virtual void RaiseRowLoaded(ItemsProviderRowLoadedEventArgs e)
        {
            if (this.RowLoaded != null)
            {
                this.RowLoaded(this, e);
            }
        }

        public virtual void Release()
        {
            this.ReleaseView(this.ListSource);
        }

        protected virtual void ReleaseView(object source)
        {
        }

        protected void SetView(DataProxyViewCache view)
        {
            this.view = view;
        }

        protected internal virtual void SubscribeToEvents()
        {
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        protected virtual void UnsubscribeFromEvents()
        {
        }

        protected void UpdateEditorsDataAccessor()
        {
            this.dataAccessor = this.CreateEditorsDataAccessor();
        }

        public int VisibleRowCount =>
            this.view.Count;

        protected internal DataProxyViewCache View =>
            this.view;

        protected Type ElementType =>
            this.dataAccessor.ElementType;

        public DevExpress.Xpf.Editors.Helpers.DataAccessor DataAccessor =>
            this.dataAccessor;

        public bool selectNullValue { get; private set; }

        public object ListSource =>
            this.listSource;

        protected virtual object OriginalSource =>
            this.listSource;

        public DataControllerItemsCache ItemsCache =>
            this.itemsCache;

        bool IDataControllerAdapter.IsOwnSearchProcessing =>
            this.GetIsOwnSearchProcessing();

        public DataProxy this[int index] =>
            this.view[index];

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataView.<>c <>9 = new DataView.<>c();
            public static Func<IList<SortingInfo>, bool> <>9__0_0;
            public static Func<IList<GroupingInfo>, bool> <>9__0_1;
            public static Func<Type, bool> <>9__50_0;
            public static Func<Type, Type> <>9__50_1;

            internal bool <GetDescriptorFromCollection>b__50_0(Type t) => 
                t.IsGenericType && (t.GetGenericTypeDefinition() == typeof(IEnumerable<>));

            internal Type <GetDescriptorFromCollection>b__50_1(Type t) => 
                t.GetGenericArguments()[0];

            internal bool <GetIsCurrentViewFIltered>b__0_0(IList<SortingInfo> x) => 
                x.Count > 0;

            internal bool <GetIsCurrentViewFIltered>b__0_1(IList<GroupingInfo> x) => 
                x.Count > 0;
        }
    }
}

