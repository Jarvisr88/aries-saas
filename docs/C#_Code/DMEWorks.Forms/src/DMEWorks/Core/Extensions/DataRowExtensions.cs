namespace DMEWorks.Core.Extensions
{
    using DMEWorks.Core;
    using System;
    using System.Data;
    using System.Runtime.CompilerServices;

    public static class DataRowExtensions
    {
        public static decimal? GetDecimal(this DataRow row, DataColumn column)
        {
            if ((row != null) && ((column != null) && (ReferenceEquals(row.Table, column.Table) && !row.IsNull(column))))
            {
                return NullableConvert.ToDecimal(row[column]);
            }
            return null;
        }

        public static double? GetDouble(this DataRow row, DataColumn column)
        {
            if ((row != null) && ((column != null) && (ReferenceEquals(row.Table, column.Table) && !row.IsNull(column))))
            {
                return NullableConvert.ToDouble(row[column]);
            }
            return null;
        }
    }
}

