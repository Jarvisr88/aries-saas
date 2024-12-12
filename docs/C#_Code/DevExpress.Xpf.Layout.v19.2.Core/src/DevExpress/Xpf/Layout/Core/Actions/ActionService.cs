namespace DevExpress.Xpf.Layout.Core.Actions
{
    using DevExpress.Xpf.Layout.Core;
    using System;

    internal class ActionService : UIService, IActionService, IUIService, IDisposable
    {
        private int actionLockCounter;

        public void Collapse(IView view)
        {
            if (!base.IsDisposing && (!this.InAction && (view != null)))
            {
                this.actionLockCounter++;
                IActionServiceListener uIServiceListener = view.GetUIServiceListener<IActionServiceListener>();
                if (uIServiceListener != null)
                {
                    uIServiceListener.OnCollapse();
                }
                this.actionLockCounter--;
            }
        }

        public void Expand(IView view)
        {
            if (!base.IsDisposing && (!this.InAction && (view != null)))
            {
                this.actionLockCounter++;
                IActionServiceListener uIServiceListener = view.GetUIServiceListener<IActionServiceListener>();
                if (uIServiceListener != null)
                {
                    uIServiceListener.OnExpand();
                }
                this.actionLockCounter--;
            }
        }

        public void Hide(IView view, bool immediatelly)
        {
            if (!base.IsDisposing && (!this.InAction && (view != null)))
            {
                this.actionLockCounter++;
                IActionServiceListener uIServiceListener = view.GetUIServiceListener<IActionServiceListener>();
                if (uIServiceListener != null)
                {
                    uIServiceListener.OnHide(immediatelly);
                }
                this.actionLockCounter--;
            }
        }

        public void HideSelection(IView view)
        {
            if (!base.IsDisposing && (!this.InAction && (view != null)))
            {
                this.actionLockCounter++;
                IActionServiceListener uIServiceListener = view.GetUIServiceListener<IActionServiceListener>();
                if (uIServiceListener != null)
                {
                    uIServiceListener.OnHideSelection();
                }
                this.actionLockCounter--;
            }
        }

        public void ShowSelection(IView view)
        {
            if (!base.IsDisposing && (!this.InAction && (view != null)))
            {
                this.actionLockCounter++;
                IActionServiceListener uIServiceListener = view.GetUIServiceListener<IActionServiceListener>();
                if (uIServiceListener != null)
                {
                    uIServiceListener.OnShowSelection();
                }
                this.actionLockCounter--;
            }
        }

        public bool InAction =>
            this.actionLockCounter > 0;
    }
}

