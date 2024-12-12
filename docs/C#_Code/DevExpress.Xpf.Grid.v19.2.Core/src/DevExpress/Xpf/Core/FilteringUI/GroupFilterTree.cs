namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    internal sealed class GroupFilterTree : FilterTreeBase
    {
        private readonly string rootFilterValuesPropertyName;

        public GroupFilterTree(FilterTreeClient client, string rootFilterValuesPropertyName) : base(client, FilterTreeKind.Groups)
        {
            this.rootFilterValuesPropertyName = rootFilterValuesPropertyName;
        }

        protected override CriteriaOperator BuildFilterCore() => 
            this.GetHierarchyFilterBuilder(this.rootFilterValuesPropertyName).BuildValuesFilter(FilterRestrictions.All(), this.GetCheckedValuesInfo(base.RootNode.Nodes, 0), false);

        [AsyncStateMachine(typeof(<BuildTree>d__8))]
        protected override void BuildTree(GroupNode<NodeValueInfo> root, IList<FilterValueInfo> filterValues, CriteriaOperator filter, Func<CriteriaOperator, CriteriaOperator> substituteFilter)
        {
            <BuildTree>d__8 d__;
            d__.<>4__this = this;
            d__.root = root;
            d__.filterValues = filterValues;
            d__.filter = filter;
            d__.<>t__builder = AsyncVoidMethodBuilder.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<BuildTree>d__8>(ref d__);
        }

        internal override bool CanBuildFilter() => 
            true;

        [AsyncStateMachine(typeof(<CreateAndAddGroupValueNodes>d__9))]
        private Task<IList<NodeBase<NodeValueInfo>>> CreateAndAddGroupValueNodes(NodeParent<NodeValueInfo> nodeParent, Task<IList<FilterValueInfo>> filterValues, string fieldName, GroupFilterInfo parentGroupInfo, bool? parentCheckState, List<object> parentValues)
        {
            <CreateAndAddGroupValueNodes>d__9 d__;
            d__.<>4__this = this;
            d__.nodeParent = nodeParent;
            d__.filterValues = filterValues;
            d__.fieldName = fieldName;
            d__.parentGroupInfo = parentGroupInfo;
            d__.parentCheckState = parentCheckState;
            d__.parentValues = parentValues;
            d__.<>t__builder = AsyncTaskMethodBuilder<IList<NodeBase<NodeValueInfo>>>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<CreateAndAddGroupValueNodes>d__9>(ref d__);
            return d__.<>t__builder.Task;
        }

        private NodeValueInfo CreteNodeValueInfo(string fieldName, FilterValueInfo filterValue)
        {
            BlankOperations operations = base.client.GetBlankOperations(fieldName);
            if (operations.IsBlankValue(filterValue.Value))
            {
                Func<string> func1 = <>c.<>9__11_0;
                if (<>c.<>9__11_0 == null)
                {
                    Func<string> local1 = <>c.<>9__11_0;
                    func1 = <>c.<>9__11_0 = () => FilterTreeBase.SelectBlanksText;
                }
                return new NodeValueInfo(null, func1, DisplayMode.DisplayText, null);
            }
            if (!operations.IsEmptyValue(filterValue.Value))
            {
                BaseEditSettings editSettings = FilterModelHelper.GetSupportedEditSettings(base.client.GetColumn(fieldName));
                return new NodeValueInfo(filterValue.Value, filterValue.GetDisplayText, DisplayMode.Value, (editSettings != null) ? new Lazy<BaseEditSettings>(() => editSettings) : null);
            }
            Func<string> getDisplayText = <>c.<>9__11_1;
            if (<>c.<>9__11_1 == null)
            {
                Func<string> local2 = <>c.<>9__11_1;
                getDisplayText = <>c.<>9__11_1 = () => FilterTreeBase.EmptyStringText;
            }
            return new NodeValueInfo(filterValue.Value, getDisplayText, DisplayMode.DisplayText, null);
        }

        private CheckedValuesInfo GetCheckedValuesInfo(ReadOnlyCollection<NodeBase<NodeValueInfo>> nodes, int level)
        {
            bool? isChecked = null;
            return new CheckedValuesInfo(this.GetValues(nodes, true), () => this.GetValues(nodes, false), (from x in this.GetNodes(nodes, isChecked) select new IndeterminateValueInfo(x.Value.Value, this.GetCheckedValuesInfo(((GroupValueNode<NodeValueInfo>) x).Nodes, level + 1), this.GroupFields[level])).ToList<IndeterminateValueInfo>());
        }

        private Task<IList<FilterValueInfo>> GetFilterValues(IEnumerable<object> values, IEnumerable<string> groupFields, string valuesPropertyName)
        {
            IEnumerable<string> second = this.rootFilterValuesPropertyName.Yield<string>().Concat<string>(groupFields);
            CriteriaOperator @operator = CriteriaOperator.And(values.Zip<object, string, CriteriaOperator>(second, (value, property) => this.GetHierarchyFilterBuilder(property).BuildSingleValueFilter(value, false, () => base.client.GetBlankOperations(property).CreateIsNullOperator(property, AllowedUnaryFilters.All))));
            return base.client.GetGroupFilterValues(valuesPropertyName, @operator, base.PropertyName);
        }

        private HierarchyFilterBuilderBase GetHierarchyFilterBuilder(string fieldName) => 
            HierarchyFilterBuilderHelper.GetAppropriateBuilder(fieldName, x => base.client.GetBlankOperations(x).CreateIsNullOperator(x, AllowedUnaryFilters.All), name => base.client.GetColumn(name).GetUseRangeDateFilter());

        private IEnumerable<NodeBase<NodeValueInfo>> GetNodes(IEnumerable<NodeBase<NodeValueInfo>> nodes, bool? isChecked) => 
            nodes.Where<NodeBase<NodeValueInfo>>(delegate (NodeBase<NodeValueInfo> x) {
                bool? actualIsChecked = x.ActualIsChecked;
                bool? nullable2 = isChecked;
                return (actualIsChecked.GetValueOrDefault() == nullable2.GetValueOrDefault()) ? ((actualIsChecked != null) == (nullable2 != null)) : false;
            });

        private List<object> GetValues(IEnumerable<NodeBase<NodeValueInfo>> nodes, bool isChecked)
        {
            Func<NodeBase<NodeValueInfo>, object> selector = <>c.<>9__17_0;
            if (<>c.<>9__17_0 == null)
            {
                Func<NodeBase<NodeValueInfo>, object> local1 = <>c.<>9__17_0;
                selector = <>c.<>9__17_0 = x => x.Value.Value;
            }
            return this.GetNodes(nodes, new bool?(isChecked)).Select<NodeBase<NodeValueInfo>, object>(selector).ToList<object>();
        }

        private IEnumerable<FilterValueInfo> MergeBlankAndEmptyValues(IList<FilterValueInfo> filterValues, BlankOperations blankOperations)
        {
            int? nullable1;
            if (filterValues.Count <= 2)
            {
                return filterValues;
            }
            FilterValueInfo info = filterValues[1];
            if (!blankOperations.IsBlankValue(info.Value))
            {
                return filterValues;
            }
            FilterValueInfo singleElement = filterValues[0];
            int? count = singleElement.Count;
            int? nullable2 = info.Count;
            if ((count != null) & (nullable2 != null))
            {
                nullable1 = new int?(count.GetValueOrDefault() + nullable2.GetValueOrDefault());
            }
            else
            {
                nullable1 = null;
            }
            singleElement.Count = nullable1;
            return singleElement.Yield<FilterValueInfo>().Concat<FilterValueInfo>(filterValues.Skip<FilterValueInfo>(2));
        }

        internal override bool HasGroupedNodes =>
            true;

        internal override bool FetchSublevelChildrenOnExpand =>
            false;

        private string[] GroupFields =>
            base.client.GroupFields;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GroupFilterTree.<>c <>9 = new GroupFilterTree.<>c();
            public static Action<NodeBase<NodeValueInfo>> <>9__9_2;
            public static Action<NodeBase<NodeValueInfo>, int?> <>9__9_3;
            public static Action<NodeBase<NodeValueInfo>> <>9__9_4;
            public static Func<string> <>9__11_0;
            public static Func<string> <>9__11_1;
            public static Func<NodeBase<NodeValueInfo>, object> <>9__17_0;

            internal void <CreateAndAddGroupValueNodes>b__9_2(NodeBase<NodeValueInfo> _)
            {
                throw new InvalidOperationException();
            }

            internal void <CreateAndAddGroupValueNodes>b__9_3(NodeBase<NodeValueInfo> _, int? __)
            {
            }

            internal void <CreateAndAddGroupValueNodes>b__9_4(NodeBase<NodeValueInfo> child)
            {
            }

            internal string <CreteNodeValueInfo>b__11_0() => 
                FilterTreeBase.SelectBlanksText;

            internal string <CreteNodeValueInfo>b__11_1() => 
                FilterTreeBase.EmptyStringText;

            internal object <GetValues>b__17_0(NodeBase<NodeValueInfo> x) => 
                x.Value.Value;
        }

        [CompilerGenerated]
        private struct <BuildTree>d__8 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncVoidMethodBuilder <>t__builder;
            public GroupFilterTree <>4__this;
            public GroupNode<NodeValueInfo> root;
            public IList<FilterValueInfo> filterValues;
            public CriteriaOperator filter;
            private TaskAwaiter<IList<NodeBase<NodeValueInfo>>> <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter<IList<NodeBase<NodeValueInfo>>> awaiter;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter<IList<NodeBase<NodeValueInfo>>>();
                        this.<>1__state = num = -1;
                        goto TR_0004;
                    }
                    else
                    {
                        NodeParent<NodeValueInfo> asNodeParent;
                        if (this.root != null)
                        {
                            asNodeParent = this.root.AsNodeParent;
                        }
                        else
                        {
                            GroupNode<NodeValueInfo> root = this.root;
                            asNodeParent = null;
                        }
                        awaiter = this.<>4__this.CreateAndAddGroupValueNodes(asNodeParent, Task.FromResult<IList<FilterValueInfo>>(this.filterValues), this.<>4__this.rootFilterValuesPropertyName, GroupFilterParser.Parse(this.filter, this.<>4__this.rootFilterValuesPropertyName, this.<>4__this.GroupFields), true, new List<object>()).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<IList<NodeBase<NodeValueInfo>>>, GroupFilterTree.<BuildTree>d__8>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    awaiter.GetResult();
                    awaiter = new TaskAwaiter<IList<NodeBase<NodeValueInfo>>>();
                    this.<>1__state = -2;
                    this.<>t__builder.SetResult();
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <CreateAndAddGroupValueNodes>d__9 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<IList<NodeBase<NodeValueInfo>>> <>t__builder;
            public GroupFilterTree <>4__this;
            public string fieldName;
            public NodeParent<NodeValueInfo> nodeParent;
            public List<object> parentValues;
            public Task<IList<FilterValueInfo>> filterValues;
            private GroupFilterTree.<>c__DisplayClass9_0 <>8__1;
            public GroupFilterInfo parentGroupInfo;
            public bool? parentCheckState;
            private List<NodeBase<NodeValueInfo>> <nodes>5__2;
            private TaskAwaiter<IList<FilterValueInfo>> <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter<IList<FilterValueInfo>> awaiter;
                    IList<FilterValueInfo> list3;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter<IList<FilterValueInfo>>();
                        this.<>1__state = num = -1;
                        goto TR_0010;
                    }
                    else
                    {
                        this.<>8__1 = new GroupFilterTree.<>c__DisplayClass9_0();
                        this.<>8__1.<>4__this = this.<>4__this;
                        this.<>8__1.fieldName = this.fieldName;
                        this.<>8__1.nodeParent = this.nodeParent;
                        this.<>8__1.parentValues = this.parentValues;
                        this.<nodes>5__2 = new List<NodeBase<NodeValueInfo>>();
                        awaiter = this.filterValues.GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0010;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<IList<FilterValueInfo>>, GroupFilterTree.<CreateAndAddGroupValueNodes>d__9>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0010:
                    list3 = awaiter.GetResult();
                    awaiter = new TaskAwaiter<IList<FilterValueInfo>>();
                    IList<FilterValueInfo> filterValues = list3;
                    IEnumerator<FilterValueInfo> enumerator = this.<>4__this.MergeBlankAndEmptyValues(filterValues, this.<>4__this.client.GetBlankOperations(this.<>8__1.fieldName)).GetEnumerator();
                    try
                    {
                        while (enumerator.MoveNext())
                        {
                            GroupFilterTree.<>c__DisplayClass9_3 class_2;
                            Either<GroupFilterInfo, bool> either1;
                            GroupFilterTree.<>c__DisplayClass9_0 CS$<>8__locals1 = this.<>8__1;
                            FilterValueInfo filterValue = enumerator.Current;
                            if (this.parentGroupInfo != null)
                            {
                                either1 = this.parentGroupInfo.GetState(filterValue.Value);
                            }
                            else
                            {
                                GroupFilterInfo parentGroupInfo = this.parentGroupInfo;
                                either1 = null;
                            }
                            Either<GroupFilterInfo, bool> state = either1;
                            bool? checkState = (state != null) ? state.GetCheckState() : new bool?(this.parentCheckState.Value);
                            GroupFilterInfo groupInfo = (state != null) ? state.GetGroupFilterInfo() : null;
                            this.<nodes>5__2.Add(new Func<Lazy<GroupValueNode<NodeValueInfo>>, GroupValueNode<NodeValueInfo>>(class_2.<CreateAndAddGroupValueNodes>b__0).WithReturnValue<GroupValueNode<NodeValueInfo>>());
                        }
                    }
                    finally
                    {
                        if ((num < 0) && (enumerator != null))
                        {
                            enumerator.Dispose();
                        }
                    }
                    IList<NodeBase<NodeValueInfo>> result = this.<nodes>5__2;
                    this.<>1__state = -2;
                    this.<>t__builder.SetResult(result);
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }
    }
}

