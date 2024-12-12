namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class DateTimeLinearFilterTree : LoadedFilterTreeBase
    {
        private readonly Func<object, string> getDisplayText;

        public DateTimeLinearFilterTree(FilterTreeClient client, Func<object, string> getDisplayText) : base(client, FilterTreeKind.DateTimeLinear)
        {
            this.getDisplayText = getDisplayText;
        }

        private CriteriaOperator BuildDateFilter(IEnumerable<object> checkedValues) => 
            MultiselectRoundedDateTimeFilterHelper.DatesToCriteria(base.PropertyName, checkedValues.Cast<DateTime>());

        protected override CriteriaOperator BuildRegularValuesFilter(GroupNode<NodeValueInfo> root)
        {
            List<object> checkedRegularValues = FilterTreeHelper.GetCheckedRegularValues(root, new Func<object, bool>(this.IsRegularValue));
            return (((checkedRegularValues.Count <= 0) || !HierarchyFilterBuilderHelper.IsBlankElement(checkedRegularValues[0])) ? this.BuildDateFilter(checkedRegularValues) : (base.CreateIsNullOperator() | this.BuildDateFilter(checkedRegularValues.Skip<object>(1))));
        }

        internal sealed override bool CanBuildFilter() => 
            FilterTreeHelper.AllowDateTimeFilters(base.FilterRestrictions);

        protected override Action<GroupNode<NodeValueInfo>, FilterValueInfo> GetRegularValueProcessor(CriteriaOperator filter)
        {
            ValueNode<NodeValueInfo> currentDateNode = null;
            return delegate (GroupNode<NodeValueInfo> root, FilterValueInfo filterValue) {
                DateTime date = ((DateTime) filterValue.Value).Date;
                if ((currentDateNode == null) || (((DateTime) currentDateNode.Value.Value) != date))
                {
                    ValueNode<NodeValueInfo> node = this.CreateAndAddCustomValueNode(root, date, () => this.getDisplayText(date));
                    node.SetCount(filterValue.Count);
                    node.IsChecked = new bool?(this.IsRegularValueChecked(date));
                    currentDateNode = node;
                }
                else
                {
                    int? nullable1;
                    int? count = currentDateNode.Count;
                    int? nullable2 = filterValue.Count;
                    if ((count != null) & (nullable2 != null))
                    {
                        nullable1 = new int?(count.GetValueOrDefault() + nullable2.GetValueOrDefault());
                    }
                    else
                    {
                        nullable1 = null;
                    }
                    currentDateNode.SetCount(nullable1);
                }
            };
        }

        internal override bool HasGroupedNodes =>
            false;
    }
}

