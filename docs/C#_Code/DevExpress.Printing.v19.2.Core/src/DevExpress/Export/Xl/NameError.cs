namespace DevExpress.Export.Xl
{
    using System;

    internal class NameError : IXlCellError
    {
        private static NameError instance = new NameError();

        internal NameError()
        {
        }

        public static IXlCellError Instance =>
            instance;

        public XlCellErrorType Type =>
            XlCellErrorType.Name;

        public string Name =>
            "#NAME?";

        public string Description =>
            "Function does not exist";

        public XlVariantValue Value =>
            XlVariantValue.ErrorName;
    }
}

