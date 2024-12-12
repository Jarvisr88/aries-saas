namespace DevExpress.Xpf.Layout.Core.UIInteraction
{
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Platform;
    using System;

    public class UIInteractionServiceListener : SupportFloatingListener, IUIInteractionServiceListener, IUIServiceListener
    {
        protected virtual bool CanFloatElementOnDoubleClick(ILayoutElement element) => 
            true;

        protected virtual bool CanMaximizeOrRestore(ILayoutElement element) => 
            false;

        protected virtual bool DockElementOnDoubleClick(ILayoutElement element) => 
            false;

        protected virtual bool DoControlItemDoubleClick(ILayoutElement element) => 
            false;

        protected virtual bool FloatElement(ILayoutElement element) => 
            this.GetFloatingView(element) != null;

        protected virtual bool FloatElementOnDoubleClick(ILayoutElement element) => 
            this.FloatElement(element);

        protected virtual bool IsControlItemElement(ILayoutElement element) => 
            false;

        protected virtual bool IsFloatingElement(ILayoutElement element) => 
            false;

        protected virtual bool IsMaximized(ILayoutElement element) => 
            false;

        protected virtual bool IsMDIDocument(ILayoutElement element) => 
            false;

        protected virtual bool MaximizeElementOnDoubleClick(ILayoutElement element) => 
            false;

        public virtual void OnActivate()
        {
        }

        public virtual bool OnActiveItemChanged(ILayoutElement element) => 
            true;

        public virtual bool OnActiveItemChanging(ILayoutElement element) => 
            true;

        public virtual bool OnClickAction(LayoutElementHitInfo clickInfo) => 
            false;

        public virtual bool OnClickPreviewAction(LayoutElementHitInfo clickInfo) => 
            false;

        public virtual void OnDeactivate()
        {
        }

        public virtual bool OnDoubleClickAction(LayoutElementHitInfo clickInfo) => 
            this.ToggleStateOnDoubleClick(clickInfo.Element);

        public virtual bool OnMenuAction(LayoutElementHitInfo clickInfo) => 
            false;

        public virtual bool OnMiddleButtonClickAction(LayoutElementHitInfo clickInfo) => 
            false;

        public virtual bool OnMouseDown(LayoutElementHitInfo clickInfo) => 
            false;

        public virtual bool OnMouseUp(LayoutElementHitInfo clickInfo) => 
            false;

        protected virtual bool RestoreElementOnDoubleClick(ILayoutElement element) => 
            false;

        protected bool ToggleStateOnDoubleClick(ILayoutElement element)
        {
            if (element == null)
            {
                return false;
            }
            bool flag = this.IsFloatingElement(element);
            if ((flag && this.CanMaximizeOrRestore(element)) || this.IsMDIDocument(element))
            {
                return (this.IsMaximized(element) ? this.RestoreElementOnDoubleClick(element) : this.MaximizeElementOnDoubleClick(element));
            }
            if (this.IsControlItemElement(element))
            {
                return this.DoControlItemDoubleClick(element);
            }
            BaseView serviceProvider = base.ServiceProvider as BaseView;
            return (((serviceProvider == null) || !(flag ? serviceProvider.CanSuspendDocking(element) : serviceProvider.CanSuspendFloating(element))) ? (flag ? this.DockElementOnDoubleClick(element) : (this.CanFloatElementOnDoubleClick(element) && this.FloatElementOnDoubleClick(element))) : false);
        }

        public object Key =>
            this.KeyOverride;

        protected virtual object KeyOverride =>
            typeof(IUIInteractionServiceListener);
    }
}

