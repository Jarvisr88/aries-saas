namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;
    using System.Xaml;

    public abstract class BindingExtensionBase : MarkupExtension
    {
        protected object defaultFallbackValue;

        public BindingExtensionBase()
        {
            this.Mode = BindingMode.Default;
            this.defaultFallbackValue = DependencyProperty.UnsetValue;
            this.FallbackValue = this.defaultFallbackValue;
        }

        protected virtual Binding CreateBinding(ResourceDictionary resources)
        {
            Binding binding1 = new Binding();
            binding1.Mode = this.Mode;
            binding1.Path = this.BindingPath;
            binding1.RelativeSource = this.RelativeSource;
            binding1.Converter = this.GetConverter(resources);
            binding1.ConverterParameter = this.ConverterParameter;
            binding1.StringFormat = this.StringFormat;
            binding1.UpdateSourceTrigger = this.UpdateSourceTrigger;
            binding1.FallbackValue = this.GetFallbackValue(resources);
            return binding1;
        }

        private IValueConverter GetConverter(ResourceDictionary resources) => 
            (this.Converter != null) ? ((IValueConverter) resources[this.Converter]) : null;

        private object GetFallbackValue(ResourceDictionary resources) => 
            !string.IsNullOrEmpty(this.FallbackValueResourceKey) ? resources[this.FallbackValueResourceKey] : this.FallbackValue;

        private static ResourceDictionary GetResources(IServiceProvider serviceProvider)
        {
            IRootObjectProvider service = (IRootObjectProvider) serviceProvider.GetService(typeof(IRootObjectProvider));
            if (service.RootObject is ResourceDictionary)
            {
                return (ResourceDictionary) service.RootObject;
            }
            if (!(service.RootObject is FrameworkElement))
            {
                throw new NotSupportedException();
            }
            return ((FrameworkElement) service.RootObject).Resources;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if ((this.FallbackValue != this.defaultFallbackValue) && !string.IsNullOrEmpty(this.FallbackValueResourceKey))
            {
                throw new ArgumentException("It is incorrect to\x00a0use FallbackValue and FallbackValueResourceKey together");
            }
            ResourceDictionary resources = GetResources(serviceProvider);
            return this.CreateBinding(resources);
        }

        public BindingMode Mode { get; set; }

        public string Path { get; set; }

        public string Converter { get; set; }

        public object ConverterParameter { get; set; }

        public string StringFormat { get; set; }

        public System.Windows.Data.UpdateSourceTrigger UpdateSourceTrigger { get; set; }

        public object FallbackValue { get; set; }

        public string FallbackValueResourceKey { get; set; }

        protected abstract PropertyPath BindingPath { get; }

        protected abstract System.Windows.Data.RelativeSource RelativeSource { get; }
    }
}

