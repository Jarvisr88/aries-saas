namespace DevExpress.Data.ExpressionEditor
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;

    public class ExpressionEditorLogicEx : ExpressionEditorLogic
    {
        public ExpressionEditorLogicEx(IExpressionEditor editor, IDataColumnInfo columnInfo);
        protected override string ConvertToCaption(string expression);
        public override string ConvertToFields(string expression);
        private static string EscapeFieldName(string fieldName);
        protected internal override void FillFieldsTable(Dictionary<string, string> itemsTable);
        protected internal override void FillParametersTable(Dictionary<string, string> itemsTable);
        protected override string GetExpressionMemoEditText();
        protected override object[] GetListOfInputTypesObjects();
        protected override void ValidateExpressionEx(string expression);

        private IDataColumnInfo ColumnInfo { get; }
    }
}

