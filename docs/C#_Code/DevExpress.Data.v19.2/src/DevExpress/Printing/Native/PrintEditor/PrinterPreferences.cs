namespace DevExpress.Printing.Native.PrintEditor
{
    using System;
    using System.Drawing.Printing;
    using System.Runtime.InteropServices;
    using System.Security;

    public class PrinterPreferences
    {
        private const int DM_IN_BUFFER = 8;
        private const int DM_OUT_BUFFER = 2;
        private const int DM_IN_PROMPT = 4;
        private const int IDOK = 1;

        protected virtual void ApplyDevModeData(PageSettings pageSettings, IntPtr devModeData)
        {
            pageSettings.PrinterSettings.SetHdevmode(devModeData);
            pageSettings.SetHdevmode(devModeData);
        }

        [DllImport("winspool.Drv", EntryPoint="DocumentPropertiesW", CallingConvention=CallingConvention.StdCall, SetLastError=true, ExactSpelling=true)]
        private static extern int DocumentProperties(IntPtr hwnd, IntPtr hPrinter, [MarshalAs(UnmanagedType.LPWStr)] string pDeviceName, IntPtr pDevModeOutput, IntPtr pDevModeInput, int fMode);
        [DllImport("kernel32.dll")]
        private static extern IntPtr GlobalFree(IntPtr hMem);
        [DllImport("kernel32.dll")]
        private static extern IntPtr GlobalLock(IntPtr hMem);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll")]
        private static extern bool GlobalUnlock(IntPtr hMem);
        [SecuritySafeCritical]
        public void ShowPrinterProperties(PageSettings defaultPageSettings, IntPtr hwnd)
        {
            PrinterSettings printerSettings = defaultPageSettings.PrinterSettings;
            if (!printerSettings.IsValid)
            {
                throw new InvalidPrinterException(printerSettings);
            }
            IntPtr hdevmode = printerSettings.GetHdevmode(defaultPageSettings);
            IntPtr pDevModeInput = GlobalLock(hdevmode);
            if (pDevModeInput == IntPtr.Zero)
            {
                GlobalFree(hdevmode);
            }
            else
            {
                IntPtr pDevModeOutput = Marshal.AllocHGlobal(DocumentProperties(hwnd, IntPtr.Zero, printerSettings.PrinterName, IntPtr.Zero, pDevModeInput, 0));
                int num2 = DocumentProperties(hwnd, IntPtr.Zero, printerSettings.PrinterName, pDevModeOutput, pDevModeInput, 14);
                GlobalUnlock(hdevmode);
                if (num2 == 1)
                {
                    this.ApplyDevModeData(defaultPageSettings, pDevModeOutput);
                }
                GlobalFree(hdevmode);
                Marshal.FreeHGlobal(pDevModeOutput);
            }
        }

        [SecuritySafeCritical]
        public void ShowPrinterProperties(PrinterSettings settings, IntPtr hwnd)
        {
            this.ShowPrinterProperties(settings.DefaultPageSettings, hwnd);
        }
    }
}

