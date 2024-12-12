namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class DXClipboard
    {
        private static IClipboardHelper dxClipboardHelper;

        public static bool ContainsText();
        private static IClipboardHelper CreateClipboardHelper();
        public static string GetText();
        public static void SetDataFromClipboardDataProvider(IClipboardDataProvider сlipboardDataProvider);
        public static void SetText(string text);

        private static IClipboardHelper ClipboardHelper { get; }
    }
}

