namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XfPropUInt16 : XfPropBase
    {
        public XfPropUInt16(short typeCode, int value) : base(typeCode)
        {
            this.Value = value;
        }

        protected override int GetSizeCore() => 
            2;

        protected override void WriteCore(BinaryWriter writer)
        {
            writer.Write((ushort) this.Value);
        }

        public int Value { get; private set; }
    }
}

