namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.XtraPrinting.BarCode;
    using System;
    using System.ComponentModel;
    using System.Windows;

    public class DataBarStyleSettings : BarCodeStyleSettings<DataBarGenerator>
    {
        public static readonly DependencyProperty FNC1SubstituteProperty = DependencyProperty.Register("FNC1Substitute", typeof(string), typeof(DataBarStyleSettings), new PropertyMetadata("#", new PropertyChangedCallback(DataBarStyleSettings.FNC1SubstitutePropertyChanged)));
        public static readonly DependencyProperty SegmentsInRowProperty = DependencyProperty.Register("SegmentsInRow", typeof(int), typeof(DataBarStyleSettings), new PropertyMetadata((int) 20, new PropertyChangedCallback(DataBarStyleSettings.SegmentsInRowPropertyChanged)));
        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(DataBarType), typeof(DataBarStyleSettings), new PropertyMetadata(DataBarType.Omnidirectional, new PropertyChangedCallback(DataBarStyleSettings.TypePropertyChanged)));

        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            BarCodePropertyProvider propertyProvider = (BarCodePropertyProvider) editor.PropertyProvider;
            propertyProvider.FNC1Substitute = this.FNC1Substitute;
            propertyProvider.SegmentsInRow = this.SegmentsInRow;
            propertyProvider.Type = this.Type;
        }

        private static void FNC1SubstitutePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DataBarStyleSettings) d).Generator.FNC1Substitute = (string) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        private static void SegmentsInRowPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DataBarStyleSettings) d).Generator.SegmentsInRow = (int) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        private static void TypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DataBarStyleSettings) d).Generator.Type = (DataBarType) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        public string FNC1Substitute
        {
            get => 
                (string) base.GetValue(FNC1SubstituteProperty);
            set => 
                base.SetValue(FNC1SubstituteProperty, value);
        }

        public int SegmentsInRow
        {
            get => 
                (int) base.GetValue(SegmentsInRowProperty);
            set => 
                base.SetValue(SegmentsInRowProperty, value);
        }

        [TypeConverter(typeof(XamlEnumTypeConverter<DataBarType>))]
        public DataBarType Type
        {
            get => 
                (DataBarType) base.GetValue(TypeProperty);
            set => 
                base.SetValue(TypeProperty, value);
        }
    }
}

