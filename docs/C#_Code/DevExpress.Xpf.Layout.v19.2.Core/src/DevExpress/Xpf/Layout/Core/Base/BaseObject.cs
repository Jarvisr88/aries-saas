namespace DevExpress.Xpf.Layout.Core.Base
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class BaseObject : IDisposable
    {
        private int lockUpdateCounter;
        private bool isDisposingCore;

        public event EventHandler Changed;

        public event EventHandler Disposed;

        protected BaseObject()
        {
            this.OnCreate();
        }

        public void BeginUpdate()
        {
            this.lockUpdateCounter++;
        }

        public void CancelUpdate()
        {
            this.lockUpdateCounter--;
        }

        public void Dispose()
        {
            if (!this.IsDisposing)
            {
                this.isDisposingCore = true;
                this.BeginUpdate();
                this.OnDispose();
                this.CancelUpdate();
                this.RaiseDisposed(EventArgs.Empty);
                this.Changed = null;
                this.Disposed = null;
                GC.SuppressFinalize(this);
            }
        }

        public void EndUpdate()
        {
            int num = this.lockUpdateCounter - 1;
            this.lockUpdateCounter = num;
            if (num == 0)
            {
                this.OnUnlockUpdate();
            }
        }

        protected virtual void OnCreate()
        {
        }

        protected virtual void OnDispose()
        {
        }

        protected void OnObjectChanged()
        {
            if (!this.IsUpdateLocked && !this.IsDisposing)
            {
                this.BeginUpdate();
                this.OnUpdateObjectCore();
                this.RaiseChanged(EventArgs.Empty);
                this.CancelUpdate();
            }
        }

        protected void OnUnlockUpdate()
        {
            this.OnObjectChanged();
        }

        protected virtual void OnUpdateObjectCore()
        {
        }

        protected void RaiseChanged(EventArgs e)
        {
            if (this.Changed != null)
            {
                this.Changed(this, e);
            }
        }

        protected void RaiseDisposed(EventArgs e)
        {
            if (this.Disposed != null)
            {
                this.Disposed(this, e);
            }
        }

        public bool IsDisposing =>
            this.isDisposingCore;

        public bool IsUpdateLocked =>
            this.lockUpdateCounter > 0;
    }
}

