namespace DevExpress.Xpf.Layout.Core.Actions
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class ActionServiceListener : IActionServiceListener, IUIServiceListener
    {
        protected ActionServiceListener()
        {
        }

        public virtual void OnCollapse()
        {
        }

        public virtual void OnExpand()
        {
        }

        public virtual void OnHide(bool immediately)
        {
        }

        public virtual void OnHideSelection()
        {
        }

        public virtual void OnShowSelection()
        {
        }

        public IUIServiceProvider ServiceProvider { get; set; }

        public object Key =>
            typeof(IActionServiceListener);
    }
}

