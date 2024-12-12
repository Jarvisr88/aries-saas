namespace DevExpress.Data.Filtering
{
    using DevExpress.Data.Helpers;
    using System;

    public static class DxFtsContainsHelper
    {
        public const string DxFtsPropertyPrefix = "DxFts_";
        public const string DxFtsContainsCustomFunctionName = "DxFtsContains";
        public const string DxFtsLikeCustomFunctionName = "DxFtsLike";

        public static CriteriaOperator BuildContains(CriteriaOperator value);
        public static CriteriaOperator BuildLike(CriteriaOperator value);
        public static CriteriaOperator Create(string[] columns, FindSearchParserResults parseResult);
        public static CriteriaOperator Create(string[] columns, FindSearchParserResults parseResult, FilterCondition filterCondition);
        private static CriteriaOperator CreateFilter(string[] values, CriteriaOperator[] properties, FilterCondition filterCondition);
        private static CriteriaOperator DoFilterCondition(CriteriaOperator value, CriteriaOperator[] columns, FilterCondition filterCondition);
        public static CriteriaOperator Expand(CriteriaOperator criteria, CriteriaOperator[] columns);
    }
}

