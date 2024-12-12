namespace DevExpress.Export.Xl
{
    using System;

    internal class NullIntersectionError : IXlCellError
    {
        private static NullIntersectionError instance = new NullIntersectionError();

        private NullIntersectionError()
        {
        }

        public static IXlCellError Instance =>
            instance;

        public XlCellErrorType Type =>
            XlCellErrorType.Null;

        public string Name =>
            "#NULL!";

        public string Description =>
            "The specified intersection includes two ranges that do not intersect.";

        public XlVariantValue Value =>
            XlVariantValue.ErrorNullIntersection;
    }
}

