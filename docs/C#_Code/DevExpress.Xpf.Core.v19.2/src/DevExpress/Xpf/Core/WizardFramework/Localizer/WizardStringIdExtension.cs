namespace DevExpress.Xpf.Core.WizardFramework.Localizer
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class WizardStringIdExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            WizardLocalizer.GetString(this.StringId);

        public WizardStringId StringId { get; set; }
    }
}

