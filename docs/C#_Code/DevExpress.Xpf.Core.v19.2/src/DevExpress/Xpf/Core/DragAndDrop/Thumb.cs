namespace DevExpress.Xpf.Core.DragAndDrop
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    [DefaultEvent("DragDelta"), Localizability(LocalizationCategory.NeverLocalize), Browsable(false)]
    public class Thumb : SLThumb
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty IsDraggingProperty;
        public static readonly DependencyProperty AllowDragProperty;
        public static readonly RoutedEvent DragCompletedEvent;
        public static readonly RoutedEvent DragDeltaEvent;
        public static readonly RoutedEvent DragStartedEvent;
        private Point previousScreenCoordPosition;

        [Category("Behavior")]
        public event DragCompletedEventHandler DragCompleted
        {
            add
            {
                base.AddHandler(DragCompletedEvent, value);
            }
            remove
            {
                base.RemoveHandler(DragCompletedEvent, value);
            }
        }

        [Category("Behavior")]
        public event DragDeltaEventHandler DragDelta
        {
            add
            {
                base.AddHandler(DragDeltaEvent, value);
            }
            remove
            {
                base.RemoveHandler(DragDeltaEvent, value);
            }
        }

        [Category("Behavior")]
        public event DragStartedEventHandler DragStarted
        {
            add
            {
                base.AddHandler(DragStartedEvent, value);
            }
            remove
            {
                base.RemoveHandler(DragStartedEvent, value);
            }
        }

        static Thumb()
        {
            IsDraggingProperty = DependencyPropertyManager.Register("IsDragging", typeof(bool), typeof(DevExpress.Xpf.Core.DragAndDrop.Thumb), new PropertyMetadata(false, (d, e) => ((DevExpress.Xpf.Core.DragAndDrop.Thumb) d).OnIsDraggingPropertyChanged()));
            AllowDragProperty = DependencyProperty.Register("AllowDrag", typeof(bool), typeof(DevExpress.Xpf.Core.DragAndDrop.Thumb), new PropertyMetadata(true, (d, e) => ((DevExpress.Xpf.Core.DragAndDrop.Thumb) d).OnAllowDragPropertyChanged()));
            DragCompletedEvent = EventManager.RegisterRoutedEvent("DragCompleted", RoutingStrategy.Direct, typeof(DragCompletedEventHandler), typeof(DevExpress.Xpf.Core.DragAndDrop.Thumb));
            DragDeltaEvent = EventManager.RegisterRoutedEvent("DragDelta", RoutingStrategy.Direct, typeof(DragDeltaEventHandler), typeof(DevExpress.Xpf.Core.DragAndDrop.Thumb));
            DragStartedEvent = EventManager.RegisterRoutedEvent("DragStarted", RoutingStrategy.Direct, typeof(DragStartedEventHandler), typeof(DevExpress.Xpf.Core.DragAndDrop.Thumb));
        }

        public Thumb()
        {
            base.Focusable = false;
            this.ClearDragProperty();
        }

        public void CancelDrag()
        {
            if (this.IsDragging)
            {
                this.RaiseDragCompleted(this.previousScreenCoordPosition.X - this.StartDragPointOnScreen.X, this.previousScreenCoordPosition.Y - this.StartDragPointOnScreen.Y, true);
                this.ClearDragProperty();
            }
        }

        protected virtual void ClearDragProperty()
        {
            Point point = new Point();
            this.StartDragPointOnThumb = point;
            point = new Point();
            this.StartDragPointOnScreen = point;
            this.IsDragging = false;
            MouseHelper.ReleaseCapture(this);
        }

        protected virtual void CompleteDrag(MouseButtonEventArgs e)
        {
            if (!this.SuppressHandleMouseEvents)
            {
                e.Handled = true;
            }
            Point position = e.GetPosition(null);
            this.RaiseDragCompleted(position.X - this.StartDragPointOnScreen.X, position.Y - this.StartDragPointOnScreen.Y, false);
            this.ClearDragProperty();
        }

        private void GoToState(string stateName)
        {
            VisualStateManager.GoToState(this, stateName, false);
        }

        protected virtual void MoveDrag(MouseEventArgs e)
        {
            Point position = e.GetPosition(this);
            Point point2 = e.GetPosition(null);
            if (point2 != this.previousScreenCoordPosition)
            {
                this.previousScreenCoordPosition = point2;
                if (!this.SuppressHandleMouseEvents)
                {
                    e.SetHandled(true);
                }
                this.RaiseDragDelta(position.X - this.StartDragPointOnThumb.X, position.Y - this.StartDragPointOnThumb.Y);
            }
        }

        protected virtual void OnAllowDragPropertyChanged()
        {
            if (this.IsDragging)
            {
                this.CancelDrag();
            }
            else
            {
                this.ClearDragProperty();
            }
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            this.UpdateVisualState();
        }

        protected virtual void OnIsDraggingPropertyChanged()
        {
            this.UpdateVisualState();
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            this.UpdateVisualState();
        }

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            if (ReferenceEquals(MouseHelper.Captured, this))
            {
                this.CancelDrag();
            }
            base.OnLostMouseCapture(e);
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            this.UpdateVisualState();
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            this.UpdateVisualState();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (!this.IsDragging && this.AllowDrag)
            {
                this.StartDrag(e);
            }
            this.UpdateVisualState();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (ReferenceEquals(MouseHelper.Captured, this) && (this.IsDragging && this.AllowDrag))
            {
                this.CompleteDrag(e);
            }
            base.OnMouseLeftButtonUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.IsDragging && this.AllowDrag)
            {
                if (this.IsDragging)
                {
                    this.MoveDrag(e);
                }
                else
                {
                    this.ClearDragProperty();
                }
            }
        }

        protected virtual void RaiseDragCompleted(double horizontalChange, double verticalChange, bool canceled)
        {
            DragCompletedEventArgs e = new DragCompletedEventArgs(horizontalChange, verticalChange, canceled);
            e.RoutedEvent = DragCompletedEvent;
            base.RaiseEvent(e);
        }

        protected virtual void RaiseDragDelta(double horizontalChange, double verticalChange)
        {
            DragDeltaEventArgs e = new DragDeltaEventArgs(horizontalChange, verticalChange);
            e.RoutedEvent = DragDeltaEvent;
            base.RaiseEvent(e);
        }

        protected virtual void RaiseDragStarted(double horizontalOffset, double verticalOffset)
        {
            DragStartedEventArgs e = new DragStartedEventArgs(horizontalOffset, verticalOffset);
            e.RoutedEvent = DragStartedEvent;
            base.RaiseEvent(e);
        }

        protected virtual void StartDrag(MouseButtonEventArgs e)
        {
            if (e != null)
            {
                if (!this.SuppressHandleMouseEvents)
                {
                    e.Handled = true;
                }
                this.StartDragPointOnThumb = e.GetPosition(this);
                this.StartDragPointOnScreen = e.GetPosition(null);
            }
            this.IsDragging = MouseHelper.Capture(this);
            this.previousScreenCoordPosition = this.StartDragPointOnScreen;
            this.RaiseDragStarted(this.StartDragPointOnThumb.X, this.StartDragPointOnThumb.Y);
        }

        private void UpdateVisualState()
        {
            if (this.IsDragging)
            {
                this.GoToState("Dragging");
            }
            else
            {
                this.GoToState("NoDragging");
                if (base.IsMouseOver)
                {
                    this.GoToState("MouseOver");
                }
                else
                {
                    this.GoToState("Normal");
                }
            }
        }

        public bool IsDragging
        {
            get => 
                (bool) base.GetValue(IsDraggingProperty);
            protected set => 
                base.SetValue(IsDraggingProperty, value);
        }

        public bool AllowDrag
        {
            get => 
                (bool) base.GetValue(AllowDragProperty);
            set => 
                base.SetValue(AllowDragProperty, value);
        }

        public Point StartDragPointOnThumb { get; protected set; }

        public Point StartDragPointOnScreen { get; protected set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool SuppressHandleMouseEvents { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DevExpress.Xpf.Core.DragAndDrop.Thumb.<>c <>9 = new DevExpress.Xpf.Core.DragAndDrop.Thumb.<>c();

            internal void <.cctor>b__54_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Core.DragAndDrop.Thumb) d).OnIsDraggingPropertyChanged();
            }

            internal void <.cctor>b__54_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Core.DragAndDrop.Thumb) d).OnAllowDragPropertyChanged();
            }
        }
    }
}

