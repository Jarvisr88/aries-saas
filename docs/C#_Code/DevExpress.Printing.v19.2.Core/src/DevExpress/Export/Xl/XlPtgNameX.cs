namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlPtgNameX : XlPtgWithDataType
    {
        public XlPtgNameX(string name, XlPtgDataType dataType) : base(dataType)
        {
            this.Name = name;
            this.XtiIndex = -1;
            this.NameIndex = -1;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public string Name { get; private set; }

        public int XtiIndex { get; internal set; }

        public int NameIndex { get; internal set; }

        public override int TypeCode =>
            0x19;
    }
}

