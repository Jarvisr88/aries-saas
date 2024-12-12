namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    public class ScrollControlController : PanelControllerBase
    {
        public ScrollControlController(DevExpress.Xpf.Core.IScrollControl control) : base(control)
        {
            this.AccumulateOffsetValuesDuringAnimatedScrolling = true;
            this.AnimateScrolling = true;
            this.DragScrolling = true;
            base.ScrollBars = ScrollBars.Auto;
        }

        protected internal virtual Timeline CreateAnimatedScrollingAnimation(Orientation orientation, double oldOffset, double newOffset, int duration, double finalOffset = (double) 1.0 / (double) 0.0, int finalOffsetDuration = 0, IEasingFunction easingFunction = null)
        {
            DoubleAnimationUsingKeyFrames element = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTarget(element, base.Control);
            Storyboard.SetTargetProperty(element, new PropertyPath(this.GetOffsetPropertyName(orientation), new object[0]));
            LinearDoubleKeyFrame keyFrame = new LinearDoubleKeyFrame();
            keyFrame.KeyTime = TimeSpan.Zero;
            keyFrame.Value = oldOffset;
            element.KeyFrames.Add(keyFrame);
            EasingDoubleKeyFrame frame2 = new EasingDoubleKeyFrame();
            frame2.KeyTime = TimeSpan.FromMilliseconds((double) duration);
            frame2.Value = newOffset;
            frame2.EasingFunction = easingFunction;
            element.KeyFrames.Add(frame2);
            if (!double.IsNaN(finalOffset))
            {
                EasingDoubleKeyFrame frame3 = new EasingDoubleKeyFrame();
                frame3.KeyTime = TimeSpan.FromMilliseconds((double) (duration + finalOffsetDuration));
                frame3.Value = finalOffset;
                frame3.EasingFunction = easingFunction;
                element.KeyFrames.Add(frame3);
            }
            element.Duration = TimeSpan.FromMilliseconds((double) (duration + finalOffsetDuration));
            return element;
        }

        protected double GetOffset(Orientation orientation) => 
            (orientation == Orientation.Horizontal) ? this.IScrollControl.HorizontalOffset : this.IScrollControl.VerticalOffset;

        protected DependencyProperty GetOffsetProperty(Orientation orientation) => 
            (orientation == Orientation.Horizontal) ? ScrollControl.HorizontalOffsetProperty : ScrollControl.VerticalOffsetProperty;

        protected string GetOffsetPropertyName(Orientation orientation) => 
            (orientation == Orientation.Horizontal) ? "HorizontalOffset" : "VerticalOffset";

        protected override void InitScrollParams(ScrollParams horzScrollParams, ScrollParams vertScrollParams)
        {
            horzScrollParams.Max = this.ScrollAreaSize.Width;
            horzScrollParams.PageSize = base.ContentBounds.Width;
            horzScrollParams.Position = this.IScrollControl.HorizontalOffset;
            vertScrollParams.Max = this.ScrollAreaSize.Height;
            vertScrollParams.PageSize = base.ContentBounds.Height;
            vertScrollParams.Position = this.IScrollControl.VerticalOffset;
        }

        public override bool IsScrollable() => 
            true;

        protected override bool Scroll(Orientation orientation, double position)
        {
            double offset = this.GetOffset(orientation);
            bool flag = base.CanAnimateScrolling && this.AnimateScrolling;
            if (flag && (this.AnimatedScrollingStoryboard != null))
            {
                this.AnimatedScrollingStoryboard.Stop();
                base.Control.BeginAnimation(this.GetOffsetProperty(orientation), null);
                this.AnimatedScrollingStoryboard = null;
                if (this.AccumulateOffsetValuesDuringAnimatedScrolling)
                {
                    double num3 = this.GetOffset(orientation);
                    if (Math.Sign((double) (position - offset)) == Math.Sign((double) (num3 - offset)))
                    {
                        position += num3 - offset;
                    }
                }
            }
            ScrollParams scrollParams = base.GetScrollParams(orientation);
            if ((!this.IsDragScrolling && !DragScrollingController.IsInertialScrolling(base.Control)) || !scrollParams.Enabled)
            {
                position = Math.Max(scrollParams.Min, Math.Min(position, scrollParams.MaxPosition));
                if (this.GetOffset(orientation) != position)
                {
                    this.SetOffset(orientation, position);
                }
                double newOffset = this.GetOffset(orientation);
                if (!flag)
                {
                    return !(newOffset == offset);
                }
                this.AnimatedScrollingStoryboard = new Storyboard();
                this.AnimatedScrollingStoryboard.Children.Add(this.CreateAnimatedScrollingAnimation(orientation, offset, newOffset, ScrollControl.AnimatedScrollingDuration, double.NaN, 0, new QuadraticEase()));
                this.AnimatedScrollingStoryboard.FillBehavior = FillBehavior.Stop;
                this.AnimatedScrollingStoryboard.Completed += delegate (object o, EventArgs e) {
                    this.AnimatedScrollingStoryboard = null;
                };
                this.AnimatedScrollingStoryboard.Begin();
            }
            return false;
        }

        public void Scroll(Orientation orientation, double position, bool animate = false, bool accumulateOffsetValuesDuringAnimation = true)
        {
            if (this.IsScrollable())
            {
                this.AccumulateOffsetValuesDuringAnimatedScrolling = accumulateOffsetValuesDuringAnimation;
                base.CanAnimateScrolling = animate;
                try
                {
                    base.GetScrollParams(orientation).Scroll(position, false);
                }
                finally
                {
                    base.CanAnimateScrolling = false;
                    this.AccumulateOffsetValuesDuringAnimatedScrolling = true;
                }
            }
        }

        protected void SetOffset(Orientation orientation, double value)
        {
            if (orientation == Orientation.Horizontal)
            {
                this.IScrollControl.HorizontalOffset = value;
            }
            else
            {
                this.IScrollControl.VerticalOffset = value;
            }
        }

        protected override bool WantsDragAndDrop(Point p, out DragAndDropController controller)
        {
            if ((!base.CanScroll() || !this.DragScrolling) || !this.ScrollableAreaBounds.Contains(p))
            {
                return base.WantsDragAndDrop(p, out controller);
            }
            controller = new DragScrollingController(this, p);
            return true;
        }

        public DevExpress.Xpf.Core.IScrollControl IScrollControl =>
            base.IControl as DevExpress.Xpf.Core.IScrollControl;

        public bool AnimateScrolling { get; protected internal set; }

        public bool DragScrolling { get; protected internal set; }

        public Size ScrollAreaSize =>
            base.IPanel.ChildrenBounds.Size();

        protected bool AccumulateOffsetValuesDuringAnimatedScrolling { get; set; }

        protected Storyboard AnimatedScrollingStoryboard { get; private set; }

        protected bool IsDragScrolling =>
            base.IsDragAndDrop && (base.DragAndDropController is DragScrollingController);
    }
}

