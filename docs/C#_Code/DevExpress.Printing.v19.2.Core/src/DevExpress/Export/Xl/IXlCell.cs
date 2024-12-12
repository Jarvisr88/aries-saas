namespace DevExpress.Export.Xl
{
    using System;

    public interface IXlCell : IDisposable
    {
        void ApplyFormatting(XlCellFormatting formatting);
        void SetFormula(IXlFormulaParameter formula);
        void SetFormula(XlExpression formula);
        void SetFormula(string formula);
        void SetRichText(XlRichTextString value);
        void SetSharedFormula(XlCellPosition hostCell);
        void SetSharedFormula(XlExpression formula, XlCellRange range);
        void SetSharedFormula(string formula, XlCellRange range);

        XlVariantValue Value { get; set; }

        int RowIndex { get; }

        int ColumnIndex { get; }

        XlCellPosition Position { get; }

        XlCellFormatting Formatting { get; set; }
    }
}

