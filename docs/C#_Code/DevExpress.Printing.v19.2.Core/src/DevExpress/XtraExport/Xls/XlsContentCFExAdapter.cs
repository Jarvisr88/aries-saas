namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using System;

    internal class XlsContentCFExAdapter : IXlsCFContentAdapter
    {
        private XlsContentCFEx content;

        public XlsContentCFExAdapter(XlsContentCFEx content)
        {
            this.content = content;
        }

        public void SetFilterPercent(bool value)
        {
            this.content.FilterPercent = value;
        }

        public void SetFilterTop(bool value)
        {
            this.content.FilterTop = value;
        }

        public void SetFilterValue(int value)
        {
            this.content.FilterValue = value;
        }

        public void SetFirstFormula(byte[] formulaBytes)
        {
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
        }

        public void SetSecondFormula(byte[] formulaBytes)
        {
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

