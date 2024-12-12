namespace DevExpress.Export.Xl
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class XlCondFmtRule
    {
        private int priority = 1;

        protected XlCondFmtRule(XlCondFmtType ruleType)
        {
            this.RuleType = ruleType;
        }

        public XlCondFmtType RuleType { get; private set; }

        public int Priority
        {
            get => 
                this.priority;
            set
            {
                Guard.ArgumentPositive(value, "Priority");
                this.priority = value;
            }
        }

        public bool StopIfTrue { get; set; }
    }
}

