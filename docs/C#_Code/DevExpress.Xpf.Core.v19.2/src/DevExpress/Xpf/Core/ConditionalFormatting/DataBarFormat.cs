namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class DataBarFormat : IndicatorFormatBase
    {
        public static readonly DependencyProperty ZeroLineBrushProperty = DependencyProperty.Register("ZeroLineBrush", typeof(Brush), typeof(DataBarFormat), new PropertyMetadata(null));
        public static readonly DependencyProperty ZeroLineThicknessProperty = DependencyProperty.Register("ZeroLineThickness", typeof(double), typeof(DataBarFormat), new PropertyMetadata(1.0));
        public static readonly DependencyProperty MarginProperty = DependencyProperty.Register("Margin", typeof(Thickness), typeof(DataBarFormat), new PropertyMetadata(new Thickness(1.0)));
        public static readonly DependencyProperty BorderThicknessProperty = DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(DataBarFormat), new PropertyMetadata(new Thickness(1.0)));
        public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(DataBarFormat), new PropertyMetadata(null));
        public static readonly DependencyProperty BorderBrushNegativeProperty = DependencyProperty.Register("BorderBrushNegative", typeof(Brush), typeof(DataBarFormat), new PropertyMetadata(null));
        public static readonly DependencyProperty FillProperty = DependencyProperty.Register("Fill", typeof(Brush), typeof(DataBarFormat), new PropertyMetadata(null));
        public static readonly DependencyProperty FillNegativeProperty = DependencyProperty.Register("FillNegative", typeof(Brush), typeof(DataBarFormat), new PropertyMetadata(null));

        public DataBarFormatInfo CalcDataBarFormatInfo(FormatValueProvider provider, DataBarFormatInfo value, decimal? minValue, decimal? maxValue)
        {
            decimal? decimalValue = GetDecimalValue(provider.Value);
            if (decimalValue == null)
            {
                return value;
            }
            decimal? nullable2 = GetSummaryValue(provider, ConditionalFormatSummaryType.Min, minValue);
            decimal? nullable3 = GetSummaryValue(provider, ConditionalFormatSummaryType.Max, maxValue);
            if ((nullable2 == null) || (nullable3 == null))
            {
                return value;
            }
            decimal num = nullable2.Value;
            if (minValue == null)
            {
                num = Math.Min(0M, num);
            }
            decimal num2 = nullable3.Value;
            if (maxValue == null)
            {
                num2 = Math.Max(0M, num2);
            }
            decimal num3 = num2 - num;
            double zeroPosition = !(num3 == 0M) ? ((num2 >= 0M) ? ((num <= 0M) ? ((double) (-num / num3)) : 0.0) : 1.0) : 0.5;
            return DataBarFormatInfo.AddDataBarFormatInfo(value, this, zeroPosition, (num3 == 0M) ? 0.5 : ((double) ((GetNormalizedValue<decimal>(decimalValue.Value, num, num2) - num) / num3)));
        }

        public override DataBarFormatInfo CoerceDataBarFormatInfo(DataBarFormatInfo value, FormatValueProvider provider, decimal? minValue, decimal? maxValue) => 
            this.CalcDataBarFormatInfo(provider, value, minValue, maxValue);

        protected override Freezable CreateInstanceCore() => 
            new DataBarFormat();

        [XtraSerializableProperty]
        public Brush ZeroLineBrush
        {
            get => 
                (Brush) base.GetValue(ZeroLineBrushProperty);
            set => 
                base.SetValue(ZeroLineBrushProperty, value);
        }

        [XtraSerializableProperty]
        public double ZeroLineThickness
        {
            get => 
                (double) base.GetValue(ZeroLineThicknessProperty);
            set => 
                base.SetValue(ZeroLineThicknessProperty, value);
        }

        [XtraSerializableProperty]
        public Thickness Margin
        {
            get => 
                (Thickness) base.GetValue(MarginProperty);
            set => 
                base.SetValue(MarginProperty, value);
        }

        [XtraSerializableProperty]
        public Thickness BorderThickness
        {
            get => 
                (Thickness) base.GetValue(BorderThicknessProperty);
            set => 
                base.SetValue(BorderThicknessProperty, value);
        }

        [XtraSerializableProperty]
        public Brush BorderBrush
        {
            get => 
                (Brush) base.GetValue(BorderBrushProperty);
            set => 
                base.SetValue(BorderBrushProperty, value);
        }

        [XtraSerializableProperty]
        public Brush BorderBrushNegative
        {
            get => 
                (Brush) base.GetValue(BorderBrushNegativeProperty);
            set => 
                base.SetValue(BorderBrushNegativeProperty, value);
        }

        [XtraSerializableProperty]
        public Brush Fill
        {
            get => 
                (Brush) base.GetValue(FillProperty);
            set => 
                base.SetValue(FillProperty, value);
        }

        [XtraSerializableProperty]
        public Brush FillNegative
        {
            get => 
                (Brush) base.GetValue(FillNegativeProperty);
            set => 
                base.SetValue(FillNegativeProperty, value);
        }
    }
}

