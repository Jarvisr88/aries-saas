namespace DevExpress.Office.NumberConverters
{
    using System;

    public class CardinalHindiNumericsProvider : INumericsProvider
    {
        private static string[] separator = new string[] { " " };
        private static string[] singleNumeral = new string[] { "शून्य" };
        private static string[] generalSingles;
        private static string[] hundreads;
        private static string[] thousands;
        private static string[] million;
        private static string[] billion;
        private static string[] trillion;
        private static string[] quadrillion;

        static CardinalHindiNumericsProvider()
        {
            string[] textArray3 = new string[0x63];
            textArray3[0] = "एक";
            textArray3[1] = "दो";
            textArray3[2] = "तीन";
            textArray3[3] = "चार";
            textArray3[4] = "पांच";
            textArray3[5] = "छः";
            textArray3[6] = "सात";
            textArray3[7] = "आठ";
            textArray3[8] = "नौ";
            textArray3[9] = "दस";
            textArray3[10] = "ग्यारह";
            textArray3[11] = "बारह";
            textArray3[12] = "तेरह";
            textArray3[13] = "चौदह";
            textArray3[14] = "पंद्रह";
            textArray3[15] = "सोलह";
            textArray3[0x10] = "सत्रह";
            textArray3[0x11] = "अट्ठारह";
            textArray3[0x12] = "उन्नीस";
            textArray3[0x13] = "बीस";
            textArray3[20] = "इक्कीस";
            textArray3[0x15] = "बाईस";
            textArray3[0x16] = "तेईस";
            textArray3[0x17] = "चौबीस";
            textArray3[0x18] = "पच्चीस";
            textArray3[0x19] = "छब्बीस";
            textArray3[0x1a] = "सत्ताईस";
            textArray3[0x1b] = "अट्ठाईस";
            textArray3[0x1c] = "उनतीस";
            textArray3[0x1d] = "तीस";
            textArray3[30] = "इकतीस";
            textArray3[0x1f] = "बत्तीस";
            textArray3[0x20] = "तैंतीस";
            textArray3[0x21] = "चौंतीस";
            textArray3[0x22] = "पैंतीस";
            textArray3[0x23] = "छत्तीस";
            textArray3[0x24] = "सैंतीस";
            textArray3[0x25] = "अड़तीस";
            textArray3[0x26] = "उनतालीस";
            textArray3[0x27] = "चालीस";
            textArray3[40] = "इकतालीस";
            textArray3[0x29] = "बयालीस";
            textArray3[0x2a] = "तैंतालीस";
            textArray3[0x2b] = "चौवालीस";
            textArray3[0x2c] = "पैंतालीस";
            textArray3[0x2d] = "छियालीस";
            textArray3[0x2e] = "सैंतालीस";
            textArray3[0x2f] = "अड़तालीस";
            textArray3[0x30] = "उनचास";
            textArray3[0x31] = "पचास";
            textArray3[50] = "इक्यावन";
            textArray3[0x33] = "बावन";
            textArray3[0x34] = "तिरेपन";
            textArray3[0x35] = "चौवन";
            textArray3[0x36] = "पचपन";
            textArray3[0x37] = "छप्पन";
            textArray3[0x38] = "सत्तावन";
            textArray3[0x39] = "अट्ठावन";
            textArray3[0x3a] = "उनसठ";
            textArray3[0x3b] = "साठ";
            textArray3[60] = "इकसठ";
            textArray3[0x3d] = "बासठ";
            textArray3[0x3e] = "तिरेसठ";
            textArray3[0x3f] = "चौंसठ";
            textArray3[0x40] = "पैंसठ";
            textArray3[0x41] = "छियासठ";
            textArray3[0x42] = "सड़सठ";
            textArray3[0x43] = "अड़सठ";
            textArray3[0x44] = "उनहत्तर";
            textArray3[0x45] = "सत्तर";
            textArray3[70] = "इकहत्तर";
            textArray3[0x47] = "बहत्तर";
            textArray3[0x48] = "तिहत्तर";
            textArray3[0x49] = "चौहत्तर";
            textArray3[0x4a] = "पचहत्तर";
            textArray3[0x4b] = "छिहत्तर";
            textArray3[0x4c] = "सतहत्तर";
            textArray3[0x4d] = "अठहत्तर";
            textArray3[0x4e] = "उनासी";
            textArray3[0x4f] = "अस्सी";
            textArray3[80] = "इक्यासी";
            textArray3[0x51] = "बयासी";
            textArray3[0x52] = "तिरासी";
            textArray3[0x53] = "चौरासी";
            textArray3[0x54] = "पचासी";
            textArray3[0x55] = "छियासी";
            textArray3[0x56] = "सतासी";
            textArray3[0x57] = "अठासी";
            textArray3[0x58] = "नवासी";
            textArray3[0x59] = "नब्बे";
            textArray3[90] = "इक्यानवे";
            textArray3[0x5b] = "बानवे";
            textArray3[0x5c] = "तिरानवे";
            textArray3[0x5d] = "चौरानवे";
            textArray3[0x5e] = "पचानवे";
            textArray3[0x5f] = "छियानवे";
            textArray3[0x60] = "सत्तानवे";
            textArray3[0x61] = "अट्ठानवे";
            textArray3[0x62] = "निन्यानवे";
            generalSingles = textArray3;
            hundreads = new string[] { "सौ", "लाख" };
            thousands = new string[] { "हज़ार" };
            million = new string[] { "करोड़" };
            billion = new string[] { "अरब", "खरब" };
            trillion = new string[] { "नील" };
            quadrillion = new string[] { "पद्म", "शंख" };
        }

        public string[] Separator =>
            separator;

        public string[] SinglesNumeral =>
            singleNumeral;

        public string[] Singles =>
            generalSingles;

        public string[] Teens =>
            null;

        public string[] Tenths =>
            null;

        public string[] Hundreds =>
            hundreads;

        public string[] Thousands =>
            thousands;

        public string[] Million =>
            million;

        public string[] Billion =>
            billion;

        public string[] Trillion =>
            trillion;

        public string[] Quadrillion =>
            quadrillion;

        public string[] Quintillion =>
            null;
    }
}

