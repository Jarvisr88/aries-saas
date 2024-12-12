namespace DevExpress.Export.Xl
{
    using System;

    public class XlPtgAreaN3d : XlPtgAreaN
    {
        private string sheetName;

        public XlPtgAreaN3d(XlCellOffset topLeft, XlCellOffset bottomRight, string sheetName) : base(topLeft, bottomRight)
        {
            this.sheetName = sheetName;
        }

        public XlPtgAreaN3d(XlCellOffset topLeft, XlCellOffset bottomRight, string sheetName, XlPtgDataType dataType) : base(topLeft, bottomRight, dataType)
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

