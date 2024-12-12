namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlPtgErr : XlPtgBase
    {
        public XlPtgErr(XlCellErrorType value)
        {
            this.Value = value;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            0x1c;

        public XlCellErrorType Value { get; set; }
    }
}

