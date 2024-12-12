namespace DevExpress.XtraExport
{
    using DevExpress.Export;
    using DevExpress.XtraPrinting;
    using System;
    using System.Globalization;

    public static class ExportUtils
    {
        private static string dateTimeFormat;

        public static bool AllowNewExcelExportEx(IDataAwareExportOptions options, ExportTarget target)
        {
            bool flag = (options != null) ? (options.ExportType == ExportType.DataAware) : (ExportSettings.DefaultExportType == ExportType.DataAware);
            return (((target == ExportTarget.Xls) || ((target == ExportTarget.Xlsx) || (target == ExportTarget.Csv))) ? flag : false);
        }

        private static string GetDateTimeFormat(DateTimeFormatInfo formatInfo) => 
            GetDateTimeFormat(formatInfo, formatInfo.TimeSeparator);

        private static string GetDateTimeFormat(DateTimeFormatInfo formatInfo, string timeSeparator)
        {
            string str = formatInfo.ShortDatePattern.ToLower() + string.Format(" h{0}mm{0}ss", timeSeparator);
            if (!string.IsNullOrEmpty(formatInfo.AMDesignator))
            {
                str = str + " AM/PM";
            }
            return (str + " [${0}{1:00}" + timeSeparator + "{2:00}]");
        }

        public static string GetDateTimeFormatString(TimeSpan timeSpan) => 
            string.Format(DateTimeFormat, ToSign(timeSpan.Ticks), timeSpan.Hours, timeSpan.Minutes);

        public static bool IsDoubleValue(object data) => 
            (data is float) || ((data is double) || (data is decimal));

        public static bool IsIntegerValue(object data) => 
            (data is short) || ((data is ushort) || ((data is int) || ((data is uint) || ((data is long) || ((data is ulong) || ((data is sbyte) || (data is byte)))))));

        public static double ToOADate(DateTime value)
        {
            try
            {
                return value.ToOADate();
            }
            catch (OverflowException)
            {
                DateTime time = new DateTime();
                return time.ToOADate();
            }
        }

        private static string ToSign(long value) => 
            (value >= 0L) ? "+" : "-";

        private static string DateTimeFormat
        {
            get
            {
                if (string.IsNullOrEmpty(dateTimeFormat))
                {
                    dateTimeFormat = GetDateTimeFormat(DateTimeFormatInfo.CurrentInfo);
                }
                return dateTimeFormat;
            }
        }

        public static DateTime TimeSpanStartDate =>
            new DateTime(0x76b, 12, 30);
    }
}

