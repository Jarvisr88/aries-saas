namespace DevExpress.Xpf.Editors.ExpressionEditor
{
    using DevExpress.Data.Controls.ExpressionEditor;
    using System;
    using System.Collections.Generic;

    public class AutoCompleteExpressionEditorContextWrapper
    {
        private ExpressionEditorContext _context;

        public AutoCompleteExpressionEditorContextWrapper(ExpressionEditorContext context)
        {
            if (context == null)
            {
                throw new NullReferenceException("Context can't be null");
            }
            this._context = context;
        }

        public List<ColumnInfo> Columns =>
            this._context.Columns;

        public List<ParameterInfo> Parameters =>
            this._context.Parameters;

        public List<FunctionInfo> Functions =>
            this._context.Functions;

        public List<OperatorInfo> Operators =>
            this._context.Operators;

        public List<ConstantInfo> Constants =>
            this._context.Constants;

        public ICriteriaOperatorValidatorProvider CriteriaOperatorValidatorProvider
        {
            get => 
                this._context.CriteriaOperatorValidatorProvider;
            set => 
                this._context.CriteriaOperatorValidatorProvider = value;
        }
    }
}

