namespace DevExpress.Export.Xl
{
    using System;

    public class XlPtgRef3d : XlPtgRef
    {
        private string sheetName;

        public XlPtgRef3d(XlCellPosition cellPosition, string sheetName) : base(cellPosition)
        {
            this.sheetName = sheetName;
        }

        public XlPtgRef3d(XlCellPosition cellPosition, string sheetName, XlPtgDataType dataType) : base(cellPosition, dataType)
        {
            this.sheetName = sheetName;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            0x1a;

        public string SheetName
        {
            get => 
                this.sheetName;
            set => 
                this.sheetName = value;
        }
    }
}

