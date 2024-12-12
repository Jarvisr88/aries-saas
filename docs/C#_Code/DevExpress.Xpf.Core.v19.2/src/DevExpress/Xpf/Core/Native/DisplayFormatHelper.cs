namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.InteropServices;

    public static class DisplayFormatHelper
    {
        private static void GetCleanFormat(string dirtyFormat, out string displayFormat);
        public static string GetDisplayFormatFromParts(string prefix, string currentDisplayFormat, string suffix);
        public static bool GetDisplayFormatParts(string sourceDisplayFormat, out string prefix, out string displayFormat, out string suffix, string nullValueDisplayFormat);
        public static string GetDisplayTextFromDisplayFormat(IFormatProvider language, string displayFormat, params object[] displayFormatArgs);
        private static bool GetIndexOfZeroClose(string subDisplayFormat, int indexOfZeroOpen, out int indexOfZeroClose);
        public static int GetLastParameterIndex(string format);
        private static bool GetSplittedDifsplayFormat(string sourceDisplayFormat, out string prefix, out string displayFormat, out string suffix);
        public static string GetSuffixFromVisualString(string str);
        public static string GetVisualStringFromSuffix(string suffix);
    }
}

