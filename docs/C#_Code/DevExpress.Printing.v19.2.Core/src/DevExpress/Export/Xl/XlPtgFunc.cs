namespace DevExpress.Export.Xl
{
    using System;

    public class XlPtgFunc : XlPtgWithDataType
    {
        private int funcCode;

        public XlPtgFunc(int funcCode) : this(funcCode, XlPtgDataType.Reference)
        {
        }

        public XlPtgFunc(int funcCode, XlPtgDataType dataType) : base(dataType)
        {
            this.funcCode = funcCode;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            1;

        public int FuncCode
        {
            get => 
                this.funcCode;
            set
            {
                if ((value < 0) || (value > 0xffff))
                {
                    throw new ArgumentOutOfRangeException($"FuncCode {value} out of range 0..{(ushort) 0xffff}.");
                }
                this.funcCode = value;
            }
        }
    }
}

