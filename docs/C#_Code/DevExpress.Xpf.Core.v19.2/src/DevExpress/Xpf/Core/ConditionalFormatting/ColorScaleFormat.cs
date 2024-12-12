namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class ColorScaleFormat : IndicatorFormatBase
    {
        public static readonly DependencyProperty ColorMinProperty = DependencyProperty.Register("ColorMin", typeof(Color), typeof(ColorScaleFormat), new PropertyMetadata(Colors.Transparent));
        public static readonly DependencyProperty ColorMiddleProperty = DependencyProperty.Register("ColorMiddle", typeof(Color?), typeof(ColorScaleFormat), new PropertyMetadata(null));
        public static readonly DependencyProperty ColorMaxProperty = DependencyProperty.Register("ColorMax", typeof(Color), typeof(ColorScaleFormat), new PropertyMetadata(Colors.Transparent));

        public Color? CalcColor(FormatValueProvider provider, decimal? minValue, decimal? maxValue)
        {
            decimal? decimalValue = GetDecimalValue(provider.Value);
            if (decimalValue == null)
            {
                return null;
            }
            decimal? nullable2 = GetSummaryValue(provider, ConditionalFormatSummaryType.Min, minValue);
            decimal? nullable3 = GetSummaryValue(provider, ConditionalFormatSummaryType.Max, maxValue);
            if ((nullable2 == null) || (nullable3 == null))
            {
                return null;
            }
            Color colorMin = this.ColorMin;
            Color colorMax = this.ColorMax;
            if (this.ColorMiddle != null)
            {
                decimal num = (nullable2.Value + nullable3.Value) / 2M;
                decimal? nullable5 = decimalValue;
                decimal num2 = num;
                if ((nullable5.GetValueOrDefault() < num2) ? (nullable5 != null) : false)
                {
                    colorMax = this.ColorMiddle.Value;
                    nullable3 = new decimal?(num);
                }
                else
                {
                    colorMin = this.ColorMiddle.Value;
                    nullable2 = new decimal?(num);
                }
            }
            return new Color?(CalcColorCore(colorMin, colorMax, nullable2.Value, nullable3.Value, GetNormalizedValue<decimal>(decimalValue.Value, nullable2.Value, nullable3.Value)));
        }

        private static Color CalcColorCore(Color colorLow, Color colorHigh, decimal min, decimal max, decimal cellValue)
        {
            decimal ratio = GetRatio(min, max, cellValue);
            return Color.FromArgb(GetScaleValue(ratio, colorLow.A, colorHigh.A), GetScaleValue(ratio, colorLow.R, colorHigh.R), GetScaleValue(ratio, colorLow.G, colorHigh.G), GetScaleValue(ratio, colorLow.B, colorHigh.B));
        }

        public override Brush CoerceBackground(Brush value, FormatValueProvider provider, decimal? minValue, decimal? maxValue)
        {
            Color? nullable = this.CalcColor(provider, minValue, maxValue);
            return ((nullable != null) ? new SolidColorBrush(nullable.Value) : value);
        }

        protected override Freezable CreateInstanceCore() => 
            new ColorScaleFormat();

        internal static decimal GetRatio(decimal min, decimal max, decimal cellValue) => 
            !(min == max) ? ((cellValue - min) / (max - min)) : 1M;

        private static byte GetScaleValue(decimal ratio, decimal low, decimal high) => 
            (byte) Math.Round((decimal) (low + ((high - low) * ratio)));

        [XtraSerializableProperty]
        public Color ColorMin
        {
            get => 
                (Color) base.GetValue(ColorMinProperty);
            set => 
                base.SetValue(ColorMinProperty, value);
        }

        [XtraSerializableProperty]
        public Color? ColorMiddle
        {
            get => 
                (Color?) base.GetValue(ColorMiddleProperty);
            set => 
                base.SetValue(ColorMiddleProperty, value);
        }

        [XtraSerializableProperty]
        public Color ColorMax
        {
            get => 
                (Color) base.GetValue(ColorMaxProperty);
            set => 
                base.SetValue(ColorMaxProperty, value);
        }
    }
}

