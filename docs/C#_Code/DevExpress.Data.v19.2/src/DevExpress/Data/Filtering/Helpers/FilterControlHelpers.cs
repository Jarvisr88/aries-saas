namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Utils;
    using DevExpress.XtraEditors.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class FilterControlHelpers
    {
        private static readonly Type[] ConvertibleTypes;

        static FilterControlHelpers();
        public static void ForceAdditionalParamsCount(IList<CriteriaOperator> additionalOperands, int p);
        public static void ForceAdditionalParamsCount(IList<CriteriaOperator> additionalOperands, int expectedCount, bool trim);
        public static IEnumerable<string> GetAvailableCustomFunctions(Type propertyType, bool onlyUnary = false);
        public static IEnumerable<string> GetAvailableGlobalCustomFunctions(ClauseNode node);
        public static IEnumerable<string> GetAvailableGlobalCustomFunctions(AnnotationAttributes annotationAttributes, CustomFunctionEventArgs args);
        public static IEnumerable<string> GetAvailableGlobalCustomFunctions(ClauseNode node, CustomFunctionEventArgs args);
        public static int GetLastElementIndex(INode node);
        public static INode GetLastNode(INode currentNode);
        public static INode GetNextNode(INode currentNode);
        public static INode GetNextNodeAfter(IGroupNode currentNode, INode node);
        internal static bool IsBinaryCustomFunction(FunctionOperator functionOp);
        private static bool IsBinaryCustomFunction(ICustomFunctionOperator function);
        internal static bool IsUnaryCustomFunction(FunctionOperator functionOp);
        private static bool IsUnaryCustomFunction(ICustomFunctionOperator function);
        private static bool IsUnaryCustomFunction(string functionName);
        public static bool IsValidClause(ClauseType clause, FilterColumnClauseClass clauseClass);
        public static bool IsValidClause(ClauseType clause, FilterColumnClauseClass clauseClass, bool showIsNullOperatorsForStrings);
        private static bool IsValidCustomFunctionType(FunctionOperator functionOp);
        private static bool IsValidCustomFunctionType(ICustomFunctionOperator function, bool onlyUnary = false);
        public static bool IsValidFunction(FunctionOperatorType functionType, FilterColumnClauseClass clauseClass);
        public static bool IsValidFunction(ICustomFunctionOperator function, Type propertyType, bool onlyUnary = false);
        public static bool IsValidFunction(string functionName, Type propertyType, bool onlyUnary = false);
        private static bool IsValidPropertyOperand(ICustomFunctionOperator function, Type propertyType, bool onlyUnary);
        private static bool IsValidResultType(ICustomFunctionOperator function);
        public static CriteriaOperator ToCriteria(INode node);
        public static bool TryGetClauseTypeByFunctionOperatorType(FunctionOperatorType functionType, out ClauseType type);
        public static bool TryGetCustomFunctionOperandType(string functionName, int index, out Type operandType);
        public static bool TryGetCustomFunctionOperandType(string functionName, int index, Type valueType, out Type operandType);
        public static bool TryGetCustomFunctionsFromAttributes(ClauseNode node, out IEnumerable<string> functionNames);
        public static bool TryGetCustomFunctionsFromAttributes(AnnotationAttributes annotationAttributes, Type propertyType, out IEnumerable<string> functionNames, bool onlyUnary = false);
        private static bool TryGetCustomFunctionsFromAttributes(IEnumerable<CustomFunctionAttribute> attributes, Type propertyType, out IEnumerable<string> names, bool onlyUnary = false);
        public static bool TryGetDisplayName(string functionType, out string displayName);
        public static bool TryGetFunctionImage(string functionType, out object image);
        public static bool TryGetFunctionImageFromAttribute(ClauseNode node, string functionName, out string image);
        private static bool TryGetFunctionParameter<T>(string functionType, Func<ICustomFunctionDisplayAttributes, T> getParameter, out T parameter) where T: class;
        private static bool TryGetFunctionParameterFromAttribute(ClauseNode node, string functionName, Func<CustomFunctionAttribute, string> getParameter, out string parameter);
        public static bool TryGetFunctionType(FunctionOperator functionOp, out object functionType);
        public static void ValidateAdditionalOperands(FunctionOperatorType functionType, IList<CriteriaOperator> additionalOperands);
        public static void ValidateAdditionalOperands(ClauseType operation, IList<CriteriaOperator> additionalOperands);
        public static void ValidateAdditionalOperands(string functionType, IList<CriteriaOperator> additionalOperands);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterControlHelpers.<>c <>9;
            public static Func<CustomFunctionAttribute, string> <>9__30_0;
            public static Func<CustomFunctionAttribute, string> <>9__31_0;
            public static Func<ICustomFunctionDisplayAttributes, object> <>9__32_0;
            public static Func<ICustomFunctionDisplayAttributes, string> <>9__33_0;

            static <>c();
            internal string <TryGetCustomFunctionsFromAttributes>b__30_0(CustomFunctionAttribute x);
            internal string <TryGetDisplayName>b__33_0(ICustomFunctionDisplayAttributes at);
            internal object <TryGetFunctionImage>b__32_0(ICustomFunctionDisplayAttributes at);
            internal string <TryGetFunctionImageFromAttribute>b__31_0(CustomFunctionAttribute at);
        }
    }
}

