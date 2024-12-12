namespace DevExpress.Data.Helpers
{
    using System;
    using System.Text.RegularExpressions;

    public static class SplitStringHelper
    {
        private static Regex reg1;
        private static Regex reg2;

        static SplitStringHelper();
        public static string SplitPascalCaseString(string value);
    }
}

