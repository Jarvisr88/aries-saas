namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class GroupValueBox<T> : ValueViewModel, IGroupValueViewModel<T>, IGroupValueViewModel, IValueViewModel, IBaseCollectionValueViewModel, IFilterValueViewModel
    {
        private readonly IGroupValues groupValues;
        private string[] cachedGrouping;
        private Type[] cachedGroupingTypes;

        public GroupValueBox();
        protected sealed override void BeforeParseCore(IEndUserFilteringMetric metric, CriteriaOperator criteria);
        protected sealed override bool CanResetCore();
        protected virtual IGroupValuesParser CreateParser();
        CriteriaOperator IFilterValueViewModel.CreateFilterCriteria();
        private static string[] EnsureNullText(string[] texts, string nullText);
        private static string GetParent(string[] grouping, object[] path);
        private void LoadGroupValues();
        protected virtual void OnGroupValuesChanded();
        private void OnGroupValuesChecked(object sender, GroupValuesCheckedEventArgs e);
        private void OnGroupValuesLoaded(object sender, GroupValuesLoadedEventArgs e);
        private void OnGroupValuesQuery(object[] path);
        protected sealed override void OnInitialized();
        protected sealed override void OnMetricAttributesSpecialMemberChanged(string propertyName);
        protected sealed override void OnReleasing();
        private bool RadioModeQuery();
        protected sealed override void ResetCore();
        private string RootTextQuery();
        private bool RootVisibilityQuery();
        protected sealed override bool TryParseCore(IEndUserFilteringMetric metric, CriteriaOperator criteria);

        public IGroupValues GroupValues { get; }

        public ICheckedGroupValues CheckedGroupValues { get; }

        protected IGroupMetricAttributes MetricAttributes { get; }

        protected sealed override bool AllowNull { get; }

        [Browsable(false)]
        public bool UseSelectAll { get; }

        [Browsable(false)]
        public bool UseRadioSelection { get; }

        [Browsable(false)]
        public string SelectAllName { get; }

        [Browsable(false)]
        public string NullName { get; }

        [Browsable(false)]
        public string[] Grouping { get; }

        [Browsable(false)]
        public Type[] GroupingTypes { get; }

        private sealed class GroupValuesInterval
        {
            public readonly int Level;
            public readonly int Group;
            private IList<object> values;

            public GroupValuesInterval(int level);
            public GroupValuesInterval(int level, int group, object value);
            public void Add(object value);
            private static void AddOrMerge(List<GroupValueBox<T>.GroupValuesInterval> intervals, GroupValueBox<T>.GroupValuesInterval interval);
            public bool CanMerge(int level, int group);
            public CriteriaOperator GetCriteria(string[] grouping, Type[] groupingTypes, IGroupValues groupValues);
            public void Merge(GroupValueBox<T>.GroupValuesInterval interval);
            public static GroupValueBox<T>.GroupValuesInterval Radio(IGroupValues groupValues, ICheckedGroupValues checkedGroupValues);
            public static IReadOnlyCollection<GroupValueBox<T>.GroupValuesInterval> Tree(IGroupValues groupValues, ICheckedGroupValues checkedGroupValues);

            public bool HasValues { get; }
        }
    }
}

