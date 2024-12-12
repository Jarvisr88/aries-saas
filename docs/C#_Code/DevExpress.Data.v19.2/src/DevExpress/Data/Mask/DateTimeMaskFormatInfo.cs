namespace DevExpress.Data.Mask
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection;

    public class DateTimeMaskFormatInfo : IEnumerable<DateTimeMaskFormatElement>, IEnumerable
    {
        protected readonly IList<DateTimeMaskFormatElement> innerList;

        public DateTimeMaskFormatInfo(string mask, DateTimeFormatInfo dateTimeFormatInfo);
        private static string ExpandFormat(string format, DateTimeFormatInfo info);
        public string Format(DateTime formatted);
        public string Format(DateTime formatted, int startFormatIndex, int endFormatIndex);
        private static int GetGroupLength(string mask);
        private static IList<DateTimeMaskFormatElement> ParseFormatString(string mask, DateTimeFormatInfo dateTimeFormatInfo);
        public static string RemoveTimePartFromTheMask(string patchedMask, IFormatProvider formatProvider);
        IEnumerator<DateTimeMaskFormatElement> IEnumerable<DateTimeMaskFormatElement>.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator();

        public int Count { get; }

        public DateTimeMaskFormatElement this[int index] { get; }

        public DateTimePart DateTimeParts { get; }
    }
}

