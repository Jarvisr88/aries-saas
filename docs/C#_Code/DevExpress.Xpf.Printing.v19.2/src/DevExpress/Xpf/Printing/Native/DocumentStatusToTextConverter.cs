namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Printing;
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class DocumentStatusToTextConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            switch (((DocumentStatus) value))
            {
                case DocumentStatus.NoPages:
                    return PrintingLocalizer.GetString(PrintingStringId.Msg_EmptyDocument);

                case DocumentStatus.DocumentCreation:
                    return PrintingLocalizer.GetString(PrintingStringId.Msg_DocumentIsPrinting);

                case DocumentStatus.WaitingForParameters:
                    return PrintingLocalizer.GetString(PrintingStringId.Msg_Waiting_ForParameterValues);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;
    }
}

