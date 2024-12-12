namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class StringToVisibilityConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            StringToVisibilityConverter converter1 = new StringToVisibilityConverter();
            converter1.Inverse = this.Inverse;
            converter1.HiddenInsteadOfCollapsed = this.HiddenInsteadOfCollapsed;
            return converter1;
        }

        public bool Inverse { get; set; }

        public bool HiddenInsteadOfCollapsed { get; set; }
    }
}

