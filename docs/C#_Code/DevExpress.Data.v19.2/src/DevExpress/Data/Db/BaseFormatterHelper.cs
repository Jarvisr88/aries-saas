namespace DevExpress.Data.Db
{
    using DevExpress.Data.Filtering;
    using System;

    public static class BaseFormatterHelper
    {
        public const string TheIifFunctionOperatorRequiresThreeOrMoreArgumentMessage = "The 'Iif' function operator requires three or more arguments. The number of arguments must be odd.";

        public static string DefaultFormatBinary(BinaryOperatorType operatorType, string leftOperand, string rightOperand);
        public static string DefaultFormatFunction(FunctionOperatorType operatorType, params string[] operands);
        public static string DefaultFormatUnary(UnaryOperatorType operatorType, string operand);
    }
}

