namespace DevExpress.Office.NumberConverters
{
    using System;

    public class CardinalThaiNumericsProvider : INumericsProvider
    {
        internal static string[] separator = new string[] { "", "" };
        internal static string[] generalSingles;
        internal static string[] teens;
        internal static string[] tenths;
        internal static string[] hundreds;
        private static string[] thousands;
        internal static string[] million;
        internal static string[] billion;
        internal static string[] trillion;
        internal static string[] quadrillion;
        internal static string[] quintillion;

        static CardinalThaiNumericsProvider()
        {
            string[] textArray2 = new string[11];
            textArray2[0] = "หนึ่ง";
            textArray2[1] = "สอง";
            textArray2[2] = "สาม";
            textArray2[3] = "สี่";
            textArray2[4] = "ห้า";
            textArray2[5] = "หก";
            textArray2[6] = "เจ็ด";
            textArray2[7] = "แปด";
            textArray2[8] = "เก้า";
            textArray2[9] = "ศูนย์";
            textArray2[10] = "เอ็ด";
            generalSingles = textArray2;
            string[] textArray3 = new string[10];
            textArray3[0] = "สิบ";
            textArray3[1] = "สิบเอ็ด";
            textArray3[2] = "สิบสอง";
            textArray3[3] = "สิบสาม";
            textArray3[4] = "สิบสี่";
            textArray3[5] = "สิบห้า";
            textArray3[6] = "สิบหก";
            textArray3[7] = "สิบเจ็ด";
            textArray3[8] = "สิบแปด";
            textArray3[9] = "สิบเก้า";
            teens = textArray3;
            tenths = new string[] { "ยี่สิบ", "สามสิบ", "สี่สิบ", "ห้าสิบ", "หกสิบ", "เจ็ดสิบ", "แปดสิบ", "เก้าสิบ" };
            string[] textArray5 = new string[9];
            textArray5[0] = "หนึ่งร้อย";
            textArray5[1] = "สองร้อย";
            textArray5[2] = "สามร้อย";
            textArray5[3] = "สี่ร้อย";
            textArray5[4] = "ห้าร้อย";
            textArray5[5] = "หกร้อย";
            textArray5[6] = "เจ็ดร้อย";
            textArray5[7] = "แปดร้อย";
            textArray5[8] = "เก้าร้อย";
            hundreds = textArray5;
            thousands = new string[] { "พัน" };
            million = new string[] { "ล้าน" };
            billion = new string[] { "พันล้าน" };
            trillion = new string[] { "ล้านล้าน" };
            quadrillion = new string[] { "พันล้านล้าน" };
            quintillion = new string[] { "ล้านล้านล้าน" };
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

