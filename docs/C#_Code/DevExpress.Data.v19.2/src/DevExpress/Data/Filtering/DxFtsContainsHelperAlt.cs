namespace DevExpress.Data.Filtering
{
    using DevExpress.Data.Helpers;
    using System;

    public static class DxFtsContainsHelperAlt
    {
        private static bool AllowColumn(FindColumnInfo column, ref object value, ref FilterCondition filterCondition);
        public static CriteriaOperator Create(FindSearchParserResults parseResult, FilterCondition defaultCondition, bool isCapriciousSource);
        public static CriteriaOperator CreateExact(string exactText, FindColumnInfo[] properties, FilterCondition defaultCondition, bool isCapriciousSource);
        private static CriteriaOperator CreateFilter(string[] values, FindColumnInfo[] properties, FilterCondition filterCondition, bool isServerMode);
        private static CriteriaOperator DoFilterCondition(string originalValue, FindColumnInfo[] columns, FilterCondition defaultCondition, bool isCapriciousSource);
    }
}

