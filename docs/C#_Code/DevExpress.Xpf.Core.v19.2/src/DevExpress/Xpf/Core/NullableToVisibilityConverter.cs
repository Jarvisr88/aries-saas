﻿namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class NullableToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            (value != null) ? (this.Invert ? Visibility.Collapsed : Visibility.Visible) : (this.Invert ? Visibility.Visible : Visibility.Collapsed);

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            null;

        public bool Invert { get; set; }
    }
}

