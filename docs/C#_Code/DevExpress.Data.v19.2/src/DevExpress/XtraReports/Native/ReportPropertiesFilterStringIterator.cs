namespace DevExpress.XtraReports.Native
{
    using System;
    using System.Collections.Generic;

    public class ReportPropertiesFilterStringIterator
    {
        private const char DotChar = '.';
        private const char CircumflexChar = '^';
        private readonly IList<string> names;
        private int currentIndex;
        private string current;

        public ReportPropertiesFilterStringIterator(IList<string> names);
        private static int CountParts(string value);
        private string GetAndUpdateCurrent(int lexerNamePartsCount);
        private static int GetIndexOfDot(string value, int skipCount);
        private static int GetIndexOfDotCore(string value, int startIndex);
        public string GetNext(string lexerName);
    }
}

