namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Threading;

    public class Controller
    {
        private bool _IsMouseEntered;
        private bool _IsMouseLeftButtonDown;
        private const int DoubleClickTime = 500;
        private const int DoubleClickArea = 4;
        private Point _ClickPosition;
        private DateTime _ClickTime;
        public const int PixelsPerLine = 120;
        private DevExpress.Xpf.Core.ScrollBars _ScrollBars;
        private bool _AllowAutoScrolling;
        private ScrollDirection _AutoScrollingDirection;
        private bool _IsAutoScrolling;
        protected int AutoScrollingTimeInterval = 100;
        protected double AutoScrollingAreaWidth = 30.0;
        public const double StartDragAreaSize = 8.0;
        private DevExpress.Xpf.Core.DragAndDropController _DragAndDropController;
        private Cursor _OriginalCursor;
        private UIElement _OriginalFocusedElement;
        private WeakReference rootVisualReference;

        public event EventHandler EndDrag;

        public event EventHandler ScrollParamsChanged;

        public event EventHandler StartDrag;

        public Controller(DevExpress.Xpf.Core.IControl control)
        {
            this.IControl = control;
            this.CheckScrollParams();
            this.CheckScrollBars();
            this.AttachToEvents();
        }

        protected virtual void AttachToEvents()
        {
            this.Control.KeyDown += (sender, e) => this.ProcessKeyDown(new DXKeyEventArgs(e, args => e.Handled = args.Handled));
            this.Control.KeyUp += (sender, e) => this.ProcessKeyUp(new DXKeyEventArgs(e, args => e.Handled = args.Handled));
            this.Control.MouseEnter += (sender, e) => this.OnMouseEnter(new DXMouseEventArgs(e));
            this.Control.MouseLeave += (sender, e) => this.OnMouseLeave(new DXMouseEventArgs(e));
            this.Control.MouseLeftButtonDown += delegate (object sender, MouseButtonEventArgs e) {
                if (this.Control.IsInVisualTree())
                {
                    this.OnMouseLeftButtonDown(new DXMouseButtonEventArgs(e, args => e.Handled = args.Handled));
                }
            };
            this.Control.MouseLeftButtonUp += (sender, e) => this.OnMouseLeftButtonUp(new DXMouseButtonEventArgs(e, args => e.Handled = args.Handled));
            this.Control.AddHandler(UIElement.MouseLeftButtonUpEvent, delegate (object sender, MouseButtonEventArgs e) {
                if (e.Handled && this.IsMouseLeftButtonDown)
                {
                    this.OnMouseLeftButtonUp(new DXMouseButtonEventArgs(e, args => e.Handled = args.Handled));
                }
            }, true);
            this.Control.MouseMove += delegate (object sender, MouseEventArgs e) {
                if (!this.IsMouseCaptureChanging && this.Control.IsInVisualTree())
                {
                    this.OnMouseMove(new DXMouseEventArgs(e));
                }
            };
            this.Control.MouseWheel += (sender, e) => this.OnMouseWheel(new DXMouseWheelEventArgs(e, args => e.Handled = args.Handled));
            this.Control.TouchDown += (sender, e) => this.OnTouchDown(e);
            this.Control.TouchUp += (sender, e) => this.OnTouchUp(e);
            this.Control.TouchEnter += (sender, e) => this.OnTouchEnter(e);
            this.Control.TouchLeave += (sender, e) => this.OnTouchLeave(e);
            this.Control.TouchMove += (sender, e) => this.OnTouchMove(e);
            this.Control.LayoutUpdated += (sender, e) => this.OnLayoutUpdated();
            this.Control.Loaded += (sender, e) => this.OnLoaded();
        }

        private void AttachToKeyboardEventsForDragAndDrop()
        {
            FrameworkElement rootVisual = this.Control.GetRootVisual();
            if (rootVisual != null)
            {
                this.rootVisualReference = new WeakReference(rootVisual);
                rootVisual.KeyDown += new KeyEventHandler(this.DragAndDropKeyDown);
                rootVisual.KeyUp += new KeyEventHandler(this.DragAndDropKeyUp);
            }
            this._OriginalFocusedElement = FocusManager.GetFocusedElement(FocusManager.GetFocusScope(this.Control)) as UIElement;
            if (this._OriginalFocusedElement != null)
            {
                this._OriginalFocusedElement.KeyDown += new KeyEventHandler(this.DragAndDropKeyDown);
                this._OriginalFocusedElement.KeyUp += new KeyEventHandler(this.DragAndDropKeyUp);
            }
        }

        private void AutoScrollingTimerTick(object sender, EventArgs e)
        {
            this.GetScrollParams(this.AutoScrollingDirection).DoSmallStep((this.AutoScrollingDirection == ScrollDirection.Right) || (this.AutoScrollingDirection == ScrollDirection.Bottom));
        }

        protected void CancelDragAndDrop()
        {
            if (this.IsDragAndDrop)
            {
                this.EndDragAndDrop(false);
            }
        }

        public bool CanScroll() => 
            this.IsScrollable() && (this.HorzScrollParams.Enabled || this.VertScrollParams.Enabled);

        public bool CaptureMouse(Point? mousePosition = new Point?())
        {
            bool flag;
            if (this.IsMouseCaptured)
            {
                return true;
            }
            UIElement mouseCaptureOwner = MouseCaptureOwner;
            MouseCaptureOwner = this.Control;
            this.IsMouseCaptureChanging = true;
            try
            {
                flag = this.Control.CaptureMouse() || this.Control.IsInDesignTool();
            }
            finally
            {
                this.IsMouseCaptureChanging = false;
            }
            if (flag)
            {
                MouseCaptureOwner.LostMouseCapture += new MouseEventHandler(this.OnLostMouseCapture);
            }
            else
            {
                MouseCaptureOwner = mouseCaptureOwner;
            }
            return flag;
        }

        protected virtual void CheckAutoScrolling(Point p)
        {
            if (this.IsScrollable() && this.AllowAutoScrolling)
            {
                for (ScrollDirection direction = ScrollDirection.Left; direction <= ScrollDirection.Bottom; direction += 1)
                {
                    if (this.GetScrollParams(direction).Enabled && this.GetAutoScrollingAreaBounds(direction).Contains(p))
                    {
                        this.AutoScrollingDirection = direction;
                        return;
                    }
                }
            }
            this.AutoScrollingDirection = ScrollDirection.None;
        }

        private void CheckMouseDoubleClick(DXMouseButtonEventArgs e)
        {
            Point position = e.GetPosition(this.Control);
            DateTime utcNow = DateTime.UtcNow;
            if ((utcNow - this._ClickTime).TotalMilliseconds <= 500.0)
            {
                Rect rect = new Rect(this._ClickPosition, new Size(4.0, 4.0));
                RectHelper.Offset(ref rect, -rect.Width / 2.0, -rect.Height / 2.0);
                if (rect.Contains(position))
                {
                    this.OnMouseDoubleClick(e);
                }
            }
            this._ClickPosition = position;
            this._ClickTime = utcNow;
        }

        protected virtual void CheckScrollBars()
        {
        }

        protected void CheckScrollParams()
        {
            if (this.IsScrollable())
            {
                this.CreateScrollParams();
            }
            else
            {
                this.DestroyScrollParams();
            }
        }

        private void CreateScrollParams()
        {
            if (this.HorzScrollParams == null)
            {
                this.HorzScrollParams = new ScrollParams();
                this.HorzScrollParams.Change += new Action<ScrollParams>(this.ScrollParamsChange);
                this.HorzScrollParams.Scrolling += new ScrollingEventHandler(this.ScrollParamsScrolling);
                this.VertScrollParams = new ScrollParams();
                this.VertScrollParams.Change += new Action<ScrollParams>(this.ScrollParamsChange);
                this.VertScrollParams.Scrolling += new ScrollingEventHandler(this.ScrollParamsScrolling);
            }
        }

        private void DestroyScrollParams()
        {
            if (this.HorzScrollParams != null)
            {
                this.HorzScrollParams.Change -= new Action<ScrollParams>(this.ScrollParamsChange);
                this.HorzScrollParams.Scrolling -= new ScrollingEventHandler(this.ScrollParamsScrolling);
                this.VertScrollParams.Change -= new Action<ScrollParams>(this.ScrollParamsChange);
                this.VertScrollParams.Scrolling -= new ScrollingEventHandler(this.ScrollParamsScrolling);
                this.HorzScrollParams = null;
                this.VertScrollParams = null;
            }
        }

        private void DetachFromKeyboardEventsForDragAndDrop()
        {
            if (this._OriginalFocusedElement != null)
            {
                this._OriginalFocusedElement.KeyDown -= new KeyEventHandler(this.DragAndDropKeyDown);
                this._OriginalFocusedElement.KeyUp -= new KeyEventHandler(this.DragAndDropKeyUp);
            }
            FrameworkElement element = (this.rootVisualReference != null) ? ((FrameworkElement) this.rootVisualReference.Target) : null;
            if (element != null)
            {
                element.KeyDown -= new KeyEventHandler(this.DragAndDropKeyDown);
                element.KeyUp -= new KeyEventHandler(this.DragAndDropKeyUp);
            }
        }

        protected virtual void DragAndDrop(Point p)
        {
            if (this.DragAndDropController != null)
            {
                this.DragAndDropController.DragAndDrop(p);
            }
        }

        private void DragAndDropKeyDown(object sender, KeyEventArgs e)
        {
            this.ProcessKeyDown(new DXKeyEventArgs(e, args => e.Handled = args.Handled));
        }

        private void DragAndDropKeyUp(object sender, KeyEventArgs e)
        {
            this.ProcessKeyUp(new DXKeyEventArgs(e, args => e.Handled = args.Handled));
        }

        protected void DragAndDropPrepared(bool isSuccess, Point? p)
        {
            this.IsDragAndDropPreparation = false;
            if (isSuccess)
            {
                this.StartDragAndDrop(p.Value);
            }
            else
            {
                this._DragAndDropController = null;
            }
        }

        protected virtual void EndDragAndDrop(bool accept)
        {
            this.ReleaseMouseCapture();
            this.Control.Cursor = this._OriginalCursor;
            this.IsDragAndDrop = false;
            this.DetachFromKeyboardEventsForDragAndDrop();
            if (this.DragAndDropController != null)
            {
                DevExpress.Xpf.Core.DragAndDropController dragAndDropController = this.DragAndDropController;
                this._DragAndDropController = null;
                dragAndDropController.EndDragAndDrop(accept);
            }
            if (this.EndDrag != null)
            {
                this.EndDrag(this.Control, EventArgs.Empty);
            }
        }

        protected virtual Rect GetAutoScrollingAreaBounds(ScrollDirection direction)
        {
            Rect scrollableAreaBounds = this.ScrollableAreaBounds;
            switch (direction)
            {
                case ScrollDirection.Left:
                    scrollableAreaBounds.Width = this.AutoScrollingAreaWidth;
                    break;

                case ScrollDirection.Top:
                    scrollableAreaBounds.Height = this.AutoScrollingAreaWidth;
                    break;

                case ScrollDirection.Right:
                    RectHelper.SetLeft(ref scrollableAreaBounds, scrollableAreaBounds.Right - this.AutoScrollingAreaWidth);
                    break;

                case ScrollDirection.Bottom:
                    RectHelper.SetTop(ref scrollableAreaBounds, scrollableAreaBounds.Bottom - this.AutoScrollingAreaWidth);
                    break;

                default:
                    scrollableAreaBounds = Rect.Empty;
                    break;
            }
            return scrollableAreaBounds;
        }

        [IteratorStateMachine(typeof(<GetInternalElements>d__1))]
        public virtual IEnumerable<UIElement> GetInternalElements()
        {
            if (this.HorzScrollBar != null)
            {
                yield return this.HorzScrollBar;
            }
            if (this.VertScrollBar != null)
            {
                yield return this.VertScrollBar;
            }
            if (this.CornerBox == null)
            {
                IEnumerator<UIElement> enumerator;
                if (this.DragAndDropController != null)
                {
                    enumerator = this.DragAndDropController.GetInternalElements().GetEnumerator();
                }
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        UIElement current = enumerator.Current;
                        yield return current;
                    }
                    else
                    {
                        enumerator = null;
                    }
                }
            }
            else
            {
                yield return this.CornerBox;
                yield break;
            }
        }

        protected ScrollParams GetScrollParams(ScrollDirection direction)
        {
            switch (direction)
            {
                case ScrollDirection.Left:
                case ScrollDirection.Right:
                    return this.HorzScrollParams;

                case ScrollDirection.Top:
                case ScrollDirection.Bottom:
                    return this.VertScrollParams;
            }
            return null;
        }

        protected ScrollParams GetScrollParams(Orientation orientation) => 
            (orientation == Orientation.Horizontal) ? this.HorzScrollParams : this.VertScrollParams;

        public static Rect GetStartDragAreaBounds(Point startDragPoint) => 
            new Rect(startDragPoint.X - 4.0, startDragPoint.Y - 4.0, 8.0, 8.0);

        public bool HasScrollBars() => 
            this.IsScrollable() && (this.ScrollBars != DevExpress.Xpf.Core.ScrollBars.None);

        protected virtual void InitScrollBars()
        {
            this.UpdateScrollParams();
            this.HorzScrollParams.AssignTo(this.HorzScrollBar);
            this.VertScrollParams.AssignTo(this.VertScrollBar);
        }

        protected virtual void InitScrollParams(ScrollParams horzScrollParams, ScrollParams vertScrollParams)
        {
        }

        public virtual bool IsScrollable() => 
            false;

        protected virtual void OnDragAndDropKeyDown(DXKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.CancelDragAndDrop();
                e.Handled = true;
            }
        }

        protected virtual void OnDragAndDropKeyUp(DXKeyEventArgs e)
        {
            if (this.Control.IsInDesignTool() && (e.Key == Key.Escape))
            {
                this.CancelDragAndDrop();
                e.Handled = true;
            }
        }

        protected virtual void OnIsMouseEnteredChanged()
        {
        }

        protected virtual void OnIsMouseLeftButtonDownChanged()
        {
        }

        protected virtual void OnKeyDown(DXKeyEventArgs e)
        {
        }

        protected virtual void OnKeyUp(DXKeyEventArgs e)
        {
        }

        protected virtual void OnLayoutUpdated()
        {
            if (this.IsLoaded && (this.NeedsUnloadedEvent && !this.Control.IsInVisualTree()))
            {
                this.OnUnloaded();
            }
        }

        protected virtual void OnLoaded()
        {
            this.IsLoaded = true;
        }

        private void OnLostMouseCapture(object sender, MouseEventArgs e)
        {
            this.ReleaseMouseCapture();
            this.OnMouseCaptureCancelled();
        }

        protected virtual void OnMouseCaptureCancelled()
        {
            if (this.IsMouseLeftButtonDown)
            {
                this.OnMouseLeftButtonUp(null);
            }
        }

        protected virtual void OnMouseDoubleClick(DXMouseButtonEventArgs e)
        {
        }

        protected virtual void OnMouseEnter(DXMouseEventArgs e)
        {
            this.IsMouseEntered = true;
        }

        protected virtual void OnMouseLeave(DXMouseEventArgs e)
        {
            this.IsMouseEntered = false;
            if (!this.IsMouseCaptured)
            {
                this.IsMouseLeftButtonDown = false;
            }
            if (this.IsDragAndDropPreparation)
            {
                Point? p = null;
                this.DragAndDropPrepared(false, p);
            }
            this.AutoScrollingDirection = ScrollDirection.None;
        }

        protected virtual void OnMouseLeftButtonDown(DXMouseButtonEventArgs e)
        {
            this.IsMouseLeftButtonDown = true;
            this.CheckMouseDoubleClick(e);
            if (!e.Handled)
            {
                if (this.WantsDragAndDrop(e.GetPosition(this.Control), out this._DragAndDropController))
                {
                    this.PrepareDragAndDrop(e.GetPosition(this.Control));
                }
                if (this.CaptureMouseOnDown)
                {
                    this.CaptureMouse(new Point?(e.GetPosition(this.Control)));
                }
                if (this.IsMouseCaptured || this.IsDragAndDropPreparation)
                {
                    e.Handled = true;
                }
            }
        }

        protected virtual void OnMouseLeftButtonUp(DXMouseButtonEventArgs e)
        {
            this.IsMouseLeftButtonDown = false;
            if (this.CaptureMouseOnDown)
            {
                this.ReleaseMouseCapture();
            }
            if (this.IsDragAndDropPreparation)
            {
                Point? p = null;
                this.DragAndDropPrepared(false, p);
            }
            if (this.IsDragAndDrop)
            {
                this.EndDragAndDrop((e != null) || this.AcceptDragAndDropWhenMouseCaptureCancelled);
                if (e != null)
                {
                    e.Handled = true;
                }
            }
        }

        protected virtual void OnMouseMove(DXMouseEventArgs e)
        {
            if ((this.IsDragAndDropPreparation && (Mouse.LeftButton == MouseButtonState.Pressed)) && !this.StartDragAreaBounds.Contains(e.GetPosition(this.Control)))
            {
                this.DragAndDropPrepared(true, new Point?(e.GetPosition(this.Control)));
                this.Control.UpdateLayout();
            }
            if (this.IsDragAndDrop)
            {
                this.DragAndDrop(e.GetPosition(this.Control));
            }
            this.CheckAutoScrolling(e.GetPosition(this.Control));
        }

        protected virtual void OnMouseWheel(DXMouseWheelEventArgs e)
        {
            if (!e.Handled && this.CanProcessMouseWheel)
            {
                e.Handled = this.ProcessMouseWheelScrolling(e.Delta);
            }
        }

        protected virtual void OnTouchDown(TouchEventArgs e)
        {
        }

        protected virtual void OnTouchEnter(TouchEventArgs e)
        {
        }

        protected virtual void OnTouchLeave(TouchEventArgs e)
        {
        }

        protected virtual void OnTouchMove(TouchEventArgs e)
        {
        }

        protected virtual void OnTouchUp(TouchEventArgs e)
        {
        }

        protected virtual void OnUnloaded()
        {
            this.IsLoaded = false;
        }

        protected void PrepareDragAndDrop(Point p)
        {
            this.StartDragPoint = p;
            if (((this.DragAndDropController == null) && this.IsImmediateDragAndDrop) || ((this.DragAndDropController != null) && this.DragAndDropController.IsImmediateDragAndDrop))
            {
                this.StartDragAndDrop(p);
            }
            else
            {
                this.IsDragAndDropPreparation = true;
            }
        }

        protected void ProcessKeyDown(DXKeyEventArgs e)
        {
            if (this.IsDragAndDrop)
            {
                this.OnDragAndDropKeyDown(e);
            }
            else
            {
                this.OnKeyDown(e);
            }
        }

        protected void ProcessKeyUp(DXKeyEventArgs e)
        {
            if (this.IsDragAndDrop)
            {
                this.OnDragAndDropKeyUp(e);
            }
            else
            {
                this.OnKeyUp(e);
            }
        }

        private bool ProcessMouseWheelScrolling(int delta)
        {
            ScrollParams vertScrollParams;
            if (!this.IsScrollable())
            {
                return false;
            }
            if (this.VertScrollParams.Enabled)
            {
                vertScrollParams = this.VertScrollParams;
            }
            else
            {
                if (!this.HorzScrollParams.Enabled)
                {
                    return false;
                }
                vertScrollParams = this.HorzScrollParams;
            }
            return this.ProcessMouseWheelScrolling(vertScrollParams, delta);
        }

        protected bool ProcessMouseWheelScrolling(ScrollParams scrollParams, int delta)
        {
            this.CanAnimateScrolling = true;
            try
            {
                scrollParams.Scroll(scrollParams.Position - Math.Round((double) ((((double) delta) / 120.0) * scrollParams.SmallStep)), false);
            }
            finally
            {
                this.CanAnimateScrolling = false;
            }
            return true;
        }

        public void ReleaseMouseCapture()
        {
            if (this.IsMouseCaptured)
            {
                MouseCaptureOwner.LostMouseCapture -= new MouseEventHandler(this.OnLostMouseCapture);
                MouseCaptureOwner = null;
                this.IsMouseCaptureChanging = true;
                try
                {
                    this.Control.ReleaseMouseCapture();
                }
                finally
                {
                    this.IsMouseCaptureChanging = false;
                }
            }
        }

        public void ResetScrollBarsVisibility()
        {
            this.IsHorzScrollBarVisible = false;
            this.IsVertScrollBarVisible = false;
        }

        protected virtual bool Scroll(Orientation orientation, double position) => 
            false;

        protected virtual void ScrollBarScroll(object sender, ScrollEventArgs e)
        {
            ScrollBar bar = (ScrollBar) sender;
            this.CanAnimateScrolling = e.ScrollEventType != ScrollEventType.ThumbTrack;
            try
            {
                this.Scroll(bar.Orientation, bar.Value);
            }
            finally
            {
                this.CanAnimateScrolling = false;
            }
        }

        protected virtual void ScrollParamsChange(ScrollParams sender)
        {
            if (this.ScrollParamsChanged != null)
            {
                this.ScrollParamsChanged(sender, EventArgs.Empty);
            }
        }

        protected virtual void ScrollParamsScrolling(object sender, ScrollKind kind)
        {
            if (sender == this.HorzScrollParams)
            {
                this.Scroll(Orientation.Horizontal, ((ScrollParams) sender).Position);
            }
            else if (sender == this.VertScrollParams)
            {
                this.Scroll(Orientation.Vertical, ((ScrollParams) sender).Position);
            }
        }

        private void StartAutoScrolling()
        {
            if (!this.IsAutoScrolling)
            {
                this._IsAutoScrolling = true;
                DispatcherTimer timer1 = new DispatcherTimer();
                timer1.Interval = TimeSpan.FromMilliseconds((double) this.AutoScrollingTimeInterval);
                this.AutoScrollingTimer = timer1;
                this.AutoScrollingTimer.Tick += new EventHandler(this.AutoScrollingTimerTick);
                this.AutoScrollingTimer.Start();
            }
        }

        protected virtual void StartDragAndDrop(Point p)
        {
            if (!this.CaptureMouse(new Point?(p)))
            {
                this._DragAndDropController = null;
            }
            else
            {
                if (this.StartDrag != null)
                {
                    this.StartDrag(this.Control, EventArgs.Empty);
                }
                this.AttachToKeyboardEventsForDragAndDrop();
                this.IsDragAndDrop = true;
                this._OriginalCursor = this.Control.Cursor;
                this.Control.Cursor = this.DragAndDropCursor;
                if (this.DragAndDropController != null)
                {
                    this.DragAndDropController.StartDragAndDrop(p);
                }
            }
        }

        private void StopAutoScrolling()
        {
            if (this.IsAutoScrolling)
            {
                this._IsAutoScrolling = false;
                this.AutoScrollingTimer.Stop();
                this.AutoScrollingTimer = null;
            }
        }

        protected virtual void UpdateScrollBars()
        {
        }

        public void UpdateScrollBarsVisibility()
        {
            if (this.HasScrollBars())
            {
                bool isHorzScrollBarVisible = this.IsHorzScrollBarVisible;
                bool isVertScrollBarVisible = this.IsVertScrollBarVisible;
                this.ResetScrollBarsVisibility();
                while (true)
                {
                    this.UpdateScrollParams();
                    bool flag3 = this.IsHorzScrollBarVisible;
                    bool flag4 = this.IsVertScrollBarVisible;
                    this.IsHorzScrollBarVisible = this.HorzScrollParams.Enabled;
                    this.IsVertScrollBarVisible = this.VertScrollParams.Enabled;
                    if ((this.IsHorzScrollBarVisible == flag3) && (this.IsVertScrollBarVisible == flag4))
                    {
                        if ((isHorzScrollBarVisible | isVertScrollBarVisible) && (!this.IsHorzScrollBarVisible && !this.IsVertScrollBarVisible))
                        {
                            this.IsHorzScrollBarVisible = isHorzScrollBarVisible;
                            this.IsVertScrollBarVisible = isVertScrollBarVisible;
                        }
                        return;
                    }
                }
            }
        }

        public bool UpdateScrolling()
        {
            if (!this.IsScrollable())
            {
                return false;
            }
            if (!this.HasScrollBars())
            {
                this.UpdateScrollParams();
            }
            else
            {
                this.InitScrollBars();
                this.UpdateScrollBars();
            }
            bool flag = this.Scroll(Orientation.Horizontal, this.HorzScrollParams.Position);
            return (this.Scroll(Orientation.Vertical, this.VertScrollParams.Position) | flag);
        }

        protected void UpdateScrollParams()
        {
            this.InitScrollParams(this.HorzScrollParams, this.VertScrollParams);
        }

        protected virtual bool WantsDragAndDrop(Point p, out DevExpress.Xpf.Core.DragAndDropController controller)
        {
            controller = null;
            return false;
        }

        public FrameworkElement Control =>
            this.IControl.Control;

        public DevExpress.Xpf.Core.IControl IControl { get; private set; }

        public bool IsLoaded { get; private set; }

        protected virtual bool NeedsUnloadedEvent =>
            false;

        public static UIElement MouseCaptureOwner { get; private set; }

        public bool IsMouseCaptured =>
            ReferenceEquals(MouseCaptureOwner, this.Control);

        protected bool IsMouseCaptureChanging { get; set; }

        public bool IsMouseEntered
        {
            get => 
                this._IsMouseEntered;
            set
            {
                if (this.IsMouseEntered != value)
                {
                    this._IsMouseEntered = value;
                    this.OnIsMouseEnteredChanged();
                }
            }
        }

        public bool IsMouseLeftButtonDown
        {
            get => 
                this._IsMouseLeftButtonDown;
            private set
            {
                if (this.IsMouseLeftButtonDown != value)
                {
                    this._IsMouseLeftButtonDown = value;
                    this.OnIsMouseLeftButtonDownChanged();
                }
            }
        }

        protected bool CaptureMouseOnDown { get; set; }

        protected virtual bool CanProcessMouseWheel =>
            true;

        public ScrollParams HorzScrollParams { get; private set; }

        public ScrollParams VertScrollParams { get; private set; }

        public bool IsHorzScrollBarVisible { get; protected set; }

        public bool IsVertScrollBarVisible { get; protected set; }

        public ScrollBar HorzScrollBar { get; protected set; }

        public ScrollBar VertScrollBar { get; protected set; }

        public DevExpress.Xpf.Core.CornerBox CornerBox { get; protected set; }

        public DevExpress.Xpf.Core.ScrollBars ScrollBars
        {
            get => 
                this._ScrollBars;
            protected internal set
            {
                if (this._ScrollBars != value)
                {
                    this._ScrollBars = value;
                    this.CheckScrollBars();
                    this.Control.InvalidateMeasure();
                }
            }
        }

        protected bool CanAnimateScrolling { get; set; }

        protected virtual Rect ScrollableAreaBounds =>
            RectHelper.New(this.Control.GetSize());

        protected internal bool AllowAutoScrolling
        {
            get => 
                this._AllowAutoScrolling;
            set
            {
                if (this._AllowAutoScrolling != value)
                {
                    this._AllowAutoScrolling = value;
                    if (!this.AllowAutoScrolling)
                    {
                        this.AutoScrollingDirection = ScrollDirection.None;
                    }
                }
            }
        }

        protected ScrollDirection AutoScrollingDirection
        {
            get => 
                this._AutoScrollingDirection;
            set
            {
                if (this._AutoScrollingDirection != value)
                {
                    this._AutoScrollingDirection = value;
                    if (this.AutoScrollingDirection == ScrollDirection.None)
                    {
                        this.StopAutoScrolling();
                    }
                    else
                    {
                        this.StartAutoScrolling();
                    }
                }
            }
        }

        protected bool IsAutoScrolling =>
            this._IsAutoScrolling;

        private DispatcherTimer AutoScrollingTimer { get; set; }

        public DevExpress.Xpf.Core.DragAndDropController DragAndDropController
        {
            get => 
                this._DragAndDropController;
            protected set => 
                this._DragAndDropController = value;
        }

        public bool IsDragAndDrop { get; private set; }

        protected virtual bool AcceptDragAndDropWhenMouseCaptureCancelled =>
            false;

        protected virtual Cursor DragAndDropCursor =>
            ((this.DragAndDropController == null) || (this.DragAndDropController.DragCursor == null)) ? this.Control.Cursor : this.DragAndDropController.DragCursor;

        protected bool IsDragAndDropPreparation { get; private set; }

        protected virtual bool IsImmediateDragAndDrop =>
            false;

        protected Rect StartDragAreaBounds =>
            GetStartDragAreaBounds(this.StartDragPoint);

        protected Point StartDragPoint { get; private set; }

    }
}

