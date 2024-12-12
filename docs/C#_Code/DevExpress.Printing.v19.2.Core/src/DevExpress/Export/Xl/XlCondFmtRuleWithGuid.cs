namespace DevExpress.Export.Xl
{
    using System;

    public abstract class XlCondFmtRuleWithGuid : XlCondFmtRule
    {
        private readonly Guid id;

        protected XlCondFmtRuleWithGuid(XlCondFmtType ruleType) : base(ruleType)
        {
            this.id = Guid.NewGuid();
        }

        internal string GetRuleId() => 
            this.id.ToString("B").ToUpper();
    }
}

