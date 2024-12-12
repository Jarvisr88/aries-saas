namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XfPropInt32 : XfPropBase
    {
        public XfPropInt32(short typeCode, int value) : base(typeCode)
        {
            this.Value = value;
        }

        protected override int GetSizeCore() => 
            4;

        protected override void WriteCore(BinaryWriter writer)
        {
            writer.Write(this.Value);
        }

        public int Value { get; private set; }
    }
}

