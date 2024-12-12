namespace DevExpress.Pdf
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class PdfAcroFormValueFormat
    {
        public static PdfAcroFormValueFormat CreateDateTimeFormat(string format)
        {
            PdfAcroFormValueFormat format1 = new PdfAcroFormValueFormat();
            format1.FormatScript = $"AFDate_FormatEx("{format}")";
            format1.KeystrokeScript = $"AFDate_KeystrokeEx("{format}")";
            return format1;
        }

        public static PdfAcroFormValueFormat CreateNumberFormat(int decimalPlaces, PdfAcroFormNumberSeparatorStyle separatorStyle) => 
            CreateNumberFormat(decimalPlaces, separatorStyle, "", PdfAcroFormCurrencyStyle.After, PdfAcroFormNegativeNumberStyle.None);

        public static PdfAcroFormValueFormat CreateNumberFormat(int decimalPlaces, PdfAcroFormNumberSeparatorStyle separatorStyle, string currencySymbol, PdfAcroFormCurrencyStyle currencyStyle, PdfAcroFormNegativeNumberStyle negativeNumberStyle)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char ch in currencySymbol ?? "")
            {
                uint num2 = ch;
                builder.Append($"\u{num2.ToString("x4")}");
            }
            string str = "false";
            switch (currencyStyle)
            {
                case PdfAcroFormCurrencyStyle.None:
                    builder.Clear();
                    break;

                case PdfAcroFormCurrencyStyle.Before:
                    str = "true";
                    break;

                case PdfAcroFormCurrencyStyle.BeforeWithSpace:
                    str = "true";
                    builder.Append(" ");
                    break;

                case PdfAcroFormCurrencyStyle.AfterWithSpace:
                    builder.Insert(0, " ");
                    break;

                default:
                    break;
            }
            string str2 = builder.ToString();
            return new PdfAcroFormValueFormat { 
                FormatScript = $"AFNumber_Format({decimalPlaces}, {(int) separatorStyle}, {(int) negativeNumberStyle}, 0, "{str2}", {str})",
                KeystrokeScript = $"AFNumber_Keystroke({decimalPlaces}, {(int) separatorStyle}, {(int) negativeNumberStyle}, 0, "{str2}", {str})"
            };
        }

        public static PdfAcroFormValueFormat CreatePercentFormat(int decimalPlaces, PdfAcroFormNumberSeparatorStyle separatorStyle)
        {
            PdfAcroFormValueFormat format1 = new PdfAcroFormValueFormat();
            format1.FormatScript = $"AFPercent_Format({decimalPlaces}, {(int) separatorStyle}, false)";
            format1.KeystrokeScript = $"AFTime_Keystroke({decimalPlaces}, {(int) separatorStyle}, false)";
            return format1;
        }

        public static PdfAcroFormValueFormat CreateSpecialFormat(PdfAcroFormSpecialFormatType format)
        {
            PdfAcroFormValueFormat format1 = new PdfAcroFormValueFormat();
            format1.FormatScript = $"AFSpecial_Format({(int) format})";
            format1.KeystrokeScript = $"AFSpecial_Keystroke({(int) format})";
            return format1;
        }

        public static PdfAcroFormValueFormat CreateSpecialFormat(string formatMask)
        {
            PdfAcroFormValueFormat format1 = new PdfAcroFormValueFormat();
            format1.KeystrokeScript = $"AFSpecial_KeystrokeEx("{formatMask}")";
            return format1;
        }

        public static PdfAcroFormValueFormat CreateTimeFormat(string format)
        {
            PdfAcroFormValueFormat format1 = new PdfAcroFormValueFormat();
            format1.FormatScript = $"AFTime_FormatEx("{format}")";
            format1.KeystrokeScript = $"AFTime_Keystroke("{format}")";
            return format1;
        }

        public string KeystrokeScript { get; set; }

        public string FormatScript { get; set; }

        public string ValidateScript { get; set; }

        public string CalculateScript { get; set; }
    }
}

