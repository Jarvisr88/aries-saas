namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public sealed class DefaultRenderTemplateSelector : RenderTemplateSelector
    {
        private static readonly Lazy<DefaultRenderTemplateSelector> instance;
        private static readonly Func<object, bool> isXmlNode;

        static DefaultRenderTemplateSelector();
        private DefaultRenderTemplateSelector();
        public override RenderTemplate SelectTemplate(FrameworkElement chrome, FrameworkRenderElementContext context, object content);
        public static RenderTemplate SelectTemplate(FrameworkElement chrome, FrameworkRenderElementContext context, object content, bool? forceVisual, ref DataTemplate dataTemplate);

        private static DefaultRenderTemplateSelector Instance { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DefaultRenderTemplateSelector.<>c <>9;

            static <>c();
            internal bool <.cctor>b__4_0(object arg);
            internal DefaultRenderTemplateSelector <.cctor>b__4_1();
        }
    }
}

