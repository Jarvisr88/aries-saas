namespace DevExpress.Xpf.Layout.Core.Actions
{
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.UIInteraction;
    using System;

    internal class ContextActionService : UIInteractionService, IContextActionService, IUIService, IDisposable
    {
        public bool CanExecute(IView view, ILayoutElement element, ContextAction action)
        {
            if (base.IsDisposing || ((view == null) || (element == null)))
            {
                return false;
            }
            IContextActionServiceListener uIServiceListener = view.GetUIServiceListener<IContextActionServiceListener>();
            return ((uIServiceListener != null) && uIServiceListener.OnCanExecute(element, action));
        }

        public bool Execute(IView view, ILayoutElement element, ContextAction action)
        {
            if (base.IsDisposing || ((view == null) || (element == null)))
            {
                return false;
            }
            IContextActionServiceListener uIServiceListener = view.GetUIServiceListener<IContextActionServiceListener>();
            bool flag = (uIServiceListener != null) && uIServiceListener.OnExecute(element, action);
            if (flag && !element.IsDisposing)
            {
                base.SetActiveItem(view, element);
            }
            return flag;
        }
    }
}

