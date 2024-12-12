namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalGermanOptionalNumericsProvider : INumericsProvider
    {
        private static string[] hundreds;

        static OrdinalGermanOptionalNumericsProvider()
        {
            string[] textArray1 = new string[9];
            textArray1[0] = "einhundertste";
            textArray1[1] = "zweihundertste";
            textArray1[2] = "dreihundertste";
            textArray1[3] = "vierhundertste";
            textArray1[4] = "f\x00fcnfhundertste";
            textArray1[5] = "sechshundertste";
            textArray1[6] = "siebenhundertste";
            textArray1[7] = "achthundertste";
            textArray1[8] = "neunhundertste";
            hundreds = textArray1;
        }

        public string[] Separator =>
            OrdinalGermanNumericsProvider.separator;

        public string[] SinglesNumeral =>
            OrdinalGermanNumericsProvider.generalSingles;

        public string[] Singles =>
            OrdinalGermanNumericsProvider.generalSingles;

        public string[] Teens =>
            OrdinalGermanNumericsProvider.teens;

        public string[] Tenths =>
            OrdinalGermanNumericsProvider.tenths;

        public string[] Hundreds =>
            hundreds;

        public string[] Thousands =>
            OrdinalGermanNumericsProvider.thousands;

        public string[] Million =>
            OrdinalGermanNumericsProvider.million;

        public string[] Billion =>
            OrdinalGermanNumericsProvider.billion;

        public string[] Trillion =>
            OrdinalGermanNumericsProvider.trillion;

        public string[] Quadrillion =>
            OrdinalGermanNumericsProvider.quadrillion;

        public string[] Quintillion =>
            OrdinalGermanNumericsProvider.quintillion;
    }
}

