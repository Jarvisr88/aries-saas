namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows;

    public class EAN128StyleSettings : Code128BarCodeStyleSettings<EAN128Generator>
    {
        public static readonly DependencyProperty FNC1SubstituteProperty = DependencyProperty.Register("FNC1Substitute", typeof(string), typeof(EAN128StyleSettings), new PropertyMetadata("#", new PropertyChangedCallback(EAN128StyleSettings.FNC1SubstitutePropertyChanged)));
        public static readonly DependencyProperty HumanReadableTextProperty = DependencyProperty.Register("HumanReadableText", typeof(bool), typeof(EAN128StyleSettings), new PropertyMetadata(true, new PropertyChangedCallback(EAN128StyleSettings.HumanReadableTextPropertyChanged)));

        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            BarCodePropertyProvider propertyProvider = (BarCodePropertyProvider) editor.PropertyProvider;
            propertyProvider.FNC1Substitute = this.FNC1Substitute;
            propertyProvider.HumanReadableText = this.HumanReadableText;
        }

        private static void FNC1SubstitutePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EAN128StyleSettings) d).Generator.FNC1Substitute = (string) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        private static void HumanReadableTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EAN128StyleSettings) d).Generator.HumanReadableText = (bool) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        public string FNC1Substitute
        {
            get => 
                (string) base.GetValue(FNC1SubstituteProperty);
            set => 
                base.SetValue(FNC1SubstituteProperty, value);
        }

        public bool HumanReadableText
        {
            get => 
                (bool) base.GetValue(HumanReadableTextProperty);
            set => 
                base.SetValue(HumanReadableTextProperty, value);
        }
    }
}

