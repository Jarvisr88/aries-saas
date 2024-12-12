namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections;
    using System.Windows;
    using System.Windows.Controls;

    public class MDIPanel : ScrollablePanel
    {
        static MDIPanel()
        {
            new DependencyPropertyRegistrator<MDIPanel>().OverrideMetadataNotDataBindable<bool>(Panel.IsItemsHostProperty, true, null, null);
        }

        protected override unsafe Size ArrangeOverride(Size arrangeSize)
        {
            Rect rect = new Rect();
            foreach (UIElement element in base.InternalChildren)
            {
                if (element != null)
                {
                    Rect elementRect = this.GetElementRect(arrangeSize, element);
                    rect.Union(elementRect);
                    Rect* rectPtr1 = &elementRect;
                    rectPtr1.X -= base.HorizontalOffset;
                    Rect* rectPtr2 = &elementRect;
                    rectPtr2.Y -= base.VerticalOffset;
                    element.Arrange(elementRect);
                }
            }
            base.VerifyScrollData(arrangeSize, rect.GetSize());
            return arrangeSize;
        }

        private Size CheckConstraints(Size constraint)
        {
            ScrollContentPresenter presenter = LayoutHelper.FindParentObject<ScrollContentPresenter>(this);
            if (double.IsInfinity(constraint.Width))
            {
                constraint.Width = (presenter != null) ? presenter.ViewportWidth : 1000.0;
            }
            if (double.IsInfinity(constraint.Height))
            {
                constraint.Height = (presenter != null) ? presenter.ViewportHeight : 1000.0;
            }
            return constraint;
        }

        private void CheckInternalChildren()
        {
            foreach (UIElement element in base.InternalChildren)
            {
                if ((element != null) && (MDIStateHelper.GetMDIState(element) == MDIState.Maximized))
                {
                    base.InvalidateMeasure();
                    break;
                }
            }
        }

        private Size CheckMDISize(MDIState state, Size mdiSize, Size constraint)
        {
            if (double.IsNaN(mdiSize.Width))
            {
                mdiSize.Width = double.PositiveInfinity;
            }
            if (double.IsNaN(mdiSize.Height))
            {
                mdiSize.Height = double.PositiveInfinity;
            }
            if (state == MDIState.Maximized)
            {
                mdiSize = constraint;
            }
            return mdiSize;
        }

        public MDIDocument GetActiveDocument()
        {
            MDIDocument document;
            using (IEnumerator enumerator = base.InternalChildren.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        UIElement current = (UIElement) enumerator.Current;
                        if (current == null)
                        {
                            continue;
                        }
                        BaseLayoutItem objB = (current as BaseLayoutItem) ?? DockLayoutManager.GetLayoutItem(current);
                        if ((objB == null) || ((objB.Parent == null) || !ReferenceEquals(objB.Parent.SelectedItem, objB)))
                        {
                            continue;
                        }
                        document = (current as MDIDocument) ?? LayoutItemsHelper.GetVisualChild<MDIDocument>(objB);
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return document;
        }

        private Rect GetElementRect(Size constraint, UIElement element)
        {
            BaseLayoutItem item = (element as BaseLayoutItem) ?? DockLayoutManager.GetLayoutItem(element);
            MDIState mDIState = MDIStateHelper.GetMDIState(element);
            Point mDILocation = DocumentPanel.GetMDILocation(element);
            Size size = (mDIState == MDIState.Minimized) ? new Size(double.NaN, double.NaN) : DocumentPanel.GetMDISize(element);
            if (double.IsNaN(size.Height) || double.IsNaN(size.Width))
            {
                Size actualMinSize = item.ActualMinSize;
                Size actualMaxSize = item.ActualMaxSize;
                double width = size.Width;
                double height = size.Height;
                if (double.IsNaN(width))
                {
                    width = MathHelper.MeasureDimension(actualMinSize.Width, actualMaxSize.Width, element.DesiredSize.Width);
                }
                if (double.IsNaN(height))
                {
                    height = MathHelper.MeasureDimension(actualMinSize.Height, actualMaxSize.Height, element.DesiredSize.Height);
                }
                size = new Size(width, height);
            }
            if (mDIState == MDIState.Maximized)
            {
                mDILocation = new Point(0.0, 0.0);
                size = constraint;
            }
            return new Rect(mDILocation, size);
        }

        protected override Size MeasureOverride(Size avalaibleSize)
        {
            Rect rect = new Rect();
            Size constraint = this.CheckConstraints(avalaibleSize);
            foreach (UIElement element in base.InternalChildren)
            {
                if (element != null)
                {
                    element.Measure(this.CheckMDISize(MDIStateHelper.GetMDIState(element), DocumentPanel.GetMDISize(element), constraint));
                    rect.Union(this.GetElementRect(constraint, element));
                }
            }
            base.VerifyScrollData(avalaibleSize, rect.GetSize());
            return base.Viewport;
        }

        protected override void OnActualSizeChanged(Size value)
        {
            base.OnActualSizeChanged(value);
            this.CheckInternalChildren();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            this.CheckInternalChildren();
        }
    }
}

