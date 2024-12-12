namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    public class PlacementAdorner : BaseSurfacedAdorner
    {
        public PlacementAdorner(UIElement container) : base(container)
        {
        }

        public void BringToFront(UIElement placementElement)
        {
            this.PlacementSurface.BringToFront(placementElement);
        }

        protected override BaseSurfacedAdorner.BaseAdornerSurface CreateAdornerSurface() => 
            new AdornerSurface(this);

        public Rect GetBoundsInContainer(UIElement placementElement) => 
            this.PlacementSurface.GetBounds(placementElement);

        public bool GetVisible(UIElement placementElement) => 
            this.PlacementSurface.GetVisible(placementElement);

        public bool IsRegistered(UIElement placementElement) => 
            this.PlacementSurface.IsPlacementElementAdded(placementElement);

        public void Register(UIElement placementElement)
        {
            this.PlacementSurface.AddPlacementElement(placementElement);
        }

        public void SetBoundsInContainer(UIElement placementElement, Rect bounds)
        {
            this.SetBoundsInContainerCore(placementElement, bounds);
        }

        protected virtual void SetBoundsInContainerCore(UIElement placementElement, Rect bounds)
        {
            placementElement.Measure(bounds.Size);
            this.PlacementSurface.SetBounds(placementElement, bounds);
        }

        public void SetVisible(UIElement placementElement, bool visible)
        {
            this.PlacementSurface.SetVisible(placementElement, visible);
        }

        public void Unregister(UIElement placementElement)
        {
            this.PlacementSurface.RemovePlacementElement(placementElement);
        }

        protected internal AdornerSurface PlacementSurface =>
            base.Surface as AdornerSurface;

        public class AdornerSurface : BaseSurfacedAdorner.BaseAdornerSurface
        {
            private readonly bool EnableValidation;
            private Dictionary<UIElement, PlacementAdorner.PlacementItemInfo> placementInfosCore;

            public AdornerSurface(PlacementAdorner adorner) : this(adorner, true)
            {
            }

            public AdornerSurface(PlacementAdorner adorner, bool enableValidation) : base(adorner)
            {
                this.placementInfosCore = new Dictionary<UIElement, PlacementAdorner.PlacementItemInfo>();
                this.EnableValidation = enableValidation;
            }

            public void AddPlacementElement(UIElement placementElement)
            {
                if (this.EnableValidation)
                {
                    this.ValidateElementIsNotAdded(placementElement);
                }
                this.PlacementInfos.Add(placementElement, new PlacementAdorner.PlacementItemInfo(this, placementElement));
                base.Children.Add(placementElement);
            }

            protected override Size ArrangeOverride(Size finalSize)
            {
                foreach (KeyValuePair<UIElement, PlacementAdorner.PlacementItemInfo> pair in this.PlacementInfos)
                {
                    pair.Key.Arrange(pair.Value.Bounds);
                }
                return finalSize;
            }

            public void BringToFront(UIElement placementElement)
            {
                if (this.EnableValidation)
                {
                    this.ValidateElementIsAdded(placementElement);
                }
                this.BringToFrontCore(placementElement);
                this.Adorner.Update();
            }

            protected virtual void BringToFrontCore(UIElement placementElement)
            {
                int num = 0;
                foreach (KeyValuePair<UIElement, PlacementAdorner.PlacementItemInfo> pair in this.PlacementInfos)
                {
                    if (pair.Key != placementElement)
                    {
                        SetZIndex(pair.Key, num++);
                    }
                }
                SetZIndex(placementElement, num++);
            }

            public Rect GetBounds(UIElement placementElement) => 
                this.GetValidatedElement(placementElement).Bounds;

            private PlacementAdorner.PlacementItemInfo GetValidatedElement(UIElement placementElement)
            {
                if (this.EnableValidation)
                {
                    this.ValidateElementIsAdded(placementElement);
                }
                return this.PlacementInfos[placementElement];
            }

            public bool GetVisible(UIElement placementElement) => 
                this.GetValidatedElement(placementElement).Visible;

            public bool IsPlacementElementAdded(UIElement placementElement) => 
                this.PlacementInfos.ContainsKey(placementElement);

            public void RemovePlacementElement(UIElement placementElement)
            {
                if (this.EnableValidation)
                {
                    this.ValidateElementIsAdded(placementElement);
                }
                this.PlacementInfos.Remove(placementElement);
                base.Children.Remove(placementElement);
            }

            public void SetBounds(UIElement placementElement, Rect bounds)
            {
                this.GetValidatedElement(placementElement).Bounds = bounds;
                this.Adorner.Update();
            }

            public void SetVisible(UIElement placementElement, bool visible)
            {
                this.GetValidatedElement(placementElement).Visible = visible;
                this.Adorner.Update();
            }

            private void ValidateElementIsAdded(UIElement placementElement)
            {
                if (!this.IsPlacementElementAdded(placementElement))
                {
                    throw new ArgumentException("Element has not been found");
                }
            }

            private void ValidateElementIsNotAdded(UIElement placementElement)
            {
                if (this.IsPlacementElementAdded(placementElement))
                {
                    throw new ArgumentException("Element has already been added");
                }
            }

            protected internal PlacementAdorner Adorner =>
                base.BaseAdorner as PlacementAdorner;

            protected internal Dictionary<UIElement, PlacementAdorner.PlacementItemInfo> PlacementInfos =>
                this.placementInfosCore;
        }

        public class PlacementItemInfo
        {
            private PlacementAdorner.AdornerSurface surfaceCore;
            private UIElement placementItemCore;
            private Rect boundsCore;
            private bool visibleCore;

            public PlacementItemInfo(PlacementAdorner.AdornerSurface surface, UIElement placementItem)
            {
                this.surfaceCore = surface;
                this.placementItemCore = placementItem;
            }

            protected void CheckVisible()
            {
                Visibility visibility = this.Visible ? Visibility.Visible : Visibility.Collapsed;
                if (this.PlacementItem.Visibility != visibility)
                {
                    this.PlacementItem.Visibility = visibility;
                }
            }

            protected void UpdateBounds()
            {
            }

            public PlacementAdorner.AdornerSurface Surface =>
                this.surfaceCore;

            public UIElement PlacementItem =>
                this.placementItemCore;

            public Rect Bounds
            {
                get => 
                    this.boundsCore;
                set
                {
                    if (this.boundsCore != value)
                    {
                        this.boundsCore = value;
                        this.UpdateBounds();
                    }
                }
            }

            public bool Visible
            {
                get => 
                    this.visibleCore;
                set
                {
                    this.visibleCore = value;
                    this.CheckVisible();
                }
            }
        }
    }
}

