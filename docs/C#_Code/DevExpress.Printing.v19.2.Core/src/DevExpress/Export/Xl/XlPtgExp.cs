namespace DevExpress.Export.Xl
{
    using System;

    public class XlPtgExp : XlPtgBase
    {
        private XlCellPosition cellPosition;

        public XlPtgExp(XlCellPosition cellPosition)
        {
            this.cellPosition = cellPosition;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            1;

        public XlCellPosition CellPosition
        {
            get => 
                this.cellPosition;
            set => 
                this.cellPosition = value;
        }
    }
}

