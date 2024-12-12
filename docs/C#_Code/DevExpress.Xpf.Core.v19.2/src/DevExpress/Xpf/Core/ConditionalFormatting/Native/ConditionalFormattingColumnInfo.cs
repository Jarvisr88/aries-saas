namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Data;
    using DevExpress.Xpf.GridData;
    using System;

    public class ConditionalFormattingColumnInfo : IColumnInfo
    {
        private readonly Func<string> getExpressionFunc;
        private readonly string fieldName = Guid.NewGuid().ToString();

        public ConditionalFormattingColumnInfo(Func<string> getExpressionFunc)
        {
            this.getExpressionFunc = getExpressionFunc;
        }

        string IColumnInfo.FieldName =>
            this.fieldName;

        ColumnSortOrder IColumnInfo.SortOrder =>
            ColumnSortOrder.None;

        UnboundColumnType IColumnInfo.UnboundType =>
            UnboundColumnType.Object;

        string IColumnInfo.UnboundExpression =>
            this.getExpressionFunc();

        bool IColumnInfo.ReadOnly =>
            true;
    }
}

