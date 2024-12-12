namespace DevExpress.Export.Xl
{
    using System;

    public class XlPtgAreaErr : XlPtgWithDataType
    {
        public XlPtgAreaErr()
        {
        }

        public XlPtgAreaErr(XlPtgDataType dataType) : base(dataType)
        {
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            11;
    }
}

