namespace DevExpress.Export.Xl
{
    using System;

    public abstract class XlPtgBase
    {
        private XlPtgDataType dataType;

        protected XlPtgBase()
        {
        }

        protected internal int GetPtgCode() => 
            (ushort) (((ushort) (this.TypeCode & 0x7f1f)) | ((ushort) ((((ushort) this.DataType) & 3) << 5)));

        protected virtual XlPtgDataType GetPtgDataType(XlPtgDataType ptgDataType) => 
            XlPtgDataType.None;

        public abstract void Visit(IXlPtgVisitor visitor);

        public abstract int TypeCode { get; }

        public XlPtgDataType DataType
        {
            get => 
                this.GetPtgDataType(this.dataType);
            set => 
                this.dataType = value;
        }
    }
}

