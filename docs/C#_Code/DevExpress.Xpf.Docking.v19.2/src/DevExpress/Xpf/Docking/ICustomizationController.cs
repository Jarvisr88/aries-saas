namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.Customization;
    using System;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public interface ICustomizationController : IControlHost, IDisposable
    {
        event DragInfoChangedEventHandler DragInfoChanged;

        void BeginCustomization();
        void CloseMenu();
        T CreateCommand<T>() where T: CustomizationControllerCommand, new();
        void DesignTimeRaiseEvent(object sender, RoutedEventArgs e);
        void EndCustomization();
        void HideClosedItemsBar();
        void HideCustomizationForm();
        void HideDocumentSelectorForm();
        void HideDragCursor();
        void SetDragCursorPosition(Point point);
        void ShowClosedItemsBar();
        void ShowContextMenu(BaseLayoutItem item);
        void ShowControlItemContextMenu(BaseLayoutItem item);
        void ShowCustomizationForm();
        void ShowDocumentSelectorForm();
        void ShowDragCursor(Point point, BaseLayoutItem item);
        void ShowHiddenItemMenu(BaseLayoutItem item);
        void ShowItemSelectorMenu(UIElement source, BaseLayoutItem[] items);
        void UpdateClosedItemsBar();
        void UpdateDragInfo(DevExpress.Xpf.Docking.Customization.DragInfo info);

        DockLayoutManager Container { get; }

        DevExpress.Xpf.Bars.BarManager BarManager { get; }

        DevExpress.Xpf.Docking.ClosedItemsBar ClosedItemsBar { get; }

        DevExpress.Xpf.Docking.Base.ClosedPanelsBarVisibility ClosedPanelsBarVisibility { get; set; }

        bool IsClosedPanelsVisible { get; }

        bool ClosedPanelsVisibility { get; set; }

        bool IsCustomizationFormVisible { get; }

        UIElement MenuSource { get; set; }

        BarManagerMenuController ItemContextMenuController { get; }

        BarManagerMenuController ItemsSelectorMenuController { get; }

        BarManagerMenuController LayoutControlItemContextMenuController { get; }

        BarManagerMenuController LayoutControlItemCustomizationMenuController { get; }

        BarManagerMenuController HiddenItemsMenuController { get; }

        bool IsCustomization { get; set; }

        ObservableCollection<BaseLayoutItem> CustomizationItems { get; }

        LayoutGroup CustomizationRoot { get; set; }

        bool IsDragCursorVisible { get; }

        DevExpress.Xpf.Docking.Customization.DragInfo DragInfo { get; }

        FloatingContainer CustomizationForm { get; }

        bool IsDocumentSelectorVisible { get; }
    }
}

