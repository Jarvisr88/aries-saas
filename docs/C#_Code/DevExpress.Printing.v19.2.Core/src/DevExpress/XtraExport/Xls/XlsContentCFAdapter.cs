namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using System;

    internal class XlsContentCFAdapter : IXlsCFContentAdapter
    {
        private XlsContentCF content;

        public XlsContentCFAdapter(XlsContentCF content)
        {
            this.content = content;
        }

        public void SetFilterPercent(bool value)
        {
        }

        public void SetFilterTop(bool value)
        {
        }

        public void SetFilterValue(int value)
        {
        }

        public void SetFirstFormula(byte[] formulaBytes)
        {
            this.content.FirstFormulaBytes = formulaBytes;
        }

        public void SetOperator(XlCondFmtOperator cfOperator)
        {
            this.content.Operator = cfOperator;
        }

        public void SetRuleTemplate(XlsCFRuleTemplate ruleTemplate)
        {
        }

        public void SetRuleType(XlCondFmtType ruleType)
        {
            if (ruleType == XlCondFmtType.Top10)
            {
                this.content.RuleType = XlCondFmtType.Expression;
            }
            else
            {
                this.content.RuleType = ruleType;
            }
        }

        public void SetSecondFormula(byte[] formulaBytes)
        {
            this.content.SecondFormulaBytes = formulaBytes;
        }

        public void SetStdDev(int value)
        {
        }

        public void SetTextRule(XlCondFmtSpecificTextType textRule)
        {
        }
    }
}

