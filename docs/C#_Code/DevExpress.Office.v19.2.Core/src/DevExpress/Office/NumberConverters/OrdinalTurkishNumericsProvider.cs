namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalTurkishNumericsProvider : INumericsProvider
    {
        private static string[] generalSingles;
        private static string[] separator;
        private static string[] teens;
        private static string[] tenths;
        private static string[] hundreds;
        private static string[] thousands;
        private static string[] million;
        private static string[] billion;
        private static string[] trillion;
        private static string[] quadrillion;
        private static string[] quintillion;

        static OrdinalTurkishNumericsProvider()
        {
            string[] textArray1 = new string[10];
            textArray1[0] = "birinci";
            textArray1[1] = "ikinci";
            textArray1[2] = "\x00fc\x00e7\x00fcnc\x00fc";
            textArray1[3] = "d\x00f6rd\x00fcnc\x00fc";
            textArray1[4] = "beşinci";
            textArray1[5] = "altıncı";
            textArray1[6] = "yedinci";
            textArray1[7] = "sekizinci";
            textArray1[8] = "dokuzuncu";
            textArray1[9] = "sıfırıncı";
            generalSingles = textArray1;
            separator = new string[] { "", "" };
            string[] textArray3 = new string[10];
            textArray3[0] = "onuncu";
            textArray3[1] = "onbirinci";
            textArray3[2] = "onikinci";
            textArray3[3] = "on\x00fc\x00e7\x00fcnc\x00fc";
            textArray3[4] = "ond\x00f6rd\x00fcnc\x00fc";
            textArray3[5] = "onbeşinci";
            textArray3[6] = "onaltıncı";
            textArray3[7] = "onyedinci";
            textArray3[8] = "onsekizinci";
            textArray3[9] = "ondokuzuncu";
            teens = textArray3;
            tenths = new string[] { "yirminci", "otuzuncu", "kırkıncı", "ellinci", "altmışıncı", "yetmişinci", "sekseninci", "doksanıncı" };
            string[] textArray5 = new string[9];
            textArray5[0] = "y\x00fcz\x00fcnc\x00fc";
            textArray5[1] = "ikiy\x00fcz\x00fcnc\x00fc";
            textArray5[2] = "\x00fc\x00e7y\x00fcz\x00fcnc\x00fc";
            textArray5[3] = "d\x00f6rty\x00fcz\x00fcnc\x00fc";
            textArray5[4] = "beşy\x00fcz\x00fcnc\x00fc";
            textArray5[5] = "altıy\x00fcz\x00fcnc\x00fc";
            textArray5[6] = "yediy\x00fcz\x00fcnc\x00fc";
            textArray5[7] = "sekizy\x00fcz\x00fcnc\x00fc";
            textArray5[8] = "dokuzy\x00fcz\x00fcnc\x00fc";
            hundreds = textArray5;
            thousands = new string[] { "bininci", "bininci" };
            million = new string[] { "milyonuncu", "milyonuncu" };
            billion = new string[] { "milyarıncı", "milyarıncı" };
            trillion = new string[] { "trilyonuncu", "trilyonuncu" };
            quadrillion = new string[] { "katrilyonuncu", "katrilyonuncu" };
            quintillion = new string[] { "kentilyonuncu", "kentilyonuncu" };
        }

        public string[] Separator =>
            separator;

        public string[] SinglesNumeral =>
            generalSingles;

        public string[] Singles =>
            generalSingles;

        public string[] Teens =>
            teens;

        public string[] Tenths =>
            tenths;

        public string[] Hundreds =>
            hundreds;

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
            quintillion;
    }
}

