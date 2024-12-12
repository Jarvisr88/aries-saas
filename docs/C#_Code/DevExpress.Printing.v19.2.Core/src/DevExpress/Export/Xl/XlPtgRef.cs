namespace DevExpress.Export.Xl
{
    using System;

    public class XlPtgRef : XlPtgWithDataType
    {
        private XlCellPosition cellPosition;

        public XlPtgRef(XlCellPosition cellPosition)
        {
            this.cellPosition = cellPosition;
        }

        public XlPtgRef(XlCellPosition cellPosition, XlPtgDataType dataType) : base(dataType)
        {
            this.cellPosition = cellPosition;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            4;

        public XlCellPosition CellPosition
        {
            get => 
                this.cellPosition;
            set => 
                this.cellPosition = value;
        }
    }
}

