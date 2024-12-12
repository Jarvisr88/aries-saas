namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class LayoutGroupExtensions
    {
        public static ILayoutControl GetLayoutControl(this FrameworkElement element)
        {
            FrameworkElement parent = element;
            while (true)
            {
                if ((parent != null) && !parent.IsLayoutControl())
                {
                    parent = parent.Parent as FrameworkElement;
                    if ((parent is ILayoutGroup) || (parent is DevExpress.Xpf.LayoutControl.LayoutControl.ItemsContainer))
                    {
                        continue;
                    }
                }
                return (parent as ILayoutControl);
            }
        }

        public static bool IsLayoutControl(this UIElement element) => 
            (element is ILayoutGroup) && ((ILayoutGroup) element).IsRoot;

        public static bool IsLayoutGroup(this UIElement element) => 
            (element is ILayoutGroup) && !((ILayoutGroup) element).IsRoot;
    }
}

