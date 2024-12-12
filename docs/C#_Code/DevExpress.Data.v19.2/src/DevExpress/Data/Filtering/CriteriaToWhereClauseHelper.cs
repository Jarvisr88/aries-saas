namespace DevExpress.Data.Filtering
{
    using DevExpress.Data.Db;
    using System;
    using System.Runtime.InteropServices;

    public static class CriteriaToWhereClauseHelper
    {
        public static string GetAccessWhere(CriteriaOperator op);
        public static string GetDataSetWhere(CriteriaOperator op);
        public static string GetDynamicLinqWhere(CriteriaOperator op);
        public static string GetMsSqlWhere(CriteriaOperator op);
        public static string GetMsSqlWhere(CriteriaOperator op, bool setQuotedIdentifiersOff);
        public static string GetMsSqlWhere(CriteriaOperator op, Func<OperandProperty, string> propertyFormatter);
        public static string GetMsSqlWhere(CriteriaOperator op, MsSqlFormatterHelper.MSSqlServerVersion sqlServerVersion, bool setQuotedIdentifiersOff);
        public static string GetMsSqlWhere(CriteriaOperator op, MsSqlFormatterHelper.MSSqlServerVersion sqlServerVersion, Func<OperandProperty, string> propertyFormatter = null);
        public static string GetOracleWhere(CriteriaOperator op);
        public static string GetOracleWhere(CriteriaOperator op, bool forceQuotesOnOperandProperties);
        public static string GetOracleWhere(CriteriaOperator op, Func<OperandProperty, string> propertyFormatter);
    }
}

