namespace DevExpress.Services.Implementation
{
    using DevExpress.Services;
    using DevExpress.Utils;
    using System;
    using System.Windows.Forms;

    public class MouseHandlerService : IMouseHandlerService
    {
        private MouseHandler handler;

        public MouseHandlerService(MouseHandler handler)
        {
            Guard.ArgumentNotNull(handler, "handler");
            this.handler = handler;
        }

        public virtual void OnMouseDown(MouseEventArgs e)
        {
            this.Handler.OnMouseDown(e);
        }

        public virtual void OnMouseMove(MouseEventArgs e)
        {
            this.Handler.OnMouseMove(e);
        }

        public virtual void OnMouseUp(MouseEventArgs e)
        {
            this.Handler.OnMouseUp(e);
        }

        public virtual void OnMouseWheel(MouseEventArgs e)
        {
            this.Handler.OnMouseWheel(e);
        }

        public virtual MouseHandler Handler =>
            this.handler;
    }
}

