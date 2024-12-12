namespace DevExpress.Office.NumberConverters
{
    using System;

    public class CardinalSpanishNumericsProvider : INumericsProvider
    {
        private static string[] separator = new string[] { " ", " y ", "" };
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

        static CardinalSpanishNumericsProvider()
        {
            string[] textArray2 = new string[9];
            textArray2[0] = "un";
            textArray2[1] = "dos";
            textArray2[2] = "tres";
            textArray2[3] = "cuatro";
            textArray2[4] = "cinco";
            textArray2[5] = "seis";
            textArray2[6] = "siete";
            textArray2[7] = "ocho";
            textArray2[8] = "nueve";
            singlesNumeral = textArray2;
            string[] textArray3 = new string[10];
            textArray3[0] = "uno";
            textArray3[1] = "dos";
            textArray3[2] = "tres";
            textArray3[3] = "cuatro";
            textArray3[4] = "cinco";
            textArray3[5] = "seis";
            textArray3[6] = "siete";
            textArray3[7] = "ocho";
            textArray3[8] = "nueve";
            textArray3[9] = "cero";
            generalSingles = textArray3;
            string[] textArray4 = new string[10];
            textArray4[0] = "diez";
            textArray4[1] = "once";
            textArray4[2] = "doce";
            textArray4[3] = "trece";
            textArray4[4] = "catorce";
            textArray4[5] = "quince";
            textArray4[6] = "diecis\x00e9is";
            textArray4[7] = "diecisiete";
            textArray4[8] = "dieciocho";
            textArray4[9] = "diecinueve";
            teens = textArray4;
            tenths = new string[] { "veinte", "treinta", "cuarenta", "cincuenta", "sesenta", "setenta", "ochenta", "noventa" };
            string[] textArray6 = new string[10];
            textArray6[0] = "ciento";
            textArray6[1] = "doscientos";
            textArray6[2] = "trescientos";
            textArray6[3] = "cuatrocientos";
            textArray6[4] = "quinientos";
            textArray6[5] = "seiscientos";
            textArray6[6] = "setecientos";
            textArray6[7] = "ochocientos";
            textArray6[8] = "novecientos";
            textArray6[9] = "cien";
            hundreds = textArray6;
            thousands = new string[] { "mil" };
            million = new string[] { "mill\x00f3n", "millones" };
            billion = new string[] { "millardo", "millardos" };
            trillion = new string[] { "bill\x00f3n", "bill\x00f3nes" };
            quadrillion = new string[] { "billardo", "billardos" };
            quintillion = new string[] { "trill\x00f3n", "trill\x00f3n" };
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

