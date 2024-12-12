namespace DevExpress.Export.Xl
{
    using System;

    internal class InvalidValueInFunctionError : IXlCellError
    {
        private static InvalidValueInFunctionError instance = new InvalidValueInFunctionError();

        private InvalidValueInFunctionError()
        {
        }

        public static IXlCellError Instance =>
            instance;

        public XlCellErrorType Type =>
            XlCellErrorType.Value;

        public string Name =>
            "#VALUE!";

        public string Description =>
            "A value used in the formula is of the wrong data type";

        public XlVariantValue Value =>
            XlVariantValue.ErrorInvalidValueInFunction;
    }
}

