namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal abstract class BaseResizingPreviewHelper : IResizingPreviewHelper
    {
        private LayoutView viewCore;
        protected Rect InitialBounds;
        private UIElement resizer;

        public BaseResizingPreviewHelper(LayoutView view)
        {
            this.viewCore = view;
        }

        protected Rect CorrectBounds(Rect bounds1)
        {
            PresentationSource source = PresentationSource.FromDependencyObject(this.Owner);
            Point location = bounds1.Location;
            if (source != null)
            {
                bounds1 = new Rect(source.CompositionTarget.TransformFromDevice.Transform(this.Owner.PointToScreen(bounds1.Location)), bounds1.Size);
            }
            else
            {
                bounds1 = new Rect(location, bounds1.Size);
            }
            return bounds1;
        }

        public void EndResizing()
        {
            this.OnEndResizing();
        }

        private void ForceUpdateElementBounds(ILayoutElement element)
        {
            element.Invalidate();
            element.EnsureBounds();
        }

        protected virtual Rect GetAdornerBounds(Point change) => 
            Rect.Empty;

        protected virtual Rect GetInitialAdornerBounds() => 
            Rect.Empty;

        protected virtual Rect GetInitialWindowBounds() => 
            Rect.Empty;

        protected internal Rect GetItemBounds(ILayoutElement element)
        {
            if (element == null)
            {
                return Rect.Empty;
            }
            this.ForceUpdateElementBounds(element);
            return ElementHelper.GetRect(element);
        }

        protected abstract UIElement GetUIElement();
        protected virtual Rect GetWindowBounds(Point change) => 
            Rect.Empty;

        public void InitResizing(Point start, ILayoutElement element)
        {
            this.Element = element;
            this.StartPoint = start;
            this.OnInitResizing();
        }

        protected virtual void OnEndResizing()
        {
            this.View.ResizingWindowHelper.HideResizeWindow();
        }

        protected virtual void OnInitResizing()
        {
            this.BoundsHelper ??= new DevExpress.Xpf.Docking.Platform.BoundsHelper(this.View, this.Element, this.MinSize);
            this.InitialBounds = (this.Mode == DevExpress.Xpf.Core.FloatingMode.Adorner) ? this.GetInitialAdornerBounds() : this.GetInitialWindowBounds();
            this.View.ResizingWindowHelper.ShowResizeWindow(this.Resizer, this.InitialBounds, this.Mode);
        }

        protected virtual void OnResizing(Point change)
        {
            Rect rect = (this.Mode == DevExpress.Xpf.Core.FloatingMode.Adorner) ? this.GetAdornerBounds(change) : this.GetWindowBounds(change);
            this.View.ResizingWindowHelper.UpdateResizeWindow(rect);
        }

        public void Resize(Point change)
        {
            this.OnResizing(change);
        }

        public LayoutView View =>
            this.viewCore;

        public DockLayoutManager Owner =>
            this.View.Container;

        protected virtual DevExpress.Xpf.Core.FloatingMode Mode =>
            this.Owner.GetRealFloatingMode();

        protected ILayoutElement Element { get; private set; }

        protected Point StartPoint { get; private set; }

        public Size MinSize { get; set; }

        public Size MaxSize { get; set; }

        public DevExpress.Xpf.Docking.Platform.BoundsHelper BoundsHelper { get; set; }

        protected UIElement Resizer
        {
            get
            {
                this.resizer ??= this.GetUIElement();
                return this.resizer;
            }
        }
    }
}

