namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalSpanishOptionalNumericsProvider : INumericsProvider
    {
        private static string[] separator = new string[] { " ", " y ", "" };
        private static string[] singlesNumeral = new string[] { "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve" };
        private static string[] teens;
        private static string[] hundreds;

        static OrdinalSpanishOptionalNumericsProvider()
        {
            string[] textArray3 = new string[10];
            textArray3[0] = "diez";
            textArray3[1] = "once";
            textArray3[2] = "doce";
            textArray3[3] = "trece";
            textArray3[4] = "catorce";
            textArray3[5] = "quince";
            textArray3[6] = "diecis\x00e9is";
            textArray3[7] = "diecisiete";
            textArray3[8] = "dieciocho";
            textArray3[9] = "diecinueve";
            teens = textArray3;
            string[] textArray4 = new string[10];
            textArray4[0] = "ciento";
            textArray4[1] = "doscientos";
            textArray4[2] = "trescientos";
            textArray4[3] = "cuatrocientos";
            textArray4[4] = "quincientos";
            textArray4[5] = "seiscientos";
            textArray4[6] = "sietecientos";
            textArray4[7] = "ochocientos";
            textArray4[8] = "nuevecientos";
            textArray4[9] = "cien";
            hundreds = textArray4;
        }

        public string[] Separator =>
            separator;

        public string[] SinglesNumeral =>
            singlesNumeral;

        public string[] Singles =>
            null;

        public string[] Teens =>
            teens;

        public string[] Tenths =>
            null;

        public string[] Hundreds =>
            hundreds;

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

