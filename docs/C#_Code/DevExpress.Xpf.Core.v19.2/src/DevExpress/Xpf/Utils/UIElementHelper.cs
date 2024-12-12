namespace DevExpress.Xpf.Utils
{
    using System;
    using System.Windows;

    public static class UIElementHelper
    {
        public static void Collapse(UIElement element)
        {
            element.Visibility = Visibility.Collapsed;
        }

        public static void Hide(UIElement element)
        {
            element.Visibility = Visibility.Hidden;
        }

        public static bool IsEnabled(UIElement element) => 
            element.IsEnabled;

        public static bool IsVisible(UIElement element) => 
            element.IsVisible;

        public static bool IsVisibleInTree(UIElement element) => 
            IsVisibleInTree(element, false);

        public static bool IsVisibleInTree(UIElement element, bool visualTreeOnly) => 
            IsVisible(element);

        public static void Show(UIElement element)
        {
            element.Visibility = Visibility.Visible;
        }
    }
}

