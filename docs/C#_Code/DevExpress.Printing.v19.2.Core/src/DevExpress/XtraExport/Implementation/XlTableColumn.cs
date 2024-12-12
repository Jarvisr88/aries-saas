namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class XlTableColumn : XlTableFormattingBase, IXlTableColumn, IXlNamedObject
    {
        private readonly XlTable owner;
        private readonly int columnIndex;
        private readonly string name;
        private bool hasColumnFormula;
        private string formulaString;
        private IXlFormulaParameter formulaParameter;
        private XlExpression expression;

        public XlTableColumn(XlTable owner, string name, int columnIndex)
        {
            Guard.ArgumentIsNotNullOrEmpty(name, "name");
            this.owner = owner;
            this.name = name;
            this.columnIndex = columnIndex;
        }

        private void CheckHasData()
        {
            if ((this.owner != null) && this.owner.HasData)
            {
                throw new InvalidOperationException(" Table has data. You cannot change column formula.");
            }
        }

        internal XlExpression GetExpression(IXlExport exporter)
        {
            if (!this.hasColumnFormula)
            {
                return null;
            }
            if (this.expression == null)
            {
                if (this.formulaParameter != null)
                {
                    this.expression = new XlFormulaConverter(exporter.DocumentOptions).Convert(this.formulaParameter);
                }
                else if (!string.IsNullOrEmpty(this.formulaString))
                {
                    IXlFormulaParser formulaParser = exporter.FormulaParser;
                    if (formulaParser == null)
                    {
                        throw new InvalidOperationException("Formula parser required for this operation.");
                    }
                    if (this.owner != null)
                    {
                        int row = this.owner.Range.TopLeft.Row;
                        if (this.owner.HasHeaderRow)
                        {
                            row++;
                        }
                        XlExpressionContext context = new XlExpressionContext {
                            CurrentCell = new XlCellPosition(this.owner.Range.TopLeft.Column + this.columnIndex, row),
                            ReferenceMode = XlCellReferenceMode.Reference,
                            ExpressionStyle = XlExpressionStyle.Normal
                        };
                        XlExpression expression = formulaParser.Parse(this.formulaString, context);
                        if ((expression == null) || (expression.Count == 0))
                        {
                            throw new InvalidOperationException($"Can't parse formula '{this.formulaString}'.");
                        }
                        this.expression = expression;
                    }
                }
            }
            return this.expression;
        }

        public void SetFormula(IXlFormulaParameter formula)
        {
            this.CheckHasData();
            this.formulaParameter = formula;
            this.expression = null;
            this.formulaString = null;
            this.hasColumnFormula = this.formulaParameter != null;
        }

        public void SetFormula(XlExpression formula)
        {
            this.CheckHasData();
            this.formulaParameter = null;
            this.expression = formula;
            this.formulaString = null;
            this.hasColumnFormula = this.expression != null;
        }

        public void SetFormula(string formula)
        {
            this.CheckHasData();
            this.formulaParameter = null;
            this.expression = null;
            this.formulaString = (string.IsNullOrEmpty(formula) || (formula[0] != '=')) ? formula : formula.Substring(1);
            this.hasColumnFormula = !string.IsNullOrEmpty(this.formulaString);
        }

        public string Name =>
            this.name;

        public XlTotalRowFunction TotalRowFunction { get; set; }

        public string TotalRowLabel { get; set; }

        public bool HiddenButton { get; set; }

        public IXlFilterCriteria FilterCriteria { get; set; }

        internal bool HasColumnFormula =>
            this.hasColumnFormula;

        internal int ColumnIndex =>
            this.columnIndex;
    }
}

