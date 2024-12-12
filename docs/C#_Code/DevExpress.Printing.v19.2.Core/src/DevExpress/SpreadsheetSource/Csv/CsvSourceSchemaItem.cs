namespace DevExpress.SpreadsheetSource.Csv
{
    using DevExpress.Export.Xl;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class CsvSourceSchemaItem
    {
        public CsvSourceSchemaItem(int index, XlVariantValueType valueType)
        {
            Guard.ArgumentNonNegative(index, "index");
            this.Index = index;
            this.ValueType = valueType;
        }

        public int Index { get; private set; }

        public XlVariantValueType ValueType { get; private set; }
    }
}

