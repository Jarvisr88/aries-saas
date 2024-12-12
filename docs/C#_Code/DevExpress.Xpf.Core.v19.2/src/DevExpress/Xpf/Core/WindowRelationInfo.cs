namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Threading;

    internal class WindowRelationInfo
    {
        private bool isParentClosed;

        public event EventHandler ParentClosed;

        protected WindowRelationInfo()
        {
        }

        protected internal WindowRelationInfo(WindowContainer parent)
        {
            this.AttachParent(parent);
        }

        public void AttachChild(Window child, bool isChildHandleRequired)
        {
            if (this.Child != null)
            {
                throw new ArgumentException("Child property is already set");
            }
            this.Child = new WindowContainer(child, isChildHandleRequired);
            this.ChildAttachedOverride();
            this.CompleteContainerInitialization(this.Child);
        }

        protected void AttachParent(WindowContainer parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException("Parent");
            }
            this.Parent = parent;
            this.CompleteContainerInitialization(this.Parent);
        }

        protected virtual void ChildAttachedOverride()
        {
        }

        private void CompleteContainerInitialization(WindowContainer container)
        {
            if (!container.IsInitialized)
            {
                container.Initialized += new EventHandler(this.OnContainerInitialized);
            }
            else
            {
                if (ReferenceEquals(container, this.Parent))
                {
                    this.SubscribeParentEvents();
                }
                else
                {
                    this.SubscribeChildEvents();
                }
                this.CompleteInitialization();
            }
        }

        private void CompleteInitialization()
        {
            if (!this.IsReleased && (!this.IsInitialized && ((this.Child != null) && (this.Child.IsInitialized && (this.Parent.Handle != IntPtr.Zero)))))
            {
                this.CompleteInitializationOverride();
                SplashScreenHelper.InvokeAsync(this.Child, new Action(this.SetChildParent), DispatcherPriority.Normal, AsyncInvokeMode.AsyncOnly);
                this.IsInitialized = true;
            }
        }

        protected virtual void CompleteInitializationOverride()
        {
        }

        private void OnChildClosed(object sender, EventArgs e)
        {
            this.UnsubscribeChildEvents();
        }

        private void OnContainerInitialized(object sender, EventArgs e)
        {
            ((WindowContainer) sender).Initialized -= new EventHandler(this.OnContainerInitialized);
            this.CompleteContainerInitialization((WindowContainer) sender);
        }

        protected virtual void OnParentClosed(object sender, EventArgs e)
        {
            this.isParentClosed = true;
            this.ParentClosed.Do<EventHandler>(x => x(this, EventArgs.Empty));
        }

        public virtual void Release()
        {
            if (!this.IsReleased)
            {
                this.IsReleased = true;
                this.UnsubscribeChildEvents();
                this.UnsubscribeParentEvents();
                this.Child.Initialized -= new EventHandler(this.OnContainerInitialized);
                this.Parent.Initialized -= new EventHandler(this.OnContainerInitialized);
                this.Child = null;
                this.Parent = null;
            }
        }

        private void SetChildParent()
        {
            if (!this.IsReleased)
            {
                SplashScreenHelper.SetParent(this.Child.Window, this.Parent.Handle);
            }
        }

        private void SubscribeChildEvents()
        {
            if ((this.Child != null) && (this.Child.Window != null))
            {
                this.Child.Window.Closed += new EventHandler(this.OnChildClosed);
                this.SubscribeChildEventsOverride();
            }
        }

        protected virtual void SubscribeChildEventsOverride()
        {
        }

        private void SubscribeParentEvents()
        {
            if (this.Parent != null)
            {
                this.Parent.Window.Do<Window>(delegate (Window x) {
                    x.Closed += new EventHandler(this.OnParentClosed);
                });
                this.Parent.Form.Do<Form>(delegate (Form x) {
                    x.FormClosed += new FormClosedEventHandler(this.OnParentClosed);
                });
                this.SubscribeParentEventsOverride();
            }
        }

        protected virtual void SubscribeParentEventsOverride()
        {
        }

        private void UnsubscribeChildEvents()
        {
            if ((this.Child != null) && (this.Child.Window != null))
            {
                this.Child.Window.Closed -= new EventHandler(this.OnChildClosed);
                this.UnsubscribeChildEventsOverride();
            }
        }

        protected virtual void UnsubscribeChildEventsOverride()
        {
        }

        private void UnsubscribeParentEvents()
        {
            if (this.Parent != null)
            {
                this.Parent.Window.Do<Window>(delegate (Window x) {
                    x.Closed -= new EventHandler(this.OnParentClosed);
                });
                this.Parent.Form.Do<Form>(delegate (Form x) {
                    x.FormClosed -= new FormClosedEventHandler(this.OnParentClosed);
                });
                this.UnsubscribeParentEventsOverride();
            }
        }

        protected virtual void UnsubscribeParentEventsOverride()
        {
        }

        public WindowContainer Parent { get; private set; }

        public WindowContainer Child { get; private set; }

        public bool IsInitialized { get; private set; }

        public bool IsReleased { get; private set; }

        public bool ActualIsParentClosed =>
            this.isParentClosed || ((this.Parent != null) && this.Parent.IsWindowClosedBeforeInit);
    }
}

