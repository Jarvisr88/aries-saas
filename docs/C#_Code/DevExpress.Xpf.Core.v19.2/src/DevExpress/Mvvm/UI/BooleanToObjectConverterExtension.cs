namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class BooleanToObjectConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            BooleanToObjectConverter converter1 = new BooleanToObjectConverter();
            converter1.TrueValue = this.TrueValue;
            converter1.FalseValue = this.FalseValue;
            converter1.NullValue = this.NullValue;
            return converter1;
        }

        public object TrueValue { get; set; }

        public object FalseValue { get; set; }

        public object NullValue { get; set; }
    }
}

