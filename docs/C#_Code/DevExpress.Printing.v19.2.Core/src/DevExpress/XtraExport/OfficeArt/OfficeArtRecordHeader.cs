namespace DevExpress.XtraExport.OfficeArt
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    internal class OfficeArtRecordHeader
    {
        public const int Size = 8;

        public void Write(BinaryWriter writer)
        {
            ushort num = (ushort) (this.Version | (this.InstanceInfo << 4));
            writer.Write(num);
            writer.Write((ushort) this.TypeCode);
            writer.Write(this.Length);
        }

        public int Version { get; set; }

        public int InstanceInfo { get; set; }

        public int TypeCode { get; set; }

        public int Length { get; set; }
    }
}

