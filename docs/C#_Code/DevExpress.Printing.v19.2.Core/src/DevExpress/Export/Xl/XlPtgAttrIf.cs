namespace DevExpress.Export.Xl
{
    using System;

    public class XlPtgAttrIf : XlPtgBase
    {
        private int offset;

        public XlPtgAttrIf(int offset)
        {
            this.offset = offset;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            0x219;

        public int Offset
        {
            get => 
                this.offset;
            set => 
                this.offset = value;
        }
    }
}

