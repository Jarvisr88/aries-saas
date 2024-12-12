namespace DevExpress.Xpf.Editors
{
    using DevExpress.XtraPrinting.BarCode;
    using System;
    using System.Windows;

    public class BarCodePropertyProvider : ActualPropertyProvider
    {
        public static readonly DependencyProperty CalcCheckSumProperty;
        public static readonly DependencyProperty StartStopPairProperty;
        public static readonly DependencyProperty WideNarrowRatioProperty;
        public static readonly DependencyProperty CharacterSetProperty;
        public static readonly DependencyProperty MSICheckSumProperty;
        public static readonly DependencyProperty FNC1SubstituteProperty;
        public static readonly DependencyProperty HumanReadableTextProperty;
        public static readonly DependencyProperty ColumnsProperty;
        public static readonly DependencyProperty PDF417CompactionModeProperty;
        public static readonly DependencyProperty DataMatrixCompactionModeProperty;
        public static readonly DependencyProperty QRCodeCompactionModeProperty;
        public static readonly DependencyProperty PDF417ErrorCorrectionLevelProperty;
        public static readonly DependencyProperty QRCodeErrorCorrectionLevelProperty;
        public static readonly DependencyProperty RowsProperty;
        public static readonly DependencyProperty TruncateSymbolProperty;
        public static readonly DependencyProperty YToXRatioProperty;
        public static readonly DependencyProperty MatrixSizeProperty;
        public static readonly DependencyProperty VersionProperty;
        public static readonly DependencyProperty SegmentsInRowProperty;
        public static readonly DependencyProperty TypeProperty;

        static BarCodePropertyProvider()
        {
            System.Type ownerType = typeof(BarCodePropertyProvider);
            CalcCheckSumProperty = DependencyProperty.Register("CalcCheckSum", typeof(bool), ownerType, new PropertyMetadata(true, new PropertyChangedCallback(BarCodePropertyProvider.PropertyChanged)));
            StartStopPairProperty = DependencyProperty.Register("StartStopPair", typeof(CodabarStartStopPair), ownerType, new PropertyMetadata(CodabarStartStopPair.AT, new PropertyChangedCallback(BarCodePropertyProvider.PropertyChanged)));
            WideNarrowRatioProperty = DependencyProperty.Register("WideNarrowRatio", typeof(float), ownerType, new PropertyMetadata(2f, new PropertyChangedCallback(BarCodePropertyProvider.PropertyChanged)));
            CharacterSetProperty = DependencyProperty.Register("CharacterSet", typeof(Code128Charset), ownerType, new PropertyMetadata(Code128Charset.CharsetA, new PropertyChangedCallback(BarCodePropertyProvider.PropertyChanged)));
            MSICheckSumProperty = DependencyProperty.Register("MSICheckSum", typeof(DevExpress.XtraPrinting.BarCode.MSICheckSum), ownerType, new PropertyMetadata(DevExpress.XtraPrinting.BarCode.MSICheckSum.Modulo10, new PropertyChangedCallback(BarCodePropertyProvider.PropertyChanged)));
            FNC1SubstituteProperty = DependencyProperty.Register("FNC1Substitute", typeof(string), ownerType, new PropertyMetadata("#", new PropertyChangedCallback(BarCodePropertyProvider.PropertyChanged)));
            HumanReadableTextProperty = DependencyProperty.Register("HumanReadableText", typeof(bool), ownerType, new PropertyMetadata(true, new PropertyChangedCallback(BarCodePropertyProvider.PropertyChanged)));
            ColumnsProperty = DependencyProperty.Register("Columns", typeof(int), ownerType, new PropertyMetadata(1, new PropertyChangedCallback(BarCodePropertyProvider.PropertyChanged)));
            PDF417CompactionModeProperty = DependencyProperty.Register("PDF417CompactionMode", typeof(DevExpress.XtraPrinting.BarCode.PDF417CompactionMode), ownerType, new PropertyMetadata(DevExpress.XtraPrinting.BarCode.PDF417CompactionMode.Text, new PropertyChangedCallback(BarCodePropertyProvider.PropertyChanged)));
            DataMatrixCompactionModeProperty = DependencyProperty.Register("DataMatrixCompactionMode", typeof(DevExpress.XtraPrinting.BarCode.DataMatrixCompactionMode), ownerType, new PropertyMetadata(DevExpress.XtraPrinting.BarCode.DataMatrixCompactionMode.ASCII, new PropertyChangedCallback(BarCodePropertyProvider.PropertyChanged)));
            QRCodeCompactionModeProperty = DependencyProperty.Register("QRCodeCompactionMode", typeof(DevExpress.XtraPrinting.BarCode.QRCodeCompactionMode), ownerType, new PropertyMetadata(DevExpress.XtraPrinting.BarCode.QRCodeCompactionMode.AlphaNumeric, new PropertyChangedCallback(BarCodePropertyProvider.PropertyChanged)));
            PDF417ErrorCorrectionLevelProperty = DependencyProperty.Register("PDF417ErrorCorrectionLevel", typeof(ErrorCorrectionLevel), ownerType, new PropertyMetadata(ErrorCorrectionLevel.Level2, new PropertyChangedCallback(BarCodePropertyProvider.PropertyChanged)));
            QRCodeErrorCorrectionLevelProperty = DependencyProperty.Register("QRCodeErrorCorrectionLevel", typeof(DevExpress.XtraPrinting.BarCode.QRCodeErrorCorrectionLevel), ownerType, new PropertyMetadata(DevExpress.XtraPrinting.BarCode.QRCodeErrorCorrectionLevel.L, new PropertyChangedCallback(BarCodePropertyProvider.PropertyChanged)));
            RowsProperty = DependencyProperty.Register("Rows", typeof(int), ownerType, new PropertyMetadata(3, new PropertyChangedCallback(BarCodePropertyProvider.PropertyChanged)));
            TruncateSymbolProperty = DependencyProperty.Register("TruncateSymbol", typeof(bool), ownerType, new PropertyMetadata(false, new PropertyChangedCallback(BarCodePropertyProvider.PropertyChanged)));
            YToXRatioProperty = DependencyProperty.Register("YToXRatio", typeof(float), ownerType, new PropertyMetadata(3f, new PropertyChangedCallback(BarCodePropertyProvider.PropertyChanged)));
            MatrixSizeProperty = DependencyProperty.Register("MatrixSize", typeof(DataMatrixSize), ownerType, new PropertyMetadata(DataMatrixSize.MatrixAuto, new PropertyChangedCallback(BarCodePropertyProvider.PropertyChanged)));
            VersionProperty = DependencyProperty.Register("Version", typeof(QRCodeVersion), ownerType, new PropertyMetadata(QRCodeVersion.AutoVersion, new PropertyChangedCallback(BarCodePropertyProvider.PropertyChanged)));
            SegmentsInRowProperty = DependencyProperty.Register("SegmentsInRow", typeof(int), ownerType, new PropertyMetadata((int) 20, new PropertyChangedCallback(BarCodePropertyProvider.PropertyChanged)));
            TypeProperty = DependencyProperty.Register("Type", typeof(DataBarType), ownerType, new PropertyMetadata(DataBarType.Omnidirectional, new PropertyChangedCallback(BarCodePropertyProvider.PropertyChanged)));
        }

        public BarCodePropertyProvider(BarCodeEdit barCodeEdit) : base(barCodeEdit)
        {
        }

        private static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BarCodeEdit) ((BarCodePropertyProvider) d).Editor).BarCodeInvalidateVisual();
        }

        public bool CalcCheckSum
        {
            get => 
                (bool) base.GetValue(CalcCheckSumProperty);
            set => 
                base.SetValue(CalcCheckSumProperty, value);
        }

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

        public Code128Charset CharacterSet
        {
            get => 
                (Code128Charset) base.GetValue(CharacterSetProperty);
            set => 
                base.SetValue(CharacterSetProperty, value);
        }

        public DevExpress.XtraPrinting.BarCode.MSICheckSum MSICheckSum
        {
            get => 
                (DevExpress.XtraPrinting.BarCode.MSICheckSum) base.GetValue(MSICheckSumProperty);
            set => 
                base.SetValue(MSICheckSumProperty, value);
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

        public int Columns
        {
            get => 
                (int) base.GetValue(ColumnsProperty);
            set => 
                base.SetValue(ColumnsProperty, value);
        }

        public DevExpress.XtraPrinting.BarCode.PDF417CompactionMode PDF417CompactionMode
        {
            get => 
                (DevExpress.XtraPrinting.BarCode.PDF417CompactionMode) base.GetValue(PDF417CompactionModeProperty);
            set => 
                base.SetValue(PDF417CompactionModeProperty, value);
        }

        public DevExpress.XtraPrinting.BarCode.DataMatrixCompactionMode DataMatrixCompactionMode
        {
            get => 
                (DevExpress.XtraPrinting.BarCode.DataMatrixCompactionMode) base.GetValue(DataMatrixCompactionModeProperty);
            set => 
                base.SetValue(DataMatrixCompactionModeProperty, value);
        }

        public DevExpress.XtraPrinting.BarCode.QRCodeCompactionMode QRCodeCompactionMode
        {
            get => 
                (DevExpress.XtraPrinting.BarCode.QRCodeCompactionMode) base.GetValue(QRCodeCompactionModeProperty);
            set => 
                base.SetValue(QRCodeCompactionModeProperty, value);
        }

        public ErrorCorrectionLevel PDF417ErrorCorrectionLevel
        {
            get => 
                (ErrorCorrectionLevel) base.GetValue(PDF417ErrorCorrectionLevelProperty);
            set => 
                base.SetValue(PDF417ErrorCorrectionLevelProperty, value);
        }

        public DevExpress.XtraPrinting.BarCode.QRCodeErrorCorrectionLevel QRCodeErrorCorrectionLevel
        {
            get => 
                (DevExpress.XtraPrinting.BarCode.QRCodeErrorCorrectionLevel) base.GetValue(QRCodeErrorCorrectionLevelProperty);
            set => 
                base.SetValue(QRCodeErrorCorrectionLevelProperty, value);
        }

        public int Rows
        {
            get => 
                (int) base.GetValue(RowsProperty);
            set => 
                base.SetValue(RowsProperty, value);
        }

        public float YToXRatio
        {
            get => 
                (float) base.GetValue(YToXRatioProperty);
            set => 
                base.SetValue(YToXRatioProperty, value);
        }

        public bool TruncateSymbol
        {
            get => 
                (bool) base.GetValue(TruncateSymbolProperty);
            set => 
                base.SetValue(TruncateSymbolProperty, value);
        }

        public DataMatrixSize MatrixSize
        {
            get => 
                (DataMatrixSize) base.GetValue(MatrixSizeProperty);
            set => 
                base.SetValue(MatrixSizeProperty, value);
        }

        public QRCodeVersion Version
        {
            get => 
                (QRCodeVersion) base.GetValue(VersionProperty);
            set => 
                base.SetValue(VersionProperty, value);
        }

        public int SegmentsInRow
        {
            get => 
                (int) base.GetValue(SegmentsInRowProperty);
            set => 
                base.SetValue(SegmentsInRowProperty, value);
        }

        public DataBarType Type
        {
            get => 
                (DataBarType) base.GetValue(TypeProperty);
            set => 
                base.SetValue(TypeProperty, value);
        }
    }
}

