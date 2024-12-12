namespace DevExpress.Xpf.Layout.Core
{
    using DevExpress.Xpf.Layout.Core.Base;
    using DevExpress.Xpf.Layout.Core.Platform;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public abstract class UIService : BaseObject, IUIService, IDisposable
    {
        private int lockEventProcessing;
        public static Point InvalidPoint = new Point(double.NaN, double.NaN);

        protected UIService()
        {
        }

        public void BeginEvent(IView sender)
        {
            this.lockEventProcessing++;
            this.Sender = sender;
            this.BeginEventOverride(sender);
        }

        protected virtual void BeginEventOverride(IView sender)
        {
        }

        [DebuggerStepThrough]
        private static DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs CheckMouseArgs(DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs ea) => 
            ea ?? new DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs(InvalidPoint, MouseButtons.None);

        public void EndEvent()
        {
            this.EndEventOverride();
            this.Sender = null;
            this.lockEventProcessing--;
        }

        protected virtual void EndEventOverride()
        {
        }

        protected virtual Key[] GetKeys() => 
            new Key[0];

        public bool ProcessKey(IView view, KeyEventType eventype, Key key)
        {
            if (base.IsDisposing || (this.IsInEvent || (view == null)))
            {
                return false;
            }
            bool flag = false;
            this.BeginEvent(view);
            if (Array.IndexOf<Key>(this.GetKeys(), key) != -1)
            {
                flag = this.ProcessKeyOverride(view, eventype, key);
            }
            this.EndEvent();
            return flag;
        }

        protected virtual bool ProcessKeyOverride(IView view, KeyEventType eventype, Key key) => 
            false;

        public bool ProcessMouse(IView view, MouseEventType eventType, DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs ea)
        {
            if (base.IsDisposing || (this.IsInEvent || (view == null)))
            {
                return false;
            }
            this.BeginEvent(view);
            bool flag = this.ProcessMouseOverride(view, eventType, CheckMouseArgs(ea));
            this.EndEvent();
            return flag;
        }

        protected virtual bool ProcessMouseOverride(IView view, MouseEventType eventType, DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs ea) => 
            false;

        public bool IsInEvent =>
            this.lockEventProcessing > 0;

        public IView Sender { get; private set; }
    }
}

