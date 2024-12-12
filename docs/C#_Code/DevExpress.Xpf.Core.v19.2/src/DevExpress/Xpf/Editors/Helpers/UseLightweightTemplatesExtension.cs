namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class UseLightweightTemplatesExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this.UseLightweightTemplates;

        public bool UseLightweightTemplates { get; set; }
    }
}

