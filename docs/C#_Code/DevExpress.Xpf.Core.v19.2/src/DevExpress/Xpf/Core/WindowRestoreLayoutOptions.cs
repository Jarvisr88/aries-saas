namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public static class WindowRestoreLayoutOptions
    {
        public static readonly DependencyProperty AllowRestoreStateForDisplayedWindowProperty = DependencyProperty.RegisterAttached("AllowRestoreStateForDisplayedWindow", typeof(bool), typeof(WindowRestoreLayoutOptions), new PropertyMetadata(false));
        public static readonly DependencyProperty RestoreMinimizedWindowInNormalStateProperty = DependencyProperty.RegisterAttached("RestoreMinimizedWindowInNormalState", typeof(bool), typeof(WindowRestoreLayoutOptions), new PropertyMetadata(true));

        public static bool GetAllowRestoreStateForDisplayedWindow(Window obj) => 
            (bool) obj.GetValue(AllowRestoreStateForDisplayedWindowProperty);

        public static bool GetRestoreMinimizedWindowInNormalState(Window obj) => 
            (bool) obj.GetValue(RestoreMinimizedWindowInNormalStateProperty);

        public static void SetAllowRestoreStateForDisplayedWindow(Window obj, bool value)
        {
            obj.SetValue(AllowRestoreStateForDisplayedWindowProperty, value);
        }

        public static void SetRestoreMinimizedWindowInNormalState(Window obj, bool value)
        {
            obj.SetValue(RestoreMinimizedWindowInNormalStateProperty, value);
        }
    }
}

