namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class ObjectToBooleanConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            ObjectToBooleanConverter converter1 = new ObjectToBooleanConverter();
            converter1.Inverse = this.Inverse;
            return converter1;
        }

        public bool Inverse { get; set; }
    }
}

