namespace DevExpress.Export.Xl
{
    using System;

    public class XlPtgRefErr : XlPtgWithDataType
    {
        public XlPtgRefErr()
        {
        }

        public XlPtgRefErr(XlPtgDataType dataType) : base(dataType)
        {
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            10;
    }
}

