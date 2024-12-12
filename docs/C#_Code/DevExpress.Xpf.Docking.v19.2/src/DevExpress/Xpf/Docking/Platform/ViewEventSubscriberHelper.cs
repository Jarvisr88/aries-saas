namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;
    using System.Windows.Input;

    internal class ViewEventSubscriberHelper
    {
        public static bool CanHandlePreviewEvent(DependencyObject container, MouseEventArgs e)
        {
            DependencyObject originalSource = e.OriginalSource as DependencyObject;
            return ((originalSource != null) && (!IsInPopupCore(originalSource) || IsChildElement(container, originalSource)));
        }

        private static bool IsChildElement(DependencyObject container, DependencyObject dObj) => 
            LayoutHelper.IsChildElement(container, dObj) || ReferenceEquals(container, dObj);

        public static bool IsInPopup(MouseEventArgs e) => 
            false;

        private static bool IsInPopupCore(DependencyObject dObj) => 
            DockLayoutManagerHelper.IsPopupRoot(LayoutHelper.FindRoot(dObj, false));
    }
}

