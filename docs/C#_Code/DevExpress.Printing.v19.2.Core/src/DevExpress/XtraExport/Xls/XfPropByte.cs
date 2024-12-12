namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XfPropByte : XfPropBase
    {
        public XfPropByte(short typeCode, byte value) : base(typeCode)
        {
            this.Value = value;
        }

        protected override int GetSizeCore() => 
            1;

        protected override void WriteCore(BinaryWriter writer)
        {
            writer.Write(this.Value);
        }

        public byte Value { get; private set; }
    }
}

