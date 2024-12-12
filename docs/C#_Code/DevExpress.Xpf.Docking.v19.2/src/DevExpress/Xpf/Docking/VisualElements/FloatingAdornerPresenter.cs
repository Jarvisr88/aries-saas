namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Platform;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class FloatingAdornerPresenter : FloatPanePresenter
    {
        public static readonly DependencyProperty SizeToContentProperty = DependencyProperty.Register("SizeToContent", typeof(System.Windows.SizeToContent), typeof(FloatingAdornerPresenter), new PropertyMetadata(System.Windows.SizeToContent.Manual, new PropertyChangedCallback(FloatingAdornerPresenter.OnSizeToContentChanged)));
        private FloatingPaneAdornerElement adornerElementCore;

        protected override void ActivateContentHolder()
        {
            base.Container.DragAdorner.BringToFront(this.AdornerElement);
        }

        protected override void AddDecoratorToContentContainer(NonLogicalDecorator decorator)
        {
            this.AdornerElement.Child = decorator;
            base.Container.DragAdorner.Register(this.AdornerElement);
            base.Container.DragAdorner.SetVisible(this.AdornerElement, true);
        }

        private static bool CanUpdateFloatingBounds(DockLayoutManager container)
        {
            UIElement topContainerWithAdornerLayer = LayoutHelper.GetTopContainerWithAdornerLayer(container);
            return ((topContainerWithAdornerLayer != null) && (topContainerWithAdornerLayer.IsArrangeValid && container.IsArrangeValid));
        }

        protected virtual FloatingPaneAdornerElement CreateAdornerElement(DockLayoutManager container) => 
            new FloatingPaneAdornerElement(this);

        protected override UIElement CreateContentContainer()
        {
            base.Container.DragAdorner.Activate();
            this.adornerElementCore = this.CreateAdornerElement(base.Container);
            return this.adornerElementCore;
        }

        protected override void DeactivateContentHolder()
        {
            base.Container.DragAdorner.Unregister(this.AdornerElement);
        }

        private void InvokeFloatingBoundsUpdate(Rect bounds)
        {
            if (!base.Container.IsDisposing && (this.AdornerElement != null))
            {
                bounds = base.Container.CorrectBoundsInAdorner(bounds);
                if (!MathHelper.IsEmpty(bounds.Size))
                {
                    base.Container.DragAdorner.SetBoundsInContainer(this.AdornerElement, bounds);
                }
            }
        }

        protected override void OnDisposing()
        {
            this.adornerElementCore = null;
        }

        protected virtual void OnFloatStateChanged(FloatState oldValue, FloatState newValue)
        {
            this.UpdateFloatingBoundsCore(new Rect(base.FloatLocation, base.FloatSize));
        }

        protected override void OnHierarchyCreated()
        {
            base.OnHierarchyCreated();
            this.adornerElementCore.EnsureFlowDirection();
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            base.LayoutUpdated -= new EventHandler(this.OnLayoutUpdated);
            this.InvokeFloatingBoundsUpdate(new Rect(base.FloatLocation, base.FloatSize));
        }

        private static void OnSizeToContentChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
        {
            FloatingAdornerPresenter presenter = dObj as FloatingAdornerPresenter;
            if (presenter != null)
            {
                presenter.OnSizeToContentChanged((System.Windows.SizeToContent) e.OldValue, (System.Windows.SizeToContent) e.NewValue);
            }
        }

        protected virtual void OnSizeToContentChanged(System.Windows.SizeToContent oldValue, System.Windows.SizeToContent newValue)
        {
            this.UpdateFloatingBoundsCore(new Rect(base.FloatLocation, base.FloatSize));
        }

        internal override bool TryGetActualRenderSize(out Size autoSize)
        {
            Size renderSize;
            FloatingPaneAdornerElement adornerElement = this.AdornerElement;
            if (adornerElement != null)
            {
                renderSize = adornerElement.RenderSize;
            }
            else
            {
                renderSize = new Size();
            }
            autoSize = renderSize;
            return (adornerElement != null);
        }

        protected override void UpdateFloatingBoundsCore(Rect bounds)
        {
            base.LayoutUpdated -= new EventHandler(this.OnLayoutUpdated);
            if (CanUpdateFloatingBounds(base.Container))
            {
                this.InvokeFloatingBoundsUpdate(bounds);
            }
            else
            {
                base.LayoutUpdated += new EventHandler(this.OnLayoutUpdated);
            }
        }

        protected override void UpdateIsOpenCore(bool isOpen)
        {
            base.Container.DragAdorner.SetVisible(this.AdornerElement, isOpen);
        }

        public System.Windows.SizeToContent SizeToContent
        {
            get => 
                (System.Windows.SizeToContent) base.GetValue(SizeToContentProperty);
            set => 
                base.SetValue(SizeToContentProperty, value);
        }

        protected internal FloatingPaneAdornerElement AdornerElement =>
            this.adornerElementCore;

        protected override bool IsAlive =>
            this.adornerElementCore != null;
    }
}

