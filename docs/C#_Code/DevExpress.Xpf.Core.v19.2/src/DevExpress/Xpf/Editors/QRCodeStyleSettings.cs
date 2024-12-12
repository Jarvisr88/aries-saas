namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.XtraPrinting.BarCode;
    using System;
    using System.ComponentModel;
    using System.Windows;

    public class QRCodeStyleSettings : BarCodeStyleSettings<QRCodeGenerator>
    {
        public static readonly DependencyProperty CompactionModeProperty = DependencyProperty.Register("CompactionMode", typeof(QRCodeCompactionMode), typeof(QRCodeStyleSettings), new PropertyMetadata(QRCodeCompactionMode.AlphaNumeric, new PropertyChangedCallback(QRCodeStyleSettings.CompactionModePropertyChanged)));
        public static readonly DependencyProperty ErrorCorrectionLevelProperty = DependencyProperty.Register("ErrorCorrectionLevel", typeof(QRCodeErrorCorrectionLevel), typeof(QRCodeStyleSettings), new PropertyMetadata(QRCodeErrorCorrectionLevel.L, new PropertyChangedCallback(QRCodeStyleSettings.ErrorCorrectionLevelPropertyChanged)));
        public static readonly DependencyProperty VersionProperty = DependencyProperty.Register("Version", typeof(QRCodeVersion), typeof(QRCodeStyleSettings), new PropertyMetadata(QRCodeVersion.AutoVersion, new PropertyChangedCallback(QRCodeStyleSettings.VersionPropertyChanged)));

        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            BarCodePropertyProvider propertyProvider = (BarCodePropertyProvider) editor.PropertyProvider;
            propertyProvider.QRCodeCompactionMode = this.CompactionMode;
            propertyProvider.QRCodeErrorCorrectionLevel = this.ErrorCorrectionLevel;
            propertyProvider.Version = this.Version;
        }

        private static void CompactionModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((QRCodeStyleSettings) d).Generator.CompactionMode = (QRCodeCompactionMode) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        private static void ErrorCorrectionLevelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((QRCodeStyleSettings) d).Generator.ErrorCorrectionLevel = (QRCodeErrorCorrectionLevel) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        private static void VersionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((QRCodeStyleSettings) d).Generator.Version = (QRCodeVersion) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        [TypeConverter(typeof(XamlEnumTypeConverter<QRCodeCompactionMode>))]
        public QRCodeCompactionMode CompactionMode
        {
            get => 
                (QRCodeCompactionMode) base.GetValue(CompactionModeProperty);
            set => 
                base.SetValue(CompactionModeProperty, value);
        }

        [TypeConverter(typeof(XamlEnumTypeConverter<QRCodeErrorCorrectionLevel>))]
        public QRCodeErrorCorrectionLevel ErrorCorrectionLevel
        {
            get => 
                (QRCodeErrorCorrectionLevel) base.GetValue(ErrorCorrectionLevelProperty);
            set => 
                base.SetValue(ErrorCorrectionLevelProperty, value);
        }

        [TypeConverter(typeof(XamlEnumTypeConverter<QRCodeVersion>))]
        public QRCodeVersion Version
        {
            get => 
                (QRCodeVersion) base.GetValue(VersionProperty);
            set => 
                base.SetValue(VersionProperty, value);
        }
    }
}

