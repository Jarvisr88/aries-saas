namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;

    internal class XlCellProxy : IXlCell, IDisposable
    {
        private IXlExport exporter;
        private readonly IXlCell subject;

        public XlCellProxy(IXlExport exporter, IXlCell subject)
        {
            this.exporter = exporter;
            this.subject = subject;
        }

        public void ApplyFormatting(XlCellFormatting formatting)
        {
            this.subject.ApplyFormatting(formatting);
        }

        public void Dispose()
        {
            if (this.exporter != null)
            {
                this.exporter.EndCell();
                this.exporter = null;
            }
        }

        public void SetFormula(IXlFormulaParameter formula)
        {
            this.subject.SetFormula(formula);
        }

        public void SetFormula(XlExpression formula)
        {
            this.subject.SetFormula(formula);
        }

        public void SetFormula(string formula)
        {
            this.subject.SetFormula(formula);
        }

        public void SetRichText(XlRichTextString value)
        {
            this.subject.SetRichText(value);
        }

        public void SetSharedFormula(XlCellPosition hostCell)
        {
            this.subject.SetSharedFormula(hostCell);
        }

        public void SetSharedFormula(XlExpression formula, XlCellRange range)
        {
            this.subject.SetSharedFormula(formula, range);
        }

        public void SetSharedFormula(string formula, XlCellRange range)
        {
            this.subject.SetSharedFormula(formula, range);
        }

        public XlVariantValue Value
        {
            get => 
                this.subject.Value;
            set => 
                this.subject.Value = value;
        }

        public int RowIndex =>
            this.subject.RowIndex;

        public int ColumnIndex =>
            this.subject.ColumnIndex;

        public XlCellPosition Position =>
            this.subject.Position;

        public XlCellFormatting Formatting
        {
            get => 
                this.subject.Formatting;
            set => 
                this.subject.Formatting = value;
        }
    }
}

