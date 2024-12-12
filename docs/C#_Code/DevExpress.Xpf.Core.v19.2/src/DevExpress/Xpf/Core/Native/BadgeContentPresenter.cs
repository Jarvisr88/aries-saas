namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class BadgeContentPresenter : ContentPresenter
    {
        private static readonly Func<DataTemplate> get_UIElementContentTemplate;
        private static DataTemplate uiElementContentTemplate;
        public static readonly DependencyProperty ContentFormatProviderProperty;
        private DataTemplate formattedStringTemplate;

        static BadgeContentPresenter();
        protected override DataTemplate ChooseTemplate();
        private DataTemplate GetFormattedStringTemplate();
        private void OnContentFormatProviderChanged(IFormatProvider oldValue, IFormatProvider newValue);
        protected override void OnContentStringFormatChanged(string oldContentStringFormat, string newContentStringFormat);
        private DataTemplate SelectTemplate(object item);

        public IFormatProvider ContentFormatProvider { get; set; }

        private DataTemplate FormattedStringTemplate { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BadgeContentPresenter.<>c <>9;

            static <>c();
            internal void <.cctor>b__17_0(DependencyObject d, DependencyPropertyChangedEventArgs args);
        }

        private class FormatterConverter : IValueConverter
        {
            private readonly string stringFormat;
            private readonly IFormatProvider formatProvider;

            public FormatterConverter(string stringFormat, IFormatProvider formatProvider);
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture);
            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
        }
    }
}

