namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlPtgName : XlPtgWithDataType
    {
        public XlPtgName(int nameIndex, XlPtgDataType dataType) : base(dataType)
        {
            this.NameIndex = nameIndex;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public int NameIndex { get; private set; }

        public override int TypeCode =>
            3;
    }
}

