namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Printing;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;

    public class ScrollablePageView : Control, IScrollInfo
    {
        public static readonly DependencyProperty ModelProperty = DependencyPropertyManager.Register("Model", typeof(IPreviewModel), typeof(ScrollablePageView), new PropertyMetadata(null, new PropertyChangedCallback(ScrollablePageView.ModelChangedCallback)));
        public static readonly DependencyProperty PageMarginProperty = DependencyPropertyManager.Register("PageMargin", typeof(Thickness), typeof(ScrollablePageView), new PropertyMetadata(new Thickness(3.0), new PropertyChangedCallback(ScrollablePageView.PageMarginChangedCallback)));
        private InputController inputController;
        private readonly Size InfiniteSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
        protected readonly TranslateTransform transform = new TranslateTransform();
        private readonly ManipulationHelper manipulationHelper;
        protected PageDraggingImplementer pageDraggingImplementer;
        protected ScrollInfoBase scrollInfo;
        protected double pageWithMarginPositionX;
        protected double pageWithMarginPositionY;

        static ScrollablePageView()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ScrollablePageView), new FrameworkPropertyMetadata(typeof(ScrollablePageView)));
        }

        public ScrollablePageView()
        {
            base.KeyDown += new KeyEventHandler(this.ScrollablePageView_KeyDown);
            base.KeyUp += new KeyEventHandler(this.ScrollablePageView_KeyUp);
            base.MouseLeftButtonDown += new MouseButtonEventHandler(this.ScrollablePageView_MouseLeftButtonDown);
            base.MouseLeftButtonUp += new MouseButtonEventHandler(this.ScrollablePageView_MouseLeftButtonUp);
            base.MouseRightButtonDown += new MouseButtonEventHandler(this.ScrollablePageView_MouseRightButtonDown);
            base.MouseWheel += new MouseWheelEventHandler(this.ScrollablePageView_MouseWheel);
            base.MouseMove += new MouseEventHandler(this.ScrollablePageView_MouseMove);
            base.Loaded += new RoutedEventHandler(this.ScrollablePageView_Loaded);
            base.SizeChanged += new SizeChangedEventHandler(this.ScrollablePageView_SizeChanged);
            base.IsManipulationEnabled = true;
            this.manipulationHelper = new ManipulationHelper(this);
            this.scrollInfo = new NullScrollInfo(this);
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            base.ArrangeOverride(arrangeBounds);
            FrameworkElement pageBorder = this.GetPageBorder();
            if (pageBorder != null)
            {
                Point dragOffset;
                double a = (arrangeBounds.Width > pageBorder.DesiredSize.Width) ? ((arrangeBounds.Width - pageBorder.DesiredSize.Width) / 2.0) : 0.0;
                double num2 = (arrangeBounds.Height > pageBorder.DesiredSize.Height) ? ((arrangeBounds.Height - pageBorder.DesiredSize.Height) / 2.0) : 0.0;
                if (this.pageDraggingImplementer != null)
                {
                    dragOffset = this.pageDraggingImplementer.DragOffset;
                }
                else
                {
                    dragOffset = new Point();
                }
                Point point = dragOffset;
                this.pageWithMarginPositionX = Math.Round(a) + point.X;
                this.pageWithMarginPositionY = Math.Round(num2) + point.Y;
                pageBorder.Arrange(new Rect(new Point(Math.Max(0.0, this.pageWithMarginPositionX), Math.Max(0.0, this.pageWithMarginPositionY)), pageBorder.DesiredSize));
            }
            this.scrollInfo.ValidateScrollData();
            this.scrollInfo.InvalidateScrollInfo();
            if (this.scrollInfo.IsVerticalScrollDataValid())
            {
                this.scrollInfo.SetCurrentPageIndex();
            }
            this.transform.X = this.scrollInfo.IsHorizontalScrollDataValid() ? Math.Round(this.scrollInfo.GetTransformX()) : 0.0;
            this.transform.Y = this.scrollInfo.IsVerticalScrollDataValid() ? Math.Round(this.scrollInfo.GetTransformY()) : 0.0;
            if (pageBorder != null)
            {
                Point correctedRenderOffset = RenderOffsetHelper.GetCorrectedRenderOffset(pageBorder, LayoutHelper.GetTopLevelVisual(this));
                this.transform.X += correctedRenderOffset.X;
                this.transform.Y += correctedRenderOffset.Y;
            }
            return arrangeBounds;
        }

        protected FrameworkElement GetPageBorder() => 
            (FrameworkElement) base.GetTemplateChild("pageBorder");

        private bool IsScrollNavigationByKeyUsed(Key pressedKey)
        {
            bool flag = ((pressedKey == Key.Left) || (pressedKey == Key.Right)) ? ((this.ExtentWidth - this.ViewportWidth) > 0.0) : false;
            bool flag2 = ((pressedKey == Key.Up) || (pressedKey == Key.Down)) ? ((this.ExtentHeight - this.ViewportHeight) > 0.0) : false;
            return (!InputController.AreModifiersPressed && ((pressedKey == Key.Next) || ((pressedKey == Key.Prior) || (flag | flag2))));
        }

        public void LineDown()
        {
            this.scrollInfo.LineDown();
        }

        public void LineLeft()
        {
            this.scrollInfo.LineLeft();
        }

        public void LineRight()
        {
            this.scrollInfo.LineRight();
        }

        public void LineUp()
        {
            this.scrollInfo.LineUp();
        }

        public Rect MakeVisible(Visual visual, Rect rectangle) => 
            !ReferenceEquals(visual, this) ? this.scrollInfo.MakeVisible(visual, rectangle) : rectangle;

        protected override Size MeasureOverride(Size constraint)
        {
            FrameworkElement pageBorder = this.GetPageBorder();
            if (pageBorder == null)
            {
                return constraint;
            }
            pageBorder.Measure(this.InfiniteSize);
            return new Size(double.IsInfinity(constraint.Width) ? pageBorder.DesiredSize.Width : constraint.Width, double.IsInfinity(constraint.Height) ? pageBorder.DesiredSize.Height : constraint.Height);
        }

        protected virtual void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if ((this.Model.ZoomMode is ZoomFitModeItem) && ((e.PropertyName == "ZoomMode") || ((e.PropertyName == "IsCreating") || (e.PropertyName == "PageContent"))))
            {
                this.SyncZoom(((ZoomFitModeItem) this.Model.ZoomMode).ZoomFitMode);
            }
        }

        private static void ModelChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((ScrollablePageView) sender).OnModelChanged(e);
        }

        public void MouseWheelDown()
        {
            this.scrollInfo.MouseWheelDown();
        }

        public void MouseWheelLeft()
        {
            this.scrollInfo.MouseWheelLeft();
        }

        public void MouseWheelRight()
        {
            this.scrollInfo.MouseWheelRight();
        }

        public void MouseWheelUp()
        {
            this.scrollInfo.MouseWheelUp();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            FrameworkElement pageBorder = this.GetPageBorder();
            if (pageBorder != null)
            {
                pageBorder.RenderTransform = this.transform;
            }
        }

        protected override void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
            base.OnManipulationDelta(e);
            IPreviewModel model = this.Model;
            model.Zoom *= Math.Max(e.DeltaManipulation.Scale.X, e.DeltaManipulation.Scale.Y);
            this.manipulationHelper.OnManipulationDelta(e);
        }

        protected override void OnManipulationInertiaStarting(ManipulationInertiaStartingEventArgs e)
        {
            base.OnManipulationInertiaStarting(e);
            this.manipulationHelper.OnManipulationInertiaStarting(e);
        }

        protected override void OnManipulationStarting(ManipulationStartingEventArgs e)
        {
            base.OnManipulationStarting(e);
            this.manipulationHelper.OnManipulationStarting(e);
        }

        protected virtual void OnModelChanged(DependencyPropertyChangedEventArgs e)
        {
            ScrollViewer scrollOwner = this.scrollInfo.ScrollOwner;
            if (e.OldValue != null)
            {
                IPreviewModel oldValue = (IPreviewModel) e.OldValue;
                this.scrollInfo = new NullScrollInfo(this);
                oldValue.PropertyChanged -= new PropertyChangedEventHandler(this.Model_PropertyChanged);
            }
            if (e.NewValue != null)
            {
                IPreviewModel newValue = (IPreviewModel) e.NewValue;
                newValue.PropertyChanged += new PropertyChangedEventHandler(this.Model_PropertyChanged);
                this.pageDraggingImplementer = new PageDraggingImplementer(newValue, this, PageDraggingType.DragViaScrollViewer);
                this.inputController = this.Model.InputController;
                if (newValue.UseSimpleScrolling)
                {
                    this.scrollInfo = new SimpleScrollInfo(this, newValue, this.PageMargin);
                }
                else if (newValue is IDocumentPreviewModel)
                {
                    this.scrollInfo = new ScrollInfo(this, (IDocumentPreviewModel) newValue, this.PageMargin);
                }
            }
            this.scrollInfo.ScrollOwner = scrollOwner;
        }

        private void OnPageMarginChanged(DependencyPropertyChangedEventArgs e)
        {
            this.scrollInfo.PageMargin = (Thickness) e.NewValue;
        }

        public void PageDown()
        {
            this.scrollInfo.PageDown();
        }

        public void PageLeft()
        {
            this.scrollInfo.PageLeft();
        }

        private static void PageMarginChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((ScrollablePageView) sender).OnPageMarginChanged(e);
        }

        public void PageRight()
        {
            this.scrollInfo.PageRight();
        }

        public void PageUp()
        {
            this.scrollInfo.PageUp();
        }

        private void ScrollablePageView_KeyDown(object sender, KeyEventArgs e)
        {
            if (!this.IsScrollNavigationByKeyUsed(e.Key) && (e.Key != Key.System))
            {
                if (this.inputController != null)
                {
                    this.inputController.HandleKeyDown(e.Key);
                }
                if (this.pageDraggingImplementer != null)
                {
                    this.pageDraggingImplementer.HandleKeyDown(e.Key);
                    if (this.pageDraggingImplementer.IsPageDraggingEnabled && ((this.Model != null) && (this.Model.PageContent != null)))
                    {
                        this.Model.PageContent.IsHitTestVisible = false;
                    }
                }
                if (this.ShouldSuppressScrollNavigation(e.Key))
                {
                    e.Handled = true;
                }
            }
        }

        private void ScrollablePageView_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.pageDraggingImplementer != null)
            {
                this.pageDraggingImplementer.HandleKeyUp(e.Key);
                if (!this.pageDraggingImplementer.IsPageDraggingEnabled && ((this.Model != null) && (this.Model.PageContent != null)))
                {
                    this.Model.PageContent.IsHitTestVisible = true;
                }
            }
        }

        private void ScrollablePageView_Loaded(object sender, RoutedEventArgs e)
        {
            FrameworkElement pageBorder = this.GetPageBorder();
            if (pageBorder != null)
            {
                pageBorder.RenderTransform = this.transform;
            }
        }

        private void ScrollablePageView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.Focus();
            if ((this.inputController != null) && InputController.AreModifiersPressed)
            {
                this.inputController.HandleMouseDown(MouseButton.Left);
            }
            if ((this.pageDraggingImplementer == null) || !this.pageDraggingImplementer.IsPageDraggingEnabled)
            {
                this.SendMouseDownToModel(e);
            }
            else
            {
                Point position = e.GetPosition(this);
                this.pageDraggingImplementer.HandleMouseDown(position);
            }
        }

        private void ScrollablePageView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if ((this.pageDraggingImplementer != null) && this.pageDraggingImplementer.IsPageDraggingEnabled)
            {
                bool flag;
                this.pageDraggingImplementer.HandleMouseUp(out flag);
                e.Handled = flag;
            }
            else if ((this.Model != null) && (this.Model.PageContent != null))
            {
                this.Model.PageContent.IsHitTestVisible = true;
            }
        }

        private void ScrollablePageView_MouseMove(object sender, MouseEventArgs e)
        {
            this.SendMouseMoveToModel(e);
        }

        private void ScrollablePageView_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.Focus();
            if ((this.inputController != null) && InputController.AreModifiersPressed)
            {
                this.inputController.HandleMouseDown(MouseButton.Right);
            }
        }

        private void ScrollablePageView_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if ((this.inputController != null) && InputController.AreModifiersPressed)
            {
                this.inputController.HandleMouseWheel(e.Delta);
                e.Handled = true;
            }
        }

        private void ScrollablePageView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.scrollInfo.InvalidateScrollInfo();
            if ((this.Model != null) && (this.Model.ZoomMode is ZoomFitModeItem))
            {
                this.SyncZoom(((ZoomFitModeItem) this.Model.ZoomMode).ZoomFitMode);
            }
        }

        private void ScrollOwner_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((this.pageDraggingImplementer == null) || !this.pageDraggingImplementer.IsPageDraggingEnabled)
            {
                this.SendMouseDoubleClickToModel(e);
            }
        }

        private void ScrollOwner_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.pageDraggingImplementer != null)
            {
                Point position = e.GetPosition(this);
                this.pageDraggingImplementer.HandleMouseMove(position);
            }
        }

        private void ScrollOwner_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if ((this.pageDraggingImplementer == null) || !this.pageDraggingImplementer.IsPageDraggingEnabled)
            {
                this.SendMouseUpToModel(e);
            }
        }

        private void SendMouseDoubleClickToModel(MouseEventArgs e)
        {
            if (this.Model != null)
            {
                this.Model.HandlePreviewDoubleClick(e, this);
            }
        }

        private void SendMouseDownToModel(MouseButtonEventArgs e)
        {
            if (this.Model != null)
            {
                this.Model.HandlePreviewMouseLeftButtonDown(e, this);
            }
        }

        private void SendMouseMoveToModel(MouseEventArgs e)
        {
            if (this.Model != null)
            {
                this.Model.HandlePreviewMouseMove(e, this);
            }
        }

        private void SendMouseUpToModel(MouseButtonEventArgs e)
        {
            if (this.Model != null)
            {
                this.Model.HandlePreviewMouseLeftButtonUp(e, this);
            }
        }

        public void SetHorizontalOffset(double offset)
        {
            this.scrollInfo.SetHorizontalOffset(offset);
        }

        public void SetVerticalOffset(double offset)
        {
            this.scrollInfo.SetVerticalOffset(offset);
        }

        private bool ShouldSuppressScrollNavigation(Key pressedKey) => 
            InputController.AreModifiersPressed && ((pressedKey == Key.Left) || ((pressedKey == Key.Right) || ((pressedKey == Key.Up) || ((pressedKey == Key.Down) || ((pressedKey == Key.Next) || ((pressedKey == Key.Prior) || ((pressedKey == Key.Prior) || (pressedKey == Key.Next))))))));

        private void SubscribeScrollOwnerToEvents()
        {
            if (this.ScrollOwner != null)
            {
                this.ScrollOwner.MouseMove += new MouseEventHandler(this.ScrollOwner_MouseMove);
                this.ScrollOwner.MouseLeftButtonUp += new MouseButtonEventHandler(this.ScrollOwner_MouseUp);
                this.ScrollOwner.MouseDoubleClick += new MouseButtonEventHandler(this.ScrollOwner_MouseDoubleClick);
            }
        }

        protected void SyncZoom(ZoomFitMode mode)
        {
            if ((this.Model.PageContent != null) && (!this.Model.PageContent.Width.IsNotNumber() && !this.Model.PageContent.Height.IsNotNumber()))
            {
                double num = (100.0 * ((((this.ViewportHeight - this.PageMargin.Top) - this.PageMargin.Bottom) - base.BorderThickness.Top) - base.BorderThickness.Left)) / this.Model.PageContent.Height;
                double num2 = (100.0 * ((((this.ViewportWidth - this.PageMargin.Left) - this.PageMargin.Right) - base.BorderThickness.Left) - base.BorderThickness.Right)) / this.Model.PageContent.Width;
                switch (mode)
                {
                    case ZoomFitMode.PageWidth:
                        this.Model.SetZoom(num2);
                        return;

                    case ZoomFitMode.PageHeight:
                        this.Model.SetZoom(num);
                        return;

                    case ZoomFitMode.WholePage:
                        this.Model.SetZoom((num > num2) ? num2 : num);
                        return;
                }
                throw new NotSupportedException();
            }
        }

        private void UnsubscribeScrollOwnerFromEvents()
        {
            if (this.ScrollOwner != null)
            {
                this.ScrollOwner.MouseMove -= new MouseEventHandler(this.ScrollOwner_MouseMove);
                this.ScrollOwner.MouseLeftButtonUp -= new MouseButtonEventHandler(this.ScrollOwner_MouseUp);
                this.ScrollOwner.MouseDoubleClick -= new MouseButtonEventHandler(this.ScrollOwner_MouseDoubleClick);
            }
        }

        public virtual void UpdatePagePosition()
        {
        }

        public IPreviewModel Model
        {
            get => 
                (IPreviewModel) base.GetValue(ModelProperty);
            set => 
                base.SetValue(ModelProperty, value);
        }

        public Thickness PageMargin
        {
            get => 
                (Thickness) base.GetValue(PageMarginProperty);
            set => 
                base.SetValue(PageMarginProperty, value);
        }

        public bool CanHorizontallyScroll
        {
            get => 
                this.scrollInfo.CanHorizontallyScroll;
            set => 
                this.scrollInfo.CanHorizontallyScroll = value;
        }

        public bool CanVerticallyScroll
        {
            get => 
                this.scrollInfo.CanVerticallyScroll;
            set => 
                this.scrollInfo.CanVerticallyScroll = value;
        }

        public double ExtentHeight =>
            this.scrollInfo.ExtentHeight;

        public double ExtentWidth =>
            this.scrollInfo.ExtentWidth;

        public double HorizontalOffset =>
            this.scrollInfo.HorizontalOffset;

        public ScrollViewer ScrollOwner
        {
            get => 
                this.scrollInfo.ScrollOwner;
            set
            {
                this.UnsubscribeScrollOwnerFromEvents();
                this.scrollInfo.ScrollOwner = value;
                this.SubscribeScrollOwnerToEvents();
            }
        }

        public double VerticalOffset =>
            this.scrollInfo.VerticalOffset;

        public double ViewportHeight =>
            this.scrollInfo.ViewportHeight;

        public double ViewportWidth =>
            this.scrollInfo.ViewportWidth;
    }
}

