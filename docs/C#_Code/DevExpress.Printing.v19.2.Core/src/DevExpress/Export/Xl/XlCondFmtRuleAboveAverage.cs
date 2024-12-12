namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlCondFmtRuleAboveAverage : XlCondFmtRuleWithFormatting
    {
        private int stdDev;

        public XlCondFmtRuleAboveAverage() : base(XlCondFmtType.AboveOrBelowAverage)
        {
            this.Condition = XlCondFmtAverageCondition.Above;
        }

        public XlCondFmtAverageCondition Condition { get; set; }

        public int StdDev
        {
            get => 
                this.stdDev;
            set
            {
                if ((value < 0) || (value > 3))
                {
                    throw new ArgumentOutOfRangeException("StdDev out of range 0...3!");
                }
                this.stdDev = value;
            }
        }

        protected internal bool AboveAverage =>
            (this.Condition == XlCondFmtAverageCondition.Above) || (this.Condition == XlCondFmtAverageCondition.AboveOrEqual);

        protected internal bool EqualAverage =>
            (this.Condition == XlCondFmtAverageCondition.AboveOrEqual) || (this.Condition == XlCondFmtAverageCondition.BelowOrEqual);
    }
}

