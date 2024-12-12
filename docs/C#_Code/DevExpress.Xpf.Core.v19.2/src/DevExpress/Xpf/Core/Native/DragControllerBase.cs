namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;

    public abstract class DragControllerBase
    {
        private bool shouldStartDrag;
        private bool suppressMouseMove;

        protected DragControllerBase();
        protected virtual bool CanStartDrag(FrameworkElement dragChild);
        protected abstract Point GetMousePosition();
        protected virtual bool IsMouseLeftButtonDownOnDragChild(object sender, MouseButtonEventArgs e);
        protected abstract void OnDrag();
        protected virtual void OnDragFinished();
        protected abstract void OnDragStarted();
        protected abstract void OnDragStopped();
        protected abstract void OnDrop();
        private void OnMouseLeave(object sender, MouseEventArgs e);
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e);
        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e);
        private void OnMouseLeftButtonUpCore(Action finishDrag);
        private void OnMouseMove(object sender, MouseEventArgs e);
        private bool ShouldStartDrag(FrameworkElement dragChild, MouseEventArgs e);
        protected internal void StartDrag(FrameworkElement dragChild, Point? dragStartPoint = new Point?());
        protected internal void StopDrag(bool doDrop);
        protected void Subscribe(FrameworkElement dragChild);
        protected void Unsubscribe(FrameworkElement dragChild);

        protected virtual bool StartDragDropOnHandledMouseEvents { get; }

        protected FrameworkElement DragChild { get; private set; }

        protected Point StartPoint { get; private set; }

        protected Point CurrentPoint { get; private set; }

        protected bool IsDragStarted { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DragControllerBase.<>c <>9;
            public static Action<Action> <>9__25_0;

            static <>c();
            internal void <OnMouseLeftButtonUpCore>b__25_0(Action x);
        }
    }
}

