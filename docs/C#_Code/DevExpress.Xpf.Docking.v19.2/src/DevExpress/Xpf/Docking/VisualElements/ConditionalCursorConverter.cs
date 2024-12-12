namespace DevExpress.Xpf.Docking.VisualElements
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;
    using System.Windows.Input;

    [ValueConversion(typeof(bool), typeof(System.Windows.Input.Cursor))]
    public class ConditionalCursorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            ((bool) value) ? this.Cursor : Cursors.Arrow;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            value;

        public System.Windows.Input.Cursor Cursor { get; set; }
    }
}

