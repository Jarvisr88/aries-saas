namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Text;

    public class PdfDate : PdfLiteralString
    {
        private readonly System.DateTime dateTime;

        public PdfDate(System.DateTime dateTime) : base(GetStringValue(dateTime))
        {
            this.dateTime = System.DateTime.MinValue;
            this.dateTime = dateTime;
        }

        private static void AppendGMTOffset(StringBuilder sb, System.DateTime dateTime)
        {
            TimeSpan utcOffset = TimeZoneInfo.Local.GetUtcOffset(dateTime);
            if (utcOffset.Ticks == 0)
            {
                sb.Append("Z");
            }
            else
            {
                if (utcOffset.Ticks > 0L)
                {
                    sb.Append("+");
                }
                sb.Append(utcOffset.Hours.ToString("D2"));
                sb.Append("'");
                sb.Append(utcOffset.Minutes.ToString("D2"));
                sb.Append("'");
            }
        }

        private static string GetStringValue(System.DateTime dateTime)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("D:");
            sb.AppendFormat("{0:D4}", dateTime.Year);
            sb.AppendFormat("{0:D2}", dateTime.Month);
            sb.AppendFormat("{0:D2}", dateTime.Day);
            sb.AppendFormat("{0:D2}", dateTime.Hour);
            sb.AppendFormat("{0:D2}", dateTime.Minute);
            sb.AppendFormat("{0:D2}", dateTime.Second);
            AppendGMTOffset(sb, dateTime);
            return sb.ToString();
        }

        public System.DateTime DateTime =>
            this.dateTime;
    }
}

