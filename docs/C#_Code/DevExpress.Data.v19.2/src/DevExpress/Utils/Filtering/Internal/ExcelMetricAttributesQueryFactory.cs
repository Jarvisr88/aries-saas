namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal sealed class ExcelMetricAttributesQueryFactory : BaseMetricAttributesQueryFactory, IMetricAttributesQueryFactory
    {
        internal static readonly IMetricAttributesQueryFactory Instance = new ExcelMetricAttributesQueryFactory();
        private readonly IDictionary<Type, Func<IEndUserFilteringMetric, IMetricAttributesQueryOwner, IMetricAttributesQuery>> initializers;

        private ExcelMetricAttributesQueryFactory()
        {
            Dictionary<Type, Func<IEndUserFilteringMetric, IMetricAttributesQueryOwner, IMetricAttributesQuery>> dictionary = new Dictionary<Type, Func<IEndUserFilteringMetric, IMetricAttributesQueryOwner, IMetricAttributesQuery>>();
            Func<IEndUserFilteringMetric, IMetricAttributesQueryOwner, IMetricAttributesQuery> func1 = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Func<IEndUserFilteringMetric, IMetricAttributesQueryOwner, IMetricAttributesQuery> local1 = <>c.<>9__1_0;
                func1 = <>c.<>9__1_0 = (metric, owner) => new UniqueValuesRangeMetricAttributesQuery(metric, owner);
            }
            dictionary.Add(typeof(IRangeMetricAttributes<>), func1);
            Func<IEndUserFilteringMetric, IMetricAttributesQueryOwner, IMetricAttributesQuery> func2 = <>c.<>9__1_1;
            if (<>c.<>9__1_1 == null)
            {
                Func<IEndUserFilteringMetric, IMetricAttributesQueryOwner, IMetricAttributesQuery> local2 = <>c.<>9__1_1;
                func2 = <>c.<>9__1_1 = (metric, owner) => new UniqueValuesLookupMetricAttributesQuery(metric, owner);
            }
            dictionary.Add(typeof(ILookupMetricAttributes<>), func2);
            Func<IEndUserFilteringMetric, IMetricAttributesQueryOwner, IMetricAttributesQuery> func3 = <>c.<>9__1_2;
            if (<>c.<>9__1_2 == null)
            {
                Func<IEndUserFilteringMetric, IMetricAttributesQueryOwner, IMetricAttributesQuery> local3 = <>c.<>9__1_2;
                func3 = <>c.<>9__1_2 = (metric, owner) => new UniqueValuesBooleanChoiceMetricAttributesQuery(metric, owner);
            }
            dictionary.Add(typeof(IChoiceMetricAttributes<>), func3);
            Func<IEndUserFilteringMetric, IMetricAttributesQueryOwner, IMetricAttributesQuery> func4 = <>c.<>9__1_3;
            if (<>c.<>9__1_3 == null)
            {
                Func<IEndUserFilteringMetric, IMetricAttributesQueryOwner, IMetricAttributesQuery> local4 = <>c.<>9__1_3;
                func4 = <>c.<>9__1_3 = (metric, owner) => new UniqueValuesEnumChoiceMetricAttributesQuery(metric, owner);
            }
            dictionary.Add(typeof(IEnumChoiceMetricAttributes<>), func4);
            Func<IEndUserFilteringMetric, IMetricAttributesQueryOwner, IMetricAttributesQuery> func5 = <>c.<>9__1_4;
            if (<>c.<>9__1_4 == null)
            {
                Func<IEndUserFilteringMetric, IMetricAttributesQueryOwner, IMetricAttributesQuery> local5 = <>c.<>9__1_4;
                func5 = <>c.<>9__1_4 = (metric, owner) => new BaseMetricAttributesQueryFactory.GroupMetricAttributesQuery(metric, owner);
            }
            dictionary.Add(typeof(IGroupMetricAttributes<>), func5);
            this.initializers = dictionary;
        }

        IMetricAttributesQuery IMetricAttributesQueryFactory.CreateQuery(IEndUserFilteringMetric metric, IMetricAttributesQueryOwner owner) => 
            base.CreateQueryCore(this.initializers, metric, owner);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExcelMetricAttributesQueryFactory.<>c <>9 = new ExcelMetricAttributesQueryFactory.<>c();
            public static Func<IEndUserFilteringMetric, IMetricAttributesQueryOwner, IMetricAttributesQuery> <>9__1_0;
            public static Func<IEndUserFilteringMetric, IMetricAttributesQueryOwner, IMetricAttributesQuery> <>9__1_1;
            public static Func<IEndUserFilteringMetric, IMetricAttributesQueryOwner, IMetricAttributesQuery> <>9__1_2;
            public static Func<IEndUserFilteringMetric, IMetricAttributesQueryOwner, IMetricAttributesQuery> <>9__1_3;
            public static Func<IEndUserFilteringMetric, IMetricAttributesQueryOwner, IMetricAttributesQuery> <>9__1_4;

            internal IMetricAttributesQuery <.ctor>b__1_0(IEndUserFilteringMetric metric, IMetricAttributesQueryOwner owner) => 
                new ExcelMetricAttributesQueryFactory.UniqueValuesRangeMetricAttributesQuery(metric, owner);

            internal IMetricAttributesQuery <.ctor>b__1_1(IEndUserFilteringMetric metric, IMetricAttributesQueryOwner owner) => 
                new ExcelMetricAttributesQueryFactory.UniqueValuesLookupMetricAttributesQuery(metric, owner);

            internal IMetricAttributesQuery <.ctor>b__1_2(IEndUserFilteringMetric metric, IMetricAttributesQueryOwner owner) => 
                new ExcelMetricAttributesQueryFactory.UniqueValuesBooleanChoiceMetricAttributesQuery(metric, owner);

            internal IMetricAttributesQuery <.ctor>b__1_3(IEndUserFilteringMetric metric, IMetricAttributesQueryOwner owner) => 
                new ExcelMetricAttributesQueryFactory.UniqueValuesEnumChoiceMetricAttributesQuery(metric, owner);

            internal IMetricAttributesQuery <.ctor>b__1_4(IEndUserFilteringMetric metric, IMetricAttributesQueryOwner owner) => 
                new BaseMetricAttributesQueryFactory.GroupMetricAttributesQuery(metric, owner);
        }

        private sealed class UniqueValuesBooleanChoiceMetricAttributesQuery : BaseMetricAttributesQueryFactory.BaseMetricAttributesQuery
        {
            public UniqueValuesBooleanChoiceMetricAttributesQuery(IEndUserFilteringMetric metric, IMetricAttributesQueryOwner owner) : base(metric, owner)
            {
            }

            public sealed override void QueryValues(IDictionary<string, object> memberValues)
            {
                QueryUniqueValuesBooleanChoiceDataEventArgs e = new QueryUniqueValuesBooleanChoiceDataEventArgs(base.Path, memberValues);
                base.Owner.RaiseMetricAttributesQuery<QueryUniqueValuesBooleanChoiceDataEventArgs, BooleanChoiceData>(e);
            }
        }

        private sealed class UniqueValuesEnumChoiceMetricAttributesQuery : BaseMetricAttributesQueryFactory.BaseMetricAttributesQuery
        {
            public UniqueValuesEnumChoiceMetricAttributesQuery(IEndUserFilteringMetric metric, IMetricAttributesQueryOwner owner) : base(metric, owner)
            {
            }

            public sealed override void QueryValues(IDictionary<string, object> memberValues)
            {
                QueryUniqueValuesEnumChoiceDataEventArgs e = new QueryUniqueValuesEnumChoiceDataEventArgs(base.Path, memberValues);
                base.Owner.RaiseMetricAttributesQuery<QueryUniqueValuesEnumChoiceDataEventArgs, EnumChoiceData>(e);
            }
        }

        private sealed class UniqueValuesLookupMetricAttributesQuery : BaseMetricAttributesQueryFactory.BaseMetricAttributesQuery
        {
            public UniqueValuesLookupMetricAttributesQuery(IEndUserFilteringMetric metric, IMetricAttributesQueryOwner owner) : base(metric, owner)
            {
            }

            public sealed override void QueryValues(IDictionary<string, object> memberValues)
            {
                QueryUniqueValuesLookupDataEventArgs e = new QueryUniqueValuesLookupDataEventArgs(base.Path, memberValues);
                base.Owner.RaiseMetricAttributesQuery<QueryLookupDataEventArgs, LookupData>(e);
            }
        }

        private sealed class UniqueValuesRangeMetricAttributesQuery : BaseMetricAttributesQueryFactory.BaseMetricAttributesQuery
        {
            public UniqueValuesRangeMetricAttributesQuery(IEndUserFilteringMetric metric, IMetricAttributesQueryOwner owner) : base(metric, owner)
            {
            }

            public sealed override void QueryValues(IDictionary<string, object> memberValues)
            {
                QueryUniqueValuesRangeDataEventArgs e = new QueryUniqueValuesRangeDataEventArgs(base.Path, memberValues);
                base.Owner.RaiseMetricAttributesQuery<QueryRangeDataEventArgs, RangeData>(e);
            }
        }
    }
}

