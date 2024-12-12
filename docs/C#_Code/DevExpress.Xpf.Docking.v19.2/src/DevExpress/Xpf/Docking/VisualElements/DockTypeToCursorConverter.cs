namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;

    [ValueConversion(typeof(Dock), typeof(Cursor))]
    public class DockTypeToCursorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            ((Dock) value).ToCursor();

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            Dock.Left;

        public int All { get; set; }

        public bool Inverted { get; set; }
    }
}

