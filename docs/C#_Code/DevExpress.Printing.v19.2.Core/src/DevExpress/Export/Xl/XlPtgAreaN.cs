namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlPtgAreaN : XlPtgWithDataType
    {
        public XlPtgAreaN(XlCellOffset topLeft, XlCellOffset bottomRight)
        {
            this.TopLeft = topLeft;
            this.BottomRight = bottomRight;
        }

        public XlPtgAreaN(XlCellOffset topLeft, XlCellOffset bottomRight, XlPtgDataType dataType) : base(dataType)
        {
            this.TopLeft = topLeft;
            this.BottomRight = bottomRight;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            13;

        public XlCellOffset TopLeft { get; set; }

        public XlCellOffset BottomRight { get; set; }
    }
}

