namespace DevExpress.Xpf.Grid.Hierarchy
{
    using System;
    using System.Runtime.CompilerServices;

    public interface IRootItemsContainer : IDetailRootItemsContainer, IItemsContainer
    {
        event HierarchyChangedEventHandler HierarchyChanged;

        double ScrollItemOffset { get; }

        IItem ScrollItem { get; }
    }
}

