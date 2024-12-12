namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class StringToBooleanConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            StringToBooleanConverter converter1 = new StringToBooleanConverter();
            converter1.Inverse = this.Inverse;
            return converter1;
        }

        public bool Inverse { get; set; }
    }
}

