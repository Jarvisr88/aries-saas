namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public abstract class XfPropBase
    {
        private const short headerSize = 4;

        protected XfPropBase(short typeCode)
        {
            this.TypeCode = typeCode;
        }

        public int GetSize() => 
            this.GetSizeCore() + 4;

        protected abstract int GetSizeCore();
        public void Write(BinaryWriter writer)
        {
            writer.Write(this.TypeCode);
            short size = (short) this.GetSize();
            writer.Write(size);
            this.WriteCore(writer);
        }

        protected abstract void WriteCore(BinaryWriter writer);

        public short TypeCode { get; private set; }
    }
}

