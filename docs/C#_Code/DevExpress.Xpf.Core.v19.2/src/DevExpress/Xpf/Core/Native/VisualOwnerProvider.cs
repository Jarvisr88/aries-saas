namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Markup;

    public class VisualOwnerProvider : MarkupExtension
    {
        public static readonly VisualOwnerProvider Default;
        private readonly bool isDefault;
        private FrameworkElement cachedOwner;

        static VisualOwnerProvider();
        public VisualOwnerProvider();
        protected VisualOwnerProvider(bool isDefault);
        public FrameworkElement GetVisualOwner(IChrome chrome);
        public override object ProvideValue(IServiceProvider serviceProvider);
        public void Reset();

        public Type ParentType { get; set; }
    }
}

