namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections.Generic;

    public class XlPtgMemArea : XlPtgMemBase
    {
        private List<XlCellRange> ranges;

        public XlPtgMemArea(int innerPtgCount) : base(innerPtgCount)
        {
            this.ranges = new List<XlCellRange>();
        }

        public XlPtgMemArea(int innerPtgCount, XlPtgDataType dataType) : base(innerPtgCount, dataType)
        {
            this.ranges = new List<XlCellRange>();
        }

        public XlPtgMemArea(int innerPtgCount, List<XlCellRange> ranges, XlPtgDataType dataType) : base(innerPtgCount, dataType)
        {
            this.ranges = new List<XlCellRange>();
            this.ranges = ranges;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public List<XlCellRange> Ranges =>
            this.ranges;

        public override int TypeCode =>
            6;
    }
}

