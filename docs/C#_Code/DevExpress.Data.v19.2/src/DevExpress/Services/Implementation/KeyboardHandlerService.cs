namespace DevExpress.Services.Implementation
{
    using DevExpress.Services;
    using DevExpress.Utils;
    using DevExpress.Utils.KeyboardHandler;
    using System;
    using System.Windows.Forms;

    public abstract class KeyboardHandlerService : IKeyboardHandlerService
    {
        private readonly DevExpress.Utils.KeyboardHandler.KeyboardHandler handler;

        protected KeyboardHandlerService(DevExpress.Utils.KeyboardHandler.KeyboardHandler handler)
        {
            Guard.ArgumentNotNull(handler, "handler");
            this.handler = handler;
        }

        public abstract object CreateContext();
        public virtual void OnKeyDown(KeyEventArgs e)
        {
            this.Handler.Context = this.CreateContext();
            e.Handled = this.Handler.HandleKey(e.KeyData);
        }

        public virtual void OnKeyPress(KeyPressEventArgs e)
        {
            if (this.Handler.IsValidChar(e.KeyChar))
            {
                this.Handler.Context = this.CreateContext();
                this.Handler.HandleKeyPress(e.KeyChar, Control.ModifierKeys);
            }
        }

        public virtual void OnKeyUp(KeyEventArgs e)
        {
            this.Handler.Context = this.CreateContext();
            e.Handled = this.Handler.HandleKeyUp(e.KeyData);
        }

        protected virtual DevExpress.Utils.KeyboardHandler.KeyboardHandler Handler =>
            this.handler;
    }
}

