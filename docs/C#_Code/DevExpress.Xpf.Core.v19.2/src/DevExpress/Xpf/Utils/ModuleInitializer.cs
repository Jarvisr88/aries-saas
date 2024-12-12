namespace DevExpress.Xpf.Utils
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    public static class ModuleInitializer
    {
        public static void Initialize()
        {
            if (!new DependencyObject().IsInDesignTool())
            {
                ThemeManager.Initialize();
                ProcessStartConfirmationService.Register();
            }
        }
    }
}

