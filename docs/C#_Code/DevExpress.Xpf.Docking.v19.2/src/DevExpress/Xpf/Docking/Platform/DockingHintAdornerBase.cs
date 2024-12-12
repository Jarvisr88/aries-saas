namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class DockingHintAdornerBase : BaseSurfacedAdorner
    {
        private DevExpress.Xpf.Docking.Platform.SelectionController selectionControllerCore;
        private DevExpress.Xpf.Docking.Platform.RenameController renameControllerCore;
        private HitTestHelper.HitCache hitCache;
        internal DevExpress.Xpf.Docking.DockHintsConfiguration DockHintsConfiguration;

        public DockingHintAdornerBase(UIElement container) : base(container)
        {
            this.selectionControllerCore = this.CreateSelectionController();
            this.renameControllerCore = this.CreateRenameController();
            this.DockHintsConfiguration = new DevExpress.Xpf.Docking.DockHintsConfiguration();
        }

        public void ClearHotTrack()
        {
            this.VisualizerSurface.ClearHotTrack();
        }

        public bool ContainsSelection(BaseLayoutItem item)
        {
            if (item == null)
            {
                return false;
            }
            LayoutGroup root = item.GetRoot();
            if (base.AdornedElement is AdornerWindowContent)
            {
                return ReferenceEquals(((AdornerWindowContent) base.AdornedElement).View, this.Manager.GetView(root));
            }
            IUIElement adornedElement = base.AdornedElement as IUIElement;
            if (base.AdornedElement is FloatPanePresenter.FloatingContentPresenter)
            {
                adornedElement = ((FloatPanePresenter.FloatingContentPresenter) base.AdornedElement).Container as IUIElement;
            }
            return ((root != null) && ReferenceEquals(this.Manager.GetView(root), this.Manager.GetView(adornedElement)));
        }

        protected override BaseSurfacedAdorner.BaseAdornerSurface CreateAdornerSurface() => 
            new AdornerSurface(this);

        protected virtual DockHintElement CreateDockHintElement(DockVisualizerElement type) => 
            DockHintElementFactory.Make(type);

        public DockHintElement CreateDockHintElement(DockVisualizerElement type, Size size, DevExpress.Xpf.Layout.Core.Alignment alignment)
        {
            DockHintElement element = this.CreateDockHintElement(type);
            if (element != null)
            {
                this.VisualizerSurface.AddElement(element, size, alignment, type);
            }
            return element;
        }

        protected virtual DevExpress.Xpf.Docking.Platform.RenameController CreateRenameController() => 
            new DevExpress.Xpf.Docking.Platform.RenameController(this);

        protected virtual DevExpress.Xpf.Docking.Platform.SelectionController CreateSelectionController() => 
            new DevExpress.Xpf.Docking.Platform.SelectionController(this);

        protected virtual void CreateVisualizerElements()
        {
            Size size = new Size(double.PositiveInfinity, double.PositiveInfinity);
            Size size2 = new Size();
            this.CreateDockHintElement(DockVisualizerElement.DockZone, size2, DevExpress.Xpf.Layout.Core.Alignment.Fill);
            if (this.ShouldCreateSideElements)
            {
                this.CreateDockHintElement(DockVisualizerElement.Left, size, DevExpress.Xpf.Layout.Core.Alignment.MiddleLeft);
                this.CreateDockHintElement(DockVisualizerElement.Right, size, DevExpress.Xpf.Layout.Core.Alignment.MiddleRight);
                this.CreateDockHintElement(DockVisualizerElement.Top, size, DevExpress.Xpf.Layout.Core.Alignment.TopCenter);
                this.CreateDockHintElement(DockVisualizerElement.Bottom, size, DevExpress.Xpf.Layout.Core.Alignment.BottomCenter);
            }
            this.CreateDockHintElement(DockVisualizerElement.Center, size, DevExpress.Xpf.Layout.Core.Alignment.MiddleCenter);
        }

        public DockHintHitInfo HitTest(Point point)
        {
            double indent = this.Indent;
            Point hitPoint = new Point(point.X + indent, point.Y + indent);
            return new DockHintHitInfo(this, HitTestHelper.HitTest(base.Surface, hitPoint, ref this.hitCache));
        }

        protected override void OnActivated()
        {
            this.Manager = DockLayoutManager.GetDockLayoutManager(base.AdornedElement);
            this.CreateVisualizerElements();
            base.InvalidateArrange();
        }

        protected override void OnDispose()
        {
            this.VisualizerSurface.Clear();
            base.Deactivate();
            Ref.Dispose<HitTestHelper.HitCache>(ref this.hitCache);
            Ref.Dispose<DevExpress.Xpf.Docking.Platform.SelectionController>(ref this.selectionControllerCore);
            Ref.Dispose<DevExpress.Xpf.Docking.Platform.RenameController>(ref this.renameControllerCore);
            base.OnDispose();
        }

        public void ResetDocking()
        {
            this.DockHintsConfiguration.Invalidate();
            this.TargetRect = Rect.Empty;
            this.HintRect = Rect.Empty;
        }

        internal void SetDockHintsConfiguration(DockLayoutElementDragInfo dragInfo)
        {
            this.DockHintsConfiguration.Invalidate();
            this.DockHintsConfiguration.SetConfiguration(this.Manager, dragInfo);
        }

        public void UpdateEnabledState()
        {
            this.VisualizerSurface.UpdateEnabledState();
        }

        public void UpdateHotTrack(DockHintHitInfo hitInfo)
        {
            this.VisualizerSurface.UpdateHotTrack(hitInfo);
        }

        public void UpdateIsAvailable()
        {
            this.VisualizerSurface.UpdateIsAvailable();
        }

        public void UpdateSelection()
        {
            this.SelectionController.UpdateHints();
        }

        public void UpdateState()
        {
            this.VisualizerSurface.UpdateState();
        }

        public bool IsDisposing =>
            base.IsDisposing;

        protected DockLayoutManager Manager { get; private set; }

        protected internal DevExpress.Xpf.Docking.Platform.SelectionController SelectionController =>
            this.selectionControllerCore;

        protected internal DevExpress.Xpf.Docking.Platform.RenameController RenameController =>
            this.renameControllerCore;

        internal DevExpress.Xpf.Layout.Core.HostType HostType { get; set; }

        private bool ShouldCreateSideElements =>
            this.HostType == DevExpress.Xpf.Layout.Core.HostType.Layout;

        protected AdornerSurface VisualizerSurface =>
            base.Surface as AdornerSurface;

        internal double Indent =>
            VisualizerAdornerHelper.GetAdornerWindowIndent(this);

        public bool ShowSelectionHints { get; set; }

        public Rect TargetRect { get; set; }

        public Rect HintRect { get; set; }

        public Rect SurfaceRect { get; set; }

        protected class AdornerSurface : BaseSurfacedAdorner.BaseAdornerSurface
        {
            private Dictionary<DockHintElement, DockingHintAdornerBase.ElementInfo> elementsCore;

            public AdornerSurface(DockingHintAdornerBase adorner) : base(adorner)
            {
                this.elementsCore = new Dictionary<DockHintElement, DockingHintAdornerBase.ElementInfo>();
            }

            public void AddElement(DockHintElement element, Size size, DevExpress.Xpf.Layout.Core.Alignment alignment, DockVisualizerElement type)
            {
                if (!this.ElementInfos.ContainsKey(element))
                {
                    base.Children.Add(element);
                    base.RemoveLogicalChild(element);
                    this.Adorner.Manager.DockHintsContainer.Add(element);
                    element.Measure(size);
                    this.ElementInfos.Add(element, new DockingHintAdornerBase.ElementInfo(element.DesiredSize, alignment, type));
                    if ((type != DockVisualizerElement.Selection) && (type != DockVisualizerElement.DockZone))
                    {
                        SetZIndex(element, 10);
                    }
                }
            }

            protected override Size ArrangeOverride(Size finalSize)
            {
                this.Adorner.SurfaceRect = new Rect(CoordinateHelper.ZeroPoint, finalSize);
                foreach (KeyValuePair<DockHintElement, DockingHintAdornerBase.ElementInfo> pair in this.ElementInfos)
                {
                    pair.Key.Arrange(this.Adorner, pair.Value);
                }
                return finalSize;
            }

            public void Clear()
            {
                DockHintElement[] array = new DockHintElement[this.ElementInfos.Count];
                this.ElementInfos.Keys.CopyTo(array, 0);
                Array.ForEach<DockHintElement>(array, new Action<DockHintElement>(this.RemoveElement));
            }

            public void ClearHotTrack()
            {
                foreach (KeyValuePair<DockHintElement, DockingHintAdornerBase.ElementInfo> pair in this.ElementInfos)
                {
                    pair.Key.UpdateHotTrack(null);
                }
            }

            public void RemoveElement(DockHintElement element)
            {
                if (this.ElementInfos.ContainsKey(element))
                {
                    base.Children.Remove(element);
                    this.ElementInfos.Remove(element);
                    this.Adorner.Manager.DockHintsContainer.Remove(element);
                }
            }

            public void UpdateEnabledState()
            {
                foreach (KeyValuePair<DockHintElement, DockingHintAdornerBase.ElementInfo> pair in this.ElementInfos)
                {
                    pair.Key.UpdateEnabledState(this.Adorner);
                }
            }

            public void UpdateHotTrack(DockHintHitInfo hitInfo)
            {
                foreach (KeyValuePair<DockHintElement, DockingHintAdornerBase.ElementInfo> pair in this.ElementInfos)
                {
                    pair.Key.UpdateHotTrack(hitInfo.HitButton);
                }
            }

            public void UpdateIsAvailable()
            {
                foreach (KeyValuePair<DockHintElement, DockingHintAdornerBase.ElementInfo> pair in this.ElementInfos)
                {
                    pair.Key.UpdateAvailableState(this.Adorner);
                }
            }

            public void UpdateState()
            {
                foreach (KeyValuePair<DockHintElement, DockingHintAdornerBase.ElementInfo> pair in this.ElementInfos)
                {
                    pair.Key.UpdateState(this.Adorner);
                }
            }

            protected DockingHintAdornerBase Adorner =>
                base.BaseAdorner as DockingHintAdornerBase;

            public Dictionary<DockHintElement, DockingHintAdornerBase.ElementInfo> ElementInfos =>
                this.elementsCore;
        }

        public class ElementInfo
        {
            public ElementInfo(System.Windows.Size size, DevExpress.Xpf.Layout.Core.Alignment alignment, DockVisualizerElement type)
            {
                this.Size = size;
                this.Alignment = alignment;
                this.Type = type;
            }

            public Rect CalcPlacement(Rect container, double Indent)
            {
                double y = Indent;
                switch (this.Alignment)
                {
                    case DevExpress.Xpf.Layout.Core.Alignment.Fill:
                        if (container.IsEmpty)
                        {
                            return container;
                        }
                        container = new Rect(container.X + y, container.Y + y, container.Width, container.Height);
                        return PlacementHelper.Arrange(this.Size, container, this.Alignment);

                    case DevExpress.Xpf.Layout.Core.Alignment.TopCenter:
                        return PlacementHelper.Arrange(this.Size, container, this.Alignment, new Point(0.0, y));

                    case DevExpress.Xpf.Layout.Core.Alignment.MiddleLeft:
                        return PlacementHelper.Arrange(this.Size, container, this.Alignment, new Point(y, 0.0));

                    case DevExpress.Xpf.Layout.Core.Alignment.MiddleRight:
                        return PlacementHelper.Arrange(this.Size, container, this.Alignment, new Point(-y, 0.0));

                    case DevExpress.Xpf.Layout.Core.Alignment.BottomCenter:
                        return PlacementHelper.Arrange(this.Size, container, this.Alignment, new Point(0.0, -y));
                }
                return PlacementHelper.Arrange(this.Size, container, this.Alignment, new Point(y, y));
            }

            public System.Windows.Size Size { get; private set; }

            public DevExpress.Xpf.Layout.Core.Alignment Alignment { get; private set; }

            public DockVisualizerElement Type { get; private set; }
        }
    }
}

