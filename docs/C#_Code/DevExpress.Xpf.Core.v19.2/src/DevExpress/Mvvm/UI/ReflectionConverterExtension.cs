namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class ReflectionConverterExtension : MarkupExtension
    {
        private Type convertBackMethodOwner = typeof(TypeUnsetValue);

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            ReflectionConverter converter1 = new ReflectionConverter();
            converter1.ConvertMethodOwner = this.ConvertMethodOwner;
            converter1.ConvertMethod = this.ConvertMethod;
            converter1.ConvertBackMethodOwner = this.ConvertBackMethodOwner;
            converter1.ConvertBackMethod = this.ConvertBackMethod;
            return converter1;
        }

        public Type ConvertMethodOwner { get; set; }

        public string ConvertMethod { get; set; }

        public Type ConvertBackMethodOwner
        {
            get => 
                (this.convertBackMethodOwner == typeof(TypeUnsetValue)) ? this.ConvertMethodOwner : this.convertBackMethodOwner;
            set => 
                this.convertBackMethodOwner = value;
        }

        public string ConvertBackMethod { get; set; }

        private class TypeUnsetValue
        {
        }
    }
}

