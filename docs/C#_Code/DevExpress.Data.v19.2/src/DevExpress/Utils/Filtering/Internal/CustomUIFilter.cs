namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Data.Helpers;
    using DevExpress.Data.Utils;
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
    using System.Threading.Tasks;

    internal abstract class CustomUIFilter : FilterUIElement<CustomUIFilterType>, ICustomUIFilter, ILocalizableUIElement<CustomUIFilterType>, ICustomUIFilterValueViewModel, IEndUserFilteringCriteriaAwareViewModel
    {
        private ICustomUIFilterCriteriaParser criteriaParser;
        private static readonly Type CustomUIFilterTypeEnum = typeof(CustomUIFilterType);
        private static readonly IDictionary<CustomUIFilterType, AnnotationAttributes> annotationAttributes = new Dictionary<CustomUIFilterType, AnnotationAttributes>();
        private string parentGroup;
        private ICustomUIFilterValue valueCore;
        private WeakReference parentRef;
        private Lazy<ICustomUIFilterSummaryItem> summaryItemCore;
        private IDisposable endEditToken;

        public CustomUIFilter(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) : base(filterType, getServiceProvider)
        {
            this.summaryItemCore = new Lazy<ICustomUIFilterSummaryItem>(new Func<ICustomUIFilterSummaryItem>(this.CreateSummaryItem));
        }

        protected virtual bool AllowCore(ICustomUIFiltersOptions userOptions) => 
            true;

        internal static ICustomUIFilter Assign(ICustomUIFilter @this, ICustomUIFilter filter)
        {
            if ((@this != null) && !ReferenceEquals(@this, filter))
            {
                if (filter != null)
                {
                    ICustomUIFilterValue value = filter.Value;
                    if ((value != null) && (value.FilterType == ((CustomUIFilterType) @this.GetID())))
                    {
                        (@this as CustomUIFilter).Do<CustomUIFilter>(delegate (CustomUIFilter _) {
                            _.SetValueCore(value);
                        });
                    }
                }
                else
                {
                    Action<CustomUIFilter> @do = <>c.<>9__65_0;
                    if (<>c.<>9__65_0 == null)
                    {
                        Action<CustomUIFilter> local1 = <>c.<>9__65_0;
                        @do = <>c.<>9__65_0 = delegate (CustomUIFilter _) {
                            _.SetValueCore(null);
                        };
                    }
                    (@this as CustomUIFilter).Do<CustomUIFilter>(@do);
                }
            }
            return @this;
        }

        protected static object CheckNullObject(ICustomUIFilterValue filterValue) => 
            CheckNullObject(filterValue.Value);

        protected static object CheckNullObject(object value) => 
            (value == BaseRowsKeeper.NullObject) ? null : value;

        protected abstract ICustomUIFilterCriteriaParser CreateCriteriaParser(IEndUserFilteringMetric metric);
        private ICustomUIFilterDialogViewModel CreateCustomUIFilterDialogViewModel()
        {
            CustomUIFilterDialogType dialogType = base.GetService<ICustomUIFilterDialogTypesResolver>().Get<ICustomUIFilterDialogTypesResolver, CustomUIFilterDialogType>(resolver => resolver.Resolve(base.id), (CustomUIFilterDialogType) 0);
            return new CustomUIFilterDialogViewModel(this.GetPath(), dialogType, base.getServiceProvider, this.GetFiltersType(), base.id, this.GetCustomUIFilterDialogViewModelParameter(this.GetActiveFilter()));
        }

        private CriteriaOperator CreateFilterCriteriaCore(IEndUserFilteringMetric metric, ICustomUIFilterValue filterValue) => 
            ((filterValue == null) || filterValue.IsDefault) ? null : this.GetCriteria(metric, filterValue);

        protected virtual ICustomUIFilterSummaryItem CreateSummaryItem() => 
            null;

        protected virtual ICustomUIFilterValue CreateValue(object[] values) => 
            base.GetService<ICustomUIFilterValuesFactory>().Get<ICustomUIFilterValuesFactory, ICustomUIFilterValue>(factory => factory.Create(this.id, values), null);

        private void CustomUIFilterDialogViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            (sender as ICustomUIFilterDialogViewModel).Do<ICustomUIFilterDialogViewModel>(viewModel => this.SyncValue(viewModel, e));
        }

        bool ICustomUIFilter.Allow(ICustomUIFiltersOptions userOptions) => 
            userOptions.Get<ICustomUIFiltersOptions, bool>(opt => opt.AllowFilters && this.AllowCore(opt), this.AllowCore(null));

        CriteriaOperator ICustomUIFilterValueViewModel.CreateFilterCriteria(IEndUserFilteringMetric metric) => 
            this.CreateFilterCriteriaCore(metric, this.Value);

        CriteriaOperator ICustomUIFilterValueViewModel.CreateFilterCriteria(IEndUserFilteringMetric metric, ICustomUIFilterValue filterValue) => 
            this.CreateFilterCriteriaCore(metric, filterValue);

        bool IEndUserFilteringCriteriaAwareViewModel.TryParse(IEndUserFilteringMetric metric, CriteriaOperator criteria)
        {
            if (criteria == null)
            {
                this.SetValueCore(null);
                return false;
            }
            if (this.EnsureCriteriaParser(metric))
            {
                ICustomUIFilterValue value2;
                if (this.TryParseCriteria(metric, criteria, out value2))
                {
                    this.SetValueCore(value2);
                    return true;
                }
                this.SetValueCore(null);
            }
            return false;
        }

        public Task Edit(object uiProvider)
        {
            ICustomUIFilterDialogViewModel viewModel = this.CreateCustomUIFilterDialogViewModel();
            ICustomUIFilterDialogService service = base.GetService<ICustomUIFilterDialogService>();
            ICustomUIFilterDialogDispatcherService dispatcherService = base.GetService<ICustomUIFilterDialogDispatcherService>();
            ICustomUIFilterValue savedValue = this.Value;
            this.SubscribeCustomUIFilterDialogViewModel(viewModel);
            this.QueryViewModelResult(viewModel);
            return service.Show(uiProvider, viewModel, out this.endEditToken).ContinueWith<bool?>(delegate (System.Threading.Tasks.Task<bool> edit) {
                this.UnsubscribeCustomUIFilterDialogViewModel(viewModel);
                if (edit.IsCanceled)
                {
                    return null;
                }
                if (!edit.Result)
                {
                    Action <>9__1;
                    Action action = <>9__1;
                    if (<>9__1 == null)
                    {
                        Action local1 = <>9__1;
                        action = <>9__1 = delegate {
                            this.ResetViewModelResult(savedValue);
                        };
                    }
                    dispatcherService.Queue(action).Wait();
                }
                return new bool?(edit.Result);
            });
        }

        public void EndEdit()
        {
            if (this.endEditToken != null)
            {
                this.endEditToken.Dispose();
            }
        }

        private static AnnotationAttributes EnsureAnnotationAttributes(CustomUIFilterType id)
        {
            AnnotationAttributes annotationAttributes;
            if (!CustomUIFilter.annotationAttributes.TryGetValue(id, out annotationAttributes))
            {
                annotationAttributes = AnnotationAttributes.GetAnnotationAttributes(CustomUIFilterTypeEnum, id);
                CustomUIFilter.annotationAttributes.Add(id, annotationAttributes);
            }
            return annotationAttributes;
        }

        private bool EnsureCriteriaParser(IEndUserFilteringMetric metric)
        {
            this.criteriaParser ??= this.CreateCriteriaParser(metric);
            return (this.criteriaParser != null);
        }

        protected ICustomUIFilter GetActiveFilter()
        {
            Func<ICustomUIFilters, ICustomUIFilter> get = <>c.<>9__38_0;
            if (<>c.<>9__38_0 == null)
            {
                Func<ICustomUIFilters, ICustomUIFilter> local1 = <>c.<>9__38_0;
                get = <>c.<>9__38_0 = filters => filters.ActiveFilter;
            }
            return (this.ParentViewModel as ICustomUIFilters).Get<ICustomUIFilters, ICustomUIFilter>(get, null);
        }

        protected abstract CriteriaOperator GetCriteria(IEndUserFilteringMetric metric, ICustomUIFilterValue filterValue);
        protected virtual ICustomUIFilterValue GetCustomUIFilterDialogViewModelParameter(ICustomUIFilter activeFilter) => 
            this.Value;

        protected CustomUIFiltersType GetFiltersType()
        {
            Func<ICustomUIFilters, CustomUIFiltersType> get = <>c.<>9__36_0;
            if (<>c.<>9__36_0 == null)
            {
                Func<ICustomUIFilters, CustomUIFiltersType> local1 = <>c.<>9__36_0;
                get = <>c.<>9__36_0 = filters => filters.GetID();
            }
            return (this.ParentViewModel as ICustomUIFilters).Get<ICustomUIFilters, CustomUIFiltersType>(get, CustomUIFiltersType.Numeric);
        }

        protected sealed override int GetHash(CustomUIFilterType id) => 
            (int) id;

        protected IMetricAttributes GetMetricAttributes()
        {
            Func<ICustomUIFilters, IMetricAttributes> get = <>c.<>9__37_0;
            if (<>c.<>9__37_0 == null)
            {
                Func<ICustomUIFilters, IMetricAttributes> local1 = <>c.<>9__37_0;
                get = <>c.<>9__37_0 = filters => filters.Metric.Attributes;
            }
            return (this.ParentViewModel as ICustomUIFilters).Get<ICustomUIFilters, IMetricAttributes>(get, null);
        }

        protected TAttributes GetMetricAttributes<TAttributes>() where TAttributes: class, IMetricAttributes => 
            this.GetMetricAttributes() as TAttributes;

        protected string GetPath()
        {
            Func<ICustomUIFilters, string> get = <>c.<>9__35_0;
            if (<>c.<>9__35_0 == null)
            {
                Func<ICustomUIFilters, string> local1 = <>c.<>9__35_0;
                get = <>c.<>9__35_0 = filters => filters.Metric.Path;
            }
            return (this.ParentViewModel as ICustomUIFilters).Get<ICustomUIFilters, string>(get, null);
        }

        internal static object GetValue(object value, Type type)
        {
            if (CheckNullObject(value) == null)
            {
                return null;
            }
            Type c = value.GetType();
            return (!type.IsAssignableFrom(c) ? (!typeof(IConvertible).IsAssignableFrom(c) ? value : Convert.ChangeType(value, DevExpress.Utils.Filtering.Internal.TypeHelper.GetConversionType(type))) : value);
        }

        protected virtual void OnValueChanged()
        {
        }

        protected virtual void QueryViewModelResult(ICustomUIFilterDialogViewModel viewModel)
        {
        }

        protected virtual void QueryViewModelResultFromSummaryItem(ICustomUIFilterDialogViewModel viewModel, object controller)
        {
            object obj2;
            if ((this.SummaryItem != null) && this.SummaryItem.QueryValue(controller, out obj2))
            {
                object[] values = new object[] { obj2 };
                viewModel.SetResult(values);
            }
        }

        private T ReadAnnotationAttributes<T>(Func<AnnotationAttributes, T> readValue)
        {
            IDictionary<CustomUIFilterType, AnnotationAttributes> annotationAttributes = CustomUIFilter.annotationAttributes;
            lock (annotationAttributes)
            {
                return readValue(EnsureAnnotationAttributes(base.id));
            }
        }

        public bool Reset() => 
            this.SetValueCore(null, true);

        protected void ResetSummaryItem()
        {
            if (this.summaryItemCore.IsValueCreated)
            {
                this.summaryItemCore = new Lazy<ICustomUIFilterSummaryItem>(new Func<ICustomUIFilterSummaryItem>(this.CreateSummaryItem));
            }
        }

        private void ResetViewModelResult(ICustomUIFilterValue savedValue)
        {
            this.SetValueCore(savedValue, this.IsActive);
        }

        internal bool SetValueCore(ICustomUIFilterValue value)
        {
            if (Equals(this.valueCore, value))
            {
                return false;
            }
            this.valueCore = value;
            this.OnValueChanged();
            return true;
        }

        internal bool SetValueCore(ICustomUIFilterValue value, bool notify)
        {
            if (!this.SetValueCore(value))
            {
                return false;
            }
            if (notify)
            {
                this.RaisePropertyChanged<ICustomUIFilterValue>(Expression.Lambda<Func<ICustomUIFilterValue>>(Expression.Property(Expression.Constant(this, typeof(CustomUIFilter)), (MethodInfo) methodof(CustomUIFilter.get_Value)), new ParameterExpression[0]));
            }
            return true;
        }

        private void SubscribeCustomUIFilterDialogViewModel(ICustomUIFilterDialogViewModel viewModel)
        {
            (viewModel as INotifyPropertyChanged).Do<INotifyPropertyChanged>(delegate (INotifyPropertyChanged npc) {
                npc.PropertyChanged += new PropertyChangedEventHandler(this.CustomUIFilterDialogViewModel_PropertyChanged);
            });
        }

        private void SyncValue(ICustomUIFilterDialogViewModel viewModel, PropertyChangedEventArgs e)
        {
            if ((e.PropertyName == "Result") && !CustomUIFilterDialogViewModel.AreEqualOrDefault(this.Value, viewModel.Result))
            {
                this.SetValueCore(viewModel.Result, true);
            }
        }

        internal static bool TryGetValue<T>(CriteriaOperator expression, out T value)
        {
            OperandValue value2;
            object obj2;
            value = default(T);
            if (!expression.Is<OperandValue>(out value2))
            {
                return false;
            }
            if (TryGetValue(value2, typeof(int), out obj2) && (obj2 is T))
            {
                value = (T) obj2;
            }
            return (obj2 is T);
        }

        internal static bool TryGetValue(OperandValue value, Type type, out object result)
        {
            try
            {
                result = GetValue(value.Value, type);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        private bool TryParseCriteria(IEndUserFilteringMetric metric, CriteriaOperator criteria, out ICustomUIFilterValue value)
        {
            object[] objArray;
            value = null;
            if (this.criteriaParser.TryParse(criteria, out objArray))
            {
                value = this.CreateValue(objArray);
            }
            return (value != null);
        }

        private void UnsubscribeCustomUIFilterDialogViewModel(ICustomUIFilterDialogViewModel viewModel)
        {
            (viewModel as INotifyPropertyChanged).Do<INotifyPropertyChanged>(delegate (INotifyPropertyChanged npc) {
                npc.PropertyChanged -= new PropertyChangedEventHandler(this.CustomUIFilterDialogViewModel_PropertyChanged);
            });
        }

        int ICustomUIFilter.Order
        {
            get
            {
                Func<AnnotationAttributes, int> readValue = <>c.<>9__16_0;
                if (<>c.<>9__16_0 == null)
                {
                    Func<AnnotationAttributes, int> local1 = <>c.<>9__16_0;
                    readValue = <>c.<>9__16_0 = a => AnnotationAttributes.GetColumnIndex(a, 0);
                }
                return this.ReadAnnotationAttributes<int>(readValue);
            }
        }

        string ICustomUIFilter.Group =>
            this.ReadAnnotationAttributes<string>(new Func<AnnotationAttributes, string>(AnnotationAttributes.GetGroupName));

        string ICustomUIFilter.ParentGroup
        {
            get
            {
                if (this.parentGroup == null)
                {
                    string group = this.ReadAnnotationAttributes<string>(new Func<AnnotationAttributes, string>(AnnotationAttributes.GetGroupName));
                    int length = group.Get<string, int>(g => group.LastIndexOf(@"\"), -1);
                    this.parentGroup = (length != -1) ? group.Substring(0, length) : string.Empty;
                }
                return this.parentGroup;
            }
        }

        bool ICustomUIFilter.Visible =>
            this.ReadAnnotationAttributes<bool>(new Func<AnnotationAttributes, bool>(AnnotationAttributes.GetAutoGenerateFilter));

        public ICustomUIFilterValue Value =>
            this.valueCore;

        public object ParentViewModel
        {
            get => 
                this.parentRef?.Target;
            set => 
                this.parentRef = (value == null) ? null : new WeakReference(value);
        }

        protected ICustomUIFilterSummaryItem SummaryItem =>
            this.summaryItemCore.Value;

        public bool IsActive =>
            Equals(this, this.GetActiveFilter());

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomUIFilter.<>c <>9 = new CustomUIFilter.<>c();
            public static Func<AnnotationAttributes, int> <>9__16_0;
            public static Func<ICustomUIFilters, string> <>9__35_0;
            public static Func<ICustomUIFilters, CustomUIFiltersType> <>9__36_0;
            public static Func<ICustomUIFilters, IMetricAttributes> <>9__37_0;
            public static Func<ICustomUIFilters, ICustomUIFilter> <>9__38_0;
            public static Action<CustomUIFilter> <>9__65_0;

            internal void <Assign>b__65_0(CustomUIFilter _)
            {
                _.SetValueCore(null);
            }

            internal int <DevExpress.Utils.Filtering.Internal.ICustomUIFilter.get_Order>b__16_0(AnnotationAttributes a) => 
                AnnotationAttributes.GetColumnIndex(a, 0);

            internal ICustomUIFilter <GetActiveFilter>b__38_0(ICustomUIFilters filters) => 
                filters.ActiveFilter;

            internal CustomUIFiltersType <GetFiltersType>b__36_0(ICustomUIFilters filters) => 
                filters.GetID();

            internal IMetricAttributes <GetMetricAttributes>b__37_0(ICustomUIFilters filters) => 
                filters.Metric.Attributes;

            internal string <GetPath>b__35_0(ICustomUIFilters filters) => 
                filters.Metric.Path;
        }

        protected abstract class BaseCustomUIFilterCriteriaParser : ICustomUIFilterCriteriaParser
        {
            private readonly string path;
            protected readonly CustomUIFilterType filterType;
            private bool invalid;

            protected BaseCustomUIFilterCriteriaParser(CustomUIFilterType filterType, string path)
            {
                this.filterType = filterType;
                this.path = path;
            }

            protected CustomUIFilterType GetFilterType(bool? isInverted, IDictionary<CustomUIFilterType, CustomUIFilterType> inversionMap) => 
                isInverted.GetValueOrDefault() ? inversionMap[this.filterType] : this.filterType;

            protected CustomUIFilterType GetFilterType(bool? isInverted, CustomUIFilterType invertedType, CustomUIFilterType type) => 
                isInverted.GetValueOrDefault() ? invertedType : type;

            protected bool IsInvalid(OperandProperty property) => 
                (property == null) || (property.PropertyName != this.path);

            protected virtual object[] LocalValuesReady(object[] localValues) => 
                localValues;

            protected void MarkInvalid()
            {
                this.invalid = true;
            }

            protected abstract void ParseCore(CriteriaOperator criteria);
            protected object[] Prepare(object[] values)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = null;
                }
                return values;
            }

            protected object[] Prepare(ref bool? inversionFlag)
            {
                inversionFlag = 0;
                return null;
            }

            protected object[] Prepare(object[] values, ref bool? inversionFlag)
            {
                inversionFlag = 0;
                return this.Prepare(values);
            }

            protected virtual object[] PrepareLocalValues() => 
                null;

            public bool TryParse(CriteriaOperator criteria, out object[] values)
            {
                this.invalid = false;
                values = null;
                object[] localValues = this.PrepareLocalValues();
                this.ParseCore(criteria);
                if (this.invalid)
                {
                    return false;
                }
                values = this.LocalValuesReady(localValues);
                return true;
            }
        }

        protected abstract class CustomUIFilterCriteriaParser : CustomUIFilter.BaseCustomUIFilterCriteriaParser, IClientCriteriaVisitor, ICriteriaVisitor
        {
            protected CustomUIFilterCriteriaParser(CustomUIFilterType filterType, string path) : base(filterType, path)
            {
            }

            void IClientCriteriaVisitor.Visit(AggregateOperand theOperand)
            {
                this.OnAggregateOperand(theOperand);
            }

            void IClientCriteriaVisitor.Visit(JoinOperand theOperand)
            {
                this.OnJoinOperand(theOperand);
            }

            void IClientCriteriaVisitor.Visit(OperandProperty theOperand)
            {
                this.OnOperandProperty(theOperand);
            }

            void ICriteriaVisitor.Visit(BetweenOperator theOperator)
            {
                this.OnBetweenOperator(theOperator);
            }

            void ICriteriaVisitor.Visit(BinaryOperator theOperator)
            {
                this.OnBinaryOperator(theOperator);
            }

            void ICriteriaVisitor.Visit(FunctionOperator theOperator)
            {
                this.OnFunctionOperator(theOperator);
            }

            void ICriteriaVisitor.Visit(GroupOperator theOperator)
            {
                this.OnGroupOperator(theOperator);
            }

            void ICriteriaVisitor.Visit(InOperator theOperator)
            {
                this.OnInOperator(theOperator);
            }

            void ICriteriaVisitor.Visit(OperandValue theOperand)
            {
                this.OnOperandValue(theOperand.Value);
            }

            void ICriteriaVisitor.Visit(UnaryOperator theOperator)
            {
                this.OnUnaryOperator(theOperator);
            }

            protected virtual void OnAggregateOperand(AggregateOperand theOperand)
            {
                base.MarkInvalid();
            }

            protected virtual void OnBetweenOperator(BetweenOperator theOperator)
            {
                base.MarkInvalid();
            }

            protected virtual void OnBinaryOperator(BinaryOperator theOperator)
            {
                base.MarkInvalid();
            }

            protected virtual void OnFunctionOperator(FunctionOperator theOperator)
            {
                base.MarkInvalid();
            }

            protected virtual void OnGroupOperator(GroupOperator theOperator)
            {
                base.MarkInvalid();
            }

            protected virtual void OnInOperator(InOperator theOperator)
            {
                base.MarkInvalid();
            }

            protected virtual void OnJoinOperand(JoinOperand theOperand)
            {
                base.MarkInvalid();
            }

            protected virtual void OnOperandProperty(OperandProperty theOperand)
            {
                base.MarkInvalid();
            }

            protected virtual void OnOperandValue(object operandValue)
            {
            }

            protected virtual void OnUnaryOperator(UnaryOperator theOperator)
            {
                base.MarkInvalid();
            }

            protected override void ParseCore(CriteriaOperator criteria)
            {
                criteria.Accept(this);
            }
        }

        protected class CustomUIFilterSummaryItem : ICustomUIFilterSummaryItem
        {
            public CustomUIFilterSummaryItem(string fieldName)
            {
                this.Column = fieldName;
            }

            private ISummaryDataController GetSummaryDataController(object controller)
            {
                ISummaryDataController controller2 = controller as ISummaryDataController;
                if (controller2 == null)
                {
                    DataController controler = controller as DataController;
                    if (controler != null)
                    {
                        controller2 = new DataControllerWrapper(controler);
                    }
                }
                return controller2;
            }

            private SummaryItem GetSummaryItem(ISummaryDataController controller) => 
                (from q in controller.Summary
                    where (q.Tag is CustomUIFilter.CustomUIFilterSummaryItem) && Equals(q.Tag, this)
                    select q).FirstOrDefault<SummaryItem>();

            public bool QueryValue(object controller, out object value)
            {
                ISummaryDataController summaryDataController = this.GetSummaryDataController(controller);
                if (summaryDataController == null)
                {
                    value = null;
                    return false;
                }
                bool flag = false;
                SummaryItem summaryItem = null;
                summaryDataController.Summary.BeginUpdate();
                try
                {
                    IDataColumnInfo column = summaryDataController.GetColumn(this.Column);
                    if (column != null)
                    {
                        summaryItem = this.GetSummaryItem(summaryDataController);
                        if (summaryItem == null)
                        {
                            summaryItem = summaryDataController.Summary.Add(column, this.Type, this.Argument, this);
                            flag = true;
                        }
                        else
                        {
                            flag = (summaryItem.SummaryTypeEx != this.Type) || (summaryItem.SummaryArgument != this.Argument);
                            summaryItem.SummaryTypeEx = this.Type;
                            summaryItem.SummaryArgument = this.Argument;
                        }
                    }
                }
                finally
                {
                    if (flag)
                    {
                        summaryDataController.Summary.EndUpdate();
                    }
                    else
                    {
                        summaryDataController.Summary.CancelUpdate();
                    }
                    value = summaryItem?.SummaryValue;
                }
                return (value != null);
            }

            public string Column { get; private set; }

            public SummaryItemTypeEx Type { get; set; }

            public decimal Argument { get; set; }

            private sealed class DataControllerWrapper : ISummaryDataController
            {
                private readonly DataController controler;
                private readonly ISummaryItemsCollection summaryItems;

                public DataControllerWrapper(DataController controler)
                {
                    this.controler = controler;
                    this.summaryItems = new SummaryItemsCollection(controler.TotalSummary);
                }

                public IDataColumnInfo GetColumn(string fieldName) => 
                    this.controler.Columns[fieldName];

                public ISummaryItemsCollection Summary =>
                    this.summaryItems;

                private sealed class SummaryItemsCollection : ISummaryItemsCollection, IEnumerable<SummaryItem>, IEnumerable
                {
                    private readonly TotalSummaryItemCollection totalSummary;

                    public SummaryItemsCollection(TotalSummaryItemCollection totalSummary)
                    {
                        this.totalSummary = totalSummary;
                    }

                    SummaryItem ISummaryItemsCollection.Add(IDataColumnInfo column, SummaryItemTypeEx type, decimal argument, ICustomUIFilterSummaryItem tag)
                    {
                        SummaryItem item1 = new SummaryItem((DataColumnInfo) column, type, argument, true);
                        item1.Tag = tag;
                        SummaryItem item = item1;
                        this.totalSummary.Add(item);
                        return item;
                    }

                    void ISummaryItemsCollection.BeginUpdate()
                    {
                        this.totalSummary.BeginUpdate();
                    }

                    void ISummaryItemsCollection.CancelUpdate()
                    {
                        this.totalSummary.CancelUpdate();
                    }

                    void ISummaryItemsCollection.EndUpdate()
                    {
                        this.totalSummary.EndUpdate();
                    }

                    [IteratorStateMachine(typeof(<System-Collections-Generic-IEnumerable<DevExpress-Data-SummaryItem>-GetEnumerator>d__6))]
                    IEnumerator<SummaryItem> IEnumerable<SummaryItem>.GetEnumerator()
                    {
                        <System-Collections-Generic-IEnumerable<DevExpress-Data-SummaryItem>-GetEnumerator>d__6 d__1 = new <System-Collections-Generic-IEnumerable<DevExpress-Data-SummaryItem>-GetEnumerator>d__6(0);
                        d__1.<>4__this = this;
                        return d__1;
                    }

                    IEnumerator IEnumerable.GetEnumerator() => 
                        this.totalSummary.GetEnumerator();

                    [CompilerGenerated]
                    private sealed class <System-Collections-Generic-IEnumerable<DevExpress-Data-SummaryItem>-GetEnumerator>d__6 : IEnumerator<SummaryItem>, IDisposable, IEnumerator
                    {
                        private int <>1__state;
                        private SummaryItem <>2__current;
                        public CustomUIFilter.CustomUIFilterSummaryItem.DataControllerWrapper.SummaryItemsCollection <>4__this;
                        private IEnumerator <>7__wrap1;

                        [DebuggerHidden]
                        public <System-Collections-Generic-IEnumerable<DevExpress-Data-SummaryItem>-GetEnumerator>d__6(int <>1__state)
                        {
                            this.<>1__state = <>1__state;
                        }

                        private void <>m__Finally1()
                        {
                            this.<>1__state = -1;
                            IDisposable disposable = this.<>7__wrap1 as IDisposable;
                            if (disposable != null)
                            {
                                disposable.Dispose();
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
                                    this.<>7__wrap1 = this.<>4__this.totalSummary.GetEnumerator();
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
                                    SummaryItem current = (SummaryItem) this.<>7__wrap1.Current;
                                    this.<>2__current = current;
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

                        SummaryItem IEnumerator<SummaryItem>.Current =>
                            this.<>2__current;

                        object IEnumerator.Current =>
                            this.<>2__current;
                    }
                }
            }
        }
    }
}

