namespace DevExpress.Export.Xl
{
    using System;

    public class XlPtgMemErr : XlPtgMemBase
    {
        private byte errorValue;

        public XlPtgMemErr(int innerPtgCount) : base(innerPtgCount)
        {
        }

        public XlPtgMemErr(int innerPtgCount, XlPtgDataType dataType) : base(innerPtgCount, dataType)
        {
        }

        public XlPtgMemErr(int innerPtgCount, XlPtgDataType dataType, byte error) : base(innerPtgCount, dataType)
        {
            this.errorValue = error;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public byte ErrorValue
        {
            get => 
                this.errorValue;
            set => 
                this.errorValue = value;
        }

        public override int TypeCode =>
            7;
    }
}

