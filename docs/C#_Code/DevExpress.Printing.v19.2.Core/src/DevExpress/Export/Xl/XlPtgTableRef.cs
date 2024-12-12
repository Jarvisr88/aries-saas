namespace DevExpress.Export.Xl
{
    using System;

    public class XlPtgTableRef : XlPtgWithDataType
    {
        private XlTableReference tableReference;

        public XlPtgTableRef(XlTableReference tableReference)
        {
            this.tableReference = tableReference;
        }

        public XlPtgTableRef(XlTableReference tableReference, XlPtgDataType dataType) : base(dataType)
        {
            this.tableReference = tableReference;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            0;

        public XlTableReference TableReference =>
            this.tableReference;
    }
}

