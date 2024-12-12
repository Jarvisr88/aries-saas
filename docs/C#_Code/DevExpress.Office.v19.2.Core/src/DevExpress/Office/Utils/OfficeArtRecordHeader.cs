namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public class OfficeArtRecordHeader
    {
        public const int Size = 8;
        private int version;
        private int instanceInfo;
        private int typeCode;
        private int length;

        public static OfficeArtRecordHeader FromStream(BinaryReader reader)
        {
            OfficeArtRecordHeader header = new OfficeArtRecordHeader();
            header.Read(reader);
            return header;
        }

        protected internal void Read(BinaryReader reader)
        {
            ushort num = reader.ReadUInt16();
            this.version = num & 15;
            this.instanceInfo = (num & 0xfff0) >> 4;
            this.typeCode = reader.ReadUInt16();
            this.length = reader.ReadInt32();
        }

        public void Write(BinaryWriter writer)
        {
            ushort num = (ushort) (this.version | (this.instanceInfo << 4));
            writer.Write(num);
            writer.Write((ushort) this.typeCode);
            writer.Write(this.length);
        }

        public int Version
        {
            get => 
                this.version;
            protected internal set => 
                this.version = value;
        }

        public int InstanceInfo
        {
            get => 
                this.instanceInfo;
            protected internal set => 
                this.instanceInfo = value;
        }

        public int TypeCode
        {
            get => 
                this.typeCode;
            protected internal set => 
                this.typeCode = value;
        }

        public int Length
        {
            get => 
                this.length;
            protected internal set => 
                this.length = value;
        }
    }
}

