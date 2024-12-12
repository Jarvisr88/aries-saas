namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsDxfFont
    {
        public const int DefaultIntValue = -1;
        public const short Size = 0x76;
        private const int maxFontNameLength = 0x3f;
        private const int normalFontWeightValue = 400;
        private const int boldFontWeightValue = 700;
        private XLUnicodeStringNoCch fontName = new XLUnicodeStringNoCch();
        private int fontSize = -1;
        private int fontWeight = -1;
        private int fontScript = -1;
        private int fontUnderline = -1;
        private int fontFamily;
        private int fontCharset;
        private int fontColorIndex = -1;
        private int firstPosition = -1;
        private int charactersCount = -1;
        private bool isDefaultFont = true;
        private bool fontItalicNinch = true;
        private bool fontStrikeThroughNinch = true;
        private bool fontScriptNinch = true;
        private bool fontUnderlineNinch = true;
        private bool fontBoldNinch = true;

        public void Read(BinaryReader reader)
        {
            this.ReadFontName(reader);
            this.ReadStxpStructure(reader);
            this.FontColorIndex = reader.ReadInt32();
            reader.ReadInt32();
            this.ReadTsNinchStructure(reader);
            this.FontScriptNinch = reader.ReadInt32() != 0;
            this.FontUnderlineNinch = reader.ReadInt32() != 0;
            this.FontBoldNinch = reader.ReadInt32() != 0;
            reader.ReadInt32();
            this.FirstPosition = reader.ReadInt32();
            this.CharactersCount = reader.ReadInt32();
            this.IsDefaultFont = reader.ReadInt16() == 0;
        }

        private void ReadFontName(BinaryReader reader)
        {
            int charCount = reader.ReadByte();
            int count = 0x3f;
            if (charCount > 0)
            {
                this.fontName = XLUnicodeStringNoCch.FromStream(reader, charCount);
                count -= this.fontName.Length;
            }
            reader.ReadBytes(count);
        }

        private void ReadStxpStructure(BinaryReader reader)
        {
            this.FontSize = reader.ReadInt32();
            this.ReadTsStructure(reader);
            this.fontWeight = reader.ReadInt16();
            this.fontScript = reader.ReadInt16();
            this.fontUnderline = reader.ReadSByte();
            this.FontFamily = reader.ReadByte();
            this.fontCharset = reader.ReadByte();
            reader.ReadByte();
        }

        private void ReadTsNinchStructure(BinaryReader reader)
        {
            uint num = reader.ReadUInt32();
            this.FontItalicNinch = Convert.ToBoolean((uint) (num & 2));
            this.FontStrikeThroughNinch = Convert.ToBoolean((uint) (num & 0x80));
        }

        private void ReadTsStructure(BinaryReader reader)
        {
            uint num = reader.ReadUInt32();
            this.FontItalic = Convert.ToBoolean((uint) (num & 2));
            this.FontStrikeThrough = Convert.ToBoolean((uint) (num & 0x80));
        }

        public void Write(BinaryWriter writer)
        {
            XlsChunkWriter writer2 = writer as XlsChunkWriter;
            if (writer2 != null)
            {
                writer2.BeginRecord(0x76);
            }
            this.WriteFontName(writer);
            this.WriteStxpStructure(writer);
            writer.Write((uint) this.FontColorIndex);
            writer.Write((uint) 0);
            this.WriteTsStructure(writer, this.FontItalicNinch, this.FontStrikeThroughNinch);
            writer.Write(this.FontScriptNinch ? 1 : 0);
            writer.Write(this.FontUnderlineNinch ? 1 : 0);
            writer.Write(this.FontBoldNinch ? 1 : 0);
            writer.Write((uint) 0);
            writer.Write((uint) this.FirstPosition);
            writer.Write((uint) this.CharactersCount);
            writer.Write(this.IsDefaultFont ? ((ushort) 0) : ((ushort) 1));
        }

        private void WriteFontName(BinaryWriter writer)
        {
            int num = 0x3f;
            if (string.IsNullOrEmpty(this.fontName.Value))
            {
                writer.Write((byte) 0);
            }
            else
            {
                writer.Write((byte) this.fontName.Value.Length);
                this.fontName.Write(writer);
                num -= this.fontName.Length;
            }
            if (num > 0)
            {
                writer.Write(new byte[num]);
            }
        }

        private void WriteStxpStructure(BinaryWriter writer)
        {
            writer.Write(this.FontSize);
            this.WriteTsStructure(writer, this.FontItalic, this.FontStrikeThrough);
            writer.Write((ushort) this.fontWeight);
            writer.Write((ushort) this.fontScript);
            writer.Write((byte) this.fontUnderline);
            writer.Write((byte) this.FontFamily);
            writer.Write((byte) this.FontCharset);
            writer.Write((byte) 0);
        }

        private void WriteTsStructure(BinaryWriter writer, bool fontItalic, bool fontStrikeThrough)
        {
            uint num = 0;
            if (fontItalic)
            {
                num |= 2;
            }
            if (fontStrikeThrough)
            {
                num |= 0x80;
            }
            writer.Write(num);
        }

        public string FontName
        {
            get => 
                this.fontName.Value;
            set
            {
                XLUnicodeStringNoCch cch = new XLUnicodeStringNoCch {
                    Value = value
                };
                if (cch.Length > 0x3f)
                {
                    throw new ArgumentException("String value too long");
                }
                this.fontName = cch;
            }
        }

        public int FontSize
        {
            get => 
                this.fontSize;
            set => 
                this.fontSize = value;
        }

        public int FontFamily
        {
            get => 
                this.fontFamily;
            set => 
                this.fontFamily = value;
        }

        public int FontCharset
        {
            get => 
                this.fontCharset;
            set => 
                this.fontCharset = value;
        }

        public int FontColorIndex
        {
            get => 
                this.fontColorIndex;
            set => 
                this.fontColorIndex = value;
        }

        public int FirstPosition
        {
            get => 
                this.firstPosition;
            set => 
                this.firstPosition = value;
        }

        public int CharactersCount
        {
            get => 
                this.charactersCount;
            set
            {
                if ((this.FirstPosition == -1) && (value != -1))
                {
                    throw new ArgumentException("Invalid number of characters");
                }
                this.charactersCount = value;
            }
        }

        public bool IsDefaultFont
        {
            get => 
                this.isDefaultFont;
            set => 
                this.isDefaultFont = value;
        }

        public bool FontItalicNinch
        {
            get => 
                this.fontItalicNinch;
            set => 
                this.fontItalicNinch = value;
        }

        public bool FontStrikeThroughNinch
        {
            get => 
                this.fontStrikeThroughNinch;
            set => 
                this.fontStrikeThroughNinch = value;
        }

        public bool FontScriptNinch
        {
            get => 
                this.fontScriptNinch;
            set => 
                this.fontScriptNinch = value;
        }

        public bool FontUnderlineNinch
        {
            get => 
                this.fontUnderlineNinch;
            set => 
                this.fontUnderlineNinch = value;
        }

        public bool FontBoldNinch
        {
            get => 
                this.fontBoldNinch;
            set => 
                this.fontBoldNinch = value;
        }

        public bool? FontBold
        {
            get
            {
                if (this.fontWeight != -1)
                {
                    return new bool?(this.fontWeight == 700);
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    this.fontWeight = value.Value ? 700 : 400;
                }
                else
                {
                    this.fontWeight = -1;
                }
            }
        }

        public XlScriptType? FontScript
        {
            get
            {
                if (this.fontScript != -1)
                {
                    return new XlScriptType?((XlScriptType) this.fontScript);
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    this.fontScript = value.Value;
                }
                else
                {
                    this.fontScript = -1;
                }
            }
        }

        public XlUnderlineType? FontUnderline
        {
            get
            {
                if (this.fontUnderline != -1)
                {
                    return new XlUnderlineType?((XlUnderlineType) this.fontUnderline);
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    this.fontUnderline = value.Value;
                }
                else
                {
                    this.fontUnderline = -1;
                }
            }
        }

        public bool FontItalic { get; set; }

        public bool FontStrikeThrough { get; set; }
    }
}

