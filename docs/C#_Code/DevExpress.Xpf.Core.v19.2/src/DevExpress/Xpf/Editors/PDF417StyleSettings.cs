namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.XtraPrinting.BarCode;
    using System;
    using System.ComponentModel;
    using System.Windows;

    public class PDF417StyleSettings : BarCodeStyleSettings<PDF417Generator>
    {
        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register("Columns", typeof(int), typeof(PDF417StyleSettings), new PropertyMetadata(1, new PropertyChangedCallback(PDF417StyleSettings.ColumnsPropertyChanged)));
        public static readonly DependencyProperty CompactionModeProperty = DependencyProperty.Register("CompactionMode", typeof(PDF417CompactionMode), typeof(PDF417StyleSettings), new PropertyMetadata(PDF417CompactionMode.Text, new PropertyChangedCallback(PDF417StyleSettings.CompactionModePropertyChanged)));
        public static readonly DependencyProperty ErrorCorrectionLevelProperty = DependencyProperty.Register("ErrorCorrectionLevel", typeof(DevExpress.XtraPrinting.BarCode.ErrorCorrectionLevel), typeof(PDF417StyleSettings), new PropertyMetadata(DevExpress.XtraPrinting.BarCode.ErrorCorrectionLevel.Level2, new PropertyChangedCallback(PDF417StyleSettings.ErrorCorrectionLevelPropertyChanged)));
        public static readonly DependencyProperty RowsProperty = DependencyProperty.Register("Rows", typeof(int), typeof(PDF417StyleSettings), new PropertyMetadata(3, new PropertyChangedCallback(PDF417StyleSettings.RowsPropertyChanged)));
        public static readonly DependencyProperty TruncateSymbolProperty = DependencyProperty.Register("TruncateSymbol", typeof(bool), typeof(PDF417StyleSettings), new PropertyMetadata(false, new PropertyChangedCallback(PDF417StyleSettings.TruncateSymbolPropertyChanged)));
        public static readonly DependencyProperty YToXRatioProperty = DependencyProperty.Register("YToXRatio", typeof(float), typeof(PDF417StyleSettings), new PropertyMetadata(3f, new PropertyChangedCallback(PDF417StyleSettings.YToXRatioPropertyChanged)));

        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            BarCodePropertyProvider propertyProvider = (BarCodePropertyProvider) editor.PropertyProvider;
            propertyProvider.Columns = this.Columns;
            propertyProvider.PDF417CompactionMode = this.CompactionMode;
            propertyProvider.PDF417ErrorCorrectionLevel = this.ErrorCorrectionLevel;
            propertyProvider.Rows = this.Rows;
            propertyProvider.TruncateSymbol = this.TruncateSymbol;
            propertyProvider.YToXRatio = this.YToXRatio;
        }

        private static void ColumnsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PDF417StyleSettings) d).Generator.Columns = (int) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        private static void CompactionModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PDF417StyleSettings) d).Generator.CompactionMode = (PDF417CompactionMode) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        private static void ErrorCorrectionLevelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PDF417StyleSettings) d).Generator.ErrorCorrectionLevel = (DevExpress.XtraPrinting.BarCode.ErrorCorrectionLevel) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        private static void RowsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PDF417StyleSettings) d).Generator.Rows = (int) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        private static void TruncateSymbolPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PDF417StyleSettings) d).Generator.TruncateSymbol = (bool) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        private static void YToXRatioPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PDF417StyleSettings) d).Generator.YToXRatio = (float) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        public int Columns
        {
            get => 
                (int) base.GetValue(ColumnsProperty);
            set => 
                base.SetValue(ColumnsProperty, value);
        }

        [TypeConverter(typeof(XamlEnumTypeConverter<PDF417CompactionMode>))]
        public PDF417CompactionMode CompactionMode
        {
            get => 
                (PDF417CompactionMode) base.GetValue(CompactionModeProperty);
            set => 
                base.SetValue(CompactionModeProperty, value);
        }

        [TypeConverter(typeof(XamlEnumTypeConverter<DevExpress.XtraPrinting.BarCode.ErrorCorrectionLevel>))]
        public DevExpress.XtraPrinting.BarCode.ErrorCorrectionLevel ErrorCorrectionLevel
        {
            get => 
                (DevExpress.XtraPrinting.BarCode.ErrorCorrectionLevel) base.GetValue(ErrorCorrectionLevelProperty);
            set => 
                base.SetValue(ErrorCorrectionLevelProperty, value);
        }

        public int Rows
        {
            get => 
                (int) base.GetValue(RowsProperty);
            set => 
                base.SetValue(RowsProperty, value);
        }

        public bool TruncateSymbol
        {
            get => 
                (bool) base.GetValue(TruncateSymbolProperty);
            set => 
                base.SetValue(TruncateSymbolProperty, value);
        }

        public float YToXRatio
        {
            get => 
                (float) base.GetValue(YToXRatioProperty);
            set => 
                base.SetValue(YToXRatioProperty, value);
        }
    }
}

