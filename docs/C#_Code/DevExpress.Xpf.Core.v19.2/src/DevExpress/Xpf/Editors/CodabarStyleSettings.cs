namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.XtraPrinting.BarCode;
    using System;
    using System.ComponentModel;
    using System.Windows;

    public class CodabarStyleSettings : BarCodeStyleSettings<CodabarGenerator>
    {
        public static readonly DependencyProperty StartStopPairProperty = DependencyProperty.Register("StartStopPair", typeof(CodabarStartStopPair), typeof(CodabarStyleSettings), new PropertyMetadata(CodabarStartStopPair.AT, new PropertyChangedCallback(CodabarStyleSettings.StartStopPairPropertyChanged)));
        public static readonly DependencyProperty WideNarrowRatioProperty = DependencyProperty.Register("WideNarrowRatio", typeof(float), typeof(CodabarStyleSettings), new PropertyMetadata(2f, new PropertyChangedCallback(CodabarStyleSettings.WideNarrowRatioPropertyChanged)));

        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            BarCodePropertyProvider propertyProvider = (BarCodePropertyProvider) editor.PropertyProvider;
            propertyProvider.StartStopPair = this.StartStopPair;
            propertyProvider.WideNarrowRatio = this.WideNarrowRatio;
        }

        private static void StartStopPairPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CodabarStyleSettings) d).Generator.StartStopPair = (CodabarStartStopPair) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        private static void WideNarrowRatioPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CodabarStyleSettings) d).Generator.WideNarrowRatio = (float) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        [TypeConverter(typeof(XamlEnumTypeConverter<CodabarStartStopPair>))]
        public CodabarStartStopPair StartStopPair
        {
            get => 
                (CodabarStartStopPair) base.GetValue(StartStopPairProperty);
            set => 
                base.SetValue(StartStopPairProperty, value);
        }

        public float WideNarrowRatio
        {
            get => 
                (float) base.GetValue(WideNarrowRatioProperty);
            set => 
                base.SetValue(WideNarrowRatioProperty, value);
        }
    }
}

