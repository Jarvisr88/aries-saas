namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    public class Industrial2of5BarCodeStyleSettings<T> : CheckSumStyleSettingsBase<T> where T: Industrial2of5Generator, new()
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty WideNarrowRatioProperty;

        static Industrial2of5BarCodeStyleSettings()
        {
            Industrial2of5BarCodeStyleSettings<T>.WideNarrowRatioProperty = DependencyProperty.Register("WideNarrowRatio", typeof(float), typeof(Industrial2of5BarCodeStyleSettings<T>), new PropertyMetadata(2.5f, new PropertyChangedCallback(Industrial2of5BarCodeStyleSettings<T>.WideNarrowRatioPropertyChanged)));
        }

        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            ((BarCodePropertyProvider) editor.PropertyProvider).WideNarrowRatio = this.WideNarrowRatio;
        }

        private static void WideNarrowRatioPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Industrial2of5BarCodeStyleSettings<T>) d).Generator.WideNarrowRatio = (float) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        public float WideNarrowRatio
        {
            get => 
                (float) base.GetValue(Industrial2of5BarCodeStyleSettings<T>.WideNarrowRatioProperty);
            set => 
                base.SetValue(Industrial2of5BarCodeStyleSettings<T>.WideNarrowRatioProperty, value);
        }
    }
}

