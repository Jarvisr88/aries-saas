namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlPtgBool : XlPtgBase
    {
        public XlPtgBool(bool value)
        {
            this.Value = value;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            0x1d;

        public bool Value { get; set; }
    }
}

