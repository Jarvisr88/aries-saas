namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Windows;

    public interface ILayoutControlCustomizableItem
    {
        FrameworkElement AddNewItem();

        bool CanAddNewItems { get; }

        bool HasHeader { get; }

        object Header { get; set; }

        bool IsLocked { get; }
    }
}

