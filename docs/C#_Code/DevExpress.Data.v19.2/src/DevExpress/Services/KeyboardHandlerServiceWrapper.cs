namespace DevExpress.Services
{
    using DevExpress.Utils;
    using System;
    using System.Windows.Forms;

    public class KeyboardHandlerServiceWrapper : IKeyboardHandlerService
    {
        private IKeyboardHandlerService service;

        public KeyboardHandlerServiceWrapper(IKeyboardHandlerService service)
        {
            Guard.ArgumentNotNull(service, "service");
            this.service = service;
        }

        public virtual void OnKeyDown(KeyEventArgs e)
        {
            this.Service.OnKeyDown(e);
        }

        public virtual void OnKeyPress(KeyPressEventArgs e)
        {
            this.Service.OnKeyPress(e);
        }

        public virtual void OnKeyUp(KeyEventArgs e)
        {
            this.Service.OnKeyUp(e);
        }

        public IKeyboardHandlerService Service =>
            this.service;
    }
}

