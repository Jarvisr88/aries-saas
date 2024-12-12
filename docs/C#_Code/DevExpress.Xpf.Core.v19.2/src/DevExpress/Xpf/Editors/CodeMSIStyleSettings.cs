namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.XtraPrinting.BarCode;
    using System;
    using System.ComponentModel;
    using System.Windows;

    public class CodeMSIStyleSettings : BarCodeStyleSettings<CodeMSIGenerator>
    {
        public static readonly DependencyProperty MSICheckSumProperty = DependencyProperty.Register("MSICheckSum", typeof(DevExpress.XtraPrinting.BarCode.MSICheckSum), typeof(CodeMSIStyleSettings), new PropertyMetadata(DevExpress.XtraPrinting.BarCode.MSICheckSum.Modulo10, new PropertyChangedCallback(CodeMSIStyleSettings.MSICheckSumPropertyChanged)));

        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            ((BarCodePropertyProvider) editor.PropertyProvider).MSICheckSum = this.MSICheckSum;
        }

        private static void MSICheckSumPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CodeMSIStyleSettings) d).Generator.MSICheckSum = (DevExpress.XtraPrinting.BarCode.MSICheckSum) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        [TypeConverter(typeof(XamlEnumTypeConverter<DevExpress.XtraPrinting.BarCode.MSICheckSum>))]
        public DevExpress.XtraPrinting.BarCode.MSICheckSum MSICheckSum
        {
            get => 
                (DevExpress.XtraPrinting.BarCode.MSICheckSum) base.GetValue(MSICheckSumProperty);
            set => 
                base.SetValue(MSICheckSumProperty, value);
        }
    }
}

