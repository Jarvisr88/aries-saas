namespace DevExpress.Xpf.Editors.EditStrategy
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class UpdateDataSourceOnAsyncProcessNewValueEventHelper : IDisposable
    {
        public UpdateDataSourceOnAsyncProcessNewValueEventHelper(LookUpEditBase editor)
        {
            this.Editor = editor;
            this.InProcessLocker = new Locker();
        }

        internal void ClearFloatingContainer(object child)
        {
            if (child == this.Window)
            {
                this.UpdateDataInternal(this.DoUpdate);
                this.DisposeInternal();
            }
        }

        internal void DisposeInternal()
        {
            this.InProcessLocker.Unlock();
            this.ResetWindow();
            if (!this.Editor.GetIsEditorKeyboardFocused())
            {
                this.Editor.Focus();
            }
            this.DoUpdate = false;
        }

        private void ResetWindow()
        {
            this.Window = null;
        }

        public void SetFloatingContainer(object element)
        {
            if (this.InProcessLocker)
            {
                this.Window = (FrameworkElement) element;
                this.Window.Focus();
            }
        }

        internal IDisposable Subscribe()
        {
            this.InProcessLocker.Lock();
            return this;
        }

        void IDisposable.Dispose()
        {
            if (this.InProcessLocker && !this.DoUpdate)
            {
                this.DisposeInternal();
            }
        }

        public void UpdateDataAsync()
        {
            if (this.Window != null)
            {
                this.DoUpdate = true;
            }
            else
            {
                this.UpdateDataInternal(true);
            }
        }

        private void UpdateDataInternal(bool force)
        {
            if (force)
            {
                this.Editor.ItemsProvider.DoRefresh();
                this.Editor.EditStrategy.ValidateOnEnterKeyPressed(null);
            }
        }

        private LookUpEditBase Editor { get; set; }

        private FrameworkElement Window { get; set; }

        private bool DoUpdate { get; set; }

        private Locker InProcessLocker { get; set; }

        public bool LockerByProcessNewValueWindow =>
            this.IsInProcessNewValue && (this.Window != null);

        public bool IsInProcessNewValue =>
            this.InProcessLocker.IsLocked;
    }
}

