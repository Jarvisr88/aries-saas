namespace DevExpress.Export.Xl
{
    using System;

    public class XlPtgMemFunc : XlPtgMemBase
    {
        public XlPtgMemFunc(int innerPtgCount) : base(innerPtgCount)
        {
        }

        public XlPtgMemFunc(int innerPtgCount, XlPtgDataType dataType) : base(innerPtgCount, dataType)
        {
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            9;
    }
}

