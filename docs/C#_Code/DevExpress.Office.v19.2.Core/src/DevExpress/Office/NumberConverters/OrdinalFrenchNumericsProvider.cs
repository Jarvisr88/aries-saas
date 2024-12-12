namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalFrenchNumericsProvider : INumericsProvider
    {
        private static string[] generalSingles;
        internal static string[] separator;
        internal static string[] teens;
        internal static string[] tenths;
        internal static string[] hundreds;
        internal static string[] thousands;
        internal static string[] million;
        internal static string[] billion;
        internal static string[] trillion;
        internal static string[] quadrillion;
        internal static string[] quintillion;

        static OrdinalFrenchNumericsProvider()
        {
            string[] textArray1 = new string[11];
            textArray1[0] = "uni\x00e8me";
            textArray1[1] = "deuxi\x00e8me";
            textArray1[2] = "troisi\x00e8me";
            textArray1[3] = "quatri\x00e8me";
            textArray1[4] = "cinqui\x00e8me";
            textArray1[5] = "sixi\x00e8me";
            textArray1[6] = "septi\x00e8me";
            textArray1[7] = "huiti\x00e8me";
            textArray1[8] = "neuvi\x00e8me";
            textArray1[9] = "z\x00e9ro";
            textArray1[10] = "i\x00e8me";
            generalSingles = textArray1;
            separator = new string[] { " ", "-", " et ", "" };
            string[] textArray3 = new string[10];
            textArray3[0] = "dixi\x00e8me";
            textArray3[1] = "onzi\x00e8me";
            textArray3[2] = "douzi\x00e8me";
            textArray3[3] = "treizi\x00e8me";
            textArray3[4] = "quatorzi\x00e8me";
            textArray3[5] = "quinzi\x00e8me";
            textArray3[6] = "seizi\x00e8me";
            textArray3[7] = "dix-septi\x00e8me";
            textArray3[8] = "dix-huiti\x00e8me";
            textArray3[9] = "dix-neuvi\x00e8me";
            teens = textArray3;
            tenths = new string[] { "vingti\x00e8me", "trenti\x00e8me", "quaranti\x00e8me", "cinquanti\x00e8me", "soixanti\x00e8me", "soixante", "quatre-vingts", "quatre-vingt-dix" };
            string[] textArray5 = new string[9];
            textArray5[0] = "centi\x00e8me";
            textArray5[1] = "deux centi\x00e8me";
            textArray5[2] = "trois centi\x00e8me";
            textArray5[3] = "quatre centi\x00e8me";
            textArray5[4] = "cinq centi\x00e8me";
            textArray5[5] = "six centi\x00e8me";
            textArray5[6] = "sept centi\x00e8me";
            textArray5[7] = "huit centi\x00e8me";
            textArray5[8] = "neuf centi\x00e8me";
            hundreds = textArray5;
            thousands = new string[] { "milli\x00e8me" };
            million = new string[] { "millioni\x00e8me", "millioni\x00e8me" };
            billion = new string[] { "milliardi\x00e8me", "milliardi\x00e8me" };
            trillion = new string[] { "billioni\x00e8me", "billioni\x00e8me" };
            quadrillion = new string[] { "billiardi\x00e8me", "billiardi\x00e8me" };
            quintillion = new string[] { "trillioni\x00e8me", "trillioni\x00e8me" };
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

