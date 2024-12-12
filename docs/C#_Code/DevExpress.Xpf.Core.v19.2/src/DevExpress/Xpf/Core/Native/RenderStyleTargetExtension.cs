namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class RenderStyleTargetExtension : MarkupExtension
    {
        public RenderStyleTargetExtension();
        public RenderStyleTargetExtension(string value);
        public override object ProvideValue(IServiceProvider serviceProvider);

        public string Value { get; set; }

        public Type TargetType { get; set; }

        public string TargetName { get; set; }
    }
}

