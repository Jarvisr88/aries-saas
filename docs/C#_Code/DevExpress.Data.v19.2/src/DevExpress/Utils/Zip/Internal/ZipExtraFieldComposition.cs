namespace DevExpress.Utils.Zip.Internal
{
    using DevExpress.Utils.Zip;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class ZipExtraFieldComposition : IZipExtraFieldCollection
    {
        private const int HeaderInfoSize = 4;
        private List<IZipExtraField> fields = new List<IZipExtraField>();

        public void Add(IZipExtraField field)
        {
            this.fields.Add(field);
        }

        public void Apply(InternalZipFile zipFile)
        {
            int count = this.fields.Count;
            for (int i = 0; i < count; i++)
            {
                this.fields[i].Apply(zipFile);
            }
        }

        public short CalculateSize(ExtraFieldType type)
        {
            short num = 0;
            int count = this.Fields.Count;
            for (int i = 0; i < count; i++)
            {
                IZipExtraField field = this.Fields[i];
                if ((field.Type & type) != 0)
                {
                    num = (short) (num + ((short) (field.ContentSize + 4)));
                }
            }
            return num;
        }

        public static ZipExtraFieldComposition Read(BinaryReader reader, long extraFieldsLength, IZipExtraFieldFactory headerFactory)
        {
            ZipExtraFieldComposition composition = new ZipExtraFieldComposition();
            long num2 = reader.BaseStream.Position + extraFieldsLength;
            while (reader.BaseStream.Position < num2)
            {
                short headerId = (short) reader.ReadUInt16();
                ushort num4 = reader.ReadUInt16();
                long position = reader.BaseStream.Position;
                long num6 = position + num4;
                IZipExtraField field = headerFactory.Create(headerId);
                FixedOffsetSequentialReadOnlyStream input = new FixedOffsetSequentialReadOnlyStream(reader.BaseStream, (long) num4);
                if (field != null)
                {
                    field.AssignRawData(new BinaryReader(input));
                    composition.Add(field);
                }
                SkipUnusedBytes(input, num6 - reader.BaseStream.Position);
            }
            return composition;
        }

        private static void SkipUnusedBytes(FixedOffsetSequentialReadOnlyStream dataStream, long bytesToSkip)
        {
            if (dataStream.CanSeek)
            {
                dataStream.Seek(0L, SeekOrigin.End);
            }
            else
            {
                for (long i = 0L; i < bytesToSkip; i += 1L)
                {
                    dataStream.ReadByte();
                }
            }
        }

        public void Write(BinaryWriter writer, ExtraFieldType type)
        {
            int count = this.Fields.Count;
            for (int i = 0; i < count; i++)
            {
                IZipExtraField field = this.Fields[i];
                if ((field.Type & type) != 0)
                {
                    writer.Write(field.Id);
                    writer.Write(field.ContentSize);
                    field.Write(writer);
                }
            }
        }

        public List<IZipExtraField> Fields =>
            this.fields;
    }
}

