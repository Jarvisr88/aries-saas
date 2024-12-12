namespace DevExpress.Export.Xl
{
    using System;

    internal class ValueNotAvailableError : IXlCellError
    {
        private static ValueNotAvailableError instance = new ValueNotAvailableError();

        private ValueNotAvailableError()
        {
        }

        public static IXlCellError Instance =>
            instance;

        public XlCellErrorType Type =>
            XlCellErrorType.NotAvailable;

        public string Name =>
            "#N/A";

        public string Description =>
            "Value is not available to a function or formula";

        public XlVariantValue Value =>
            XlVariantValue.ErrorValueNotAvailable;
    }
}

