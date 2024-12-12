namespace DevExpress.Xpf.Layout.Core.Platform
{
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Actions;
    using DevExpress.Xpf.Layout.Core.Dragging;
    using DevExpress.Xpf.Layout.Core.Selection;
    using DevExpress.Xpf.Layout.Core.UIInteraction;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class ViewAdapter : LayoutElementHostAdapter, IViewAdapter, ILayoutElementHostAdapter, IDisposable
    {
        private ViewCollection viewsCore;
        private IDragService dragServiceCore;
        private ISelectionService selectionServiceCore;
        private IUIInteractionService uiInteractionServiceCore;
        private IActionService actionServiceCore;
        private IContextActionService contextActionService;

        protected virtual ViewCollection CreateViews() => 
            new ViewCollection(this);

        public IView GetBehindView(IView source, Point screenPoint) => 
            this.GetBehindViewOverride(source, screenPoint);

        protected virtual IView GetBehindViewOverride(IView source, Point screenPoint)
        {
            if (source != null)
            {
                IView[] viewsSortedByZOrder = this.GetViewsSortedByZOrder();
                bool flag = false;
                for (int i = 0; i < viewsSortedByZOrder.Length; i++)
                {
                    IView objA = viewsSortedByZOrder[i];
                    if (!flag)
                    {
                        flag = ReferenceEquals(objA, source);
                    }
                    else if (flag)
                    {
                        base.Ensure(objA);
                        if (objA.IsActiveAndCanProcessEvent && base.HitTest(objA, objA.ScreenToClient(screenPoint)))
                        {
                            return objA;
                        }
                    }
                }
            }
            return null;
        }

        public Point GetBehindViewPoint(IView source, IView behindView, Point screenPoint) => 
            this.GetBehindViewPointOverride(source, behindView, screenPoint);

        protected virtual Point GetBehindViewPointOverride(IView source, IView behindView, Point screenPoint) => 
            behindView.ScreenToClient(screenPoint);

        public IView GetView(ILayoutElement element)
        {
            ILayoutElement root = ElementHelper.GetRoot(element);
            foreach (IView view in this.GetViewsSortedByZOrder())
            {
                if (ReferenceEquals(view.LayoutRoot, root))
                {
                    return view;
                }
            }
            return null;
        }

        public IView GetView(object rootKey)
        {
            foreach (IView view in this.GetViewsSortedByZOrder())
            {
                if (view.RootKey == rootKey)
                {
                    return view;
                }
            }
            return null;
        }

        public IView GetView(Point screenPoint)
        {
            foreach (IView view in this.GetViewsSortedByZOrder())
            {
                base.Ensure(view);
                if ((view.IsActiveAndCanProcessEvent && (view.ZOrder != -1)) && base.HitTest(view, view.ScreenToClient(screenPoint)))
                {
                    return view;
                }
            }
            return null;
        }

        private IView[] GetViewsSortedByZOrder()
        {
            IView[] views = this.Views.ToArray();
            KeyValuePair<int, IView>[] keys = new KeyValuePair<int, IView>[views.Length];
            for (int i = 0; i < views.Length; i++)
            {
                keys[i] = new KeyValuePair<int, IView>(i, views[i]);
            }
            StableSortingHelper helper1 = new StableSortingHelper(keys, views);
            return views;
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            this.viewsCore = this.CreateViews();
        }

        protected override void OnDispose()
        {
            Ref.Dispose<IActionService>(ref this.actionServiceCore);
            Ref.Dispose<IUIInteractionService>(ref this.uiInteractionServiceCore);
            Ref.Dispose<ISelectionService>(ref this.selectionServiceCore);
            Ref.Dispose<IDragService>(ref this.dragServiceCore);
            Ref.Dispose<ViewCollection>(ref this.viewsCore);
            Ref.Dispose<IContextActionService>(ref this.contextActionService);
            this.NotificationSource = null;
            base.OnDispose();
        }

        public void ProcessAction(ViewAction action)
        {
            if (!base.IsDisposing)
            {
                foreach (IView view in this.GetViewsSortedByZOrder())
                {
                    this.ProcessActionCore(view, action);
                }
            }
        }

        public void ProcessAction(IView view, ViewAction action)
        {
            if (!base.IsDisposing)
            {
                this.ProcessActionCore(view, action);
            }
        }

        protected void ProcessActionCore(IView view, ViewAction action)
        {
            if (view != null)
            {
                switch (action)
                {
                    case ViewAction.Hiding:
                        this.ActionService.Hide(view, false);
                        return;

                    case ViewAction.Hide:
                        this.ActionService.Hide(view, true);
                        return;

                    case ViewAction.ShowSelection:
                        this.ActionService.ShowSelection(view);
                        return;

                    case ViewAction.HideSelection:
                        this.ActionService.HideSelection(view);
                        return;
                }
            }
        }

        public void ProcessKey(IView view, KeyEventType eventype, Key key)
        {
            if (!base.IsDisposing && !base.IsInEvent)
            {
                base.BeginEvent(view);
                if (!this.UIInteractionService.ProcessKey(view, eventype, key))
                {
                    this.SelectionService.ProcessKey(view, eventype, key);
                    this.DragService.ProcessKey(view, eventype, key);
                }
                base.EndEvent();
            }
        }

        public void ProcessMouseEvent(IView view, MouseEventType eventType, DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs ea)
        {
            if (!base.IsDisposing && !base.IsInEvent)
            {
                base.BeginEvent(view);
                if (!this.UIInteractionService.ProcessMouse(view, eventType, ea))
                {
                    this.SelectionService.ProcessMouse(view, eventType, ea);
                    this.DragService.ProcessMouse(view, eventType, ea);
                }
                base.EndEvent();
            }
        }

        protected virtual IActionService ResolveActionService() => 
            new DevExpress.Xpf.Layout.Core.Actions.ActionService();

        protected virtual IContextActionService ResolveContextActionService() => 
            new DevExpress.Xpf.Layout.Core.Actions.ContextActionService();

        protected virtual IDragService ResolveDragService() => 
            new DevExpress.Xpf.Layout.Core.Dragging.DragService();

        protected virtual ISelectionService ResolveSelectionService() => 
            new DevExpress.Xpf.Layout.Core.Selection.SelectionService();

        protected virtual IUIInteractionService ResolveUIInteractionService() => 
            new DevExpress.Xpf.Layout.Core.UIInteraction.UIInteractionService();

        public object NotificationSource { get; protected set; }

        public ViewCollection Views =>
            this.viewsCore;

        public IDragService DragService
        {
            [DebuggerStepThrough]
            get
            {
                this.dragServiceCore ??= this.ResolveDragService();
                return this.dragServiceCore;
            }
        }

        public ISelectionService SelectionService
        {
            [DebuggerStepThrough]
            get
            {
                this.selectionServiceCore ??= this.ResolveSelectionService();
                return this.selectionServiceCore;
            }
        }

        public IUIInteractionService UIInteractionService
        {
            [DebuggerStepThrough]
            get
            {
                this.uiInteractionServiceCore ??= this.ResolveUIInteractionService();
                return this.uiInteractionServiceCore;
            }
        }

        public IActionService ActionService
        {
            [DebuggerStepThrough]
            get
            {
                this.actionServiceCore ??= this.ResolveActionService();
                return this.actionServiceCore;
            }
        }

        public IContextActionService ContextActionService
        {
            [DebuggerStepThrough]
            get
            {
                this.contextActionService ??= this.ResolveContextActionService();
                return this.contextActionService;
            }
        }

        private class StableSortingHelper : IComparer<KeyValuePair<int, IView>>
        {
            private KeyValuePair<int, IView>[] keys;

            public StableSortingHelper(KeyValuePair<int, IView>[] keys, IView[] views)
            {
                this.keys = keys;
                Array.Sort<KeyValuePair<int, IView>, IView>(keys, views, this);
            }

            private static int CompareZOrder(int order1, int order2) => 
                order2.CompareTo(order1);

            int IComparer<KeyValuePair<int, IView>>.Compare(KeyValuePair<int, IView> pair1, KeyValuePair<int, IView> pair2)
            {
                if (pair1.Value == pair2.Value)
                {
                    return 0;
                }
                int num = CompareZOrder(pair1.Value.ZOrder, pair2.Value.ZOrder);
                return ((num == 0) ? CompareZOrder(pair1.Key, pair2.Key) : num);
            }
        }
    }
}

