namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public class OfficeArtFileDrawingGroupRecord : OfficeDrawingPartBase
    {
        private const int mainDocumentClusterId = 1;
        private const int headerClusterId = 2;
        private const int headerVersion = 0;
        private const int headerInstanceInfo = 0;
        private const int headerTypeCode = 0xf006;
        private const int currentMaximumShapeID = 0x3ffd7ff;
        private const int idClusterSize = 8;
        private const int basePartSize = 0x10;
        private int mainDocumentPicturesCount;
        private int headerPicturesCount;

        private int CalcClustersCount() => 
            this.HasPicturesInHeader ? 2 : 1;

        private int CalcDrawingsCount() => 
            this.HasPicturesInHeader ? 2 : 1;

        private int CalcMaxShapeIdentifier() => 
            this.HasPicturesInHeader ? ((0x800 + this.HeaderFloatingObjectsCount) + 1) : ((0x401 + this.MainDocumentFloatingObjectsCount) + 1);

        private int CalcShapesCount()
        {
            int num = this.MainDocumentFloatingObjectsCount + 1;
            if (this.HeaderFloatingObjectsCount > 0)
            {
                num += this.HeaderFloatingObjectsCount + 1;
            }
            return num;
        }

        public static OfficeArtFileDrawingGroupRecord FromStream(BinaryReader reader, OfficeArtRecordHeader header)
        {
            OfficeArtFileDrawingGroupRecord record = new OfficeArtFileDrawingGroupRecord();
            record.Read(reader, header);
            return record;
        }

        protected internal override int GetSize() => 
            (this.CalcClustersCount() * 8) + 0x10;

        protected internal void Read(BinaryReader reader, OfficeArtRecordHeader header)
        {
            if (reader.ReadInt32() >= 0x3ffd7ff)
            {
                OfficeArtExceptions.ThrowInvalidContent();
            }
            int num2 = reader.ReadInt32() - 1;
            if (header.Length != ((num2 * 8) + 0x10))
            {
                OfficeArtExceptions.ThrowInvalidContent();
            }
            reader.ReadInt32();
            reader.ReadInt32();
            reader.BaseStream.Seek((long) (num2 * 8), SeekOrigin.Current);
        }

        private void WriteClustersInfo(BinaryWriter writer)
        {
            writer.Write(1);
            writer.Write((int) (this.MainDocumentFloatingObjectsCount + 2));
            if (this.HasPicturesInHeader)
            {
                writer.Write(2);
                writer.Write((int) (this.HeaderFloatingObjectsCount + 1));
            }
        }

        protected internal override void WriteCore(BinaryWriter writer)
        {
            writer.Write(this.CalcMaxShapeIdentifier());
            writer.Write((int) (this.CalcClustersCount() + 1));
            writer.Write(this.CalcShapesCount());
            writer.Write(this.CalcDrawingsCount());
            this.WriteClustersInfo(writer);
        }

        public override int HeaderInstanceInfo =>
            0;

        public override int HeaderTypeCode =>
            0xf006;

        public override int HeaderVersion =>
            0;

        public int MainDocumentFloatingObjectsCount
        {
            get => 
                this.mainDocumentPicturesCount;
            set => 
                this.mainDocumentPicturesCount = value;
        }

        public int HeaderFloatingObjectsCount
        {
            get => 
                this.headerPicturesCount;
            set => 
                this.headerPicturesCount = value;
        }

        internal bool HasPicturesInHeader =>
            this.HeaderFloatingObjectsCount > 0;
    }
}

