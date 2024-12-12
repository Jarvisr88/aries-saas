namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public sealed class CheckedTreeListFilterModel : FilterModel
    {
        internal const string DefaultRoundedDateFormatString = "d";
        private FilterTreeBase filterTree;
        private IReadOnlyCollection<NodeBase<NodeValueInfo>> nodes;
        private string searchString;

        internal CheckedTreeListFilterModel(FilterModelClient client) : base(client)
        {
            this.DateTimeFilterTreeType = DevExpress.Xpf.Core.FilteringUI.DateTimeFilterTreeType.Hierarchy;
            this.StringFilterTreeType = DevExpress.Xpf.Core.FilteringUI.StringFilterTreeType.Linear;
            this.ShowSearchPanel = true;
        }

        [CompilerGenerated, DebuggerHidden]
        private Task <>n__0() => 
            base.UpdateCoreAsync();

        internal override CriteriaOperator BuildFilter()
        {
            CriteriaOperator operator1;
            if (this.filterTree != null)
            {
                operator1 = this.filterTree.BuildFilter();
            }
            else
            {
                FilterTreeBase filterTree = this.filterTree;
                operator1 = null;
            }
            return operator1;
        }

        internal bool? CalculateHasFilter()
        {
            GroupNode<NodeValueInfo> rootNode;
            if (!base.FilterValuesCreated)
            {
                return null;
            }
            base.EnsureFilterValuesCollection();
            if (this.filterTree != null)
            {
                rootNode = this.filterTree.RootNode;
            }
            else
            {
                FilterTreeBase filterTree = this.filterTree;
                rootNode = null;
            }
            return new bool?((rootNode == null) ? false : this.filterTree.RootNode.ActualIsChecked.GetValueOrDefault(true));
        }

        internal override bool CanBuildFilterCore() => 
            (this.filterTree != null) && this.filterTree.CanBuildFilter();

        private bool CheckIsNodeVisible(NodeBase<NodeValueInfo> node)
        {
            if (string.IsNullOrEmpty(this.SearchString))
            {
                return true;
            }
            string local1 = node.Value.GetDisplayText();
            string source = local1;
            if (local1 == null)
            {
                string local2 = local1;
                source = string.Empty;
            }
            return (CultureInfo.CurrentCulture.CompareInfo.IndexOf(source, this.SearchString, CompareOptions.IgnoreCase) >= 0);
        }

        private FilterTreeBase CreateDateTimeFilterTree(FilterTreeClient filterTreeClient)
        {
            DevExpress.Xpf.Core.FilteringUI.DateTimeFilterTreeType dateTimeFilterTreeType = this.DateTimeFilterTreeType;
            if (dateTimeFilterTreeType == DevExpress.Xpf.Core.FilteringUI.DateTimeFilterTreeType.Linear)
            {
                return this.CreateLinearDateTimeFilterTree(filterTreeClient);
            }
            if (dateTimeFilterTreeType != DevExpress.Xpf.Core.FilteringUI.DateTimeFilterTreeType.Hierarchy)
            {
                throw new NotSupportedException(this.DateTimeFilterTreeType.ToString());
            }
            return (!base.Column.AllowHierarchicalFilterTree ? this.CreateLinearDateTimeFilterTree(filterTreeClient) : new DateTimeHierarchyFilterTree(filterTreeClient));
        }

        private FilterTreeBase CreateFilterTree()
        {
            FilterTreeClient filterTreeClient = new FilterTreeClient(base.PropertyName, base.client.GetColumnByName, base.SupportedEditSettingsLazy, propertyName => (base.client.GetColumnByName(propertyName).Type == typeof(string)) ? FilterTreeHelper.GetStringBlankOperations(this.ImplyEmptyStringLikeBlank) : FilterTreeHelper.ObjectBlankOperations, new Action(this.ApplyFilter), delegate {
                this.IsUpdating = true;
            }, delegate {
                this.IsUpdating = false;
            }, new Func<NodeBase<NodeValueInfo>, bool>(this.CheckIsNodeVisible), base.Column.GetFilterRestrictions(), base.Column.GroupFields, delegate (string valuesPropertyName, CriteriaOperator filter, string filterPropertyName) {
                <<CreateFilterTree>b__47_3>d local;
                local.<>4__this = this;
                local.valuesPropertyName = valuesPropertyName;
                local.filter = filter;
                local.filterPropertyName = filterPropertyName;
                local.<>t__builder = AsyncTaskMethodBuilder<IList<FilterValueInfo>>.Create();
                local.<>1__state = -1;
                local.<>t__builder.Start<<<CreateFilterTree>b__47_3>d>(ref local);
                return local.<>t__builder.Task;
            });
            return (!base.Column.GroupFields.Any<string>() ? (!FilterTreeHelper.IsDateTimeProperty(base.Column.Type) ? (!(base.Column.Type == typeof(string)) ? this.CreateSimpleFilterTree(filterTreeClient) : this.CreateStringFilterTree(filterTreeClient)) : this.CreateDateTimeFilterTree(filterTreeClient)) : new GroupFilterTree(filterTreeClient, base.Column.RootFilterValuesPropertyName));
        }

        private FilterTreeBase CreateLinearDateTimeFilterTree(FilterTreeClient filterTreeClient) => 
            !base.Column.GetRoundDateTimeFilter() ? this.CreateSimpleFilterTree(filterTreeClient) : new DateTimeLinearFilterTree(filterTreeClient, base.Column.GetDisplayText);

        private FilterTreeBase CreateSimpleFilterTree(FilterTreeClient filterTreeClient) => 
            LinearFilterTree.Linear(filterTreeClient);

        private FilterTreeBase CreateStringFilterTree(FilterTreeClient filterTreeClient)
        {
            DevExpress.Xpf.Core.FilteringUI.StringFilterTreeType stringFilterTreeType = this.StringFilterTreeType;
            if (stringFilterTreeType == DevExpress.Xpf.Core.FilteringUI.StringFilterTreeType.Linear)
            {
                return LinearFilterTree.StringLinear(filterTreeClient);
            }
            if (stringFilterTreeType != DevExpress.Xpf.Core.FilteringUI.StringFilterTreeType.Alphabetical)
            {
                throw new NotSupportedException(this.DateTimeFilterTreeType.ToString());
            }
            return (!base.Column.AllowHierarchicalFilterTree ? LinearFilterTree.StringLinear(filterTreeClient) : new StringHierarchyFilterTree(filterTreeClient));
        }

        internal override FilteringColumn GetActualFilteringColumn() => 
            base.client.GetColumnByName(base.Column.RootFilterValuesPropertyName);

        protected override IEnumerable<FilterModel.CountedLookUp<TValue>> GetSortedFilterValues<TValue>(IEnumerable<FilterModel.CountedLookUp<TValue>> actualValues, bool showCounts)
        {
            if (base.Column.AllowHierarchicalFilterTree)
            {
                if (FilterTreeHelper.IsDateTimeProperty(base.Column.Type) && (this.DateTimeFilterTreeType == DevExpress.Xpf.Core.FilteringUI.DateTimeFilterTreeType.Hierarchy))
                {
                    return GetSortedFilterValuesCore<TValue, DateTime>(actualValues);
                }
                if ((base.Column.Type == typeof(string)) && (this.StringFilterTreeType == DevExpress.Xpf.Core.FilteringUI.StringFilterTreeType.Alphabetical))
                {
                    return GetSortedFilterValuesCore<TValue, string>(actualValues);
                }
            }
            return base.GetSortedFilterValues<TValue>(actualValues, showCounts);
        }

        private static IEnumerable<FilterModel.CountedLookUp<TValue>> GetSortedFilterValuesCore<TValue, TType>(IEnumerable<FilterModel.CountedLookUp<TValue>> actualValues)
        {
            FilterModel.CountedLookUp<TValue>? singleElement = null;
            List<FilterModel.CountedLookUp<TValue>> source = new List<FilterModel.CountedLookUp<TValue>>();
            List<FilterModel.CountedLookUp<TValue>> second = new List<FilterModel.CountedLookUp<TValue>>();
            foreach (FilterModel.CountedLookUp<TValue> up in actualValues)
            {
                if (up.Value is TType)
                {
                    source.Add(up);
                    continue;
                }
                if (up.Value == null)
                {
                    singleElement = new FilterModel.CountedLookUp<TValue>?(up);
                    continue;
                }
                second.Add(up);
            }
            Func<FilterModel.CountedLookUp<TValue>, TValue> keySelector = <>c__44<TValue, TType>.<>9__44_0;
            if (<>c__44<TValue, TType>.<>9__44_0 == null)
            {
                Func<FilterModel.CountedLookUp<TValue>, TValue> local1 = <>c__44<TValue, TType>.<>9__44_0;
                keySelector = <>c__44<TValue, TType>.<>9__44_0 = x => x.Value;
            }
            return singleElement.YieldIfNotNull<FilterModel.CountedLookUp<TValue>?>().Cast<FilterModel.CountedLookUp<TValue>>().Concat<FilterModel.CountedLookUp<TValue>>(source.OrderBy<FilterModel.CountedLookUp<TValue>, TValue>(keySelector)).Concat<FilterModel.CountedLookUp<TValue>>(second);
        }

        internal override Task<UniqueValues> GetUniqueValues(CountsIncludeMode countsIncludeMode) => 
            !base.Column.GroupFields.Any<string>() ? base.GetUniqueValues(countsIncludeMode) : base.client.GetGroupUniqueValues(base.Column.RootFilterValuesPropertyName, null, countsIncludeMode, base.Column.Name);

        private void OnDateTimeFilterTreeTypeChanged()
        {
            if (FilterTreeHelper.IsDateTimeProperty(base.Column.Type))
            {
                this.UpdateFilterTreeIfNeeded();
            }
        }

        private void OnSearchStringChanged()
        {
            this.IsUpdating = true;
            LoadedFilterTreeBase filterTree = this.filterTree as LoadedFilterTreeBase;
            if (filterTree == null)
            {
                LoadedFilterTreeBase local1 = filterTree;
            }
            else
            {
                filterTree.ValidateNodes();
            }
            this.IsUpdating = false;
        }

        private void RebuildFilterTree()
        {
            this.filterTree = this.CreateFilterTree();
            this.FetchSublevelChildrenOnExpand = this.filterTree.FetchSublevelChildrenOnExpand;
            if (base.FilterValuesCreated)
            {
                bool hasGroupedNodes;
                object obj1;
                if (this.filterTree == null)
                {
                    FilterTreeBase filterTree = this.filterTree;
                }
                else
                {
                    this.filterTree.Build(base.FilterValues, base.Filter, base.client.SubstituteFilter);
                }
                if (this.filterTree != null)
                {
                    hasGroupedNodes = this.filterTree.HasGroupedNodes;
                }
                else
                {
                    FilterTreeBase filterTree = this.filterTree;
                    hasGroupedNodes = false;
                }
                this.HasGroupedNodes = hasGroupedNodes;
                this.ValidateDefaultSelection();
                if (this.filterTree == null)
                {
                    obj1 = null;
                }
                else
                {
                    GroupNode<NodeValueInfo>[] collection = new GroupNode<NodeValueInfo>[] { this.filterTree.RootNode };
                    obj1 = new List<NodeBase<NodeValueInfo>>(collection).AsReadOnly();
                }
                this.nodes = (IReadOnlyCollection<NodeBase<NodeValueInfo>>) obj1;
                base.RaisePropertyChanged("Nodes");
            }
        }

        private void RebuildStringFilterTree()
        {
            if (base.Column.Type == typeof(string))
            {
                this.UpdateFilterTreeIfNeeded();
            }
        }

        internal override void ResetFilterValues()
        {
            base.ResetFilterValues();
            this.RebuildFilterTree();
        }

        private void UpdateActualShowSearchPanel()
        {
            this.ActualShowSearchPanel = this.ShowSearchPanel && !base.Column.GroupFields.Any<string>();
        }

        [AsyncStateMachine(typeof(<UpdateCoreAsync>d__39))]
        internal override Task UpdateCoreAsync()
        {
            <UpdateCoreAsync>d__39 d__;
            d__.<>4__this = this;
            d__.<>t__builder = AsyncTaskMethodBuilder.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<UpdateCoreAsync>d__39>(ref d__);
            return d__.<>t__builder.Task;
        }

        private void UpdateFilterTreeIfNeeded()
        {
            base.UpdateContentAsync(delegate {
                this.RebuildFilterTree();
                return FilteringUIExtensions.CompletedTask;
            }).AwaitIfNotCompleted();
        }

        private void ValidateDefaultSelection()
        {
            if ((base.Filter == null) && ((this.filterTree != null) && (this.filterTree.RootNode != null)))
            {
                this.filterTree.RootNode.IsChecked = new bool?(this.SelectAllWhenFilterIsNull);
            }
        }

        internal DevExpress.Xpf.Core.FilteringUI.DateTimeFilterTreeType DateTimeFilterTreeType
        {
            get => 
                base.GetValue<DevExpress.Xpf.Core.FilteringUI.DateTimeFilterTreeType>("DateTimeFilterTreeType");
            set => 
                base.SetValue<DevExpress.Xpf.Core.FilteringUI.DateTimeFilterTreeType>(value, new Action(this.OnDateTimeFilterTreeTypeChanged), "DateTimeFilterTreeType");
        }

        internal DevExpress.Xpf.Core.FilteringUI.StringFilterTreeType StringFilterTreeType
        {
            get => 
                base.GetValue<DevExpress.Xpf.Core.FilteringUI.StringFilterTreeType>("StringFilterTreeType");
            set => 
                base.SetValue<DevExpress.Xpf.Core.FilteringUI.StringFilterTreeType>(value, new Action(this.RebuildStringFilterTree), "StringFilterTreeType");
        }

        internal bool ImplyEmptyStringLikeBlank
        {
            get => 
                base.GetValue<bool>("ImplyEmptyStringLikeBlank");
            set => 
                base.SetValue<bool>(value, new Action(this.RebuildStringFilterTree), "ImplyEmptyStringLikeBlank");
        }

        internal bool SelectAllWhenFilterIsNull
        {
            get => 
                base.GetValue<bool>("SelectAllWhenFilterIsNull");
            set => 
                base.SetValue<bool>(value, new Action(this.ValidateDefaultSelection), "SelectAllWhenFilterIsNull");
        }

        public IReadOnlyCollection<NodeBase<NodeValueInfo>> Nodes
        {
            get
            {
                if (this.nodes == null)
                {
                    base.EnsureFilterValuesCollection();
                }
                return this.nodes;
            }
        }

        public string SearchString
        {
            get => 
                this.searchString;
            set
            {
                if (base.SetValue<string>(ref this.searchString, value, "SearchString"))
                {
                    this.OnSearchStringChanged();
                }
            }
        }

        public bool IsUpdating
        {
            get => 
                base.GetValue<bool>("IsUpdating");
            private set => 
                base.SetValue<bool>(value, "IsUpdating");
        }

        public bool ShowSearchPanel
        {
            get => 
                base.GetValue<bool>("ShowSearchPanel");
            internal set
            {
                base.SetValue<bool>(value, "ShowSearchPanel");
                this.UpdateActualShowSearchPanel();
            }
        }

        public bool ActualShowSearchPanel
        {
            get => 
                base.GetValue<bool>("ActualShowSearchPanel");
            private set => 
                base.SetValue<bool>(value, "ActualShowSearchPanel");
        }

        public bool HasGroupedNodes
        {
            get => 
                base.GetValue<bool>("HasGroupedNodes");
            private set => 
                base.SetValue<bool>(value, "HasGroupedNodes");
        }

        public bool FetchSublevelChildrenOnExpand
        {
            get => 
                base.GetValue<bool>("FetchSublevelChildrenOnExpand");
            private set => 
                base.SetValue<bool>(value, "FetchSublevelChildrenOnExpand");
        }

        [CompilerGenerated]
        private struct <<CreateFilterTree>b__47_3>d : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<IList<FilterValueInfo>> <>t__builder;
            public CheckedTreeListFilterModel <>4__this;
            public string valuesPropertyName;
            public CriteriaOperator filter;
            public string filterPropertyName;
            private CheckedTreeListFilterModel.<>c__DisplayClass47_0 <>8__1;
            private TaskAwaiter<UniqueValues> <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter<UniqueValues> awaiter;
                    UniqueValues values2;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter<UniqueValues>();
                        this.<>1__state = num = -1;
                        goto TR_0004;
                    }
                    else
                    {
                        this.<>8__1 = new CheckedTreeListFilterModel.<>c__DisplayClass47_0();
                        this.<>8__1.getValuesDisplayText = this.<>4__this.client.GetColumnByName(this.valuesPropertyName).GetDisplayText;
                        this.<>8__1.createValueInfo = new Func<object, int?, FilterValueInfo>(this.<>8__1.<CreateFilterTree>b__4);
                        awaiter = this.<>4__this.client.GetGroupUniqueValues(this.valuesPropertyName, this.filter, this.<>4__this.ActualCountsInculedMode, this.filterPropertyName).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<UniqueValues>, CheckedTreeListFilterModel.<<CreateFilterTree>b__47_3>d>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    values2 = awaiter.GetResult();
                    awaiter = new TaskAwaiter<UniqueValues>();
                    IList<FilterValueInfo> result = values2.Value.Match<IEnumerable<FilterValueInfo>>(new Func<ReadOnlyCollection<object>, IEnumerable<FilterValueInfo>>(this.<>8__1.<CreateFilterTree>b__5), new Func<ReadOnlyCollection<Counted<object>>, IEnumerable<FilterValueInfo>>(this.<>8__1.<CreateFilterTree>b__7)).ToList<FilterValueInfo>();
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

        [Serializable, CompilerGenerated]
        private sealed class <>c__44<TValue, TType>
        {
            public static readonly CheckedTreeListFilterModel.<>c__44<TValue, TType> <>9;
            public static Func<FilterModel.CountedLookUp<TValue>, TValue> <>9__44_0;

            static <>c__44()
            {
                CheckedTreeListFilterModel.<>c__44<TValue, TType>.<>9 = new CheckedTreeListFilterModel.<>c__44<TValue, TType>();
            }

            internal TValue <GetSortedFilterValuesCore>b__44_0(FilterModel.CountedLookUp<TValue> x) => 
                x.Value;
        }

        [CompilerGenerated]
        private struct <UpdateCoreAsync>d__39 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public CheckedTreeListFilterModel <>4__this;
            private TaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter awaiter;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter();
                        this.<>1__state = num = -1;
                        goto TR_0004;
                    }
                    else
                    {
                        awaiter = this.<>4__this.<>n__0().GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, CheckedTreeListFilterModel.<UpdateCoreAsync>d__39>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    awaiter.GetResult();
                    awaiter = new TaskAwaiter();
                    this.<>4__this.RebuildFilterTree();
                    this.<>4__this.UpdateActualShowSearchPanel();
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
    }
}

