namespace DevExpress.Emf
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class EmfCommentEmfPlusRecord : EmfRecord
    {
        public const int EmfPlusCommentIdentifier = 0x2b464d45;
        private readonly IList<EmfPlusRecord> records;
        private readonly int recordsSize;

        public EmfCommentEmfPlusRecord(IList<EmfPlusRecord> records, int recordsSize)
        {
            this.records = records;
            this.recordsSize = recordsSize;
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            foreach (EmfPlusRecord record in this.records)
            {
                record.Accept(visitor);
            }
        }

        public static EmfCommentEmfPlusRecord Parse(BinaryReader reader, int dataSize)
        {
            int num = 8;
            IList<EmfPlusRecord> records = new List<EmfPlusRecord>();
            while (num < dataSize)
            {
                EmfPlusRecordType type = (EmfPlusRecordType) reader.ReadInt16();
                short flags = reader.ReadInt16();
                num += reader.ReadInt32();
                EmfPlusRecord item = EmfPlusRecord.Create(type, flags, reader.ReadBytes(reader.ReadInt32()));
                if (item == null)
                {
                    return null;
                }
                records.Add(item);
            }
            return new EmfCommentEmfPlusRecord(records, dataSize - 4);
        }

        public override void Write(EmfContentWriter writer)
        {
            writer.Write(70);
            int num = this.recordsSize + 4;
            writer.Write((int) (num + 12));
            writer.Write(num);
            writer.Write(0x2b464d45);
            foreach (EmfPlusRecord record in this.records)
            {
                record.Write(writer);
            }
        }

        public IList<EmfPlusRecord> Records =>
            this.records;
    }
}

