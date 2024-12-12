namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class ProgressTypeToEditSettingsConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            (((ProgressType) value) == ProgressType.Marquee) ? ((object) new ProgressBarMarqueeStyleSettings()) : ((object) new ProgressBarStyleSettings());

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;
    }
}

