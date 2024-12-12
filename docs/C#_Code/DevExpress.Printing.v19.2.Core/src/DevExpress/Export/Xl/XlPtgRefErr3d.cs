namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlPtgRefErr3d : XlPtgWithDataType
    {
        public XlPtgRefErr3d(string sheetName)
        {
            this.SheetName = sheetName;
        }

        public XlPtgRefErr3d(string sheetName, XlPtgDataType dataType) : base(dataType)
        {
            this.SheetName = sheetName;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            0x1c;

        public string SheetName { get; set; }
    }
}

