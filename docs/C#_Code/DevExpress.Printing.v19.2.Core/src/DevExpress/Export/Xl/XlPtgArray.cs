namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections.Generic;

    public class XlPtgArray : XlPtgWithDataType
    {
        private readonly int width;
        private readonly int height;
        private readonly IList<XlVariantValue> values;

        public XlPtgArray(int width, int height, IList<XlVariantValue> values, XlPtgDataType dataType) : base(dataType)
        {
            this.width = width;
            this.height = height;
            this.values = values;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public int Width =>
            this.width;

        public int Height =>
            this.height;

        public IList<XlVariantValue> Values =>
            this.values;

        public override int TypeCode =>
            0x20;
    }
}

