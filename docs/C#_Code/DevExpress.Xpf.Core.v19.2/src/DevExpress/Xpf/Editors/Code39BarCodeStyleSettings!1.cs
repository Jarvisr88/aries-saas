namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    public abstract class Code39BarCodeStyleSettings<T> : CheckSumStyleSettingsBase<T> where T: Code39Generator, new()
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty WideNarrowRatioProperty;

        static Code39BarCodeStyleSettings()
        {
            Code39BarCodeStyleSettings<T>.WideNarrowRatioProperty = DependencyProperty.Register("WideNarrowRatio", typeof(float), typeof(Code39BarCodeStyleSettings<T>), new PropertyMetadata(3f, new PropertyChangedCallback(Code39BarCodeStyleSettings<T>.WideNarrowRatioPropertyChanged)));
        }

        protected Code39BarCodeStyleSettings()
        {
        }

        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            ((BarCodePropertyProvider) editor.PropertyProvider).WideNarrowRatio = this.WideNarrowRatio;
        }

        private static void WideNarrowRatioPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Code39BarCodeStyleSettings<T>) d).Generator.WideNarrowRatio = (float) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        public float WideNarrowRatio
        {
            get => 
                (float) base.GetValue(Code39BarCodeStyleSettings<T>.WideNarrowRatioProperty);
            set => 
                base.SetValue(Code39BarCodeStyleSettings<T>.WideNarrowRatioProperty, value);
        }
    }
}

