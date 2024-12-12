namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public class DibHeader
    {
        public const int DibHeaderSize = 40;
        private const int dibPlanes = 1;
        private int headerSize = 40;
        private int width;
        private int height;
        private short planes = 1;
        private short bitCount;
        private int compression;
        private int imageSize;
        private int horizontalPixelsPerMeter;
        private int verticalPixelsPerMeter;
        private int usedColors;
        private int importantColors;

        public static DibHeader FromStream(BinaryReader reader)
        {
            DibHeader header = new DibHeader();
            header.Read(reader);
            return header;
        }

        protected internal void Read(BinaryReader reader)
        {
            this.headerSize = reader.ReadInt32();
            if (this.headerSize != 40)
            {
                OfficeArtExceptions.ThrowInvalidContent();
            }
            this.width = reader.ReadInt32();
            this.height = reader.ReadInt32();
            this.planes = reader.ReadInt16();
            this.bitCount = reader.ReadInt16();
            this.compression = reader.ReadInt32();
            this.imageSize = reader.ReadInt32();
            this.horizontalPixelsPerMeter = reader.ReadInt32();
            this.verticalPixelsPerMeter = reader.ReadInt32();
            this.usedColors = reader.ReadInt32();
            this.importantColors = reader.ReadInt32();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(this.headerSize);
            writer.Write(this.width);
            writer.Write(this.height);
            writer.Write(this.planes);
            writer.Write(this.bitCount);
            writer.Write(this.compression);
            writer.Write(this.imageSize);
            writer.Write(this.horizontalPixelsPerMeter);
            writer.Write(this.verticalPixelsPerMeter);
            writer.Write(this.usedColors);
            writer.Write(this.importantColors);
        }

        protected internal int HeaderSize =>
            this.headerSize;

        public int Width
        {
            get => 
                this.width;
            set => 
                this.width = value;
        }

        public int Height
        {
            get => 
                this.height;
            set => 
                this.height = value;
        }

        protected internal short Planes =>
            this.planes;

        public short BitCount
        {
            get => 
                this.bitCount;
            set => 
                this.bitCount = value;
        }

        protected internal int Compression
        {
            get => 
                this.compression;
            set => 
                this.compression = value;
        }

        protected internal int ImageSize
        {
            get => 
                this.imageSize;
            set => 
                this.imageSize = value;
        }

        protected internal int HorizontalPixelsPerMeter
        {
            get => 
                this.horizontalPixelsPerMeter;
            set => 
                this.horizontalPixelsPerMeter = value;
        }

        protected internal int VerticalPixelsPerMeter
        {
            get => 
                this.verticalPixelsPerMeter;
            set => 
                this.verticalPixelsPerMeter = value;
        }

        protected internal int UsedColors
        {
            get => 
                this.usedColors;
            set => 
                this.usedColors = value;
        }

        protected internal int ImportantColors
        {
            get => 
                this.importantColors;
            set => 
                this.importantColors = value;
        }
    }
}

