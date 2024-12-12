namespace DevExpress.Xpf.Editors.RangeControl.Internal
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.RangeControl;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;

    public class RangeControlController
    {
        private const double MinManipulationDelta = 4.0;
        private const double MinTouchWidth = 10.0;
        private const double Deceleration = 0.001;
        private const double MinExpansionDelta = 0.2;
        private bool isAutoScrollInProcess;
        private double CummulativeDelta;
        private double OldExpansion;
        private Locker prepareSelectionLocker = new Locker();
        private double lastMovePosition = double.NaN;
        private InputDevice draggingDevice;
        private DateTime lastClickTime;
        private Point lastClickPosition;
        private Point currentDownPosition;
        private List<InputDevice> activeDevices = new List<InputDevice>();
        private DispatcherTimer holdingTimer;

        public RangeControlController(FrameworkElement clientContainer, DevExpress.Xpf.Editors.RangeControl.RangeControl rangeControl, FrameworkElement manipulationContainer)
        {
            this.ClientContainer = clientContainer;
            this.Owner = rangeControl;
            this.ManipulationContainer = manipulationContainer;
            Stylus.SetIsPressAndHoldEnabled(this.ClientContainer, false);
            this.SubscribeEvents();
        }

        private void AddInputDevice(InputDevice device)
        {
            this.activeDevices.Add(device);
        }

        private double CalcMoveDelta(double position)
        {
            if (double.IsNaN(this.lastMovePosition))
            {
                this.lastMovePosition = position;
            }
            double num = position - this.lastMovePosition;
            this.lastMovePosition = position;
            return num;
        }

        private bool CanProcessClick(Point upPosition) => 
            (this.currentDownPosition == upPosition) && !this.IsHolding;

        private bool CanProcessManipulationDelta(ManipulationDeltaEventArgs e)
        {
            if (this.HitTestType == RangeControlHitTestType.ThumbsArea)
            {
                return false;
            }
            this.CummulativeDelta += Math.Abs(e.DeltaManipulation.Translation.X);
            return (this.CummulativeDelta > 4.0);
        }

        private bool CanProcessZoom(double newExpansion) => 
            (Math.Abs(newExpansion) > 0.2) && !(this.OldExpansion == newExpansion);

        private bool CanStopInertia() => 
            this.IsStopIntertia || (this.State != RangeControlStateType.Scrolling);

        private void DetectAutoScroll(double position)
        {
            if (this.Owner.AllowScroll)
            {
                bool flag = this.Owner.IsPositionOutOfBounds(position);
                if (flag && !this.IsAutoScrollInProcess)
                {
                    this.ProcessAutoScroll(position);
                }
                this.IsAutoScrollInProcess = flag;
            }
        }

        private RangeControlStateType GetStateFromHitTest() => 
            (this.HitTestType == RangeControlHitTestType.SelectionArea) ? RangeControlStateType.MoveSelection : RangeControlStateType.Selection;

        private void InitializeDragging(Point position, InputDevice device)
        {
            this.State = RangeControlStateType.ThumbDragging;
            this.Owner.ThumbDragStarted(this.currentDownPosition, position.X - this.currentDownPosition.X);
            this.IsDragStarted = true;
            this.draggingDevice = device;
        }

        private bool IsDoubleClick(Point clickPosition, bool isMouse)
        {
            DateTime now = DateTime.Now;
            bool flag = isMouse ? this.IsMouseClickPositionsEquals(clickPosition, this.lastClickPosition) : this.IsTouchClickPositions(clickPosition, this.lastClickPosition);
            bool flag2 = ((now - this.lastClickTime).TotalMilliseconds < 1000.0) & flag;
            this.lastClickTime = now;
            this.lastClickPosition = clickPosition;
            return flag2;
        }

        private bool IsMouseClickPositionsEquals(Point current, Point last) => 
            last == current;

        private bool IsMoveDeltaChanged() => 
            !this.MoveDelta.AreClose(0.0);

        private bool IsTouchClickPositions(Point current, Point last)
        {
            double num2 = Math.Abs((double) (current.Y - last.Y));
            return ((Math.Abs((double) (current.X - last.X)) < 10.0) && (num2 < 10.0));
        }

        private void LostMouseCapture(object sender, MouseEventArgs e)
        {
            this.ResetCursor();
        }

        internal void ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            e.Handled = true;
            if (e.IsInertial && ((e.FinalVelocities.LinearVelocity.X != 0.0) && (this.State == RangeControlStateType.Scrolling)))
            {
                e.Cancel();
            }
            else
            {
                this.ResetCore();
            }
        }

        internal void ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            e.Handled = true;
            this.HasActiveManipulation = true;
            if (this.CanProcessManipulationDelta(e) && !this.IsAutoScrollInProcess)
            {
                this.StopHoldingTimer();
                this.ProcessManipulationDelta(e);
            }
        }

        internal void ManipulationInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        {
            e.Handled = true;
            e.TranslationBehavior.DesiredDeceleration = 0.001;
        }

        internal void ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = this.ManipulationContainer;
            e.Mode = ManipulationModes.Scale | ManipulationModes.Translate;
            e.Handled = true;
        }

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.ActiveDevice = InputDeviceType.Mouse;
            if (this.State != RangeControlStateType.ThumbDragging)
            {
                this.ClientContainer.CaptureMouse();
                this.ProcessDown(e.GetPosition(this.ClientContainer), e.Device);
            }
        }

        private void MouseLeave(object sender, MouseEventArgs e)
        {
            this.ResetCursor();
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            double x = e.GetPosition(this.Owner).X;
            this.UpdateCursor(e.GetPosition(this.ClientContainer));
            this.UpdateMoveDelta(x);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DetectAutoScroll(x);
                if (this.IsMoveDeltaChanged() && !this.IsAutoScrollInProcess)
                {
                    this.ProcessMouseMove(e);
                }
            }
        }

        private void MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.ProcessUp(e.GetPosition(this.ClientContainer), e.Device, true);
            this.ClientContainer.ReleaseMouseCapture();
            this.ResetCore();
        }

        private void MouseWheel(object sender, MouseWheelEventArgs e)
        {
            this.ProcessWheel(e);
        }

        private void PrepareResizeSelection()
        {
            if (this.State == RangeControlStateType.Selection)
            {
                this.Owner.SelectByHitTest();
                this.Owner.PrepareResizeSelection();
            }
        }

        private void ProcessAutoScroll(double position)
        {
            if ((this.State == RangeControlStateType.ThumbDragging) || (this.State == RangeControlStateType.Selection))
            {
                this.Owner.StartAutoScroll(position, true);
            }
            else if (this.State == RangeControlStateType.MoveSelection)
            {
                this.Owner.StartAutoScroll(position, false);
            }
        }

        private void ProcessClick()
        {
            this.State = RangeControlStateType.Click;
            this.LastClickTime = DateTime.Now;
            if (this.HitTestType == RangeControlHitTestType.LabelArea)
            {
                this.Owner.SelectGroupInterval();
            }
            else
            {
                this.Owner.SelectByHitTest();
            }
        }

        private void ProcessDelta(ManipulationDeltaEventArgs e)
        {
            double x = e.DeltaManipulation.Translation.X;
            switch (this.State)
            {
                case RangeControlStateType.MoveSelection:
                    if (x.AreClose(0.0))
                    {
                        break;
                    }
                    this.Owner.MoveSelection(x);
                    return;

                case RangeControlStateType.Zoom:
                    this.ProcessZoom(e);
                    break;

                case RangeControlStateType.Scrolling:
                    this.Owner.ScrollByDelta(-x);
                    return;

                case RangeControlStateType.Selection:
                    this.Owner.ResizeSelection(e.ManipulationOrigin.X, true);
                    return;

                default:
                    return;
            }
        }

        private void ProcessDoubleClick()
        {
            this.State = RangeControlStateType.DoubleClick;
            this.Owner.ZoomByDoubleTap(this.currentDownPosition);
        }

        private void ProcessDown(Point position, InputDevice device)
        {
            this.AddInputDevice(device);
            this.currentDownPosition = position;
            this.SetupCurrentState();
            if (this.ActiveDevice == InputDeviceType.Touch)
            {
                this.StartHoldingTimer();
            }
            else
            {
                this.PrepareResizeSelection();
            }
        }

        private void ProcessDragging(Point position, InputDevice device)
        {
            double totalMilliseconds = (DateTime.Now - this.LastClickTime).TotalMilliseconds;
            if ((this.HitTestType == RangeControlHitTestType.ThumbsArea) && ((this.activeDevices.Count > 0) && (!this.IsHolding && (totalMilliseconds > this.MinTapTime))))
            {
                if (!this.IsDragStarted)
                {
                    this.InitializeDragging(position, device);
                }
                if (ReferenceEquals(device, this.draggingDevice))
                {
                    this.Owner.ProcessSelectionResizing(position.X);
                }
            }
        }

        private void ProcessHolding()
        {
            if (this.HitTestType == RangeControlHitTestType.LabelArea)
            {
                this.State = RangeControlStateType.Selection;
                this.Owner.SelectGroupInterval();
            }
            else
            {
                this.IsHolding = true;
                if (this.Owner.IsInsideSelectionArea(this.currentDownPosition))
                {
                    this.ProcessHoldInsideSelection();
                }
                else
                {
                    this.ProcessHoldOutsideSelection();
                }
            }
        }

        private void ProcessHoldInsideSelection()
        {
            this.State = RangeControlStateType.MoveSelection;
            this.HitTestType = RangeControlHitTestType.SelectionArea;
            this.Owner.PrepareMoveSelection();
        }

        private void ProcessHoldOutsideSelection()
        {
            this.State = RangeControlStateType.Selection;
            this.HitTestType = RangeControlHitTestType.ScrollableArea;
            this.Owner.SelectByHitTest();
            this.Owner.PrepareResizeSelection();
        }

        internal void ProcessManipulationDelta(ManipulationDeltaEventArgs e)
        {
            if (e.IsInertial && this.CanStopInertia())
            {
                e.Complete();
            }
            else
            {
                this.ProcessDelta(e);
            }
        }

        private void ProcessMouseMove(MouseEventArgs e)
        {
            RangeControlStateType state = this.State;
            if (state == RangeControlStateType.MoveSelection)
            {
                if (!this.prepareSelectionLocker.IsLocked)
                {
                    ChangeCursorHelper.SetHandCursor();
                    this.prepareSelectionLocker.Lock();
                    this.Owner.PrepareMoveSelection();
                }
                this.Owner.MoveSelection(this.MoveDelta);
            }
            else if (state == RangeControlStateType.Selection)
            {
                this.Owner.ResizeSelection(e.GetPosition(this.ClientContainer).X, false);
            }
            else if (state == RangeControlStateType.ThumbDragging)
            {
                this.ProcessDragging(e.GetPosition(this.ClientContainer), e.Device);
            }
        }

        private void ProcessUp(Point position, InputDevice device, bool isMouse)
        {
            if ((this.State != RangeControlStateType.Zoom) && (this.State != RangeControlStateType.Normal))
            {
                if (this.IsDoubleClick(position, isMouse))
                {
                    this.ProcessDoubleClick();
                }
                else if (this.CanProcessClick(position))
                {
                    this.ProcessClick();
                }
            }
            this.RemoveInputDevice(device);
        }

        private void ProcessWheel(MouseWheelEventArgs e)
        {
            this.Owner.ZoomByWheel(e.Delta, e.GetPosition(this.ClientContainer).X);
        }

        private void ProcessZoom(ManipulationDeltaEventArgs e)
        {
            double x = e.DeltaManipulation.Expansion.X;
            this.OldExpansion = (this.OldExpansion == 0.0) ? x : this.OldExpansion;
            if (this.CanProcessZoom(x))
            {
                double scale = e.DeltaManipulation.Scale.X + e.DeltaManipulation.Scale.Y;
                this.Owner.ZoomByPinch(scale, e.ManipulationOrigin.X);
            }
            this.OldExpansion = x;
        }

        private void RemoveInputDevice(InputDevice device)
        {
            if (this.activeDevices.Contains(device))
            {
                this.activeDevices.Remove(device);
            }
        }

        public void Reset()
        {
            this.ResetCore();
        }

        private void ResetCore()
        {
            this.Owner.OnControllerReset(this.State);
            ChangeCursorHelper.ResetCursorToDefault();
            this.IsAutoScrollInProcess = false;
            this.State = RangeControlStateType.Normal;
            this.StopHoldingTimer();
            this.prepareSelectionLocker.Unlock();
            this.IsHolding = false;
            this.IsStopIntertia = false;
            this.HasActiveManipulation = true;
            this.OldExpansion = 0.0;
            this.CummulativeDelta = 0.0;
            this.IsDragStarted = false;
            this.MoveDelta = 0.0;
            this.lastMovePosition = double.NaN;
        }

        private void ResetCursor()
        {
            ChangeCursorHelper.ResetCursorToDefault();
        }

        private void SetupCurrentState()
        {
            this.UpdateHitTest();
            if (this.activeDevices.Count > 1)
            {
                this.State = RangeControlStateType.Zoom;
                this.StopHoldingTimer();
                this.Owner.PrepareZoom();
            }
            else if (this.HitTestType == RangeControlHitTestType.ThumbsArea)
            {
                this.State = RangeControlStateType.ThumbDragging;
            }
            else
            {
                this.State = (this.ActiveDevice == InputDeviceType.Touch) ? RangeControlStateType.Scrolling : this.GetStateFromHitTest();
            }
        }

        private void StartHoldingTimer()
        {
            this.StopHoldingTimer();
            DispatcherTimer timer1 = new DispatcherTimer();
            timer1.Interval = new TimeSpan(0, 0, 0, 0, this.HoldingDelay);
            this.holdingTimer = timer1;
            this.holdingTimer.Tick += delegate (object s, EventArgs e) {
                this.holdingTimer.Stop();
                this.ProcessHolding();
            };
            this.holdingTimer.Start();
        }

        private void StopHoldingTimer()
        {
            if (this.holdingTimer != null)
            {
                this.holdingTimer.Stop();
                this.holdingTimer = null;
            }
        }

        public void StopInetria()
        {
            this.IsStopIntertia = true;
        }

        private void SubscribeEvents()
        {
            this.ClientContainer.MouseDown += new MouseButtonEventHandler(this.MouseDown);
            this.ClientContainer.MouseUp += new MouseButtonEventHandler(this.MouseUp);
            this.ClientContainer.MouseMove += new MouseEventHandler(this.MouseMove);
            this.ClientContainer.MouseWheel += new MouseWheelEventHandler(this.MouseWheel);
            this.ClientContainer.TouchDown += new EventHandler<TouchEventArgs>(this.TouchDown);
            this.ClientContainer.TouchUp += new EventHandler<TouchEventArgs>(this.TouchUp);
            this.ClientContainer.TouchMove += new EventHandler<TouchEventArgs>(this.TouchMove);
            this.ClientContainer.LostMouseCapture += new MouseEventHandler(this.LostMouseCapture);
            this.ClientContainer.MouseLeave += new MouseEventHandler(this.MouseLeave);
            this.ManipulationContainer.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(this.ManipulationDelta);
            this.ManipulationContainer.ManipulationInertiaStarting += new EventHandler<ManipulationInertiaStartingEventArgs>(this.ManipulationInertiaStarting);
            this.ManipulationContainer.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(this.ManipulationCompleted);
            this.ManipulationContainer.ManipulationStarting += new EventHandler<ManipulationStartingEventArgs>(this.ManipulationStarting);
        }

        private void TouchDown(object sender, TouchEventArgs e)
        {
            this.ActiveDevice = InputDeviceType.Touch;
            if (this.State != RangeControlStateType.ThumbDragging)
            {
                this.ClientContainer.CaptureTouch(e.TouchDevice);
                this.ProcessDown(e.GetTouchPoint(this.ClientContainer).Position, e.Device);
            }
        }

        private void TouchMove(object sender, TouchEventArgs e)
        {
            double x = e.GetTouchPoint(this.Owner).Position.X;
            this.UpdateMoveDelta(x);
            this.DetectAutoScroll(x);
            if (this.State == RangeControlStateType.ThumbDragging)
            {
                this.StopHoldingTimer();
                this.DetectAutoScroll(x);
                if (this.IsMoveDeltaChanged() && !this.IsAutoScrollInProcess)
                {
                    this.ProcessDragging(e.GetTouchPoint(this.ClientContainer).Position, e.Device);
                }
            }
        }

        private void TouchUp(object sender, TouchEventArgs e)
        {
            this.ProcessUp(e.GetTouchPoint(this.ClientContainer).Position, e.Device, false);
            this.ClientContainer.ReleaseTouchCapture(e.TouchDevice);
            if (!this.HasActiveManipulation)
            {
                this.ResetCore();
            }
        }

        private void UpdateCursor(Point position)
        {
            if (this.State != RangeControlStateType.MoveSelection)
            {
                if (this.Owner.HitTest(position) == RangeControlHitTestType.ThumbsArea)
                {
                    ChangeCursorHelper.SetResizeCursor();
                }
                else
                {
                    this.ResetCursor();
                }
            }
        }

        private void UpdateHitTest()
        {
            this.HitTestType = this.Owner.HitTest(this.currentDownPosition);
        }

        private void UpdateMoveDelta(double newPosition)
        {
            this.MoveDelta = this.CalcMoveDelta(newPosition);
        }

        public DevExpress.Xpf.Editors.RangeControl.RangeControl Owner { get; private set; }

        public RangeControlStateType State { get; private set; }

        public RangeControlHitTestType HitTestType { get; private set; }

        private FrameworkElement ClientContainer { get; set; }

        private FrameworkElement ManipulationContainer { get; set; }

        private bool IsHolding { get; set; }

        private bool IsDragStarted { get; set; }

        private DateTime LastClickTime { get; set; }

        private int MinTapTime =>
            300;

        private int HoldingDelay =>
            150;

        private double MoveDelta { get; set; }

        private bool IsStopIntertia { get; set; }

        private bool HasActiveManipulation { get; set; }

        private InputDeviceType ActiveDevice { get; set; }

        private bool IsAutoScrollInProcess
        {
            get => 
                this.isAutoScrollInProcess;
            set
            {
                if (this.isAutoScrollInProcess != value)
                {
                    this.isAutoScrollInProcess = value;
                    if (!value)
                    {
                        this.Owner.StopAutoScroll();
                    }
                }
            }
        }
    }
}

