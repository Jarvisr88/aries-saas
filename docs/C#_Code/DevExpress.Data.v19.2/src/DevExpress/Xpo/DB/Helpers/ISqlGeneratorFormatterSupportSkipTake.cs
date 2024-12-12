namespace DevExpress.Xpo.DB.Helpers
{
    using System;

    public interface ISqlGeneratorFormatterSupportSkipTake : ISqlGeneratorFormatter
    {
        string FormatSelect(string selectedPropertiesSql, string fromSql, string whereSql, string orderBySql, string groupBySql, string havingSql, int skipSelectedRecords, int topSelectedRecords);

        bool NativeSkipTakeSupported { get; }
    }
}

