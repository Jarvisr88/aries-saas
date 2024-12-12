namespace DevExpress.Data.Controls.ExpressionEditor
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class JoinFunction : ICustomFunctionOperatorBrowsable, ICustomFunctionOperator
    {
        public const string Name = "Join";

        static JoinFunction();
        object ICustomFunctionOperator.Evaluate(params object[] operands);
        Type ICustomFunctionOperator.ResultType(params Type[] operands);
        bool ICustomFunctionOperatorBrowsable.IsValidOperandCount(int count);
        bool ICustomFunctionOperatorBrowsable.IsValidOperandType(int operandIndex, int operandCount, Type type);
        internal static void Register();
        internal static void Unregister();

        internal static ICustomFunctionOperatorBrowsable Instance { get; }

        FunctionCategory ICustomFunctionOperatorBrowsable.Category { get; }

        string ICustomFunctionOperatorBrowsable.Description { get; }

        int ICustomFunctionOperatorBrowsable.MaxOperandCount { get; }

        int ICustomFunctionOperatorBrowsable.MinOperandCount { get; }

        string ICustomFunctionOperator.Name { get; }
    }
}

