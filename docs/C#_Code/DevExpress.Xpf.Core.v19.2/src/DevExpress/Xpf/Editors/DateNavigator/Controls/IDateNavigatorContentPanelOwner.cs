namespace DevExpress.Xpf.Editors.DateNavigator.Controls
{
    using System;
    using System.Windows;

    public interface IDateNavigatorContentPanelOwner
    {
        UIElement CreateItem();
        Size GetItemSize();
        void ItemCountChanged();
        void UninitializeItem(UIElement item);
        void UpdateItemPositions(int colCount, int rowCount);

        bool IsHidden { get; }
    }
}

