namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public class EditableValueConverter : IValueConverter
    {
        public object Convert(object editValue) => 
            this.Convert(editValue, editValue?.GetType(), null, CultureInfo.CurrentCulture);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            (this.Converter == null) ? value : this.Converter.Convert(value, targetType, parameter, culture);

        public object ConvertBack(object editValue) => 
            this.ConvertBack(editValue, editValue?.GetType(), null, CultureInfo.CurrentCulture);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            (this.Converter == null) ? value : this.Converter.ConvertBack(value, targetType, parameter, culture);

        public IValueConverter Converter { get; set; }
    }
}

