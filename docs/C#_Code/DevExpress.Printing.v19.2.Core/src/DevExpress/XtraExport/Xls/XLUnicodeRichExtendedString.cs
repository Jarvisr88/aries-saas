namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class XLUnicodeRichExtendedString : XLUnicodeStringBase, ISupportPartialReading
    {
        private const int headerSize = 3;
        private List<XlsFormatRun> formatRuns = new List<XlsFormatRun>();
        private byte[] phoneticData = new byte[0];
        private int charCount;
        private int formatRunCount;
        private int phoneticDataLen;

        private void Cleanup()
        {
            this.charCount = 0;
            base.Value = null;
            this.FormatRuns.Clear();
            this.PhoneticData = null;
        }

        public void CopyFrom(XLUnicodeRichExtendedString value)
        {
            this.Cleanup();
            base.ForceHighBytes = value.ForceHighBytes;
            base.Value = value.Value;
            foreach (XlsFormatRun run in value.FormatRuns)
            {
                XlsFormatRun item = new XlsFormatRun {
                    CharIndex = run.CharIndex,
                    FontIndex = run.FontIndex
                };
                this.FormatRuns.Add(item);
            }
            if (value.PhoneticData.Length != 0)
            {
                byte[] destinationArray = new byte[value.PhoneticData.Length];
                Array.Copy(value.PhoneticData, destinationArray, destinationArray.Length);
                this.PhoneticData = destinationArray;
            }
        }

        public override bool Equals(object obj)
        {
            XLUnicodeRichExtendedString str = obj as XLUnicodeRichExtendedString;
            if (str == null)
            {
                return false;
            }
            if (base.Value != str.Value)
            {
                return false;
            }
            if (this.FormatRuns.Count != str.FormatRuns.Count)
            {
                return false;
            }
            if (this.FormatRuns.Count > 0)
            {
                for (int i = 0; i < this.FormatRuns.Count; i++)
                {
                    if (!this.FormatRuns[i].Equals(str.FormatRuns[i]))
                    {
                        return false;
                    }
                }
            }
            if (this.PhoneticData.Length != str.PhoneticData.Length)
            {
                return false;
            }
            if (this.PhoneticData.Length != 0)
            {
                for (int i = 0; i < this.PhoneticData.Length; i++)
                {
                    if (this.PhoneticData[i] != str.PhoneticData[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static XLUnicodeRichExtendedString FromStream(XlReader reader)
        {
            XLUnicodeRichExtendedString str = new XLUnicodeRichExtendedString();
            str.Read(reader);
            return str;
        }

        protected override byte GetFlags()
        {
            byte flags = base.GetFlags();
            if (this.HasPhoneticData)
            {
                flags = (byte) (flags | 4);
            }
            if (this.IsRichString)
            {
                flags = (byte) (flags | 8);
            }
            return flags;
        }

        public override int GetHashCode() => 
            0;

        protected override int GetHeaderSize()
        {
            int num = 3;
            if (base.Value.Length > 0)
            {
                num += base.HasHighBytes ? 2 : 1;
            }
            if (this.IsRichString)
            {
                num += 2;
            }
            if (this.HasPhoneticData)
            {
                num += 4;
            }
            return num;
        }

        private bool IsEmpty() => 
            (this.charCount == 0) && ((base.Value.Length == 0) && ((this.FormatRuns.Count == 0) && (this.PhoneticData.Length == 0)));

        private bool IsFormatRunsCompleted() => 
            this.FormatRuns.Count == this.formatRunCount;

        private bool IsPhoneticDataCompleted() => 
            this.PhoneticData.Length == this.phoneticDataLen;

        private bool IsValueCompleted() => 
            base.Value.Length == this.charCount;

        public void ReadData(XlReader reader)
        {
            if (this.Complete)
            {
                this.Cleanup();
            }
            if (this.IsEmpty())
            {
                this.charCount = this.ReadCharCount(reader);
                byte flags = base.ReadFlags(reader);
                this.ReadExtraHeader(reader, flags);
            }
            if (this.ReadValuePart(reader) && this.ReadFormatRunsPart(reader))
            {
                this.ReadPhoneticDataPart(reader);
            }
        }

        protected override void ReadExtraData(BinaryDataReaderBase reader, byte flags)
        {
            this.formatRuns.Clear();
            if (this.formatRunCount > 0)
            {
                for (int i = 0; i < this.formatRunCount; i++)
                {
                    this.formatRuns.Add(XlsFormatRun.FromStream(reader));
                }
            }
            if (this.phoneticDataLen > 0)
            {
                this.PhoneticData = reader.ReadBytes(this.phoneticDataLen);
            }
            else
            {
                this.PhoneticData = null;
            }
        }

        protected override void ReadExtraHeader(BinaryDataReaderBase reader, byte flags)
        {
            bool flag = Convert.ToBoolean((int) (flags & 4));
            this.formatRunCount = !Convert.ToBoolean((int) (flags & 8)) ? 0 : reader.ReadUInt16();
            if (flag)
            {
                this.phoneticDataLen = reader.ReadInt32();
            }
            else
            {
                this.phoneticDataLen = 0;
            }
        }

        private bool ReadFormatRunsPart(XlReader reader)
        {
            while (true)
            {
                if (!this.IsFormatRunsCompleted())
                {
                    long num = reader.StreamLength - reader.Position;
                    if (num >= 4L)
                    {
                        this.FormatRuns.Add(XlsFormatRun.FromStream(reader));
                        continue;
                    }
                }
                return this.IsFormatRunsCompleted();
            }
        }

        private void ReadPhoneticDataPart(XlReader reader)
        {
            if (!this.IsPhoneticDataCompleted())
            {
                int count = this.phoneticDataLen - this.PhoneticData.Length;
                long num2 = reader.StreamLength - reader.Position;
                if (count > num2)
                {
                    count = (int) num2;
                }
                byte[] sourceArray = reader.ReadBytes(count);
                if (this.PhoneticData.Length == 0)
                {
                    this.PhoneticData = sourceArray;
                }
                else
                {
                    int length = this.PhoneticData.Length;
                    byte[] destinationArray = new byte[length + count];
                    Array.Copy(this.PhoneticData, destinationArray, length);
                    Array.Copy(sourceArray, 0, destinationArray, length, count);
                    this.PhoneticData = destinationArray;
                }
            }
        }

        private bool ReadValuePart(XlReader reader)
        {
            if (!this.IsValueCompleted())
            {
                if (reader.Position == 0)
                {
                    base.ReadFlags(reader);
                }
                long num = reader.StreamLength - reader.Position;
                int count = this.charCount - base.Value.Length;
                if (base.HasHighBytes)
                {
                    count *= 2;
                }
                if (count > num)
                {
                    count = (int) num;
                }
                byte[] bytes = reader.ReadBytes(count);
                string str = XLStringEncoder.GetEncoding(base.HasHighBytes).GetString(bytes, 0, count);
                base.Value = base.Value + str;
            }
            return this.IsValueCompleted();
        }

        protected override void WriteExtraData(BinaryWriter writer)
        {
            if (this.IsRichString)
            {
                for (int i = 0; i < this.FormatRuns.Count; i++)
                {
                    this.FormatRuns[i].Write(writer);
                }
            }
            if (this.HasPhoneticData)
            {
                writer.Write(this.PhoneticData);
            }
        }

        protected override void WriteExtraHeader(BinaryWriter writer)
        {
            if (this.IsRichString)
            {
                writer.Write((ushort) this.FormatRuns.Count);
            }
            if (this.HasPhoneticData)
            {
                writer.Write(this.PhoneticData.Length);
            }
        }

        public bool HasPhoneticData =>
            this.phoneticData.Length != 0;

        public bool IsRichString =>
            this.formatRuns.Count > 0;

        public IList<XlsFormatRun> FormatRuns =>
            this.formatRuns;

        public byte[] PhoneticData
        {
            get => 
                this.phoneticData;
            set
            {
                if (value == null)
                {
                    this.phoneticData = new byte[0];
                }
                else
                {
                    this.phoneticData = value;
                }
            }
        }

        public override int Length
        {
            get
            {
                int num = 3 + (base.HasHighBytes ? (base.Value.Length * 2) : base.Value.Length);
                if (this.IsRichString)
                {
                    num += (this.formatRuns.Count * 4) + 2;
                }
                if (this.HasPhoneticData)
                {
                    num += this.phoneticData.Length + 4;
                }
                return num;
            }
        }

        public bool Complete =>
            this.IsValueCompleted() && (this.IsFormatRunsCompleted() && this.IsPhoneticDataCompleted());
    }
}

