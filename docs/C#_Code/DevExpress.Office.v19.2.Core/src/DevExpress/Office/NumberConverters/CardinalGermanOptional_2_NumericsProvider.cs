namespace DevExpress.Office.NumberConverters
{
    using System;

    public class CardinalGermanOptional_2_NumericsProvider : INumericsProvider
    {
        private static string[] separator = new string[] { "", "", "und" };
        private static string[] hundreds;

        static CardinalGermanOptional_2_NumericsProvider()
        {
            string[] textArray2 = new string[9];
            textArray2[0] = "einhundert";
            textArray2[1] = "zweihundert";
            textArray2[2] = "dreihundert";
            textArray2[3] = "vierhundert";
            textArray2[4] = "f\x00fcnfhundert";
            textArray2[5] = "sechshundert";
            textArray2[6] = "siebenhundert";
            textArray2[7] = "achthundert";
            textArray2[8] = "neunhundert";
            hundreds = textArray2;
        }

        public string[] Separator =>
            separator;

        public string[] SinglesNumeral =>
            CardinalGermanOptional_1_NumericsProvider.generalSingles;

        public string[] Singles =>
            CardinalGermanOptional_1_NumericsProvider.generalSingles;

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

