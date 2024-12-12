namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Data.Filtering;
    using System;

    public interface ISqlGeneratorFormatterEx : ISqlGeneratorFormatter
    {
        string FormatFunction(ProcessParameter processParameter, FunctionOperatorType operatorType, params object[] operands);
    }
}

