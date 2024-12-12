namespace DevExpress.Utils.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;

    internal class CMAPTable
    {
        private CharMapper4 mapper;

        private EncodingRecord FindEncodingRecord(IEnumerable<EncodingRecord> encodingRecords, Platform platform, ushort encodingID)
        {
            EncodingRecord record2;
            using (IEnumerator<EncodingRecord> enumerator = encodingRecords.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        EncodingRecord current = enumerator.Current;
                        if (current.Platform != platform)
                        {
                            continue;
                        }
                        record2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return record2;
        }

        internal Size[] GetCharSegments()
        {
            CharMapper4Segment[] segments = this.mapper.Segments;
            int length = segments.Length;
            Size[] sizeArray = new Size[length];
            for (int i = 0; i < length; i++)
            {
                sizeArray[i] = new Size(segments[i].StartCode, segments[i].EndCode);
            }
            return sizeArray;
        }

        public int GetGlyphIndex(char chr)
        {
            if (this.mapper == null)
            {
                throw new InvalidOperationException("cmap table isn't loaded");
            }
            return this.mapper.GetGlyphIndex(chr);
        }

        public void Read(BigEndianStreamReader reader)
        {
            if (reader.ReadUShort() != 0)
            {
                throw new NotSupportedException("invalid cmap table version");
            }
            this.ReadV0(reader);
        }

        private List<EncodingRecord> ReadEncodingRecords(BigEndianStreamReader reader, ushort numRecords)
        {
            List<EncodingRecord> list = new List<EncodingRecord>(numRecords);
            for (ushort i = 0; i < numRecords; i = (ushort) (i + 1))
            {
                list.Add(new EncodingRecord(reader));
            }
            return list;
        }

        public void ReadInternal(BigEndianStreamReader reader)
        {
            this.mapper = new CharMapper4();
            this.mapper.ReadInternal(reader);
        }

        private void ReadV0(BigEndianStreamReader reader)
        {
            long num = reader.Stream.Position - 2L;
            ushort numRecords = reader.ReadUShort();
            List<EncodingRecord> encodingRecords = this.ReadEncodingRecords(reader, numRecords);
            EncodingRecord record = this.FindEncodingRecord(encodingRecords, Platform.Windows, 1);
            if (record == null)
            {
                throw new NotSupportedException("only Windows Unicode cmap tables are supported");
            }
            reader.Stream.Seek(num + record.Offset, SeekOrigin.Begin);
            reader.ReadUShort();
            this.mapper = new CharMapper4();
            this.mapper.Read(reader);
        }

        public void WriteInternal(BigEndianStreamWriter writer)
        {
            this.mapper.WriteInternal(writer);
        }
    }
}

