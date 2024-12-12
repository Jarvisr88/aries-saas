namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    public abstract class CheckSumStyleSettingsBase<T> : BarCodeStyleSettings<T> where T: BarCodeGeneratorBase, new()
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty CalcCheckSumProperty;

        static CheckSumStyleSettingsBase()
        {
            CheckSumStyleSettingsBase<T>.CalcCheckSumProperty = DependencyProperty.Register("CalcCheckSum", typeof(bool), typeof(CheckSumStyleSettingsBase<T>), new PropertyMetadata(true, new PropertyChangedCallback(CheckSumStyleSettingsBase<T>.CalcCheckSumPropertyChanged)));
        }

        public CheckSumStyleSettingsBase()
        {
            this.CalcCheckSum = base.Generator.CalcCheckSum;
        }

        private static void CalcCheckSumPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CheckSumStyleSettingsBase<T>) d).Generator.CalcCheckSum = (bool) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        public bool CalcCheckSum
        {
            get => 
                (bool) base.GetValue(CheckSumStyleSettingsBase<T>.CalcCheckSumProperty);
            set => 
                base.SetValue(CheckSumStyleSettingsBase<T>.CalcCheckSumProperty, value);
        }
    }
}

