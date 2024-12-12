namespace DevExpress.Xpf.Printing.Parameters
{
    using DevExpress.Xpf.Printing;
    using System;
    using System.Collections.Generic;

    public static class BooleanValuesHelper
    {
        private static IEnumerable<KeyValuePair<string, bool>> booleanValues;

        public static string TrueString =>
            PrintingLocalizer.GetString(PrintingStringId.True);

        public static string FalseString =>
            PrintingLocalizer.GetString(PrintingStringId.False);

        public static IEnumerable<KeyValuePair<string, bool>> BooleanValues
        {
            get
            {
                IEnumerable<KeyValuePair<string, bool>> booleanValues = BooleanValuesHelper.booleanValues;
                if (BooleanValuesHelper.booleanValues == null)
                {
                    IEnumerable<KeyValuePair<string, bool>> local1 = BooleanValuesHelper.booleanValues;
                    KeyValuePair<string, bool>[] pairArray1 = new KeyValuePair<string, bool>[] { new KeyValuePair<string, bool>(TrueString, true), new KeyValuePair<string, bool>(FalseString, false) };
                    booleanValues = BooleanValuesHelper.booleanValues = pairArray1;
                }
                return booleanValues;
            }
        }
    }
}

