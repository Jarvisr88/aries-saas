namespace DevExpress.Xpf.Editors
{
    using DevExpress.XtraPrinting.BarCode;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class BarCodeStyleSettings : BaseEditStyleSettings
    {
        public BarCodeStyleSettings()
        {
            base.Visibility = Visibility.Collapsed;
        }

        public static void BarCodeControlInvalidateVisual(DependencyObject d)
        {
            if (((BarCodeStyleSettings) d).BarCodeEdit != null)
            {
                ((BarCodeStyleSettings) d).BarCodeEdit.BarCodeInvalidateVisual();
            }
        }

        public abstract BarCodeGeneratorBase GeneratorBase { get; }

        internal DevExpress.Xpf.Editors.BarCodeEdit BarCodeEdit { get; set; }
    }
}

