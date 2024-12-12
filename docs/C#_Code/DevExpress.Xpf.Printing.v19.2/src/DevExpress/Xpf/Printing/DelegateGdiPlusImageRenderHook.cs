namespace DevExpress.Xpf.Printing
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class DelegateGdiPlusImageRenderHook : GdiPlusImageRenderHook
    {
        private readonly Action<Graphics> configureGraphics;

        public DelegateGdiPlusImageRenderHook(Action<Graphics> configureGraphics)
        {
            Guard.ArgumentNotNull(configureGraphics, "configureGraphics");
            this.configureGraphics = configureGraphics;
        }

        public override void BeforeRender(Graphics graphics)
        {
            this.configureGraphics(graphics);
        }
    }
}

