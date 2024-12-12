namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public class NumericToVisibilityConverter : IValueConverter
    {
        private bool hiddenInsteadOfCollapsed;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            ConverterHelper.BooleanToVisibility(ConverterHelper.NumericToBoolean(value, this.Inverse), this.HiddenInsteadOfCollapsed);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            null;

        public bool Inverse { get; set; }

        public bool HiddenInsteadOfCollapsed
        {
            get => 
                this.hiddenInsteadOfCollapsed;
            set => 
                this.hiddenInsteadOfCollapsed = value;
        }
    }
}

