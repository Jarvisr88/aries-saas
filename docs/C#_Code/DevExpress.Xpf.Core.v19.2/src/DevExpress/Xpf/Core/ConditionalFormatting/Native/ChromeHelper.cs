namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Core.Native;
    using System;

    public static class ChromeHelper
    {
        public static FrameworkRenderElementContext CreateContext(IChrome chrome, RenderTemplate template)
        {
            Namescope namescopeHolder = new Namescope(chrome);
            return template.CreateContext(namescopeHolder, chrome);
        }
    }
}

