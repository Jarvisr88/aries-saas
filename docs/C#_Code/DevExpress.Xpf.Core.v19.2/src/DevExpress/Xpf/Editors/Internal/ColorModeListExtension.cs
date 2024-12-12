namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.Windows.Markup;

    public class ColorModeListExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            ColorBase.ComboBoxItemsSource;
    }
}

