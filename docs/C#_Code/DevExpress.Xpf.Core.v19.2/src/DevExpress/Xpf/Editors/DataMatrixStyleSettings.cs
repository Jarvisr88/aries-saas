namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.XtraPrinting.BarCode;
    using System;
    using System.ComponentModel;
    using System.Windows;

    public class DataMatrixStyleSettings : BarCodeStyleSettings<DataMatrixGenerator>
    {
        public static readonly DependencyProperty CompactionModeProperty = DependencyProperty.Register("CompactionMode", typeof(DataMatrixCompactionMode), typeof(DataMatrixStyleSettings), new PropertyMetadata(DataMatrixCompactionMode.ASCII, new PropertyChangedCallback(DataMatrixStyleSettings.CompactionModePropertyChanged)));
        public static readonly DependencyProperty MatrixSizeProperty = DependencyProperty.Register("MatrixSize", typeof(DataMatrixSize), typeof(DataMatrixStyleSettings), new PropertyMetadata(DataMatrixSize.MatrixAuto, new PropertyChangedCallback(DataMatrixStyleSettings.MatrixSizePropertyChanged)));

        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            BarCodePropertyProvider propertyProvider = (BarCodePropertyProvider) editor.PropertyProvider;
            propertyProvider.DataMatrixCompactionMode = this.CompactionMode;
            propertyProvider.MatrixSize = this.MatrixSize;
        }

        private static void CompactionModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DataMatrixStyleSettings) d).Generator.CompactionMode = (DataMatrixCompactionMode) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        private static void MatrixSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DataMatrixStyleSettings) d).Generator.MatrixSize = (DataMatrixSize) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        [TypeConverter(typeof(XamlEnumTypeConverter<DataMatrixCompactionMode>))]
        public DataMatrixCompactionMode CompactionMode
        {
            get => 
                (DataMatrixCompactionMode) base.GetValue(CompactionModeProperty);
            set => 
                base.SetValue(CompactionModeProperty, value);
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

