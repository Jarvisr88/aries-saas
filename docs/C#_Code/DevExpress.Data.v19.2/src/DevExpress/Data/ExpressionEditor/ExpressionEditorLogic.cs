namespace DevExpress.Data.ExpressionEditor
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public abstract class ExpressionEditorLogic
    {
        protected readonly IExpressionEditor editor;
        protected readonly object contextInstance;
        private ItemClickHelper itemClickHelper;
        private FunctionEditorCategory availableCategories;

        protected ExpressionEditorLogic(IExpressionEditor editor, object contextInstance);
        public bool CanCloseWithOKResult();
        protected virtual string ConvertToCaption(string expression);
        public virtual string ConvertToFields(string expression);
        protected internal abstract void FillFieldsTable(Dictionary<string, string> itemsTable);
        protected internal abstract void FillParametersTable(Dictionary<string, string> itemsTable);
        public string GetExpression();
        protected abstract string GetExpressionMemoEditText();
        protected virtual IList<FunctionEditorCategory> GetFunctionsTypeNames();
        private static string GetFunctionType(IExpressionEditor editor, string functionType);
        private ExpressionEditorLogic.FunctionTypeItem[] GetFunctionTypes();
        protected virtual ItemClickHelper GetItemClickHelper(string selectedItemText, IExpressionEditor editor);
        protected abstract object[] GetListOfInputTypesObjects();
        public void Initialize();
        public void InsertTextInExpressionMemo(string text);
        public void OnFunctionTypeChanged();
        public void OnInputParametersChanged();
        public void OnInputTypeChanged();
        public void OnInsertInputParameter();
        public void OnInsertOperation(string operation);
        public void OnLoad();
        public void OnWrapExpression();
        protected virtual void RefreshInputParameters();
        public void ResetMemoText();
        protected virtual void ShowError(Exception exception);
        protected virtual bool ValidateExpression();
        protected virtual void ValidateExpressionEx(string expression);

        protected IMemoEdit ExpressionMemoEdit { get; }

        private ISelector ListOfInputTypes { get; }

        private ISelector ListOfInputParameters { get; }

        private ISelector FunctionsTypes { get; }

        internal FunctionEditorCategory AvailableCategories { get; }

        public class FunctionTypeItem
        {
            public override string ToString();

            public FunctionEditorCategory Category { get; set; }

            public string Name { get; set; }
        }
    }
}

