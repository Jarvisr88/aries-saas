namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Windows.Markup;

    public class TypeCastConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            new TypeCastConverter();
    }
}

