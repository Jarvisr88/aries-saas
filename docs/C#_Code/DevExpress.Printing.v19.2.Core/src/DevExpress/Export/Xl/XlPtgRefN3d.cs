namespace DevExpress.Export.Xl
{
    using System;

    public class XlPtgRefN3d : XlPtgRefN
    {
        private string sheetName;

        public XlPtgRefN3d(XlCellOffset cellOffset, string sheetName) : base(cellOffset)
        {
            this.sheetName = sheetName;
        }

        public XlPtgRefN3d(XlCellOffset cellOffset, string sheetName, XlPtgDataType dataType) : base(cellOffset, dataType)
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

