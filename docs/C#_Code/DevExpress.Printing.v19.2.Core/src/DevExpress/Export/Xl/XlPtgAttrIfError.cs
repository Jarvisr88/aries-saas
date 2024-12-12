namespace DevExpress.Export.Xl
{
    using System;

    public class XlPtgAttrIfError : XlPtgBase
    {
        private int offset;

        public XlPtgAttrIfError(int offset)
        {
            this.offset = offset;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            0x8019;

        public int Offset
        {
            get => 
                this.offset;
            set => 
                this.offset = value;
        }
    }
}

