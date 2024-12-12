namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.ExpressionEditor;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;

    public class FunctionOperatorHelper
    {
        private static Dictionary<FunctionOperatorType, Dictionary<int, ItemClickHelper.FunctionInfo>> functionInfoStaticDict;
        private static List<ItemClickHelper.FunctionInfo> aggregateFunctionInfo;

        static FunctionOperatorHelper();
        public static void GetAllCustomFunctionInfo(IExpressionEditor editor, List<ItemClickHelper.FunctionInfo> result);
        public static ItemClickHelper.FunctionInfo[] GetAllFunctionInfo(IExpressionEditor editor);
        public static int[] GetFunctionArgumentsCount(FunctionOperatorType functionType);
        public static ItemClickHelper.FunctionInfo GetFunctionInfo(IExpressionEditor editor, FunctionOperatorType functionType, int argumentsCount);
        private static ItemClickHelper.FunctionInfo GetLocalizedFunctionInfo(IExpressionEditor editor, ItemClickHelper.FunctionInfo functionInfo);
        public static bool IsValidCustomFunctionArgumentsCount(string functionName, int argumentsCount);
        private static FunctionEditorCategory ToFunctionEditorCategory(FunctionCategory category);

        private class FunctionInfoComparer : IComparer<ItemClickHelper.FunctionInfo>
        {
            int IComparer<ItemClickHelper.FunctionInfo>.Compare(ItemClickHelper.FunctionInfo x, ItemClickHelper.FunctionInfo y);
        }
    }
}

