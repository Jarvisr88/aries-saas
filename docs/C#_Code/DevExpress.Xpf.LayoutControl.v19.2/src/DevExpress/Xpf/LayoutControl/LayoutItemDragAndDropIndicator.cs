namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    public class LayoutItemDragAndDropIndicator : PanelBase
    {
        private LayoutItemInsertionInfo _InsertionInfo;

        public LayoutItemDragAndDropIndicator(FrameworkElement parentedDragControl)
        {
            this.ParentedDragControl = parentedDragControl;
            this.ShowingStoryboard = this.CreateShowingStoryboard();
        }

        protected void ActivateInsertionPointIndicator(LayoutItemInsertionPoint oldInsertionPoint, LayoutItemInsertionPoint newInsertionPoint)
        {
            LayoutItemDragAndDropInsertionPointIndicator insertionPointIndicator = this.GetInsertionPointIndicator(oldInsertionPoint);
            if (insertionPointIndicator != null)
            {
                insertionPointIndicator.IsActive = false;
            }
            insertionPointIndicator = this.GetInsertionPointIndicator(newInsertionPoint);
            if (insertionPointIndicator != null)
            {
                insertionPointIndicator.IsActive = true;
            }
        }

        protected virtual LayoutItemDragAndDropInsertionPointIndicator CreateInsertionPointIndicator(LayoutItemInsertionPoint insertionPoint) => 
            new LayoutItemDragAndDropInsertionPointIndicator(insertionPoint, this.InsertionInfo.InsertionKind);

        protected void CreateInsertionPointIndicators()
        {
            base.Children.Clear();
            foreach (LayoutItemInsertionPoint point in this.GetInsertionPoints())
            {
                LayoutItemDragAndDropInsertionPointIndicator element = this.CreateInsertionPointIndicator(point);
                element.Style = this.InsertionPointIndicatorStyle;
                base.Children.Add(element);
            }
        }

        protected virtual Storyboard CreateShowingStoryboard()
        {
            DoubleAnimation element = new DoubleAnimation();
            Storyboard.SetTarget(element, this);
            Storyboard.SetTargetProperty(element, new PropertyPath("Opacity", new object[0]));
            element.From = 0.0;
            element.To = new double?((double) 1);
            element.Duration = TimeSpan.FromMilliseconds(250.0);
            return new Storyboard { Children = { element } };
        }

        protected Rect GetInsertionPointBounds(LayoutItemInsertionPoint insertionPoint) => 
            (insertionPoint.Element.IsLayoutGroup() || ReferenceEquals(insertionPoint.Element, this.InsertionInfo.DestinationItem.GetLayoutControl())) ? ((ILayoutGroup) insertionPoint.Element).GetInsertionPointBounds(insertionPoint.IsInternalInsertion, this.InsertionInfo.DestinationItem) : insertionPoint.Element.GetBounds(this.InsertionInfo.DestinationItem);

        protected LayoutItemDragAndDropInsertionPointIndicator GetInsertionPointIndicator(LayoutItemInsertionPoint insertionPoint)
        {
            LayoutItemDragAndDropInsertionPointIndicator indicator2;
            using (IEnumerator enumerator = base.Children.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        LayoutItemDragAndDropInsertionPointIndicator current = (LayoutItemDragAndDropInsertionPointIndicator) enumerator.Current;
                        if (!current.InsertionPoint.Equals(insertionPoint))
                        {
                            continue;
                        }
                        indicator2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return indicator2;
        }

        protected virtual Rect GetInsertionPointIndicatorBounds(Rect insertionPointZoneBounds, Rect insertionPointBounds)
        {
            Rect rect = insertionPointZoneBounds;
            DevExpress.Xpf.Core.Side? side = this.InsertionInfo.InsertionKind.GetSide();
            if (side != null)
            {
                if (side.Value.GetOrientation() == Orientation.Horizontal)
                {
                    rect.X = insertionPointBounds.X;
                    rect.Width = insertionPointBounds.Width;
                }
                else
                {
                    rect.Y = insertionPointBounds.Y;
                    rect.Height = insertionPointBounds.Height;
                }
            }
            return rect;
        }

        protected LayoutItemInsertionPoints GetInsertionPoints()
        {
            LayoutItemInsertionPoints points = new LayoutItemInsertionPoints();
            if (this.DestinationItemGroup != null)
            {
                this.DestinationItemGroup.GetInsertionPoints(this.ParentedDragControl, this.InsertionInfo.DestinationItem, this.InsertionInfo.DestinationItem, this.InsertionInfo.InsertionKind, points);
            }
            return points;
        }

        protected override Size OnArrange(Rect bounds)
        {
            for (int i = 0; i < base.Children.Count; i++)
            {
                LayoutItemDragAndDropInsertionPointIndicator indicator = (LayoutItemDragAndDropInsertionPointIndicator) base.Children[i];
                Rect insertionPointIndicatorBounds = this.GetInsertionPointIndicatorBounds(this.DestinationItemGroup.GetInsertionPointZoneBounds(this.InsertionInfo.DestinationItem, this.InsertionInfo.InsertionKind, i, base.Children.Count), this.GetInsertionPointBounds(indicator.InsertionPoint));
                indicator.Arrange(insertionPointIndicatorBounds);
            }
            return base.OnArrange(bounds);
        }

        protected virtual void OnInsertionInfoChanged(LayoutItemInsertionInfo oldInsertionInfo)
        {
            if (!ReferenceEquals(this.InsertionInfo.DestinationItem, oldInsertionInfo.DestinationItem) && (this.InsertionInfo.DestinationItem != null))
            {
                this.SetBounds(this.InsertionInfo.DestinationItem.GetBounds((FrameworkElement) base.Parent));
            }
            if (!ReferenceEquals(this.InsertionInfo.DestinationItem, oldInsertionInfo.DestinationItem) || (this.InsertionInfo.InsertionKind != oldInsertionInfo.InsertionKind))
            {
                this.UpdateInsertionPointIndicators(oldInsertionInfo.InsertionPoint);
                oldInsertionInfo.InsertionPoint = null;
            }
            if (!ReferenceEquals(this.InsertionInfo.InsertionPoint, oldInsertionInfo.InsertionPoint))
            {
                this.ActivateInsertionPointIndicator(oldInsertionInfo.InsertionPoint, this.InsertionInfo.InsertionPoint);
            }
        }

        protected override Size OnMeasure(Size availableSize)
        {
            foreach (UIElement element in base.Children)
            {
                element.Measure(SizeHelper.Infinite);
            }
            return base.OnMeasure(availableSize);
        }

        protected void UpdateInsertionPointIndicators(LayoutItemInsertionPoint oldInsertionPoint)
        {
            if (this.ShowingStoryboard != null)
            {
                this.ShowingStoryboard.Stop();
            }
            this.ActivateInsertionPointIndicator(oldInsertionPoint, null);
            this.CreateInsertionPointIndicators();
            if (this.ShowingStoryboard != null)
            {
                this.ShowingStoryboard.Begin();
            }
        }

        public LayoutItemInsertionInfo InsertionInfo
        {
            get => 
                this._InsertionInfo;
            set
            {
                if (!this.InsertionInfo.Equals(value))
                {
                    LayoutItemInsertionInfo insertionInfo = this.InsertionInfo;
                    this._InsertionInfo = value;
                    this.OnInsertionInfoChanged(insertionInfo);
                }
            }
        }

        public Style InsertionPointIndicatorStyle { get; set; }

        protected ILayoutGroup DestinationItemGroup =>
            (this.InsertionInfo.DestinationItem != null) ? (this.InsertionInfo.DestinationItem.Parent as ILayoutGroup) : null;

        protected FrameworkElement ParentedDragControl { get; private set; }

        protected Storyboard ShowingStoryboard { get; private set; }
    }
}

