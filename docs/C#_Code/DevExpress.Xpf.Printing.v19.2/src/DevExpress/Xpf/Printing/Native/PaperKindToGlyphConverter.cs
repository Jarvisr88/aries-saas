namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class PaperKindToGlyphConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            string str;
            PaperKind? nullable = value as PaperKind?;
            if (nullable != null)
            {
                str = "pack://application:,,,/{0};component/Images/PaperKind_{1}.svg";
                PaperKind kind = nullable.Value;
                switch (kind)
                {
                    case PaperKind.Letter:
                    case PaperKind.Tabloid:
                    case PaperKind.Legal:
                    case PaperKind.Statement:
                    case PaperKind.Executive:
                    case PaperKind.A3:
                    case PaperKind.A4:
                    case PaperKind.A5:
                    case PaperKind.B4:
                    case PaperKind.B5:
                        goto TR_0001;

                    case PaperKind.LetterSmall:
                    case PaperKind.Ledger:
                    case PaperKind.A4Small:
                        break;

                    default:
                        if (kind != PaperKind.A6)
                        {
                            break;
                        }
                        goto TR_0001;
                }
            }
            return null;
        TR_0001:
            SvgImageSourceExtension extension1 = new SvgImageSourceExtension();
            extension1.Uri = new Uri(string.Format(str, "DevExpress.Xpf.Printing.v19.2", nullable.Value));
            return extension1.ProvideValue(null);
        }

        object IValueConverter.ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

