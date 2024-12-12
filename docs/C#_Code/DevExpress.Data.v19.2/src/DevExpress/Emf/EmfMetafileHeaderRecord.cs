namespace DevExpress.Emf
{
    using System;
    using System.IO;

    public class EmfMetafileHeaderRecord : EmfRecord
    {
        public const int RecordSize = 0x58;
        private const int ENHMETA_SIGNATURE = 0x464d4520;
        private const int emfVersion = 0x10000;
        private const int emfRecordsCount = 3;
        private readonly EmfRectL bounds;
        private readonly EmfRectL frame;
        private readonly int size;
        private readonly int recordsCount;
        private readonly short handlesCount;
        private readonly int palEntries;
        private readonly EmfSizeL device;
        private readonly EmfSizeL millimeters;

        public EmfMetafileHeaderRecord(byte[] content)
        {
            using (BinaryReader reader = new BinaryReader(new MemoryStream(content)))
            {
                this.bounds = new EmfRectL(reader);
                this.frame = new EmfRectL(reader);
                reader.ReadInt32();
                reader.ReadInt32();
                this.size = reader.ReadInt32();
                this.recordsCount = reader.ReadInt32();
                this.handlesCount = reader.ReadInt16();
                reader.ReadBytes(10);
                this.palEntries = reader.ReadInt32();
                this.device = new EmfSizeL(reader);
                this.millimeters = new EmfSizeL(reader);
            }
        }

        public EmfMetafileHeaderRecord(EmfRectL bounds, EmfRectL frame, int size, EmfSizeL device, EmfSizeL millimeters)
        {
            this.bounds = bounds;
            this.frame = frame;
            this.size = size;
            this.device = device;
            this.millimeters = millimeters;
            this.recordsCount = 3;
            this.handlesCount = 1;
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Write(EmfContentWriter writer)
        {
            writer.Write(1);
            writer.Write(0x58);
            writer.Write(this.bounds);
            writer.Write(this.frame);
            writer.Write(0x464d4520);
            writer.Write(0x10000);
            writer.Write(this.size);
            writer.Write(this.recordsCount);
            writer.Write(this.handlesCount);
            writer.Write(new byte[10]);
            writer.Write(this.palEntries);
            writer.Write(this.device);
            writer.Write(this.millimeters);
        }

        public EmfRectL Bounds =>
            this.bounds;

        public EmfRectL Frame =>
            this.frame;

        public int Size =>
            this.size;

        public int RecordsCount =>
            this.recordsCount;

        public short HandlesCount =>
            this.handlesCount;

        public int PalEntries =>
            this.palEntries;

        public EmfSizeL Device =>
            this.device;

        public EmfSizeL Millimeters =>
            this.millimeters;
    }
}

