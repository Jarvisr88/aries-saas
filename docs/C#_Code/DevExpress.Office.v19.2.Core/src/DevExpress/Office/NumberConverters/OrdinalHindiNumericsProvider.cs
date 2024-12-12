namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalHindiNumericsProvider : INumericsProvider
    {
        private static string[] separator = new string[] { " " };
        private static string[] singleNumeral = new string[] { "शून्यवाँ" };
        private static string[] generalSingles;
        private static string[] hundreads;
        private static string[] thousands;
        private static string[] million;
        private static string[] billion;
        private static string[] trillion;
        private static string[] quadrillion;

        static OrdinalHindiNumericsProvider()
        {
            string[] textArray3 = new string[0x63];
            textArray3[0] = "पहला";
            textArray3[1] = "दूसरा";
            textArray3[2] = "तीसरा";
            textArray3[3] = "चौथा";
            textArray3[4] = "पांचवाँ";
            textArray3[5] = "छठा";
            textArray3[6] = "सातवाँ";
            textArray3[7] = "आठवाँ";
            textArray3[8] = "नौवाँ";
            textArray3[9] = "दसवाँ";
            textArray3[10] = "ग्यारहवाँ";
            textArray3[11] = "बारहवाँ";
            textArray3[12] = "तेरहवाँ";
            textArray3[13] = "चौदहवाँ";
            textArray3[14] = "पंद्रहवाँ";
            textArray3[15] = "सोलहवाँ";
            textArray3[0x10] = "सत्रहवाँ";
            textArray3[0x11] = "अट्ठारहवाँ";
            textArray3[0x12] = "उन्नीसवाँ";
            textArray3[0x13] = "बीसवाँ";
            textArray3[20] = "इक्कीसवाँ";
            textArray3[0x15] = "बाईसवाँ";
            textArray3[0x16] = "तेईसवाँ";
            textArray3[0x17] = "चौबीसवाँ";
            textArray3[0x18] = "पच्चीसवाँ";
            textArray3[0x19] = "छब्बीसवाँ";
            textArray3[0x1a] = "सत्ताईसवाँ";
            textArray3[0x1b] = "अट्ठाईसवाँ";
            textArray3[0x1c] = "उनतीसवाँ";
            textArray3[0x1d] = "तीसवाँ";
            textArray3[30] = "इकतीसवाँ";
            textArray3[0x1f] = "बत्तीसवाँ";
            textArray3[0x20] = "तैंतीसवाँ";
            textArray3[0x21] = "चौंतीसवाँ";
            textArray3[0x22] = "पैंतीसवाँ";
            textArray3[0x23] = "छत्तीसवाँ";
            textArray3[0x24] = "सैंतीसवाँ";
            textArray3[0x25] = "अड़तीसवाँ";
            textArray3[0x26] = "उनतालीसवाँ";
            textArray3[0x27] = "चालीसवाँ";
            textArray3[40] = "इकतालीसवाँ";
            textArray3[0x29] = "बयालीसवाँ";
            textArray3[0x2a] = "तैंतालीसवाँ";
            textArray3[0x2b] = "चौवालीसवाँ";
            textArray3[0x2c] = "पैंतालीसवाँ";
            textArray3[0x2d] = "छियालीसवाँ";
            textArray3[0x2e] = "सैंतालीसवाँ";
            textArray3[0x2f] = "अड़तालीसवाँ";
            textArray3[0x30] = "उनचासवाँ";
            textArray3[0x31] = "पचासवाँ";
            textArray3[50] = "इक्यावनवाँ";
            textArray3[0x33] = "बावनवाँ";
            textArray3[0x34] = "तिरेपनवाँ";
            textArray3[0x35] = "चौवनवाँ";
            textArray3[0x36] = "पचपनवाँ";
            textArray3[0x37] = "छप्पनवाँ";
            textArray3[0x38] = "सत्तावनवाँ";
            textArray3[0x39] = "अट्ठावनवाँ";
            textArray3[0x3a] = "उनसठवाँ";
            textArray3[0x3b] = "साठवाँ";
            textArray3[60] = "इकसठवाँ";
            textArray3[0x3d] = "बासठवाँ";
            textArray3[0x3e] = "तिरेसठवाँ";
            textArray3[0x3f] = "चौंसठवाँ";
            textArray3[0x40] = "पैंसठवाँ";
            textArray3[0x41] = "छियासठवाँ";
            textArray3[0x42] = "सड़सठवाँ";
            textArray3[0x43] = "अड़सठवाँ";
            textArray3[0x44] = "उनहत्तरवाँ";
            textArray3[0x45] = "सत्तरवाँ";
            textArray3[70] = "इकहत्तरवाँ";
            textArray3[0x47] = "बहत्तरवाँ";
            textArray3[0x48] = "तिहत्तरवाँ";
            textArray3[0x49] = "चौहत्तरवाँ";
            textArray3[0x4a] = "पचहत्तरवाँ";
            textArray3[0x4b] = "छिहत्तरवाँ";
            textArray3[0x4c] = "सतहत्तरवाँ";
            textArray3[0x4d] = "अठहत्तरवाँ";
            textArray3[0x4e] = "उनासीवाँ";
            textArray3[0x4f] = "अस्सीवाँ";
            textArray3[80] = "इक्यासीवाँ";
            textArray3[0x51] = "बयासीवाँ";
            textArray3[0x52] = "तिरासीवाँ";
            textArray3[0x53] = "चौरासीवाँ";
            textArray3[0x54] = "पचासीवाँ";
            textArray3[0x55] = "छियासीवाँ";
            textArray3[0x56] = "सतासीवाँ";
            textArray3[0x57] = "अठासीवाँ";
            textArray3[0x58] = "नवासीवाँ";
            textArray3[0x59] = "नब्बेवाँ";
            textArray3[90] = "इक्यानवेवाँ";
            textArray3[0x5b] = "बानवेवाँ";
            textArray3[0x5c] = "तिरानवेवाँ";
            textArray3[0x5d] = "चौरानवेवाँ";
            textArray3[0x5e] = "पचानवेवाँ";
            textArray3[0x5f] = "छियानवेवाँ";
            textArray3[0x60] = "सत्तानवेवाँ";
            textArray3[0x61] = "अट्ठानवेवाँ";
            textArray3[0x62] = "निन्यानवेवाँ";
            generalSingles = textArray3;
            hundreads = new string[] { "सौवाँ", "लाखवाँ" };
            thousands = new string[] { "हज़ारवाँ" };
            million = new string[] { "करोड़वाँ" };
            billion = new string[] { "अरबवाँ", "खरबवाँ" };
            trillion = new string[] { "नीलवाँ" };
            quadrillion = new string[] { "पद्मवाँ", "शंखवाँ" };
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

