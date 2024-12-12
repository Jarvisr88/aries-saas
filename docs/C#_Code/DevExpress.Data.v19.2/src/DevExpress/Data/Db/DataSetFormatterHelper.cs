namespace DevExpress.Data.Db
{
    using DevExpress.Data.Filtering;
    using System;

    public static class DataSetFormatterHelper
    {
        public static string EscapeStringForLike(string str);
        public static string FormatBinary(BinaryOperatorType operatorType, string leftOperand, string rightOperand);
        public static string FormatColumn(string columnName);
        public static string FormatColumn(string columnName, string tableAlias);
        public static string FormatFunction(FunctionOperatorType operatorType, params string[] operands);
        public static string FormatTable(string schema, string tableName);
        public static string FormatTable(string schema, string tableName, string tableAlias);
        public static string TrimForLike(string str);
    }
}

