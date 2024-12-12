namespace DevExpress.Export.Xl
{
    using System;

    public class XlPtgArea : XlPtgWithDataType
    {
        private XlCellPosition topLeft;
        private XlCellPosition bottomRight;

        public XlPtgArea(XlCellRange range) : this(range.TopLeft, range.BottomRight)
        {
        }

        public XlPtgArea(XlCellPosition topLeft, XlCellPosition bottomRight)
        {
            this.topLeft = topLeft;
            this.bottomRight = bottomRight;
        }

        public XlPtgArea(XlCellPosition topLeft, XlCellPosition bottomRight, XlPtgDataType dataType) : base(dataType)
        {
            this.topLeft = topLeft;
            this.bottomRight = bottomRight;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            5;

        public XlCellPosition TopLeft
        {
            get => 
                this.topLeft;
            set => 
                this.topLeft = value;
        }

        public XlCellPosition BottomRight
        {
            get => 
                this.bottomRight;
            set => 
                this.bottomRight = value;
        }

        public XlCellRange CellRange =>
            new XlCellRange(this.topLeft, this.bottomRight);
    }
}

