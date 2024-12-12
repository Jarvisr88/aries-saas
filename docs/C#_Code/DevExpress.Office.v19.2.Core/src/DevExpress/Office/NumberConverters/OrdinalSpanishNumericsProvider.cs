namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalSpanishNumericsProvider : INumericsProvider
    {
        private static string[] separator = new string[] { " ", " ", "" };
        private static string[] singlesNumeral;
        private static string[] generalSingles;
        private static string[] teens;
        private static string[] tenths;
        private static string[] hundreds;
        private static string[] thousands;
        private static string[] million;
        private static string[] billion;
        private static string[] trillion;
        private static string[] quadrillion;
        private static string[] quintillion;

        static OrdinalSpanishNumericsProvider()
        {
            string[] textArray2 = new string[10];
            textArray2[0] = "";
            textArray2[1] = "dos";
            textArray2[2] = "tres";
            textArray2[3] = "cuatro";
            textArray2[4] = "cinco";
            textArray2[5] = "seis";
            textArray2[6] = "siete";
            textArray2[7] = "ocho";
            textArray2[8] = "nueve";
            textArray2[9] = "un";
            singlesNumeral = textArray2;
            string[] textArray3 = new string[10];
            textArray3[0] = "primero";
            textArray3[1] = "segundo";
            textArray3[2] = "tercero";
            textArray3[3] = "cuarto";
            textArray3[4] = "quinto";
            textArray3[5] = "sexto";
            textArray3[6] = "s\x00e9ptimo";
            textArray3[7] = "octavo";
            textArray3[8] = "noveno";
            textArray3[9] = "cero";
            generalSingles = textArray3;
            string[] textArray4 = new string[10];
            textArray4[0] = "d\x00e9cimo";
            textArray4[1] = "und\x00e9cimo";
            textArray4[2] = "duod\x00e9cimo";
            textArray4[3] = "decimotercero";
            textArray4[4] = "decimocuarto";
            textArray4[5] = "decimoquinto";
            textArray4[6] = "decimosexto";
            textArray4[7] = "decimos\x00e9ptimo";
            textArray4[8] = "decimoctavo";
            textArray4[9] = "decimonoveno";
            teens = textArray4;
            tenths = new string[] { "vig\x00e9simo", "trig\x00e9simo", "cuadrag\x00e9simo", "quincuag\x00e9simo", "sexag\x00e9simo", "septuag\x00e9simo", "octog\x00e9simo", "nonag\x00e9simo" };
            string[] textArray6 = new string[9];
            textArray6[0] = "cent\x00e9simo";
            textArray6[1] = "ducent\x00e9simo";
            textArray6[2] = "tricent\x00e9simo";
            textArray6[3] = "cuadringent\x00e9simo";
            textArray6[4] = "quingent\x00e9simo";
            textArray6[5] = "sexcent\x00e9simo";
            textArray6[6] = "septingent\x00e9simo";
            textArray6[7] = "octingent\x00e9simo";
            textArray6[8] = "noningent\x00e9simo";
            hundreds = textArray6;
            thousands = new string[] { "mil\x00e9simo" };
            million = new string[] { "millon\x00e9simo" };
            billion = new string[] { "millard\x00e9simo" };
            trillion = new string[] { "billon\x00e9simo" };
            quadrillion = new string[] { "billard\x00e9simo" };
            quintillion = new string[] { "trill\x00f3n\x00e9simo" };
        }

        public string[] Separator =>
            separator;

        public string[] SinglesNumeral =>
            singlesNumeral;

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

