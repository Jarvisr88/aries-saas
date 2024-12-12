namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalGreekOptionalNumericsProvider : INumericsProvider
    {
        private static string[] singlesNumeral;
        private static string[] teens;

        static OrdinalGreekOptionalNumericsProvider()
        {
            string[] textArray1 = new string[10];
            textArray1[0] = "";
            textArray1[1] = "δισ";
            textArray1[2] = "τρισ";
            textArray1[3] = "τετρακισ";
            textArray1[4] = "πεντακισ";
            textArray1[5] = "εξακισ";
            textArray1[6] = "επτακισ";
            textArray1[7] = "οκτακισ";
            textArray1[8] = "εννιακισ";
            textArray1[9] = "";
            singlesNumeral = textArray1;
            string[] textArray2 = new string[10];
            textArray2[0] = "δεκακισ";
            textArray2[1] = "ενδεκακισ";
            textArray2[2] = "δωδεκακισ";
            textArray2[3] = "δεκατριακισ";
            textArray2[4] = "δεκατετρακισ";
            textArray2[5] = "δεκαπεντακισ";
            textArray2[6] = "δεκαεξακισ";
            textArray2[7] = "δεκαεπτακισ";
            textArray2[8] = "δεκαοκτακισ";
            textArray2[9] = "δεκαεννιακισ";
            teens = textArray2;
        }

        public string[] Separator =>
            null;

        public string[] SinglesNumeral =>
            singlesNumeral;

        public string[] Singles =>
            null;

        public string[] Teens =>
            teens;

        public string[] Tenths =>
            null;

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

