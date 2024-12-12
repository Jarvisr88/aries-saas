namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils.Themes;
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Interop;

    public static class FloatingContainerFactory
    {
        private static FloatingMode CheckOwnerMode(DependencyObject owner, FloatingMode mode) => 
            ((owner == null) || (Window.GetWindow(owner) == null)) ? FloatingMode.Adorner : mode;

        public static FloatingMode CheckPopupHost(DependencyObject owner)
        {
            FrameworkElement element = LayoutHelper.FindRoot(owner, false) as FrameworkElement;
            return (((element == null) || !(element.Parent is Popup)) ? FloatingMode.Window : FloatingMode.Popup);
        }

        public static FloatingContainer Create(FloatingMode mode)
        {
            FloatingMode mode2 = BrowserInteropHelper.IsBrowserHosted ? FloatingMode.Adorner : mode;
            return ((mode != FloatingMode.Popup) ? ((mode2 == FloatingMode.Adorner) ? ((FloatingContainer) new FloatingAdornerContainer()) : ((FloatingContainer) new FloatingWindowContainer())) : ((FloatingContainer) new PopupFloatingContainer()));
        }

        public static FloatingContainer CreateWithOwner(FloatingMode mode, DependencyObject owner)
        {
            FloatingMode mode2 = BrowserInteropHelper.IsBrowserHosted ? FloatingMode.Adorner : CheckOwnerMode(owner, mode);
            return ((mode != FloatingMode.Popup) ? ((mode2 == FloatingMode.Adorner) ? ((FloatingContainer) new FloatingAdornerContainer()) : ((FloatingContainer) new FloatingWindowContainer())) : ((FloatingContainer) new PopupFloatingContainer()));
        }

        public static DataTemplate FindContainerTemplate(FrameworkElement container)
        {
            FloatingContainerThemeKeyExtension resourceKey = new FloatingContainerThemeKeyExtension();
            resourceKey.ResourceKey = FloatingContainerThemeKey.FloatingContainerTemplate;
            return (DataTemplate) container.FindResource(resourceKey);
        }
    }
}

