namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlPtgRefN : XlPtgWithDataType
    {
        public XlPtgRefN(XlCellOffset cellOffset)
        {
            this.CellOffset = cellOffset;
        }

        public XlPtgRefN(XlCellOffset cellOffset, XlPtgDataType dataType) : base(dataType)
        {
            this.CellOffset = cellOffset;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            12;

        public XlCellOffset CellOffset { get; set; }
    }
}

