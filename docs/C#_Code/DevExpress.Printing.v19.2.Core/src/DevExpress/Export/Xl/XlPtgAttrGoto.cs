namespace DevExpress.Export.Xl
{
    using System;

    public class XlPtgAttrGoto : XlPtgBase
    {
        private int offset;

        public XlPtgAttrGoto(int offset)
        {
            this.offset = offset;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            0x819;

        public int Offset
        {
            get => 
                this.offset;
            set => 
                this.offset = value;
        }
    }
}

