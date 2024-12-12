namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public class DocMetafileHeader
    {
        private const int emusPerHundredthsOfMillimeter = 360;
        private const byte deflateCode = 0;
        private const byte uncompressedCode = 0xfe;
        private const byte filterCode = 0xfe;
        private int uncompressedSize;
        private int left;
        private int top;
        private int right;
        private int bottom;
        private int widthInEmus;
        private int heightInEmus;
        private int compressedSize;
        private bool compressed;

        public static DocMetafileHeader FromStream(BinaryReader reader)
        {
            DocMetafileHeader header = new DocMetafileHeader();
            header.Read(reader);
            return header;
        }

        protected void Read(BinaryReader reader)
        {
            this.uncompressedSize = reader.ReadInt32();
            this.left = reader.ReadInt32();
            this.top = reader.ReadInt32();
            this.right = reader.ReadInt32();
            this.bottom = reader.ReadInt32();
            this.widthInEmus = reader.ReadInt32();
            this.heightInEmus = reader.ReadInt32();
            this.compressedSize = reader.ReadInt32();
            if (reader.ReadByte() == 0)
            {
                this.compressed = true;
            }
            if (reader.ReadByte() != 0xfe)
            {
                OfficeArtExceptions.ThrowInvalidContent();
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(this.uncompressedSize);
            writer.Write(this.left);
            writer.Write(this.top);
            writer.Write(this.right);
            writer.Write(this.bottom);
            writer.Write(this.widthInEmus);
            writer.Write(this.heightInEmus);
            writer.Write(this.compressedSize);
            writer.Write(this.compressed ? ((byte) 0) : ((byte) 0xfe));
            writer.Write((byte) 0xfe);
        }

        public int UncompressedSize
        {
            get => 
                this.uncompressedSize;
            protected internal set => 
                this.uncompressedSize = value;
        }

        public int Left
        {
            get => 
                this.left;
            protected internal set => 
                this.left = value;
        }

        public int Top
        {
            get => 
                this.top;
            protected internal set => 
                this.top = value;
        }

        public int Right
        {
            get => 
                this.right;
            protected internal set => 
                this.right = value;
        }

        public int Bottom
        {
            get => 
                this.bottom;
            protected internal set => 
                this.bottom = value;
        }

        public int WidthInEmus
        {
            get => 
                this.widthInEmus;
            protected internal set => 
                this.widthInEmus = value;
        }

        public int WidthInHundredthsOfMillimeter =>
            this.widthInEmus / 360;

        public int HeightInEmus
        {
            get => 
                this.heightInEmus;
            protected internal set => 
                this.heightInEmus = value;
        }

        public int HeightInHundredthsOfMillimeter =>
            this.heightInEmus / 360;

        public int CompressedSize
        {
            get => 
                this.compressedSize;
            protected internal set => 
                this.compressedSize = value;
        }

        public bool Compressed
        {
            get => 
                this.compressed;
            protected internal set => 
                this.compressed = value;
        }
    }
}

