namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;

    public static class BackgroundServiceHelper
    {
        public static void ReplaceBackgroundService(PrintingSystemBase printingSystem)
        {
            printingSystem.ReplaceService<IBackgroundService>(new BackgroundService());
        }
    }
}

