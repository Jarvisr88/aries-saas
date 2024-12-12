namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public class BoolToObjectConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty TrueValueProperty;
        public static readonly DependencyProperty FalseValueProperty;
        private object trueValue;
        private object falseValue;

        static BoolToObjectConverter()
        {
            TrueValueProperty = DependencyProperty.Register("TrueValue", typeof(object), typeof(BoolToObjectConverter), new PropertyMetadata(null, (d, e) => ((BoolToObjectConverter) d).UpdateCachedValues()));
            FalseValueProperty = DependencyProperty.Register("FalseValue", typeof(object), typeof(BoolToObjectConverter), new PropertyMetadata(null, (d, e) => ((BoolToObjectConverter) d).UpdateCachedValues()));
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object obj2 = System.Convert.ToBoolean(value) ? this.TrueValue : this.FalseValue;
            SealHelper.SealIfSealable(obj2);
            return ConverterHelper.Convert(obj2, targetType);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private void UpdateCachedValues()
        {
            this.trueValue = base.GetValue(TrueValueProperty);
            this.falseValue = base.GetValue(FalseValueProperty);
        }

        public object TrueValue
        {
            get => 
                this.trueValue;
            set => 
                base.SetValue(TrueValueProperty, value);
        }

        public object FalseValue
        {
            get => 
                this.falseValue;
            set => 
                base.SetValue(FalseValueProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BoolToObjectConverter.<>c <>9 = new BoolToObjectConverter.<>c();

            internal void <.cctor>b__14_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BoolToObjectConverter) d).UpdateCachedValues();
            }

            internal void <.cctor>b__14_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BoolToObjectConverter) d).UpdateCachedValues();
            }
        }
    }
}

