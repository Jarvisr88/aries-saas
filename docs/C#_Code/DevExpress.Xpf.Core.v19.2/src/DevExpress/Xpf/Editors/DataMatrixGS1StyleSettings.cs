namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.XtraPrinting.BarCode;
    using System;
    using System.ComponentModel;
    using System.Windows;

    public class DataMatrixGS1StyleSettings : BarCodeStyleSettings<DataMatrixGS1Generator>
    {
        public static readonly DependencyProperty FNC1SubstituteProperty = DependencyProperty.Register("FNC1Substitute", typeof(string), typeof(DataMatrixGS1StyleSettings), new PropertyMetadata("#", new PropertyChangedCallback(DataMatrixGS1StyleSettings.FNC1SubstitutePropertyChanged)));
        public static readonly DependencyProperty HumanReadableTextProperty = DependencyProperty.Register("HumanReadableText", typeof(bool), typeof(DataMatrixGS1StyleSettings), new PropertyMetadata(true, new PropertyChangedCallback(DataMatrixGS1StyleSettings.HumanReadableTextPropertyChanged)));
        public static readonly DependencyProperty MatrixSizeProperty = DependencyProperty.Register("MatrixSize", typeof(DataMatrixSize), typeof(DataMatrixGS1StyleSettings), new PropertyMetadata(DataMatrixSize.MatrixAuto, new PropertyChangedCallback(DataMatrixGS1StyleSettings.MatrixSizePropertyChanged)));

        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            BarCodePropertyProvider propertyProvider = (BarCodePropertyProvider) editor.PropertyProvider;
            propertyProvider.FNC1Substitute = this.FNC1Substitute;
            propertyProvider.HumanReadableText = this.HumanReadableText;
            propertyProvider.MatrixSize = this.MatrixSize;
        }

        private static void FNC1SubstitutePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DataMatrixGS1StyleSettings) d).Generator.FNC1Substitute = (string) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        private static void HumanReadableTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DataMatrixGS1StyleSettings) d).Generator.HumanReadableText = (bool) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        private static void MatrixSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DataMatrixGS1StyleSettings) d).Generator.MatrixSize = (DataMatrixSize) e.NewValue;
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

        [TypeConverter(typeof(XamlEnumTypeConverter<DataMatrixSize>))]
        public DataMatrixSize MatrixSize
        {
            get => 
                (DataMatrixSize) base.GetValue(MatrixSizeProperty);
            set => 
                base.SetValue(MatrixSizeProperty, value);
        }
    }
}

