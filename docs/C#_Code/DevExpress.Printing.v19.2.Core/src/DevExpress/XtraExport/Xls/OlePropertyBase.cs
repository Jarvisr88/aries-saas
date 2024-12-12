namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    public abstract class OlePropertyBase
    {
        protected OlePropertyBase(int propertyId, int propertyType)
        {
            this.PropertyId = propertyId;
            this.PropertyType = propertyType;
        }

        public abstract void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet);
        public abstract int GetSize(OlePropertySetBase propertySet);
        protected bool IsSingleByteEncoding(Encoding encoding) => 
            DXEncoding.GetEncodingCodePage(encoding) != 0x4b0;

        public abstract void Read(BinaryReader reader, OlePropertySetBase propertySet);
        public abstract void Write(BinaryWriter writer, OlePropertySetBase propertySet);
        protected void WritePadding(BinaryWriter writer, int paddingCount)
        {
            for (int i = 0; i < paddingCount; i++)
            {
                writer.Write((byte) 0);
            }
        }

        public int PropertyId { get; private set; }

        public int PropertyType { get; private set; }
    }
}

