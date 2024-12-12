namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class PreviewStringIdExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            PreviewLocalizer.GetString(this.StringId);

        public PreviewStringId StringId { get; set; }
    }
}

