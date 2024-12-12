namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;

    public class ShadowResizeAdorner : PlacementAdorner
    {
        private UIElement savedControl;
        private UIElement ResizeBackground;

        public ShadowResizeAdorner(UIElement container) : base(container)
        {
            base.IsHitTestVisible = false;
        }

        protected override BaseSurfacedAdorner.BaseAdornerSurface CreateAdornerSurface() => 
            new ShadowResizeAdornerSurface(this, false);

        internal void EndResizing()
        {
            this.ResetElements();
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            this.ResizeBackground = new ShadowResizeBackground();
            this.RegisterElement(this.ResizeBackground);
        }

        protected override void OnDeactivated()
        {
            this.UnregisterElement(this.ResizeBackground);
            if (this.savedControl != null)
            {
                this.UnregisterElement(this.savedControl);
            }
            base.OnDeactivated();
        }

        private void RegisterElement(UIElement element)
        {
            base.Register(element);
            (base.Surface as ShadowResizeAdornerSurface).Do<ShadowResizeAdornerSurface>(x => x.RemoveLogicalChild(element));
            DockLayoutManager.GetDockLayoutManager(base.AdornedElement).DockHintsContainer.Add(element);
        }

        private void ResetElements()
        {
            base.SetVisible(this.ResizeBackground, false);
            if (this.savedControl != null)
            {
                base.SetVisible(this.savedControl, false);
                this.UnregisterElement(this.savedControl);
                this.savedControl = null;
            }
        }

        internal void Resize(Rect bounds)
        {
            this.UpdateBounds(this.savedControl, bounds);
        }

        internal void ShowBackground(Rect bounds)
        {
            base.SetVisible(this.ResizeBackground, !bounds.IsEmpty);
            this.UpdateBounds(this.ResizeBackground, bounds);
        }

        internal void StartResizing(UIElement control, Rect bounds)
        {
            this.savedControl = control;
            this.RegisterElement(control);
            base.SetVisible(control, !bounds.IsEmpty);
            this.UpdateBounds(control, bounds);
        }

        private void UnregisterElement(UIElement element)
        {
            base.Unregister(element);
            DockLayoutManager.GetDockLayoutManager(base.AdornedElement).DockHintsContainer.Remove(element);
        }

        private unsafe void UpdateBounds(UIElement control, Rect bounds)
        {
            if (!bounds.IsEmpty)
            {
                double indent = this.Indent;
                Rect* rectPtr1 = &bounds;
                rectPtr1.X += indent;
                Rect* rectPtr2 = &bounds;
                rectPtr2.Y += indent;
                base.SetBoundsInContainer(control, bounds);
            }
        }

        private double Indent =>
            VisualizerAdornerHelper.GetAdornerWindowIndent(this);

        private class ShadowResizeAdornerSurface : PlacementAdorner.AdornerSurface
        {
            public ShadowResizeAdornerSurface(PlacementAdorner adorner) : base(adorner)
            {
            }

            public ShadowResizeAdornerSurface(PlacementAdorner adorner, bool enableValidation) : base(adorner, enableValidation)
            {
            }

            public void RemoveLogicalChild(object child)
            {
                base.RemoveLogicalChild(child);
            }
        }
    }
}

