namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class LayoutViewContextActionServiceListener : LayoutViewUIInteractionListener, IContextActionServiceListener, IUIServiceListener
    {
        protected override IFloatingHelper CreateFloatingHelper() => 
            new ContextActionFloatingHelper(base.View);

        public virtual bool OnCanExecute(ILayoutElement element, ContextAction action)
        {
            IDockLayoutElement element2 = element as IDockLayoutElement;
            if ((element2 == null) || (element2.Item == null))
            {
                return false;
            }
            BaseLayoutItem scrollTarget = element2.Item;
            return ((action == ContextAction.ScrollPrev) ? this.CanScrollPrevCore(scrollTarget) : ((action != ContextAction.ScrollNext) || this.CanScrollNextCore(scrollTarget)));
        }

        public virtual bool OnExecute(ILayoutElement element, ContextAction action)
        {
            IDockLayoutElement element2 = element as IDockLayoutElement;
            if ((element2 != null) && (element2.Item != null))
            {
                BaseLayoutItem itemToClose = element2.Item;
                switch (action)
                {
                    case ContextAction.Close:
                        return this.CloseItemCore(itemToClose);

                    case ContextAction.ExpandToggle:
                        return this.ExpandItemCore(itemToClose);

                    case ContextAction.Float:
                        return this.FloatElement(element);

                    case ContextAction.Hide:
                        return this.HideCore(itemToClose);

                    case ContextAction.Maximize:
                        return this.MaximizeItemCore(itemToClose);

                    case ContextAction.Menu:
                        return base.ShowMenuCore(itemToClose, true);

                    case ContextAction.Minimize:
                        return this.MinimizeItemCore(itemToClose);

                    case ContextAction.Pin:
                        return this.PinItemCore(itemToClose);

                    case ContextAction.Restore:
                        return this.RestoreItemCore(itemToClose);

                    case ContextAction.ScrollPrev:
                        return this.ScrollPrevCore(itemToClose);

                    case ContextAction.ScrollNext:
                        return this.ScrollNextCore(itemToClose);
                }
            }
            return false;
        }

        protected override object KeyOverride =>
            typeof(IContextActionServiceListener);

        private class ContextActionFloatingHelper : FloatingHelper
        {
            public ContextActionFloatingHelper(LayoutView view) : base(view)
            {
            }

            internal override Rect Check(Rect screenRect, Point startPoint) => 
                new Rect(new Point(), base.Check(screenRect, startPoint).Size);
        }
    }
}

