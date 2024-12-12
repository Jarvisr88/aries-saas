namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class DisplayFormatStringExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this.DisplayFormat;

        public string DisplayFormat { get; set; }
    }
}

