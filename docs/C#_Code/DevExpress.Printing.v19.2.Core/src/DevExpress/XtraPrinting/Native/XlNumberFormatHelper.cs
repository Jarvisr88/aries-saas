namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraExport.Csv;
    using System;

    public static class XlNumberFormatHelper
    {
        public static bool IsValidXlsxFormatString(string xlsxFormatString)
        {
            try
            {
                return (string.IsNullOrEmpty(xlsxFormatString) || (new XlNumFmtParser().Parse(xlsxFormatString) != null));
            }
            catch
            {
                return false;
            }
        }
    }
}

