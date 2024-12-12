namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public abstract class XLUnicodeStringBase
    {
        private bool hasHighBytes;
        private bool forceHighBytes;
        private string value = string.Empty;

        protected XLUnicodeStringBase()
        {
        }

        protected virtual byte GetFlags()
        {
            byte num = 0;
            if (this.HasHighBytes)
            {
                num = (byte) (num | 1);
            }
            return num;
        }

        protected abstract int GetHeaderSize();
        protected void Read(BinaryDataReaderBase reader)
        {
            int num = this.ReadCharCount(reader);
            byte flags = this.ReadFlags(reader);
            this.ReadExtraHeader(reader, flags);
            if (num <= 0)
            {
                this.value = string.Empty;
            }
            else
            {
                int count = this.HasHighBytes ? (num * 2) : num;
                byte[] bytes = reader.ReadBytes(count);
                this.value = XLStringEncoder.GetEncoding(this.HasHighBytes).GetString(bytes, 0, count);
            }
            this.ReadExtraData(reader, flags);
        }

        protected void Read(BinaryReader reader)
        {
            int num = this.ReadCharCount(reader);
            byte flags = this.ReadFlags(reader);
            this.ReadExtraHeader(reader, flags);
            if (num <= 0)
            {
                this.value = string.Empty;
            }
            else
            {
                int count = this.HasHighBytes ? (num * 2) : num;
                byte[] bytes = reader.ReadBytes(count);
                this.value = XLStringEncoder.GetEncoding(this.HasHighBytes).GetString(bytes, 0, count);
            }
            this.ReadExtraData(reader, flags);
        }

        protected void Read(XlReader reader, int size)
        {
            long position = reader.Position;
            int num2 = this.ReadCharCount(reader);
            byte flags = this.ReadFlags(reader);
            this.ReadExtraHeader(reader, flags);
            if (num2 <= 0)
            {
                this.value = string.Empty;
            }
            else
            {
                int num4 = size - ((int) (reader.Position - position));
                if (num4 < 0)
                {
                    throw new Exception("Wrong bytes to read for XLUnicodeString");
                }
                int count = this.HasHighBytes ? (num2 * 2) : num2;
                if (count > num4)
                {
                    count = !this.HasHighBytes ? num4 : ((num4 / 2) * 2);
                }
                byte[] bytes = reader.ReadBytes(count);
                this.value = XLStringEncoder.GetEncoding(this.HasHighBytes).GetString(bytes, 0, count);
            }
            this.ReadExtraData(reader, flags);
        }

        protected virtual int ReadCharCount(BinaryDataReaderBase reader) => 
            reader.ReadInt16();

        protected virtual int ReadCharCount(BinaryReader reader) => 
            reader.ReadInt16();

        protected virtual int ReadCharCount(byte[] data, ref long position)
        {
            short num = (short) (data[(int) ((IntPtr) position)] | (data[(int) ((IntPtr) (position + 1L))] << 8));
            position += 2L;
            return num;
        }

        protected virtual void ReadExtraData(BinaryDataReaderBase reader, byte flags)
        {
        }

        protected virtual void ReadExtraData(BinaryReader reader, byte flags)
        {
        }

        protected virtual void ReadExtraHeader(BinaryDataReaderBase reader, byte flags)
        {
        }

        protected virtual void ReadExtraHeader(BinaryReader reader, byte flags)
        {
        }

        protected internal byte ReadFlags(BinaryDataReaderBase reader)
        {
            byte num = reader.ReadByte();
            this.hasHighBytes = (num & 1) != 0;
            return num;
        }

        protected internal byte ReadFlags(BinaryReader reader)
        {
            byte num = reader.ReadByte();
            this.hasHighBytes = (num & 1) != 0;
            return num;
        }

        protected internal byte ReadFlags(byte[] data, ref long position)
        {
            byte num = data[(int) ((IntPtr) position)];
            position += 1L;
            this.hasHighBytes = (num & 1) != 0;
            return num;
        }

        private bool StringHasHighBytes(string text) => 
            !this.forceHighBytes ? XLStringEncoder.StringHasHighBytes(text) : true;

        protected virtual void ValidateStringValue(string text)
        {
            if (text.Length > 0x7fff)
            {
                throw new ArgumentException("String value too long");
            }
        }

        public void Write(BinaryWriter writer)
        {
            XlsChunkWriter writer2 = writer as XlsChunkWriter;
            if (writer2 != null)
            {
                writer2.BeginRecord(this.GetHeaderSize());
            }
            this.WriteCharCount(writer);
            writer.Write(this.GetFlags());
            this.WriteExtraHeader(writer);
            if (writer2 != null)
            {
                writer2.BeginStringValue(this.HasHighBytes);
            }
            writer.Write(XLStringEncoder.GetBytes(this.value, this.HasHighBytes));
            if (writer2 != null)
            {
                writer2.EndStringValue();
            }
            this.WriteExtraData(writer);
        }

        protected virtual void WriteCharCount(BinaryWriter writer)
        {
            writer.Write((short) this.Value.Length);
        }

        protected virtual void WriteExtraData(BinaryWriter writer)
        {
        }

        protected virtual void WriteExtraHeader(BinaryWriter writer)
        {
        }

        public bool HasHighBytes =>
            this.hasHighBytes;

        public bool ForceHighBytes
        {
            get => 
                this.forceHighBytes;
            set
            {
                this.forceHighBytes = value;
                this.hasHighBytes = this.StringHasHighBytes(this.value);
            }
        }

        public string Value
        {
            get => 
                this.value;
            set
            {
                value ??= string.Empty;
                this.ValidateStringValue(value);
                this.value = value;
                this.hasHighBytes = this.StringHasHighBytes(this.value);
            }
        }

        public abstract int Length { get; }
    }
}

