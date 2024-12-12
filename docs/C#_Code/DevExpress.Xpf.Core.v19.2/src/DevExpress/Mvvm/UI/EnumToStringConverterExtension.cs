namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Windows.Markup;

    public class EnumToStringConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            new EnumToStringConverter();
    }
}

