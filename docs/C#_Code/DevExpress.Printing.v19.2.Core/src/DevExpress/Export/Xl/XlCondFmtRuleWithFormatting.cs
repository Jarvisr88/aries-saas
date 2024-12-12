namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class XlCondFmtRuleWithFormatting : XlCondFmtRule
    {
        protected XlCondFmtRuleWithFormatting(XlCondFmtType ruleType) : base(ruleType)
        {
        }

        public XlDifferentialFormatting Formatting { get; set; }
    }
}

