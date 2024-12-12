namespace DevExpress.Utils
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class BeginMouseDragHelperState : MouseHandlerState
    {
        private readonly Point initialPoint;
        private readonly MouseHandlerState dragState;

        public BeginMouseDragHelperState(MouseHandler mouseHandler, MouseHandlerState dragState, Point point) : base(mouseHandler)
        {
            if (dragState == null)
            {
                throw new ArgumentNullException();
            }
            this.DragAllowed = true;
            this.dragState = dragState;
            this.initialPoint = point;
        }

        private bool IsDragStarted(MouseEventArgs e)
        {
            Size dragSize = base.MouseHandler.GetDragSize();
            return ((Math.Abs((int) (this.initialPoint.X - e.X)) > dragSize.Width) || (Math.Abs((int) (this.initialPoint.Y - e.Y)) > dragSize.Height));
        }

        public override void OnMouseCaptureChanged()
        {
            if (this.DragAllowed)
            {
                this.DragState.OnMouseCaptureChanged();
            }
        }

        public override void OnMouseMove(MouseEventArgs e)
        {
            if (this.DragAllowed && this.IsDragStarted(e))
            {
                base.MouseHandler.SwitchStateCore(this.DragState, new Point(e.X, e.Y));
                this.DragState.OnMouseMove(e);
            }
        }

        public override void OnMouseUp(MouseEventArgs e)
        {
            MouseButtons button = e.Button;
            if ((button == MouseButtons.Left) || ((button == MouseButtons.Right) && this.CancelOnRightMouseUp))
            {
                base.MouseHandler.SwitchToDefaultState();
            }
        }

        public override void OnMouseWheel(MouseEventArgs e)
        {
            if (this.DragAllowed)
            {
                base.MouseHandler.SwitchStateCore(this.DragState, Point.Empty);
                this.DragState.OnMouseWheel(e);
            }
        }

        public override bool OnPopupMenu(MouseEventArgs e)
        {
            if (!this.CancelOnPopupMenu)
            {
                return base.OnPopupMenu(e);
            }
            base.MouseHandler.SwitchToDefaultState();
            return false;
        }

        public bool DragAllowed { get; set; }

        public MouseHandlerState DragState =>
            this.dragState;

        public bool CancelOnPopupMenu { get; set; }

        public bool CancelOnRightMouseUp { get; set; }

        public override bool StopClickTimerOnStart =>
            false;
    }
}

