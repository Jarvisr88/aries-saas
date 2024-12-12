namespace DevExpress.Data.Controls.ExpressionEditor
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class FunctionInfo : ItemInfoBase
    {
        internal const string DefaultCategoryName = "Functions";

        public FunctionInfo();
        public FunctionInfo(FunctionInfo other);
        public FunctionInfo(ICustomFunctionOperator functionOperator);
        public FunctionInfo(string category);
        public FunctionInfo(ICustomFunctionOperator functionOperator, string category);

        public string DisplayName { get; set; }

        public int CaretOffset { get; set; }

        public string FunctionCategory { get; set; }

        public Type[] ArgumentTypes { get; set; }

        public string UsageSample { get; set; }

        public ICustomFunctionOperator CustomFunctionOperator { get; set; }
    }
}

