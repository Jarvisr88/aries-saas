namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class NumericToVisibilityConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            NumericToVisibilityConverter converter1 = new NumericToVisibilityConverter();
            converter1.Inverse = this.Inverse;
            converter1.HiddenInsteadOfCollapsed = this.HiddenInsteadOfCollapsed;
            return converter1;
        }

        public bool Inverse { get; set; }

        public bool HiddenInsteadOfCollapsed { get; set; }
    }
}

