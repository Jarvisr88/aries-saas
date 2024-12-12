namespace DevExpress.Utils.Zip.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    public class CentralDirectoryEntry
    {
        public int Crc32 { get; set; }

        public int CompressedSize { get; set; }

        public int UncompressedSize { get; set; }

        public string FileName { get; set; }

        public int MsDosDateTime { get; set; }

        public int RelativeOffset { get; set; }

        public int FileAttributes { get; set; }

        public string Comment { get; set; }

        public IZipExtraFieldCollection ExtraFields { get; set; }

        public short GeneralPurposeFlag { get; set; }

        public DevExpress.Utils.Zip.Internal.CompressionMethod CompressionMethod { get; set; }
    }
}

