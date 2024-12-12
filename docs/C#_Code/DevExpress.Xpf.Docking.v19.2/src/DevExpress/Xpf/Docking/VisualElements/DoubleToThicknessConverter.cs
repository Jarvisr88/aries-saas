namespace DevExpress.Xpf.Docking.VisualElements
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    [ValueConversion(typeof(double), typeof(Thickness))]
    public class DoubleToThicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double num = (double) value;
            Thickness thickness = new Thickness(this.All ? num : 0.0);
            if (!this.All)
            {
                switch (this.Direction)
                {
                    case Dock.Left:
                        thickness.Left = num;
                        break;

                    case Dock.Top:
                        thickness.Top = num;
                        break;

                    case Dock.Right:
                        thickness.Right = num;
                        break;

                    case Dock.Bottom:
                        thickness.Bottom = num;
                        break;

                    default:
                        break;
                }
            }
            return thickness;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public bool All { get; set; }

        public Dock Direction { get; set; }
    }
}

