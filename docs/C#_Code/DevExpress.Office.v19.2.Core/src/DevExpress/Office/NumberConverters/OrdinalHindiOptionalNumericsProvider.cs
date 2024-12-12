namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalHindiOptionalNumericsProvider : INumericsProvider
    {
        private static string[] generalSingles;

        static OrdinalHindiOptionalNumericsProvider()
        {
            string[] textArray1 = new string[0x63];
            textArray1[0] = "एकवाँ";
            textArray1[1] = "दोवाँ";
            textArray1[2] = "तीनवाँ";
            textArray1[3] = "चारवाँ";
            textArray1[4] = "पांचवाँ";
            textArray1[5] = "छठवाँ";
            textArray1[6] = "सातवाँ";
            textArray1[7] = "आठवाँ";
            textArray1[8] = "नौवाँ";
            textArray1[9] = "दसवाँ";
            textArray1[10] = "ग्यारहवाँ";
            textArray1[11] = "बारहवाँ";
            textArray1[12] = "तेरहवाँ";
            textArray1[13] = "चौदहवाँ";
            textArray1[14] = "पंद्रहवाँ";
            textArray1[15] = "सोलहवाँ";
            textArray1[0x10] = "सत्रहवाँ";
            textArray1[0x11] = "अट्ठारहवाँ";
            textArray1[0x12] = "उन्नीसवाँ";
            textArray1[0x13] = "बीसवाँ";
            textArray1[20] = "इक्कीसवाँ";
            textArray1[0x15] = "बाईसवाँ";
            textArray1[0x16] = "तेईसवाँ";
            textArray1[0x17] = "चौबीसवाँ";
            textArray1[0x18] = "पच्चीसवाँ";
            textArray1[0x19] = "छब्बीसवाँ";
            textArray1[0x1a] = "सत्ताईसवाँ";
            textArray1[0x1b] = "अट्ठाईसवाँ";
            textArray1[0x1c] = "उनतीसवाँ";
            textArray1[0x1d] = "तीसवाँ";
            textArray1[30] = "इकतीसवाँ";
            textArray1[0x1f] = "बत्तीसवाँ";
            textArray1[0x20] = "तैंतीसवाँ";
            textArray1[0x21] = "चौंतीसवाँ";
            textArray1[0x22] = "पैंतीसवाँ";
            textArray1[0x23] = "छत्तीसवाँ";
            textArray1[0x24] = "सैंतीसवाँ";
            textArray1[0x25] = "अड़तीसवाँ";
            textArray1[0x26] = "उनतालीसवाँ";
            textArray1[0x27] = "चालीसवाँ";
            textArray1[40] = "इकतालीसवाँ";
            textArray1[0x29] = "बयालीसवाँ";
            textArray1[0x2a] = "तैंतालीसवाँ";
            textArray1[0x2b] = "चौवालीसवाँ";
            textArray1[0x2c] = "पैंतालीसवाँ";
            textArray1[0x2d] = "छियालीसवाँ";
            textArray1[0x2e] = "सैंतालीसवाँ";
            textArray1[0x2f] = "अड़तालीसवाँ";
            textArray1[0x30] = "उनचासवाँ";
            textArray1[0x31] = "पचासवाँ";
            textArray1[50] = "इक्यावनवाँ";
            textArray1[0x33] = "बावनवाँ";
            textArray1[0x34] = "तिरेपनवाँ";
            textArray1[0x35] = "चौवनवाँ";
            textArray1[0x36] = "पचपनवाँ";
            textArray1[0x37] = "छप्पनवाँ";
            textArray1[0x38] = "सत्तावनवाँ";
            textArray1[0x39] = "अट्ठावनवाँ";
            textArray1[0x3a] = "उनसठवाँ";
            textArray1[0x3b] = "साठवाँ";
            textArray1[60] = "इकसठवाँ";
            textArray1[0x3d] = "बासठवाँ";
            textArray1[0x3e] = "तिरेसठवाँ";
            textArray1[0x3f] = "चौंसठवाँ";
            textArray1[0x40] = "पैंसठवाँ";
            textArray1[0x41] = "छियासठवाँ";
            textArray1[0x42] = "सड़सठवाँ";
            textArray1[0x43] = "अड़सठवाँ";
            textArray1[0x44] = "उनहत्तरवाँ";
            textArray1[0x45] = "सत्तरवाँ";
            textArray1[70] = "इकहत्तरवाँ";
            textArray1[0x47] = "बहत्तरवाँ";
            textArray1[0x48] = "तिहत्तरवाँ";
            textArray1[0x49] = "चौहत्तरवाँ";
            textArray1[0x4a] = "पचहत्तरवाँ";
            textArray1[0x4b] = "छिहत्तरवाँ";
            textArray1[0x4c] = "सतहत्तरवाँ";
            textArray1[0x4d] = "अठहत्तरवाँ";
            textArray1[0x4e] = "उनासीवाँ";
            textArray1[0x4f] = "अस्सीवाँ";
            textArray1[80] = "इक्यासीवाँ";
            textArray1[0x51] = "बयासीवाँ";
            textArray1[0x52] = "तिरासीवाँ";
            textArray1[0x53] = "चौरासीवाँ";
            textArray1[0x54] = "पचासीवाँ";
            textArray1[0x55] = "छियासीवाँ";
            textArray1[0x56] = "सतासीवाँ";
            textArray1[0x57] = "अठासीवाँ";
            textArray1[0x58] = "नवासीवाँ";
            textArray1[0x59] = "नब्बेवाँ";
            textArray1[90] = "इक्यानवेवाँ";
            textArray1[0x5b] = "बानवेवाँ";
            textArray1[0x5c] = "तिरानवेवाँ";
            textArray1[0x5d] = "चौरानवेवाँ";
            textArray1[0x5e] = "पचानवेवाँ";
            textArray1[0x5f] = "छियानवेवाँ";
            textArray1[0x60] = "सत्तानवेवाँ";
            textArray1[0x61] = "अट्ठानवेवाँ";
            textArray1[0x62] = "निन्यानवेवाँ";
            generalSingles = textArray1;
        }

        public string[] Separator =>
            null;

        public string[] SinglesNumeral =>
            null;

        public string[] Singles =>
            generalSingles;

        public string[] Teens =>
            null;

        public string[] Tenths =>
            null;

        public string[] Hundreds =>
            null;

        public string[] Thousands =>
            null;

        public string[] Million =>
            null;

        public string[] Billion =>
            null;

        public string[] Trillion =>
            null;

        public string[] Quadrillion =>
            null;

        public string[] Quintillion =>
            null;
    }
}

