namespace DevExpress.Mvvm.UI
{
    using DevExpress.Utils;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public class BooleanToVisibilityConverter : IValueConverter
    {
        private bool hiddenInsteadOfCollapsed;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            ConverterHelper.BooleanToVisibility(ConverterHelper.GetBooleanValue(value) ^ this.Inverse, this.HiddenInsteadOfCollapsed);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = ((value is Visibility) && (((Visibility) value) == Visibility.Visible)) ^ this.Inverse;
            return (!(targetType == typeof(DefaultBoolean)) ? ((object) flag) : ((object) ConverterHelper.ToDefaultBoolean(new bool?(flag))));
        }

        public bool Inverse { get; set; }

        [Obsolete("Use the HiddenInsteadOfCollapsed property instead."), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool HiddenInsteadCollapsed
        {
            get => 
                this.hiddenInsteadOfCollapsed;
            set => 
                this.hiddenInsteadOfCollapsed = value;
        }

        public bool HiddenInsteadOfCollapsed
        {
            get => 
                this.hiddenInsteadOfCollapsed;
            set => 
                this.hiddenInsteadOfCollapsed = value;
        }
    }
}

