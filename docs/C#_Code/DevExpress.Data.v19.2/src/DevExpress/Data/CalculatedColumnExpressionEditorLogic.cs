namespace DevExpress.Data
{
    using DevExpress.Data.ExpressionEditor;
    using System;
    using System.Collections.Generic;

    public class CalculatedColumnExpressionEditorLogic : ExpressionEditorLogic
    {
        private CalculatedColumn calculatedColumn;

        public CalculatedColumnExpressionEditorLogic(IExpressionEditor editor, CalculatedColumn calculatedColumn);
        private static string ConvertString(string expression);
        private static string EscapeFieldName(string fieldName);
        protected internal override void FillFieldsTable(Dictionary<string, string> itemsTable);
        protected internal override void FillParametersTable(Dictionary<string, string> itemsTable);
        protected override string GetExpressionMemoEditText();
        protected override object[] GetListOfInputTypesObjects();
        protected override void ValidateExpressionEx(string expression);
    }
}

