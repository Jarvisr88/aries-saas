namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;
    using System.Windows.Data;

    public class BindingValueEvaluator : FrameworkElement
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(BindingValueEvaluator), null);
        private BindingBase binding;

        public BindingValueEvaluator(BindingBase binding)
        {
            this.binding = binding;
        }

        public object Value
        {
            get
            {
                this.Value = DependencyProperty.UnsetValue;
                base.SetBinding(ValueProperty, this.binding);
                return base.GetValue(ValueProperty);
            }
            private set => 
                base.SetValue(ValueProperty, value);
        }
    }
}

