namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalGreekNumericsProvider : INumericsProvider
    {
        private static string[] separator = new string[] { " ", " ", "" };
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

        static OrdinalGreekNumericsProvider()
        {
            string[] textArray2 = new string[10];
            textArray2[0] = "πρώτο";
            textArray2[1] = "δεύτερο";
            textArray2[2] = "τρίτο";
            textArray2[3] = "τέταρτο";
            textArray2[4] = "πέμπτο";
            textArray2[5] = "έκτο";
            textArray2[6] = "έβδομο";
            textArray2[7] = "όγδοο";
            textArray2[8] = "ένατο";
            textArray2[9] = "μηδενικό";
            generalSingles = textArray2;
            string[] textArray3 = new string[10];
            textArray3[0] = "δέκατο";
            textArray3[1] = "ενδέκατο";
            textArray3[2] = "δωδέκατο";
            textArray3[3] = "δέκατο τρίτο";
            textArray3[4] = "δέκατο τέταρτο";
            textArray3[5] = "δέκατο πέμπτο";
            textArray3[6] = "δέκατο έκτο";
            textArray3[7] = "δέκατο έβδομο";
            textArray3[8] = "δέκατο όγδοο";
            textArray3[9] = "δέκατο ένατο";
            teens = textArray3;
            tenths = new string[] { "εικοστό", "τριακοστό", "τεσσαρακοστό", "πεντηκοστό", "εξηκοστό", "εβδομηκοστό", "ογδοηκοστό", "ενενηκοστό" };
            string[] textArray5 = new string[9];
            textArray5[0] = "εκατοστό";
            textArray5[1] = "διακοσιοστό";
            textArray5[2] = "τριακοσιοστό";
            textArray5[3] = "τετρακοσιοστό";
            textArray5[4] = "πεντακοσιοστό";
            textArray5[5] = "εξακοσιοστό";
            textArray5[6] = "επτακοσιοστό";
            textArray5[7] = "οκτακοσιοστό";
            textArray5[8] = "εννιακοσιοστό";
            hundreds = textArray5;
            thousands = new string[] { "χιλιοστό" };
            million = new string[] { "εκατομμυριοστό" };
            billion = new string[] { "δισεκατομμυριοστό" };
            trillion = new string[] { "τρισεκατομμυριοστό" };
            quadrillion = new string[] { "τετράκις εκατομμυριοστό" };
            quintillion = new string[] { "πεντάκις εκατομμυριοστό" };
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

