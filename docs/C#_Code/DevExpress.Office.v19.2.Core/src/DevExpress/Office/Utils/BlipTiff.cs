namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public class BlipTiff : BlipBase
    {
        public BlipTiff(OfficeImage image) : base(image)
        {
        }

        public BlipTiff(BinaryReader reader, OfficeArtRecordHeader header) : base(reader, header)
        {
        }

        public override OfficeArtRecordHeader CreateRecordHeader() => 
            new OfficeArtRecordHeader { 
                Version = 0,
                InstanceInfo = 0x6e4,
                TypeCode = 0xf029,
                Length = 0x11 + base.ImageBytesLength
            };

        public override int SingleMD4Message =>
            0x6e4;

        public override int DoubleMD4Message =>
            0x6e5;
    }
}

