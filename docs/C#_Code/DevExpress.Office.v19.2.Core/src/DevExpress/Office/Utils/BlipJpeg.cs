namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public class BlipJpeg : BlipBase
    {
        public BlipJpeg(OfficeImage image) : base(image)
        {
        }

        public BlipJpeg(BinaryReader reader, OfficeArtRecordHeader header) : base(reader, header)
        {
        }

        protected internal override int CalcMD4MessageOffset(int uidInfo)
        {
            if ((uidInfo == 0x46a) || (uidInfo == 0x6e2))
            {
                return 0x11;
            }
            if ((uidInfo == 0x46b) || (uidInfo == 0x6e3))
            {
                return 0x21;
            }
            OfficeArtExceptions.ThrowInvalidContent();
            return 0;
        }

        public override OfficeArtRecordHeader CreateRecordHeader() => 
            new OfficeArtRecordHeader { 
                Version = 0,
                InstanceInfo = 0x46a,
                TypeCode = 0xf01d,
                Length = 0x11 + base.ImageBytesLength
            };

        public override int SingleMD4Message =>
            0x46a;

        public override int DoubleMD4Message =>
            0x46b;
    }
}

