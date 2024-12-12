namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Utils.Localization;
    using System;
    using System.Collections.Generic;

    public static class DisplayFormatValidationHelper
    {
        private static XtraLocalizer<EditorStringId> localizer;
        private static List<char> notValidAfterColonValues;

        static DisplayFormatValidationHelper();
        public static ErrorParameters GetNullErrorParameters();
        private static bool IsBaketsSuffixValid(string splitSuffix);
        public static ErrorParameters IsDisplayFormatStringValid(ref string text);
        public static ErrorParameters IsSuffixValid(string suffix);
        private static void RemoveDubleBakets(ref string splitSuffix);
        private static void RemoveDubleBaketsCore(ref string splitSuffix, string bakets);
        private static bool ValidateAfterColonValue(string text);
        private static bool ValidateBakets(string text);
        private static bool ValidateBakets(string text, char backet);
        private static bool ValidateComma(string text);
    }
}

