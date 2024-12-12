namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections.Generic;

    public class XlPtgAttrChoose : XlPtgBase
    {
        private IList<int> offsets;

        public XlPtgAttrChoose(List<int> offsets)
        {
            this.offsets = offsets;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IList<int> Offsets
        {
            get => 
                this.offsets;
            set => 
                this.offsets = value;
        }

        public override int TypeCode =>
            0x419;
    }
}

