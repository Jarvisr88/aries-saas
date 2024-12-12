namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    public class DragScrollingController : DragAndDropController
    {
        private static Dictionary<FrameworkElement, Storyboard> InertialScrollingAnimations = new Dictionary<FrameworkElement, Storyboard>();

        public DragScrollingController(DevExpress.Xpf.Core.Controller controller, Point startDragPoint) : base(controller, startDragPoint)
        {
            this.OriginalOffset = this.Controller.IScrollControl.Offset;
            this.StopInertialScrolling();
        }

        protected void ChangeOffset(Point by)
        {
            this.CorrectOffsetChange(this.OriginalOffset, ref by, new Point(this.GetMaxOffsetExcess(by.X), this.GetMaxOffsetExcess(by.Y)));
            this.OffsetChanges ??= new Point[] { this.OffsetChanges[1], this.OffsetChanges[2], by };
            this.Controller.IScrollControl.SetOffset(PointHelper.Add(this.OriginalOffset, by));
        }

        protected void CorrectOffsetChange(Point offset, ref Point offsetChange, Point maxOffsetExcess)
        {
            if (!this.Controller.HorzScrollParams.Enabled)
            {
                maxOffsetExcess.X = 0.0;
            }
            if (!this.Controller.VertScrollParams.Enabled)
            {
                maxOffsetExcess.Y = 0.0;
            }
            Point point = new Point(this.Controller.HorzScrollParams.Min, this.Controller.VertScrollParams.Min);
            Point point2 = new Point(this.Controller.HorzScrollParams.MaxPosition, this.Controller.VertScrollParams.MaxPosition);
            Point point3 = PointHelper.Add(offset, offsetChange);
            point3 = PointHelper.Min(PointHelper.Max(PointHelper.Subtract(point, maxOffsetExcess), point3), PointHelper.Add(point2, maxOffsetExcess));
            offsetChange = PointHelper.Subtract(point3, offset);
        }

        protected virtual void CreateInertialScrollingAnimation(Storyboard storyboard, Orientation orientation, double offset, double newOffset, int duration)
        {
            if (this.IsOffsetExcessive(orientation, offset))
            {
                QuadraticEase easingFunction = new QuadraticEase();
                easingFunction.EasingMode = EasingMode.EaseOut;
                storyboard.Children.Add(this.Controller.CreateAnimatedScrollingAnimation(orientation, offset, this.GetOffsetExcessBoundary(orientation, offset), 300, double.NaN, 0, easingFunction));
            }
            else
            {
                double naN = double.NaN;
                int finalOffsetDuration = 0;
                if (this.IsOffsetExcessive(orientation, newOffset))
                {
                    naN = this.GetOffsetExcessBoundary(orientation, newOffset);
                    finalOffsetDuration = 150;
                }
                QuadraticEase easingFunction = new QuadraticEase();
                easingFunction.EasingMode = EasingMode.EaseOut;
                storyboard.Children.Add(this.Controller.CreateAnimatedScrollingAnimation(orientation, offset, newOffset, duration, naN, finalOffsetDuration, easingFunction));
            }
        }

        public override void DragAndDrop(Point p)
        {
            base.DragAndDrop(p);
            this.ChangeOffset(PointHelper.Subtract(base.StartDragPoint, p));
        }

        public override void EndDragAndDrop(bool accept)
        {
            base.EndDragAndDrop(accept);
            if (this.Controller.AnimateScrolling)
            {
                this.StartInertialScrolling();
            }
            else
            {
                this.Controller.UpdateScrolling();
            }
        }

        protected virtual void GetInertialScrollingParams(out Point offsetChange, out int duration)
        {
            if (this.OffsetChanges == null)
            {
                offsetChange = new Point();
                duration = 0;
            }
            else
            {
                offsetChange = PointHelper.Add(PointHelper.Multiply(PointHelper.Subtract(this.OffsetChanges[1], this.OffsetChanges[0]), (double) 0.33333333333333331), PointHelper.Multiply(PointHelper.Subtract(this.OffsetChanges[2], this.OffsetChanges[1]), (double) 0.66666666666666663));
                if ((Math.Abs(offsetChange.X) < 5.0) && (Math.Abs(offsetChange.Y) < 5.0))
                {
                    offsetChange = new Point();
                    duration = 0;
                }
                else
                {
                    Rect contentBounds = this.Controller.IScrollControl.ContentBounds;
                    offsetChange = PointHelper.Multiply(offsetChange, new Point(0.015 * contentBounds.Width, 0.015 * contentBounds.Height));
                    offsetChange = PointHelper.Multiply(PointHelper.Sign(offsetChange), PointHelper.Max(new Point(150.0, 150.0), PointHelper.Abs(offsetChange)));
                    this.CorrectOffsetChange(this.Controller.IScrollControl.Offset, ref offsetChange, new Point(150.0, 150.0));
                    double num = 1.0 + (Math.Pow(Math.Max(Math.Abs(offsetChange.X), Math.Abs(offsetChange.Y)) - 150.0, 2.0) / 2000000.0);
                    duration = Math.Max(300, (int) Math.Max((double) (Math.Abs(offsetChange.X) / num), (double) (Math.Abs(offsetChange.Y) / num)));
                }
            }
        }

        protected double GetMaxOffset(Orientation orientation) => 
            (orientation == Orientation.Horizontal) ? this.Controller.HorzScrollParams.MaxPosition : this.Controller.VertScrollParams.MaxPosition;

        protected virtual double GetMaxOffsetExcess(double offsetChange) => 
            600.0 * Math.Log10(1.0 + (Math.Abs(offsetChange) / 200.0));

        protected double GetMinOffset(Orientation orientation) => 
            (orientation == Orientation.Horizontal) ? this.Controller.HorzScrollParams.Min : this.Controller.VertScrollParams.Min;

        protected double GetOffsetExcessBoundary(Orientation orientation, double offset) => 
            (offset < this.GetMinOffset(orientation)) ? this.GetMinOffset(orientation) : this.GetMaxOffset(orientation);

        public static bool IsInertialScrolling(FrameworkElement element) => 
            InertialScrollingAnimations.ContainsKey(element);

        protected bool IsOffsetExcessive(Orientation orientation, double offset) => 
            (offset < this.GetMinOffset(orientation)) || (offset > this.GetMaxOffset(orientation));

        protected void StartInertialScrolling()
        {
            Point point;
            int num;
            this.GetInertialScrollingParams(out point, out num);
            Point offset = this.Controller.IScrollControl.Offset;
            Point point3 = PointHelper.Add(offset, point);
            Storyboard storyboard = new Storyboard();
            if (this.Controller.HorzScrollParams.Enabled)
            {
                this.CreateInertialScrollingAnimation(storyboard, Orientation.Horizontal, offset.X, point3.X, num);
            }
            if (this.Controller.VertScrollParams.Enabled)
            {
                this.CreateInertialScrollingAnimation(storyboard, Orientation.Vertical, offset.Y, point3.Y, num);
            }
            storyboard.Completed += (o, e) => this.StopInertialScrolling();
            InertialScrollingAnimations[this.Controller.Control] = storyboard;
            storyboard.Begin();
        }

        protected void StopInertialScrolling()
        {
            if (IsInertialScrolling(this.Controller.Control))
            {
                Point offset = this.Controller.IScrollControl.Offset;
                InertialScrollingAnimations[this.Controller.Control].Stop();
                InertialScrollingAnimations.Remove(this.Controller.Control);
                this.Controller.IScrollControl.SetOffset(offset);
            }
        }

        protected ScrollControlController Controller =>
            (ScrollControlController) base.Controller;

        protected Point[] OffsetChanges { get; private set; }

        protected Point OriginalOffset { get; private set; }
    }
}

