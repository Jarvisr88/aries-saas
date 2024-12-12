namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    public abstract class Interleaved2of5BarCodeStyleSettings<T> : CheckSumStyleSettingsBase<T> where T: Interleaved2of5Generator, new()
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty WideNarrowRatioProperty;

        static Interleaved2of5BarCodeStyleSettings()
        {
            Interleaved2of5BarCodeStyleSettings<T>.WideNarrowRatioProperty = DependencyProperty.Register("WideNarrowRatio", typeof(float), typeof(Interleaved2of5BarCodeStyleSettings<T>), new PropertyMetadata(3f, new PropertyChangedCallback(Interleaved2of5BarCodeStyleSettings<T>.WideNarrowRatioPropertyChanged)));
        }

        protected Interleaved2of5BarCodeStyleSettings()
        {
        }

        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            ((BarCodePropertyProvider) editor.PropertyProvider).WideNarrowRatio = this.WideNarrowRatio;
        }

        private static void WideNarrowRatioPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Interleaved2of5BarCodeStyleSettings<T>) d).Generator.WideNarrowRatio = (float) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        public float WideNarrowRatio
        {
            get => 
                (float) base.GetValue(Interleaved2of5BarCodeStyleSettings<T>.WideNarrowRatioProperty);
            set => 
                base.SetValue(Interleaved2of5BarCodeStyleSettings<T>.WideNarrowRatioProperty, value);
        }
    }
}

