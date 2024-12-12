namespace DevExpress.Data.Controls.ExpressionEditor
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class CreateTableFunction : ICustomFunctionOperatorBrowsable, ICustomFunctionOperator
    {
        private static readonly string description;
        private const int minOperandCount = 1;
        private const int maxOperandCount = 10;
        internal const string Name = "CreateTable";

        static CreateTableFunction();
        object ICustomFunctionOperator.Evaluate(params object[] operands);
        Type ICustomFunctionOperator.ResultType(params Type[] operands);
        bool ICustomFunctionOperatorBrowsable.IsValidOperandCount(int count);
        bool ICustomFunctionOperatorBrowsable.IsValidOperandType(int operandIndex, int operandCount, Type type);
        internal static void Register();
        internal static void Unregister();

        internal static ICustomFunctionOperatorBrowsable Instance { get; }

        string ICustomFunctionOperator.Name { get; }

        int ICustomFunctionOperatorBrowsable.MinOperandCount { get; }

        int ICustomFunctionOperatorBrowsable.MaxOperandCount { get; }

        string ICustomFunctionOperatorBrowsable.Description { get; }

        FunctionCategory ICustomFunctionOperatorBrowsable.Category { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CreateTableFunction.<>c <>9;
            public static Func<Array, int> <>9__12_0;

            static <>c();
            internal int <DevExpress.Data.Filtering.ICustomFunctionOperator.Evaluate>b__12_0(Array array);
        }
    }
}

