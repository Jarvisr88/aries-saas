namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Linq;
    using DevExpress.Data.Linq.Helpers;
    using System;
    using System.Collections.Concurrent;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class LikeCustomFunction : ICustomFunctionOperatorEvaluatableWithCaseSensitivity, ICustomFunctionOperator, ICustomFunctionOperatorConvertibleToExpression, ICustomFunctionOperatorCompileableWithCaseSensitivity, ICustomFunctionOperatorCompileable, ICustomFunctionOperatorCompileableWithSettings, ICustomFunctionOperatorFormattable, ICustomFunctionOperatorEvaluatableWithCaseSensitivityAnd3ValuedLogic
    {
        public const string Name = "Like";
        private static readonly ConcurrentDictionary<Type, LikeCustomFunction.TypeItem> TypeInformationCache;

        static LikeCustomFunction();
        public static FunctionOperator Convert(BinaryOperator like);
        public static BinaryOperator Convert(FunctionOperator like);
        public static FunctionOperator Create(CriteriaOperator value, CriteriaOperator pattern);
        object ICustomFunctionOperatorEvaluatableWithCaseSensitivityAnd3ValuedLogic.Evaluate(bool caseSensitive, bool is3VL, params object[] operands);
        object ICustomFunctionOperator.Evaluate(params object[] operands);
        Type ICustomFunctionOperator.ResultType(params Type[] operands);
        Expression ICustomFunctionOperatorCompileable.Create(Expression[] operands);
        Expression ICustomFunctionOperatorCompileableWithCaseSensitivity.Create(bool caseSensitive, Expression[] operands);
        Expression ICustomFunctionOperatorCompileableWithSettings.Create(CriteriaCompilerAuxSettings settings, Expression[] operands);
        Expression ICustomFunctionOperatorConvertibleToExpression.Convert(ICriteriaToExpressionConverter converter, Expression[] operands);
        object ICustomFunctionOperatorEvaluatableWithCaseSensitivity.Evaluate(bool caseSensitive, params object[] operands);
        string ICustomFunctionOperatorFormattable.Format(Type providerType, params string[] operands);
        private static object EvaluateCore(bool caseSensitive, bool is3ValLogic, object[] operands);
        private static LikeCustomFunction.TypeItem GetTypeInformation(Type type);
        private static LikeCustomFunction.TypeItem GetTypeInformationCore(Type type);
        public static bool IsBinaryCompatibleLikeFunction(CriteriaOperator op);
        public static bool IsName(string nm);
        private static Expression MakeCompile(bool caseSensitive, bool is3ValuedLogic, Expression[] operands);
        private Expression MakeEf(ICriteriaToExpressionConverter converter, Expression valueExpression, Expression patternExpression, EntityQueryTypeInfo queryTypeInfo);
        private static Expression MakeLinq(ICriteriaToExpressionConverter converter, Expression vExpr, Expression pExpr);

        string ICustomFunctionOperator.Name { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LikeCustomFunction.<>c <>9;
            public static Predicate<OperandValue> <>9__5_1;
            public static Predicate<FunctionOperator> <>9__5_0;
            public static Func<Type, LikeCustomFunction.TypeItem> <>9__23_0;

            static <>c();
            internal LikeCustomFunction.TypeItem <GetTypeInformation>b__23_0(Type t);
            internal bool <IsBinaryCompatibleLikeFunction>b__5_0(FunctionOperator f);
            internal bool <IsBinaryCompatibleLikeFunction>b__5_1(OperandValue nm);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct TypeItem
        {
            public bool IsPostgreSqlConnectionProvider;
            public bool IsDynamicLinqWhereGenerator;
        }
    }
}

