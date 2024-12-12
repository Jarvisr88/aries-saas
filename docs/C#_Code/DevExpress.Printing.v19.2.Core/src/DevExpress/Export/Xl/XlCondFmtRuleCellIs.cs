namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlCondFmtRuleCellIs : XlCondFmtRuleWithFormatting
    {
        private XlValueObject value;
        private XlValueObject secondValue;

        public XlCondFmtRuleCellIs() : base(XlCondFmtType.CellIs)
        {
            this.value = XlValueObject.Empty;
            this.secondValue = XlValueObject.Empty;
        }

        public XlCondFmtOperator Operator { get; set; }

        public XlValueObject Value
        {
            get => 
                this.value;
            set
            {
                if (value == null)
                {
                    this.value = XlValueObject.Empty;
                }
                else
                {
                    this.value = value;
                }
            }
        }

        public XlValueObject SecondValue
        {
            get => 
                this.secondValue;
            set
            {
                if (value == null)
                {
                    this.secondValue = XlValueObject.Empty;
                }
                else
                {
                    this.secondValue = value;
                }
            }
        }
    }
}

