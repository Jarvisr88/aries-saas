namespace DevExpress.Xpf.Data
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Data.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Threading;

    public abstract class VirtualSourceBase : BindableBase, IVirtualSourceAccess, INotifyCollectionChanged, INotifyPropertyChanged, IList, ICollection, IEnumerable, ITypedList, IDisposable
    {
        protected readonly VirtualSourceEventsHelper events;
        protected static readonly string FetchRowsToken = "FetchRows";
        private static readonly string GetSummariesToken = "GetSummaries";
        private static readonly string GetUniqueValuesToken = "GetUniqueValues";
        private static readonly string UpdateRowToken = "UpdateRow";
        private SortDefinition[] fSortInfo = new SortDefinition[0];
        private CriteriaOperator filter;
        private SummaryDefinition[] summaries;
        protected readonly SummaryCache summaryCache;
        private object skipToken;
        private Type elementType;
        private PropertyDescriptorCollection customProperties;
        private string summaryInProgressText;
        private EventHandler totalSummariesCalculated;
        private NotifyCollectionChangedEventHandler collectionChanged;
        private bool disposed;
        private readonly Dispatcher dispatcher = Dispatcher.CurrentDispatcher;

        event EventHandler IVirtualSourceAccess.TotalSummariesCalculated
        {
            add
            {
                this.totalSummariesCalculated += value;
            }
            remove
            {
                this.totalSummariesCalculated -= value;
            }
        }

        event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
        {
            add
            {
                this.collectionChanged += value;
            }
            remove
            {
                this.collectionChanged -= value;
            }
        }

        internal VirtualSourceBase()
        {
            this.SummaryInProgressText = "...";
            this.summaryCache = new SummaryCache(new Action(this.StartSummariesCalculation), () => this.SummaryInProgressText);
            this.events = this.CreateEventsHelper();
        }

        protected abstract Exception ApplyException(Exception exception, int skip);
        protected abstract void ApplyRows(int skip, object[] rows, ref bool hasMoreRows);
        protected abstract void ApplySuccessState(bool? hasMoreRows);
        protected virtual void ClearSummaries()
        {
            this.summaryCache.Clear();
            this.RaiseSummariesCalculated();
        }

        protected abstract VirtualSourceEventsHelper CreateEventsHelper();
        protected AsyncTask CreateFetchRowsTask(int skip, int? take)
        {
            GetRowsState getRowsState = new GetRowsState(skip, this.skipToken, take, this.fSortInfo, this.filter);
            return AsyncTask.Create<GetRowsState, FetchRowsResult>(this.GetFetchRowsToken(skip), getRowsState, delegate {
                this.AreRowsFetching = true;
            }, (cancellationToken, state) => this.FetchRowsAsync(state, cancellationToken), delegate (FetchRowsResult result, Exception exception) {
                if (!this.fSortInfo.SafeListsEqual<SortDefinition>(getRowsState.SortOrder) || !Equals(this.filter, getRowsState.Filter))
                {
                    throw new InvalidOperationException("Apply rows for obsolete sort or filter");
                }
                result ??= new FetchRowsResult(null, true, null);
                if ((exception == null) && (result.NextSkipToken != null))
                {
                    exception ??= this.GetSkipTokenNotAllowedException();
                }
                if (exception != null)
                {
                    this.FetchRowsException = this.ApplyException(exception, skip);
                }
                else
                {
                    bool hasMoreRows = this.HasMoreRows(result.Rows.Length) && result.HasMoreRows;
                    this.ApplyRows(skip, result.Rows, ref hasMoreRows);
                    this.skipToken = result.NextSkipToken;
                    this.FetchRowsException = null;
                    this.ApplySuccessState(new bool?(hasMoreRows));
                }
            }, delegate {
                this.FinishFetchRows();
            });
        }

        protected AsyncTask CreateGetSummariesTask()
        {
            SummaryDefinition[] requiredSummaries = this.GetNeededSummaries().Except<SummaryDefinition>(this.summaryCache.GetSummaries()).ToArray<SummaryDefinition>();
            Action onStart = <>c.<>9__54_0;
            if (<>c.<>9__54_0 == null)
            {
                Action local1 = <>c.<>9__54_0;
                onStart = <>c.<>9__54_0 = delegate {
                };
            }
            Action onFinish = <>c.<>9__54_5;
            if (<>c.<>9__54_5 == null)
            {
                Action local2 = <>c.<>9__54_5;
                onFinish = <>c.<>9__54_5 = delegate {
                };
            }
            return AsyncTask.Create<VirtualSourceEventsHelper.GetTotalSummariesState, object[]>(GetSummariesToken, new VirtualSourceEventsHelper.GetTotalSummariesState(requiredSummaries, this.filter), onStart, delegate (CancellationToken cancellationToken, VirtualSourceEventsHelper.GetTotalSummariesState state) {
                Func<object[], object[]> selector = <>c.<>9__54_2;
                if (<>c.<>9__54_2 == null)
                {
                    Func<object[], object[]> local1 = <>c.<>9__54_2;
                    selector = <>c.<>9__54_2 = x => x ?? new object[0];
                }
                return this.events.GetSummariesAsync(state, cancellationToken).Linq<object[]>(((TaskScheduler) null)).Select<object[], object[]>(selector).Schedule<object[]>(TaskScheduler.Default);
            }, delegate (object[] result, Exception exception) {
                if ((result != null) && (result.Length != requiredSummaries.Length))
                {
                    exception = new InconsistentNumberOfSummaryValuesException($"Inconsistent number of summary values calculated. Expected: {requiredSummaries.Length}, but calculated: {result.Length}.");
                    result = null;
                }
                result = result ?? Enumerable.Repeat<Exception>(exception, requiredSummaries.Length).ToArray<Exception>();
                object[] resultSelector = result;
                if (<>c.<>9__54_4 == null)
                {
                    object[] local2 = result;
                    resultSelector = (object[]) (<>c.<>9__54_4 = (value, summary) => new KeyValuePair<SummaryDefinition, object>(summary, value));
                }
                IEnumerable<KeyValuePair<SummaryDefinition, object>> values = ((IEnumerable<object>) <>c.<>9__54_4).Zip<object, SummaryDefinition, KeyValuePair<SummaryDefinition, object>>(requiredSummaries, resultSelector);
                this.summaryCache.Add(values);
                this.RaiseSummariesCalculated();
            }, onFinish);
        }

        void IVirtualSourceAccess.Apply(SortDefinition[] sortOrder, CriteriaOperatorValue? filter, SummaryDefinition[] summaries)
        {
            if (summaries != null)
            {
                SummaryDefinition[] definitionArray2 = (this.summaries ?? new SummaryDefinition[0]).Except<SummaryDefinition>(summaries).ToArray<SummaryDefinition>();
                int index = 0;
                while (true)
                {
                    if (index >= definitionArray2.Length)
                    {
                        if ((this.summaries == null) || summaries.Except<SummaryDefinition>(this.summaries).Any<SummaryDefinition>())
                        {
                            this.summaries = summaries;
                            this.RequerySummariesCalculation();
                        }
                        break;
                    }
                    SummaryDefinition summary = definitionArray2[index];
                    this.summaryCache.Remove(summary);
                    index++;
                }
            }
            bool flag = false;
            if ((sortOrder != null) && !this.fSortInfo.SafeListsEqual<SortDefinition>(sortOrder))
            {
                flag = true;
                this.fSortInfo = sortOrder;
            }
            if ((filter != null) && !Equals(filter.Value.Criteria, this.filter))
            {
                flag = true;
                this.filter = filter.Value.Criteria;
                this.ClearSummaries();
                this.RequerySummariesCalculation();
            }
            if (flag)
            {
                this.ResetRows(false);
            }
        }

        object IVirtualSourceAccess.GetTotalSummaryValue(SummaryDefinition summary)
        {
            if (this.GetHandlers().Summary == null)
            {
                throw new InvalidOperationException("Subscribe to the GetTotalSummaries event before accessing summaries.");
            }
            return (((this.summaries == null) || !this.summaries.Any<SummaryDefinition>(x => Equals(x, summary))) ? null : this.summaryCache.GetValueOrStartCalculation(summary));
        }

        void IVirtualSourceAccess.GetUniquePropertyValues(string propertyName, CriteriaOperator filter, Action<Either<object[], ValueAndCount[]>, Exception> onRequestCompleted, bool allowThrottle)
        {
            if (this.GetHandlers().Unique == null)
            {
                throw new InvalidOperationException("Subscribe to the GetUniqueValues event before accessing the property's unique values.");
            }
            this.RequestSourceIfNeeded();
            string text1 = GetUniqueValuesToken + "_" + propertyName + (allowThrottle ? null : Guid.NewGuid().ToString());
            if (<>c.<>9__67_0 == null)
            {
                string local1 = GetUniqueValuesToken + "_" + propertyName + (allowThrottle ? null : Guid.NewGuid().ToString());
                text1 = (string) (<>c.<>9__67_0 = delegate {
                });
            }
            Action onFinish = <>c.<>9__67_5;
            if (<>c.<>9__67_5 == null)
            {
                Action local2 = <>c.<>9__67_5;
                onFinish = <>c.<>9__67_5 = delegate {
                };
            }
            AsyncTask task = AsyncTask.Create<VirtualSourceEventsHelper.GetUniqueValuesState, Either<object[], ValueAndCount[]>>(<>c.<>9__67_0, new VirtualSourceEventsHelper.GetUniqueValuesState(propertyName, filter), (Action) text1, delegate (CancellationToken cancellationToken, VirtualSourceEventsHelper.GetUniqueValuesState state) {
                Func<Either<object[], ValueAndCount[]>, Either<object[], ValueAndCount[]>> selector = <>c.<>9__67_2;
                if (<>c.<>9__67_2 == null)
                {
                    Func<Either<object[], ValueAndCount[]>, Either<object[], ValueAndCount[]>> local1 = <>c.<>9__67_2;
                    selector = <>c.<>9__67_2 = delegate (Either<object[], ValueAndCount[]> x) {
                        Func<object[], object[]> func1 = <>c.<>9__67_3;
                        if (<>c.<>9__67_3 == null)
                        {
                            Func<object[], object[]> local1 = <>c.<>9__67_3;
                            func1 = <>c.<>9__67_3 = values => values ?? new object[0];
                        }
                        return x.SelectLeft<object[], ValueAndCount[], object[]>(func1);
                    };
                }
                return this.events.GetUniqueValuesAsync(state, cancellationToken).Linq<Either<object[], ValueAndCount[]>>(((TaskScheduler) null)).Select<Either<object[], ValueAndCount[]>, Either<object[], ValueAndCount[]>>(selector).Schedule<Either<object[], ValueAndCount[]>>(TaskScheduler.Default);
            }, (result, exception) => onRequestCompleted(result, exception), onFinish);
            this.Worker.ReplaceOrAddTask(task);
        }

        void IVirtualSourceAccess.UpdateRow(int index, Action<UpdateRowResult> onRequestCompleted)
        {
            if (this.IsRowUpdating)
            {
                throw new InvalidOperationException("Row is updating.");
            }
            this.RequestSourceIfNeeded();
            AsyncTask task = AsyncTask.Create<VirtualSourceEventsHelper.UpdateRowState, UpdateRowResult>(UpdateRowToken, new VirtualSourceEventsHelper.UpdateRowState(((IList) this)[index]), () => this.IsRowUpdating = true, delegate (CancellationToken cancellationToken, VirtualSourceEventsHelper.UpdateRowState state) {
                Func<UpdateRowResult, UpdateRowResult> selector = <>c.<>9__69_2;
                if (<>c.<>9__69_2 == null)
                {
                    Func<UpdateRowResult, UpdateRowResult> local1 = <>c.<>9__69_2;
                    selector = <>c.<>9__69_2 = x => new UpdateRowResult(null);
                }
                return this.events.UpdateRowAsync(state, cancellationToken).Linq<UpdateRowResult>(((TaskScheduler) null)).Select<UpdateRowResult, UpdateRowResult>(selector).Schedule<UpdateRowResult>(TaskScheduler.Default);
            }, delegate (UpdateRowResult result, Exception exception) {
                if (exception != null)
                {
                    throw new InvalidOperationException();
                }
                onRequestCompleted(result);
            }, () => this.IsRowUpdating = false);
            this.Worker.ReplaceOrAddTask(task);
        }

        public void Dispose()
        {
            this.VerifyAccess();
            if (!this.disposed)
            {
                this.disposed = true;
                this.Worker.Dispose();
            }
        }

        protected abstract Task<FetchRowsResult> FetchRowsAsync(GetRowsState state, CancellationToken cancellationToken);
        protected virtual void FinishFetchRows()
        {
            this.AreRowsFetching = false;
        }

        protected abstract string GetFetchRowsToken(int skip);
        protected abstract Handlers GetHandlers();
        protected virtual object GetItem(int index) => 
            this.List[index];

        protected virtual SummaryDefinition[] GetNeededSummaries() => 
            this.summaries ?? new SummaryDefinition[0];

        protected T GetProperty<T>(Expression<Func<T>> expression)
        {
            this.VerifyAccess();
            return base.GetProperty<T>(expression);
        }

        protected virtual Exception GetSkipTokenNotAllowedException() => 
            null;

        internal PropertyDescriptor[] GetSourceProperties() => 
            (this.customProperties ?? TypeDescriptor.GetProperties(this.elementType)).Cast<PropertyDescriptor>().ToArray<PropertyDescriptor>();

        protected abstract bool HasMoreRows(int fetchedRowsCount);
        protected void NotifyCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            if (this.collectionChanged != null)
            {
                this.collectionChanged(this, args);
            }
        }

        protected void NotifyCollectionReset()
        {
            this.NotifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            base.RaisePropertyChanged<int>(Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(VirtualSourceBase)), (MethodInfo) methodof(VirtualSourceBase.get_Count)), new ParameterExpression[0]));
        }

        protected virtual void OnCountRequested()
        {
        }

        internal void RaiseIsDataLoadingChanged()
        {
        }

        protected virtual void RaiseSummariesCalculated()
        {
            if (this.totalSummariesCalculated != null)
            {
                this.totalSummariesCalculated(this, EventArgs.Empty);
            }
        }

        public void RefreshRows()
        {
            this.VerifyAccess();
            this.ResetRows(true);
        }

        protected void RequerySummariesCalculation()
        {
            this.Worker.ReplaceTask(this.CreateGetSummariesTask());
        }

        protected void RequestSourceIfNeeded()
        {
            this.events.RequestSourceIfNeeded();
        }

        private void ResetRows(bool manualReset)
        {
            this.skipToken = null;
            this.FetchRowsException = null;
            bool? hasMoreRows = null;
            this.ApplySuccessState(hasMoreRows);
            this.ResetRowsCore(manualReset);
        }

        protected abstract void ResetRowsCore(bool manualReset = false);
        protected bool SetProperty<T>(Expression<Func<T>> expression, T value)
        {
            this.VerifyAccess();
            return base.SetProperty<T>(expression, value);
        }

        private void StartSummariesCalculation()
        {
            this.RequestSourceIfNeeded();
            this.Worker.ReplaceOrAddTask(this.CreateGetSummariesTask());
        }

        void ICollection.CopyTo(Array array, int index)
        {
            ((IList) this.List).CopyTo(array, index);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.List.GetEnumerator();

        int IList.Add(object value)
        {
            throw new NotSupportedException();
        }

        void IList.Clear()
        {
            throw new NotSupportedException();
        }

        bool IList.Contains(object value) => 
            this.List.Contains(value);

        int IList.IndexOf(object value) => 
            this.List.IndexOf(value);

        void IList.Insert(int index, object value)
        {
            throw new NotSupportedException();
        }

        void IList.Remove(object value)
        {
            throw new NotSupportedException();
        }

        void IList.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            if ((this.ElementType == null) && (this.CustomProperties == null))
            {
                throw new InvalidOperationException("Either the ElementType or CustomProperties property should be specified before initializing the source.");
            }
            return this.events.GetItemProperties();
        }

        string ITypedList.GetListName(PropertyDescriptor[] listAccessors) => 
            null;

        public void UpdateSummaries()
        {
            this.VerifyAccess();
            this.ClearSummaries();
            this.StartSummariesCalculation();
        }

        protected internal void VerifyAccess()
        {
            this.dispatcher.VerifyAccess();
        }

        protected abstract IList<object> List { get; }

        protected AsyncWorkerBase Worker =>
            this.events.Worker;

        public Type ElementType
        {
            get
            {
                this.VerifyAccess();
                return this.elementType;
            }
            set
            {
                this.VerifyAccess();
                if (this.elementType != value)
                {
                    this.events.CheckPropertiesNotSupplied("ElementType");
                    this.elementType = value;
                }
            }
        }

        internal bool HasCustomProperties =>
            this.customProperties != null;

        public PropertyDescriptorCollection CustomProperties
        {
            get
            {
                this.VerifyAccess();
                return this.customProperties;
            }
            set
            {
                this.VerifyAccess();
                if (!ReferenceEquals(this.customProperties, value))
                {
                    this.events.CheckPropertiesNotSupplied("CustomProperties");
                    this.customProperties = value;
                }
            }
        }

        public Exception FetchRowsException
        {
            get => 
                this.GetProperty<Exception>(Expression.Lambda<Func<Exception>>(Expression.Property(Expression.Constant(this, typeof(VirtualSourceBase)), (MethodInfo) methodof(VirtualSourceBase.get_FetchRowsException)), new ParameterExpression[0]));
            protected set => 
                this.SetProperty<Exception>(Expression.Lambda<Func<Exception>>(Expression.Property(Expression.Constant(this, typeof(VirtualSourceBase)), (MethodInfo) methodof(VirtualSourceBase.get_FetchRowsException)), new ParameterExpression[0]), value);
        }

        public bool AreRowsFetching
        {
            get => 
                this.GetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(VirtualSourceBase)), (MethodInfo) methodof(VirtualSourceBase.get_AreRowsFetching)), new ParameterExpression[0]));
            private set => 
                this.SetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(VirtualSourceBase)), (MethodInfo) methodof(VirtualSourceBase.get_AreRowsFetching)), new ParameterExpression[0]), value);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsRowUpdating
        {
            get => 
                this.GetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(VirtualSourceBase)), (MethodInfo) methodof(VirtualSourceBase.get_IsRowUpdating)), new ParameterExpression[0]));
            private set => 
                this.SetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(VirtualSourceBase)), (MethodInfo) methodof(VirtualSourceBase.get_IsRowUpdating)), new ParameterExpression[0]), value);
        }

        public string SummaryInProgressText
        {
            get
            {
                this.VerifyAccess();
                return this.summaryInProgressText;
            }
            set
            {
                this.VerifyAccess();
                this.summaryInProgressText = value;
            }
        }

        object IList.this[int index]
        {
            get => 
                this.GetItem(index);
            set
            {
                throw new NotSupportedException();
            }
        }

        public int Count
        {
            get
            {
                if (this.GetHandlers().Fetch == null)
                {
                    throw new InvalidOperationException("Subscribe to the FetchRows or FetchPage event before using the source.");
                }
                this.OnCountRequested();
                return this.List.Count;
            }
        }

        bool IList.IsReadOnly =>
            true;

        bool IList.IsFixedSize =>
            true;

        object ICollection.SyncRoot =>
            ((ICollection) this.List).SyncRoot;

        bool ICollection.IsSynchronized =>
            false;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly VirtualSourceBase.<>c <>9 = new VirtualSourceBase.<>c();
            public static Action <>9__54_0;
            public static Func<object[], object[]> <>9__54_2;
            public static Func<object, SummaryDefinition, KeyValuePair<SummaryDefinition, object>> <>9__54_4;
            public static Action <>9__54_5;
            public static Action <>9__67_0;
            public static Func<object[], object[]> <>9__67_3;
            public static Func<Either<object[], ValueAndCount[]>, Either<object[], ValueAndCount[]>> <>9__67_2;
            public static Action <>9__67_5;
            public static Func<UpdateRowResult, UpdateRowResult> <>9__69_2;

            internal void <CreateGetSummariesTask>b__54_0()
            {
            }

            internal object[] <CreateGetSummariesTask>b__54_2(object[] x) => 
                x ?? new object[0];

            internal KeyValuePair<SummaryDefinition, object> <CreateGetSummariesTask>b__54_4(object value, SummaryDefinition summary) => 
                new KeyValuePair<SummaryDefinition, object>(summary, value);

            internal void <CreateGetSummariesTask>b__54_5()
            {
            }

            internal void <DevExpress.Xpf.Data.Native.IVirtualSourceAccess.GetUniquePropertyValues>b__67_0()
            {
            }

            internal Either<object[], ValueAndCount[]> <DevExpress.Xpf.Data.Native.IVirtualSourceAccess.GetUniquePropertyValues>b__67_2(Either<object[], ValueAndCount[]> x)
            {
                Func<object[], object[]> selector = <>9__67_3;
                if (<>9__67_3 == null)
                {
                    Func<object[], object[]> local1 = <>9__67_3;
                    selector = <>9__67_3 = values => values ?? new object[0];
                }
                return x.SelectLeft<object[], ValueAndCount[], object[]>(selector);
            }

            internal object[] <DevExpress.Xpf.Data.Native.IVirtualSourceAccess.GetUniquePropertyValues>b__67_3(object[] values) => 
                values ?? new object[0];

            internal void <DevExpress.Xpf.Data.Native.IVirtualSourceAccess.GetUniquePropertyValues>b__67_5()
            {
            }

            internal UpdateRowResult <DevExpress.Xpf.Data.Native.IVirtualSourceAccess.UpdateRow>b__69_2(UpdateRowResult x) => 
                new UpdateRowResult(null);
        }

        protected class GetRowsState
        {
            public readonly int Skip;
            public readonly object SkipToken;
            public readonly int? Take;
            public readonly SortDefinition[] SortOrder;
            public readonly CriteriaOperator Filter;

            public GetRowsState(int skip, object skipToken, int? take, SortDefinition[] sortOrder, CriteriaOperator filter)
            {
                this.Skip = skip;
                this.SkipToken = skipToken;
                this.Take = take;
                this.SortOrder = sortOrder;
                this.Filter = filter;
            }

            public override bool Equals(object obj)
            {
                VirtualSourceBase.GetRowsState state = obj as VirtualSourceBase.GetRowsState;
                if ((state == null) || (this.Skip != state.Skip))
                {
                    return false;
                }
                int? take = this.Take;
                int? nullable2 = state.Take;
                return (((take.GetValueOrDefault() == nullable2.GetValueOrDefault()) ? ((take != null) == (nullable2 != null)) : false) && (this.SortOrder.SafeListsEqual<SortDefinition>(state.SortOrder) && Equals(this.Filter, state.Filter)));
            }

            public override int GetHashCode()
            {
                throw new NotImplementedException();
            }
        }

        protected class Handlers
        {
            public readonly Delegate Fetch;
            public readonly Delegate Summary;
            public readonly Delegate Unique;

            public Handlers(Delegate fetch, Delegate summary, Delegate unique)
            {
                this.Fetch = fetch;
                this.Summary = summary;
                this.Unique = unique;
            }
        }
    }
}

