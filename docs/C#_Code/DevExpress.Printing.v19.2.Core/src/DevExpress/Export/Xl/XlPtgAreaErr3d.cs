namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlPtgAreaErr3d : XlPtgWithDataType
    {
        public XlPtgAreaErr3d(string sheetName)
        {
            this.SheetName = sheetName;
        }

        public XlPtgAreaErr3d(string sheetName, XlPtgDataType dataType) : base(dataType)
        {
            this.SheetName = sheetName;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            0x1d;

        public string SheetName { get; set; }
    }
}

