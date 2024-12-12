namespace DevExpress.Xpf.Editors.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;

    public abstract class VisualClientOwner : DependencyObject
    {
        private FrameworkElement innerEditor;

        protected VisualClientOwner(PopupBaseEdit editor)
        {
            this.OpenPopupLocker = new Locker();
            this.Editor = editor;
        }

        public virtual void BeforePopupOpened()
        {
            this.OpenPopupLocker.Lock();
            this.ClearInnerEditor(null);
            if (this.InnerEditor != null)
            {
                this.SetupEditor();
            }
        }

        public void BeforeProcessKeyDown()
        {
            this.BeforeProcessKeyDownInternal();
        }

        protected virtual void BeforeProcessKeyDownInternal()
        {
        }

        protected virtual void ClearInnerEditor(FrameworkElement editor)
        {
            this.innerEditor = null;
        }

        protected abstract FrameworkElement FindEditor();
        public virtual void InnerEditorMouseMove(object sender, MouseEventArgs e)
        {
            this.PostPopupValue = true;
        }

        public virtual bool IsClosePopupWithAcceptGesture(Key key, ModifierKeys modifiers) => 
            key == Key.Return;

        public virtual bool IsClosePopupWithCancelGesture(Key key, ModifierKeys modifiers) => 
            key == Key.Escape;

        public virtual void PopupClosed()
        {
            this.UnsubscribeEvents();
            this.OpenPopupLocker.Unlock();
            this.PostPopupValue = false;
        }

        public virtual void PopupContentLoaded()
        {
            if (this.IsLoaded)
            {
                this.SetupEditor();
            }
        }

        public virtual void PopupDestroyed()
        {
            this.ClearInnerEditor(this.innerEditor);
        }

        public virtual void PopupOpened()
        {
            this.SubscribeEvents();
        }

        public virtual void PreviewTextInput(TextCompositionEventArgs e)
        {
        }

        public bool ProcessKeyDown(KeyEventArgs e)
        {
            if (e.Handled)
            {
                return true;
            }
            bool flag = this.ProcessKeyDownInternal(e);
            this.PostPopupValue = e.Handled;
            return flag;
        }

        protected abstract bool ProcessKeyDownInternal(KeyEventArgs e);
        public bool ProcessPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Handled)
            {
                return true;
            }
            bool flag = this.ProcessPreviewKeyDownInternal(e);
            this.PostPopupValue = e.Handled;
            return flag;
        }

        protected abstract bool ProcessPreviewKeyDownInternal(KeyEventArgs e);
        protected abstract void SetupEditor();
        protected virtual void SubscribeEvents()
        {
            if (this.InnerEditor != null)
            {
                this.InnerEditor.MouseMove += new MouseEventHandler(this.InnerEditorMouseMove);
            }
        }

        public abstract void SyncProperties(bool syncDataSource);
        public virtual void SyncValues(bool resetTotal = false)
        {
        }

        protected virtual void UnsubscribeEvents()
        {
            if (this.InnerEditor != null)
            {
                this.InnerEditor.MouseMove -= new MouseEventHandler(this.InnerEditorMouseMove);
            }
        }

        private Locker OpenPopupLocker { get; set; }

        public bool IsPopupOpened =>
            this.OpenPopupLocker.IsLocked;

        public bool PostPopupValue { get; protected set; }

        public FrameworkElement InnerEditor
        {
            get
            {
                if (!LookUpEditHelper.HasPopupContent(this.Editor))
                {
                    return null;
                }
                FrameworkElement innerEditor = this.innerEditor;
                if (this.innerEditor == null)
                {
                    FrameworkElement local1 = this.innerEditor;
                    innerEditor = this.innerEditor = this.FindEditor();
                }
                return innerEditor;
            }
        }

        protected virtual bool HasInnerEditor =>
            this.innerEditor != null;

        protected virtual bool IsLoaded =>
            this.IsPopupOpened && (this.InnerEditor != null);

        public bool IsKeyboardFocusWithin =>
            this.IsLoaded && this.InnerEditor.GetIsKeyboardFocusWithin();

        protected PopupBaseEdit Editor { get; private set; }
    }
}

