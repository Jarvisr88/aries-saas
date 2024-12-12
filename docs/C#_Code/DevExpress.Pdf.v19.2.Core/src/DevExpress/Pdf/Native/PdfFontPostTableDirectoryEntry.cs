namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfFontPostTableDirectoryEntry : PdfFontTableDirectoryEntry
    {
        public const string EntryTag = "post";
        private const int version3 = 0x30000;
        private static readonly string[] standardMacCharacterSet;
        private readonly float italicAngle;
        private readonly short underlinePosition;
        private readonly short underlineThickness;
        private readonly bool isFixedPitch;
        private readonly int minMemType42;
        private readonly int maxMemType42;
        private readonly int minMemType1;
        private readonly int maxMemType1;
        private readonly string[] glyphNames;

        static PdfFontPostTableDirectoryEntry()
        {
            string[] textArray1 = new string[0x102];
            textArray1[0] = ".notdef";
            textArray1[1] = "Null";
            textArray1[2] = "nonmarkingreturn";
            textArray1[3] = "space";
            textArray1[4] = "exclam";
            textArray1[5] = "quotedbl";
            textArray1[6] = "numbersign";
            textArray1[7] = "dollar";
            textArray1[8] = "percent";
            textArray1[9] = "ampersand";
            textArray1[10] = "quotesingle";
            textArray1[11] = "parenleft";
            textArray1[12] = "parenright";
            textArray1[13] = "asterisk";
            textArray1[14] = "plus";
            textArray1[15] = "comma";
            textArray1[0x10] = "hyphen";
            textArray1[0x11] = "period";
            textArray1[0x12] = "slash";
            textArray1[0x13] = "zero";
            textArray1[20] = "one";
            textArray1[0x15] = "two";
            textArray1[0x16] = "three";
            textArray1[0x17] = "four";
            textArray1[0x18] = "five";
            textArray1[0x19] = "six";
            textArray1[0x1a] = "seven";
            textArray1[0x1b] = "eight";
            textArray1[0x1c] = "nine";
            textArray1[0x1d] = "colon";
            textArray1[30] = "semicolon";
            textArray1[0x1f] = "less";
            textArray1[0x20] = "equal";
            textArray1[0x21] = "greater";
            textArray1[0x22] = "question";
            textArray1[0x23] = "at";
            textArray1[0x24] = "A";
            textArray1[0x25] = "B";
            textArray1[0x26] = "C";
            textArray1[0x27] = "D";
            textArray1[40] = "E";
            textArray1[0x29] = "F";
            textArray1[0x2a] = "G";
            textArray1[0x2b] = "H";
            textArray1[0x2c] = "I";
            textArray1[0x2d] = "J";
            textArray1[0x2e] = "K";
            textArray1[0x2f] = "L";
            textArray1[0x30] = "M";
            textArray1[0x31] = "N";
            textArray1[50] = "O";
            textArray1[0x33] = "P";
            textArray1[0x34] = "Q";
            textArray1[0x35] = "R";
            textArray1[0x36] = "S";
            textArray1[0x37] = "T";
            textArray1[0x38] = "U";
            textArray1[0x39] = "V";
            textArray1[0x3a] = "W";
            textArray1[0x3b] = "X";
            textArray1[60] = "Y";
            textArray1[0x3d] = "Z";
            textArray1[0x3e] = "bracketleft";
            textArray1[0x3f] = "backslash";
            textArray1[0x40] = "bracketright";
            textArray1[0x41] = "asciicircum";
            textArray1[0x42] = "underscore";
            textArray1[0x43] = "grave";
            textArray1[0x44] = "a";
            textArray1[0x45] = "b";
            textArray1[70] = "c";
            textArray1[0x47] = "d";
            textArray1[0x48] = "e";
            textArray1[0x49] = "f";
            textArray1[0x4a] = "g";
            textArray1[0x4b] = "h";
            textArray1[0x4c] = "i";
            textArray1[0x4d] = "j";
            textArray1[0x4e] = "k";
            textArray1[0x4f] = "l";
            textArray1[80] = "m";
            textArray1[0x51] = "n";
            textArray1[0x52] = "o";
            textArray1[0x53] = "p";
            textArray1[0x54] = "q";
            textArray1[0x55] = "r";
            textArray1[0x56] = "s";
            textArray1[0x57] = "t";
            textArray1[0x58] = "u";
            textArray1[0x59] = "v";
            textArray1[90] = "w";
            textArray1[0x5b] = "x";
            textArray1[0x5c] = "y";
            textArray1[0x5d] = "z";
            textArray1[0x5e] = "braceleft";
            textArray1[0x5f] = "bar";
            textArray1[0x60] = "braceright";
            textArray1[0x61] = "asciitilde";
            textArray1[0x62] = "Adieresis";
            textArray1[0x63] = "Aring";
            textArray1[100] = "Ccedilla";
            textArray1[0x65] = "Eacute";
            textArray1[0x66] = "Ntilde";
            textArray1[0x67] = "Odieresis";
            textArray1[0x68] = "Udieresis";
            textArray1[0x69] = "aacute";
            textArray1[0x6a] = "agrave";
            textArray1[0x6b] = "acircumflex";
            textArray1[0x6c] = "adieresis";
            textArray1[0x6d] = "atilde";
            textArray1[110] = "aring";
            textArray1[0x6f] = "ccedilla";
            textArray1[0x70] = "eacute";
            textArray1[0x71] = "egrave";
            textArray1[0x72] = "ecircumflex";
            textArray1[0x73] = "edieresis";
            textArray1[0x74] = "iacute";
            textArray1[0x75] = "igrave";
            textArray1[0x76] = "icircumflex";
            textArray1[0x77] = "idieresis";
            textArray1[120] = "ntilde";
            textArray1[0x79] = "oacute";
            textArray1[0x7a] = "ograve";
            textArray1[0x7b] = "ocircumflex";
            textArray1[0x7c] = "odieresis";
            textArray1[0x7d] = "otilde";
            textArray1[0x7e] = "uacute";
            textArray1[0x7f] = "ugrave";
            textArray1[0x80] = "ucircumflex";
            textArray1[0x81] = "udieresis";
            textArray1[130] = "dagger";
            textArray1[0x83] = "degree";
            textArray1[0x84] = "cent";
            textArray1[0x85] = "sterling";
            textArray1[0x86] = "section";
            textArray1[0x87] = "bullet";
            textArray1[0x88] = "paragraph";
            textArray1[0x89] = "germandbls";
            textArray1[0x8a] = "registered";
            textArray1[0x8b] = "copyright";
            textArray1[140] = "trademark";
            textArray1[0x8d] = "acute";
            textArray1[0x8e] = "dieresis";
            textArray1[0x8f] = "notequal";
            textArray1[0x90] = "AE";
            textArray1[0x91] = "Oslash";
            textArray1[0x92] = "infinity";
            textArray1[0x93] = "plusminus";
            textArray1[0x94] = "lessequal";
            textArray1[0x95] = "greaterequal";
            textArray1[150] = "yen";
            textArray1[0x97] = "mu";
            textArray1[0x98] = "partialdiff";
            textArray1[0x99] = "summation";
            textArray1[0x9a] = "product";
            textArray1[0x9b] = "pi";
            textArray1[0x9c] = "integral";
            textArray1[0x9d] = "ordfeminine";
            textArray1[0x9e] = "ordmasculine";
            textArray1[0x9f] = "Omega";
            textArray1[160] = "ae";
            textArray1[0xa1] = "oslash";
            textArray1[0xa2] = "questiondown";
            textArray1[0xa3] = "exclamdown";
            textArray1[0xa4] = "logicalnot";
            textArray1[0xa5] = "radical";
            textArray1[0xa6] = "florin";
            textArray1[0xa7] = "approxequal";
            textArray1[0xa8] = "increment";
            textArray1[0xa9] = "guillemotleft";
            textArray1[170] = "guillemotright";
            textArray1[0xab] = "ellipsis";
            textArray1[0xac] = "nonbreakingspace";
            textArray1[0xad] = "Agrave";
            textArray1[0xae] = "Atilde";
            textArray1[0xaf] = "Otilde";
            textArray1[0xb0] = "OE";
            textArray1[0xb1] = "oe";
            textArray1[0xb2] = "endash";
            textArray1[0xb3] = "emdash";
            textArray1[180] = "quotedblleft";
            textArray1[0xb5] = "quotedblright";
            textArray1[0xb6] = "quoteleft";
            textArray1[0xb7] = "quoteright";
            textArray1[0xb8] = "divide";
            textArray1[0xb9] = "lozenge";
            textArray1[0xba] = "ydieresis";
            textArray1[0xbb] = "Ydieresis";
            textArray1[0xbc] = "fraction";
            textArray1[0xbd] = "currency";
            textArray1[190] = "guilsinglleft";
            textArray1[0xbf] = "guilsinglright";
            textArray1[0xc0] = "fi";
            textArray1[0xc1] = "fl";
            textArray1[0xc2] = "daggerdbl";
            textArray1[0xc3] = "periodcentered";
            textArray1[0xc4] = "quotesinglbase";
            textArray1[0xc5] = "quotedblbase";
            textArray1[0xc6] = "perthousand";
            textArray1[0xc7] = "Acircumflex";
            textArray1[200] = "Ecircumflex";
            textArray1[0xc9] = "Aacute";
            textArray1[0xca] = "Edieresis";
            textArray1[0xcb] = "Egrave";
            textArray1[0xcc] = "Iacute";
            textArray1[0xcd] = "Icircumflex";
            textArray1[0xce] = "Idieresis";
            textArray1[0xcf] = "Igrave";
            textArray1[0xd0] = "Oacute";
            textArray1[0xd1] = "Ocircumflex";
            textArray1[210] = "apple";
            textArray1[0xd3] = "Ograve";
            textArray1[0xd4] = "Uacute";
            textArray1[0xd5] = "Ucircumflex";
            textArray1[0xd6] = "Ugrave";
            textArray1[0xd7] = "dotlessi";
            textArray1[0xd8] = "circumflex";
            textArray1[0xd9] = "tilde";
            textArray1[0xda] = "macron";
            textArray1[0xdb] = "breve";
            textArray1[220] = "dotaccent";
            textArray1[0xdd] = "ring";
            textArray1[0xde] = "cedilla";
            textArray1[0xdf] = "hungarumlaut";
            textArray1[0xe0] = "ogonek";
            textArray1[0xe1] = "caron";
            textArray1[0xe2] = "Lslash";
            textArray1[0xe3] = "lslash";
            textArray1[0xe4] = "Scaron";
            textArray1[0xe5] = "scaron";
            textArray1[230] = "Zcaron";
            textArray1[0xe7] = "zcaron";
            textArray1[0xe8] = "brokenbar";
            textArray1[0xe9] = "Eth";
            textArray1[0xea] = "eth";
            textArray1[0xeb] = "Yacute";
            textArray1[0xec] = "yacute";
            textArray1[0xed] = "Thorn";
            textArray1[0xee] = "thorn";
            textArray1[0xef] = "minus";
            textArray1[240] = "multiply";
            textArray1[0xf1] = "onesuperior";
            textArray1[0xf2] = "twosuperior";
            textArray1[0xf3] = "threesuperior";
            textArray1[0xf4] = "onehalf";
            textArray1[0xf5] = "onequarter";
            textArray1[0xf6] = "threequarters";
            textArray1[0xf7] = "franc";
            textArray1[0xf8] = "Gbreve";
            textArray1[0xf9] = "gbreve";
            textArray1[250] = "Idotaccent";
            textArray1[0xfb] = "Scedilla";
            textArray1[0xfc] = "scedilla";
            textArray1[0xfd] = "Cacute";
            textArray1[0xfe] = "cacute";
            textArray1[0xff] = "Ccaron";
            textArray1[0x100] = "ccaron";
            textArray1[0x101] = "dcroat";
            standardMacCharacterSet = textArray1;
        }

        public PdfFontPostTableDirectoryEntry(byte[] tableData) : base("post", tableData)
        {
            PdfBinaryStream tableStream = base.TableStream;
            int num = tableStream.ReadInt();
            this.italicAngle = tableStream.ReadFixed();
            this.underlinePosition = tableStream.ReadShort();
            this.underlineThickness = tableStream.ReadShort();
            this.isFixedPitch = tableStream.ReadInt() != 0;
            this.minMemType42 = tableStream.ReadInt();
            this.maxMemType42 = tableStream.ReadInt();
            this.minMemType1 = tableStream.ReadInt();
            this.maxMemType1 = tableStream.ReadInt();
            if (num == 0x10000)
            {
                this.glyphNames = standardMacCharacterSet;
            }
            else if (num == 0x20000)
            {
                int length = tableData.Length;
                if (tableData.Length > 0x20)
                {
                    int num3 = tableStream.ReadUshort();
                    short[] numArray = tableStream.ReadShortArray(num3);
                    List<string> list = new List<string>();
                    while (true)
                    {
                        if (tableStream.Position >= length)
                        {
                            int count = list.Count;
                            int num5 = standardMacCharacterSet.Length;
                            this.glyphNames = new string[num3];
                            for (int i = 0; i < num3; i++)
                            {
                                short index = numArray[i];
                                if ((index >= 0) && (index < num5))
                                {
                                    this.glyphNames[i] = standardMacCharacterSet[index];
                                }
                                else
                                {
                                    short num8 = (short) (index - num5);
                                    this.glyphNames[i] = ((num8 < 0) || (num8 >= count)) ? ".notdef" : list[num8];
                                }
                            }
                            break;
                        }
                        list.Add(tableStream.ReadString(tableStream.ReadByte()));
                    }
                }
            }
        }

        public PdfFontPostTableDirectoryEntry(PdfFont font) : base("post")
        {
            PdfFontDescriptor fontDescriptor = font.FontDescriptor;
            if (fontDescriptor == null)
            {
                this.italicAngle = 0f;
                this.isFixedPitch = false;
            }
            else
            {
                this.italicAngle = (float) fontDescriptor.ItalicAngle;
                this.isFixedPitch = fontDescriptor.Flags.HasFlag(PdfFontFlags.FixedPitch);
            }
            this.underlineThickness = (short) (this.underlinePosition / 5);
            PdfBinaryStream tableStream = base.TableStream;
            tableStream.WriteInt(0x30000);
            tableStream.WriteFixed(this.italicAngle);
            tableStream.WriteShort(this.underlinePosition);
            tableStream.WriteShort(this.underlineThickness);
            tableStream.WriteInt(this.isFixedPitch ? 1 : 0);
            tableStream.WriteInt(this.minMemType42);
            tableStream.WriteInt(this.maxMemType42);
            tableStream.WriteInt(this.minMemType1);
            tableStream.WriteInt(this.maxMemType1);
        }

        public float ItalicAngle =>
            this.italicAngle;

        public short UnderlinePosition =>
            this.underlinePosition;

        public short UnderlineThickness =>
            this.underlineThickness;

        public bool IsFixedPitch =>
            this.isFixedPitch;

        public int MinMemType42 =>
            this.minMemType42;

        public int MaxMemType42 =>
            this.maxMemType42;

        public int MinMemType1 =>
            this.minMemType1;

        public int MaxMemType1 =>
            this.maxMemType1;

        public IList<string> GlyphNames =>
            this.glyphNames;
    }
}

