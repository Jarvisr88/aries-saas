namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public static class ContextMenuManager
    {
        public static bool OverrideStandardContextMenuInChildControls;
        public static readonly DependencyProperty HasDXContextMenuProperty;
        public static int hashCode;
        private static WeakReference _openedMenu;

        static ContextMenuManager();
        private static bool AllowSwitcher(UIElement elem, ButtonSwitcher button);
        private static bool CheckIsDXContextMenuOpened();
        internal static object FindLeftMenu(object sender);
        public static BarPopupBase GetContextMenu(object contextElement);
        private static UIElement GetContextMenuHolder(DependencyObject from);
        public static bool GetHasDXContextMenu(DependencyObject obj);
        private static BaseEdit GetTemplatedParentEditor(DependencyObject from);
        private static bool HasStandardContextMenu(object source);
        private static bool HasStandardContextMenu(DependencyObject source, DependencyObject originalSource, DependencyObject sender);
        private static bool IsDXContextMenuSet(DependencyObject editor);
        private static bool IsEditor(object elem);
        private static bool IsHashCodeNotRepeated(object sender);
        private static bool IsMenuEnabled(object sender, ButtonSwitcher button);
        internal static void OnContextMenuOpening(object sender, ContextMenuEventArgs e);
        private static void OnHasDXContextMenuPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        internal static void OnLeftClickContextMenuOpening(object sender, MouseButtonEventArgs e);
        private static bool OpenContextMenu(object sender, ContextMenuOpenReason openReason);
        private static void PrepareContextMenu(UIElement contextElement, BarPopupBase contextMenu, ContextMenuOpenReason openReason);
        public static void RegistryContextElement(object contextElement, IPopupControl contextMenu);
        public static void SetBarManager(DependencyObject contextElement, BarManager manager);
        private static void SetContextMenuPosition(BarPopupBase contextMenu, Point position);
        public static void SetHasDXContextMenu(DependencyObject obj, bool value);
        public static bool ShowElementContextMenu(object contextElement, ContextMenuOpenReason openReason = 0);
        public static void UnregistryContextElement(object contextElement);
        private static void UpdateContexMenu(DependencyObject d, bool hasDXContextMenu);

        internal static IPopupControl OpenedMenu { get; set; }
    }
}

