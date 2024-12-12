namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    internal sealed class DateTimeHierarchyFilterTree : LoadedFilterTreeBase
    {
        public DateTimeHierarchyFilterTree(FilterTreeClient client) : base(client, FilterTreeKind.DateTimeHierarchy)
        {
        }

        protected override CriteriaOperator BuildRegularValuesFilter(GroupNode<NodeValueInfo> root)
        {
            List<NodeBase<NodeValueInfo>> topmostCheckedNodes = CheckedTreeHelper.GetTopmostCheckedNodes<NodeValueInfo>(root);
            CriteriaOperator @operator = DateRangesFilterBuilder.Build(base.PropertyName, DateRangesFilterInfo.Create(topmostCheckedNodes));
            NodeBase<NodeValueInfo> base2 = (topmostCheckedNodes.Count > 0) ? topmostCheckedNodes[0] : null;
            return (((base2 == null) || !HierarchyFilterBuilderHelper.IsBlankElement(base2.Value.Value)) ? @operator : (base.CreateIsNullOperator() | @operator));
        }

        internal override bool CanBuildFilter() => 
            FilterTreeHelper.AllowDateTimeFilters(base.FilterRestrictions);

        protected override Action<GroupNode<NodeValueInfo>, FilterValueInfo> GetRegularValueProcessor(CriteriaOperator filter)
        {
            ValueNode<NodeValueInfo> currentDayNode = null;
            GroupNode<NodeValueInfo> currentYearNode = null;
            GroupNode<NodeValueInfo> currentMonthNode = null;
            return delegate (GroupNode<NodeValueInfo> root, FilterValueInfo filterValue) {
                int? nullable1;
                DateTime date = ((DateTime) filterValue.Value).Date;
                Func<ValueNode<NodeValueInfo>, DateTime?> evaluator = <>c.<>9__3_1;
                if (<>c.<>9__3_1 == null)
                {
                    Func<ValueNode<NodeValueInfo>, DateTime?> local1 = <>c.<>9__3_1;
                    evaluator = <>c.<>9__3_1 = x => new DateTime?(((DateTimeUnit) x.Value.Value).Date);
                }
                DateTime? nullable = currentDayNode.Return<ValueNode<NodeValueInfo>, DateTime?>(evaluator, <>c.<>9__3_2 ??= ((Func<DateTime?>) (() => null)));
                bool flag = (nullable == null) || (nullable.Value.Year != date.Year);
                bool flag2 = flag || (nullable.Value.Month != date.Month);
                bool flag3 = flag2 || (nullable.Value.Day != date.Day);
                if (flag)
                {
                    currentYearNode = this.CreateAndAddGroupNode(root, new DateTimeUnit(DateTimeUnitType.Year, date), () => date.Year.ToString());
                }
                if (flag2)
                {
                    currentMonthNode = this.CreateAndAddGroupNode(currentYearNode, new DateTimeUnit(DateTimeUnitType.Month, date), () => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month));
                }
                if (flag3)
                {
                    ValueNode<NodeValueInfo> node = this.CreateAndAddCustomValueNode(currentMonthNode, new DateTimeUnit(DateTimeUnitType.Day, date), () => date.Day.ToString());
                    currentDayNode = node;
                    node.IsChecked = new bool?(this.IsRegularValueChecked(date));
                }
                int? count = currentDayNode.Count;
                int? nullable3 = filterValue.Count;
                if ((count != null) & (nullable3 != null))
                {
                    nullable1 = new int?(count.GetValueOrDefault() + nullable3.GetValueOrDefault());
                }
                else
                {
                    nullable1 = null;
                }
                currentDayNode.SetCount(nullable1);
            };
        }

        protected override bool IsRegularValue(object value) => 
            base.IsRegularValue(value) || (value is DateTimeUnit);

        internal override bool HasGroupedNodes =>
            true;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DateTimeHierarchyFilterTree.<>c <>9 = new DateTimeHierarchyFilterTree.<>c();
            public static Func<ValueNode<NodeValueInfo>, DateTime?> <>9__3_1;
            public static Func<DateTime?> <>9__3_2;

            internal DateTime? <GetRegularValueProcessor>b__3_1(ValueNode<NodeValueInfo> x) => 
                new DateTime?(((DateTimeUnit) x.Value.Value).Date);

            internal DateTime? <GetRegularValueProcessor>b__3_2() => 
                null;
        }
    }
}

