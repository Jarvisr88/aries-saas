namespace DevExpress.Data.Controls.ExpressionEditor
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public sealed class ExpressionEditorContext
    {
        private static ExpressionEditorContext defaultContext;

        static ExpressionEditorContext();
        public ExpressionEditorContext();

        public static bool UseRichDocumentation { get; set; }

        public static ExpressionEditorContext Default { get; }

        public List<ColumnInfo> Columns { get; }

        public List<ParameterInfo> Parameters { get; }

        public List<FunctionInfo> Functions { get; }

        public List<OperatorInfo> Operators { get; }

        public List<ConstantInfo> Constants { get; }

        public IExpressionEditorColorProvider ColorProvider { get; set; }

        public ICriteriaOperatorValidatorProvider CriteriaOperatorValidatorProvider { get; set; }

        public ExpressionEditorOptionsBehavior OptionsBehavior { get; }

        public IAutoCompleteItemsProvider AutoCompleteItemsProvider { get; set; }

        public IColumnDynamicProvider ColumnDynamicProvider { get; set; }
    }
}

