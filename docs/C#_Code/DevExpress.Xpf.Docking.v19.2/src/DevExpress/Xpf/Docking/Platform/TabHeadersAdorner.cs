namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class TabHeadersAdorner : PlacementAdorner
    {
        public TabHeadersAdorner(UIElement container) : base(container)
        {
            base.IsHitTestVisible = false;
        }

        protected override BaseSurfacedAdorner.BaseAdornerSurface CreateAdornerSurface()
        {
            TabHeadersAdornerSurface surface1 = new TabHeadersAdornerSurface(this);
            surface1.Opacity = 0.75;
            return surface1;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            this.Tab = new TabHint();
            this.Header = new TabHeaderHint();
            this.Manager = DockLayoutManager.GetDockLayoutManager(base.AdornedElement);
            this.RegisterEx(this.Tab);
            this.RegisterEx(this.Header);
        }

        protected override void OnDeactivated()
        {
            base.Unregister(this.Tab);
            base.Unregister(this.Header);
            base.OnDeactivated();
        }

        protected override void OnDispose()
        {
            base.Deactivate();
            base.OnDispose();
        }

        public void RegisterEx(UIElement element)
        {
            base.Register(element);
            this.TabHeadersSurface.RemoveFromLogicalTree(element);
            this.Manager.DockHintsContainer.Add(element);
        }

        public void ResetElements()
        {
            if (!base.IsDisposing)
            {
                base.SetVisible(this.Tab, false);
                base.SetVisible(this.Header, false);
                this.IsTabHeaderHintEnabled = false;
            }
        }

        public unsafe void ShowElements(Dock headerLocation, Rect tab, Rect header)
        {
            this.Tab.TabHeaderLocation = headerLocation;
            this.Header.TabHeaderLocation = headerLocation;
            base.SetVisible(this.Tab, !tab.IsEmpty);
            base.SetVisible(this.Header, !header.IsEmpty);
            double indent = this.Indent;
            if (!tab.IsEmpty)
            {
                Rect* rectPtr1 = &tab;
                rectPtr1.X += indent;
                Rect* rectPtr2 = &tab;
                rectPtr2.Y += indent;
                base.SetBoundsInContainer(this.Tab, tab);
            }
            if (!header.IsEmpty)
            {
                Rect* rectPtr3 = &header;
                rectPtr3.X += indent;
                Rect* rectPtr4 = &header;
                rectPtr4.Y += indent;
                base.SetBoundsInContainer(this.Header, header);
            }
            this.IsTabHeaderHintEnabled = !header.IsEmpty;
        }

        public TabHint Tab { get; private set; }

        public TabHeaderHint Header { get; private set; }

        public DockLayoutManager Manager { get; private set; }

        private TabHeadersAdornerSurface TabHeadersSurface =>
            base.PlacementSurface as TabHeadersAdornerSurface;

        public bool IsTabHeaderHintEnabled { get; private set; }

        private double Indent =>
            VisualizerAdornerHelper.GetAdornerWindowIndent(this);

        private class TabHeadersAdornerSurface : PlacementAdorner.AdornerSurface
        {
            public TabHeadersAdornerSurface(PlacementAdorner adorner) : base(adorner, false)
            {
            }

            public void RemoveFromLogicalTree(UIElement element)
            {
                base.RemoveLogicalChild(element);
            }
        }
    }
}

