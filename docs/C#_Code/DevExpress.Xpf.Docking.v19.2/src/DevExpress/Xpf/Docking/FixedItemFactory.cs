namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.ComponentModel;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public static class FixedItemFactory
    {
        public static FixedItem CreateFixedItem(FixedItem item) => 
            CreateFixedItem(item.ItemType);

        public static FixedItem CreateFixedItem(LayoutItemType type)
        {
            FixedItem item = null;
            switch (type)
            {
                case LayoutItemType.LayoutSplitter:
                    item = new LayoutSplitter {
                        Caption = DockingLocalizer.GetString(DockingStringId.DefaultSplitterContent)
                    };
                    break;

                case LayoutItemType.EmptySpaceItem:
                    item = new EmptySpaceItem {
                        Caption = DockingLocalizer.GetString(DockingStringId.DefaultEmptySpaceContent)
                    };
                    break;

                case LayoutItemType.Separator:
                    item = new SeparatorItem {
                        Caption = DockingLocalizer.GetString(DockingStringId.DefaultSeparatorContent)
                    };
                    break;

                case LayoutItemType.Label:
                    item = new LabelItem {
                        Caption = DockingLocalizer.GetString(DockingStringId.DefaultLabelContent)
                    };
                    break;

                default:
                    return null;
            }
            item.BeginInit();
            item.EndInit();
            return item;
        }
    }
}

