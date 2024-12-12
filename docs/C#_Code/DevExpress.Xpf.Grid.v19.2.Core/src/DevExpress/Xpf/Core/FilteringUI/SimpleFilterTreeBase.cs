namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using System;

    internal abstract class SimpleFilterTreeBase : LoadedFilterTreeBase
    {
        public SimpleFilterTreeBase(FilterTreeClient client, FilterTreeKind kind) : base(client, kind)
        {
        }

        protected sealed override CriteriaOperator BuildRegularValuesFilter(GroupNode<NodeValueInfo> root) => 
            HierarchyFilterBuilderHelper.GetAppropriateBuilder(base.PropertyName, _ => base.CreateIsNullOperator(), null).BuildValuesFilter(base.FilterRestrictions, new CheckedValuesInfo(FilterTreeHelper.GetCheckedRegularValues(root, new Func<object, bool>(this.IsRegularValue)), () => FilterTreeHelper.GetUncheckedRegularValues(root, new Func<object, bool>(this.IsRegularValue)), null), false);

        internal sealed override bool CanBuildFilter() => 
            CanBuildFilter(base.FilterRestrictions);

        internal static bool CanBuildFilter(FilterRestrictions filterRestrictions) => 
            filterRestrictions.AllowedAnyOfFilters != AllowedAnyOfFilters.None;
    }
}

