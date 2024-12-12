namespace DevExpress.Office.NumberConverters
{
    using System;

    public class CardinalSpanishOptionalNumericsProvider : INumericsProvider
    {
        internal static string[] tenths;

        static CardinalSpanishOptionalNumericsProvider()
        {
            string[] textArray1 = new string[9];
            textArray1[0] = "veintiuno";
            textArray1[1] = "veintid\x00f3s";
            textArray1[2] = "veintitr\x00e9s";
            textArray1[3] = "veinticuatro";
            textArray1[4] = "veinticinco";
            textArray1[5] = "veintis\x00e9is";
            textArray1[6] = "veintisiete";
            textArray1[7] = "veintiocho";
            textArray1[8] = "veintinueve";
            tenths = textArray1;
        }

        public string[] Separator =>
            null;

        public string[] SinglesNumeral =>
            null;

        public string[] Singles =>
            null;

        public string[] Teens =>
            null;

        public string[] Tenths =>
            tenths;

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

