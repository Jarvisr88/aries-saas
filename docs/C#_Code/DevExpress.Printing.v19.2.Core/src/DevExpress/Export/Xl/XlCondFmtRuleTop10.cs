namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlCondFmtRuleTop10 : XlCondFmtRuleWithFormatting
    {
        private bool percent;
        private int rank;

        public XlCondFmtRuleTop10() : base(XlCondFmtType.Top10)
        {
            this.rank = 10;
        }

        public bool Bottom { get; set; }

        public bool Percent
        {
            get => 
                this.percent;
            set
            {
                this.percent = value;
                if (this.percent && (this.rank > 100))
                {
                    this.rank = 100;
                }
            }
        }

        public int Rank
        {
            get => 
                this.rank;
            set
            {
                int num = this.percent ? 100 : 0x3e8;
                if ((value < 1) || (value > num))
                {
                    throw new ArgumentOutOfRangeException($"Rank out of range 1..{num}");
                }
                this.rank = value;
            }
        }
    }
}

