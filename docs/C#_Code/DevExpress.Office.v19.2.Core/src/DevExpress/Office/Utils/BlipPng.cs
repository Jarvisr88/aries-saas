namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public class BlipPng : BlipBase
    {
        public BlipPng(OfficeImage image) : base(image)
        {
        }

        public BlipPng(BinaryReader reader, OfficeArtRecordHeader header) : base(reader, header)
        {
        }

        public override OfficeArtRecordHeader CreateRecordHeader() => 
            new OfficeArtRecordHeader { 
                Version = 0,
                InstanceInfo = 0x6e0,
                TypeCode = 0xf01e,
                Length = 0x11 + base.ImageBytesLength
            };

        public override int SingleMD4Message =>
            0x6e0;

        public override int DoubleMD4Message =>
            0x6e1;

        protected internal override OfficeImageFormat Format =>
            OfficeImageFormat.Png;
    }
}

