namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public class BlipEmf : BlipMetaFile
    {
        public BlipEmf(OfficeImage image) : base(image)
        {
        }

        public BlipEmf(BinaryReader reader, OfficeArtRecordHeader header) : base(reader, header)
        {
        }

        public override OfficeArtRecordHeader CreateRecordHeader() => 
            new OfficeArtRecordHeader { 
                Version = 0,
                InstanceInfo = 980,
                TypeCode = 0xf01a,
                Length = 50 + base.ImageBytesLength
            };

        public override int SingleMD4Message =>
            980;

        public override int DoubleMD4Message =>
            0x3d5;
    }
}

