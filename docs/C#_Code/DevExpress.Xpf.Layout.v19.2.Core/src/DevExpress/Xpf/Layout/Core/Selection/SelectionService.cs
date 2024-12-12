namespace DevExpress.Xpf.Layout.Core.Selection
{
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Platform;
    using System;
    using System.Windows.Input;

    internal class SelectionService : UIService, ISelectionService, IUIService, IDisposable
    {
        private SelectionInfo<ILayoutElement> selectionCore;

        protected bool CanProcessSelection(MouseEventType eventType, DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs ea, ILayoutElement element) => 
            (element != null) ? ((eventType == MouseEventType.MouseDown) && ((ea.Buttons != MouseButtons.None) && (((ea.Buttons != (MouseButtons.None | MouseButtons.Right)) || (element == null)) ? (ea.Buttons == MouseButtons.Left) : !this.Selection.GetSelectedState(element)))) : false;

        public void ClearSelection(IView view)
        {
            base.BeginEvent(view);
            this.Selection.Select(null);
            base.EndEvent();
        }

        protected virtual SelectionInfo<ILayoutElement> CreateSelectionInfo() => 
            new SelectionInfo<ILayoutElement>(new SelectionChangingHandler<ILayoutElement>(this.OnSelectionChanging), new SelectionChangedHandler<ILayoutElement>(this.OnSelectionChanged), new RequestSelectionRangeHandler<ILayoutElement>(this.OnRequestSelectionRange));

        public bool ExtendSelectionToParent(IView view)
        {
            if ((view == null) || (this.Selection.LastSelectedElement == null))
            {
                return false;
            }
            base.BeginEvent(view);
            bool flag = this.ExtendSelectionToParentCore(view);
            base.EndEvent();
            return flag;
        }

        private bool ExtendSelectionToParentCore(IView view)
        {
            ILayoutElement parent = this.GetParent(view, this.Selection.LastSelectedElement);
            return ((parent != null) && this.SelectCore(view, parent, SelectionMode.SingleItem));
        }

        protected override Key[] GetKeys() => 
            new Key[] { Key.Escape };

        private ILayoutElement GetParent(IView view, ILayoutElement element)
        {
            if (element == null)
            {
                return null;
            }
            if (!element.IsDisposing)
            {
                return element.Parent;
            }
            ILayoutElement element2 = view.GetElement(SelectionHelper.GetElementKey<ILayoutElement>(element));
            return ((element2 != null) ? element2.Parent : element);
        }

        public bool GetSelectedState(ILayoutElement element) => 
            this.Selection.GetSelectedState(element);

        private IView GetView(ILayoutElement element)
        {
            if (!(element is ISelectionKey))
            {
                return base.Sender?.Adapter.GetView(element);
            }
            object viewKey = SelectionHelper.GetViewKey<ILayoutElement>(element);
            return base.Sender?.Adapter.GetView(viewKey);
        }

        protected override void OnCreate()
        {
            this.selectionCore = this.CreateSelectionInfo();
            base.OnCreate();
        }

        protected override void OnDispose()
        {
            Ref.Dispose<SelectionInfo<ILayoutElement>>(ref this.selectionCore);
            base.OnDispose();
        }

        private ILayoutElement[] OnRequestSelectionRange(ILayoutElement first, ILayoutElement last)
        {
            IView view = this.GetView(first);
            if (view != null)
            {
                ISelectionServiceListener uIServiceListener = view.GetUIServiceListener<ISelectionServiceListener>();
                if (uIServiceListener != null)
                {
                    return uIServiceListener.OnRequestSelectionRange(first, last);
                }
            }
            return new ILayoutElement[0];
        }

        private void OnSelectionChanged(ILayoutElement element, bool selected)
        {
            IView view = this.GetView(element);
            if (view != null)
            {
                ISelectionServiceListener uIServiceListener = view.GetUIServiceListener<ISelectionServiceListener>();
                if (uIServiceListener != null)
                {
                    uIServiceListener.OnSelectionChanged(element, selected);
                }
            }
        }

        private bool OnSelectionChanging(ILayoutElement element, bool selected)
        {
            IView view = this.GetView(element);
            if (view == null)
            {
                return true;
            }
            ISelectionServiceListener uIServiceListener = view.GetUIServiceListener<ISelectionServiceListener>();
            return ((uIServiceListener == null) || uIServiceListener.OnSelectionChanging(element, selected));
        }

        protected override bool ProcessKeyOverride(IView view, KeyEventType evetType, Key key) => 
            (view != null) && ((this.Selection.LastSelectedElement != null) && this.ExtendSelectionToParentCore(view));

        protected override bool ProcessMouseOverride(IView view, MouseEventType eventType, DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs ea)
        {
            IViewAdapter adapter = view.Adapter;
            if (adapter != null)
            {
                ILayoutElement element = adapter.CalcHitInfo(view, ea.Point).Element;
                if (this.CanProcessSelection(eventType, ea, element))
                {
                    this.ProcessSelection(view, element);
                    return true;
                }
            }
            return false;
        }

        protected void ProcessSelection(IView view, ILayoutElement element)
        {
            ISelectionServiceListener uIServiceListener = view.GetUIServiceListener<ISelectionServiceListener>();
            if (uIServiceListener == null)
            {
                this.Selection.Select(null);
            }
            else
            {
                this.SelectCore(view, element, uIServiceListener.CheckMode(element));
                uIServiceListener.OnProcessSelection(element);
            }
        }

        public void Select(IView view, ILayoutElement element, SelectionMode mode)
        {
            base.BeginEvent(view);
            if (view.GetUIServiceListener<ISelectionServiceListener>() != null)
            {
                this.SelectCore(view, element, mode);
            }
            base.EndEvent();
        }

        protected bool SelectCore(IView view, ILayoutElement target, SelectionMode mode)
        {
            ILayoutElementBehavior elementBehavior = view.GetElementBehavior(target);
            if ((elementBehavior == null) || !elementBehavior.CanSelect())
            {
                return false;
            }
            this.Selection.Mode = mode;
            this.Selection.Select(target);
            return true;
        }

        public SelectionInfo<ILayoutElement> Selection =>
            this.selectionCore;
    }
}

