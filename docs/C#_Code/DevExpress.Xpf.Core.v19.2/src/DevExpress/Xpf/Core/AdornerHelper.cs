namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;
    using System.Windows.Documents;

    public static class AdornerHelper
    {
        public static AdornerLayer FindAdornerLayer(UIElement rootElement)
        {
            UIElement topContainerWithAdornerLayer = LayoutHelper.GetTopContainerWithAdornerLayer(rootElement);
            return ((topContainerWithAdornerLayer != null) ? AdornerLayer.GetAdornerLayer(topContainerWithAdornerLayer) : null);
        }
    }
}

