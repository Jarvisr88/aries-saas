namespace DevExpress.Export.Xl
{
    using DevExpress.XtraExport.Xls;
    using System;

    public class XlPtgNum : XlPtgBase
    {
        private double value;

        public XlPtgNum(double value)
        {
            this.value = value;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            0x1f;

        public double Value
        {
            get => 
                this.value;
            set
            {
                XNumChecker.CheckValue(value);
                this.value = value;
            }
        }
    }
}

