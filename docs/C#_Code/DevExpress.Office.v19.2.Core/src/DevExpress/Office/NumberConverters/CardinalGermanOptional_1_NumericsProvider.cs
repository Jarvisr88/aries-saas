namespace DevExpress.Office.NumberConverters
{
    using System;

    public class CardinalGermanOptional_1_NumericsProvider : INumericsProvider
    {
        internal static string[] generalSingles;
        private static string[] separator;
        private static string[] hundreds;

        static CardinalGermanOptional_1_NumericsProvider()
        {
            string[] textArray1 = new string[10];
            textArray1[0] = "eins";
            textArray1[1] = "zwei";
            textArray1[2] = "drei";
            textArray1[3] = "vier";
            textArray1[4] = "f\x00fcnf";
            textArray1[5] = "sechs";
            textArray1[6] = "sieben";
            textArray1[7] = "acht";
            textArray1[8] = "neun";
            textArray1[9] = "null";
            generalSingles = textArray1;
            separator = new string[] { "", "", "und" };
            string[] textArray3 = new string[9];
            textArray3[0] = "hundert";
            textArray3[1] = "zweihundert";
            textArray3[2] = "dreihundert";
            textArray3[3] = "vierhundert";
            textArray3[4] = "f\x00fcnfhundert";
            textArray3[5] = "sechshundert";
            textArray3[6] = "siebenhundert";
            textArray3[7] = "achthundert";
            textArray3[8] = "neunhundert";
            hundreds = textArray3;
        }

        public string[] Separator =>
            separator;

        public string[] SinglesNumeral =>
            generalSingles;

        public string[] Singles =>
            generalSingles;

        public string[] Teens =>
            CardinalGermanNumericsProvider.teens;

        public string[] Tenths =>
            CardinalGermanNumericsProvider.tenths;

        public string[] Hundreds =>
            hundreds;

        public string[] Thousands =>
            CardinalGermanNumericsProvider.thousands;

        public string[] Million =>
            CardinalGermanNumericsProvider.million;

        public string[] Billion =>
            CardinalGermanNumericsProvider.billion;

        public string[] Trillion =>
            CardinalGermanNumericsProvider.trillion;

        public string[] Quadrillion =>
            CardinalGermanNumericsProvider.quadrillion;

        public string[] Quintillion =>
            CardinalGermanNumericsProvider.quintillion;
    }
}

