namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections.Generic;

    public interface IXlFormulaEngine
    {
        IXlFormulaParameter Concatenate(params IXlFormulaParameter[] parameters);
        IXlFormulaParameter Param(XlVariantValue value);
        IXlFormulaParameter Subtotal(XlCellRange range, XlSummary summary, bool ignoreHidden);
        IXlFormulaParameter Subtotal(IList<XlCellRange> ranges, XlSummary summary, bool ignoreHidden);
        IXlFormulaParameter Text(IXlFormulaParameter value, string netFormatString, bool isDateTimeFormatString);
        IXlFormulaParameter Text(XlVariantValue value, string netFormatString, bool isDateTimeFormatString);
        IXlFormulaParameter VLookup(IXlFormulaParameter lookupValue, XlCellRange table, int columnIndex, bool rangeLookup);
        IXlFormulaParameter VLookup(XlVariantValue lookupValue, XlCellRange table, int columnIndex, bool rangeLookup);
    }
}

