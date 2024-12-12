﻿namespace DevExpress.Office.NumberConverters
{
    using System;

    public class Hebrew1Converter : OrdinalBasedNumberConverter
    {
        private static string[] hebrew;

        static Hebrew1Converter()
        {
            string[] textArray1 = new string[0x188];
            textArray1[0] = "א";
            textArray1[1] = "ב";
            textArray1[2] = "ג";
            textArray1[3] = "ד";
            textArray1[4] = "ה";
            textArray1[5] = "ו";
            textArray1[6] = "ז";
            textArray1[7] = "ח";
            textArray1[8] = "ט";
            textArray1[9] = "י";
            textArray1[10] = "יא";
            textArray1[11] = "יב";
            textArray1[12] = "יג";
            textArray1[13] = "יד";
            textArray1[14] = "טו";
            textArray1[15] = "טז";
            textArray1[0x10] = "יז";
            textArray1[0x11] = "יח";
            textArray1[0x12] = "יט";
            textArray1[0x13] = "כ";
            textArray1[20] = "כא";
            textArray1[0x15] = "כב";
            textArray1[0x16] = "כג";
            textArray1[0x17] = "כד";
            textArray1[0x18] = "כה";
            textArray1[0x19] = "כו";
            textArray1[0x1a] = "כז";
            textArray1[0x1b] = "כח";
            textArray1[0x1c] = "כט";
            textArray1[0x1d] = "ל";
            textArray1[30] = "לא";
            textArray1[0x1f] = "לב";
            textArray1[0x20] = "לג";
            textArray1[0x21] = "לד";
            textArray1[0x22] = "לה";
            textArray1[0x23] = "לו";
            textArray1[0x24] = "לז";
            textArray1[0x25] = "לח";
            textArray1[0x26] = "לט";
            textArray1[0x27] = "מ";
            textArray1[40] = "מא";
            textArray1[0x29] = "מב";
            textArray1[0x2a] = "מג";
            textArray1[0x2b] = "מד";
            textArray1[0x2c] = "מה";
            textArray1[0x2d] = "מו";
            textArray1[0x2e] = "מז";
            textArray1[0x2f] = "מח";
            textArray1[0x30] = "מט";
            textArray1[0x31] = "נ";
            textArray1[50] = "נא";
            textArray1[0x33] = "נב";
            textArray1[0x34] = "נג";
            textArray1[0x35] = "נד";
            textArray1[0x36] = "נה";
            textArray1[0x37] = "נו";
            textArray1[0x38] = "נז";
            textArray1[0x39] = "נח";
            textArray1[0x3a] = "נט";
            textArray1[0x3b] = "ס";
            textArray1[60] = "סא";
            textArray1[0x3d] = "סב";
            textArray1[0x3e] = "סג";
            textArray1[0x3f] = "סד";
            textArray1[0x40] = "סה";
            textArray1[0x41] = "סו";
            textArray1[0x42] = "סז";
            textArray1[0x43] = "סח";
            textArray1[0x44] = "סט";
            textArray1[0x45] = "ע";
            textArray1[70] = "עא";
            textArray1[0x47] = "עב";
            textArray1[0x48] = "עג";
            textArray1[0x49] = "עד";
            textArray1[0x4a] = "עה";
            textArray1[0x4b] = "עו";
            textArray1[0x4c] = "עז";
            textArray1[0x4d] = "עח";
            textArray1[0x4e] = "עט";
            textArray1[0x4f] = "פ";
            textArray1[80] = "פא";
            textArray1[0x51] = "פב";
            textArray1[0x52] = "פג";
            textArray1[0x53] = "פד";
            textArray1[0x54] = "פה";
            textArray1[0x55] = "פו";
            textArray1[0x56] = "פז";
            textArray1[0x57] = "פח";
            textArray1[0x58] = "פט";
            textArray1[0x59] = "צ";
            textArray1[90] = "צא";
            textArray1[0x5b] = "צב";
            textArray1[0x5c] = "צג";
            textArray1[0x5d] = "צד";
            textArray1[0x5e] = "צה";
            textArray1[0x5f] = "צו";
            textArray1[0x60] = "צז";
            textArray1[0x61] = "צח";
            textArray1[0x62] = "צט";
            textArray1[0x63] = "ק";
            textArray1[100] = "קא";
            textArray1[0x65] = "קב";
            textArray1[0x66] = "קג";
            textArray1[0x67] = "קד";
            textArray1[0x68] = "קה";
            textArray1[0x69] = "קו";
            textArray1[0x6a] = "קז";
            textArray1[0x6b] = "קח";
            textArray1[0x6c] = "קט";
            textArray1[0x6d] = "קי";
            textArray1[110] = "קיא";
            textArray1[0x6f] = "קיב";
            textArray1[0x70] = "קיג";
            textArray1[0x71] = "קיד";
            textArray1[0x72] = "קטו";
            textArray1[0x73] = "קטז";
            textArray1[0x74] = "קיז";
            textArray1[0x75] = "קיח";
            textArray1[0x76] = "קיט";
            textArray1[0x77] = "קכ";
            textArray1[120] = "קכא";
            textArray1[0x79] = "קכב";
            textArray1[0x7a] = "קכג";
            textArray1[0x7b] = "קכד";
            textArray1[0x7c] = "קכה";
            textArray1[0x7d] = "קכו";
            textArray1[0x7e] = "קכז";
            textArray1[0x7f] = "קכח";
            textArray1[0x80] = "קכט";
            textArray1[0x81] = "קל";
            textArray1[130] = "קלא";
            textArray1[0x83] = "קלב";
            textArray1[0x84] = "קלג";
            textArray1[0x85] = "קלד";
            textArray1[0x86] = "קלה";
            textArray1[0x87] = "קלו";
            textArray1[0x88] = "קלז";
            textArray1[0x89] = "קלח";
            textArray1[0x8a] = "קלט";
            textArray1[0x8b] = "קמ";
            textArray1[140] = "קמא";
            textArray1[0x8d] = "קמב";
            textArray1[0x8e] = "קמג";
            textArray1[0x8f] = "קמד";
            textArray1[0x90] = "קמה";
            textArray1[0x91] = "קמו";
            textArray1[0x92] = "קמז";
            textArray1[0x93] = "קמח";
            textArray1[0x94] = "קמט";
            textArray1[0x95] = "קנ";
            textArray1[150] = "קנא";
            textArray1[0x97] = "קנב";
            textArray1[0x98] = "קנג";
            textArray1[0x99] = "קנד";
            textArray1[0x9a] = "קנה";
            textArray1[0x9b] = "קנו";
            textArray1[0x9c] = "קנז";
            textArray1[0x9d] = "קנח";
            textArray1[0x9e] = "קנט";
            textArray1[0x9f] = "קס";
            textArray1[160] = "קסא";
            textArray1[0xa1] = "קסב";
            textArray1[0xa2] = "קסג";
            textArray1[0xa3] = "קסד";
            textArray1[0xa4] = "קסה";
            textArray1[0xa5] = "קסו";
            textArray1[0xa6] = "קסז";
            textArray1[0xa7] = "קסח";
            textArray1[0xa8] = "קסט";
            textArray1[0xa9] = "קע";
            textArray1[170] = "קעא";
            textArray1[0xab] = "קעב";
            textArray1[0xac] = "קעג";
            textArray1[0xad] = "קעד";
            textArray1[0xae] = "קעה";
            textArray1[0xaf] = "קעו";
            textArray1[0xb0] = "קעז";
            textArray1[0xb1] = "קעח";
            textArray1[0xb2] = "קעט";
            textArray1[0xb3] = "קפ";
            textArray1[180] = "קפא";
            textArray1[0xb5] = "קפב";
            textArray1[0xb6] = "קפג";
            textArray1[0xb7] = "קפד";
            textArray1[0xb8] = "קפה";
            textArray1[0xb9] = "קפו";
            textArray1[0xba] = "קפז";
            textArray1[0xbb] = "קפח";
            textArray1[0xbc] = "קפט";
            textArray1[0xbd] = "קצ";
            textArray1[190] = "קצא";
            textArray1[0xbf] = "קצב";
            textArray1[0xc0] = "קצג";
            textArray1[0xc1] = "קצד";
            textArray1[0xc2] = "קצה";
            textArray1[0xc3] = "קצו";
            textArray1[0xc4] = "קצז";
            textArray1[0xc5] = "קצח";
            textArray1[0xc6] = "קצט";
            textArray1[0xc7] = "ר";
            textArray1[200] = "רא";
            textArray1[0xc9] = "רב";
            textArray1[0xca] = "רג";
            textArray1[0xcb] = "רד";
            textArray1[0xcc] = "רה";
            textArray1[0xcd] = "רו";
            textArray1[0xce] = "רז";
            textArray1[0xcf] = "רח";
            textArray1[0xd0] = "רט";
            textArray1[0xd1] = "רי";
            textArray1[210] = "ריא";
            textArray1[0xd3] = "ריב";
            textArray1[0xd4] = "ריג";
            textArray1[0xd5] = "ריד";
            textArray1[0xd6] = "רטו";
            textArray1[0xd7] = "רטז";
            textArray1[0xd8] = "ריז";
            textArray1[0xd9] = "ריח";
            textArray1[0xda] = "ריט";
            textArray1[0xdb] = "רכ";
            textArray1[220] = "רכא";
            textArray1[0xdd] = "רכב";
            textArray1[0xde] = "רכג";
            textArray1[0xdf] = "רכד";
            textArray1[0xe0] = "רכה";
            textArray1[0xe1] = "רכו";
            textArray1[0xe2] = "רכז";
            textArray1[0xe3] = "רכח";
            textArray1[0xe4] = "רכט";
            textArray1[0xe5] = "רל";
            textArray1[230] = "רלא";
            textArray1[0xe7] = "רלב";
            textArray1[0xe8] = "רלג";
            textArray1[0xe9] = "רלד";
            textArray1[0xea] = "רלה";
            textArray1[0xeb] = "רלו";
            textArray1[0xec] = "רלז";
            textArray1[0xed] = "רלח";
            textArray1[0xee] = "רלט";
            textArray1[0xef] = "רמ";
            textArray1[240] = "רמא";
            textArray1[0xf1] = "רמב";
            textArray1[0xf2] = "רמג";
            textArray1[0xf3] = "רמד";
            textArray1[0xf4] = "רמה";
            textArray1[0xf5] = "רמו";
            textArray1[0xf6] = "רמז";
            textArray1[0xf7] = "רמח";
            textArray1[0xf8] = "רמט";
            textArray1[0xf9] = "רנ";
            textArray1[250] = "רנא";
            textArray1[0xfb] = "רנב";
            textArray1[0xfc] = "רנג";
            textArray1[0xfd] = "רנד";
            textArray1[0xfe] = "רנה";
            textArray1[0xff] = "רנו";
            textArray1[0x100] = "רנז";
            textArray1[0x101] = "רנח";
            textArray1[0x102] = "רנט";
            textArray1[0x103] = "רס";
            textArray1[260] = "רסא";
            textArray1[0x105] = "רסב";
            textArray1[0x106] = "רסג";
            textArray1[0x107] = "רסד";
            textArray1[0x108] = "רסה";
            textArray1[0x109] = "רסו";
            textArray1[0x10a] = "רסז";
            textArray1[0x10b] = "רסח";
            textArray1[0x10c] = "רסט";
            textArray1[0x10d] = "רע";
            textArray1[270] = "רעא";
            textArray1[0x10f] = "רעב";
            textArray1[0x110] = "רעג";
            textArray1[0x111] = "רעד";
            textArray1[0x112] = "רעה";
            textArray1[0x113] = "רעו";
            textArray1[0x114] = "רעז";
            textArray1[0x115] = "רעח";
            textArray1[0x116] = "רעט";
            textArray1[0x117] = "רפ";
            textArray1[280] = "רפא";
            textArray1[0x119] = "רפב";
            textArray1[0x11a] = "רפג";
            textArray1[0x11b] = "רפד";
            textArray1[0x11c] = "רפה";
            textArray1[0x11d] = "רפו";
            textArray1[0x11e] = "רפז";
            textArray1[0x11f] = "רפח";
            textArray1[0x120] = "רפט";
            textArray1[0x121] = "רצ";
            textArray1[290] = "רצא";
            textArray1[0x123] = "רצב";
            textArray1[0x124] = "רצג";
            textArray1[0x125] = "רצד";
            textArray1[0x126] = "רצה";
            textArray1[0x127] = "רצו";
            textArray1[0x128] = "רצז";
            textArray1[0x129] = "רצח";
            textArray1[0x12a] = "רצט";
            textArray1[0x12b] = "ש";
            textArray1[300] = "שא";
            textArray1[0x12d] = "שב";
            textArray1[0x12e] = "שג";
            textArray1[0x12f] = "שד";
            textArray1[0x130] = "שה";
            textArray1[0x131] = "שו";
            textArray1[0x132] = "שז";
            textArray1[0x133] = "שח";
            textArray1[0x134] = "שט";
            textArray1[0x135] = "שי";
            textArray1[310] = "שיא";
            textArray1[0x137] = "שיב";
            textArray1[0x138] = "שיג";
            textArray1[0x139] = "שיד";
            textArray1[0x13a] = "שטו";
            textArray1[0x13b] = "שטז";
            textArray1[0x13c] = "שיז";
            textArray1[0x13d] = "שיח";
            textArray1[0x13e] = "שיט";
            textArray1[0x13f] = "שכ";
            textArray1[320] = "שכא";
            textArray1[0x141] = "שכב";
            textArray1[0x142] = "שכג";
            textArray1[0x143] = "שכד";
            textArray1[0x144] = "שכה";
            textArray1[0x145] = "שכו";
            textArray1[0x146] = "שכז";
            textArray1[0x147] = "שכח";
            textArray1[0x148] = "שכט";
            textArray1[0x149] = "של";
            textArray1[330] = "שלא";
            textArray1[0x14b] = "שלב";
            textArray1[0x14c] = "שלג";
            textArray1[0x14d] = "שלד";
            textArray1[0x14e] = "שלה";
            textArray1[0x14f] = "שלו";
            textArray1[0x150] = "שלז";
            textArray1[0x151] = "שלח";
            textArray1[0x152] = "שלט";
            textArray1[0x153] = "שמ";
            textArray1[340] = "שמא";
            textArray1[0x155] = "שמב";
            textArray1[0x156] = "שמג";
            textArray1[0x157] = "שמד";
            textArray1[0x158] = "שמה";
            textArray1[0x159] = "שמו";
            textArray1[0x15a] = "שמז";
            textArray1[0x15b] = "שמח";
            textArray1[0x15c] = "שמט";
            textArray1[0x15d] = "שנ";
            textArray1[350] = "שנא";
            textArray1[0x15f] = "שנב";
            textArray1[0x160] = "שנג";
            textArray1[0x161] = "שנד";
            textArray1[0x162] = "שנה";
            textArray1[0x163] = "שנו";
            textArray1[0x164] = "שנז";
            textArray1[0x165] = "שנח";
            textArray1[0x166] = "שנט";
            textArray1[0x167] = "שס";
            textArray1[360] = "שסא";
            textArray1[0x169] = "שסב";
            textArray1[0x16a] = "שסג";
            textArray1[0x16b] = "שסד";
            textArray1[0x16c] = "שסה";
            textArray1[0x16d] = "שסו";
            textArray1[0x16e] = "שסז";
            textArray1[0x16f] = "שסח";
            textArray1[0x170] = "שסט";
            textArray1[0x171] = "שע";
            textArray1[370] = "שעא";
            textArray1[0x173] = "שעב";
            textArray1[0x174] = "שעג";
            textArray1[0x175] = "שעד";
            textArray1[0x176] = "שעה";
            textArray1[0x177] = "שעו";
            textArray1[0x178] = "שעז";
            textArray1[0x179] = "שעח";
            textArray1[0x17a] = "שעט";
            textArray1[0x17b] = "שפ";
            textArray1[380] = "שפא";
            textArray1[0x17d] = "שפב";
            textArray1[0x17e] = "שפג";
            textArray1[0x17f] = "שפד";
            textArray1[0x180] = "שפה";
            textArray1[0x181] = "שפו";
            textArray1[0x182] = "שפז";
            textArray1[0x183] = "שפח";
            textArray1[0x184] = "שפט";
            textArray1[0x185] = "שצ";
            textArray1[390] = "שצא";
            textArray1[0x187] = "שצב";
            hebrew = textArray1;
        }

        public override string ConvertNumberCore(long value)
        {
            if (value == 0)
            {
                return string.Empty;
            }
            value = (value - 1L) % 0x188L;
            return hebrew[(int) ((IntPtr) value)];
        }

        protected internal override NumberingFormat Type =>
            NumberingFormat.Hebrew1;
    }
}

