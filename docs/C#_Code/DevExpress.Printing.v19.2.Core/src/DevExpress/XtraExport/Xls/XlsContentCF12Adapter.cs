namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using System;

    internal class XlsContentCF12Adapter : IXlsCFContentAdapter
    {
        private XlsContentCF12 content;
        private bool withFormula;

        public XlsContentCF12Adapter(XlsContentCF12 content, bool withFormula)
        {
            this.content = content;
            this.withFormula = withFormula;
        }

        public void SetFilterPercent(bool value)
        {
            this.content.FilterPercent = false;
            this.content.FilterParams.Percent = value;
            this.content.FilterParams.IsEmpty = false;
        }

        public void SetFilterTop(bool value)
        {
            this.content.FilterTop = false;
            this.content.FilterParams.Top = value;
            this.content.FilterParams.IsEmpty = false;
        }

        public void SetFilterValue(int value)
        {
            this.content.FilterValue = 0;
            this.content.FilterParams.Value = value;
            this.content.FilterParams.IsEmpty = false;
        }

        public void SetFirstFormula(byte[] formulaBytes)
        {
            if (this.withFormula)
            {
                this.content.FirstFormulaBytes = formulaBytes;
            }
        }

        public void SetOperator(XlCondFmtOperator cfOperator)
        {
            this.content.Operator = cfOperator;
        }

        public void SetRuleTemplate(XlsCFRuleTemplate ruleTemplate)
        {
            this.content.RuleTemplate = ruleTemplate;
        }

        public void SetRuleType(XlCondFmtType ruleType)
        {
            this.content.RuleType = ruleType;
        }

        public void SetSecondFormula(byte[] formulaBytes)
        {
            if (this.withFormula)
            {
                this.content.SecondFormulaBytes = formulaBytes;
            }
        }

        public void SetStdDev(int value)
        {
            this.content.StdDev = value;
        }

        public void SetTextRule(XlCondFmtSpecificTextType textRule)
        {
            this.content.TextRule = textRule;
        }
    }
}

