namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class DXMessageBoxStringIdExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            DXMessageBoxLocalizer.GetString(this.StringId);

        public DXMessageBoxStringId StringId { get; set; }
    }
}

