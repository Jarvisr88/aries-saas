namespace DevExpress.Xpf.Editors.RangeControl.Internal
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.RangeControl;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;

    public class RangeBar : Control
    {
        private const double MinThumbsSpacing = 10.0;
        public static readonly DependencyProperty SelectionBrushProperty;
        private Grid rootContainer;
        private Thumb leftResizeThumb;
        private Thumb rightResizeThumb;
        private Thumb slider;
        private Grid middleContainer;
        private Grid topContainer;
        private Grid thumbContainer;
        private double prevResizeThumbPosition;
        private bool isViewPortStartResizing;
        private double prevSliderPostion;
        private double leftThumbPosition;
        private double rightThumbPosition;

        static RangeBar()
        {
            SelectionBrushProperty = DependencyProperty.Register("SelectionBrush", typeof(Brush), typeof(RangeBar), new FrameworkPropertyMetadata((d, e) => ((RangeBar) d).OnSelectionBrushChanged()));
        }

        public RangeBar()
        {
            base.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
        }

        private bool CanRenderSelectionSide() => 
            (this.rootContainer != null) && ((this.Maximum - this.Minimum) >= 0.0);

        private double ConstrainZero(double value) => 
            double.IsNaN(value) ? 0.0 : Math.Max(value, 0.0);

        private double GetCorrectNormalValue(double value) => 
            Math.Min(Math.Max(value, 0.0), 1.0);

        private double GetElementWidth(FrameworkElement element) => 
            (element == null) ? 0.0 : element.ActualWidth;

        private void InitializeElements()
        {
            if (this.rootContainer != null)
            {
                this.InitializeTransparecyEffect();
                this.leftResizeThumb = LayoutHelper.FindElementByName(this.rootContainer, "PART_LeftResizeThumb") as Thumb;
                this.rightResizeThumb = LayoutHelper.FindElementByName(this.rootContainer, "PART_RightResizeThumb") as Thumb;
                this.slider = LayoutHelper.FindElementByName(this.rootContainer, "PART_Slider") as Thumb;
                this.middleContainer = LayoutHelper.FindElementByName(this.rootContainer, "PART_MiddleLayerContainer") as Grid;
                this.thumbContainer = LayoutHelper.FindElementByName(this.rootContainer, "PART_ThumbContainer") as Grid;
                this.topContainer = LayoutHelper.FindElementByName(this.rootContainer, "PART_TopLayerContainer") as Grid;
                this.slider.DragStarted += new DragStartedEventHandler(this.OnSliderDragStarted);
                this.slider.DragDelta += new DragDeltaEventHandler(this.SliderDragDelta);
                this.slider.DragCompleted += new DragCompletedEventHandler(this.OnSliderDragCompleted);
                this.leftResizeThumb.DragStarted += new DragStartedEventHandler(this.OnLeftResizeThumbDragStarted);
                this.leftResizeThumb.DragCompleted += new DragCompletedEventHandler(this.OnLeftResizeThumbDragCompleted);
                this.leftResizeThumb.DragDelta += new DragDeltaEventHandler(this.leftResizeThumb_DragDelta);
                this.rightResizeThumb.DragStarted += new DragStartedEventHandler(this.OnRightResizeThumbDragStarted);
                this.rightResizeThumb.DragCompleted += new DragCompletedEventHandler(this.OnRightResizeThumbDragCompleted);
                this.rightResizeThumb.DragDelta += new DragDeltaEventHandler(this.leftResizeThumb_DragDelta);
                base.MouseLeftButtonUp += new MouseButtonEventHandler(this.RangeBarMouseLeftButtonUp);
                base.TouchUp += new EventHandler<TouchEventArgs>(this.RangeBarTouchUp);
                this.InitializeTransparecyEffect();
                this.Invalidate();
            }
        }

        private void InitializeTransparecyEffect()
        {
        }

        public void Invalidate()
        {
            this.Render();
        }

        private bool IsResizeThumbArea(Point point) => 
            TransformHelper.GetElementBounds(this.leftResizeThumb, this).Contains(point) || TransformHelper.GetElementBounds(this.rightResizeThumb, this).Contains(point);

        private bool IsSliderArea(Point point) => 
            TransformHelper.GetElementBounds(this.slider, this).Contains(point);

        private void leftResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double x = Mouse.GetPosition(this).X;
            if (double.IsNaN(this.prevResizeThumbPosition))
            {
                this.prevResizeThumbPosition = x;
            }
            else
            {
                double elementCenter = TransformHelper.GetElementCenter(this.isViewPortStartResizing ? this.leftResizeThumb : this.rightResizeThumb, this);
                if ((elementCenter >= 0.0) && (elementCenter <= base.ActualWidth))
                {
                    double num3 = x - elementCenter;
                    double num4 = x - this.prevResizeThumbPosition;
                    this.prevResizeThumbPosition = x;
                    if (Math.Sign(num4) == Math.Sign(num3))
                    {
                        this.UpdateViewPort(this.NormalizeByLength(num3));
                    }
                }
            }
        }

        protected double NormalizeByLength(double value) => 
            ((base.ActualWidth == 0.0) || (this.ViewPortWidth <= 0.0)) ? 0.0 : (value / (base.ActualWidth - ((this.GetElementWidth(this.leftResizeThumb) + this.GetElementWidth(this.rightResizeThumb)) + 10.0)));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ContentControl control = LayoutHelper.FindElementByName(this, "PART_RangeBarContainer") as ContentControl;
            this.rootContainer = control.Content as Grid;
            base.MouseMove += new MouseEventHandler(this.RangeBarMouseMove);
            base.MouseLeave += new MouseEventHandler(this.RangeBarMouseLeave);
            this.InitializeElements();
        }

        private void OnLeftResizeThumbDragCompleted(object sender, DragCompletedEventArgs e)
        {
            this.ResetIsDragged();
            this.ResetCursor();
            this.Owner.OnRangeBarViewPortChanged();
        }

        private void OnLeftResizeThumbDragStarted(object sender, DragStartedEventArgs e)
        {
            if (this.Owner.AllowZoom)
            {
                this.SetIsDragged();
                ChangeCursorHelper.SetResizeCursor();
                this.isViewPortStartResizing = true;
                this.prevResizeThumbPosition = double.NaN;
            }
        }

        private void OnRightResizeThumbDragCompleted(object sender, DragCompletedEventArgs e)
        {
            this.ResetIsDragged();
            this.ResetCursor();
            this.Owner.OnRangeBarViewPortChanged();
        }

        private void OnRightResizeThumbDragStarted(object sender, DragStartedEventArgs e)
        {
            if (this.Owner.AllowZoom)
            {
                this.SetIsDragged();
                ChangeCursorHelper.SetResizeCursor();
                this.isViewPortStartResizing = false;
                this.prevResizeThumbPosition = double.NaN;
            }
        }

        private void OnSelectionBrushChanged()
        {
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Invalidate();
        }

        private void OnSliderDragCompleted(object sender, DragCompletedEventArgs e)
        {
            this.ResetIsDragged();
            this.ResetCursor();
            this.Owner.OnRangeBarViewPortChanged();
        }

        private void OnSliderDragStarted(object sender, DragStartedEventArgs e)
        {
            if (!this.Owner.IsWholeRangeVisible())
            {
                this.SetIsDragged();
                this.prevSliderPostion = double.NaN;
                ChangeCursorHelper.SetHandCursor();
            }
        }

        private void RangeBarMouseLeave(object sender, MouseEventArgs e)
        {
            this.ResetCursor();
        }

        private void RangeBarMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.ScrollByClick(e.GetPosition(this).X);
        }

        private void RangeBarMouseMove(object sender, MouseEventArgs e)
        {
            if (!this.IsDragged)
            {
                Point position = e.GetPosition(this);
                if (this.IsSliderArea(position) && !this.Owner.IsWholeRangeVisible())
                {
                    ChangeCursorHelper.SetHandCursor();
                }
                else if (this.IsResizeThumbArea(position) && this.Owner.AllowZoom)
                {
                    ChangeCursorHelper.SetResizeCursor();
                }
                else
                {
                    this.ResetCursor();
                }
            }
        }

        private void RangeBarTouchUp(object sender, TouchEventArgs e)
        {
            this.ScrollByClick(e.GetTouchPoint(this).Position.X);
        }

        private void Render()
        {
            if ((this.rootContainer == null) || ((this.ViewPortWidth <= 0.0) || (base.ActualWidth <= 0.0)))
            {
                this.RenderDefault();
            }
            else
            {
                this.RenderThumbs();
                this.RenderMiddleContainer();
                this.RenderTopContainer();
            }
        }

        private void RenderDefault()
        {
            if (this.topContainer != null)
            {
                this.RenderTopContainer(this.RenderWidth, 0.0, 0.0);
            }
        }

        private void RenderMiddleContainer()
        {
            if (this.CanRenderSelectionSide())
            {
                double num = this.Minimum * this.RenderWidth;
                double num2 = (1.0 - this.Maximum) * this.RenderWidth;
                this.RenderMiddleContainer(num, this.RenderWidth - (num + num2), num2);
            }
        }

        private void RenderMiddleContainer(double width1, double width2, double width3)
        {
            this.middleContainer.ColumnDefinitions[0].Width = new GridLength(this.ConstrainZero(width1));
            this.middleContainer.ColumnDefinitions[1].Width = new GridLength(this.ConstrainZero(width2));
            this.middleContainer.ColumnDefinitions[2].Width = new GridLength(this.ConstrainZero(width3));
            this.middleContainer.InvalidateArrange();
        }

        private void RenderThumbContainer(double width1, double width2, double width3)
        {
            double num = (((this.ViewPortWidth / 2.0) * this.RenderWidth) < 1.0) ? 0.0 : (this.ViewPortWidth / 2.0);
            this.thumbContainer.ColumnDefinitions[0].Width = new GridLength(this.ViewPortStart, GridUnitType.Star);
            this.thumbContainer.ColumnDefinitions[2].Width = new GridLength(num, GridUnitType.Star);
            this.thumbContainer.ColumnDefinitions[4].Width = new GridLength(num, GridUnitType.Star);
            this.thumbContainer.ColumnDefinitions[6].Width = new GridLength(1.0 - this.ViewPortEnd, GridUnitType.Star);
            this.thumbContainer.InvalidateArrange();
        }

        private void RenderThumbs()
        {
            double num = base.ActualWidth - ((this.GetElementWidth(this.leftResizeThumb) + this.GetElementWidth(this.rightResizeThumb)) + 10.0);
            this.leftThumbPosition = this.ViewPortStart * num;
            double num2 = (this.ViewPortWidth * num) + 10.0;
            this.rightThumbPosition = (this.leftThumbPosition + this.GetElementWidth(this.leftResizeThumb)) + num2;
            double leftThumbPosition = this.leftThumbPosition;
            double num4 = this.RenderWidth - (this.rightThumbPosition + this.GetElementWidth(this.rightResizeThumb));
            this.RenderThumbContainer(leftThumbPosition, this.RenderWidth - (((leftThumbPosition + num4) + this.GetElementWidth(this.leftResizeThumb)) + this.GetElementWidth(this.rightResizeThumb)), num4);
        }

        private void RenderTopContainer()
        {
            double num = this.leftThumbPosition + this.GetElementWidth(this.leftResizeThumb);
            double num2 = this.RenderWidth - this.rightThumbPosition;
            this.RenderTopContainer(num, this.RenderWidth - (num + num2), num2);
        }

        private void RenderTopContainer(double width1, double width2, double width3)
        {
            this.topContainer.ColumnDefinitions[0].Width = new GridLength(this.ConstrainZero(width1));
            this.topContainer.ColumnDefinitions[1].Width = new GridLength(this.ConstrainZero(width2));
            this.topContainer.ColumnDefinitions[2].Width = new GridLength(this.ConstrainZero(width3));
            this.topContainer.InvalidateArrange();
        }

        private void ResetCursor()
        {
            ChangeCursorHelper.ResetCursorToDefault();
        }

        private void ResetIsDragged()
        {
            this.IsDragged = false;
        }

        private void ScrollByClick(double position)
        {
            double num = this.ViewPortWidth * base.ActualWidth;
            position -= num / 2.0;
            position = Math.Min(Math.Max(position, 0.0), base.ActualWidth - num);
            double normalOffset = Math.Min(Math.Max((double) (position / base.ActualWidth), (double) 0.0), 1.0);
            this.Owner.ScrollRangeBar(normalOffset);
        }

        private void ScrollToPosition(double newPosition)
        {
            if (double.IsNaN(this.prevSliderPostion))
            {
                this.prevSliderPostion = newPosition;
            }
            else
            {
                double num = newPosition - this.prevSliderPostion;
                this.prevSliderPostion = newPosition;
                if (this.Owner != null)
                {
                    this.Owner.OnRangeBarSliderMoved(this.NormalizeByLength(num));
                }
            }
        }

        private void SetIsDragged()
        {
            this.IsDragged = true;
        }

        private void SliderDragDelta(object sender, DragDeltaEventArgs e)
        {
            this.ScrollToPosition(Mouse.GetPosition(this).X);
        }

        private void UpdateViewPort(double delta)
        {
            double viewPortStart = this.ViewPortStart;
            double viewPortEnd = this.ViewPortEnd;
            if (this.isViewPortStartResizing)
            {
                viewPortStart = Math.Min(this.GetCorrectNormalValue(this.ViewPortStart + delta), this.ViewPortEnd);
            }
            else
            {
                viewPortEnd = Math.Max(this.GetCorrectNormalValue(this.ViewPortEnd + delta), this.ViewPortStart);
            }
            if (this.Owner != null)
            {
                this.Owner.OnRangeBarViewPortResizing(viewPortStart, viewPortEnd, this.isViewPortStartResizing);
            }
        }

        public Brush SelectionBrush
        {
            get => 
                (Brush) base.GetValue(SelectionBrushProperty);
            set => 
                base.SetValue(SelectionBrushProperty, value);
        }

        public double ViewPortEnd { get; set; }

        public double ViewPortStart { get; set; }

        public double ViewPortWidth =>
            this.ViewPortEnd - this.ViewPortStart;

        public double Minimum { get; set; }

        public double Maximum { get; set; }

        public DevExpress.Xpf.Editors.RangeControl.RangeControl Owner { get; set; }

        internal TransparencyEffect ShaderEffect =>
            ((this.rootContainer != null) ? ((TransparencyEffect) this.rootContainer.Effect) : null) as TransparencyEffect;

        private double RenderWidth =>
            (this.rootContainer != null) ? this.rootContainer.ActualWidth : 0.0;

        private bool IsDragged { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RangeBar.<>c <>9 = new RangeBar.<>c();

            internal void <.cctor>b__2_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((RangeBar) d).OnSelectionBrushChanged();
            }
        }
    }
}

