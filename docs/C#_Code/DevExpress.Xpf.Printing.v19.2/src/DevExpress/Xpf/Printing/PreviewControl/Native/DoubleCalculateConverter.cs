namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;
    using System.Windows.Markup;

    internal class DoubleCalculateConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double? nullable5;
            double? nullable = value as double?;
            if (this.MaxValue != null)
            {
                nullable = new double?(Math.Min(nullable.Value, this.MaxValue.Value));
            }
            if (nullable == null)
            {
                nullable5 = 0.0;
            }
            else
            {
                double? nullable1;
                double? nullable3 = nullable;
                double num2 = this.Argument1;
                if (nullable3 != null)
                {
                    nullable1 = new double?(nullable3.GetValueOrDefault() * num2);
                }
                else
                {
                    nullable1 = null;
                }
                double? nullable2 = nullable1;
                double num = this.Argument2;
                if (nullable2 != null)
                {
                    nullable5 = new double?(nullable2.GetValueOrDefault() + num);
                }
                else
                {
                    nullable3 = null;
                    nullable5 = nullable3;
                }
            }
            return nullable5;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public double Argument1 { get; set; }

        public double Argument2 { get; set; }

        public double? MaxValue { get; set; }
    }
}

