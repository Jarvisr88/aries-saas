namespace DevExpress.Export.Xl
{
    using System;

    internal class ReferenceError : IXlCellError
    {
        private static ReferenceError instance = new ReferenceError();

        private ReferenceError()
        {
        }

        public static IXlCellError Instance =>
            instance;

        public XlCellErrorType Type =>
            XlCellErrorType.Reference;

        public string Name =>
            "#REF!";

        public string Description =>
            "Cell reference is not valid";

        public XlVariantValue Value =>
            XlVariantValue.ErrorReference;
    }
}

