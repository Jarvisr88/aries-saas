namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public class ChromeRenderTemplateSelector : RenderTemplateSelector
    {
        public static readonly RenderTemplateSelector Default;

        static ChromeRenderTemplateSelector();
        public override RenderTemplate SelectTemplate(FrameworkElement chrome, FrameworkRenderElementContext context, object content);
    }
}

