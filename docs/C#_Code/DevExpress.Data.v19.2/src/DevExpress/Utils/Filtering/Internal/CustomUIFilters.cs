namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
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

    [DebuggerDisplay("ActiveFilter={ActiveFilter}")]
    internal sealed class CustomUIFilters : FilterUIElement<CustomUIFiltersType>, ICustomUIFilters, ILocalizableUIElement<CustomUIFiltersType>, IEnumerable<ICustomUIFilter>, IEnumerable
    {
        private readonly Lazy<ICustomUIFiltersOptions> optionsCore;
        private readonly IEndUserFilteringMetric metricCore;
        private readonly IMetricAttributesQuery queryCore;
        private Lazy<IStorage<CustomUIFilterType, ICustomUIFilter>> childrenCore;
        internal const string FilterCriteriaNotify = ".CustomFilterCriteria";
        private ICustomUIFilter activeFilterCore;
        private ICustomUIFilterValue activeFilterValueCore;
        private Lazy<CriteriaOperator> filterCriteriaCore;
        private WeakReference parentViewModelRef;

        public CustomUIFilters(IEndUserFilteringMetric metric, IMetricAttributesQuery query, CustomUIFiltersType filterType, Func<IServiceProvider> getServiceProvider) : base(filterType, getServiceProvider)
        {
            this.metricCore = metric;
            this.queryCore = query;
            this.optionsCore = new Lazy<ICustomUIFiltersOptions>(new Func<ICustomUIFiltersOptions>(this.CreateOptions));
            this.childrenCore = new Lazy<IStorage<CustomUIFilterType, ICustomUIFilter>>(new Func<IStorage<CustomUIFilterType, ICustomUIFilter>>(this.CreateStorage));
            this.filterCriteriaCore = new Lazy<CriteriaOperator>(new Func<CriteriaOperator>(this.CreateFilterCriteria));
        }

        public bool ApplyFilterCriteria(CriteriaOperator criteria, out ICustomUIFilter filter)
        {
            bool flag;
            using (IEnumerator<ICustomUIFilter> enumerator = CustomUIFilterDatePeriod.ExcludeDataPeriods(this.Children).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        ICustomUIFilter current = enumerator.Current;
                        IEndUserFilteringCriteriaAwareViewModel model = current as IEndUserFilteringCriteriaAwareViewModel;
                        if ((model == null) || !current.Allow(this.UserOptions))
                        {
                            continue;
                        }
                        current.SetParentViewModel(this);
                        if (!model.TryParse(this.Metric, criteria))
                        {
                            continue;
                        }
                        this.SetActiveFilter(current, GetFilterValue(current), true);
                        filter = current;
                        flag = true;
                    }
                    else
                    {
                        this.SetActiveFilter(null, null, true);
                        filter = this.GetDefaultFilter();
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public bool CanReset() => 
            this.activeFilterValueCore != null;

        private CriteriaOperator CreateFilterCriteria() => 
            (this.activeFilterCore as ICustomUIFilterValueViewModel).Get<ICustomUIFilterValueViewModel, CriteriaOperator>(fvm => fvm.CreateFilterCriteria(this.Metric), null);

        private ICustomUIFiltersOptions CreateOptions()
        {
            ICustomUIFiltersOptions options = base.GetService<ICustomUIFiltersOptionsFactory>().Get<ICustomUIFiltersOptionsFactory, ICustomUIFiltersOptions>(factory => factory.Create(this.Metric), null);
            (options as INotifyPropertyChanged).Do<INotifyPropertyChanged>(delegate (INotifyPropertyChanged optionsNPC) {
                optionsNPC.PropertyChanged += new PropertyChangedEventHandler(this.OptionsChanged);
            });
            return options;
        }

        private IStorage<CustomUIFilterType, ICustomUIFilter> CreateStorage() => 
            new CustomUIFilterElementStorage(this, this.GetChildren());

        bool ICustomUIFilters.AllowFilter(ICustomUIFilter filter) => 
            filter.Get<ICustomUIFilter, bool>(x => x.Allow(this.UserOptions), false);

        [IteratorStateMachine(typeof(<GetChildren>d__52))]
        private IEnumerable<ICustomUIFilter> GetChildren()
        {
            <GetChildren>d__52 d__1 = new <GetChildren>d__52(-2);
            d__1.<>4__this = this;
            return d__1;
        }

        private ICustomUIFilter GetDefaultFilter()
        {
            Func<ICustomUIFiltersOptions, CustomUIFilterType> get = <>c.<>9__38_0;
            if (<>c.<>9__38_0 == null)
            {
                Func<ICustomUIFiltersOptions, CustomUIFilterType> local1 = <>c.<>9__38_0;
                get = <>c.<>9__38_0 = opt => opt.DefaultFilterType;
            }
            ICustomUIFilter filter1 = this[this.UserOptions.Get<ICustomUIFiltersOptions, CustomUIFilterType>(get, this.Options.DefaultFilterType)];
            ICustomUIFilter filter2 = filter1;
            if (filter1 == null)
            {
                ICustomUIFilter local2 = filter1;
                filter2 = this[CustomUIFiltersOptions.GetDefaultFilterType(base.id, this.Metric.Type)];
            }
            return filter2;
        }

        private static ICustomUIFilterValue GetFilterValue(ICustomUIFilter value) => 
            value?.Value;

        protected sealed override int GetHash(CustomUIFiltersType id) => 
            (int) id;

        private object GetParentViewModel()
        {
            Func<WeakReference, object> get = <>c.<>9__54_0;
            if (<>c.<>9__54_0 == null)
            {
                Func<WeakReference, object> local1 = <>c.<>9__54_0;
                get = <>c.<>9__54_0 = r => r.Target;
            }
            return this.parentViewModelRef.Get<WeakReference, object>(get, null);
        }

        private ICustomUIFilter GetUserDefinedOrDefaultFilter()
        {
            ICustomUIFilter @this = this[CustomUIFilterType.User];
            return (@this.Get<ICustomUIFilter, bool>(x => x.Allow(this.UserOptions), false) ? @this : this.GetDefaultFilter());
        }

        internal static bool IsFilterCriteriaNotify(PropertyChangedEventArgs e, out string path) => 
            IsFilterCriteriaNotify(e.PropertyName, out path);

        internal static bool IsFilterCriteriaNotify(string propertyName, out string path)
        {
            path = propertyName;
            int length = !string.IsNullOrEmpty(path) ? path.LastIndexOf(".CustomFilterCriteria", StringComparison.Ordinal) : -1;
            if (length != -1)
            {
                path = path.Substring(0, length);
            }
            return (length != -1);
        }

        private void OnParentViewModelChanged()
        {
            if (!ExcelFilteringUIViewModelProvider.IsSurrogateViewModel(this.ParentViewModel))
            {
                this.Metric.Attributes.UpdateMemberBindings(this.ParentViewModel, null, this.Query);
            }
        }

        private void OptionsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.childrenCore.IsValueCreated)
            {
                Action<IDisposable> @do = <>c.<>9__42_0;
                if (<>c.<>9__42_0 == null)
                {
                    Action<IDisposable> local1 = <>c.<>9__42_0;
                    @do = <>c.<>9__42_0 = disposable => disposable.Dispose();
                }
                (this.childrenCore.Value as IDisposable).Do<IDisposable>(@do);
                this.childrenCore = new Lazy<IStorage<CustomUIFilterType, ICustomUIFilter>>(new Func<IStorage<CustomUIFilterType, ICustomUIFilter>>(this.CreateStorage));
            }
        }

        public void Reset()
        {
            this.ActiveFilter.Reset();
            this.SetActiveFilter(null, null, false);
            base.RaisePropertyChanged(null);
        }

        private void ResetFilterCriteriaCore()
        {
            if (this.filterCriteriaCore.IsValueCreated)
            {
                this.filterCriteriaCore = new Lazy<CriteriaOperator>(new Func<CriteriaOperator>(this.CreateFilterCriteria));
            }
        }

        private void SetActiveFilter(ICustomUIFilter filter, ICustomUIFilterValue filterValue, bool notifyActiveFilter)
        {
            ICustomUIFilter activeFilterCore = this.activeFilterCore;
            this.activeFilterCore = filter;
            this.activeFilterValueCore = filterValue;
            if (notifyActiveFilter)
            {
                this.RaisePropertyChanged<ICustomUIFilter>(Expression.Lambda<Func<ICustomUIFilter>>(Expression.Property(Expression.Constant(this, typeof(CustomUIFilters)), (MethodInfo) methodof(CustomUIFilters.get_ActiveFilter)), new ParameterExpression[0]));
            }
            this.ResetFilterCriteriaCore();
            if (!ReferenceEquals(activeFilterCore, filter))
            {
                CustomUIFilter.Assign(activeFilterCore, null);
            }
        }

        IEnumerator<ICustomUIFilter> IEnumerable<ICustomUIFilter>.GetEnumerator() => 
            this.Children.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            this.Children.GetEnumerator();

        public IEndUserFilteringMetric Metric =>
            this.metricCore;

        public IMetricAttributesQuery Query =>
            this.queryCore;

        private IStorage<CustomUIFilterType, ICustomUIFilter> Children =>
            this.childrenCore.Value;

        public ICustomUIFilter this[CustomUIFilterType filterType]
        {
            get
            {
                Func<ICustomUIFilter, CustomUIFilterType> func1 = <>c.<>9__13_0;
                if (<>c.<>9__13_0 == null)
                {
                    Func<ICustomUIFilter, CustomUIFilterType> local1 = <>c.<>9__13_0;
                    func1 = <>c.<>9__13_0 = child => child.GetID();
                }
                return this.Children[filterType, func1];
            }
        }

        public IEnumerable<IGrouping<string, ICustomUIFilter>> Groups
        {
            get
            {
                Func<ICustomUIFilter, string> keySelector = <>c.<>9__15_0;
                if (<>c.<>9__15_0 == null)
                {
                    Func<ICustomUIFilter, string> local1 = <>c.<>9__15_0;
                    keySelector = <>c.<>9__15_0 = child => child.Group;
                }
                return this.Children.GroupBy<ICustomUIFilter, string>(keySelector);
            }
        }

        public ICustomUIFiltersOptions Options =>
            this.optionsCore.Value;

        public ICustomUIFiltersOptions UserOptions { get; set; }

        public sealed override string Name =>
            ((((CustomUIFiltersType) base.id) != CustomUIFiltersType.DateTime) || !this.IsTimeSpanRange) ? base.Name : CustomUIFiltersEx.Duration.Name;

        public sealed override string Description =>
            ((((CustomUIFiltersType) base.id) != CustomUIFiltersType.DateTime) || !this.IsTimeSpanRange) ? base.Description : CustomUIFiltersEx.Duration.Description;

        private bool IsTimeSpanRange
        {
            get
            {
                IRangeMetricAttributes attributes = (this.metricCore != null) ? (this.Metric.Attributes as IRangeMetricAttributes) : null;
                return ((attributes != null) && attributes.IsTimeSpanRange);
            }
        }

        public ICustomUIFilter ActiveFilter
        {
            get => 
                this.activeFilterCore ?? this.GetUserDefinedOrDefaultFilter();
            internal set
            {
                ICustomUIFilterValue filterValue = GetFilterValue(value);
                if (!ReferenceEquals(this.activeFilterCore, value) || !ReferenceEquals(filterValue, this.activeFilterValueCore))
                {
                    this.SetActiveFilter(value, filterValue, true);
                    this.RaisePropertyChanged<CriteriaOperator>(Expression.Lambda<Func<CriteriaOperator>>(Expression.Property(Expression.Constant(this, typeof(CustomUIFilters)), (MethodInfo) methodof(CustomUIFilters.get_FilterCriteria)), new ParameterExpression[0]));
                    this.ParentViewModel.RaisePropertyChanged(this.Metric.Path + ".CustomFilterCriteria");
                }
            }
        }

        public CriteriaOperator FilterCriteria =>
            this.filterCriteriaCore.Value;

        private object ParentViewModel
        {
            get => 
                this.GetParentViewModel();
            set
            {
                object parentViewModel = this.GetParentViewModel();
                if ((parentViewModel != value) && (!ExcelFilteringUIViewModelProvider.IsSurrogateViewModel(value) || (parentViewModel == null)))
                {
                    this.parentViewModelRef = new WeakReference(value);
                    this.OnParentViewModelChanged();
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomUIFilters.<>c <>9 = new CustomUIFilters.<>c();
            public static Func<ICustomUIFilter, CustomUIFilterType> <>9__13_0;
            public static Func<ICustomUIFilter, string> <>9__15_0;
            public static Func<ICustomUIFiltersOptions, CustomUIFilterType> <>9__38_0;
            public static Action<IDisposable> <>9__42_0;
            public static Func<WeakReference, object> <>9__54_0;

            internal string <get_Groups>b__15_0(ICustomUIFilter child) => 
                child.Group;

            internal CustomUIFilterType <get_Item>b__13_0(ICustomUIFilter child) => 
                child.GetID();

            internal CustomUIFilterType <GetDefaultFilter>b__38_0(ICustomUIFiltersOptions opt) => 
                opt.DefaultFilterType;

            internal object <GetParentViewModel>b__54_0(WeakReference r) => 
                r.Target;

            internal void <OptionsChanged>b__42_0(IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        [CompilerGenerated]
        private sealed class <GetChildren>d__52 : IEnumerable<ICustomUIFilter>, IEnumerable, IEnumerator<ICustomUIFilter>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private ICustomUIFilter <>2__current;
            private int <>l__initialThreadId;
            public CustomUIFilters <>4__this;
            private ICustomUIFilterFactory <factory>5__1;
            private IEnumerator<CustomUIFilterType> <>7__wrap1;

            [DebuggerHidden]
            public <GetChildren>d__52(int <>1__state)
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
                        ICustomUIFilterTypesResolver service = this.<>4__this.GetService<ICustomUIFilterTypesResolver>();
                        this.<factory>5__1 = this.<>4__this.GetService<ICustomUIFilterFactory>();
                        if ((service == null) || (this.<factory>5__1 == null))
                        {
                            return false;
                        }
                        else
                        {
                            this.<>7__wrap1 = service.Resolve(this.<>4__this.Metric, this.<>4__this.id, this.<>4__this.Options).GetEnumerator();
                            this.<>1__state = -3;
                        }
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
                        CustomUIFilterType current = this.<>7__wrap1.Current;
                        this.<>2__current = this.<factory>5__1.Create(current, this.<>4__this.getServiceProvider);
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
            IEnumerator<ICustomUIFilter> IEnumerable<ICustomUIFilter>.GetEnumerator()
            {
                CustomUIFilters.<GetChildren>d__52 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new CustomUIFilters.<GetChildren>d__52(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Utils.Filtering.Internal.ICustomUIFilter>.GetEnumerator();

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

            ICustomUIFilter IEnumerator<ICustomUIFilter>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        private sealed class CustomUIFilterElementStorage : StorageBase<CustomUIFilterType, ICustomUIFilter>, IDisposable
        {
            private readonly WeakReference ownerRef;

            internal CustomUIFilterElementStorage(CustomUIFilters filters, IEnumerable<ICustomUIFilter> children) : this(children, func1)
            {
                Func<ICustomUIFilter, int> func1 = <>c.<>9__1_0;
                if (<>c.<>9__1_0 == null)
                {
                    Func<ICustomUIFilter, int> local1 = <>c.<>9__1_0;
                    func1 = <>c.<>9__1_0 = child => child.Order;
                }
                this.ownerRef = new WeakReference(filters);
            }

            protected sealed override ICustomUIFilter Actualize(CustomUIFilterType path, ICustomUIFilter filter)
            {
                (this.ownerRef.Target as CustomUIFilters).Do<CustomUIFilters>(delegate (CustomUIFilters owner) {
                    filter.SetParentViewModel(owner);
                });
                (filter as INotifyPropertyChanged).Do<INotifyPropertyChanged>(delegate (INotifyPropertyChanged npc) {
                    npc.PropertyChanged += new PropertyChangedEventHandler(this.Element_PropertyChanged);
                });
                return filter;
            }

            private static ICustomUIFilter CalcFilter(ICustomUIFilters filters, ICustomUIFilter value)
            {
                if (value == null)
                {
                    return null;
                }
                if ((value.Value == null) || value.Value.IsDefault)
                {
                    return (!value.IsActive ? null : value);
                }
                if (value.Value == null)
                {
                    return value;
                }
                ICustomUIFilter filter = value;
                CustomUIFilterType sourceFilterType = value.Value.FilterType;
                value = filters.FirstOrDefault<ICustomUIFilter>(filter => ((CustomUIFilterType) filter.GetID()) == sourceFilterType);
                return CustomUIFilter.Assign(value, filter);
            }

            private void Element_PropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                (this.ownerRef.Target as CustomUIFilters).Do<CustomUIFilters>(owner => owner.ActiveFilter = CalcFilter(owner, sender as ICustomUIFilter));
            }

            void IDisposable.Dispose()
            {
                base.ReleaseElements(filter => (filter as INotifyPropertyChanged).Do<INotifyPropertyChanged>(delegate (INotifyPropertyChanged npc) {
                    npc.PropertyChanged -= new PropertyChangedEventHandler(this.Element_PropertyChanged);
                }));
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly CustomUIFilters.CustomUIFilterElementStorage.<>c <>9 = new CustomUIFilters.CustomUIFilterElementStorage.<>c();
                public static Func<ICustomUIFilter, int> <>9__1_0;

                internal int <.ctor>b__1_0(ICustomUIFilter child) => 
                    child.Order;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        private sealed class CustomUIFiltersEx : LocalizableUIElement<CustomUIFilters.CustomUIFiltersEx.CustomUIFiltersTypeEx>
        {
            internal static readonly CustomUIFilters.CustomUIFiltersEx Duration = new CustomUIFilters.CustomUIFiltersEx(CustomUIFiltersTypeEx.Duration);

            private CustomUIFiltersEx(CustomUIFiltersTypeEx type) : base(type)
            {
            }

            protected sealed override int GetHash(CustomUIFiltersTypeEx id) => 
                (int) id;

            public enum CustomUIFiltersTypeEx
            {
                Duration = 100
            }
        }
    }
}

