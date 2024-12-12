namespace DevExpress.Export.Xl
{
    using System;

    public class XlCondFmtRuleExpression : XlCondFmtRuleWithFormatting
    {
        private readonly XlExpression expression;
        private readonly string formula;

        public XlCondFmtRuleExpression(XlExpression expression) : base(XlCondFmtType.Expression)
        {
            this.expression = expression;
            this.formula = null;
        }

        public XlCondFmtRuleExpression(string formula) : base(XlCondFmtType.Expression)
        {
            this.expression = null;
            if (!string.IsNullOrEmpty(formula) && (formula[0] == '='))
            {
                this.formula = formula.Substring(1);
            }
            else
            {
                this.formula = formula;
            }
        }

        public XlExpression Expression =>
            this.expression;

        public string Formula =>
            this.formula;
    }
}

