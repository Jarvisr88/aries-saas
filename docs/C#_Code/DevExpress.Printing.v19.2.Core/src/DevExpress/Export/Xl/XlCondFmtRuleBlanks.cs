namespace DevExpress.Export.Xl
{
    using System;

    public class XlCondFmtRuleBlanks : XlCondFmtRuleWithFormatting
    {
        public XlCondFmtRuleBlanks(bool containsBlanks) : this(containsBlanks ? XlCondFmtType.ContainsBlanks : XlCondFmtType.NotContainsBlanks)
        {
        }
    }
}

