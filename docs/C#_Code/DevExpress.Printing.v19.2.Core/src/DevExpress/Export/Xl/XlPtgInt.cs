namespace DevExpress.Export.Xl
{
    using System;

    public class XlPtgInt : XlPtgBase
    {
        private int value;

        public XlPtgInt(int value)
        {
            this.value = value;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            30;

        public int Value
        {
            get => 
                this.value;
            set
            {
                if ((value < 0) || (value > 0xffff))
                {
                    throw new ArgumentOutOfRangeException($"Value {value} out of range 0..{(ushort) 0xffff}.");
                }
                this.value = value;
            }
        }
    }
}

