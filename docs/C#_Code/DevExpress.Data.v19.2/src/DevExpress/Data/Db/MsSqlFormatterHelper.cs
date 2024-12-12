namespace DevExpress.Data.Db
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpo.DB.Helpers;
    using System;

    public static class MsSqlFormatterHelper
    {
        public const string NullString = "null";
        private static readonly char[] achtungChars;

        static MsSqlFormatterHelper();
        private static string AsString(object value);
        private static string FixNonFixedText(string toFix);
        private static string FnConcat(string[] operands);
        public static string FormatBinary(BinaryOperatorType operatorType, string leftOperand, string rightOperand);
        public static string FormatColumn(string columnName);
        public static string FormatColumn(string columnName, string tableAlias);
        public static string FormatConstant(object value, MsSqlFormatterHelper.MSSqlServerVersion sqlServerVersion);
        public static string FormatConstraint(string constraintName);
        public static string FormatDelete(string tableName, string whereClause);
        public static string FormatFunction(FunctionOperatorType operatorType, MsSqlFormatterHelper.MSSqlServerVersion sqlServerVersion, params string[] operands);
        public static string FormatFunction(ProcessParameter processParameter, FunctionOperatorType operatorType, MsSqlFormatterHelper.MSSqlServerVersion sqlServerVersion, params object[] operands);
        public static string FormatInsert(string tableName, string fields, string values);
        public static string FormatInsertDefaultValues(string tableName);
        public static string FormatUpdate(string tableName, string sets, string whereClause);
        private static string GetVarCharMaxTypeName(MsSqlFormatterHelper.MSSqlServerVersion sqlServerVersion);

        public class MSSqlServerVersion
        {
            public bool Is2000;
            public bool Is2005;
            public bool Is2008;
            public bool? IsAzure;

            public MSSqlServerVersion(bool is2000, bool is2005, bool? isAzure);
            public MSSqlServerVersion(bool is2000, bool is2005, bool is2008, bool? isAzure);
        }
    }
}

