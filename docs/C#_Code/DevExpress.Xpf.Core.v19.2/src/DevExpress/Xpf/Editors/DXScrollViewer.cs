namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Threading;

    public class DXScrollViewer : ScrollViewer
    {
        public static readonly DependencyProperty VerticalSnapPointsAlignmentProperty;
        public static readonly DependencyProperty HorizontalSnapPointsAlignmentProperty;
        public static readonly DependencyProperty IsLoopedProperty;
        public static readonly RoutedEvent ViewChangedEvent;
        private static readonly TimeSpan MouseWheelCompleteInterval = TimeSpan.FromMilliseconds(300.0);
        private const double ManipulationDeceleration = 0.008;
        private readonly DispatcherTimer timerToCompleteScrollByWheelManipulation = new DispatcherTimer();
        private bool manipulated;
        private DateTime manipulationBeginTime;
        private IList<Tuple<double, long>> manipulationPairs;

        public event ViewChangedEventHandler ViewChanged
        {
            add
            {
                base.AddHandler(ViewChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(ViewChangedEvent, value);
            }
        }

        static DXScrollViewer()
        {
            Type ownerType = typeof(DXScrollViewer);
            VerticalSnapPointsAlignmentProperty = DependencyPropertyManager.Register("VerticalSnapPointsAlignment", typeof(SnapPointsAlignment), ownerType, new PropertyMetadata(SnapPointsAlignment.Center));
            HorizontalSnapPointsAlignmentProperty = DependencyPropertyManager.Register("HorizontalSnapPointsAlignment", typeof(SnapPointsAlignment), ownerType, new PropertyMetadata(SnapPointsAlignment.Center));
            IsLoopedProperty = DependencyPropertyManager.Register("IsLooped", typeof(bool), ownerType, new PropertyMetadata(false));
            ViewChangedEvent = EventManager.RegisterRoutedEvent("ViewChanged", RoutingStrategy.Bubble, typeof(ViewChangedEventHandler), ownerType);
        }

        public DXScrollViewer()
        {
            this.timerToCompleteScrollByWheelManipulation.Tick += new EventHandler(this.CompleteScrollByWheelManipulation);
            this.manipulationPairs = new List<Tuple<double, long>>();
        }

        private void CalcManipulationInertia()
        {
            double num = 0.0;
            foreach (Tuple<double, long> tuple in this.manipulationPairs)
            {
                double num4 = tuple.Item1 / ((double) tuple.Item2);
                num = !num.AreClose(0.0) ? ((num4 + num) / 2.0) : num4;
            }
            double num2 = num / 0.008;
            double offset = this.IsLooped ? (base.VerticalOffset - num2) : this.CoerceVerticalOffset(base.VerticalOffset - num2);
            this.ScrollToClosestSnapPoint(offset);
        }

        private double CoerceVerticalOffset(double offset) => 
            (this.IsLooped || !offset.GreaterThanOrClose(base.ScrollableHeight)) ? ((this.IsLooped || !offset.LessThanOrClose(0.0)) ? offset : 0.0) : base.ScrollableHeight;

        private void CompleteScrollByWheelManipulation(object sender, EventArgs e)
        {
            double closestSnapPoint = this.GetClosestSnapPoint(Orientation.Vertical, base.VerticalOffset);
            if (closestSnapPoint.AreClose(base.VerticalOffset))
            {
                this.RaiseViewChangedEvent(false);
            }
            this.AnimateScrollToVerticalOffset(closestSnapPoint, null, null, () => this.IsIntermediate = false, null, ScrollDataAnimationEase.BeginAnimation);
            this.timerToCompleteScrollByWheelManipulation.Stop();
        }

        internal double EnsureVerticalOffset(double offset) => 
            this.IsLooped ? ((!offset.GreaterThanOrClose(0.0) || !offset.LessThan(base.ExtentHeight)) ? (offset.GreaterThan(0.0) ? (offset % base.ExtentHeight) : (base.ExtentHeight - (Math.Abs(offset) % base.ExtentHeight))) : offset) : offset;

        private double GetClosestSnapPoint(Orientation orientation, double currentOffset)
        {
            IScrollSnapPointsInfo scrollInfo = base.ScrollInfo as IScrollSnapPointsInfo;
            if (scrollInfo != null)
            {
                switch (((orientation == Orientation.Vertical) ? this.VerticalSnapPointsAlignment : this.HorizontalSnapPointsAlignment))
                {
                    case SnapPointsAlignment.Center:
                        currentOffset += (orientation == Orientation.Vertical) ? (base.ViewportHeight / 2.0) : (base.ViewportWidth / 2.0);
                        break;

                    case SnapPointsAlignment.Far:
                        currentOffset += (orientation == Orientation.Vertical) ? base.ViewportHeight : base.ViewportWidth;
                        break;

                    default:
                        break;
                }
                if (!((orientation == Orientation.Vertical) ? !scrollInfo.AreVerticalSnapPointsRegular : !scrollInfo.AreHorizontalSnapPointsRegular))
                {
                    float num3;
                    float num4 = scrollInfo.GetRegularSnapPoints(orientation, (orientation == Orientation.Vertical) ? this.VerticalSnapPointsAlignment : this.HorizontalSnapPointsAlignment, out num3);
                    double num5 = ((double) num4) / 2.0;
                    double num6 = (orientation == Orientation.Vertical) ? base.ExtentHeight : base.ExtentWidth;
                    if (this.IsLooped)
                    {
                        num3 -= (float) num6;
                        num6 *= 2.0;
                    }
                    for (double i = num3; i.LessThanOrClose(num6); i += num4)
                    {
                        double num8 = Math.Abs((double) (currentOffset - i));
                        if (num8.LessThanOrClose(num5))
                        {
                            switch (((orientation == Orientation.Vertical) ? this.VerticalSnapPointsAlignment : this.HorizontalSnapPointsAlignment))
                            {
                                case SnapPointsAlignment.Near:
                                    return i;

                                case SnapPointsAlignment.Center:
                                    return (i - ((orientation == Orientation.Vertical) ? (base.ViewportHeight / 2.0) : (base.ViewportWidth / 2.0)));

                                case SnapPointsAlignment.Far:
                                    return (i - ((orientation == Orientation.Vertical) ? base.ViewportHeight : base.ViewportWidth));

                                default:
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    IEnumerable<float> irregularSnapPoints = scrollInfo.GetIrregularSnapPoints(orientation, (orientation == Orientation.Vertical) ? this.VerticalSnapPointsAlignment : this.HorizontalSnapPointsAlignment);
                    if (!irregularSnapPoints.Any<float>())
                    {
                        return currentOffset;
                    }
                    Dictionary<float, float> source = new Dictionary<float, float>();
                    foreach (float num2 in irregularSnapPoints)
                    {
                        source.Add(num2, Math.Abs((float) (((float) currentOffset) - num2)));
                    }
                    Func<KeyValuePair<float, float>, float> keySelector = <>c.<>9__44_0;
                    if (<>c.<>9__44_0 == null)
                    {
                        Func<KeyValuePair<float, float>, float> local1 = <>c.<>9__44_0;
                        keySelector = <>c.<>9__44_0 = x => x.Value;
                    }
                    double key = (double) source.OrderBy<KeyValuePair<float, float>, float>(keySelector).First<KeyValuePair<float, float>>().Key;
                    switch (((orientation == Orientation.Vertical) ? this.VerticalSnapPointsAlignment : this.HorizontalSnapPointsAlignment))
                    {
                        case SnapPointsAlignment.Near:
                            return key;

                        case SnapPointsAlignment.Center:
                            return (key - ((orientation == Orientation.Vertical) ? (base.ViewportHeight / 2.0) : (base.ViewportWidth / 2.0)));

                        case SnapPointsAlignment.Far:
                            return (key - ((orientation == Orientation.Vertical) ? base.ViewportHeight : base.ViewportWidth));

                        default:
                            break;
                    }
                }
            }
            return currentOffset;
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            this.StopCurrentAnimatedScroll();
            this.PreviousPosition = new Point?(e.GetPosition(this));
            this.manipulated = false;
            this.manipulationBeginTime = DateTime.Now;
            this.manipulationPairs = new List<Tuple<double, long>>();
            base.CaptureMouse();
            base.OnPreviewMouseDown(e);
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            if (this.PreviousPosition != null)
            {
                Point position = e.GetPosition(this);
                double num = position.Y - this.PreviousPosition.Value.Y;
                this.PreviousPosition = new Point?(position);
                if (Math.Abs(num).GreaterThanOrClose(1.0))
                {
                    this.IsIntermediate = true;
                    long milliseconds = (DateTime.Now - this.manipulationBeginTime).Milliseconds;
                    if (milliseconds > 0L)
                    {
                        this.manipulationPairs.Add(new Tuple<double, long>(num, milliseconds));
                    }
                    base.ScrollToVerticalOffset(this.EnsureVerticalOffset(base.VerticalOffset - num));
                    this.manipulated = true;
                    this.manipulationBeginTime = DateTime.Now;
                }
            }
            base.OnPreviewMouseMove(e);
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            this.PreviousPosition = null;
            this.CalcManipulationInertia();
            base.ReleaseMouseCapture();
            if (this.manipulated)
            {
                e.Handled = true;
            }
            base.OnPreviewMouseUp(e);
        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                this.StopCurrentAnimatedScroll();
                if (base.ScrollInfo != null)
                {
                    if (e.Delta > 0)
                    {
                        base.ScrollInfo.MouseWheelDown();
                    }
                    else
                    {
                        base.ScrollInfo.MouseWheelUp();
                    }
                }
                e.Handled = true;
                this.timerToCompleteScrollByWheelManipulation.Stop();
                this.timerToCompleteScrollByWheelManipulation.Interval = MouseWheelCompleteInterval;
                this.timerToCompleteScrollByWheelManipulation.Start();
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            this.IsIntermediate = false;
        }

        protected override void OnScrollChanged(ScrollChangedEventArgs e)
        {
            base.OnScrollChanged(e);
            this.RaiseViewChangedEvent(this.IsIntermediate);
            if (this.IsIntermediate)
            {
                this.IsIntermediate = false;
            }
        }

        private void RaiseViewChangedEvent(bool isIntermediate)
        {
            ViewChangedEventArgs e = new ViewChangedEventArgs(isIntermediate);
            e.RoutedEvent = ViewChangedEvent;
            e.Source = this;
            base.RaiseEvent(e);
        }

        private void ScrollToClosestSnapPoint(double offset)
        {
            double closestSnapPoint = this.GetClosestSnapPoint(Orientation.Vertical, offset);
            if (closestSnapPoint.AreClose(base.VerticalOffset))
            {
                this.RaiseViewChangedEvent(false);
            }
            this.AnimateScrollToVerticalOffset(closestSnapPoint, null, null, () => this.IsIntermediate = false, new Func<double, double>(this.EnsureVerticalOffset), ScrollDataAnimationEase.BeginAnimation);
        }

        public SnapPointsAlignment VerticalSnapPointsAlignment
        {
            get => 
                (SnapPointsAlignment) base.GetValue(VerticalSnapPointsAlignmentProperty);
            set => 
                base.SetValue(VerticalSnapPointsAlignmentProperty, value);
        }

        public SnapPointsAlignment HorizontalSnapPointsAlignment
        {
            get => 
                (SnapPointsAlignment) base.GetValue(HorizontalSnapPointsAlignmentProperty);
            set => 
                base.SetValue(HorizontalSnapPointsAlignmentProperty, value);
        }

        public bool IsLooped
        {
            get => 
                (bool) base.GetValue(IsLoopedProperty);
            set => 
                base.SetValue(IsLoopedProperty, value);
        }

        public bool IsIntermediate { get; set; }

        private Point? PreviousPosition { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXScrollViewer.<>c <>9 = new DXScrollViewer.<>c();
            public static Func<KeyValuePair<float, float>, float> <>9__44_0;

            internal float <GetClosestSnapPoint>b__44_0(KeyValuePair<float, float> x) => 
                x.Value;
        }
    }
}

