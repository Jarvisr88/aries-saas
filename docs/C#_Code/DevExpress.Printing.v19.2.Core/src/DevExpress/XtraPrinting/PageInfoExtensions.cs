namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Runtime.CompilerServices;

    public static class PageInfoExtensions
    {
        public static string FormatValues(this PageInfo pageInfo, IFormatProvider provider, string format, params object[] values)
        {
            string str = string.Empty;
            try
            {
                str = FormatValuesCore(provider, format, values);
            }
            catch
            {
            }
            return (!string.IsNullOrEmpty(str) ? str : FormatValuesCore(provider, pageInfo.GetDefaultStringFormat(), values));
        }

        private static string FormatValuesCore(IFormatProvider provider, string format, params object[] values) => 
            (string.IsNullOrEmpty(format) || (provider == null)) ? (!string.IsNullOrEmpty(format) ? string.Format(format, values) : string.Empty) : string.Format(provider, format, values);

        public static string GetDefaultStringFormat(this PageInfo pageInfo) => 
            (pageInfo == PageInfo.None) ? string.Empty : ((pageInfo == PageInfo.NumberOfTotal) ? "{0}/{1}" : ((pageInfo != PageInfo.DateTime) ? "{0}" : "{0:D}"));

        public static string GetText(this PageInfo pageInfo, string format, int pageNumber, int pageCount, object textValue)
        {
            string str = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(format))
                {
                    str = pageInfo.GetTextCore(format, pageNumber, pageCount, textValue);
                }
            }
            catch
            {
            }
            return (!string.IsNullOrEmpty(str) ? str : pageInfo.GetTextCore(pageInfo.GetDefaultStringFormat(), pageNumber, pageCount, textValue));
        }

        private static string GetTextCore(this PageInfo pageInfo, string format, int pageNumber, int pageCount, object textValue)
        {
            switch (pageInfo)
            {
                case PageInfo.None:
                    return format;

                case PageInfo.Number:
                    return string.Format(format, pageNumber, textValue);

                case PageInfo.NumberOfTotal:
                    return string.Format(format, pageNumber, pageCount, textValue);

                case (PageInfo.None | PageInfo.Number | PageInfo.NumberOfTotal):
                case (PageInfo.None | PageInfo.Number | PageInfo.RomLowNumber):
                case (PageInfo.None | PageInfo.NumberOfTotal | PageInfo.RomLowNumber):
                case (PageInfo.None | PageInfo.Number | PageInfo.NumberOfTotal | PageInfo.RomLowNumber):
                    break;

                case PageInfo.RomLowNumber:
                {
                    string str2 = PSConvert.ToRomanString(pageNumber).ToLower();
                    return string.Format(format, str2, textValue);
                }
                case PageInfo.RomHiNumber:
                {
                    string str = PSConvert.ToRomanString(pageNumber);
                    return string.Format(format, str, textValue);
                }
                default:
                    if (pageInfo == PageInfo.UserName)
                    {
                        return string.Format(format, PrintingSystemBase.UserName, textValue);
                    }
                    if (pageInfo != PageInfo.Total)
                    {
                        break;
                    }
                    return string.Format(format, pageCount, textValue);
            }
            return string.Empty;
        }
    }
}

