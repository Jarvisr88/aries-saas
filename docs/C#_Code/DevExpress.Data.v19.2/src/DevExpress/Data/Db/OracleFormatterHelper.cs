namespace DevExpress.Data.Db
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpo.DB;
    using DevExpress.Xpo.DB.Helpers;
    using System;

    public static class OracleFormatterHelper
    {
        private const string NullString = "NULL";
        private static readonly char[] achtungChars;

        static OracleFormatterHelper();
        private static string AsString(object value);
        private static string FixNonFixedText(string toFix);
        public static string FormatBinary(BinaryOperatorType operatorType, string leftOperand, string rightOperand);
        public static string FormatColumn(string columnName);
        public static string FormatColumn(string columnName, string tableAlias);
        public static string FormatConstant(object value);
        public static string FormatConstraint(string constraintName);
        public static string FormatDelete(string tableName, string whereClause);
        public static string FormatFunction(FunctionOperatorType operatorType, params string[] operands);
        public static string FormatFunction(ProcessParameter processParameter, FunctionOperatorType operatorType, params object[] operands);
        public static string FormatInsert(string tableName, string fields, string values);
        public static string FormatInsertDefaultValues(string tableName);
        public static string FormatOrder(string sortProperty, SortingDirection direction);
    }
}

