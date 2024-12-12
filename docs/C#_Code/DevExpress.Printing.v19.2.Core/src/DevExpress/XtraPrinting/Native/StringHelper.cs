namespace DevExpress.XtraPrinting.Native
{
    using System;

    public static class StringHelper
    {
        public static string GetNonEmptyValue(params string[] values);
        public static void ValidateFormatString(string formatString);
        public static void ValidateFormatStringForTwoArguments(string formatString);
    }
}

