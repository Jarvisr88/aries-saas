namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data;
    using DevExpress.Data.Helpers;
    using DevExpress.Utils.Filtering;
    using DevExpress.Utils.MVVM;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public abstract class DataClientFilteringUIViewModelProvider : FilteringUIViewModelProviderBase, IEndUserFilteringViewModelProvider, IEndUserFilteringViewModelPropertyValues, IEnumerable<IEndUserFilteringMetricViewModel>, IEnumerable, IEndUserFilteringCriteriaChangeAware
    {
        private readonly IDictionary<string, object[]> uniqueValuesCache;

        protected DataClientFilteringUIViewModelProvider(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.uniqueValuesCache = new Dictionary<string, object[]>();
            base.RegisterService<IEndUserFilteringViewModelProvider>(this);
            base.RegisterService<IDisplayTemplatesCustomizationServiceFactory>(this.GetDisplayTemplatesCustomizationServiceFactory());
        }

        protected void CheckUniqueValues(string path, object uniqueValues)
        {
            IEndUserFilteringMetricViewModel metricViewModel = base.PropertyValues[path];
            if (metricViewModel != null)
            {
                IUniqueValuesMetricAttributes attributes = metricViewModel.Metric.Attributes as IUniqueValuesMetricAttributes;
                if ((attributes != null) && (attributes.HasUniqueValues && !Equals(attributes.UniqueValues, uniqueValues)))
                {
                    using (metricViewModel.LockValue())
                    {
                        base.UpdateMemberBindings(metricViewModel, null, null);
                    }
                }
            }
        }

        void IEndUserFilteringViewModelProvider.RetrieveFields(Action<Type> retrieveFields, Type sourceType, IEnumerable<IEndUserFilteringMetricAttributes> attributes, Type viewModelBaseType)
        {
            if (retrieveFields != null)
            {
                base.RetrieveType(sourceType, attributes, viewModelBaseType).Do<Type>(retrieveFields);
            }
        }

        void IEndUserFilteringViewModelProvider.UpdateMemberBindings(string path)
        {
            base.UpdateMemberBindings(path);
        }

        protected virtual IDisplayTemplatesCustomizationServiceFactory GetDisplayTemplatesCustomizationServiceFactory() => 
            new ContextDisplayTemplatesCustomizationServiceFactoryForDataClient(base.GetGetContextServiceProvider());

        protected internal void Initialize(string path, object[] uniqueValues)
        {
            UniqueValues.Initialize(this.uniqueValuesCache, path, uniqueValues);
            this.CheckUniqueValues(path, uniqueValues);
        }

        protected sealed override void RaiseBooleanChoiceMetricAttributesQuery(QueryBooleanChoiceDataEventArgs e)
        {
            base.RaiseBooleanChoiceMetricAttributesQuery(e);
            ((UniqueValuesBooleanChoiceData) e.Result).UniqueValues = UniqueValues.Get(this.uniqueValuesCache, e.PropertyPath);
        }

        protected sealed override void RaiseEnumChoiceMetricAttributesQuery(QueryEnumChoiceDataEventArgs e)
        {
            base.RaiseEnumChoiceMetricAttributesQuery(e);
            ((UniqueValuesEnumChoiceData) e.Result).UniqueValues = UniqueValues.Get(this.uniqueValuesCache, e.PropertyPath);
        }

        protected sealed override void RaiseGroupMetricAttributesQuery(QueryGroupDataEventArgs e)
        {
            base.RaiseGroupMetricAttributesQuery(e);
            e.Result.SetGroupValues(UniqueValues.Get(this.uniqueValuesCache, e.PropertyPath));
        }

        protected sealed override void RaiseLookupMetricAttributesQuery(QueryLookupDataEventArgs e)
        {
            base.RaiseLookupMetricAttributesQuery(e);
            ((UniqueValuesLookupData) e.Result).UniqueValues = UniqueValues.Get(this.uniqueValuesCache, e.PropertyPath);
        }

        protected sealed override void RaiseRangeMetricAttributesQuery(QueryRangeDataEventArgs e)
        {
            object[] uniqueValues = UniqueValues.Get(this.uniqueValuesCache, e.PropertyPath);
            object[] objArray2 = UniqueValues.Aggregate(base.Settings[e.PropertyPath].Type, uniqueValues);
            base.RaiseRangeMetricAttributesQuery(e);
            ((UniqueValuesRangeData) e.Result).UniqueValues = uniqueValues;
            e.Result.Minimum = objArray2[0];
            e.Result.Maximum = objArray2[1];
            e.Result.Average = objArray2[2];
        }

        Type IEndUserFilteringViewModelProvider.SourceType
        {
            get => 
                null;
            set
            {
            }
        }

        IEnumerable<IEndUserFilteringMetricAttributes> IEndUserFilteringViewModelProvider.Attributes
        {
            get => 
                null;
            set
            {
            }
        }

        Type IEndUserFilteringViewModelProvider.ViewModelBaseType
        {
            get => 
                null;
            set
            {
            }
        }

        object IEndUserFilteringViewModelProvider.ParentViewModel
        {
            get => 
                null;
            set
            {
            }
        }

        IViewModelProvider IEndUserFilteringViewModelProvider.ParentViewModelProvider
        {
            get => 
                null;
            set
            {
            }
        }

        protected abstract class DataClientViewModelDataContext : IEndUserFilteringViewModelDataContext
        {
            protected readonly DataClientFilteringUIViewModelProvider owner;
            private readonly HashSet<string> dataMembers = new HashSet<string>();

            protected DataClientViewModelDataContext(DataClientFilteringUIViewModelProvider owner)
            {
                this.owner = owner;
            }

            void IEndUserFilteringViewModelDataContext.Complete(string path)
            {
                this.dataMembers.Contains(path);
            }

            void IEndUserFilteringViewModelDataContext.DataBind(string path)
            {
                if (this.dataMembers.Contains(path))
                {
                    LohPooled.OnceEnumerableCollection<object> source = LohPooled.SortUtils.SortSameThread<object>(<>c.<>9__4_0 ??= (x, y) => ValueComparer.Default.Compare(x, y), this.GetValues(path) ?? new object[0], true);
                    this.owner.Initialize(path, source.ToArray<object>());
                }
            }

            void IEndUserFilteringViewModelDataContext.Initialize(string path)
            {
                this.dataMembers.Add(path);
            }

            protected abstract IEnumerable<object> GetValues(string path);

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly DataClientFilteringUIViewModelProvider.DataClientViewModelDataContext.<>c <>9 = new DataClientFilteringUIViewModelProvider.DataClientViewModelDataContext.<>c();
                public static Comparison<object> <>9__4_0;

                internal int <DevExpress.Utils.Filtering.Internal.IEndUserFilteringViewModelDataContext.DataBind>b__4_0(object x, object y) => 
                    ValueComparer.Default.Compare(x, y);
            }
        }
    }
}

