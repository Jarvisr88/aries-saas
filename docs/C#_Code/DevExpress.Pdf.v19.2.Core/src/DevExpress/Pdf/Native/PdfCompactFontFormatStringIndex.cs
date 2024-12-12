namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class PdfCompactFontFormatStringIndex : PdfCompactFontFormatNameIndex
    {
        public const short StandardStringsCount = 0x187;
        private static readonly string[] standardStrings;
        private static readonly Dictionary<string, short> standardSIDMapping;
        private readonly Dictionary<string, short> sidMapping;

        static PdfCompactFontFormatStringIndex()
        {
            string[] textArray1 = new string[0x187];
            textArray1[0] = ".notdef";
            textArray1[1] = "space";
            textArray1[2] = "exclam";
            textArray1[3] = "quotedbl";
            textArray1[4] = "numbersign";
            textArray1[5] = "dollar";
            textArray1[6] = "percent";
            textArray1[7] = "ampersand";
            textArray1[8] = "quoteright";
            textArray1[9] = "parenleft";
            textArray1[10] = "parenright";
            textArray1[11] = "asterisk";
            textArray1[12] = "plus";
            textArray1[13] = "comma";
            textArray1[14] = "hyphen";
            textArray1[15] = "period";
            textArray1[0x10] = "slash";
            textArray1[0x11] = "zero";
            textArray1[0x12] = "one";
            textArray1[0x13] = "two";
            textArray1[20] = "three";
            textArray1[0x15] = "four";
            textArray1[0x16] = "five";
            textArray1[0x17] = "six";
            textArray1[0x18] = "seven";
            textArray1[0x19] = "eight";
            textArray1[0x1a] = "nine";
            textArray1[0x1b] = "colon";
            textArray1[0x1c] = "semicolon";
            textArray1[0x1d] = "less";
            textArray1[30] = "equal";
            textArray1[0x1f] = "greater";
            textArray1[0x20] = "question";
            textArray1[0x21] = "at";
            textArray1[0x22] = "A";
            textArray1[0x23] = "B";
            textArray1[0x24] = "C";
            textArray1[0x25] = "D";
            textArray1[0x26] = "E";
            textArray1[0x27] = "F";
            textArray1[40] = "G";
            textArray1[0x29] = "H";
            textArray1[0x2a] = "I";
            textArray1[0x2b] = "J";
            textArray1[0x2c] = "K";
            textArray1[0x2d] = "L";
            textArray1[0x2e] = "M";
            textArray1[0x2f] = "N";
            textArray1[0x30] = "O";
            textArray1[0x31] = "P";
            textArray1[50] = "Q";
            textArray1[0x33] = "R";
            textArray1[0x34] = "S";
            textArray1[0x35] = "T";
            textArray1[0x36] = "U";
            textArray1[0x37] = "V";
            textArray1[0x38] = "W";
            textArray1[0x39] = "X";
            textArray1[0x3a] = "Y";
            textArray1[0x3b] = "Z";
            textArray1[60] = "bracketleft";
            textArray1[0x3d] = "backslash";
            textArray1[0x3e] = "bracketright";
            textArray1[0x3f] = "asciicircum";
            textArray1[0x40] = "underscore";
            textArray1[0x41] = "quoteleft";
            textArray1[0x42] = "a";
            textArray1[0x43] = "b";
            textArray1[0x44] = "c";
            textArray1[0x45] = "d";
            textArray1[70] = "e";
            textArray1[0x47] = "f";
            textArray1[0x48] = "g";
            textArray1[0x49] = "h";
            textArray1[0x4a] = "i";
            textArray1[0x4b] = "j";
            textArray1[0x4c] = "k";
            textArray1[0x4d] = "l";
            textArray1[0x4e] = "m";
            textArray1[0x4f] = "n";
            textArray1[80] = "o";
            textArray1[0x51] = "p";
            textArray1[0x52] = "q";
            textArray1[0x53] = "r";
            textArray1[0x54] = "s";
            textArray1[0x55] = "t";
            textArray1[0x56] = "u";
            textArray1[0x57] = "v";
            textArray1[0x58] = "w";
            textArray1[0x59] = "x";
            textArray1[90] = "y";
            textArray1[0x5b] = "z";
            textArray1[0x5c] = "braceleft";
            textArray1[0x5d] = "bar";
            textArray1[0x5e] = "braceright";
            textArray1[0x5f] = "asciitilde";
            textArray1[0x60] = "exclamdown";
            textArray1[0x61] = "cent";
            textArray1[0x62] = "sterling";
            textArray1[0x63] = "fraction";
            textArray1[100] = "yen";
            textArray1[0x65] = "florin";
            textArray1[0x66] = "section";
            textArray1[0x67] = "currency";
            textArray1[0x68] = "quotesingle";
            textArray1[0x69] = "quotedblleft";
            textArray1[0x6a] = "guillemotleft";
            textArray1[0x6b] = "guilsinglleft";
            textArray1[0x6c] = "guilsinglright";
            textArray1[0x6d] = "fi";
            textArray1[110] = "fl";
            textArray1[0x6f] = "endash";
            textArray1[0x70] = "dagger";
            textArray1[0x71] = "daggerdbl";
            textArray1[0x72] = "periodcentered";
            textArray1[0x73] = "paragraph";
            textArray1[0x74] = "bullet";
            textArray1[0x75] = "quotesinglbase";
            textArray1[0x76] = "quotedblbase";
            textArray1[0x77] = "quotedblright";
            textArray1[120] = "guillemotright";
            textArray1[0x79] = "ellipsis";
            textArray1[0x7a] = "perthousand";
            textArray1[0x7b] = "questiondown";
            textArray1[0x7c] = "grave";
            textArray1[0x7d] = "acute";
            textArray1[0x7e] = "circumflex";
            textArray1[0x7f] = "tilde";
            textArray1[0x80] = "macron";
            textArray1[0x81] = "breve";
            textArray1[130] = "dotaccent";
            textArray1[0x83] = "dieresis";
            textArray1[0x84] = "ring";
            textArray1[0x85] = "cedilla";
            textArray1[0x86] = "hungarumlaut";
            textArray1[0x87] = "ogonek";
            textArray1[0x88] = "caron";
            textArray1[0x89] = "emdash";
            textArray1[0x8a] = "AE";
            textArray1[0x8b] = "ordfeminine";
            textArray1[140] = "Lslash";
            textArray1[0x8d] = "Oslash";
            textArray1[0x8e] = "OE";
            textArray1[0x8f] = "ordmasculine";
            textArray1[0x90] = "ae";
            textArray1[0x91] = "dotlessi";
            textArray1[0x92] = "lslash";
            textArray1[0x93] = "oslash";
            textArray1[0x94] = "oe";
            textArray1[0x95] = "germandbls";
            textArray1[150] = "onesuperior";
            textArray1[0x97] = "logicalnot";
            textArray1[0x98] = "mu";
            textArray1[0x99] = "trademark";
            textArray1[0x9a] = "Eth";
            textArray1[0x9b] = "onehalf";
            textArray1[0x9c] = "plusminus";
            textArray1[0x9d] = "Thorn";
            textArray1[0x9e] = "onequarter";
            textArray1[0x9f] = "divide";
            textArray1[160] = "brokenbar";
            textArray1[0xa1] = "degree";
            textArray1[0xa2] = "thorn";
            textArray1[0xa3] = "threequarters";
            textArray1[0xa4] = "twosuperior";
            textArray1[0xa5] = "registered";
            textArray1[0xa6] = "minus";
            textArray1[0xa7] = "eth";
            textArray1[0xa8] = "multiply";
            textArray1[0xa9] = "threesuperior";
            textArray1[170] = "copyright";
            textArray1[0xab] = "Aacute";
            textArray1[0xac] = "Acircumflex";
            textArray1[0xad] = "Adieresis";
            textArray1[0xae] = "Agrave";
            textArray1[0xaf] = "Aring";
            textArray1[0xb0] = "Atilde";
            textArray1[0xb1] = "Ccedilla";
            textArray1[0xb2] = "Eacute";
            textArray1[0xb3] = "Ecircumflex";
            textArray1[180] = "Edieresis";
            textArray1[0xb5] = "Egrave";
            textArray1[0xb6] = "Iacute";
            textArray1[0xb7] = "Icircumflex";
            textArray1[0xb8] = "Idieresis";
            textArray1[0xb9] = "Igrave";
            textArray1[0xba] = "Ntilde";
            textArray1[0xbb] = "Oacute";
            textArray1[0xbc] = "Ocircumflex";
            textArray1[0xbd] = "Odieresis";
            textArray1[190] = "Ograve";
            textArray1[0xbf] = "Otilde";
            textArray1[0xc0] = "Scaron";
            textArray1[0xc1] = "Uacute";
            textArray1[0xc2] = "Ucircumflex";
            textArray1[0xc3] = "Udieresis";
            textArray1[0xc4] = "Ugrave";
            textArray1[0xc5] = "Yacute";
            textArray1[0xc6] = "Ydieresis";
            textArray1[0xc7] = "Zcaron";
            textArray1[200] = "aacute";
            textArray1[0xc9] = "acircumflex";
            textArray1[0xca] = "adieresis";
            textArray1[0xcb] = "agrave";
            textArray1[0xcc] = "aring";
            textArray1[0xcd] = "atilde";
            textArray1[0xce] = "ccedilla";
            textArray1[0xcf] = "eacute";
            textArray1[0xd0] = "ecircumflex";
            textArray1[0xd1] = "edieresis";
            textArray1[210] = "egrave";
            textArray1[0xd3] = "iacute";
            textArray1[0xd4] = "icircumflex";
            textArray1[0xd5] = "idieresis";
            textArray1[0xd6] = "igrave";
            textArray1[0xd7] = "ntilde";
            textArray1[0xd8] = "oacute";
            textArray1[0xd9] = "ocircumflex";
            textArray1[0xda] = "odieresis";
            textArray1[0xdb] = "ograve";
            textArray1[220] = "otilde";
            textArray1[0xdd] = "scaron";
            textArray1[0xde] = "uacute";
            textArray1[0xdf] = "ucircumflex";
            textArray1[0xe0] = "udieresis";
            textArray1[0xe1] = "ugrave";
            textArray1[0xe2] = "yacute";
            textArray1[0xe3] = "ydieresis";
            textArray1[0xe4] = "zcaron";
            textArray1[0xe5] = "exclamsmall";
            textArray1[230] = "Hungarumlautsmall";
            textArray1[0xe7] = "dollaroldstyle";
            textArray1[0xe8] = "dollarsuperior";
            textArray1[0xe9] = "ampersandsmall";
            textArray1[0xea] = "Acutesmall";
            textArray1[0xeb] = "parenleftsuperior";
            textArray1[0xec] = "parenrightsuperior";
            textArray1[0xed] = "twodotenleader";
            textArray1[0xee] = "onedotenleader";
            textArray1[0xef] = "zerooldstyle";
            textArray1[240] = "oneoldstyle";
            textArray1[0xf1] = "twooldstyle";
            textArray1[0xf2] = "threeoldstyle";
            textArray1[0xf3] = "fouroldstyle";
            textArray1[0xf4] = "fiveoldstyle";
            textArray1[0xf5] = "sixoldstyle";
            textArray1[0xf6] = "sevenoldstyle";
            textArray1[0xf7] = "eightoldstyle";
            textArray1[0xf8] = "nineoldstyle";
            textArray1[0xf9] = "commasuperior";
            textArray1[250] = "threequartersemdash";
            textArray1[0xfb] = "periodsuperior";
            textArray1[0xfc] = "questionsmall";
            textArray1[0xfd] = "asuperior";
            textArray1[0xfe] = "bsuperior";
            textArray1[0xff] = "centsuperior";
            textArray1[0x100] = "dsuperior";
            textArray1[0x101] = "esuperior";
            textArray1[0x102] = "isuperior";
            textArray1[0x103] = "lsuperior";
            textArray1[260] = "msuperior";
            textArray1[0x105] = "nsuperior";
            textArray1[0x106] = "osuperior";
            textArray1[0x107] = "rsuperior";
            textArray1[0x108] = "ssuperior";
            textArray1[0x109] = "tsuperior";
            textArray1[0x10a] = "ff";
            textArray1[0x10b] = "ffi";
            textArray1[0x10c] = "ffl";
            textArray1[0x10d] = "parenleftinferior";
            textArray1[270] = "parenrightinferior";
            textArray1[0x10f] = "Circumflexsmall";
            textArray1[0x110] = "hyphensuperior";
            textArray1[0x111] = "Gravesmall";
            textArray1[0x112] = "Asmall";
            textArray1[0x113] = "Bsmall";
            textArray1[0x114] = "Csmall";
            textArray1[0x115] = "Dsmall";
            textArray1[0x116] = "Esmall";
            textArray1[0x117] = "Fsmall";
            textArray1[280] = "Gsmall";
            textArray1[0x119] = "Hsmall";
            textArray1[0x11a] = "Ismall";
            textArray1[0x11b] = "Jsmall";
            textArray1[0x11c] = "Ksmall";
            textArray1[0x11d] = "Lsmall";
            textArray1[0x11e] = "Msmall";
            textArray1[0x11f] = "Nsmall";
            textArray1[0x120] = "Osmall";
            textArray1[0x121] = "Psmall";
            textArray1[290] = "Qsmall";
            textArray1[0x123] = "Rsmall";
            textArray1[0x124] = "Ssmall";
            textArray1[0x125] = "Tsmall";
            textArray1[0x126] = "Usmall";
            textArray1[0x127] = "Vsmall";
            textArray1[0x128] = "Wsmall";
            textArray1[0x129] = "Xsmall";
            textArray1[0x12a] = "Ysmall";
            textArray1[0x12b] = "Zsmall";
            textArray1[300] = "colonmonetary";
            textArray1[0x12d] = "onefitted";
            textArray1[0x12e] = "rupiah";
            textArray1[0x12f] = "Tildesmall";
            textArray1[0x130] = "exclamdownsmall";
            textArray1[0x131] = "centoldstyle";
            textArray1[0x132] = "Lslashsmall";
            textArray1[0x133] = "Scaronsmall";
            textArray1[0x134] = "Zcaronsmall";
            textArray1[0x135] = "Dieresissmall";
            textArray1[310] = "Brevesmall";
            textArray1[0x137] = "Caronsmall";
            textArray1[0x138] = "Dotaccentsmall";
            textArray1[0x139] = "Macronsmall";
            textArray1[0x13a] = "figuredash";
            textArray1[0x13b] = "hypheninferior";
            textArray1[0x13c] = "Ogoneksmall";
            textArray1[0x13d] = "Ringsmall";
            textArray1[0x13e] = "Cedillasmall";
            textArray1[0x13f] = "questiondownsmall";
            textArray1[320] = "oneeighth";
            textArray1[0x141] = "threeeighths";
            textArray1[0x142] = "fiveeighths";
            textArray1[0x143] = "seveneighths";
            textArray1[0x144] = "onethird";
            textArray1[0x145] = "twothirds";
            textArray1[0x146] = "zerosuperior";
            textArray1[0x147] = "foursuperior";
            textArray1[0x148] = "fivesuperior";
            textArray1[0x149] = "sixsuperior";
            textArray1[330] = "sevensuperior";
            textArray1[0x14b] = "eightsuperior";
            textArray1[0x14c] = "ninesuperior";
            textArray1[0x14d] = "zeroinferior";
            textArray1[0x14e] = "oneinferior";
            textArray1[0x14f] = "twoinferior";
            textArray1[0x150] = "threeinferior";
            textArray1[0x151] = "fourinferior";
            textArray1[0x152] = "fiveinferior";
            textArray1[0x153] = "sixinferior";
            textArray1[340] = "seveninferior";
            textArray1[0x155] = "eightinferior";
            textArray1[0x156] = "nineinferior";
            textArray1[0x157] = "centinferior";
            textArray1[0x158] = "dollarinferior";
            textArray1[0x159] = "periodinferior";
            textArray1[0x15a] = "commainferior";
            textArray1[0x15b] = "Agravesmall";
            textArray1[0x15c] = "Aacutesmall";
            textArray1[0x15d] = "Acircumflexsmall";
            textArray1[350] = "Atildesmall";
            textArray1[0x15f] = "Adieresissmall";
            textArray1[0x160] = "Aringsmall";
            textArray1[0x161] = "AEsmall";
            textArray1[0x162] = "Ccedillasmall";
            textArray1[0x163] = "Egravesmall";
            textArray1[0x164] = "Eacutesmall";
            textArray1[0x165] = "Ecircumflexsmall";
            textArray1[0x166] = "Edieresissmall";
            textArray1[0x167] = "Igravesmall";
            textArray1[360] = "Iacutesmall";
            textArray1[0x169] = "Icircumflexsmall";
            textArray1[0x16a] = "Idieresissmall";
            textArray1[0x16b] = "Ethsmall";
            textArray1[0x16c] = "Ntildesmall";
            textArray1[0x16d] = "Ogravesmall";
            textArray1[0x16e] = "Oacutesmall";
            textArray1[0x16f] = "Ocircumflexsmall";
            textArray1[0x170] = "Otildesmall";
            textArray1[0x171] = "Odieresissmall";
            textArray1[370] = "OEsmall";
            textArray1[0x173] = "Oslashsmall";
            textArray1[0x174] = "Ugravesmall";
            textArray1[0x175] = "Uacutesmall";
            textArray1[0x176] = "Ucircumflexsmall";
            textArray1[0x177] = "Udieresissmall";
            textArray1[0x178] = "Yacutesmall";
            textArray1[0x179] = "Thornsmall";
            textArray1[0x17a] = "Ydieresissmall";
            textArray1[0x17b] = "_001_000";
            textArray1[380] = "_001_001";
            textArray1[0x17d] = "_001_002";
            textArray1[0x17e] = "_001_003";
            textArray1[0x17f] = "Black";
            textArray1[0x180] = "Bold";
            textArray1[0x181] = "Book";
            textArray1[0x182] = "Light";
            textArray1[0x183] = "Medium";
            textArray1[0x184] = "Regular";
            textArray1[0x185] = "Roman";
            textArray1[390] = "Semibold";
            standardStrings = textArray1;
            standardSIDMapping = new Dictionary<string, short>();
            for (short i = 0; i < 0x187; i = (short) (i + 1))
            {
                standardSIDMapping.Add(standardStrings[i], i);
            }
        }

        public PdfCompactFontFormatStringIndex(params string[] strings) : base(strings)
        {
            this.sidMapping = new Dictionary<string, short>();
            int length = strings.Length;
            short index = 0;
            for (short i = 0x187; index < length; i = (short) (i + 1))
            {
                this.sidMapping[strings[index]] = i;
                index = (short) (index + 1);
            }
        }

        public PdfCompactFontFormatStringIndex(PdfBinaryStream stream) : base(stream)
        {
            this.sidMapping = new Dictionary<string, short>();
            string[] strings = base.Strings;
            int length = strings.Length;
            short index = 0;
            for (short i = 0x187; index < length; i = (short) (i + 1))
            {
                this.sidMapping[strings[index]] = i;
                index = (short) (index + 1);
            }
        }

        public short GetSID(string str)
        {
            short num;
            if (!standardSIDMapping.TryGetValue(str, out num) && !this.sidMapping.TryGetValue(str, out num))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return num;
        }

        public string GetString(IList<object> operands)
        {
            if (operands.Count != 1)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            object obj2 = operands[0];
            if (!(obj2 is int))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return this[(int) obj2];
        }

        public short TryGetSID(string str)
        {
            short num;
            return ((standardSIDMapping.TryGetValue(str, out num) || this.sidMapping.TryGetValue(str, out num)) ? num : 0);
        }

        public string this[int index] =>
            (index < 0x187) ? standardStrings[index] : base.Strings[index - 0x187];
    }
}

