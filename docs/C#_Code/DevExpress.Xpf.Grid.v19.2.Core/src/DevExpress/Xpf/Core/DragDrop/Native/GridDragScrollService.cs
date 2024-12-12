namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Grid;
    using DevExpress.Xpf.Grid.Hierarchy;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    internal class GridDragScrollService : IDragScrollService
    {
        private readonly DataViewBase view;
        private double speed = 1.0;
        private double scrollActivationAreaHeight = 10.0;
        private System.Windows.Controls.Orientation orientation = System.Windows.Controls.Orientation.Vertical;
        private IScrollStrategy scrollStrategy;

        public GridDragScrollService(DataViewBase view)
        {
            Guard.ArgumentNotNull(view, "view");
            this.view = view;
            this.UpdateScrollStrategy();
        }

        private bool CanAutoScroll() => 
            this.GetContentSize() > (2.0 * this.ScrollActivationAreaSize);

        private double GetContentSize() => 
            this.scrollStrategy.GetSize(this.View.ScrollContentPresenter);

        private double GetPosition(Func<IInputElement, Point> getPosition) => 
            this.scrollStrategy.GetPosition(getPosition(this.View.ScrollContentPresenter));

        private double GetScrollNextThreshold() => 
            this.GetContentSize() - this.GetTotalScrollAreaSize(this.scrollStrategy.GetFixedNextAreaSize(this.View));

        private double GetScrollPreviousThreshold() => 
            this.GetTotalScrollAreaSize(this.scrollStrategy.GetFixedPreviousAreaSize(this.View));

        private double GetTotalScrollAreaSize(double fixedArea) => 
            this.ScrollActivationAreaSize + fixedArea;

        private void ScrollNext()
        {
            this.scrollStrategy.Scroll(this.View, this.Speed);
        }

        private void ScrollPrevious()
        {
            this.scrollStrategy.Scroll(this.View, -this.Speed);
        }

        public void Update(IDragEventArgs e)
        {
            this.Update(new Func<IInputElement, Point>(e.GetPosition));
        }

        public void Update(Func<IInputElement, Point> getPosition)
        {
            if (this.CanAutoScroll())
            {
                double position = this.GetPosition(getPosition);
                if (position < this.GetScrollPreviousThreshold())
                {
                    this.ScrollPrevious();
                }
                if (position > this.GetScrollNextThreshold())
                {
                    this.ScrollNext();
                }
            }
        }

        private void UpdateScrollStrategy()
        {
            if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
            {
                this.scrollStrategy = new HorizontalScrolling();
            }
            else
            {
                this.scrollStrategy = new VerticalScrolling();
            }
        }

        private DataViewBase View =>
            this.view;

        public double Speed
        {
            get => 
                this.speed;
            set => 
                this.speed = value;
        }

        public double ScrollActivationAreaSize
        {
            get => 
                this.scrollActivationAreaHeight;
            set => 
                this.scrollActivationAreaHeight = value;
        }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                this.orientation;
            set
            {
                if (this.orientation != value)
                {
                    this.orientation = value;
                    this.UpdateScrollStrategy();
                }
            }
        }

        private class HorizontalScrolling : GridDragScrollService.IScrollStrategy
        {
            public double GetFixedNextAreaSize(DataViewBase view) => 
                0.0;

            public double GetFixedPreviousAreaSize(DataViewBase view) => 
                0.0;

            public double GetPosition(Point point) => 
                point.X;

            public double GetSize(FrameworkElement element) => 
                element.ActualWidth;

            public void Scroll(DataViewBase view, double offset)
            {
                view.ChangeHorizontalScrollOffsetBy(offset);
            }
        }

        private interface IScrollStrategy
        {
            double GetFixedNextAreaSize(DataViewBase view);
            double GetFixedPreviousAreaSize(DataViewBase view);
            double GetPosition(Point point);
            double GetSize(FrameworkElement element);
            void Scroll(DataViewBase view, double offset);
        }

        private class VerticalScrolling : GridDragScrollService.IScrollStrategy
        {
            private HierarchyPanel GestFixedPanelOwner(DataViewBase view) => 
                view.DataPresenter.Panel;

            public double GetFixedNextAreaSize(DataViewBase view) => 
                this.GestFixedPanelOwner(view).FixedBottomRowsHeight;

            public double GetFixedPreviousAreaSize(DataViewBase view) => 
                this.GestFixedPanelOwner(view).FixedTopRowsHeight;

            public double GetPosition(Point point) => 
                point.Y;

            public double GetSize(FrameworkElement element) => 
                element.ActualHeight;

            public void Scroll(DataViewBase view, double offset)
            {
                view.ChangeVerticalScrollOffsetBy(offset);
            }
        }
    }
}

