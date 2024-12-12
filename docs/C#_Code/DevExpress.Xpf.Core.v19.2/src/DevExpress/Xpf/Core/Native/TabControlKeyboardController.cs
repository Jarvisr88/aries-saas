namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows.Input;

    public static class TabControlKeyboardController
    {
        private static bool CanNavigate(DXTabControl tabControl);
        private static bool CheckArrowKeyNavigation(DXTabControl tabControl, KeyEventArgs e);
        public static void OnTabControlKeyDown(DXTabControl tabControl, KeyEventArgs e);
        public static void OnTabControlKeyUp(DXTabControl tabControl, KeyEventArgs e);
        public static void OnTabItemKeyDown(DXTabItem tabItem, KeyEventArgs e);
    }
}

