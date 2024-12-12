namespace DevExpress.Xpf.DXBinding
{
    using System;
    using System.Windows;
    using System.Windows.Data;

    public abstract class DXTriggerBase : Freezable
    {
        private BindingBase binding;
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(DXTriggerBase), new PropertyMetadata(null));

        protected DXTriggerBase()
        {
        }

        protected override Freezable CreateInstanceCore()
        {
            throw new NotSupportedException();
        }

        public BindingBase Binding
        {
            get => 
                this.binding;
            set
            {
                base.WritePreamble();
                this.binding = value;
            }
        }

        public object Value
        {
            get => 
                base.GetValue(ValueProperty);
            set => 
                base.SetValue(ValueProperty, value);
        }
    }
}

