namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal sealed class MultiFilterOperatorMenuSubstitutionDecorator<TID, TOP> where TID: class where TOP: class
    {
        private readonly OperatorMenuItemFactory<MultiFilterModelItem, TID> component;
        private readonly Func<OperatorMenuItemsSubstitutionInfo<TOP>, OperatorMenuItemsSubstitutionInfo<TOP>> substituteOperatorMenuItems;
        private readonly OperatorItemSubstitutionInfoProvider<TID, TOP> provider;
        private OperatorMenuItemFactory<MultiFilterModelItem, TID> factoryCore;

        public MultiFilterOperatorMenuSubstitutionDecorator(OperatorMenuItemFactory<MultiFilterModelItem, TID> component, Func<OperatorMenuItemsSubstitutionInfo<TOP>, OperatorMenuItemsSubstitutionInfo<TOP>> substituteOperatorMenuItems, OperatorItemSubstitutionInfoProvider<TID, TOP> provider)
        {
            Guard.ArgumentNotNull(component, "component");
            Guard.ArgumentNotNull(substituteOperatorMenuItems, "substituteOperatorMenuItems");
            Guard.ArgumentNotNull(provider, "provider");
            this.component = component;
            this.substituteOperatorMenuItems = substituteOperatorMenuItems;
            this.provider = provider;
        }

        private AvailableMenuItems<MultiFilterModelItem, TID> CreateAvailableItems(AvailableMenuItemIdentities<TID> identities)
        {
            AvailableMenuItems<MultiFilterModelItem, TID> items = this.component.CreateAvailableItems(identities);
            TOP[] list = this.CreateUserOperatorItems(items.Items).ToArray<TOP>();
            OperatorMenuItemsSubstitutionInfo<TOP> arg = new OperatorMenuItemsSubstitutionInfo<TOP>(list, this.GetDefaultOperatorItem(list, items.DefaultID));
            arg = this.substituteOperatorMenuItems(arg);
            return new AvailableMenuItems<MultiFilterModelItem, TID>(this.CreateMultiModelItems(arg.List), this.GetDefaultID(arg));
        }

        private Tree<IdentifiedOperatorMenuItem<TID, MultiFilterModelItem>, string>[] CreateMultiModelItems(IList<TOP> userOperatorItems) => 
            userOperatorItems.GroupBy<TOP, string>(this.provider.GetGroupName).SelectMany<IGrouping<string, TOP>, Tree<IdentifiedOperatorMenuItem<TID, MultiFilterModelItem>, string>>(new Func<IGrouping<string, TOP>, IEnumerable<Tree<IdentifiedOperatorMenuItem<TID, MultiFilterModelItem>, string>>>(this.CreateMultiModelItems)).ToArray<Tree<IdentifiedOperatorMenuItem<TID, MultiFilterModelItem>, string>>();

        private IEnumerable<Tree<IdentifiedOperatorMenuItem<TID, MultiFilterModelItem>, string>> CreateMultiModelItems(IGrouping<string, TOP> group) => 
            !string.IsNullOrEmpty(group.Key) ? Tree<IdentifiedOperatorMenuItem<TID, MultiFilterModelItem>, string>.CreateGroup(group.Key, (from z in group select base.CreateMultiModelItemTree(z)).ToArray<Tree<IdentifiedOperatorMenuItem<TID, MultiFilterModelItem>, string>>()).Yield<Tree<IdentifiedOperatorMenuItem<TID, MultiFilterModelItem>, string>>() : (from x in group select base.CreateMultiModelItemTree(x));

        private Tree<IdentifiedOperatorMenuItem<TID, MultiFilterModelItem>, string> CreateMultiModelItemTree(TOP item) => 
            Tree<IdentifiedOperatorMenuItem<TID, MultiFilterModelItem>, string>.CreateLeaf(new IdentifiedOperatorMenuItem<TID, MultiFilterModelItem>(this.provider.GetOperatorInfoID(item), this.provider.CreateMultiModelItem(item)));

        private TOP[] CreateUserOperatorItems(Tree<IdentifiedOperatorMenuItem<TID, MultiFilterModelItem>, string>[] forest)
        {
            Func<IEnumerable<TOP>, IEnumerable<TOP>> selector = <>c<TID, TOP>.<>9__8_4;
            if (<>c<TID, TOP>.<>9__8_4 == null)
            {
                Func<IEnumerable<TOP>, IEnumerable<TOP>> local1 = <>c<TID, TOP>.<>9__8_4;
                selector = <>c<TID, TOP>.<>9__8_4 = x => x;
            }
            return forest.Transform<IdentifiedOperatorMenuItem<TID, MultiFilterModelItem>, string, IEnumerable<TOP>>(leaf => base.provider.CreateOperatorInfo(leaf).Yield<TOP>(), (group, children) => children.SelectMany<IEnumerable<TOP>, TOP>(delegate (IEnumerable<TOP> child) {
                Func<TOP, TOP> <>9__3;
                Func<TOP, TOP> func2 = <>9__3;
                if (<>9__3 == null)
                {
                    Func<TOP, TOP> local1 = <>9__3;
                    func2 = <>9__3 = operatorItem => this.provider.UpdateGroupName(operatorItem, group);
                }
                return child.Select<TOP, TOP>(func2);
            })).SelectMany<IEnumerable<TOP>, TOP>(selector).ToArray<TOP>();
        }

        private TID GetDefaultID(OperatorMenuItemsSubstitutionInfo<TOP> info)
        {
            TOP defaultItem = info.DefaultItem;
            return (((defaultItem == null) || !info.List.Contains(defaultItem)) ? info.List.FirstOrDefault<TOP>().With<TOP, TID>(x => base.provider.GetOperatorInfoID(x)) : this.provider.GetOperatorInfoID(defaultItem));
        }

        private TOP GetDefaultOperatorItem(IList<TOP> items, TID id) => 
            items.FirstOrDefault<TOP>(x => Equals(((MultiFilterOperatorMenuSubstitutionDecorator<TID, TOP>) this).provider.GetOperatorInfoID(x), id));

        public OperatorMenuItemFactory<MultiFilterModelItem, TID> Factory
        {
            get
            {
                this.factoryCore ??= new OperatorMenuItemFactory<MultiFilterModelItem, TID>(new Func<AvailableMenuItemIdentities<TID>, AvailableMenuItems<MultiFilterModelItem, TID>>(this.CreateAvailableItems), this.component.CreateNonAvailableItem);
                return this.factoryCore;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MultiFilterOperatorMenuSubstitutionDecorator<TID, TOP>.<>c <>9;
            public static Func<IEnumerable<TOP>, IEnumerable<TOP>> <>9__8_4;

            static <>c()
            {
                MultiFilterOperatorMenuSubstitutionDecorator<TID, TOP>.<>c.<>9 = new MultiFilterOperatorMenuSubstitutionDecorator<TID, TOP>.<>c();
            }

            internal IEnumerable<TOP> <CreateUserOperatorItems>b__8_4(IEnumerable<TOP> x) => 
                x;
        }
    }
}

