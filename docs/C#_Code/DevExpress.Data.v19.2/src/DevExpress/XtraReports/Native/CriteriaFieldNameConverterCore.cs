namespace DevExpress.XtraReports.Native
{
    using System;

    public static class CriteriaFieldNameConverterCore
    {
        public static string Convert(string source, FilterStringVisitorBase visitor);
        private static string CorrectBackRefs(string src, string replacement);
        public static string EscapeFieldName(string fieldName);
        private static int GetDepth(string s);
    }
}

