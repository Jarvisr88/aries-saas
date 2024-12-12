namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using System.Windows.Media;

    public sealed class GroupNodeModel : NodeModelBase
    {
        private readonly DelegateCommand addCommandCore;
        private readonly ObservableCollectionCore<MenuItemBase> addChildMenuCore = new ObservableCollectionCore<MenuItemBase>();
        private readonly ObservableCollectionCore<MenuItemBase> operatorTypeMenuCore = new ObservableCollectionCore<MenuItemBase>();
        private readonly ObservableCollection<NodeModelBase> childrenCore;
        private readonly GroupNodeModelClient client;
        private readonly bool isRoot;
        private GroupOperatorInfo groupOperatorCore;
        private DelegateCommand removeCommandCore;

        private GroupNodeModel(GroupNodeModelClient client, CriteriaOperator filter, IList<NodeModelBase> children, bool isRoot)
        {
            this.client = client;
            this.isRoot = isRoot;
            this.childrenCore = new ObservableCollection<NodeModelBase>(children);
            this.childrenCore.CollectionChanged += (_, __) => this.OnChildrenCollectionChanged();
            this.<Children>k__BackingField = new ReadOnlyObservableCollection<NodeModelBase>(this.childrenCore);
            Lazy<CriteriaOperator> arg = new Lazy<CriteriaOperator>(() => filter);
            GroupNodeModelChildMenuOptions options = client.GetChildMenuOptions(arg);
            AllowedGroupFilters allowedGroupFilters = client.AllowedGroupFilters(arg);
            this.UpdateAddChildMenu(options, allowedGroupFilters);
            this.<AddChildMenu>k__BackingField = new ReadOnlyObservableCollection<MenuItemBase>(this.addChildMenuCore);
            this.UpdateOperatorTypeMenu(allowedGroupFilters);
            this.<OperatorTypeMenu>k__BackingField = new ReadOnlyObservableCollection<MenuItemBase>(this.operatorTypeMenuCore);
            this.groupOperatorCore = CreateGroupOperatorInfo(filter, allowedGroupFilters);
            bool? useCommandManager = null;
            this.addCommandCore = new DelegateCommand(delegate {
                MenuItem local1 = this.addChildMenuCore.OfType<MenuItem>().FirstOrDefault<MenuItem>();
                if (local1 == null)
                {
                    MenuItem local2 = local1;
                }
                else
                {
                    local1.Command.Execute(null);
                }
            }, () => this.addChildMenuCore.OfType<MenuItem>().Any<MenuItem>(), useCommandManager);
        }

        private void AddChild(NodeModelBase newNode)
        {
            if (newNode != null)
            {
                this.childrenCore.Insert(0, newNode);
                this.client.NodeAddedCallback(newNode);
            }
        }

        private void ClearChildren()
        {
            this.childrenCore.Clear();
        }

        private static GroupOperatorInfo CreateAnd()
        {
            Func<IList<CriteriaOperator>, CriteriaOperator> factory = <>c.<>9__32_0;
            if (<>c.<>9__32_0 == null)
            {
                Func<IList<CriteriaOperator>, CriteriaOperator> local1 = <>c.<>9__32_0;
                factory = <>c.<>9__32_0 = children => CriteriaOperator.And(children);
            }
            return new GroupOperatorInfo(GetString(EditorStringId.FilterGroupAnd), factory);
        }

        private CustomExpressionNodeModel CreateDefaultCustomExpression() => 
            this.client.NodeModelFactory.CreateCustom(null);

        private LeafNodeModel CreateDefaultLeaf() => 
            this.client.NodeModelFactory.CreateLeaf(null);

        private static GroupOperatorInfo CreateGroupOperatorInfo(CriteriaOperator filter, AllowedGroupFilters allowedGroupFilters)
        {
            Lazy<GroupOperatorInfo> and = new Lazy<GroupOperatorInfo>(new Func<GroupOperatorInfo>(GroupNodeModel.CreateAnd));
            Lazy<GroupOperatorInfo> or = new Lazy<GroupOperatorInfo>(new Func<GroupOperatorInfo>(GroupNodeModel.CreateOr));
            FallbackMapper<GroupOperatorInfo> fallback = <>c.<>9__31_3;
            if (<>c.<>9__31_3 == null)
            {
                FallbackMapper<GroupOperatorInfo> local1 = <>c.<>9__31_3;
                fallback = <>c.<>9__31_3 = (FallbackMapper<GroupOperatorInfo>) (_ => null);
            }
            return filter.Map<GroupOperatorInfo>(null, null, null, null, null, _ => and.Value, _ => or.Value, x => ((x != and.Value) ? ((x != or.Value) ? (!allowedGroupFilters.HasFlag(AllowedGroupFilters.NotAnd) ? (!allowedGroupFilters.HasFlag(AllowedGroupFilters.NotOr) ? CreateNotAnd() : CreateNotOr()) : CreateNotAnd()) : CreateNotOr()) : CreateNotAnd()), fallback, () => (!allowedGroupFilters.HasFlag(AllowedGroupFilters.And) ? (!allowedGroupFilters.HasFlag(AllowedGroupFilters.Or) ? (!allowedGroupFilters.HasFlag(AllowedGroupFilters.NotAnd) ? (!allowedGroupFilters.HasFlag(AllowedGroupFilters.NotOr) ? and.Value : CreateNotOr()) : CreateNotAnd()) : or.Value) : and.Value));
        }

        private static GroupOperatorInfo CreateNotAnd()
        {
            Func<IList<CriteriaOperator>, CriteriaOperator> factory = <>c.<>9__34_0;
            if (<>c.<>9__34_0 == null)
            {
                Func<IList<CriteriaOperator>, CriteriaOperator> local1 = <>c.<>9__34_0;
                factory = <>c.<>9__34_0 = delegate (IList<CriteriaOperator> children) {
                    CriteriaOperator operator1 = CriteriaOperator.And(children);
                    if (operator1 != null)
                    {
                        return operator1.Not();
                    }
                    CriteriaOperator local1 = operator1;
                    return null;
                };
            }
            return new GroupOperatorInfo(GetString(EditorStringId.FilterGroupNotAnd), factory);
        }

        private static GroupOperatorInfo CreateNotOr()
        {
            Func<IList<CriteriaOperator>, CriteriaOperator> factory = <>c.<>9__35_0;
            if (<>c.<>9__35_0 == null)
            {
                Func<IList<CriteriaOperator>, CriteriaOperator> local1 = <>c.<>9__35_0;
                factory = <>c.<>9__35_0 = delegate (IList<CriteriaOperator> children) {
                    CriteriaOperator operator1 = CriteriaOperator.Or(children);
                    if (operator1 != null)
                    {
                        return operator1.Not();
                    }
                    CriteriaOperator local1 = operator1;
                    return null;
                };
            }
            return new GroupOperatorInfo(GetString(EditorStringId.FilterGroupNotOr), factory);
        }

        private static GroupOperatorInfo CreateOr()
        {
            Func<IList<CriteriaOperator>, CriteriaOperator> factory = <>c.<>9__33_0;
            if (<>c.<>9__33_0 == null)
            {
                Func<IList<CriteriaOperator>, CriteriaOperator> local1 = <>c.<>9__33_0;
                factory = <>c.<>9__33_0 = children => CriteriaOperator.Or(children);
            }
            return new GroupOperatorInfo(GetString(EditorStringId.FilterGroupOr), factory);
        }

        internal static GroupNodeModel CreateRoot(GroupNodeModelClient client, CriteriaOperator filter, IList<NodeModelBase> children) => 
            new GroupNodeModel(client, filter, children, true);

        internal static GroupNodeModel CreateSubGroup(GroupNodeModelClient client, CriteriaOperator filter, IList<NodeModelBase> children) => 
            new GroupNodeModel(client, filter, children, false);

        private static ImageSource GetImage(string name) => 
            FilterImageProvider.GetImage(name);

        private static string GetString(EditorStringId id) => 
            EditorLocalizer.GetString(id);

        private void OnChildrenCollectionChanged()
        {
            this.addCommandCore.RaiseCanExecuteChanged();
            this.client.NodesChangedCallback();
            if (this.removeCommandCore == null)
            {
                DelegateCommand removeCommandCore = this.removeCommandCore;
            }
            else
            {
                this.removeCommandCore.RaiseCanExecuteChanged();
            }
        }

        internal void RemoveChild(NodeModelBase node)
        {
            this.childrenCore.Remove(node);
        }

        internal void Update()
        {
            Lazy<CriteriaOperator> arg = new Lazy<CriteriaOperator>(() => this.BuildMinimizedFilter(null));
            GroupNodeModelChildMenuOptions options = this.client.GetChildMenuOptions(arg);
            AllowedGroupFilters allowedGroupFilters = this.client.AllowedGroupFilters(arg);
            this.UpdateAddChildMenu(options, allowedGroupFilters);
            this.UpdateOperatorTypeMenu(allowedGroupFilters);
        }

        private void UpdateAddChildMenu(GroupNodeModelChildMenuOptions options, AllowedGroupFilters allowedGroupFilters)
        {
            List<MenuItemBase> source = new List<MenuItemBase>();
            bool flag = !this.Children.Any<NodeModelBase>() || (allowedGroupFilters != AllowedGroupFilters.None);
            if (options.AllowAddCondition & flag)
            {
                source.Add(new MenuItem(GetString(EditorStringId.FilterGroupAddCondition), GetImage("AddCondition"), () => this.AddChild(this.CreateDefaultLeaf())));
            }
            if (options.AllowAddGroup && (allowedGroupFilters != AllowedGroupFilters.None))
            {
                source.Add(new MenuItem(GetString(EditorStringId.FilterGroupAddGroup), GetImage("AddGroup"), delegate {
                    Func<LeafNodeModel, LeafNodeModel[]> evaluator = <>c.<>9__21_2;
                    if (<>c.<>9__21_2 == null)
                    {
                        Func<LeafNodeModel, LeafNodeModel[]> local1 = <>c.<>9__21_2;
                        evaluator = <>c.<>9__21_2 = x => x.YieldToArray<LeafNodeModel>();
                    }
                    LeafNodeModel[] modelArray = this.CreateDefaultLeaf().Return<LeafNodeModel, LeafNodeModel[]>(evaluator, <>c.<>9__21_3 ??= () => new LeafNodeModel[0]);
                    this.AddChild(this.client.NodeModelFactory.CreateGroup(null, modelArray, false));
                }));
            }
            if (options.AllowAddCustomExpression & flag)
            {
                source.Add(new MenuItem(GetString(EditorStringId.FilterGroupAddCustomExpression), GetImage("AddCondition"), () => this.AddChild(this.CreateDefaultCustomExpression())));
            }
            this.addChildMenuCore.Assign(source);
        }

        private void UpdateOperatorTypeMenu(AllowedGroupFilters allowedGroupFilters)
        {
            var typeArray1 = new [] { new { 
                Flag = AllowedGroupFilters.And,
                StringId = EditorStringId.FilterGroupAnd,
                ImageName = "And",
                Factory = new Func<GroupOperatorInfo>(GroupNodeModel.CreateAnd)
            }, new { 
                Flag = AllowedGroupFilters.Or,
                StringId = EditorStringId.FilterGroupOr,
                ImageName = "Or",
                Factory = new Func<GroupOperatorInfo>(GroupNodeModel.CreateOr)
            }, new { 
                Flag = AllowedGroupFilters.NotAnd,
                StringId = EditorStringId.FilterGroupNotAndMenuCaption,
                ImageName = "NotAnd",
                Factory = new Func<GroupOperatorInfo>(GroupNodeModel.CreateNotAnd)
            }, new { 
                Flag = AllowedGroupFilters.NotOr,
                StringId = EditorStringId.FilterGroupNotOrMenuCaption,
                ImageName = "NotOr",
                Factory = new Func<GroupOperatorInfo>(GroupNodeModel.CreateNotOr)
            } };
            MenuItem[] source = (from x in typeArray1
                where allowedGroupFilters.HasFlag(x.Flag)
                select new MenuItem(GetString(x.StringId), GetImage(x.ImageName), () => this.GroupOperator = x.Factory())).ToArray<MenuItem>();
            this.operatorTypeMenuCore.Assign(source);
        }

        public GroupOperatorInfo GroupOperator
        {
            get => 
                this.groupOperatorCore;
            set
            {
                if (!ReferenceEquals(this.groupOperatorCore, value))
                {
                    this.groupOperatorCore = value;
                    this.client.NodesChangedCallback();
                    base.RaisePropertyChanged("GroupOperator");
                    if (this.removeCommandCore == null)
                    {
                        DelegateCommand removeCommandCore = this.removeCommandCore;
                    }
                    else
                    {
                        this.removeCommandCore.RaiseCanExecuteChanged();
                    }
                }
            }
        }

        public IList<MenuItemBase> OperatorTypeMenu { get; }

        public IList<MenuItemBase> AddChildMenu { get; }

        public IList<NodeModelBase> Children { get; }

        public ICommand AddCommand =>
            this.addCommandCore;

        public override ICommand RemoveCommand
        {
            get
            {
                if (this.removeCommandCore == null)
                {
                    Lazy<CriteriaOperator> lazyFilter = new Lazy<CriteriaOperator>(() => this.BuildEvaluableFilter(null));
                    this.removeCommandCore = this.isRoot ? new DelegateCommand(new Action(this.ClearChildren), () => !this.Children.Any<NodeModelBase>() ? false : this.client.CanExecuteRemoveAction(lazyFilter), false) : new DelegateCommand(delegate {
                        this.client.RemoveNode(this);
                    }, () => this.client.CanExecuteRemoveAction(lazyFilter), false);
                }
                return this.removeCommandCore;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GroupNodeModel.<>c <>9 = new GroupNodeModel.<>c();
            public static Func<LeafNodeModel, LeafNodeModel[]> <>9__21_2;
            public static Func<LeafNodeModel[]> <>9__21_3;
            public static FallbackMapper<GroupOperatorInfo> <>9__31_3;
            public static Func<IList<CriteriaOperator>, CriteriaOperator> <>9__32_0;
            public static Func<IList<CriteriaOperator>, CriteriaOperator> <>9__33_0;
            public static Func<IList<CriteriaOperator>, CriteriaOperator> <>9__34_0;
            public static Func<IList<CriteriaOperator>, CriteriaOperator> <>9__35_0;

            internal CriteriaOperator <CreateAnd>b__32_0(IList<CriteriaOperator> children) => 
                CriteriaOperator.And(children);

            internal GroupOperatorInfo <CreateGroupOperatorInfo>b__31_3(CriteriaOperator _) => 
                null;

            internal CriteriaOperator <CreateNotAnd>b__34_0(IList<CriteriaOperator> children)
            {
                CriteriaOperator operator1 = CriteriaOperator.And(children);
                if (operator1 != null)
                {
                    return operator1.Not();
                }
                CriteriaOperator local1 = operator1;
                return null;
            }

            internal CriteriaOperator <CreateNotOr>b__35_0(IList<CriteriaOperator> children)
            {
                CriteriaOperator operator1 = CriteriaOperator.Or(children);
                if (operator1 != null)
                {
                    return operator1.Not();
                }
                CriteriaOperator local1 = operator1;
                return null;
            }

            internal CriteriaOperator <CreateOr>b__33_0(IList<CriteriaOperator> children) => 
                CriteriaOperator.Or(children);

            internal LeafNodeModel[] <UpdateAddChildMenu>b__21_2(LeafNodeModel x) => 
                x.YieldToArray<LeafNodeModel>();

            internal LeafNodeModel[] <UpdateAddChildMenu>b__21_3() => 
                new LeafNodeModel[0];
        }
    }
}

