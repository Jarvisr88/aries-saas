namespace DevExpress.Export.Xl
{
    using System;

    public class XlPtgArea3d : XlPtgArea
    {
        private string sheetName;

        public XlPtgArea3d(XlCellRange range, string sheetName) : base(range)
        {
            this.sheetName = sheetName;
        }

        public XlPtgArea3d(XlCellPosition topLeft, XlCellPosition bottomRight, string sheetName) : base(topLeft, bottomRight)
        {
            this.sheetName = sheetName;
        }

        public XlPtgArea3d(XlCellPosition topLeft, XlCellPosition bottomRight, string sheetName, XlPtgDataType dataType) : base(topLeft, bottomRight, dataType)
        {
            this.sheetName = sheetName;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            0x1b;

        public string SheetName
        {
            get => 
                this.sheetName;
            set => 
                this.sheetName = value;
        }
    }
}

