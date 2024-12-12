namespace DevExpress.Office.Export.Binary
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    internal class PatternBlip : BlipDib
    {
        private byte[] patternData;

        public PatternBlip(OfficeImage image) : base(image)
        {
        }

        public PatternBlip(BinaryReader reader, OfficeArtRecordHeader header) : base(reader, header)
        {
        }

        protected override Stream CreateImageBytesStream() => 
            new MemoryStream(this.PatternData);

        public override OfficeArtRecordHeader CreateRecordHeader() => 
            new PatternArtRecordHeader(0, 0x7a8, 0xf01f, 0x11 + base.ImageBytesLength);

        public void SetData(byte[] patternData, byte tag)
        {
            MemoryStream input = new MemoryStream(patternData);
            DibHeader header = DibHeader.FromStream(new BinaryReader(input));
            int bytesInLine = header.BitCount * header.Width;
            bytesInLine = ((bytesInLine % 0x20) != 0) ? (((bytesInLine / 0x20) + 1) * 4) : (bytesInLine / 8);
            input.Seek(0L, SeekOrigin.Begin);
            base.Image = OfficeImage.CreateImage(DibHelper.CreateDib(input, header.Width, header.Height, bytesInLine));
            this.patternData = patternData;
            base.TagValue = tag;
        }

        private byte[] PatternData =>
            this.patternData ?? base.Image.GetImageBytes(OfficeImageFormat.Bmp);

        private class PatternArtRecordHeader : OfficeArtRecordHeader
        {
            public PatternArtRecordHeader(int version, int instanceInfo, int typeCode, int length)
            {
                base.Version = version;
                base.InstanceInfo = instanceInfo;
                base.TypeCode = typeCode;
                base.Length = length;
            }
        }
    }
}

