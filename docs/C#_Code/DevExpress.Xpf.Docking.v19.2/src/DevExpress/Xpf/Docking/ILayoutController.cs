namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;

    public interface ILayoutController : IActiveItemOwner, IDisposable
    {
        bool CancelRenaming();
        bool ChangeGroupOrientation(LayoutGroup group, Orientation orientation);
        T CreateCommand<T>(BaseLayoutItem[] items) where T: LayoutControllerCommand, new();
        bool EndRenaming();
        bool Group(BaseLayoutItem[] items);
        bool Hide(BaseLayoutItem item);
        void HideSelectedItems();
        bool Move(BaseLayoutItem item, BaseLayoutItem target, MoveType type);
        bool Move(BaseLayoutItem item, BaseLayoutItem target, MoveType type, int insertIndex);
        bool Rename(BaseLayoutItem item);
        bool Restore(BaseLayoutItem item);
        bool SetGroupBorderStyle(LayoutGroup group, GroupBorderStyle style);
        bool Ungroup(LayoutGroup group);

        bool IsCustomization { get; }

        DevExpress.Xpf.Docking.Selection Selection { get; }

        HiddenItemsCollection HiddenItems { get; }

        IEnumerable<BaseLayoutItem> FixedItems { get; }
    }
}

