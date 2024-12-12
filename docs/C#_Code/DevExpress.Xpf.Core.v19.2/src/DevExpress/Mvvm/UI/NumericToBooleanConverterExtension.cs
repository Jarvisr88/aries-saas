namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class NumericToBooleanConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            NumericToBooleanConverter converter1 = new NumericToBooleanConverter();
            converter1.Inverse = this.Inverse;
            return converter1;
        }

        public bool Inverse { get; set; }
    }
}

