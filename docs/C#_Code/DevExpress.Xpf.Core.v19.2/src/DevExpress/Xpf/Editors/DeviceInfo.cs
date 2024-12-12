namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class DeviceInfo : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        public string Moniker { get; set; }

        public string Name { get; set; }
    }
}

