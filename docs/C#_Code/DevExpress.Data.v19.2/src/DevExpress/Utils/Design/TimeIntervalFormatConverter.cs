namespace DevExpress.Utils.Design
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Globalization;

    public class TimeIntervalFormatConverter : DateTimeFormatConverter
    {
        private const string DefaultTimeIntervalPattern = "{0:D} - {1:D}";
        private const string TimeIntervalStartFormatPattern = "{{0:{0}}}";
        private const string TimeIntervalEndFormatPattern = "{{1:{0}}}";
        private const string TimeIntervalFormatPattern = "{{0:{0}}} - {{1:{0}}}";

        protected virtual string BuildTimeIntervalFormat(string pattern, string formatInfo) => 
            string.Format(pattern, formatInfo);

        protected internal override StringCollection GetDateTimeFormats(ITypeDescriptorContext context)
        {
            StringCollection strings = new StringCollection();
            if (context != null)
            {
                foreach (string str in new DateTimeFormatInfo().GetAllDateTimePatterns())
                {
                    strings.Add(this.BuildTimeIntervalFormat("{{0:{0}}}", str));
                    strings.Add(this.BuildTimeIntervalFormat("{{1:{0}}}", str));
                    strings.Add(this.BuildTimeIntervalFormat("{{0:{0}}} - {{1:{0}}}", str));
                }
            }
            return strings;
        }

        protected override string DefaultString =>
            "{0:D} - {1:D}";
    }
}

