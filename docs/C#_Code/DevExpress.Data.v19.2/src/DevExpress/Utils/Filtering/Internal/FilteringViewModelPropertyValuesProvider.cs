namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using DevExpress.Utils;
    using DevExpress.Utils.Filtering;
    using DevExpress.Utils.IoC;
    using DevExpress.Utils.MVVM;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class FilteringViewModelPropertyValuesProvider : IMetricAttributesQueryOwner, IEndUserFilteringViewModelPropertyValues, IEnumerable<IEndUserFilteringMetricViewModel>, IEnumerable, IEndUserFilteringCriteriaChangeAware, IViewModelProvider, IServiceProvider
    {
        protected readonly IServiceProvider serviceProvider;
        private int resetting;
        private Lazy<IEndUserFilteringSettings> settingsCore;
        private Lazy<IStorage<IEndUserFilteringMetricViewModel>> storageCore;
        private Lazy<IEndUserFilteringViewModelProperties> propertiesCore;
        private Lazy<IEndUserFilteringViewModelBindableProperties> bindablePropertiesCore;
        private Lazy<IEndUserFilteringViewModelPropertyValues> propertyValuesCore;
        private WeakReference contextRef;
        private readonly IDictionary<string, Func<bool>> blanksSuppressionCache = new Dictionary<string, Func<bool>>();
        private readonly IDictionary<string, Func<bool>> radioPropagationCache = new Dictionary<string, Func<bool>>();
        private readonly WeakEventHandler<EventArgs, EventHandler> viewModelCreated = new WeakEventHandler<EventArgs, EventHandler>();

        event EventHandler IViewModelProvider.ViewModelChanged
        {
            add
            {
                this.viewModelCreated.Add(value);
            }
            remove
            {
                this.viewModelCreated.Remove(value);
            }
        }

        public FilteringViewModelPropertyValuesProvider(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.settingsCore = new Lazy<IEndUserFilteringSettings>(new Func<IEndUserFilteringSettings>(this.CreateSettings));
            this.storageCore = new Lazy<IStorage<IEndUserFilteringMetricViewModel>>(new Func<IStorage<IEndUserFilteringMetricViewModel>>(this.CreateStorage));
            this.propertiesCore = new Lazy<IEndUserFilteringViewModelProperties>(new Func<IEndUserFilteringViewModelProperties>(this.CreateProperties));
            this.bindablePropertiesCore = new Lazy<IEndUserFilteringViewModelBindableProperties>(new Func<IEndUserFilteringViewModelBindableProperties>(this.CreateBindableProperties));
            this.propertyValuesCore = new Lazy<IEndUserFilteringViewModelPropertyValues>(new Func<IEndUserFilteringViewModelPropertyValues>(this.CreatePropertyValues));
            this.RegisterService<IViewModelProvider>(this);
        }

        protected internal virtual void CheckBindableProperty(string path)
        {
            this.CheckFilterType(path, null, FilterType.Default, FilterValuesType.Default, FilterGroupType.Default);
        }

        protected void CheckFilterType(string path, Type type, FilterType filterType, FilterValuesType valuesType, FilterGroupType groupType)
        {
            if (this.Settings.Ensure(path, type, filterType, valuesType, groupType))
            {
                this.EnsureFilterTypeCore(path);
            }
        }

        protected void CheckForceRadio(string path, Func<bool> getForceRadio)
        {
            if (!this.radioPropagationCache.ContainsKey(path))
            {
                this.radioPropagationCache.Add(path, getForceRadio);
            }
            else
            {
                this.radioPropagationCache[path] = getForceRadio;
            }
        }

        protected void CheckSuppressBlanks(string path, Func<bool> getSuppressBlanks)
        {
            if (!this.blanksSuppressionCache.ContainsKey(path))
            {
                this.blanksSuppressionCache.Add(path, getSuppressBlanks);
            }
            else
            {
                this.blanksSuppressionCache[path] = getSuppressBlanks;
            }
        }

        protected virtual IEndUserFilteringViewModelBindableProperties CreateBindableProperties() => 
            new EndUserFilteringViewModelBindableProperties(this);

        protected IEndUserFilteringMetricViewModelValueBox CreateEndUserFilteringMetricViewModelValueBox(IEndUserFilteringMetric metric) => 
            new EndUserFilteringMetricViewModelValueBox(this.serviceProvider, metric, this);

        protected IEndUserFilteringMetricViewModel CreateMetricViewModel(IEndUserFilteringMetricViewModelFactory viewModelFactory, IEndUserFilteringMetric metric, IEndUserFilteringMetricViewModelValueBox valueBox) => 
            viewModelFactory.Get<IEndUserFilteringMetricViewModelFactory, IEndUserFilteringMetricViewModel>(factory => factory.Create(metric, valueBox), null);

        protected IEndUserFilteringViewModelProperties CreateProperties() => 
            this.CreateProperties(this.Settings);

        protected virtual IEndUserFilteringViewModelProperties CreateProperties(IEndUserFilteringSettings settings)
        {
            IValueTypeResolver typeResolver = this.GetService<IValueTypeResolver>();
            return new EndUserFilteringViewModelProperties(settings, m => typeResolver.GetValueViewModelType(m));
        }

        protected IEndUserFilteringViewModelPropertyValues CreatePropertyValues() => 
            this.CreatePropertyValues(this.storageCore.Value);

        protected virtual IEndUserFilteringViewModelPropertyValues CreatePropertyValues(IStorage<IEndUserFilteringMetricViewModel> storage) => 
            new EndUserFilteringViewModelPropertyValues(storage, this.serviceProvider);

        protected IEndUserFilteringSettings CreateSettings() => 
            this.CreateSettings(this.GetSourceType(), this.GetAttributes());

        protected virtual IEndUserFilteringSettings CreateSettings(Type sourceType, IEnumerable<IEndUserFilteringMetricAttributes> attributes) => 
            this.GetService<IEndUserFilteringSettingsFactory>().Get<IEndUserFilteringSettingsFactory, IEndUserFilteringSettings>(f => f.Create(sourceType, attributes), null);

        protected virtual IStorage<IEndUserFilteringMetricViewModel> CreateStorage() => 
            this.CreateStorage(this.GetChildren(this.Settings));

        protected virtual IStorage<IEndUserFilteringMetricViewModel> CreateStorage(IEnumerable<IEndUserFilteringMetricViewModel> children)
        {
            Func<IEndUserFilteringMetricViewModel, int> getOrder = <>c.<>9__27_0;
            if (<>c.<>9__27_0 == null)
            {
                Func<IEndUserFilteringMetricViewModel, int> local1 = <>c.<>9__27_0;
                getOrder = <>c.<>9__27_0 = m => m.Metric.Order;
            }
            return new Storage<IEndUserFilteringMetricViewModel>(children, getOrder);
        }

        IDisposable IEndUserFilteringCriteriaChangeAware.EnterFilterCriteriaChange() => 
            this.IsPropertyValuesCreated ? this.PropertyValues.EnterFilterCriteriaChange() : null;

        void IEndUserFilteringCriteriaChangeAware.QueueFilterCriteriaChange(string path, Action<string> change)
        {
            if (this.IsPropertyValuesCreated)
            {
                this.PropertyValues.QueueFilterCriteriaChange(path, change);
            }
        }

        void IEndUserFilteringViewModelPropertyValues.ApplyFilterCriteria(Func<object> getViewModel, CriteriaOperator criteria)
        {
            this.PropertyValues.ApplyFilterCriteria(getViewModel, criteria);
        }

        void IEndUserFilteringViewModelPropertyValues.ApplyFilterCriteria(Func<object> getViewModel, string path, CriteriaOperator criteria)
        {
            this.PropertyValues.ApplyFilterCriteria(getViewModel, path, criteria);
        }

        void IEndUserFilteringViewModelPropertyValues.EnsureValueType(string path)
        {
            this.PropertyValues.EnsureValueType(path);
        }

        IEndUserFilteringViewModelPropertyValues IEndUserFilteringViewModelPropertyValues.GetNestedValues(string rootPath) => 
            this.PropertyValues.GetNestedValues(rootPath);

        bool IEndUserFilteringViewModelPropertyValues.ParseFilterCriteria(string path, CriteriaOperator criteria)
        {
            DevExpress.Utils.Filtering.ParseFilterCriteriaEventArgs args = this.GetService<IFilterCriteriaParseFactory>().Create(this.PropertyValues[path]).Initialize(criteria);
            return this.ParseFilterCriteria(args);
        }

        CriteriaOperator IEndUserFilteringViewModelPropertyValues.QueryFilterCriteria(string path, CriteriaOperator criteria)
        {
            DevExpress.Utils.Filtering.QueryFilterCriteriaEventArgs args = this.GetService<IFilterCriteriaQueryFactory>().Create(this.PropertyValues[path]).Initialize(criteria);
            return this.QueryFilterCriteria(args);
        }

        void IMetricAttributesQueryOwner.RaiseMetricAttributesQuery<TEventArgs, TData>(TEventArgs e) where TEventArgs: QueryDataEventArgs<TData> where TData: MetricAttributesData
        {
            this.RaiseMetricAttributesQuery<TEventArgs, TData>(e);
            this.RaiseMetricAttributesContextQuery<TEventArgs, TData>(e);
        }

        void IMetricAttributesQueryOwner.RegisterContext(IMetricAttributesQueryOwner queryContext)
        {
            this.RegisterQueryContext(queryContext);
        }

        void IMetricAttributesQueryOwner.UnregisterContext()
        {
            this.UnregisterQueryContext();
        }

        protected virtual void EnsureFilterTypeCore(string path)
        {
            this.PropertyValues.EnsureValueType(path);
        }

        [IteratorStateMachine(typeof(<GetAttributes>d__13))]
        protected virtual IEnumerable<IEndUserFilteringMetricAttributes> GetAttributes() => 
            new <GetAttributes>d__13(-2);

        [IteratorStateMachine(typeof(<GetChildren>d__28))]
        protected IEnumerable<IEndUserFilteringMetricViewModel> GetChildren(IEndUserFilteringSettings settings)
        {
            <GetChildren>d__28 d__1 = new <GetChildren>d__28(-2);
            d__1.<>4__this = this;
            d__1.<>3__settings = settings;
            return d__1;
        }

        private bool GetForceRadio(string path)
        {
            Func<bool> func;
            if (!this.radioPropagationCache.TryGetValue(path, out func))
            {
                return false;
            }
            Func<Func<bool>, bool> func1 = <>c.<>9__95_0;
            if (<>c.<>9__95_0 == null)
            {
                Func<Func<bool>, bool> local1 = <>c.<>9__95_0;
                func1 = <>c.<>9__95_0 = get => get();
            }
            return func.Get<Func<bool>, bool>(func1, false);
        }

        protected Func<IServiceProvider> GetGetContextServiceProvider() => 
            delegate {
                Func<WeakReference, IServiceProvider> get = <>c.<>9__117_1;
                if (<>c.<>9__117_1 == null)
                {
                    Func<WeakReference, IServiceProvider> local1 = <>c.<>9__117_1;
                    get = <>c.<>9__117_1 = ctxRef => ctxRef.Target as IServiceProvider;
                }
                return this.contextRef.Get<WeakReference, IServiceProvider>(get, null);
            };

        protected TService GetService<TService>() where TService: class => 
            this.serviceProvider.GetService<TService>();

        protected virtual Type GetSourceType() => 
            null;

        private bool GetSuppressBlanks(string path)
        {
            Func<bool> func;
            if (!this.blanksSuppressionCache.TryGetValue(path, out func))
            {
                return false;
            }
            Func<Func<bool>, bool> func1 = <>c.<>9__91_0;
            if (<>c.<>9__91_0 == null)
            {
                Func<Func<bool>, bool> local1 = <>c.<>9__91_0;
                func1 = <>c.<>9__91_0 = get => get();
            }
            return func.Get<Func<bool>, bool>(func1, false);
        }

        protected IEndUserFilteringViewModel GetViewModelCore(Lazy<IEndUserFilteringViewModel> lazy)
        {
            IEndUserFilteringViewModel viewModel = lazy.Value;
            if (!lazy.IsValueCreated)
            {
                this.OnViewModelCreated(viewModel);
            }
            return viewModel;
        }

        protected virtual object GetViewModelForBindableProperties() => 
            this.GetViewModelCore(this.ViewModelCore);

        protected void InitializeMemberBindings(string path)
        {
            this.GetViewModelCore(this.ViewModelCore).Do<IEndUserFilteringViewModel>(delegate (IEndUserFilteringViewModel viewModel) {
                Func<IEndUserFilteringMetricViewModel, string> func1 = <>c.<>9__11_1;
                if (<>c.<>9__11_1 == null)
                {
                    Func<IEndUserFilteringMetricViewModel, string> local1 = <>c.<>9__11_1;
                    func1 = <>c.<>9__11_1 = x => x.Metric.Path;
                }
                this.storageCore.Value[path, func1].Do<IEndUserFilteringMetricViewModel>(delegate (IEndUserFilteringMetricViewModel metricViewModel) {
                    using (metricViewModel.LockValue())
                    {
                        this.UpdateMemberBindings(metricViewModel, viewModel, null);
                    }
                });
            });
        }

        protected void OnBindablePropertiesInitialized()
        {
            if (this.bindablePropertiesCore.IsValueCreated)
            {
                this.BindableProperties.RaisePropertyChanged(string.Empty);
            }
        }

        protected virtual void OnModelChanged()
        {
            this.RaisePropertyChanged<IEndUserFilteringSettings>(Expression.Lambda<Func<IEndUserFilteringSettings>>(Expression.Property(Expression.Constant(this, typeof(FilteringViewModelPropertyValuesProvider)), (MethodInfo) methodof(FilteringViewModelPropertyValuesProvider.get_Settings)), new ParameterExpression[0]));
            this.RaisePropertyChanged<IEndUserFilteringViewModelProperties>(Expression.Lambda<Func<IEndUserFilteringViewModelProperties>>(Expression.Property(Expression.Constant(this, typeof(FilteringViewModelPropertyValuesProvider)), (MethodInfo) methodof(FilteringViewModelPropertyValuesProvider.get_Properties)), new ParameterExpression[0]));
            this.RaisePropertyChanged<IEndUserFilteringViewModelBindableProperties>(Expression.Lambda<Func<IEndUserFilteringViewModelBindableProperties>>(Expression.Property(Expression.Constant(this, typeof(FilteringViewModelPropertyValuesProvider)), (MethodInfo) methodof(FilteringViewModelPropertyValuesProvider.get_BindableProperties)), new ParameterExpression[0]));
            this.RaisePropertyChanged<IEndUserFilteringViewModelPropertyValues>(Expression.Lambda<Func<IEndUserFilteringViewModelPropertyValues>>(Expression.Property(Expression.Constant(this, typeof(FilteringViewModelPropertyValuesProvider)), (MethodInfo) methodof(FilteringViewModelPropertyValuesProvider.get_PropertyValues)), new ParameterExpression[0]));
        }

        protected virtual void OnViewModelCreated(IEndUserFilteringViewModel viewModel)
        {
            this.RaiseViewModelChanged();
        }

        protected virtual bool ParseFilterCriteria(DevExpress.Utils.Filtering.ParseFilterCriteriaEventArgs args)
        {
            this.RaiseParseFilterCriteria(args);
            this.RaiseContextParseFilterCriteria(args);
            return args.HasResult;
        }

        protected virtual CriteriaOperator QueryFilterCriteria(DevExpress.Utils.Filtering.QueryFilterCriteriaEventArgs args)
        {
            this.RaiseQueryFilterCriteria(args);
            this.RaiseContextQueryFilterCriteria(args);
            return args.FilterCriteria;
        }

        protected void QueueFilterCriteriaChange(string path, Action<string> change)
        {
            if (this.IsPropertyValuesCreated)
            {
                this.PropertyValues.QueueFilterCriteriaChange(path, change);
            }
        }

        protected virtual void RaiseBooleanChoiceMetricAttributesQuery(QueryBooleanChoiceDataEventArgs e)
        {
        }

        protected virtual void RaiseContextParseFilterCriteria(DevExpress.Utils.Filtering.ParseFilterCriteriaEventArgs args)
        {
            this.ResolveQueryContext<IFilterCriteriaParseContext>().Do<IFilterCriteriaParseContext>(ctx => ctx.RaiseParseFilterCriteria(args));
        }

        protected virtual void RaiseContextQueryFilterCriteria(DevExpress.Utils.Filtering.QueryFilterCriteriaEventArgs args)
        {
            this.ResolveQueryContext<IFilterCriteriaQueryContext>().Do<IFilterCriteriaQueryContext>(ctx => ctx.RaiseQueryFilterCriteria(args));
        }

        protected virtual void RaiseEnumChoiceMetricAttributesQuery(QueryEnumChoiceDataEventArgs e)
        {
        }

        protected virtual void RaiseGroupMetricAttributesQuery(QueryGroupDataEventArgs e)
        {
        }

        protected virtual void RaiseLookupMetricAttributesQuery(QueryLookupDataEventArgs e)
        {
        }

        protected virtual void RaiseMetricAttributesContextQuery<TEventArgs, TData>(TEventArgs e) where TEventArgs: QueryDataEventArgs<TData> where TData: MetricAttributesData
        {
            this.ResolveQueryContext<IMetricAttributesQueryOwner>().Do<IMetricAttributesQueryOwner>(ctx => ctx.RaiseMetricAttributesQuery<TEventArgs, TData>(e));
        }

        protected virtual void RaiseMetricAttributesQuery<TEventArgs, TData>(TEventArgs e) where TEventArgs: QueryDataEventArgs<TData> where TData: MetricAttributesData
        {
            (e as QueryRangeDataEventArgs).Do<QueryRangeDataEventArgs>(new Action<QueryRangeDataEventArgs>(this.RaiseRangeMetricAttributesQuery));
            (e as QueryLookupDataEventArgs).Do<QueryLookupDataEventArgs>(new Action<QueryLookupDataEventArgs>(this.RaiseLookupMetricAttributesQuery));
            (e as QueryBooleanChoiceDataEventArgs).Do<QueryBooleanChoiceDataEventArgs>(new Action<QueryBooleanChoiceDataEventArgs>(this.RaiseBooleanChoiceMetricAttributesQuery));
            (e as QueryEnumChoiceDataEventArgs).Do<QueryEnumChoiceDataEventArgs>(new Action<QueryEnumChoiceDataEventArgs>(this.RaiseEnumChoiceMetricAttributesQuery));
            (e as QueryGroupDataEventArgs).Do<QueryGroupDataEventArgs>(new Action<QueryGroupDataEventArgs>(this.RaiseGroupMetricAttributesQuery));
        }

        protected virtual void RaiseParseFilterCriteria(DevExpress.Utils.Filtering.ParseFilterCriteriaEventArgs args)
        {
        }

        protected virtual void RaiseQueryFilterCriteria(DevExpress.Utils.Filtering.QueryFilterCriteriaEventArgs args)
        {
        }

        protected virtual void RaiseRangeMetricAttributesQuery(QueryRangeDataEventArgs e)
        {
        }

        protected void RaiseViewModelChanged()
        {
            this.viewModelCreated.Raise(this, EventArgs.Empty);
        }

        protected virtual void RegisterQueryContext(IMetricAttributesQueryOwner context)
        {
            this.contextRef = new WeakReference(context);
        }

        protected void RegisterService<TService>(TService service) where TService: class
        {
            (this.serviceProvider as BaseFilteringUIServiceProvider).Do<BaseFilteringUIServiceProvider>(x => x.RegisterService<TService>(service));
            (this.serviceProvider as IntegrityContainer).Do<IntegrityContainer>(x => x.RegisterInstance<TService>(service));
        }

        public void Reset()
        {
            if (this.resetting <= 0)
            {
                this.resetting++;
                this.ResetCore();
                this.viewModelCreated.Purge();
                this.resetting--;
            }
        }

        protected void ResetBindablePropertiesCore()
        {
            if (this.bindablePropertiesCore.IsValueCreated)
            {
                this.bindablePropertiesCore = new Lazy<IEndUserFilteringViewModelBindableProperties>(new Func<IEndUserFilteringViewModelBindableProperties>(this.CreateBindableProperties));
            }
        }

        protected virtual void ResetCore()
        {
            this.ResetSettingsCore();
            this.ResetStorageCore();
            this.ResetPropertiesCore();
            this.ResetBindablePropertiesCore();
            this.ResetPropertyValuesCore();
        }

        public void ResetDisplayOptions()
        {
            this.blanksSuppressionCache.Clear();
            this.radioPropagationCache.Clear();
        }

        protected void ResetPropertiesCore()
        {
            if (this.propertiesCore.IsValueCreated)
            {
                this.propertiesCore = new Lazy<IEndUserFilteringViewModelProperties>(new Func<IEndUserFilteringViewModelProperties>(this.CreateProperties));
            }
        }

        protected void ResetPropertyValuesCore()
        {
            if (this.IsPropertyValuesCreated)
            {
                this.propertyValuesCore = new Lazy<IEndUserFilteringViewModelPropertyValues>(new Func<IEndUserFilteringViewModelPropertyValues>(this.CreatePropertyValues));
            }
        }

        protected void ResetSettingsCore()
        {
            if (this.settingsCore.IsValueCreated)
            {
                this.settingsCore = new Lazy<IEndUserFilteringSettings>(new Func<IEndUserFilteringSettings>(this.CreateSettings));
            }
        }

        protected void ResetStorageCore()
        {
            if (this.storageCore.IsValueCreated)
            {
                foreach (IEndUserFilteringMetricViewModel model in this.storageCore.Value)
                {
                    Action<IDisposable> @do = <>c.<>9__25_0;
                    if (<>c.<>9__25_0 == null)
                    {
                        Action<IDisposable> local1 = <>c.<>9__25_0;
                        @do = <>c.<>9__25_0 = disposable => disposable.Dispose();
                    }
                    (model as IDisposable).Do<IDisposable>(@do);
                }
                this.storageCore = new Lazy<IStorage<IEndUserFilteringMetricViewModel>>(new Func<IStorage<IEndUserFilteringMetricViewModel>>(this.CreateStorage));
            }
        }

        protected TQueryContext ResolveQueryContext<TQueryContext>() where TQueryContext: class
        {
            if (!this.CanRaiseContextQuery)
            {
                return default(TQueryContext);
            }
            Func<WeakReference, TQueryContext> get = <>c__73<TQueryContext>.<>9__73_0;
            if (<>c__73<TQueryContext>.<>9__73_0 == null)
            {
                Func<WeakReference, TQueryContext> local1 = <>c__73<TQueryContext>.<>9__73_0;
                get = <>c__73<TQueryContext>.<>9__73_0 = ctxRef => ctxRef.Target as TQueryContext;
            }
            TQueryContext defaultValue = default(TQueryContext);
            return this.contextRef.Get<WeakReference, TQueryContext>(get, defaultValue);
        }

        protected void SetupDisplayBlanks(MetricAttributesData data, string path)
        {
            if (this.GetSuppressBlanks(path))
            {
                data.SetDisplayBlanks(false);
            }
            else
            {
                data.ResetDisplayBlanks();
            }
        }

        protected internal void SetupDisplayRadio(MetricAttributesData data, string path)
        {
            if (this.GetForceRadio(path))
            {
                data.SetDisplayRadio(true);
            }
            else
            {
                data.ResetDisplayRadio();
            }
        }

        IEnumerator<IEndUserFilteringMetricViewModel> IEnumerable<IEndUserFilteringMetricViewModel>.GetEnumerator() => 
            this.PropertyValues.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            this.PropertyValues.GetEnumerator();

        object IServiceProvider.GetService(Type serviceType) => 
            this.serviceProvider.GetServiceObj(serviceType);

        protected virtual void UnregisterQueryContext()
        {
            this.contextRef = null;
        }

        protected void Update()
        {
            this.Reset();
            this.OnModelChanged();
        }

        protected void UpdateMemberBindings(object viewModel, string propertyName = null)
        {
            foreach (IEndUserFilteringMetricViewModel model in (IEnumerable<IEndUserFilteringMetricViewModel>) this)
            {
                this.UpdateMemberBindings(model, viewModel, propertyName);
            }
        }

        protected void UpdateMemberBindings(string path, MetricAttributesData data)
        {
            if (this.IsPropertyValuesCreated)
            {
                this.PropertyValues[path].Do<IEndUserFilteringMetricViewModel>(metricViewModel => metricViewModel.Metric.Attributes.UpdateMemberBindings(data, metricViewModel.Query));
            }
        }

        protected void UpdateMemberBindings(IEndUserFilteringMetricViewModel metricViewModel, object viewModel, string propertyName = null)
        {
            metricViewModel.Metric.Attributes.UpdateMemberBindings(viewModel, propertyName, metricViewModel.Query);
        }

        protected void UpdatePropertyValuesDataBinding(string path)
        {
            if (this.IsPropertyValuesCreated)
            {
                this.PropertyValues.UpdateDataBinding(this, path);
            }
        }

        protected bool IsResetting =>
            this.resetting > 0;

        public IEndUserFilteringMetricViewModel this[string path] =>
            this.PropertyValues[path];

        public IEndUserFilteringSettings Settings =>
            this.settingsCore.Value;

        protected IEnumerable<IEndUserFilteringMetricViewModel> Children =>
            this.storageCore.Value;

        public IEndUserFilteringViewModelProperties Properties =>
            this.propertiesCore.Value;

        protected virtual IEnumerable<string> BindablePaths =>
            this.Settings.Paths;

        public IEndUserFilteringViewModelBindableProperties BindableProperties =>
            this.bindablePropertiesCore.Value;

        public IEndUserFilteringViewModelPropertyValues PropertyValues =>
            this.propertyValuesCore.Value;

        protected bool IsPropertyValuesCreated =>
            this.propertyValuesCore.IsValueCreated;

        protected virtual bool CanRaiseContextQuery =>
            this.contextRef != null;

        object IViewModelProvider.ViewModel =>
            this.GetViewModelCore(this.ViewModelCore);

        bool IViewModelProvider.IsViewModelCreated =>
            this.ViewModelCore.IsValueCreated;

        protected abstract Lazy<IEndUserFilteringViewModel> ViewModelCore { get; }

        protected virtual bool IsInitializingBindableProperties =>
            false;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilteringViewModelPropertyValuesProvider.<>c <>9 = new FilteringViewModelPropertyValuesProvider.<>c();
            public static Func<IEndUserFilteringMetricViewModel, string> <>9__11_1;
            public static Action<IDisposable> <>9__25_0;
            public static Func<IEndUserFilteringMetricViewModel, int> <>9__27_0;
            public static Func<Func<bool>, bool> <>9__91_0;
            public static Func<Func<bool>, bool> <>9__95_0;
            public static Func<WeakReference, IServiceProvider> <>9__117_1;

            internal int <CreateStorage>b__27_0(IEndUserFilteringMetricViewModel m) => 
                m.Metric.Order;

            internal bool <GetForceRadio>b__95_0(Func<bool> get) => 
                get();

            internal IServiceProvider <GetGetContextServiceProvider>b__117_1(WeakReference ctxRef) => 
                ctxRef.Target as IServiceProvider;

            internal bool <GetSuppressBlanks>b__91_0(Func<bool> get) => 
                get();

            internal string <InitializeMemberBindings>b__11_1(IEndUserFilteringMetricViewModel x) => 
                x.Metric.Path;

            internal void <ResetStorageCore>b__25_0(IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__73<TQueryContext> where TQueryContext: class
        {
            public static readonly FilteringViewModelPropertyValuesProvider.<>c__73<TQueryContext> <>9;
            public static Func<WeakReference, TQueryContext> <>9__73_0;

            static <>c__73()
            {
                FilteringViewModelPropertyValuesProvider.<>c__73<TQueryContext>.<>9 = new FilteringViewModelPropertyValuesProvider.<>c__73<TQueryContext>();
            }

            internal TQueryContext <ResolveQueryContext>b__73_0(WeakReference ctxRef) => 
                ctxRef.Target as TQueryContext;
        }

        [CompilerGenerated]
        private sealed class <GetAttributes>d__13 : IEnumerable<IEndUserFilteringMetricAttributes>, IEnumerable, IEnumerator<IEndUserFilteringMetricAttributes>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IEndUserFilteringMetricAttributes <>2__current;
            private int <>l__initialThreadId;

            [DebuggerHidden]
            public <GetAttributes>d__13(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                if (this.<>1__state == 0)
                {
                    this.<>1__state = -1;
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<IEndUserFilteringMetricAttributes> IEnumerable<IEndUserFilteringMetricAttributes>.GetEnumerator()
            {
                FilteringViewModelPropertyValuesProvider.<GetAttributes>d__13 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new FilteringViewModelPropertyValuesProvider.<GetAttributes>d__13(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Utils.Filtering.Internal.IEndUserFilteringMetricAttributes>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            IEndUserFilteringMetricAttributes IEnumerator<IEndUserFilteringMetricAttributes>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <GetChildren>d__28 : IEnumerable<IEndUserFilteringMetricViewModel>, IEnumerable, IEnumerator<IEndUserFilteringMetricViewModel>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IEndUserFilteringMetricViewModel <>2__current;
            private int <>l__initialThreadId;
            public FilteringViewModelPropertyValuesProvider <>4__this;
            private IEndUserFilteringSettings settings;
            public IEndUserFilteringSettings <>3__settings;
            private IEndUserFilteringMetricViewModelFactory <metricViewModelFactory>5__1;
            private IEnumerator<IEndUserFilteringMetric> <>7__wrap1;

            [DebuggerHidden]
            public <GetChildren>d__28(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                if (this.<>7__wrap1 != null)
                {
                    this.<>7__wrap1.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<metricViewModelFactory>5__1 = this.<>4__this.GetService<IEndUserFilteringMetricViewModelFactory>();
                        this.<>7__wrap1 = this.settings.GetEnumerator();
                        this.<>1__state = -3;
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -3;
                    }
                    else
                    {
                        return false;
                    }
                    if (!this.<>7__wrap1.MoveNext())
                    {
                        this.<>m__Finally1();
                        this.<>7__wrap1 = null;
                        flag = false;
                    }
                    else
                    {
                        IEndUserFilteringMetric current = this.<>7__wrap1.Current;
                        this.<>2__current = this.<>4__this.CreateMetricViewModel(this.<metricViewModelFactory>5__1, current, this.<>4__this.CreateEndUserFilteringMetricViewModelValueBox(current));
                        this.<>1__state = 1;
                        flag = true;
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<IEndUserFilteringMetricViewModel> IEnumerable<IEndUserFilteringMetricViewModel>.GetEnumerator()
            {
                FilteringViewModelPropertyValuesProvider.<GetChildren>d__28 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new FilteringViewModelPropertyValuesProvider.<GetChildren>d__28(0) {
                        <>4__this = this.<>4__this
                    };
                }
                d__.settings = this.<>3__settings;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Utils.Filtering.Internal.IEndUserFilteringMetricViewModel>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if ((num == -3) || (num == 1))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            IEndUserFilteringMetricViewModel IEnumerator<IEndUserFilteringMetricViewModel>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        private sealed class EndUserFilteringMetricViewModelValueBox : FilteringViewModelPropertyValuesProvider.MetricValueBoxCore<IValueViewModel>, IEndUserFilteringMetricViewModelValueBox
        {
            private Lazy<System.Type> valueBoxType;
            private Lazy<System.Type> valueViewModelType;
            private static readonly object valueChanged = new object();

            event EventHandler IEndUserFilteringMetricViewModelValueBox.ValueChanged
            {
                add
                {
                    base.handlers.AddHandler(valueChanged, value);
                }
                remove
                {
                    base.handlers.RemoveHandler(valueChanged, value);
                }
            }

            public EndUserFilteringMetricViewModelValueBox(IServiceProvider serviceProvider, IEndUserFilteringMetric metric, IMetricAttributesQueryOwner queryOwner) : base(serviceProvider, metric, queryOwner)
            {
                this.InitializeValueAndQuery();
            }

            protected sealed override IValueViewModel CreateValue() => 
                this.CreateValueViewModel();

            private IValueViewModel CreateValueViewModel()
            {
                IViewModelBuilder builder = this.CreateViewModelBuilder();
                return this.CreateValueViewModel(builder);
            }

            private IValueViewModel CreateValueViewModel(IViewModelBuilder builder) => 
                base.GetService<IViewModelFactory>().Get<IViewModelFactory, IValueViewModel>(factory => (IValueViewModel) factory.Create(this.valueBoxType.Value, builder), null);

            private IViewModelBuilder CreateViewModelBuilder() => 
                base.GetService<IViewModelBuilderResolver>().Get<IViewModelBuilderResolver, IViewModelBuilder>(resolver => resolver.CreateValueViewModelBuilder(base.Metric), null);

            void IEndUserFilteringMetricViewModelValueBox.EnsureValueType()
            {
                this.ReleaseValueChangedSubscription();
                this.EnsureValueAndQuery();
            }

            void IEndUserFilteringMetricViewModelValueBox.ReleaseValue()
            {
                Action<IValueViewModel> release = <>c.<>9__11_0;
                if (<>c.<>9__11_0 == null)
                {
                    Action<IValueViewModel> local1 = <>c.<>9__11_0;
                    release = <>c.<>9__11_0 = value => value.Release();
                }
                this.ReleaseValue(release);
            }

            private void EnsureValueAndQuery()
            {
                IValueTypeResolver typeResolver = base.GetService<IValueTypeResolver>();
                if (this.valueBoxType.IsValueCreated)
                {
                    this.valueBoxType = new Lazy<System.Type>(() => typeResolver.GetValueBoxType(this.Metric));
                }
                if (this.valueViewModelType.IsValueCreated)
                {
                    this.valueViewModelType = new Lazy<System.Type>(() => typeResolver.GetValueViewModelType(this.Metric));
                }
                base.EnsureValue();
                base.EnsureQuery();
            }

            private void EnsureValueChangedSubscription()
            {
                if (!base.IsValueCreated)
                {
                    base.BaseValue.Changed += new EventHandler(this.Value_Changed);
                }
            }

            protected sealed override IValueViewModel GetOrCreateValue() => 
                base.GetOrCreateValue(this.Type);

            private void InitializeValueAndQuery()
            {
                IValueTypeResolver typeResolver = base.GetService<IValueTypeResolver>();
                this.valueBoxType = new Lazy<System.Type>(() => typeResolver.GetValueBoxType(this.Metric));
                this.valueViewModelType = new Lazy<System.Type>(() => typeResolver.GetValueViewModelType(this.Metric));
                base.InitializeValue();
                base.InitializeQuery();
            }

            protected override void OnDisposing()
            {
                this.ReleaseValueChangedSubscription();
            }

            protected sealed override void OnValueChanged(IValueViewModel value)
            {
                value.Release();
            }

            private void ReleaseValueChangedSubscription()
            {
                if (base.IsValueCreated)
                {
                    base.BaseValue.Changed -= new EventHandler(this.Value_Changed);
                }
            }

            private void Value_Changed(object sender, EventArgs e)
            {
                if (!base.IsMemberBindingsUpdating)
                {
                    EventHandler handler = base.handlers[valueChanged] as EventHandler;
                    if (handler != null)
                    {
                        handler((IValueViewModel) sender, e);
                    }
                }
            }

            public System.Type Type =>
                this.valueViewModelType.Value;

            public IValueViewModel Value
            {
                get
                {
                    if (!base.IsDisposing)
                    {
                        this.EnsureValueChangedSubscription();
                    }
                    return base.BaseValue;
                }
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly FilteringViewModelPropertyValuesProvider.EndUserFilteringMetricViewModelValueBox.<>c <>9 = new FilteringViewModelPropertyValuesProvider.EndUserFilteringMetricViewModelValueBox.<>c();
                public static Action<IValueViewModel> <>9__11_0;

                internal void <DevExpress.Utils.Filtering.Internal.IEndUserFilteringMetricViewModelValueBox.ReleaseValue>b__11_0(IValueViewModel value)
                {
                    value.Release();
                }
            }
        }

        private sealed class EndUserFilteringViewModelBindableProperties : IEndUserFilteringViewModelBindableProperties, IBindingList, IList, ICollection, IEnumerable, ITypedList
        {
            private ListChangedEventHandler ListChangedCore;
            private readonly FilteringViewModelPropertyValuesProvider provider;
            private readonly Lazy<IStorage<ViewModelPropertyDescriptor>> descriptors;

            event ListChangedEventHandler IBindingList.ListChanged
            {
                add
                {
                    this.ListChangedCore += value;
                }
                remove
                {
                    this.ListChangedCore -= value;
                }
            }

            public EndUserFilteringViewModelBindableProperties(FilteringViewModelPropertyValuesProvider provider)
            {
                this.provider = provider;
                this.descriptors = new Lazy<IStorage<ViewModelPropertyDescriptor>>(new Func<IStorage<ViewModelPropertyDescriptor>>(this.CreateDescriptors));
            }

            private IStorage<ViewModelPropertyDescriptor> CreateDescriptors()
            {
                Func<ViewModelPropertyDescriptor, int> getOrder = <>c.<>9__61_0;
                if (<>c.<>9__61_0 == null)
                {
                    Func<ViewModelPropertyDescriptor, int> local1 = <>c.<>9__61_0;
                    getOrder = <>c.<>9__61_0 = x => 0;
                }
                return new Storage<ViewModelPropertyDescriptor>(this.GetChildren(), getOrder);
            }

            private IValueViewModel EnsureValue(string path)
            {
                if (this.provider.IsInitializingBindableProperties || this.provider.IsResetting)
                {
                    return null;
                }
                this.provider.CheckBindableProperty(path);
                return this.provider.PropertyValues[path].Get<IEndUserFilteringMetricViewModel, IValueViewModel>(delegate (IEndUserFilteringMetricViewModel metricViewModel) {
                    metricViewModel.SetParentViewModel(this.provider.GetViewModelForBindableProperties());
                    return metricViewModel.Value;
                }, null);
            }

            [IteratorStateMachine(typeof(<GetChildren>d__62))]
            private IEnumerable<ViewModelPropertyDescriptor> GetChildren()
            {
                HashSet<string> <roots>5__1 = new HashSet<string>();
                IEnumerator<string> enumerator = this.Paths.GetEnumerator();
                while (true)
                {
                    string current;
                    if (enumerator.MoveNext())
                    {
                        current = enumerator.Current;
                        if (NestedPropertiesHelper.HasRootPath(current))
                        {
                            string item = NestedPropertiesHelper.GetRootPath(current);
                            if (!<roots>5__1.Contains(item))
                            {
                                <roots>5__1.Add(item);
                                yield return new ViewModelPropertyDescriptor(item + ".");
                            }
                        }
                    }
                    else
                    {
                        enumerator = null;
                    }
                    yield return new ViewModelPropertyDescriptor(current);
                    current = null;
                }
            }

            private void OnBindablePropertiesReset()
            {
                ListChangedEventHandler listChangedCore = this.ListChangedCore;
                if (listChangedCore != null)
                {
                    listChangedCore(this, new ListChangedEventArgs(ListChangedType.Reset, -1));
                }
            }

            private void OnBindablePropertyChanged(string propertyName)
            {
                ListChangedEventHandler listChangedCore = this.ListChangedCore;
                if (listChangedCore != null)
                {
                    listChangedCore(this, new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, -1, this[propertyName]));
                }
            }

            public void RaisePropertyChanged(string propertyName)
            {
                if (string.IsNullOrEmpty(propertyName))
                {
                    this.OnBindablePropertiesReset();
                }
                else
                {
                    this.OnBindablePropertyChanged(propertyName);
                }
            }

            void ICollection.CopyTo(Array array, int index)
            {
                array.SetValue(this, index);
            }

            [IteratorStateMachine(typeof(<System-Collections-IEnumerable-GetEnumerator>d__48))]
            IEnumerator IEnumerable.GetEnumerator()
            {
                <System-Collections-IEnumerable-GetEnumerator>d__48 d__1 = new <System-Collections-IEnumerable-GetEnumerator>d__48(0);
                d__1.<>4__this = this;
                return d__1;
            }

            int IList.Add(object value) => 
                (value == this) ? 0 : -1;

            void IList.Clear()
            {
            }

            bool IList.Contains(object value) => 
                value == this;

            int IList.IndexOf(object value) => 
                (value == this) ? 0 : -1;

            void IList.Insert(int index, object value)
            {
            }

            void IList.Remove(object value)
            {
            }

            void IList.RemoveAt(int index)
            {
            }

            void IBindingList.AddIndex(PropertyDescriptor property)
            {
            }

            object IBindingList.AddNew() => 
                this;

            void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
            {
            }

            int IBindingList.Find(PropertyDescriptor property, object key) => 
                -1;

            void IBindingList.RemoveIndex(PropertyDescriptor property)
            {
            }

            void IBindingList.RemoveSort()
            {
            }

            PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
            {
                // Unresolved stack state at '00000090'
            }

            string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
            {
                string rootPath = NestedPropertiesHelper.GetRootPath(listAccessors);
                if (string.IsNullOrEmpty(rootPath))
                {
                    return null;
                }
                Func<PropertyDescriptor, string> get = <>c.<>9__64_0;
                if (<>c.<>9__64_0 == null)
                {
                    Func<PropertyDescriptor, string> local1 = <>c.<>9__64_0;
                    get = <>c.<>9__64_0 = x => x.Name;
                }
                return this[rootPath + "."].Get<PropertyDescriptor, string>(get, null);
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
                true;

            bool IBindingList.SupportsSearching =>
                false;

            bool IBindingList.SupportsSorting =>
                false;

            bool IList.IsFixedSize =>
                true;

            bool IList.IsReadOnly =>
                true;

            object IList.this[int index]
            {
                get => 
                    (index == 0) ? this : null;
                set
                {
                }
            }

            int ICollection.Count =>
                1;

            bool ICollection.IsSynchronized =>
                true;

            object ICollection.SyncRoot =>
                this;

            public IEnumerable<string> Paths =>
                this.provider.BindablePaths;

            public PropertyDescriptor this[string path]
            {
                get
                {
                    Func<ViewModelPropertyDescriptor, string> func1 = <>c.<>9__59_0;
                    if (<>c.<>9__59_0 == null)
                    {
                        Func<ViewModelPropertyDescriptor, string> local1 = <>c.<>9__59_0;
                        func1 = <>c.<>9__59_0 = x => x.Path;
                    }
                    return this.descriptors.Value[path, func1];
                }
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties.<>c <>9 = new FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties.<>c();
                public static Func<FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties.ViewModelPropertyDescriptor, string> <>9__59_0;
                public static Func<FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties.ViewModelPropertyDescriptor, int> <>9__61_0;
                public static Func<FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties.ViewModelPropertyDescriptor, string> <>9__63_0;
                public static Func<FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties.ViewModelPropertyDescriptor, FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties.ViewModelPropertyDescriptor> <>9__63_1;
                public static Func<KeyValuePair<string, FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties.ViewModelPropertyDescriptor>, FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties.ViewModelPropertyDescriptor> <>9__63_3;
                public static Func<PropertyDescriptor, string> <>9__64_0;

                internal int <CreateDescriptors>b__61_0(FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties.ViewModelPropertyDescriptor x) => 
                    0;

                internal string <get_Item>b__59_0(FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties.ViewModelPropertyDescriptor x) => 
                    x.Path;

                internal string <System.ComponentModel.ITypedList.GetItemProperties>b__63_0(FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties.ViewModelPropertyDescriptor x) => 
                    x.Path;

                internal FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties.ViewModelPropertyDescriptor <System.ComponentModel.ITypedList.GetItemProperties>b__63_1(FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties.ViewModelPropertyDescriptor x) => 
                    x;

                internal FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties.ViewModelPropertyDescriptor <System.ComponentModel.ITypedList.GetItemProperties>b__63_3(KeyValuePair<string, FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties.ViewModelPropertyDescriptor> p) => 
                    p.Value;

                internal string <System.ComponentModel.ITypedList.GetListName>b__64_0(PropertyDescriptor x) => 
                    x.Name;
            }


            [CompilerGenerated]
            private sealed class <System-Collections-IEnumerable-GetEnumerator>d__48 : IEnumerator<object>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private object <>2__current;
                public FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties <>4__this;

                [DebuggerHidden]
                public <System-Collections-IEnumerable-GetEnumerator>d__48(int <>1__state)
                {
                    this.<>1__state = <>1__state;
                }

                private bool MoveNext()
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<>2__current = this.<>4__this;
                        this.<>1__state = 1;
                        return true;
                    }
                    if (num == 1)
                    {
                        this.<>1__state = -1;
                    }
                    return false;
                }

                [DebuggerHidden]
                void IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }

                [DebuggerHidden]
                void IDisposable.Dispose()
                {
                }

                object IEnumerator<object>.Current =>
                    this.<>2__current;

                object IEnumerator.Current =>
                    this.<>2__current;
            }

            private static class NestedPropertiesHelper
            {
                private static string GetPath(string rootPath, string propertyName)
                {
                    if (string.IsNullOrEmpty(rootPath))
                    {
                        return propertyName;
                    }
                    string[] textArray1 = new string[] { rootPath, propertyName };
                    return string.Join(".", textArray1);
                }

                internal static string GetPropertyName(string path)
                {
                    string str = IsRooted(path) ? path.Substring(0, path.Length - 1) : path;
                    int num = str.LastIndexOf(".", StringComparison.Ordinal);
                    return ((num > 0) ? str.Substring(num + 1) : str);
                }

                internal static string GetRootPath(string path)
                {
                    if (string.IsNullOrEmpty(path))
                    {
                        return null;
                    }
                    int length = path.LastIndexOf(".", StringComparison.Ordinal);
                    return ((length > 0) ? path.Substring(0, length) : null);
                }

                internal static string GetRootPath(PropertyDescriptor[] listAccessors)
                {
                    if ((listAccessors == null) || (listAccessors.Length == 0))
                    {
                        return string.Empty;
                    }
                    Func<PropertyDescriptor, string> selector = <>c.<>9__2_0;
                    if (<>c.<>9__2_0 == null)
                    {
                        Func<PropertyDescriptor, string> local1 = <>c.<>9__2_0;
                        selector = <>c.<>9__2_0 = x => x.Name;
                    }
                    return string.Join(".", listAccessors.Select<PropertyDescriptor, string>(selector));
                }

                internal static bool HasRootPath(string path) => 
                    !string.IsNullOrEmpty(path) ? (path.IndexOf(".", StringComparison.Ordinal) > 0) : false;

                internal static bool IsRooted(string path) => 
                    path.EndsWith(".", StringComparison.Ordinal);

                internal static bool IsRooted(string path, string propertyName, string rootPath)
                {
                    string str = IsRooted(path) ? GetPath(GetPath(rootPath, propertyName), string.Empty) : GetPath(rootPath, propertyName);
                    return path.Equals(str, StringComparison.Ordinal);
                }

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties.NestedPropertiesHelper.<>c <>9 = new FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties.NestedPropertiesHelper.<>c();
                    public static Func<PropertyDescriptor, string> <>9__2_0;

                    internal string <GetRootPath>b__2_0(PropertyDescriptor x) => 
                        x.Name;
                }
            }

            [DebuggerDisplay("{Path}, {PropertyType}")]
            private sealed class ViewModelPropertyDescriptor : PropertyDescriptor
            {
                private readonly string path;

                public ViewModelPropertyDescriptor(string path) : base(FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties.NestedPropertiesHelper.GetPropertyName(path), null)
                {
                    this.path = path;
                }

                public sealed override bool CanResetValue(object component) => 
                    false;

                private IValueViewModel GetValue(FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties bindingList) => 
                    bindingList.Get<FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties, IValueViewModel>(x => x.EnsureValue(this.path), null);

                public sealed override object GetValue(object component)
                {
                    FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties bindingList = component as FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties;
                    return (this.IsContentProperty ? ((object) bindingList) : ((object) this.GetValue(bindingList)));
                }

                public sealed override void ResetValue(object component)
                {
                }

                public sealed override void SetValue(object component, object value)
                {
                }

                public sealed override bool ShouldSerializeValue(object component) => 
                    false;

                public string Path =>
                    this.path;

                public bool IsContentProperty =>
                    FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelBindableProperties.NestedPropertiesHelper.IsRooted(this.path);

                public sealed override Type ComponentType =>
                    typeof(IEndUserFilteringViewModelBindableProperties);

                public sealed override Type PropertyType =>
                    this.IsContentProperty ? typeof(IEndUserFilteringViewModelBindableProperties) : typeof(IValueViewModel);

                public sealed override bool IsReadOnly =>
                    true;
            }
        }

        private sealed class EndUserFilteringViewModelProperties : IEndUserFilteringViewModelProperties, IEnumerable<KeyValuePair<string, Type>>, IEnumerable
        {
            private readonly IEnumerable<KeyValuePair<string, Type>> pairs;

            private EndUserFilteringViewModelProperties(IEnumerable<KeyValuePair<string, Type>> pairs)
            {
                this.pairs = pairs;
            }

            internal EndUserFilteringViewModelProperties(IEndUserFilteringSettings settings, Func<IEndUserFilteringMetric, Type> getType)
            {
                this.pairs = settings.GetPairs<Type>(getType);
            }

            IEndUserFilteringViewModelProperties IEndUserFilteringViewModelProperties.GetNestedProperties(string rootPath) => 
                new FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelProperties(from p in this.pairs
                    where p.Key.StartsWith(rootPath)
                    select p);

            IEnumerator<KeyValuePair<string, Type>> IEnumerable<KeyValuePair<string, Type>>.GetEnumerator() => 
                this.pairs.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => 
                this.pairs.GetEnumerator();
        }

        private sealed class EndUserFilteringViewModelPropertyValues : IEndUserFilteringViewModelPropertyValues, IEnumerable<IEndUserFilteringMetricViewModel>, IEnumerable, IEndUserFilteringCriteriaChangeAware, IServiceProvider
        {
            private readonly IStorage<IEndUserFilteringMetricViewModel> storageCore;
            private readonly WeakReference serviceProviderRef;
            private FilterCriteriaChangeContext filterCriteriaChange;

            internal EndUserFilteringViewModelPropertyValues(IStorage<IEndUserFilteringMetricViewModel> storage, IServiceProvider serviceProvider)
            {
                this.storageCore = storage;
                this.serviceProviderRef = new WeakReference(serviceProvider);
            }

            internal EndUserFilteringViewModelPropertyValues(IEnumerable<IEndUserFilteringMetricViewModel> values, WeakReference serviceProviderRef)
            {
                Func<IEndUserFilteringMetricViewModel, int> getOrder = <>c.<>9__2_0;
                if (<>c.<>9__2_0 == null)
                {
                    Func<IEndUserFilteringMetricViewModel, int> local1 = <>c.<>9__2_0;
                    getOrder = <>c.<>9__2_0 = vm => vm.Metric.Order;
                }
                this.storageCore = new Storage<IEndUserFilteringMetricViewModel>(values, getOrder);
                this.serviceProviderRef = serviceProviderRef;
            }

            private void ApplyFilterCriteriaCore(IEndUserFilteringMetricViewModel metricViewModel, CriteriaOperator criteria)
            {
                (metricViewModel.Value as IEndUserFilteringCriteriaAwareViewModel).Do<IEndUserFilteringCriteriaAwareViewModel>(criteriaAware => criteriaAware.TryParse(metricViewModel.Metric, criteria));
            }

            private bool ApplyFilterCriteriaCore(Func<object> getViewModel, string path, CriteriaOperator criteria)
            {
                if (string.IsNullOrEmpty(path))
                {
                    return false;
                }
                this[path].Do<IEndUserFilteringMetricViewModel>(delegate (IEndUserFilteringMetricViewModel metricViewModel) {
                    metricViewModel.SetParentViewModel(getViewModel());
                    this.ApplyFilterCriteriaCore(metricViewModel, criteria);
                });
                return true;
            }

            IDisposable IEndUserFilteringCriteriaChangeAware.EnterFilterCriteriaChange()
            {
                FilterCriteriaChangeContext filterCriteriaChange = this.filterCriteriaChange;
                if (this.filterCriteriaChange == null)
                {
                    FilterCriteriaChangeContext local1 = this.filterCriteriaChange;
                    filterCriteriaChange = this.filterCriteriaChange = new FilterCriteriaChangeContext(this);
                }
                return filterCriteriaChange;
            }

            void IEndUserFilteringCriteriaChangeAware.QueueFilterCriteriaChange(string path, Action<string> change)
            {
                if (this.filterCriteriaChange != null)
                {
                    this.filterCriteriaChange.Queue(path, change);
                }
                else
                {
                    change.Do<Action<string>>(x => x(path));
                }
            }

            void IEndUserFilteringViewModelPropertyValues.ApplyFilterCriteria(Func<object> getViewModel, CriteriaOperator criteria)
            {
                Func<IEndUserFilteringMetricViewModel, bool> predicate = <>c.<>9__10_0;
                if (<>c.<>9__10_0 == null)
                {
                    Func<IEndUserFilteringMetricViewModel, bool> local1 = <>c.<>9__10_0;
                    predicate = <>c.<>9__10_0 = vm => vm.HasValue;
                }
                Func<IEndUserFilteringMetricViewModel, string> keySelector = <>c.<>9__10_1;
                if (<>c.<>9__10_1 == null)
                {
                    Func<IEndUserFilteringMetricViewModel, string> local2 = <>c.<>9__10_1;
                    keySelector = <>c.<>9__10_1 = x => x.Metric.Path;
                }
                Dictionary<string, IEndUserFilteringMetricViewModel> dictionary = this.storageCore.Where<IEndUserFilteringMetricViewModel>(predicate).ToDictionary<IEndUserFilteringMetricViewModel, string>(keySelector);
                foreach (KeyValuePair<string, CriteriaOperator> pair in CriteriaColumnAffinityResolver.SplitByColumnNames(criteria, null).Item2)
                {
                    if (this.ApplyFilterCriteriaCore(getViewModel, pair.Key, pair.Value))
                    {
                        dictionary.Remove(pair.Key);
                    }
                }
                foreach (KeyValuePair<string, IEndUserFilteringMetricViewModel> pair2 in dictionary)
                {
                    this.ApplyFilterCriteriaCore(pair2.Value, null);
                }
            }

            void IEndUserFilteringViewModelPropertyValues.ApplyFilterCriteria(Func<object> getViewModel, string path, CriteriaOperator criteria)
            {
                this.ApplyFilterCriteriaCore(getViewModel, path, criteria);
            }

            void IEndUserFilteringViewModelPropertyValues.EnsureValueType(string path)
            {
                Func<IEndUserFilteringMetricViewModel, string> func1 = <>c.<>9__4_0;
                if (<>c.<>9__4_0 == null)
                {
                    Func<IEndUserFilteringMetricViewModel, string> local1 = <>c.<>9__4_0;
                    func1 = <>c.<>9__4_0 = vm => vm.Metric.Path;
                }
                Action<IEndUserFilteringMetricViewModel> @do = <>c.<>9__4_1;
                if (<>c.<>9__4_1 == null)
                {
                    Action<IEndUserFilteringMetricViewModel> local2 = <>c.<>9__4_1;
                    @do = <>c.<>9__4_1 = x => x.EnsureValueType();
                }
                this.storageCore[path, func1].Do<IEndUserFilteringMetricViewModel>(@do);
            }

            IEndUserFilteringViewModelPropertyValues IEndUserFilteringViewModelPropertyValues.GetNestedValues(string rootPath) => 
                new FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelPropertyValues(from vm in this.storageCore
                    where vm.Metric.Path.StartsWith(rootPath)
                    select vm, this.serviceProviderRef);

            bool IEndUserFilteringViewModelPropertyValues.ParseFilterCriteria(string path, CriteriaOperator criteria) => 
                false;

            CriteriaOperator IEndUserFilteringViewModelPropertyValues.QueryFilterCriteria(string path, CriteriaOperator criteria) => 
                null;

            IEnumerator<IEndUserFilteringMetricViewModel> IEnumerable<IEndUserFilteringMetricViewModel>.GetEnumerator() => 
                this.storageCore.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => 
                this.storageCore.GetEnumerator();

            object IServiceProvider.GetService(Type serviceType)
            {
                Func<WeakReference, IServiceProvider> get = <>c.<>9__16_0;
                if (<>c.<>9__16_0 == null)
                {
                    Func<WeakReference, IServiceProvider> local1 = <>c.<>9__16_0;
                    get = <>c.<>9__16_0 = x => x.Target as IServiceProvider;
                }
                return this.serviceProviderRef.Get<WeakReference, IServiceProvider>(get, null).GetServiceObj(serviceType);
            }

            public IEndUserFilteringMetricViewModel this[string path]
            {
                get
                {
                    Func<IEndUserFilteringMetricViewModel, string> func1 = <>c.<>9__6_0;
                    if (<>c.<>9__6_0 == null)
                    {
                        Func<IEndUserFilteringMetricViewModel, string> local1 = <>c.<>9__6_0;
                        func1 = <>c.<>9__6_0 = vm => vm.Metric.Path;
                    }
                    IEndUserFilteringMetricViewModel @this = this.storageCore[path, func1];
                    Func<IEndUserFilteringMetricViewModel, IValueViewModel> get = <>c.<>9__6_1;
                    if (<>c.<>9__6_1 == null)
                    {
                        Func<IEndUserFilteringMetricViewModel, IValueViewModel> local2 = <>c.<>9__6_1;
                        get = <>c.<>9__6_1 = x => x.Value;
                    }
                    @this.Get<IEndUserFilteringMetricViewModel, IValueViewModel>(get, null).Do<IValueViewModel>(delegate (IValueViewModel x) {
                        x.Initialize(this);
                    });
                    return @this;
                }
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelPropertyValues.<>c <>9 = new FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelPropertyValues.<>c();
                public static Func<IEndUserFilteringMetricViewModel, int> <>9__2_0;
                public static Func<IEndUserFilteringMetricViewModel, string> <>9__4_0;
                public static Action<IEndUserFilteringMetricViewModel> <>9__4_1;
                public static Func<IEndUserFilteringMetricViewModel, string> <>9__6_0;
                public static Func<IEndUserFilteringMetricViewModel, IValueViewModel> <>9__6_1;
                public static Func<IEndUserFilteringMetricViewModel, bool> <>9__10_0;
                public static Func<IEndUserFilteringMetricViewModel, string> <>9__10_1;
                public static Func<WeakReference, IServiceProvider> <>9__16_0;

                internal int <.ctor>b__2_0(IEndUserFilteringMetricViewModel vm) => 
                    vm.Metric.Order;

                internal bool <DevExpress.Utils.Filtering.Internal.IEndUserFilteringViewModelPropertyValues.ApplyFilterCriteria>b__10_0(IEndUserFilteringMetricViewModel vm) => 
                    vm.HasValue;

                internal string <DevExpress.Utils.Filtering.Internal.IEndUserFilteringViewModelPropertyValues.ApplyFilterCriteria>b__10_1(IEndUserFilteringMetricViewModel x) => 
                    x.Metric.Path;

                internal string <DevExpress.Utils.Filtering.Internal.IEndUserFilteringViewModelPropertyValues.EnsureValueType>b__4_0(IEndUserFilteringMetricViewModel vm) => 
                    vm.Metric.Path;

                internal void <DevExpress.Utils.Filtering.Internal.IEndUserFilteringViewModelPropertyValues.EnsureValueType>b__4_1(IEndUserFilteringMetricViewModel x)
                {
                    x.EnsureValueType();
                }

                internal string <get_Item>b__6_0(IEndUserFilteringMetricViewModel vm) => 
                    vm.Metric.Path;

                internal IValueViewModel <get_Item>b__6_1(IEndUserFilteringMetricViewModel x) => 
                    x.Value;

                internal IServiceProvider <System.IServiceProvider.GetService>b__16_0(WeakReference x) => 
                    x.Target as IServiceProvider;
            }

            private sealed class FilterCriteriaChangeContext : IDisposable
            {
                private int filterCriteriaChangeLocked;
                private FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelPropertyValues values;
                private Action<string> change;
                private readonly List<string> paths = new List<string>();

                internal FilterCriteriaChangeContext(FilteringViewModelPropertyValuesProvider.EndUserFilteringViewModelPropertyValues values)
                {
                    this.values = values;
                    this.filterCriteriaChangeLocked++;
                }

                public void Queue(string path, Action<string> change)
                {
                    this.change = change;
                    if (!this.paths.Contains(path))
                    {
                        this.paths.Add(path);
                    }
                }

                void IDisposable.Dispose()
                {
                    int num = this.filterCriteriaChangeLocked - 1;
                    this.filterCriteriaChangeLocked = num;
                    if (num == 0)
                    {
                        if (this.change != null)
                        {
                            this.change(this.Path);
                        }
                        this.change = null;
                        if (this.values != null)
                        {
                            this.values.filterCriteriaChange = null;
                        }
                        this.values = null;
                        this.paths.Clear();
                    }
                    GC.SuppressFinalize(this);
                }

                private string Path =>
                    (this.paths.Count == 1) ? this.paths[0] : string.Empty;
            }
        }

        protected abstract class MetricValueBoxCore<TValue> : IDisposable
        {
            protected readonly EventHandlerList handlers;
            protected readonly IServiceProvider serviceProvider;
            private readonly IEndUserFilteringMetric metric;
            private readonly IMetricAttributesQueryOwner queryOwner;
            private Lazy<IMetricAttributesQuery> queryCore;
            private Lazy<TValue> valueCore;
            private bool disposing;
            private readonly IDictionary<Type, TValue> cache;
            private int updatingMemberBindings;

            protected MetricValueBoxCore(IServiceProvider serviceProvider, IEndUserFilteringMetric metric, IMetricAttributesQueryOwner queryOwner)
            {
                this.handlers = new EventHandlerList();
                this.cache = new Dictionary<Type, TValue>();
                this.serviceProvider = serviceProvider;
                this.metric = metric;
                this.queryOwner = queryOwner;
            }

            protected IMetricAttributesQuery CreateQuery() => 
                this.GetService<IMetricAttributesQueryFactory>().Get<IMetricAttributesQueryFactory, IMetricAttributesQuery>(factory => factory.CreateQuery(base.metric, base.queryOwner), null);

            protected abstract TValue CreateValue();
            protected void EnsureQuery()
            {
                if (this.queryCore.IsValueCreated)
                {
                    this.queryCore = new Lazy<IMetricAttributesQuery>(new Func<IMetricAttributesQuery>(this.CreateQuery));
                }
            }

            protected void EnsureValue()
            {
                if (this.IsValueCreated)
                {
                    this.OnValueChanged(this.BaseValue);
                    this.valueCore = new Lazy<TValue>(new Func<TValue>(this.GetOrCreateValue));
                }
            }

            protected abstract TValue GetOrCreateValue();
            protected TValue GetOrCreateValue(Type key)
            {
                TValue local = default(TValue);
                if (!this.cache.TryGetValue(key, out local))
                {
                    local = this.CreateValue();
                    this.cache.Add(key, local);
                }
                return local;
            }

            protected TService GetService<TService>() where TService: class => 
                this.serviceProvider.GetService<TService>();

            protected void InitializeQuery()
            {
                this.queryCore = new Lazy<IMetricAttributesQuery>(new Func<IMetricAttributesQuery>(this.CreateQuery));
            }

            protected void InitializeValue()
            {
                this.valueCore = new Lazy<TValue>(new Func<TValue>(this.GetOrCreateValue));
            }

            protected virtual void OnDisposing()
            {
            }

            protected virtual void OnValueChanged(TValue value)
            {
            }

            protected virtual void OnValueCreated(TValue value)
            {
            }

            protected void ReleaseValue(Action<TValue> release)
            {
                if (this.IsValueCreated)
                {
                    release(this.valueCore.Value);
                }
            }

            void IDisposable.Dispose()
            {
                if (!this.disposing)
                {
                    this.disposing = true;
                    this.OnDisposing();
                    this.cache.Clear();
                    this.handlers.Dispose();
                }
                GC.SuppressFinalize(this);
            }

            private void UpdateMemberBindings()
            {
                this.GetService<IViewModelProvider>().Do<IViewModelProvider>(delegate (IViewModelProvider viewModelProvider) {
                    if (viewModelProvider.IsViewModelCreated && !ExcelFilteringUIViewModelProvider.IsSurrogateViewModel(viewModelProvider.ViewModel as IEndUserFilteringViewModel))
                    {
                        base.updatingMemberBindings++;
                        base.Metric.Attributes.UpdateMemberBindings(null, null, base.Query);
                        base.updatingMemberBindings--;
                    }
                });
            }

            protected bool IsDisposing =>
                this.disposing;

            public IEndUserFilteringMetric Metric =>
                this.metric;

            public IMetricAttributesQuery Query =>
                this.queryCore.Value;

            protected bool IsValueCreated =>
                this.valueCore.IsValueCreated;

            protected TValue BaseValue
            {
                get
                {
                    TValue local = this.valueCore.Value;
                    if (!this.IsValueCreated)
                    {
                        if (!this.IsMemberBindingsUpdating)
                        {
                            this.UpdateMemberBindings();
                        }
                        this.OnValueCreated(local);
                    }
                    return local;
                }
            }

            protected bool IsMemberBindingsUpdating =>
                this.updatingMemberBindings > 0;
        }
    }
}

