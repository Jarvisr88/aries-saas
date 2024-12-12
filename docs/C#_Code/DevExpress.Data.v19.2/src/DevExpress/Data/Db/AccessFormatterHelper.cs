namespace DevExpress.Data.Db
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpo.DB.Helpers;
    using System;

    public static class AccessFormatterHelper
    {
        private const string NullString = "null";

        private static string AsString(object value);
        private static string FixNonFixedText(string toFix);
        private static string FnCharIndex(string[] operands);
        private static string FnConcat(string[] operands);
        private static string FnPadLeft(string[] operands);
        private static string FnPadRight(string[] operands);
        private static string FnRemove(string[] operands);
        private static string FnSubstring(string[] operands);
        private static string FnUtcNow(ProcessParameter processParameter);
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
        public static string FormatTable(string schema, string tableName);
        public static string FormatTable(string schema, string tableName, string tableAlias);
        public static string FormatUpdate(string tableName, string sets, string whereClause);
    }
}

