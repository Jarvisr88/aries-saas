namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    internal class TTFTableDirectoryEntry
    {
        private byte[] tag;
        private uint checkSum;
        private uint offset;
        private uint length;

        public void Initialize(TTFTable table)
        {
            if (table.Tag.Length == 4)
            {
                this.tag = new byte[4];
                for (int i = 0; i < 4; i++)
                {
                    this.tag[i] = Convert.ToByte(table.Tag[i]);
                }
                this.checkSum = 0;
                this.offset = 0;
                this.length = (uint) table.Length;
            }
        }

        public void InitializeOffset(int offset)
        {
            this.offset ??= ((uint) offset);
        }

        public void Read(TTFStream ttfStream)
        {
            this.tag = ttfStream.ReadBytes(4);
            this.checkSum = ttfStream.ReadULong();
            this.offset = ttfStream.ReadULong();
            this.length = ttfStream.ReadULong();
        }

        public void Write(TTFStream ttfStream)
        {
            ttfStream.WriteBytes(this.tag);
            ttfStream.WriteULong(this.checkSum);
            ttfStream.WriteULong(this.offset);
            ttfStream.WriteULong(this.length);
        }

        public void WriteCheckSum(TTFStream ttfStream)
        {
            this.checkSum ??= TTFUtils.CalculateCheckSum(ttfStream, this.Offset, this.Length);
            ttfStream.Move(4);
            ttfStream.WriteULong(this.checkSum);
            ttfStream.Move(8);
        }

        public void WriteOffset(TTFStream ttfStream)
        {
            ttfStream.Move(8);
            ttfStream.WriteULong(this.offset);
            ttfStream.Move(4);
        }

        public static int SizeOf =>
            0x10;

        public string Tag
        {
            get
            {
                if (this.tag == null)
                {
                    return null;
                }
                string str = "";
                for (int i = 0; i < this.tag.Length; i++)
                {
                    str = str + Convert.ToChar(this.tag[i]).ToString();
                }
                return str;
            }
        }

        public int Offset =>
            Convert.ToInt32(this.offset);

        public int Length =>
            Convert.ToInt32(this.length);

        public uint CheckSum =>
            this.checkSum;
    }
}

