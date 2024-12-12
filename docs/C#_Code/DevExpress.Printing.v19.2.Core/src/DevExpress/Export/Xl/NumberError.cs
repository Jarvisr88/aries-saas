namespace DevExpress.Export.Xl
{
    using System;

    internal class NumberError : IXlCellError
    {
        private static NumberError instance = new NumberError();

        private NumberError()
        {
        }

        public static IXlCellError Instance =>
            instance;

        public XlCellErrorType Type =>
            XlCellErrorType.Number;

        public string Name =>
            "#NUM!";

        public string Description =>
            "Invalid numeric values in a formula or function";

        public XlVariantValue Value =>
            XlVariantValue.ErrorNumber;
    }
}

