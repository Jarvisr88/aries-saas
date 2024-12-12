namespace DevExpress.Export.Xl
{
    using System;

    public interface IXlTableColumn
    {
        void SetFormula(IXlFormulaParameter formula);
        void SetFormula(XlExpression formula);
        void SetFormula(string formula);

        string Name { get; }

        XlTotalRowFunction TotalRowFunction { get; set; }

        string TotalRowLabel { get; set; }

        XlDifferentialFormatting DataFormatting { get; set; }

        XlDifferentialFormatting TotalRowFormatting { get; set; }

        bool HiddenButton { get; set; }

        IXlFilterCriteria FilterCriteria { get; set; }
    }
}

