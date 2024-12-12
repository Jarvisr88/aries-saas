namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public class FunctionBindingBehavior : FunctionBindingBehaviorBase
    {
        public static readonly DependencyProperty PropertyProperty;
        public static readonly DependencyProperty ConverterProperty;
        public static readonly DependencyProperty ConverterParameterProperty;
        public static readonly DependencyProperty FunctionProperty;

        static FunctionBindingBehavior()
        {
            PropertyProperty = DependencyProperty.Register("Property", typeof(string), typeof(FunctionBindingBehavior), new PropertyMetadata(null, (d, e) => ((FunctionBindingBehavior) d).OnResultAffectedPropertyChanged()));
            ConverterProperty = DependencyProperty.Register("Converter", typeof(IValueConverter), typeof(FunctionBindingBehavior), new PropertyMetadata(null, (d, e) => ((FunctionBindingBehavior) d).OnResultAffectedPropertyChanged()));
            ConverterParameterProperty = DependencyProperty.Register("ConverterParameter", typeof(object), typeof(FunctionBindingBehavior), new PropertyMetadata(null, (d, e) => ((FunctionBindingBehavior) d).OnResultAffectedPropertyChanged()));
            FunctionProperty = DependencyProperty.Register("Function", typeof(string), typeof(FunctionBindingBehavior), new PropertyMetadata(null, (d, e) => ((FunctionBindingBehavior) d).OnResultAffectedPropertyChanged()));
        }

        protected object GetSourceMethodValue()
        {
            object result = InvokeSourceFunction(base.ActualSource, this.ActualFunction, GetArgsInfo(this), new Func<MethodInfo, Type, string, bool>(FunctionBindingBehaviorBase.DefaultMethodInfoChecker));
            return this.Converter.Return<IValueConverter, object>(x => x.Convert(result, null, this.ConverterParameter, CultureInfo.InvariantCulture), () => result);
        }

        protected override void OnResultAffectedPropertyChanged()
        {
            if ((base.ActualTarget != null) && ((base.ActualSource != null) && (!string.IsNullOrEmpty(this.ActualFunction) && (!string.IsNullOrEmpty(this.Property) && base.IsAttached))))
            {
                Action<object> action = GetObjectPropertySetter(base.ActualTarget, this.Property, false);
                if (action != null)
                {
                    object sourceMethodValue = this.GetSourceMethodValue();
                    if (sourceMethodValue != DependencyProperty.UnsetValue)
                    {
                        action(sourceMethodValue);
                    }
                }
            }
        }

        public string Property
        {
            get => 
                (string) base.GetValue(PropertyProperty);
            set => 
                base.SetValue(PropertyProperty, value);
        }

        public IValueConverter Converter
        {
            get => 
                (IValueConverter) base.GetValue(ConverterProperty);
            set => 
                base.SetValue(ConverterProperty, value);
        }

        public object ConverterParameter
        {
            get => 
                base.GetValue(ConverterParameterProperty);
            set => 
                base.SetValue(ConverterParameterProperty, value);
        }

        public string Function
        {
            get => 
                (string) base.GetValue(FunctionProperty);
            set => 
                base.SetValue(FunctionProperty, value);
        }

        protected override string ActualFunction =>
            this.Function;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FunctionBindingBehavior.<>c <>9 = new FunctionBindingBehavior.<>c();

            internal void <.cctor>b__21_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FunctionBindingBehavior) d).OnResultAffectedPropertyChanged();
            }

            internal void <.cctor>b__21_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FunctionBindingBehavior) d).OnResultAffectedPropertyChanged();
            }

            internal void <.cctor>b__21_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FunctionBindingBehavior) d).OnResultAffectedPropertyChanged();
            }

            internal void <.cctor>b__21_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FunctionBindingBehavior) d).OnResultAffectedPropertyChanged();
            }
        }
    }
}

