namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentHeaderFooter : XlsContentBase
    {
        private const int fixedPartSize = 0x26;
        private FutureRecordHeader recordHeader = new FutureRecordHeader();
        private Guid viewId = Guid.Empty;
        private XLUnicodeString evenHeader = new XLUnicodeString();
        private XLUnicodeString evenFooter = new XLUnicodeString();
        private XLUnicodeString firstHeader = new XLUnicodeString();
        private XLUnicodeString firstFooter = new XLUnicodeString();

        public override int GetSize()
        {
            int num = 0;
            if (this.evenHeader.Value.Length > 0)
            {
                num += this.evenHeader.Length;
            }
            if (this.evenFooter.Value.Length > 0)
            {
                num += this.evenFooter.Length;
            }
            if (this.firstHeader.Value.Length > 0)
            {
                num += this.firstHeader.Length;
            }
            if (this.firstFooter.Value.Length > 0)
            {
                num += this.firstFooter.Length;
            }
            return (0x26 + num);
        }

        public override void Read(XlReader reader, int size)
        {
            this.recordHeader = FutureRecordHeader.FromStream(reader);
            byte[] b = reader.ReadBytes(0x10);
            this.viewId = new Guid(b);
            ushort num = reader.ReadUInt16();
            this.DifferentOddEven = Convert.ToBoolean((int) (num & 1));
            this.DifferentFirst = Convert.ToBoolean((int) (num & 2));
            this.ScaleWithDoc = Convert.ToBoolean((int) (num & 4));
            this.AlignWithMargins = Convert.ToBoolean((int) (num & 8));
            int num3 = reader.ReadUInt16();
            int num4 = reader.ReadUInt16();
            int num5 = reader.ReadUInt16();
            if (reader.ReadUInt16() > 0)
            {
                this.evenHeader = XLUnicodeString.FromStream(reader);
            }
            if (num3 > 0)
            {
                this.evenFooter = XLUnicodeString.FromStream(reader);
            }
            if (num4 > 0)
            {
                this.firstHeader = XLUnicodeString.FromStream(reader);
            }
            if (num5 > 0)
            {
                this.firstFooter = XLUnicodeString.FromStream(reader);
            }
            int count = size - this.GetSize();
            if (count > 0)
            {
                reader.ReadBytes(count);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            this.recordHeader.Write(writer);
            writer.Write(this.viewId.ToByteArray());
            ushort num = 0;
            if (this.DifferentOddEven)
            {
                num = (ushort) (num | 1);
            }
            if (this.DifferentFirst)
            {
                num = (ushort) (num | 2);
            }
            if (this.ScaleWithDoc)
            {
                num = (ushort) (num | 4);
            }
            if (this.AlignWithMargins)
            {
                num = (ushort) (num | 8);
            }
            writer.Write(num);
            int length = this.evenHeader.Value.Length;
            int num3 = this.evenFooter.Value.Length;
            int num4 = this.firstHeader.Value.Length;
            int num5 = this.firstFooter.Value.Length;
            writer.Write((ushort) length);
            writer.Write((ushort) num3);
            writer.Write((ushort) num4);
            writer.Write((ushort) num5);
            if (length > 0)
            {
                this.evenHeader.Write(writer);
            }
            if (num3 > 0)
            {
                this.evenFooter.Write(writer);
            }
            if (num4 > 0)
            {
                this.firstHeader.Write(writer);
            }
            if (num5 > 0)
            {
                this.firstFooter.Write(writer);
            }
        }

        public Guid ViewId
        {
            get => 
                this.viewId;
            set => 
                this.viewId = value;
        }

        public bool AlignWithMargins { get; set; }

        public bool DifferentFirst { get; set; }

        public bool DifferentOddEven { get; set; }

        public bool ScaleWithDoc { get; set; }

        public string EvenHeader
        {
            get => 
                this.evenHeader.Value;
            set => 
                this.evenHeader.Value = value;
        }

        public string EvenFooter
        {
            get => 
                this.evenFooter.Value;
            set => 
                this.evenFooter.Value = value;
        }

        public string FirstHeader
        {
            get => 
                this.firstHeader.Value;
            set => 
                this.firstHeader.Value = value;
        }

        public string FirstFooter
        {
            get => 
                this.firstFooter.Value;
            set => 
                this.firstFooter.Value = value;
        }

        public override FutureRecordHeaderBase RecordHeader =>
            this.recordHeader;
    }
}

