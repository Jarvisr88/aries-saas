namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using System;

    internal interface IXlsCFContentAdapter
    {
        void SetFilterPercent(bool value);
        void SetFilterTop(bool value);
        void SetFilterValue(int value);
        void SetFirstFormula(byte[] formulaBytes);
        void SetOperator(XlCondFmtOperator cfOperator);
        void SetRuleTemplate(XlsCFRuleTemplate ruleTemplate);
        void SetRuleType(XlCondFmtType ruleType);
        void SetSecondFormula(byte[] formulaBytes);
        void SetStdDev(int value);
        void SetTextRule(XlCondFmtSpecificTextType textRule);
    }
}

