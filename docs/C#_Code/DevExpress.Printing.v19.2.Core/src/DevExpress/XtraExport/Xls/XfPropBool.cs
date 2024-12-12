namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XfPropBool : XfPropBase
    {
        public XfPropBool(short typeCode, bool value) : base(typeCode)
        {
            this.Value = value;
        }

        protected override int GetSizeCore() => 
            1;

        protected override void WriteCore(BinaryWriter writer)
        {
            writer.Write(Convert.ToByte(this.Value));
        }

        public bool Value { get; private set; }
    }
}

