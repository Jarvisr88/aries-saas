namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    internal class XlVLookupFunction : IXlFormulaParameter
    {
        public static XlVLookupFunction Create(IXlFormulaParameter lookupValue, XlCellRange table, int columnIndex, bool rangeLookup) => 
            new XlVLookupFunction { 
                LookupValue = lookupValue,
                Table = table,
                ColumnIndex = columnIndex,
                RangeLookup = rangeLookup
            };

        public static XlVLookupFunction Create(XlVariantValue lookupValue, XlCellRange table, int columnIndex, bool rangeLookup) => 
            Create(new XlFormulaParameter(lookupValue), table, columnIndex, rangeLookup);

        public string ToString(CultureInfo culture)
        {
            XlVariantValue value2 = new XlVariantValue {
                BooleanValue = this.RangeLookup
            };
            string str = (this.LookupValue == null) ? "#VALUE!" : this.LookupValue.ToString(culture);
            return $"VLOOKUP({str}, {this.Table.ToString()}, {this.ColumnIndex}, {value2.ToText().TextValue})";
        }

        public IXlFormulaParameter LookupValue { get; set; }

        public XlCellRange Table { get; set; }

        public int ColumnIndex { get; set; }

        public bool RangeLookup { get; set; }
    }
}

