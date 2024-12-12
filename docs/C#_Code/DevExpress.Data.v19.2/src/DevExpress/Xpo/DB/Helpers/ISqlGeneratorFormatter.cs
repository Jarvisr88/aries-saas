namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpo.DB;
    using System;

    public interface ISqlGeneratorFormatter
    {
        string ComposeSafeColumnName(string columnName);
        string ComposeSafeSchemaName(string tableName);
        string ComposeSafeTableName(string tableName);
        string FormatBinary(BinaryOperatorType operatorType, string leftOperand, string rightOperand);
        string FormatColumn(string columnName);
        string FormatColumn(string columnName, string tableAlias);
        string FormatDelete(string tableName, string whereClause);
        string FormatFunction(FunctionOperatorType operatorType, params string[] operands);
        string FormatInsert(string tableName, string fields, string values);
        string FormatInsertDefaultValues(string tableName);
        string FormatOrder(string sortProperty, SortingDirection direction);
        string FormatSelect(string selectedPropertiesSql, string fromSql, string whereSql, string orderBySql, string groupBySql, string havingSql, int topSelectedRecords);
        string FormatTable(string schema, string tableName);
        string FormatTable(string schema, string tableName, string tableAlias);
        string FormatUnary(UnaryOperatorType operatorType, string operand);
        string FormatUpdate(string tableName, string sets, string whereClause);
        string GetParameterName(OperandValue parameter, int index, ref bool createParameter);

        bool BraceJoin { get; }

        bool SupportNamedParameters { get; }
    }
}

