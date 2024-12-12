namespace DevExpress.Emf
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class EmfMetafileParser : IEnumerable<EmfRecord>, IEnumerable
    {
        private readonly byte[] metafileBytes;

        public EmfMetafileParser(byte[] metafileBytes)
        {
            this.metafileBytes = metafileBytes;
        }

        [IteratorStateMachine(typeof(<GetEnumerator>d__2))]
        public IEnumerator<EmfRecord> GetEnumerator()
        {
            BinaryReader reader = new BinaryReader(new MemoryStream(this.metafileBytes));
            bool <isShouldProcessEmf>5__3 = false;
            while (true)
            {
                if (reader.BaseStream.Position >= reader.BaseStream.Length)
                {
                    reader = null;
                    yield break;
                    break;
                }
                EmfRecordType type = (EmfRecordType) reader.ReadInt32();
                int <size>5__2 = reader.ReadInt32();
                if (type == EmfRecordType.EMR_HEADER)
                {
                    yield return new EmfMetafileHeaderRecord(reader.ReadBytes(<size>5__2 - 8));
                    yield break;
                    break;
                }
                if (type == EmfRecordType.EMR_EOF)
                {
                    yield return new EmfEofRecord();
                    yield break;
                    break;
                }
                if (type == EmfRecordType.EMR_COMMENT)
                {
                    int dataSize = reader.ReadInt32();
                    if (reader.ReadInt32() == 0x2b464d45)
                    {
                        EmfCommentEmfPlusRecord record = EmfCommentEmfPlusRecord.Parse(reader, dataSize);
                        if (record != null)
                        {
                            IList<EmfPlusRecord> records = record.Records;
                            int count = records.Count;
                            if (count > 0)
                            {
                                <isShouldProcessEmf>5__3 = records[count - 1] is EmfPlusGetDCRecord;
                            }
                        }
                        yield return record;
                        yield break;
                        break;
                    }
                    reader.ReadBytes(<size>5__2 - 0x10);
                    continue;
                }
                if (<isShouldProcessEmf>5__3)
                {
                    yield return null;
                    yield break;
                    break;
                }
                reader.ReadBytes(<size>5__2 - 8);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

    }
}

