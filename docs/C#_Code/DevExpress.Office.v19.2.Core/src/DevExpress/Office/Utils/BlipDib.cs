namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public class BlipDib : BlipBase
    {
        private const int widthPosition = 4;
        private const int heightPosition = 8;
        private const int bitCountPosition = 14;
        private const int dwordSize = 0x20;
        private const int headerInfoSize = 0x10;

        public BlipDib(OfficeImage image) : base(image)
        {
        }

        public BlipDib(BinaryReader reader, OfficeArtRecordHeader header) : base(reader, header)
        {
        }

        public override OfficeArtRecordHeader CreateRecordHeader() => 
            new OfficeArtRecordHeader { 
                Version = 0,
                InstanceInfo = 0x6e0,
                TypeCode = 0xf01e,
                Length = 0x11 + base.ImageBytesLength
            };

        public override int GetSize() => 
            0x19 + base.ImageBytesLength;

        protected override void LoadImageFromStream(MemoryStream imageStream)
        {
            DibHeader header = DibHeader.FromStream(new BinaryReader(imageStream));
            int bytesInLine = header.BitCount * header.Width;
            bytesInLine = ((bytesInLine % 0x20) != 0) ? (((bytesInLine / 0x20) + 1) * 4) : (bytesInLine / 8);
            imageStream.Seek(0L, SeekOrigin.Begin);
            base.Image = OfficeImage.CreateImage(DibHelper.CreateDib(imageStream, header.Width, header.Height, bytesInLine));
        }

        protected override void Read(BinaryReader reader, OfficeArtRecordHeader header)
        {
            int num = this.CalcMD4MessageOffset(header.InstanceInfo);
            reader.BaseStream.Seek((long) 0x10, SeekOrigin.Current);
            base.TagValue = reader.ReadByte();
            if (header.InstanceInfo == this.DoubleMD4Message)
            {
                reader.BaseStream.Seek((long) 0x10, SeekOrigin.Current);
            }
            int count = header.Length - num;
            MemoryStream imageStream = new MemoryStream(reader.ReadBytes(count));
            this.LoadImageFromStream(imageStream);
        }

        public override int SingleMD4Message =>
            0x7a8;

        public override int DoubleMD4Message =>
            0x7a9;

        protected internal override OfficeImageFormat Format =>
            OfficeImageFormat.Png;
    }
}

