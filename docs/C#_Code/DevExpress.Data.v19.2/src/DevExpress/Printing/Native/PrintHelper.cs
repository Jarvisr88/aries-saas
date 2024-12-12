namespace DevExpress.Printing.Native
{
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.ComponentModel;
    using System.Drawing.Printing;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    internal static class PrintHelper
    {
        public static Task<bool> GetCollateAsync(PrinterSettings settings) => 
            Task.Factory.StartNew<bool>(() => settings.Collate);

        public static Task<short> GetCopiesAsync(PrinterSettings settings) => 
            Task.Factory.StartNew<short>(() => settings.Copies);

        public static Task<Duplex> GetDuplexAsync(PrinterSettings settings) => 
            Task.Factory.StartNew<Duplex>(() => settings.Duplex);

        public static Task<PrinterSettings.PaperSizeCollection> GetPaperSizesAsync(PrinterSettings settings) => 
            Task.Factory.StartNew<PrinterSettings.PaperSizeCollection>(() => settings.PaperSizes);

        public static Task<PrinterSettings.PaperSourceCollection> GetPaperSourcesAsync(PrinterSettings settings) => 
            Task.Factory.StartNew<PrinterSettings.PaperSourceCollection>(() => settings.PaperSources);

        public static Exception GetPrinterException(Exception ex)
        {
            string str;
            return ((!(ex is Win32Exception) || !TryGetMessage(((Win32Exception) ex).NativeErrorCode, out str)) ? ((ex is InvalidPrinterException) ? ex : null) : new Exception(str, ex));
        }

        public static Task<string> GetPrintFileNameAsync(PrinterSettings settings) => 
            Task.Factory.StartNew<string>(() => settings.PrintFileName);

        public static Task<PrintRange> GetPrintRangeAsync(PrinterSettings settings) => 
            Task.Factory.StartNew<PrintRange>(() => settings.PrintRange);

        public static Task<bool> GetPrintToFileAsync(PrinterSettings settings) => 
            Task.Factory.StartNew<bool>(() => settings.PrintToFile);

        private static bool TryGetMessage(int nativeErrorCode, out string message)
        {
            if ((nativeErrorCode != 0x4c7) && ((nativeErrorCode != 0xbbb) && (nativeErrorCode != 0x3f)))
            {
                message = (nativeErrorCode != 0x709) ? ((nativeErrorCode != 0x6ba) ? PreviewStringId.Msg_WrongPrinting.GetString() : PreviewStringId.Msg_UnavailableNetPrinter.GetString()) : PreviewStringId.Msg_WrongPrinter.GetString();
                return true;
            }
            message = string.Empty;
            return false;
        }
    }
}

