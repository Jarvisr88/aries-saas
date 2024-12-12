namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;

    internal static class CollectionParser<T>
    {
        internal static List<T> GetValues(FunctionOperator isSameDayOp);
        internal static List<T> GetValues(InOperator inOp);
        internal static List<T> GetValues(IEnumerable<OperandValue> operands);
        internal static List<T> GetValues(object[] valuesArray);
    }
}

