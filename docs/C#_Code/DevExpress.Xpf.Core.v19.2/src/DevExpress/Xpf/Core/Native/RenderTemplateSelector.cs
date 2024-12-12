namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;
    using System.Windows.Markup;

    public class RenderTemplateSelector : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider);
        public virtual RenderTemplate SelectTemplate(FrameworkElement chrome, FrameworkRenderElementContext context, object content);
    }
}

