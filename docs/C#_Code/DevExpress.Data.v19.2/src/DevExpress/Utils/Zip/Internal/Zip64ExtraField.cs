namespace DevExpress.Utils.Zip.Internal
{
    using DevExpress.Utils.Zip;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class Zip64ExtraField : ZipExtraField
    {
        public const int HeaderId = 1;

        public override void Apply(InternalZipFile zipFile)
        {
            if (zipFile.CompressedSize == 0xffffffffUL)
            {
                zipFile.CompressedSize = this.CompressedSize;
            }
            if (zipFile.UncompressedSize == 0xffffffffUL)
            {
                zipFile.UncompressedSize = this.UncompressedSize;
            }
        }

        public override void AssignRawData(BinaryReader reader)
        {
            this.UncompressedSize = reader.ReadInt64();
            this.CompressedSize = reader.ReadInt64();
        }

        public override void Write(BinaryWriter writer)
        {
        }

        public override short Id =>
            1;

        public override ExtraFieldType Type =>
            ExtraFieldType.Both;

        public override short ContentSize =>
            30;

        public long UncompressedSize { get; set; }

        public long CompressedSize { get; set; }
    }
}

