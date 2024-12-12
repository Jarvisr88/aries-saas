namespace DevExpress.Xpo.DB.Helpers
{
    using System;

    public interface ISqlGeneratorFormatterSupportOuterApply : ISqlGeneratorFormatter
    {
        string FormatOuterApply(string sql, string alias);

        bool NativeOuterApplySupported { get; }
    }
}

