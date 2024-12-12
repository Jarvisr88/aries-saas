namespace DevExpress.Export.Xl
{
    using System;

    internal class DivisionByZeroError : IXlCellError
    {
        private static DivisionByZeroError instance = new DivisionByZeroError();

        private DivisionByZeroError()
        {
        }

        public static IXlCellError Instance =>
            instance;

        public XlCellErrorType Type =>
            XlCellErrorType.DivisionByZero;

        public string Name =>
            "#DIV/0!";

        public string Description =>
            "Division by zero!";

        public XlVariantValue Value =>
            XlVariantValue.ErrorDivisionByZero;
    }
}

