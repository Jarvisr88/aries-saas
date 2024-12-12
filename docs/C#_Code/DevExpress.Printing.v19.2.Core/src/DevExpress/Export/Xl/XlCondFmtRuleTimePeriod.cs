namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlCondFmtRuleTimePeriod : XlCondFmtRuleWithFormatting
    {
        public XlCondFmtRuleTimePeriod() : base(XlCondFmtType.TimePeriod)
        {
        }

        public XlCondFmtTimePeriod TimePeriod { get; set; }
    }
}

