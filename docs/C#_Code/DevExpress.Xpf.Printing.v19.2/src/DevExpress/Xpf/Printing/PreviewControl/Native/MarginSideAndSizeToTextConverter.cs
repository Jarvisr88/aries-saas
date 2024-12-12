namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class MarginSideAndSizeToTextConverter : MarkupExtension, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string str;
            MarginSide side = Guard.ArgumentMatchType<MarginSide>(values[0], "values[0]");
            double num = Guard.ArgumentMatchType<double>(values[1], "values[1]");
            switch (side)
            {
                case MarginSide.Left:
                    str = PreviewStringId.Margin_LeftMargin.GetString();
                    break;

                case MarginSide.Top:
                    str = PreviewStringId.Margin_TopMargin.GetString();
                    break;

                case (MarginSide.Top | MarginSide.Left):
                    goto TR_0000;

                case MarginSide.Right:
                    str = PreviewStringId.Margin_RightMargin.GetString();
                    break;

                default:
                    if (side == MarginSide.Bottom)
                    {
                        str = PreviewStringId.Margin_BottomMargin.GetString();
                    }
                    else
                    {
                        goto TR_0000;
                    }
                    break;
            }
            bool isMetric = RegionInfo.CurrentRegion.IsMetric;
            float toDpi = isMetric ? 25.4f : 1f;
            double num3 = Math.Round((double) GraphicsUnitConverter.Convert((float) num, (float) 300f, toDpi), 2);
            string str2 = isMetric ? PreviewStringId.Margin_Millimeter.GetString() : "\"";
            return $"{str}: {num3:0.00} {str2}";
        TR_0000:
            throw new InvalidOperationException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

