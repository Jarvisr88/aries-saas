namespace DevExpress.Xpf.Docking.VisualElements
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    [ValueConversion(typeof(Dock), typeof(System.Windows.Thickness)), Obsolete]
    public class DockTypeToThicknessRotateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double bottom;
            double left;
            double top;
            double right;
            switch (((Dock) value))
            {
                case Dock.Left:
                    bottom = this.Thickness.Bottom;
                    left = this.Thickness.Left;
                    top = this.Thickness.Top;
                    right = this.Thickness.Right;
                    break;

                case Dock.Top:
                    bottom = this.Thickness.Left;
                    left = this.Thickness.Bottom;
                    top = this.Thickness.Right;
                    right = this.Thickness.Top;
                    break;

                case Dock.Right:
                    bottom = this.Thickness.Top;
                    left = this.Thickness.Left;
                    top = this.Thickness.Bottom;
                    right = this.Thickness.Right;
                    break;

                default:
                    bottom = this.Thickness.Left;
                    left = this.Thickness.Top;
                    top = this.Thickness.Right;
                    right = this.Thickness.Bottom;
                    break;
            }
            return new System.Windows.Thickness(bottom, left, top, right);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            this.Inverted ? Dock.Right : Dock.Left;

        public System.Windows.Thickness Thickness { get; set; }

        public bool Inverted { get; set; }
    }
}

