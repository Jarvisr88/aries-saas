namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public class BlipWmf : BlipMetaFile
    {
        public BlipWmf(OfficeImage image) : base(image)
        {
        }

        public BlipWmf(BinaryReader reader, OfficeArtRecordHeader header) : base(reader, header)
        {
        }

        public override OfficeArtRecordHeader CreateRecordHeader() => 
            new OfficeArtRecordHeader { 
                Version = 0,
                InstanceInfo = 0x216,
                TypeCode = 0xf01b,
                Length = 50 + base.ImageBytesLength
            };

        public override int SingleMD4Message =>
            0x216;

        public override int DoubleMD4Message =>
            0x217;
    }
}

