namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class FloatingMultiTemplateControl : MultiTemplateControl
    {
        private bool fHidden;
        private bool fHideRequested;
        private bool fTemplateApplied;

        private void ClearTemplate()
        {
            if (this.fHideRequested)
            {
                this.fHideRequested = false;
                DependencyObject child = LayoutItemsHelper.GetChild<DependencyObject>(this);
                if (child is IDisposable)
                {
                    ((IDisposable) child).Dispose();
                }
                base.ClearValue(Control.TemplateProperty);
                if (child != null)
                {
                    DockLayoutManager.Release(child);
                }
                this.fHidden = true;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.fTemplateApplied = true;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            this.UpdateTemplateOnLoadOrUnload();
        }

        protected override void OnUnloaded()
        {
            base.OnUnloaded();
            this.UpdateTemplateOnLoadOrUnload();
        }

        internal void QueryClearTemplate()
        {
            this.fHideRequested = true;
            this.ClearTemplate();
        }

        private void UpdateTemplateOnLoadOrUnload()
        {
            if (base.Container != null)
            {
                if (base.IsLoaded || !base.Container.CloseFloatWindowsOnManagerUnloading)
                {
                    if (this.fHideRequested)
                    {
                        this.fHideRequested = false;
                    }
                    if (this.fHidden || !this.fTemplateApplied)
                    {
                        this.SelectTemplate(base.LayoutItem);
                        (base.Container ?? DockLayoutManager.GetDockLayoutManager(this)).Do<DockLayoutManager>(x => x.InvalidateView(base.LayoutItem.GetRoot()));
                    }
                }
                else if ((base.Container.GetRealFloatingMode() == DevExpress.Xpf.Core.FloatingMode.Window) && this.fTemplateApplied)
                {
                    this.fHideRequested = true;
                    base.Dispatcher.BeginInvoke(new Action(this.ClearTemplate), new object[0]);
                }
            }
        }
    }
}

