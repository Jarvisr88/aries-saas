namespace DevExpress.Utils.Internal
{
    using System;
    using System.IO;

    internal class CharMapper4
    {
        private CharMapper4Segment[] segments;

        public int GetGlyphIndex(char chr)
        {
            if (this.segments == null)
            {
                throw new InvalidOperationException("you should load char mapper first");
            }
            int index = this.IndexOfSegment(chr);
            return ((index >= 0) ? this.segments[index].GetGlyphIndex(chr) : 0);
        }

        protected int IndexOfSegment(char chr)
        {
            if (this.segments != null)
            {
                ushort num = chr;
                int num2 = 0;
                int num3 = this.segments.Length - 1;
                while (num2 <= num3)
                {
                    int index = num2 + ((num3 - num2) >> 1);
                    CharMapper4Segment segment = this.segments[index];
                    if ((segment.StartCode <= num) && (num <= segment.EndCode))
                    {
                        return index;
                    }
                    if (num > segment.EndCode)
                    {
                        num2 = index + 1;
                        continue;
                    }
                    num3 = index - 1;
                }
            }
            return -1;
        }

        public void Read(BigEndianStreamReader reader)
        {
            reader.Stream.Seek(4L, SeekOrigin.Current);
            int num = reader.ReadUShort();
            if ((num % 2) != 0)
            {
                throw new ArgumentException("Invalid data");
            }
            num /= 2;
            reader.Stream.Seek(6L, SeekOrigin.Current);
            this.segments = new CharMapper4Segment[num];
            for (int i = 0; i < num; i++)
            {
                this.segments[i] = new CharMapper4Segment();
                this.segments[i].EndCode = reader.ReadUShort();
                if ((i == (num - 1)) && (this.segments[i].EndCode != 0xffff))
                {
                    throw new ArgumentException("corrupted data: last segment's endCount have to be 0xFFFF");
                }
            }
            if (reader.ReadUShort() != 0)
            {
                throw new ArgumentException("corrupted data: zero pad is missing");
            }
            for (int j = 0; j < num; j++)
            {
                this.segments[j].StartCode = reader.ReadUShort();
                if (this.segments[j].StartCode > this.segments[j].EndCode)
                {
                    throw new ArgumentException("corrupted data: invalid startCount");
                }
            }
            for (int k = 0; k < num; k++)
            {
                this.segments[k].IdDelta = reader.ReadShort();
            }
            for (int m = 0; m < num; m++)
            {
                ushort num7 = reader.ReadUShort();
                if (num7 != 0)
                {
                    long position = reader.Stream.Position;
                    reader.Stream.Seek((long) (num7 - 2), SeekOrigin.Current);
                    CharMapper4Segment segment = this.segments[m];
                    segment.IndexArray = new ushort[segment.EndCode - segment.StartCode];
                    int index = 0;
                    while (true)
                    {
                        if (index >= segment.IndexArray.Length)
                        {
                            reader.Stream.Seek(position, SeekOrigin.Begin);
                            break;
                        }
                        segment.IndexArray[index] = reader.ReadUShort();
                        index++;
                    }
                }
            }
        }

        public void ReadInternal(BigEndianStreamReader reader)
        {
            int num = reader.ReadUShort();
            this.segments = new CharMapper4Segment[num];
            for (int i = 0; i < num; i++)
            {
                this.segments[i] = new CharMapper4Segment();
                this.segments[i].ReadInternal(reader);
            }
        }

        public void WriteInternal(BigEndianStreamWriter writer)
        {
            int length = this.segments.Length;
            writer.WriteUShort((ushort) length);
            for (int i = 0; i < length; i++)
            {
                this.segments[i].WriteInternal(writer);
            }
        }

        public CharMapper4Segment[] Segments =>
            this.segments;
    }
}

