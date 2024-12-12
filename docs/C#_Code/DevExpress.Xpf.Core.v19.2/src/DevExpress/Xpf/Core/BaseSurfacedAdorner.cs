namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Media;

    public abstract class BaseSurfacedAdorner : Adorner, IDisposable
    {
        private System.Windows.Documents.AdornerLayer adornerLayerCore;
        private bool visibleCore;
        private bool isDisposing;

        protected BaseSurfacedAdorner(UIElement container) : base(container)
        {
            this.Surface = this.CreateAdornerSurface();
            base.AddVisualChild(this.Surface);
        }

        public void Activate()
        {
            if (!this.IsActivated && (this.AdornerLayer != null))
            {
                this.AdornerLayer.Add(this);
                this.OnActivated();
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Size renderSize = base.AdornedElement.RenderSize;
            this.Surface.Arrange(new Rect(0.0, 0.0, renderSize.Width, renderSize.Height));
            return finalSize;
        }

        protected void CheckSurfaceVisibility()
        {
            Visibility visibility = this.Visible ? Visibility.Visible : Visibility.Hidden;
            if (this.Surface.Visibility != visibility)
            {
                this.Surface.Visibility = visibility;
            }
        }

        protected abstract BaseAdornerSurface CreateAdornerSurface();
        public void Deactivate()
        {
            if (this.IsActivated && (this.AdornerLayer != null))
            {
                this.AdornerLayer.Remove(this);
                this.OnDeactivated();
            }
        }

        protected virtual System.Windows.Documents.AdornerLayer FindAdornerLayer(UIElement adornedElement) => 
            AdornerHelper.FindAdornerLayer(adornedElement);

        protected override Visual GetVisualChild(int index) => 
            this.Surface;

        protected override Size MeasureOverride(Size constraint) => 
            new Size(0.0, 0.0);

        protected virtual void OnActivated()
        {
        }

        protected virtual void OnDeactivated()
        {
        }

        protected virtual void OnDispose()
        {
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);
            if (oldParent != null)
            {
                this.adornerLayerCore = null;
            }
        }

        void IDisposable.Dispose()
        {
            if (!this.isDisposing)
            {
                this.isDisposing = true;
                this.OnDispose();
                this.adornerLayerCore = null;
            }
            GC.SuppressFinalize(this);
        }

        public void Update()
        {
            if (this.IsActivated)
            {
                this.Surface.InvalidateArrange();
            }
        }

        public void Update(bool visible)
        {
            this.Visible = visible;
            this.Update();
        }

        protected bool IsDisposing =>
            this.isDisposing;

        protected System.Windows.Documents.AdornerLayer AdornerLayer
        {
            get
            {
                if (this.adornerLayerCore == null)
                {
                    this.adornerLayerCore = this.FindAdornerLayer(base.AdornedElement);
                    if (this.adornerLayerCore == null)
                    {
                        throw new InvalidOperationException("Adorned element has no adorner layer");
                    }
                }
                return this.adornerLayerCore;
            }
        }

        public bool IsActivated =>
            this.adornerLayerCore != null;

        protected BaseAdornerSurface Surface { get; private set; }

        protected override int VisualChildrenCount =>
            1;

        public bool Visible
        {
            get => 
                this.visibleCore;
            set
            {
                this.visibleCore = value;
                this.CheckSurfaceVisibility();
            }
        }

        public abstract class BaseAdornerSurface : Panel
        {
            protected readonly BaseSurfacedAdorner BaseAdorner;

            protected BaseAdornerSurface(BaseSurfacedAdorner adorner)
            {
                this.BaseAdorner = adorner;
            }
        }
    }
}

