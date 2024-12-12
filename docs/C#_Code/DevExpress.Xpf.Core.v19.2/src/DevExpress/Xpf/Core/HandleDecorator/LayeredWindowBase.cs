namespace DevExpress.Xpf.Core.HandleDecorator
{
    using System;
    using System.Windows.Forms;

    public class LayeredWindowBase : NativeWindow, IDisposable
    {
        protected IntPtr hWndParent = IntPtr.Zero;
        private int WS_POPUP = -2147483648;

        public override void CreateHandle(CreateParams cp)
        {
            cp.Parent = this.hWndParent;
            cp.ExStyle = 0x80800a0;
            cp.Style = this.WS_POPUP;
            base.CreateHandle(cp);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this.IsCreated)
            {
                this.DestroyHandle();
            }
        }

        public void EnsureHandle()
        {
            if (!this.IsCreated)
            {
                this.CreateHandle(new CreateParams());
            }
        }

        public IntPtr Handle
        {
            get
            {
                if (!this.IsCreated)
                {
                    this.EnsureHandle();
                }
                return base.Handle;
            }
        }

        public bool IsCreated =>
            base.Handle != IntPtr.Zero;
    }
}

