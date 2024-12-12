namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DragAdorner : PlacementAdorner
    {
        public DragAdorner(DockLayoutManager container) : base(container)
        {
        }

        protected override BaseSurfacedAdorner.BaseAdornerSurface CreateAdornerSurface() => 
            new DragAdornerSurface(this, false);

        protected override void OnActivated()
        {
            base.OnActivated();
            WindowHelper.BindFlowDirectionIfNeeded(this, base.AdornedElement);
        }

        protected override void SetBoundsInContainerCore(UIElement placementElement, Rect bounds)
        {
            ISupportAutoSize size = placementElement as ISupportAutoSize;
            if ((size == null) || !size.IsAutoSize)
            {
                base.SetBoundsInContainerCore(placementElement, bounds);
            }
            else
            {
                Size size2 = size.FitToContent(bounds.Size);
                base.PlacementSurface.SetBounds(placementElement, new Rect(bounds.Location, size2));
            }
        }

        public class DragAdornerSurface : PlacementAdorner.AdornerSurface
        {
            public DragAdornerSurface(PlacementAdorner adorner) : base(adorner)
            {
            }

            public DragAdornerSurface(PlacementAdorner adorner, bool enableValidation) : base(adorner, enableValidation)
            {
            }

            protected override void BringToFrontCore(UIElement placementElement)
            {
                int num = 0;
                Func<UIElement, int> keySelector = <>c.<>9__2_0;
                if (<>c.<>9__2_0 == null)
                {
                    Func<UIElement, int> local1 = <>c.<>9__2_0;
                    keySelector = <>c.<>9__2_0 = x => GetZIndex(x);
                }
                foreach (UIElement element in base.PlacementInfos.Keys.OrderBy<UIElement, int>(keySelector))
                {
                    if (!ReferenceEquals(element, placementElement))
                    {
                        SetZIndex(element, num++);
                    }
                }
                SetZIndex(placementElement, num++);
            }

            protected override Size MeasureOverride(Size availableSize)
            {
                foreach (KeyValuePair<UIElement, PlacementAdorner.PlacementItemInfo> pair in base.PlacementInfos)
                {
                    ISupportAutoSize key = pair.Key as ISupportAutoSize;
                    if ((key != null) && key.IsAutoSize)
                    {
                        Rect bounds = pair.Value.Bounds;
                        Size size = key.FitToContent(bounds.Size);
                        base.SetBounds(pair.Key, new Rect(bounds.Location, size));
                    }
                }
                return base.MeasureOverride(availableSize);
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly DragAdorner.DragAdornerSurface.<>c <>9 = new DragAdorner.DragAdornerSurface.<>c();
                public static Func<UIElement, int> <>9__2_0;

                internal int <BringToFrontCore>b__2_0(UIElement x) => 
                    Panel.GetZIndex(x);
            }
        }
    }
}

