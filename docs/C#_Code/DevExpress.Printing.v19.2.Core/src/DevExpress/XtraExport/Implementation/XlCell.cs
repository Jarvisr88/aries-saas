namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Runtime.CompilerServices;

    public class XlCell : IXlCell, IDisposable
    {
        private string formulaString;
        private XlVariantValue variantValue;
        private XlRichTextString richTextValue;

        public XlCell()
        {
            this.SharedFormulaPosition = XlCellPosition.InvalidValue;
        }

        internal void ApplyDifferentialFormatting(XlDifferentialFormatting formatting)
        {
            if (formatting != null)
            {
                this.Formatting ??= new XlCellFormatting();
                this.Formatting.MergeWith(formatting);
            }
        }

        public void ApplyFormatting(XlCellFormatting formatting)
        {
            if (formatting != null)
            {
                this.Formatting ??= new XlCellFormatting();
                this.Formatting.MergeWith(formatting);
            }
        }

        public void Dispose()
        {
        }

        public void SetFormula(IXlFormulaParameter formula)
        {
            this.Formula = formula;
            this.Expression = null;
            this.FormulaString = null;
            this.SharedFormulaRange = null;
            this.SharedFormulaPosition = XlCellPosition.InvalidValue;
        }

        public void SetFormula(XlExpression formula)
        {
            this.Formula = null;
            this.Expression = formula;
            this.FormulaString = null;
            this.SharedFormulaRange = null;
            this.SharedFormulaPosition = XlCellPosition.InvalidValue;
        }

        public void SetFormula(string formula)
        {
            this.Formula = null;
            this.Expression = null;
            this.FormulaString = formula;
            this.SharedFormulaRange = null;
            this.SharedFormulaPosition = XlCellPosition.InvalidValue;
        }

        public void SetRichText(XlRichTextString value)
        {
            this.richTextValue = value;
            if (value == null)
            {
                this.variantValue = XlVariantValue.Empty;
            }
            else
            {
                this.variantValue = value.Text;
            }
        }

        public void SetSharedFormula(XlCellPosition hostCell)
        {
            this.Formula = null;
            this.Expression = null;
            this.FormulaString = null;
            this.SharedFormulaRange = null;
            this.SharedFormulaPosition = hostCell;
        }

        public void SetSharedFormula(XlExpression formula, XlCellRange range)
        {
            this.Formula = null;
            this.Expression = formula;
            this.FormulaString = null;
            this.SharedFormulaRange = range;
            this.SharedFormulaPosition = XlCellPosition.InvalidValue;
        }

        public void SetSharedFormula(string formula, XlCellRange range)
        {
            this.Formula = null;
            this.Expression = null;
            this.FormulaString = formula;
            this.SharedFormulaRange = range;
            this.SharedFormulaPosition = XlCellPosition.InvalidValue;
        }

        public XlVariantValue Value
        {
            get => 
                this.variantValue;
            set
            {
                this.variantValue = value;
                this.richTextValue = null;
            }
        }

        public int RowIndex { get; internal set; }

        public int ColumnIndex { get; set; }

        public XlCellPosition Position =>
            new XlCellPosition(this.ColumnIndex, this.RowIndex);

        public XlCellFormatting Formatting { get; set; }

        internal IXlFormulaParameter Formula { get; set; }

        internal XlExpression Expression { get; set; }

        internal string FormulaString
        {
            get => 
                this.formulaString;
            set
            {
                if (!string.IsNullOrEmpty(value) && (value[0] == '='))
                {
                    this.formulaString = value.Substring(1);
                }
                else
                {
                    this.formulaString = value;
                }
            }
        }

        internal XlCellRange SharedFormulaRange { get; set; }

        internal XlCellPosition SharedFormulaPosition { get; set; }

        internal bool HasFormula =>
            ((this.Formula != null) || ((this.Expression != null) || !string.IsNullOrEmpty(this.FormulaString))) || this.SharedFormulaPosition.IsValid;

        internal bool HasFormulaWithoutValue =>
            this.HasFormula && this.Value.IsEmpty;

        internal XlRichTextString RichTextValue =>
            this.richTextValue;
    }
}

