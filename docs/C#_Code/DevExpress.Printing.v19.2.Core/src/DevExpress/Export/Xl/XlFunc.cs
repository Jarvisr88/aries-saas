namespace DevExpress.Export.Xl
{
    using DevExpress.Utils;
    using DevExpress.XtraExport.Implementation;
    using System;
    using System.Collections.Generic;

    public static class XlFunc
    {
        public static IXlFormulaParameter Average(params IXlFormulaParameter[] parameters) => 
            new XlAverageFunction(parameters);

        public static IXlFormulaParameter Column() => 
            new XlColumnFunction();

        public static IXlFormulaParameter Column(XlCellRange range) => 
            new XlColumnFunction(range);

        public static IXlFormulaParameter Concatenate(params IXlFormulaParameter[] parameters) => 
            new XlConcatenateFunction(parameters);

        public static IXlFormulaParameter Count(params IXlFormulaParameter[] parameters) => 
            new XlCountFunction(parameters);

        public static IXlFormulaParameter CountA(params IXlFormulaParameter[] parameters) => 
            new XlCountAFunction(parameters);

        public static IXlFormulaParameter CountBlank(XlCellRange range) => 
            new XlCountBlankFunction(range);

        public static IXlFormulaParameter If(IXlFormulaParameter predicate, IXlFormulaParameter thenParam, IXlFormulaParameter elseParam) => 
            new XlIfFunction(predicate, thenParam, elseParam);

        public static IXlFormulaParameter Max(params IXlFormulaParameter[] parameters) => 
            new XlMaxFunction(parameters);

        public static IXlFormulaParameter Min(params IXlFormulaParameter[] parameters) => 
            new XlMinFunction(parameters);

        public static IXlFormulaParameter Param(XlVariantValue value) => 
            new XlFormulaParameter(value);

        public static IXlFormulaParameter Row() => 
            new XlRowFunction();

        public static IXlFormulaParameter Row(XlCellRange range) => 
            new XlRowFunction(range);

        public static IXlFormulaParameter Subtotal(XlCellRange range, XlSummary summary, bool ignoreHidden) => 
            XlSubtotalFunction.Create(range, summary, ignoreHidden);

        public static IXlFormulaParameter Subtotal(IList<XlCellRange> ranges, XlSummary summary, bool ignoreHidden) => 
            XlSubtotalFunction.Create(ranges, summary, ignoreHidden);

        public static IXlFormulaParameter Sum(params IXlFormulaParameter[] parameters) => 
            new XlSumFunction(parameters);

        public static IXlFormulaParameter Text(IXlFormulaParameter formula, XlNumberFormat numberFormat) => 
            XlTextFunction.Create(formula, numberFormat);

        public static IXlFormulaParameter Text(XlVariantValue value, XlNumberFormat numberFormat) => 
            XlTextFunction.Create(value, numberFormat);

        public static IXlFormulaParameter Text(IXlFormulaParameter formula, string netFormatString, bool isDateTimeFormatString) => 
            XlTextFunction.Create(formula, netFormatString, isDateTimeFormatString);

        public static IXlFormulaParameter Text(XlVariantValue value, string netFormatString, bool isDateTimeFormatString) => 
            XlTextFunction.Create(value, netFormatString, isDateTimeFormatString);

        public static IXlFormulaParameter Trunc(IXlFormulaParameter number) => 
            new XlTruncFunction(number);

        public static IXlFormulaParameter Trunc(IXlFormulaParameter number, int num_digits)
        {
            Guard.ArgumentNonNegative(num_digits, "num_digits");
            return new XlTruncFunction(number, num_digits);
        }

        public static IXlFormulaParameter VLookup(IXlFormulaParameter lookupValue, XlCellRange table, int columnIndex, bool rangeLookup) => 
            XlVLookupFunction.Create(lookupValue, table, columnIndex, rangeLookup);

        public static IXlFormulaParameter VLookup(XlVariantValue lookupValue, XlCellRange table, int columnIndex, bool rangeLookup) => 
            XlVLookupFunction.Create(lookupValue, table, columnIndex, rangeLookup);
    }
}

