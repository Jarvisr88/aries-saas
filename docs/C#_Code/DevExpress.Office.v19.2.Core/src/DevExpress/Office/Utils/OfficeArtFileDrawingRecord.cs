namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public class OfficeArtFileDrawingRecord : OfficeDrawingPartBase
    {
        private const int recordLength = 8;
        private int drawingId;
        private int numberOfShapes;
        private int lastShapeId;

        public OfficeArtFileDrawingRecord(int drawingId)
        {
            this.drawingId = drawingId;
        }

        public static OfficeArtFileDrawingRecord FromStream(BinaryReader reader, OfficeArtRecordHeader header)
        {
            OfficeArtFileDrawingRecord record = new OfficeArtFileDrawingRecord(header.InstanceInfo);
            record.Read(reader, header);
            return record;
        }

        protected internal override int GetSize() => 
            8;

        protected internal void Read(BinaryReader reader, OfficeArtRecordHeader header)
        {
            if (header.Length != 8)
            {
                OfficeArtExceptions.ThrowInvalidContent();
            }
            this.numberOfShapes = reader.ReadInt32();
            this.lastShapeId = reader.ReadInt32();
        }

        protected internal override void WriteCore(BinaryWriter writer)
        {
            writer.Write(this.NumberOfShapes);
            writer.Write(this.LastShapeIdentifier);
        }

        public override int HeaderInstanceInfo =>
            this.drawingId;

        public override int HeaderTypeCode =>
            0xf008;

        public override int HeaderVersion =>
            0;

        public override int Length =>
            8;

        public int NumberOfShapes
        {
            get => 
                this.numberOfShapes;
            set => 
                this.numberOfShapes = value;
        }

        public int LastShapeIdentifier
        {
            get => 
                this.lastShapeId;
            set => 
                this.lastShapeId = value;
        }
    }
}

