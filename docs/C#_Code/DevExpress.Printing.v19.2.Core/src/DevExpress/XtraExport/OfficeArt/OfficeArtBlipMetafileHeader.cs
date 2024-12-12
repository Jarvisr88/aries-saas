namespace DevExpress.XtraExport.OfficeArt
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    internal class OfficeArtBlipMetafileHeader
    {
        public void Write(BinaryWriter writer)
        {
            writer.Write(this.UncompressedSize);
            writer.Write(this.Left);
            writer.Write(this.Top);
            writer.Write(this.Right);
            writer.Write(this.Bottom);
            writer.Write(this.WidthInEmus);
            writer.Write(this.HeightInEmus);
            writer.Write(this.CompressedSize);
            writer.Write(this.Compressed ? ((byte) 0) : ((byte) 0xfe));
            writer.Write((byte) 0xfe);
        }

        public int UncompressedSize { get; set; }

        public int Left { get; set; }

        public int Top { get; set; }

        public int Right { get; set; }

        public int Bottom { get; set; }

        public int WidthInEmus { get; set; }

        public int HeightInEmus { get; set; }

        public int CompressedSize { get; set; }

        public bool Compressed { get; set; }
    }
}

