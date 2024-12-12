namespace DevExpress.Export.Xl
{
    using System;

    public class XlPtgFuncVar : XlPtgFunc
    {
        private int paramCount;

        public XlPtgFuncVar(int funcCode, int paramCount) : this(funcCode, paramCount, XlPtgDataType.Reference)
        {
        }

        public XlPtgFuncVar(int funcCode, int paramCount, XlPtgDataType dataType) : base(funcCode, dataType)
        {
            this.paramCount = paramCount;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            2;

        public int ParamCount
        {
            get => 
                this.paramCount;
            set
            {
                if ((value < 0) || (value > 0xff))
                {
                    throw new ArgumentOutOfRangeException($"ParamCount {value} out of range 0..{(byte) 0xff}.");
                }
                this.paramCount = value;
            }
        }
    }
}

