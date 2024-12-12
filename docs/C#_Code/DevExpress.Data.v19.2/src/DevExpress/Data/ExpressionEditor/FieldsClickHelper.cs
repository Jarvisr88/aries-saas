namespace DevExpress.Data.ExpressionEditor
{
    using DevExpress.Data;
    using System;

    internal class FieldsClickHelper : ItemClickHelper
    {
        public FieldsClickHelper(IExpressionEditor editor);
        protected override void FillItemsTable();

        public override ColumnSortOrder ParametersSortOrder { get; }
    }
}

