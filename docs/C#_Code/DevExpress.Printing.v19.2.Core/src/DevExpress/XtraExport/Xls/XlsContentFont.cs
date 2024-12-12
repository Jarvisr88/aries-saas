namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentFont : XlsContentBase
    {
        private const double fontCoeff = 20.0;
        private const short defaultNormal = 400;
        private const short defaultBoldness = 700;
        private const int basePartSize = 14;
        private short boldness = 400;
        private ShortXLUnicodeString fontName;

        public XlsContentFont()
        {
            ShortXLUnicodeString text1 = new ShortXLUnicodeString();
            text1.ForceHighBytes = true;
            this.fontName = text1;
        }

        public override int GetSize() => 
            14 + this.fontName.Length;

        public override void Read(XlReader reader, int size)
        {
            this.Size = ((double) reader.ReadInt16()) / 20.0;
            ushort num = reader.ReadUInt16();
            this.Italic = (num & 2) != 0;
            this.StrikeThrough = (num & 8) != 0;
            this.Outline = (num & 0x10) != 0;
            this.Shadow = (num & 0x20) != 0;
            this.Condense = (num & 0x40) != 0;
            this.Extend = (num & 0x80) != 0;
            this.ColorIndex = reader.ReadInt16();
            this.boldness = reader.ReadInt16();
            this.Script = (XlScriptType) reader.ReadInt16();
            this.Underline = (XlUnderlineType) reader.ReadByte();
            this.FontFamily = reader.ReadByte();
            this.Charset = reader.ReadByte();
            reader.ReadByte();
            this.fontName = ShortXLUnicodeString.FromStream(reader);
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((short) (this.Size * 20.0));
            ushort num = 0;
            if (this.Italic)
            {
                num = (ushort) (num | 2);
            }
            if (this.Underline == XlUnderlineType.Single)
            {
                num = (ushort) (num | 4);
            }
            if (this.StrikeThrough)
            {
                num = (ushort) (num | 8);
            }
            if (this.Outline)
            {
                num = (ushort) (num | 0x10);
            }
            if (this.Shadow)
            {
                num = (ushort) (num | 0x20);
            }
            if (this.Condense)
            {
                num = (ushort) (num | 0x40);
            }
            if (this.Extend)
            {
                num = (ushort) (num | 0x80);
            }
            writer.Write(num);
            writer.Write((short) this.ColorIndex);
            if (this.Bold)
            {
                writer.Write((short) 700);
            }
            else
            {
                writer.Write((short) 400);
            }
            writer.Write((short) this.Script);
            writer.Write((byte) this.Underline);
            writer.Write((byte) this.FontFamily);
            writer.Write((byte) this.Charset);
            writer.Write((byte) 0);
            this.fontName.Write(writer);
        }

        public double Size { get; set; }

        public bool Bold
        {
            get => 
                this.boldness >= 700;
            set => 
                this.boldness = value ? ((short) 700) : ((short) 400);
        }

        public bool Italic { get; set; }

        public bool StrikeThrough { get; set; }

        public bool Outline { get; set; }

        public bool Shadow { get; set; }

        public bool Condense { get; set; }

        public bool Extend { get; set; }

        public XlScriptType Script { get; set; }

        public XlUnderlineType Underline { get; set; }

        public int FontFamily { get; set; }

        public int Charset { get; set; }

        public string FontName
        {
            get => 
                this.fontName.Value;
            set
            {
                if (!string.IsNullOrEmpty(value) && (value.Length > 0x1f))
                {
                    value = value.Substring(0, 0x1f);
                }
                this.fontName.Value = value;
            }
        }

        public int ColorIndex { get; set; }

        public short Boldness =>
            this.boldness;
    }
}

