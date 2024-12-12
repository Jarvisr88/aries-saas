namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentStyle : XlsContentBase
    {
        private const short basePartSize = 2;
        private XLUnicodeString styleName = new XLUnicodeString();

        public XlsContentStyle()
        {
            this.BuiltInId = -2147483648;
            this.OutlineLevel = -2147483648;
        }

        public override int GetSize() => 
            !this.IsBuiltIn ? (2 + this.styleName.Length) : 4;

        public override void Read(XlReader reader, int size)
        {
            ushort num = reader.ReadUInt16();
            this.StyleFormatId = num & 0xfff;
            this.IsBuiltIn = Convert.ToBoolean((int) (num & 0x8000));
            if (!this.IsBuiltIn)
            {
                this.BuiltInId = -2147483648;
                this.OutlineLevel = -2147483648;
                this.styleName = XLUnicodeString.FromStream(reader);
            }
            else
            {
                this.BuiltInId = reader.ReadByte();
                this.OutlineLevel = reader.ReadByte() + 1;
                if ((this.BuiltInId != 1) && (this.BuiltInId != 2))
                {
                    this.OutlineLevel = -2147483648;
                }
                this.StyleName = string.Empty;
            }
        }

        public override void Write(BinaryWriter writer)
        {
            int styleFormatId = this.StyleFormatId;
            if (this.IsBuiltIn)
            {
                styleFormatId |= 0x8000;
            }
            writer.Write((ushort) styleFormatId);
            if (!this.IsBuiltIn)
            {
                this.styleName.Write(writer);
            }
            else
            {
                writer.Write((byte) this.BuiltInId);
                if ((this.BuiltInId != 1) && (this.BuiltInId != 2))
                {
                    writer.Write((byte) 0xff);
                }
                else
                {
                    writer.Write((byte) (this.OutlineLevel - 1));
                }
            }
        }

        public string StyleName
        {
            get => 
                this.styleName.Value;
            set => 
                this.styleName.Value = value;
        }

        public bool IsHidden { get; set; }

        public int BuiltInId { get; set; }

        public int OutlineLevel { get; set; }

        public int StyleFormatId { get; set; }

        public bool IsBuiltIn { get; set; }
    }
}

