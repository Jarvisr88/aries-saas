namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using System;
    using System.Collections.Generic;

    internal static class ColumnTableCreator<TCol> where TCol: class, IColumn
    {
        public static Dictionary<string, TValue> Create<TValue>(IEnumerable<TCol> gridColumns, Func<TCol, TValue> value)
        {
            Dictionary<string, TValue> dictionary = new Dictionary<string, TValue>();
            foreach (TCol local in gridColumns)
            {
                TValue local2 = value(local);
                if ((local2 != null) && (!string.IsNullOrEmpty(local.FieldName) && !dictionary.ContainsKey(local.FieldName)))
                {
                    dictionary.Add(local.FieldName, value(local));
                }
            }
            return dictionary;
        }
    }
}

