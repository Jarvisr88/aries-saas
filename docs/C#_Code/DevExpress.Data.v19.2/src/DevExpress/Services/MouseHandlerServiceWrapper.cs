namespace DevExpress.Services
{
    using DevExpress.Utils;
    using System;
    using System.Windows.Forms;

    public class MouseHandlerServiceWrapper : IMouseHandlerService
    {
        private IMouseHandlerService service;

        public MouseHandlerServiceWrapper(IMouseHandlerService service)
        {
            Guard.ArgumentNotNull(service, "service");
            this.service = service;
        }

        public virtual void OnMouseDown(MouseEventArgs e)
        {
            this.Service.OnMouseDown(e);
        }

        public virtual void OnMouseMove(MouseEventArgs e)
        {
            this.Service.OnMouseMove(e);
        }

        public virtual void OnMouseUp(MouseEventArgs e)
        {
            this.Service.OnMouseUp(e);
        }

        public virtual void OnMouseWheel(MouseEventArgs e)
        {
            this.Service.OnMouseWheel(e);
        }

        public IMouseHandlerService Service =>
            this.service;
    }
}

