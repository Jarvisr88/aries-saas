namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using DevExpress.Utils.Filtering;
    using System;
    using System.Collections.Generic;

    internal abstract class BaseMetricAttributesQueryFactory
    {
        protected BaseMetricAttributesQueryFactory()
        {
        }

        protected IMetricAttributesQuery CreateQueryCore(IDictionary<Type, Func<IEndUserFilteringMetric, IMetricAttributesQueryOwner, IMetricAttributesQuery>> initializers, IEndUserFilteringMetric metric, IMetricAttributesQueryOwner owner)
        {
            Func<IEndUserFilteringMetric, IMetricAttributesQueryOwner, IMetricAttributesQuery> func;
            return (!initializers.TryGetValue(metric.AttributesTypeDefinition, out func) ? EmptyMetricAttributesQuery.Instance : func(metric, owner));
        }

        protected abstract class BaseMetricAttributesQuery : IMetricAttributesQuery
        {
            private readonly IEndUserFilteringMetric metricCore;
            private IMetricAttributesQueryOwner ownerCore;

            protected BaseMetricAttributesQuery(IEndUserFilteringMetric metric, IMetricAttributesQueryOwner owner)
            {
                this.metricCore = metric;
                this.ownerCore = owner;
            }

            public virtual IDictionary<string, object> InitializeValues(MetricAttributesData data) => 
                new Dictionary<string, object>();

            public abstract void QueryValues(IDictionary<string, object> memberValues);

            protected string Path =>
                this.metricCore.Path;

            protected IMetricAttributesQueryOwner Owner =>
                this.ownerCore;

            string IMetricAttributesQuery.Path =>
                this.metricCore.Path;
        }

        protected sealed class BooleanChoiceMetricAttributesQuery : BaseMetricAttributesQueryFactory.BaseMetricAttributesQuery
        {
            public BooleanChoiceMetricAttributesQuery(IEndUserFilteringMetric metric, IMetricAttributesQueryOwner owner) : base(metric, owner)
            {
            }

            public sealed override void QueryValues(IDictionary<string, object> memberValues)
            {
                QueryBooleanChoiceDataEventArgs e = new QueryBooleanChoiceDataEventArgs(base.Path, memberValues);
                base.Owner.RaiseMetricAttributesQuery<QueryBooleanChoiceDataEventArgs, BooleanChoiceData>(e);
            }
        }

        private sealed class EmptyMetricAttributesQuery : IMetricAttributesQuery
        {
            internal static readonly IMetricAttributesQuery Instance = new BaseMetricAttributesQueryFactory.EmptyMetricAttributesQuery();

            private EmptyMetricAttributesQuery()
            {
            }

            public IDictionary<string, object> InitializeValues(MetricAttributesData data) => 
                null;

            public void QueryValues(IDictionary<string, object> memberValues)
            {
            }

            public string Path =>
                string.Empty;
        }

        protected sealed class EnumChoiceMetricAttributesQuery : BaseMetricAttributesQueryFactory.BaseMetricAttributesQuery
        {
            public EnumChoiceMetricAttributesQuery(IEndUserFilteringMetric metric, IMetricAttributesQueryOwner owner) : base(metric, owner)
            {
            }

            public sealed override void QueryValues(IDictionary<string, object> memberValues)
            {
                QueryEnumChoiceDataEventArgs e = new QueryEnumChoiceDataEventArgs(base.Path, memberValues);
                base.Owner.RaiseMetricAttributesQuery<QueryEnumChoiceDataEventArgs, EnumChoiceData>(e);
            }
        }

        protected sealed class GroupMetricAttributesQuery : BaseMetricAttributesQueryFactory.BaseMetricAttributesQuery, IGroupMetricAttributesQuery, IMetricAttributesQuery
        {
            private readonly IEndUserFilteringMetric metric;
            private object[] parentValuesCore;
            private CriteriaOperator parentCriteriaCore;
            private string groupPathCore;

            public GroupMetricAttributesQuery(IEndUserFilteringMetric metric, IMetricAttributesQueryOwner owner) : base(metric, owner)
            {
                this.metric = metric;
            }

            IGroupMetricAttributesQuery IGroupMetricAttributesQuery.Initialize(object[] values, CriteriaOperator criteria, string group)
            {
                object[] parentValuesCore = this.parentValuesCore;
                if (this.parentValuesCore == null)
                {
                    object[] local1 = this.parentValuesCore;
                    parentValuesCore = values;
                    if (values == null)
                    {
                        object[] local2 = values;
                        parentValuesCore = UniqueValues.Empty;
                    }
                }
                this.parentValuesCore = parentValuesCore;
                CriteriaOperator parentCriteriaCore = this.parentCriteriaCore;
                if (this.parentCriteriaCore == null)
                {
                    CriteriaOperator local3 = this.parentCriteriaCore;
                    parentCriteriaCore = criteria;
                }
                this.parentCriteriaCore = parentCriteriaCore;
                string groupPathCore = this.groupPathCore;
                if (this.groupPathCore == null)
                {
                    string local4 = this.groupPathCore;
                    groupPathCore = group;
                }
                this.groupPathCore = groupPathCore;
                return this;
            }

            private static object InitializeDataItemsLookup(IEndUserFilteringMetric metric) => 
                (metric.Attributes as IDisplayMetricAttributes).DataItemsLookup ?? new Dictionary<int, object>();

            private static object InitializeGroupTexts(IEndUserFilteringMetric metric)
            {
                string[] strArray;
                if (!(metric.Attributes as IGroupMetricAttributes).GroupTexts.TryGetValue(-2128831035, out strArray))
                {
                    return new Dictionary<int, string[]>();
                }
                Dictionary<int, string[]> dictionary1 = new Dictionary<int, string[]>();
                dictionary1.Add(-2128831035, strArray);
                return dictionary1;
            }

            private static object InitializeGroupValues(IEndUserFilteringMetric metric)
            {
                object[] objArray;
                if (!(metric.Attributes as IGroupMetricAttributes).GroupValues.TryGetValue(-2128831035, out objArray))
                {
                    return new Dictionary<int, object[]>();
                }
                Dictionary<int, object[]> dictionary1 = new Dictionary<int, object[]>();
                dictionary1.Add(-2128831035, objArray);
                return dictionary1;
            }

            public sealed override IDictionary<string, object> InitializeValues(MetricAttributesData data)
            {
                Dictionary<string, object> memberValues = new Dictionary<string, object>(6);
                this.InitializeValues(memberValues);
                GroupData groupData = data as GroupData;
                if (groupData != null)
                {
                    UpdateGroupValuesAndTexts(memberValues, groupData);
                    UpdateDataItemsLookup(memberValues, groupData);
                }
                return memberValues;
            }

            private void InitializeValues(IDictionary<string, object> memberValues)
            {
                memberValues["GroupValues"] = InitializeGroupValues(this.metric);
                memberValues["GroupTexts"] = InitializeGroupTexts(this.metric);
                memberValues["DataItemsLookup"] = InitializeDataItemsLookup(this.metric);
                memberValues["#GroupParentValues"] = this.ParentValues;
                memberValues["#GroupParentCriteria"] = this.ParentCriteria;
                memberValues["#GroupPath"] = this.GroupPath;
            }

            public sealed override void QueryValues(IDictionary<string, object> memberValues)
            {
                QueryGroupDataEventArgs e = new QueryGroupDataEventArgs(base.Path, memberValues);
                this.InitializeValues(memberValues);
                base.Owner.RaiseMetricAttributesQuery<QueryGroupDataEventArgs, GroupData>(e);
                this.parentValuesCore = null;
                this.parentCriteriaCore = null;
                this.groupPathCore = null;
            }

            private static void UpdateDataItemsLookup(Dictionary<string, object> memberValues, GroupData groupData)
            {
                IDictionary<int, object> dictionary;
                if (groupData.TryGetValue<IDictionary<int, object>>("DataItemsLookup", out dictionary))
                {
                    object obj2 = memberValues["DataItemsLookup"];
                    if ((obj2 != dictionary) && (obj2 is IDictionary<int, object>))
                    {
                        foreach (KeyValuePair<int, object> pair in dictionary)
                        {
                            ((IDictionary<int, object>) obj2)[pair.Key] = pair.Value;
                        }
                    }
                }
            }

            private static void UpdateGroupData<T>(string memberName, IDictionary<string, object> memberValues, GroupData groupData)
            {
                IDictionary<int, T[]> dictionary;
                if (groupData.TryGetValue<IDictionary<int, T[]>>(memberName, out dictionary))
                {
                    IDictionary<int, T[]> dictionary2 = (IDictionary<int, T[]>) memberValues[memberName];
                    foreach (KeyValuePair<int, T[]> pair in dictionary)
                    {
                        dictionary2[pair.Key] = pair.Value;
                    }
                }
            }

            private static void UpdateGroupValuesAndTexts(Dictionary<string, object> memberValues, GroupData groupData)
            {
                UpdateGroupData<object>("GroupValues", memberValues, groupData);
                UpdateGroupData<string>("GroupTexts", memberValues, groupData);
            }

            public object[] ParentValues =>
                this.parentValuesCore;

            public CriteriaOperator ParentCriteria =>
                this.parentCriteriaCore;

            public string GroupPath =>
                this.groupPathCore;
        }
    }
}

