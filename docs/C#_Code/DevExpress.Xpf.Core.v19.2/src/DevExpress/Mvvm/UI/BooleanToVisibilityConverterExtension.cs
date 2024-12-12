namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class BooleanToVisibilityConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            BooleanToVisibilityConverter converter1 = new BooleanToVisibilityConverter();
            converter1.Inverse = this.Inverse;
            converter1.HiddenInsteadOfCollapsed = this.HiddenInsteadOfCollapsed;
            return converter1;
        }

        public bool Inverse { get; set; }

        public bool HiddenInsteadOfCollapsed { get; set; }
    }
}

