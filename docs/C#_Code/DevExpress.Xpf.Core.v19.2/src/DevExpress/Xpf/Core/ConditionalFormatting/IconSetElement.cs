namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class IconSetElement : Freezable
    {
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(ImageSource), typeof(IconSetElement), new PropertyMetadata(null));
        public static readonly DependencyProperty ThresholdProperty = DependencyProperty.Register("Threshold", typeof(double), typeof(IconSetElement), new PropertyMetadata(0.0));
        public static readonly DependencyProperty ThresholdComparisonTypeProperty = DependencyProperty.Register("ThresholdComparisonType", typeof(DevExpress.Xpf.Core.ConditionalFormatting.ThresholdComparisonType), typeof(IconSetElement), new PropertyMetadata(DevExpress.Xpf.Core.ConditionalFormatting.ThresholdComparisonType.GreaterOrEqual));

        protected override Freezable CreateInstanceCore() => 
            new IconSetElement();

        [XtraSerializableProperty]
        public ImageSource Icon
        {
            get => 
                (ImageSource) base.GetValue(IconProperty);
            set => 
                base.SetValue(IconProperty, value);
        }

        [XtraSerializableProperty]
        public double Threshold
        {
            get => 
                (double) base.GetValue(ThresholdProperty);
            set => 
                base.SetValue(ThresholdProperty, value);
        }

        [XtraSerializableProperty]
        public DevExpress.Xpf.Core.ConditionalFormatting.ThresholdComparisonType ThresholdComparisonType
        {
            get => 
                (DevExpress.Xpf.Core.ConditionalFormatting.ThresholdComparisonType) base.GetValue(ThresholdComparisonTypeProperty);
            set => 
                base.SetValue(ThresholdComparisonTypeProperty, value);
        }
    }
}

