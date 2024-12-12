namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public class BlipPict : BlipMetaFile
    {
        public BlipPict(BinaryReader reader, OfficeArtRecordHeader header) : base(reader, header)
        {
        }

        public override OfficeArtRecordHeader CreateRecordHeader() => 
            new OfficeArtRecordHeader { 
                Version = 0,
                InstanceInfo = 0x542,
                TypeCode = 0xf01c,
                Length = 50 + base.ImageBytesLength
            };

        protected override void Read(BinaryReader reader, OfficeArtRecordHeader header)
        {
            int num = this.CalcMD4MessageOffset(header.InstanceInfo);
            reader.BaseStream.Seek((long) num, SeekOrigin.Current);
            DocMetafileHeader header2 = DocMetafileHeader.FromStream(reader);
            if (header2.Compressed)
            {
                reader.BaseStream.Seek((long) header2.CompressedSize, SeekOrigin.Current);
            }
            else
            {
                reader.BaseStream.Seek((long) header2.UncompressedSize, SeekOrigin.Current);
            }
        }

        public override int SingleMD4Message =>
            0x542;

        public override int DoubleMD4Message =>
            0x543;
    }
}

