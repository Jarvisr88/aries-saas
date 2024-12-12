namespace #xOk
{
    using ActiproSoftware.ComponentModel;
    using ActiproSoftware.WinUICore.Rendering;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    internal class #4sk : DisposableObject
    {
        public #4sk(CanvasControl ownerCanvasControl)
        {
            if (ownerCanvasControl != null)
            {
                System.Drawing.Graphics g = ownerCanvasControl.CreateGraphics();
                this.Graphics = g;
                if (g != null)
                {
                    this.Renderer = new #zOk(g, ownerCanvasControl.IsForPrinter);
                }
            }
        }

        protected override void Dispose(bool #Fee)
        {
            if (#Fee)
            {
                #zOk renderer = this.Renderer;
                if (renderer != null)
                {
                    renderer.Dispose();
                    this.Renderer = null;
                }
                System.Drawing.Graphics graphics = this.Graphics;
                if (graphics != null)
                {
                    graphics.Dispose();
                    this.Graphics = null;
                }
            }
        }

        public System.Drawing.Graphics Graphics { get; private set; }

        public #zOk Renderer { get; private set; }
    }
}

